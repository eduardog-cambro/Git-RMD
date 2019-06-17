using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cambro
{
    public partial class _runDataHAITIAN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // redirect is not authorized
            if (!_security.checkSecurity("forms/machineRunData.aspx").isAuthorized)
            {
                Response.Redirect(_global.rootURL() + "default.aspx?target=NOTAUTHORIZED", true);
            }

            if (!Page.IsPostBack)
            {
                int intMachineDataHeaderId = 0;
                if (Request.QueryString["id"] != null)
                {
                    intMachineDataHeaderId = _global.Fld2Int(Request.QueryString["id"]);
                }

                int intAction = 0;
                if (Request.QueryString["action"] != null)
                {
                    intAction = _global.Fld2Int(Request.QueryString["action"]);
                }

                FormLoad(intMachineDataHeaderId, intAction);
            }

            SetTabOrder();

            _global.ButtonClickEventGovenor(btnSaveTop);
            _global.ButtonClickEventGovenor(btnSaveBottom);
        }

        private void FormLoad(int MachineDataHeaderId)
        {
            FormLoad(MachineDataHeaderId, 0);
        }
        private void FormLoad(int MachineDataHeaderId, int Action)
        {
            hMachineDataHeaderId.Value = MachineDataHeaderId.ToString();
            hMachineType.Value = "HAITIAN";
            hPlantNumber.Value = _security.plantNumber();

            //get data
            GetHeader(Action);
            GetBarrelTemperatures();
            GetGasAssistData();
            GetHotTipController();
            GetInjectionProfile();
            GetMoldCoolingTemps();
            GetNozzleInformation();
            GetRecoveryClampProfile();
            GetReferenceData();
            GetValveGateData();

            if (Action == runSheetActions.IsCopy)
            {
                hMachineDataHeaderId.Value = "";
                hBarrelTemperaturesRecId.Value = "";
                hGasAssistDataRecId.Value = "";
                hHotTipControllerRecId.Value = "";
                hInjectionProfileRecId.Value = "";
                hMoldCoolingTempsRecId.Value = "";
                hNozzleInformationRecId.Value = "";
                hRecoveryClampProfileRecId.Value = "";
                hReferenceDataRecId.Value = "";
                hValveGateDataRecId.Value = "";

                txtRunComments1.Text = "";
                txtRunCommentsDate1.Text = "";
                txtRunCycleTime1.Text = "";
                txtRunShotWeight1.Text = "";
                txtRunComments2.Text = "";
                txtRunCommentsDate2.Text = "";
                txtRunCycleTime2.Text = "";
                txtRunShotWeight2.Text = "";
                txtRunComments3.Text = "";
                txtRunCommentsDate3.Text = "";
                txtRunCycleTime3.Text = "";
                txtRunShotWeight3.Text = "";
                lblDateCreated.InnerHtml = System.DateTime.Today.ToString("MM/dd/yyyy");
            }
        }

        protected void btnGetTools_Click(object sender, EventArgs e)
        {
            //skip if form is protected
            if (hProtect.Value == "1") { return; } 

            if (!GetToolNumbers(txtPartNumber.Text))
            {
                string strScript = "alert('No tools found for this product number.');";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", strScript, true);
            }
        }
        protected void ddlToolNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            //skip if form is protected
            if (hProtect.Value == "1") { return; }

            if (!GetMachineNumbers(txtPartNumber.Text, ddlToolNumber.Text))
            {
                string strScript = "alert('No machines found for this tool number.');";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", strScript, true);
            }
        }
        protected void ddlMachineNumber_SelectedIndexChanged(object sender, EventArgs e)
        {   
            //skip if form is protected
            if (hProtect.Value == "1") { return; }

            InitAutoLoad(3);

            string machineNumber = ((ddlMachineNumber.Text != "") ? ddlMachineNumber.Text : "*blank*");

            string strSQL = "get_MachineDefaults ";
            strSQL += "@userId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@PlantNumber = " + _global.Str2Fld(hPlantNumber.Value) + ", ";
            strSQL += "@MachineType = " + _global.Str2Fld(hMachineType.Value) + ", ";
            strSQL += "@PartNumber = " + _global.Str2Fld(txtPartNumber.Text) + ", ";
            strSQL += "@ToolNumber = " + _global.Str2Fld(ddlToolNumber.Text) + ", ";
            strSQL += "@MachineNumber = " + _global.Str2Fld(machineNumber);

            DataSet oDs = _shared.getDataSet(strSQL, _global.conString());
            if (oDs.Tables[0].Rows.Count == 0)
            {
                if (machineNumber != "*blank*")
                {
                    string strScript = "alert('No machines defaults found for this machine number.');";
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", strScript, true);
                }
                else
                {
                    lblMaterialNumber.InnerHtml = "";
                    lblMaterialDescription.InnerHtml = "";
                    lblResinAdditiveNumber.InnerHtml = "";
                    lblResinAdditiveDesc.InnerHtml = "";
                    lblColorantNumber.InnerHtml = "";
                    lblColorantDesc.InnerHtml = "";
                    lblColorPercentage.InnerHtml = "";
                    lblToolCavities.InnerHtml = "";
                    lblCycleTimeStd.InnerHtml = "";
                    lblShotWeightStd.InnerHtml = "";
                    lblMachineNumber.InnerHtml = "";
                }
            }
            else
            {
                foreach (DataRow oRow in oDs.Tables[0].Rows)
                {
                    lblMaterialNumber.InnerHtml = _global.Fld2Str(oRow["MaterialNumber"]);
                    lblMaterialDescription.InnerHtml = _global.Fld2Str(oRow["MaterialDescription"]);
                    lblResinAdditiveNumber.InnerHtml = _global.Fld2Str(oRow["ResinAdditiveNumber"]);
                    lblResinAdditiveDesc.InnerHtml = _global.Fld2Str(oRow["ResinAdditiveDesc"]);
                    lblColorantNumber.InnerHtml = _global.Fld2Str(oRow["ColorantNumber"]);
                    lblColorantDesc.InnerHtml = _global.Fld2Str(oRow["ColorantDesc"]);
                    lblColorPercentage.InnerHtml = _global.Fld2Num(oRow["ColorPercentage"], 2);
                    lblToolCavities.InnerHtml = _global.Fld2IntStr(oRow["ToolCavities"]);
                    lblCycleTimeStd.InnerHtml = _global.Fld2Num(oRow["CycleTimeStd"], 2);
                    lblShotWeightStd.InnerHtml = _global.Fld2Num(oRow["ShotWeightStd"], 2);
                    lblMachineNumber.InnerHtml = _global.Fld2Str(oRow["MachineNumber"]); ;
                }
            }
            oDs.Dispose();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strError = "";

            if (SaveHeader()) {

                if (hProtect.Value != "1")
                {
                    strError = SaveBarrelTemperatures();
					strError += SaveGasAssistData();
                    strError += SaveHotTipController();
                    strError += SaveInjectionProfile();
                    strError += SaveMoldCoolingTemps();
                    strError += SaveNozzleInformation();
                    strError += SaveRecoveryClampProfile();
                    strError += SaveReferenceData();
                    strError += SaveValveGateData();

                    if (strError != "") { ErrorMessage(strError); }
                    else {
                        FormLoad(_global.Fld2Int(hMachineDataHeaderId.Value));
                        SuccessMessage("Run data saved successfully!");
                    }
                }
                else {
                    FormLoad(_global.Fld2Int(hMachineDataHeaderId.Value));
                    SuccessMessage("Run comments saved successfully!");
                }
            }
        }

        private bool SaveHeader()
        {
            ClearMessage();

            _database.postResults results = new _database.postResults();
            results = _database.postMachineDataHeader(_global.Fld2Int(hMachineDataHeaderId.Value),
                                                        hPlantNumber.Value,
                                                        txtPartNumber.Text,
                                                        ddlToolNumber.Text,
                                                        lblMaterialNumber.InnerHtml,
                                                        lblMaterialDescription.InnerHtml,
                                                        ddlMachineNumber.Text,
                                                        hMachineType.Value,
                                                        txtTechName.Text,
                                                        lblResinAdditiveNumber.InnerHtml,
                                                        lblResinAdditiveDesc.InnerHtml,
                                                        lblColorantNumber.InnerHtml,
                                                        lblColorantDesc.InnerHtml,
                                                        _global.Fld2Dbl(lblColorPercentage.InnerHtml),
                                                        _global.Fld2Int(lblToolCavities.InnerHtml),
                                                        _global.Fld2Dbl(txtMcGuireSetting.Text),
                                                        _global.Fld2Dbl(txtCertPartsWeightGm.Text),
                                                        _global.Fld2Dbl(hCycleTimeMin.Value),
                                                        _global.Fld2Dbl(hCycleTimeMid.Value),
                                                        _global.Fld2Dbl(txtCycleTimeMax.Text),
                                                        _global.Fld2Dbl(lblCycleTimeStd.InnerHtml),
                                                        _global.Fld2Dbl(txtCycleTimeCert.Text),
                                                        _global.Fld2Dbl(lblShotWeightStd.InnerHtml),
                                                        _global.Fld2Dbl(txtShotWeightCert.Text),
                                                        _global.Fld2Dbl(txtRunnerWeightGm.Text),
                                                        _global.Fld2Dbl(hFinalPartsWeightMin.Value),
                                                        _global.Fld2Dbl(txtFinalPartsWeightMid.Text),
                                                        _global.Fld2Dbl(hFinalPartsWeightMax.Value),
                                                        txtComments.Text,
                                                        txtRunComments1.Text,
                                                        _global.Fld2Date(txtRunCommentsDate1.Text),
                                                        _global.Fld2Dbl(txtRunCycleTime1.Text),
                                                        _global.Fld2Dbl(txtRunShotWeight1.Text),
                                                        txtRunComments2.Text,
                                                        _global.Fld2Date(txtRunCommentsDate2.Text),
                                                        _global.Fld2Dbl(txtRunCycleTime2.Text),
                                                        _global.Fld2Dbl(txtRunShotWeight2.Text),
                                                        txtRunComments3.Text,
                                                        _global.Fld2Date(txtRunCommentsDate3.Text),
                                                        _global.Fld2Dbl(txtRunCycleTime3.Text),
                                                        _global.Fld2Dbl(txtRunShotWeight3.Text));

            if (results.errorMessage != "")
            {
                ErrorMessage(results.errorMessage);
                return false;
            }
            hMachineDataHeaderId.Value = results.recId.ToString();

            return true;
        }
        private void GetHeader(int Action)
        {
            string strStatus = "";

            //init header fields
            InitHeader();

            string strSQL = "get_MachineDataHeader ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + _global.Fld2Int(hMachineDataHeaderId.Value).ToString();

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hMachineDataHeaderId.Value = _global.Fld2IntStr(oRow["MachineDataHeaderId"]);
                hPlantNumber.Value = _global.Fld2Str(oRow["PlantNumber"]);

                string strPartNumber = _global.Fld2Str(oRow["PartNumber"]);
                string strToolNumber = _global.Fld2Str(oRow["ToolNumber"]);

                GetToolNumbers(strPartNumber);
                GetMachineNumbers(strPartNumber, strToolNumber);

                txtPartNumber.Text = strPartNumber;
                _global.dpFindString(ref ddlToolNumber, strToolNumber);
                lblMaterialNumber.InnerHtml = _global.Fld2Str(oRow["MaterialNumber"]);
                lblMaterialDescription.InnerHtml = _global.Fld2Str(oRow["MaterialDescription"]);
                lblMachineNumber.InnerHtml = _global.Fld2Str(oRow["MachineNumber"]);
                _global.dpFindString(ref ddlMachineNumber, _global.Fld2Str(oRow["MachineNumber"]));
                txtTechName.Text = _global.Fld2Str(oRow["TechName"]);
                lblDateCreated.InnerHtml = _global.Fld2Date(oRow["DateCreated"]);
                lblResinAdditiveNumber.InnerHtml = _global.Fld2Str(oRow["ResinAdditiveNumber"]);
                lblResinAdditiveDesc.InnerHtml = _global.Fld2Str(oRow["ResinAdditiveDesc"]);
                lblColorantNumber.InnerHtml = _global.Fld2Str(oRow["ColorantNumber"]);
                lblColorantDesc.InnerHtml = _global.Fld2Str(oRow["ColorantDesc"]);
                lblColorPercentage.InnerHtml = _global.Fld2Num(oRow["ColorPercentage"], 2);
                lblToolCavities.InnerHtml = _global.Fld2IntStr(oRow["ToolCavities"]);
                txtMcGuireSetting.Text = _global.Fld2Num(oRow["McGuireSetting"], 2);
                txtCertPartsWeightGm.Text = _global.Fld2Num(oRow["CertPartsWeightGm"], 2);
                lblCycleTimeMin.InnerHtml = _global.Fld2Num(oRow["CycleTimeMin"], 2);
                lblCycleTimeMid.InnerHtml = _global.Fld2Num(oRow["CycleTimeMid"], 2);
                txtCycleTimeMax.Text = _global.Fld2Num(oRow["CycleTimeMax"], 2);
                lblCycleTimeStd.InnerHtml = _global.Fld2Num(oRow["CycleTimeStd"], 2);
                txtCycleTimeCert.Text = _global.Fld2Num(oRow["CycleTimeCert"], 2);
                lblShotWeightStd.InnerHtml = _global.Fld2Num(oRow["ShotWeightStd"], 2);
                txtShotWeightCert.Text = _global.Fld2Num(oRow["ShotWeightCert"], 2);
                txtRunnerWeightGm.Text = _global.Fld2Num(oRow["RunnerWeightGm"], 2);
                lblFinalPartsWeightMin.InnerHtml = _global.Fld2Num(oRow["FinalPartsWeightMin"], 3);
                txtFinalPartsWeightMid.Text = _global.Fld2Num(oRow["FinalPartsWeightMid"], 3);
                lblFinalPartsWeightMax.InnerHtml = _global.Fld2Num(oRow["FinalPartsWeightMax"], 3);
                txtComments.Text = _global.Fld2Str(oRow["Comments"]);

                txtRunComments1.Text = _global.Fld2Str(oRow["RunComments1"]);
                txtRunCommentsDate1.Text = _global.Fld2Date(oRow["RunCommentsDate1"]);
                txtRunCycleTime1.Text = _global.Fld2Num(oRow["RunCycleTime1"], 2);
                txtRunShotWeight1.Text = _global.Fld2Num(oRow["RunShotWeight1"], 2);
                txtRunComments2.Text = _global.Fld2Str(oRow["RunComments2"]);
                txtRunCommentsDate2.Text = _global.Fld2Date(oRow["RunCommentsDate2"]);
                txtRunCycleTime2.Text = _global.Fld2Num(oRow["RunCycleTime2"], 2);
                txtRunShotWeight2.Text = _global.Fld2Num(oRow["RunShotWeight2"], 2);
                txtRunComments3.Text = _global.Fld2Str(oRow["RunComments3"]);
                txtRunCommentsDate3.Text = _global.Fld2Date(oRow["RunCommentsDate3"]);
                txtRunCycleTime3.Text = _global.Fld2Num(oRow["RunCycleTime3"], 2);
                txtRunShotWeight3.Text = _global.Fld2Num(oRow["RunShotWeight3"], 2);

                hCycleTimeMin.Value = _global.Fld2Num(oRow["CycleTimeMin"], 2);
                hCycleTimeMid.Value = _global.Fld2Num(oRow["CycleTimeMid"], 2);
                hFinalPartsWeightMin.Value = _global.Fld2Num(oRow["FinalPartsWeightMin"], 3);
                hFinalPartsWeightMax.Value = _global.Fld2Num(oRow["FinalPartsWeightMax"], 3);

                if (Action != runSheetActions.IsCopy) { strStatus = _global.Fld2Str(oRow["Status"]); }
                lblHeading.Text = hMachineType.Value + " - " + ((strStatus == "") ? "NEW" : strStatus);
            }
            oDs.Dispose();

            //if cerfified or approved show run comments
            dvRunComments.Visible = ((strStatus != "") ? true : false);

            SecureForm(Action, strStatus);
        }
        private void InitHeader()
        {
            ClearMessage();

            txtPartNumber.Text = "";
            ddlToolNumber.SelectedIndex = -1;
            lblMaterialNumber.InnerHtml = "";
            lblMaterialDescription.InnerHtml = "";
            lblMachineNumber.InnerHtml = "";
            ddlMachineNumber.SelectedIndex = -1;
            txtTechName.Text = "";
            lblDateCreated.InnerHtml = System.DateTime.Today.ToString("MM/dd/yyyy"); ;
            lblResinAdditiveNumber.InnerHtml = "";
            lblResinAdditiveDesc.InnerHtml = "";
            lblColorantNumber.InnerHtml = "";
            lblColorantDesc.InnerHtml = "";
            lblColorPercentage.InnerHtml = "";
            lblToolCavities.InnerHtml = "";
            txtMcGuireSetting.Text = "";
            txtCertPartsWeightGm.Text = "";
            lblCycleTimeMin.InnerHtml = "";
            lblCycleTimeMid.InnerHtml = "";
            txtCycleTimeMax.Text = "";
            lblCycleTimeStd.InnerHtml = "";
            txtCycleTimeCert.Text = "";
            lblShotWeightStd.InnerHtml = "";
            txtShotWeightCert.Text = "";
            txtRunnerWeightGm.Text = "";
            lblFinalPartsWeightMin.InnerHtml = "";
            txtFinalPartsWeightMid.Text = "";
            lblFinalPartsWeightMax.InnerHtml = "";
            txtComments.Text = "";

            txtRunComments1.Text = "";
            txtRunCommentsDate1.Text = "";
            txtRunCycleTime1.Text = "";
            txtRunShotWeight1.Text = "";
            txtRunComments2.Text = "";
            txtRunCommentsDate2.Text = "";
            txtRunCycleTime2.Text = "";
            txtRunShotWeight2.Text = "";
            txtRunComments3.Text = "";
            txtRunCommentsDate3.Text = "";
            txtRunCycleTime3.Text = "";
            txtRunShotWeight3.Text = "";

            hBarrelTemperaturesRecId.Value = "";
            hGasAssistDataRecId.Value = "";
            hHotTipControllerRecId.Value = "";
            hInjectionProfileRecId.Value = "";
            hMoldCoolingTempsRecId.Value = "";
            hNozzleInformationRecId.Value = "";
            hRecoveryClampProfileRecId.Value = "";
            hReferenceDataRecId.Value = "";

            hCycleTimeMin.Value = "";
            hCycleTimeMid.Value = "";
            hFinalPartsWeightMin.Value = "";
            hFinalPartsWeightMax.Value = "";

            lblHeading.Text = hMachineType.Value + " - NEW";
            dvRunComments.Visible = false;
        }

        private string SaveBarrelTemperatures()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postBarrelTemperatures(_global.Fld2Int(hBarrelTemperaturesRecId.Value),
                                                        _global.Fld2Int(hMachineDataHeaderId.Value),
                                                        _global.Fld2Str(txtNozzleTipPercent.Text),
                                                        _global.Fld2Str(txtNozzleBodyPercent.Text),
                                                        _global.Fld2Str(txtAdapter.Text),
                                                        0, //_global.Fld2Int(txtSHHeat.Text),
                                                        0, //_global.Fld2Int(txtBarrelHead.Text),
                                                        _global.Fld2Int(txtBarrelFront.Text),
                                                        _global.Fld2Int(txtBarrelCenter1.Text),
                                                        _global.Fld2Int(txtBarrelCenter2.Text),
                                                        _global.Fld2Int(txtBarrelCenter3.Text),
                                                        _global.Fld2Int(txtBarrelCenter4.Text),
                                                        _global.Fld2Int(txtBarrelCenter5.Text),
                                                        _global.Fld2Int(txtBarrelRear.Text));

            if (results.errorMessage == "") { hBarrelTemperaturesRecId.Value = results.recId.ToString(); }

            return results.errorMessage;

        }
        private void GetBarrelTemperatures()
        {
            InitBarrelTemperatures();

            string strSQL = "get_BarrelTemperatures ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hBarrelTemperaturesRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                txtNozzleTipPercent.Text = _global.Fld2Str(oRow["NozzleTipPercent"]);
                txtNozzleBodyPercent.Text = _global.Fld2Str(oRow["NozzleBodyPercent"]);
                txtAdapter.Text = _global.Fld2Str(oRow["Adapter"]);
                //txtSHHeat.Text = _global.Fld2Num(oRow["SHHeat"]);
                //txtBarrelHead.Text = _global.Fld2Num(oRow["BarrelHead"]);
                txtBarrelFront.Text = _global.Fld2Num(oRow["BarrelFront"]);
                txtBarrelCenter1.Text = _global.Fld2Num(oRow["BarrelCenter1"]);
                txtBarrelCenter2.Text = _global.Fld2Num(oRow["BarrelCenter2"]);
                txtBarrelCenter3.Text = _global.Fld2Num(oRow["BarrelCenter3"]);
                txtBarrelCenter4.Text = _global.Fld2Num(oRow["BarrelCenter4"]);
                txtBarrelCenter5.Text = _global.Fld2Num(oRow["BarrelCenter5"]);
                txtBarrelRear.Text = _global.Fld2Num(oRow["BarrelRear"]);

            }
            oDs.Dispose();

        }
        private void InitBarrelTemperatures()
        {
            hBarrelTemperaturesRecId.Value = "";
            txtNozzleTipPercent.Text = "";
            txtNozzleBodyPercent.Text = "";
            txtAdapter.Text = "";
            //txtSHHeat.Text = "";
            //txtBarrelHead.Text = "";
            txtBarrelFront.Text = "";
            txtBarrelCenter1.Text = "";
            txtBarrelCenter2.Text = "";
            txtBarrelCenter3.Text = "";
            txtBarrelCenter4.Text = "";
            txtBarrelCenter5.Text = "";
            txtBarrelRear.Text = "";
        }

        private string SaveGasAssistData()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postGasAssistData(_global.Fld2Int(hGasAssistDataRecId.Value),
                                                _global.Fld2Int(hMachineDataHeaderId.Value),
                                                _global.Fld2Dbl(txtDelay_C1.Text),
                                                _global.Fld2Dbl(txtHold_C1.Text),
                                                _global.Fld2Dbl(txtExhaust_C1.Text),
                                                _global.Fld2Dbl(txtPressure1_C1.Text),
                                                _global.Fld2Dbl(txtTime1_C1.Text),
                                                _global.Fld2Dbl(txtPressure2_C1.Text),
                                                _global.Fld2Dbl(txtTime2_C1.Text),
                                                _global.Fld2Dbl(txtDelay_C2.Text),
                                                _global.Fld2Dbl(txtHold_C2.Text),
                                                _global.Fld2Dbl(txtExhaust_C2.Text),
                                                _global.Fld2Dbl(txtPressure1_C2.Text),
                                                _global.Fld2Dbl(txtTime1_C2.Text),
                                                _global.Fld2Dbl(txtPressure2_C2.Text),
                                                _global.Fld2Dbl(txtTime2_C2.Text),
                                                _global.Fld2Dbl(txtDelay_C3.Text),
                                                _global.Fld2Dbl(txtHold_C3.Text),
                                                _global.Fld2Dbl(txtExhaust_C3.Text),
                                                _global.Fld2Dbl(txtPressure1_C3.Text),
                                                _global.Fld2Dbl(txtTime1_C3.Text),
                                                _global.Fld2Dbl(txtPressure2_C3.Text),
                                                _global.Fld2Dbl(txtTime2_C3.Text),
                                                _global.Fld2Dbl(txtDelay_C4.Text),
                                                _global.Fld2Dbl(txtHold_C4.Text),
                                                _global.Fld2Dbl(txtExhaust_C4.Text),
                                                _global.Fld2Dbl(txtPressure1_C4.Text),
                                                _global.Fld2Dbl(txtTime1_C4.Text),
                                                _global.Fld2Dbl(txtPressure2_C4.Text),
                                                _global.Fld2Dbl(txtTime2_C4.Text));

            if (results.errorMessage == "") { hGasAssistDataRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetGasAssistData()
        {
            InitGasAssistData();

            string strSQL = "get_GasAssistData ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hGasAssistDataRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                txtDelay_C1.Text = _global.Fld2Num(oRow["Delay_C1"], 1);
                txtHold_C1.Text = _global.Fld2Num(oRow["Hold_C1"], 1);
                txtExhaust_C1.Text = _global.Fld2Num(oRow["Exhaust_C1"], 1);
                txtPressure1_C1.Text = _global.Fld2Num(oRow["Pressure1_C1"]);
                txtTime1_C1.Text = _global.Fld2Num(oRow["Time1_C1"], 1);
                txtPressure2_C1.Text = _global.Fld2Num(oRow["Pressure2_C1"]);
                txtTime2_C1.Text = _global.Fld2Num(oRow["Time2_C1"], 1);
                txtDelay_C2.Text = _global.Fld2Num(oRow["Delay_C2"], 1);
                txtHold_C2.Text = _global.Fld2Num(oRow["Hold_C2"], 1);
                txtExhaust_C2.Text = _global.Fld2Num(oRow["Exhaust_C2"], 1);
                txtPressure1_C2.Text = _global.Fld2Num(oRow["Pressure1_C2"]);
                txtTime1_C2.Text = _global.Fld2Num(oRow["Time1_C2"], 1);
                txtPressure2_C2.Text = _global.Fld2Num(oRow["Pressure2_C2"]);
                txtTime2_C2.Text = _global.Fld2Num(oRow["Time2_C2"], 1);
                txtDelay_C3.Text = _global.Fld2Num(oRow["Delay_C3"], 1);
                txtHold_C3.Text = _global.Fld2Num(oRow["Hold_C3"], 1);
                txtExhaust_C3.Text = _global.Fld2Num(oRow["Exhaust_C3"], 1);
                txtPressure1_C3.Text = _global.Fld2Num(oRow["Pressure1_C3"]);
                txtTime1_C3.Text = _global.Fld2Num(oRow["Time1_C3"], 1);
                txtPressure2_C3.Text = _global.Fld2Num(oRow["Pressure2_C3"]);
                txtTime2_C3.Text = _global.Fld2Num(oRow["Time2_C3"], 1);
                txtDelay_C4.Text = _global.Fld2Num(oRow["Delay_C4"], 1);
                txtHold_C4.Text = _global.Fld2Num(oRow["Hold_C4"], 1);
                txtExhaust_C4.Text = _global.Fld2Num(oRow["Exhaust_C4"], 1);
                txtPressure1_C4.Text = _global.Fld2Num(oRow["Pressure1_C4"]);
                txtTime1_C4.Text = _global.Fld2Num(oRow["Time1_C4"], 1);
                txtPressure2_C4.Text = _global.Fld2Num(oRow["Pressure2_C4"]);
                txtTime2_C4.Text = _global.Fld2Num(oRow["Time2_C4"], 1);

            }
            oDs.Dispose();

        }
        private void InitGasAssistData()
        {
            hGasAssistDataRecId.Value = "";
            txtDelay_C1.Text = "";
            txtHold_C1.Text = "";
            txtExhaust_C1.Text = "";
            txtPressure1_C1.Text = "";
            txtTime1_C1.Text = "";
            txtPressure2_C1.Text = "";
            txtTime2_C1.Text = "";
            txtDelay_C2.Text = "";
            txtHold_C2.Text = "";
            txtExhaust_C2.Text = "";
            txtPressure1_C2.Text = "";
            txtTime1_C2.Text = "";
            txtPressure2_C2.Text = "";
            txtTime2_C2.Text = "";
            txtDelay_C3.Text = "";
            txtHold_C3.Text = "";
            txtExhaust_C3.Text = "";
            txtPressure1_C3.Text = "";
            txtTime1_C3.Text = "";
            txtPressure2_C3.Text = "";
            txtTime2_C3.Text = "";
            txtDelay_C4.Text = "";
            txtHold_C4.Text = "";
            txtExhaust_C4.Text = "";
            txtPressure1_C4.Text = "";
            txtTime1_C4.Text = "";
            txtPressure2_C4.Text = "";
            txtTime2_C4.Text = "";
        }

        private string SaveHotTipController()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postHotTipController(_global.Fld2Int(hHotTipControllerRecId.Value),
                                                    _global.Fld2Int(hMachineDataHeaderId.Value),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos1_Box1.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos2_Box1.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos3_Box1.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos4_Box1.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos5_Box1.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos6_Box1.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos1_Box2.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos2_Box2.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos3_Box2.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos4_Box2.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos5_Box2.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos6_Box2.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos1_Box3.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos2_Box3.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos3_Box3.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos4_Box3.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos5_Box3.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos6_Box3.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos1_Box4.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos2_Box4.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos3_Box4.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos4_Box4.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos5_Box4.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos6_Box4.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos1_Box5.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos2_Box5.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos3_Box5.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos4_Box5.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos5_Box5.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos6_Box5.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos1_Box6.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos2_Box6.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos3_Box6.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos4_Box6.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos5_Box6.Text),
                                                    _global.Fld2Int(txtGate_HB_Man_Pos6_Box6.Text));

            if (results.errorMessage == "") { hHotTipControllerRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetHotTipController()
        {
            InitHotTipController();

            string strSQL = "get_HotTipController ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hHotTipControllerRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                txtGate_HB_Man_Pos1_Box1.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos1_Box1"]);
                txtGate_HB_Man_Pos2_Box1.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos2_Box1"]);
                txtGate_HB_Man_Pos3_Box1.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos3_Box1"]);
                txtGate_HB_Man_Pos4_Box1.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos4_Box1"]);
                txtGate_HB_Man_Pos5_Box1.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos5_Box1"]);
                txtGate_HB_Man_Pos6_Box1.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos6_Box1"]);
                txtGate_HB_Man_Pos1_Box2.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos1_Box2"]);
                txtGate_HB_Man_Pos2_Box2.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos2_Box2"]);
                txtGate_HB_Man_Pos3_Box2.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos3_Box2"]);
                txtGate_HB_Man_Pos4_Box2.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos4_Box2"]);
                txtGate_HB_Man_Pos5_Box2.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos5_Box2"]);
                txtGate_HB_Man_Pos6_Box2.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos6_Box2"]);
                txtGate_HB_Man_Pos1_Box3.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos1_Box3"]);
                txtGate_HB_Man_Pos2_Box3.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos2_Box3"]);
                txtGate_HB_Man_Pos3_Box3.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos3_Box3"]);
                txtGate_HB_Man_Pos4_Box3.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos4_Box3"]);
                txtGate_HB_Man_Pos5_Box3.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos5_Box3"]);
                txtGate_HB_Man_Pos6_Box3.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos6_Box3"]);
                txtGate_HB_Man_Pos1_Box4.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos1_Box4"]);
                txtGate_HB_Man_Pos2_Box4.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos2_Box4"]);
                txtGate_HB_Man_Pos3_Box4.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos3_Box4"]);
                txtGate_HB_Man_Pos4_Box4.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos4_Box4"]);
                txtGate_HB_Man_Pos5_Box4.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos5_Box4"]);
                txtGate_HB_Man_Pos6_Box4.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos6_Box4"]);
                txtGate_HB_Man_Pos1_Box5.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos1_Box5"]);
                txtGate_HB_Man_Pos2_Box5.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos2_Box5"]);
                txtGate_HB_Man_Pos3_Box5.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos3_Box5"]);
                txtGate_HB_Man_Pos4_Box5.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos4_Box5"]);
                txtGate_HB_Man_Pos5_Box5.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos5_Box5"]);
                txtGate_HB_Man_Pos6_Box5.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos6_Box5"]);
                txtGate_HB_Man_Pos1_Box6.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos1_Box6"]);
                txtGate_HB_Man_Pos2_Box6.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos2_Box6"]);
                txtGate_HB_Man_Pos3_Box6.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos3_Box6"]);
                txtGate_HB_Man_Pos4_Box6.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos4_Box6"]);
                txtGate_HB_Man_Pos5_Box6.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos5_Box6"]);
                txtGate_HB_Man_Pos6_Box6.Text = _global.Fld2Num(oRow["Gate_HB_Man_Pos6_Box6"]);
            }
            oDs.Dispose();
        }
        private void InitHotTipController()
        {
            hHotTipControllerRecId.Value = "";
            txtGate_HB_Man_Pos1_Box1.Text = "";
            txtGate_HB_Man_Pos2_Box1.Text = "";
            txtGate_HB_Man_Pos3_Box1.Text = "";
            txtGate_HB_Man_Pos4_Box1.Text = "";
            txtGate_HB_Man_Pos5_Box1.Text = "";
            txtGate_HB_Man_Pos6_Box1.Text = "";
            txtGate_HB_Man_Pos1_Box2.Text = "";
            txtGate_HB_Man_Pos2_Box2.Text = "";
            txtGate_HB_Man_Pos3_Box2.Text = "";
            txtGate_HB_Man_Pos4_Box2.Text = "";
            txtGate_HB_Man_Pos5_Box2.Text = "";
            txtGate_HB_Man_Pos6_Box2.Text = "";
            txtGate_HB_Man_Pos1_Box3.Text = "";
            txtGate_HB_Man_Pos2_Box3.Text = "";
            txtGate_HB_Man_Pos3_Box3.Text = "";
            txtGate_HB_Man_Pos4_Box3.Text = "";
            txtGate_HB_Man_Pos5_Box3.Text = "";
            txtGate_HB_Man_Pos6_Box3.Text = "";
            txtGate_HB_Man_Pos1_Box4.Text = "";
            txtGate_HB_Man_Pos2_Box4.Text = "";
            txtGate_HB_Man_Pos3_Box4.Text = "";
            txtGate_HB_Man_Pos4_Box4.Text = "";
            txtGate_HB_Man_Pos5_Box4.Text = "";
            txtGate_HB_Man_Pos6_Box4.Text = "";
            txtGate_HB_Man_Pos1_Box5.Text = "";
            txtGate_HB_Man_Pos2_Box5.Text = "";
            txtGate_HB_Man_Pos3_Box5.Text = "";
            txtGate_HB_Man_Pos4_Box5.Text = "";
            txtGate_HB_Man_Pos5_Box5.Text = "";
            txtGate_HB_Man_Pos6_Box5.Text = "";
            txtGate_HB_Man_Pos1_Box6.Text = "";
            txtGate_HB_Man_Pos2_Box6.Text = "";
            txtGate_HB_Man_Pos3_Box6.Text = "";
            txtGate_HB_Man_Pos4_Box6.Text = "";
            txtGate_HB_Man_Pos5_Box6.Text = "";
            txtGate_HB_Man_Pos6_Box6.Text = "";
        }

        private string SaveInjectionProfile()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postInjectionProfile(_global.Fld2Int(hInjectionProfileRecId.Value),
                                                    _global.Fld2Int(hMachineDataHeaderId.Value),
                                                    _global.Fld2Dbl(txtShotSize.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionVelocity1.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionVelocity2.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionVelocity3.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionVelocity4.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionVelocity5.Text),
                                                    _global.Fld2Dbl(txtInjectionChangePos1.Text),
                                                    _global.Fld2Dbl(txtInjectionChangePos2.Text),
                                                    _global.Fld2Dbl(txtInjectionChangePos3.Text),
                                                    _global.Fld2Dbl(txtInjectionChangePos4.Text),
                                                    _global.Fld2Dbl(txtInjectionPress1.Text),
                                                    _global.Fld2Dbl(txtInjectionPress2.Text),
                                                    _global.Fld2Dbl(txtInjectionPress3.Text),
                                                    _global.Fld2Dbl(txtInjectionPress4.Text),
                                                    _global.Fld2Dbl(txtInjectionPress5.Text),
                                                    _global.Fld2Int(txtInjectionFlow1.Text),
                                                    _global.Fld2Int(txtInjectionFlow2.Text),
                                                    _global.Fld2Int(txtInjectionFlow3.Text),
                                                    _global.Fld2Int(txtInjectionFlow4.Text),
                                                    _global.Fld2Int(txtInjectionFlow5.Text),
                                                    "", //txtTransMode.Text,
                                                    0, //_global.Fld2Dbl(txtTransPosition.Text),
                                                    0, //_global.Fld2Int(txtIPS_PSI.Text),
                                                    _global.Fld2Dbl(txtInjectionPressureThreshold.Text),
                                                    _global.Fld2Dbl(txtInjectionPositionThreshold.Text),
                                                    _global.Fld2Dbl(txtInjectionTimeThreshold.Text),
                                                    0, //_global.Fld2Int(txtInjectionPressLimit.Text),
                                                    _global.Fld2Int(txtInjectionPSIAtTransfer.Text),
                                                    _global.Fld2Dbl(txtInjectionTimeAct.Text),
                                                    _global.Fld2Int(txtHoldPress1_PSI.Text),
                                                    _global.Fld2Int(txtHoldPress2_PSI.Text),
                                                    0, //_global.Fld2Dbl(txtHoldPress1_Percent.Text),
                                                    0, //_global.Fld2Dbl(txtHoldPress2_Percent.Text),
                                                    0, //_global.Fld2Dbl(txtHoldPress1_Seconds.Text),
                                                    0, //_global.Fld2Dbl(txtHoldPress2_Seconds.Text),
                                                    _global.Fld2Int(txtHoldFlow1.Text),
                                                    _global.Fld2Int(txtHoldFlow2.Text),
                                                    _global.Fld2Dbl(txtHoldTime1.Text),
                                                    _global.Fld2Dbl(txtHoldTime2.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionHoldTime.Text),
                                                    _global.Fld2Dbl(txtFinalCushionMM.Text));

            if (results.errorMessage == "") { hInjectionProfileRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetInjectionProfile()
        {
            InitInjectionProfile();

            string strSQL = "get_InjectionProfile ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hInjectionProfileRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                txtShotSize.Text = _global.Fld2Num(oRow["ShotSize"], 2);
                //txtInjectionVelocity1.Text = _global.Fld2Num(oRow["InjectionVelocity1"]);
                //txtInjectionVelocity2.Text = _global.Fld2Num(oRow["InjectionVelocity2"]);
                //txtInjectionVelocity3.Text = _global.Fld2Num(oRow["InjectionVelocity3"]);
                //txtInjectionVelocity4.Text = _global.Fld2Num(oRow["InjectionVelocity4"]);
                //txtInjectionVelocity5.Text = _global.Fld2Num(oRow["InjectionVelocity5"]);
                txtInjectionChangePos1.Text = _global.Fld2Num(oRow["InjectionChangePos1"], 2);
                txtInjectionChangePos2.Text = _global.Fld2Num(oRow["InjectionChangePos2"], 2);
                txtInjectionChangePos3.Text = _global.Fld2Num(oRow["InjectionChangePos3"], 2);
                txtInjectionChangePos4.Text = _global.Fld2Num(oRow["InjectionChangePos4"], 2);
                txtInjectionPress1.Text = _global.Fld2Num(oRow["InjectionPress1"]);
                txtInjectionPress2.Text = _global.Fld2Num(oRow["InjectionPress2"]);
                txtInjectionPress3.Text = _global.Fld2Num(oRow["InjectionPress3"]);
                txtInjectionPress4.Text = _global.Fld2Num(oRow["InjectionPress4"]);
                txtInjectionPress5.Text = _global.Fld2Num(oRow["InjectionPress5"]);
                txtInjectionFlow1.Text = _global.Fld2Num(oRow["InjectionFlow1"]);
                txtInjectionFlow2.Text = _global.Fld2Num(oRow["InjectionFlow2"]);
                txtInjectionFlow3.Text = _global.Fld2Num(oRow["InjectionFlow3"]);
                txtInjectionFlow4.Text = _global.Fld2Num(oRow["InjectionFlow4"]);
                txtInjectionFlow5.Text = _global.Fld2Num(oRow["InjectionFlow5"]);
                //txtTransMode.Text = _global.Fld2Str(oRow["TransMode"]);
                //txtTransPosition.Text = _global.Fld2Num(oRow["TransPosition"], 1);
                //txtIPS_PSI.Text = _global.Fld2Num(oRow["IPS_PSI"]);
                txtInjectionPressureThreshold.Text = _global.Fld2Num(oRow["InjectionPressureThreshold"],2);
                txtInjectionPositionThreshold.Text = _global.Fld2Num(oRow["InjectionPositionThreshold"], 2);
                txtInjectionTimeThreshold.Text = _global.Fld2Num(oRow["InjectionTimeThreshold"], 2);
                //txtInjectionPressLimit.Text = _global.Fld2Num(oRow["InjectionPressLimit"]);
                txtInjectionPSIAtTransfer.Text = _global.Fld2Num(oRow["InjectionPSIAtTransfer"]);
                txtInjectionTimeAct.Text = _global.Fld2Num(oRow["InjectionTimeAct"], 2);
                txtHoldPress1_PSI.Text = _global.Fld2Num(oRow["HoldPress1_PSI"]);
                txtHoldPress2_PSI.Text = _global.Fld2Num(oRow["HoldPress2_PSI"]);
                //txtHoldPress1_Percent.Text = _global.Fld2Num(oRow["HoldPress1_Percent"], 2);
                //txtHoldPress2_Percent.Text = _global.Fld2Num(oRow["HoldPress2_Percent"], 2);
                //txtHoldPress1_Seconds.Text = _global.Fld2Num(oRow["HoldPress1_Seconds"], 2);
                //txtHoldPress2_Seconds.Text = _global.Fld2Num(oRow["HoldPress2_Seconds"], 2);
                txtHoldFlow1.Text = _global.Fld2Num(oRow["HoldFlow1"]);
                txtHoldFlow2.Text = _global.Fld2Num(oRow["HoldFlow2"]);
                txtHoldTime1.Text = _global.Fld2Num(oRow["HoldTime1"],2);
                txtHoldTime2.Text = _global.Fld2Num(oRow["HoldTime2"],2);
                //txtInjectionHoldTime.Text = _global.Fld2Num(oRow["InjectionHoldTime"], 2);
                txtFinalCushionMM.Text = _global.Fld2Num(oRow["FinalCushionMM"], 2);
            }
            oDs.Dispose();
        }
        private void InitInjectionProfile()
        {
            hInjectionProfileRecId.Value = "";
            txtShotSize.Text = "";
            //txtInjectionVelocity1.Text = "";
            //txtInjectionVelocity2.Text = "";
            //txtInjectionVelocity3.Text = "";
            //txtInjectionVelocity4.Text = "";
            //txtInjectionVelocity5.Text = "";
            txtInjectionChangePos1.Text = "";
            txtInjectionChangePos2.Text = "";
            txtInjectionChangePos3.Text = "";
            txtInjectionChangePos4.Text = "";
            txtInjectionPress1.Text = "";
            txtInjectionPress2.Text = "";
            txtInjectionPress3.Text = "";
            txtInjectionPress4.Text = "";
            txtInjectionPress5.Text = "";
            txtInjectionFlow1.Text = "";
            txtInjectionFlow2.Text = "";
            txtInjectionFlow3.Text = "";
            txtInjectionFlow4.Text = "";
            txtInjectionFlow5.Text = "";
            //txtTransMode.Text = "";
            //txtTransPosition.Text = "";
            //txtIPS_PSI.Text = "";
            txtInjectionPressureThreshold.Text = "";
            txtInjectionPositionThreshold.Text = "";
            txtInjectionTimeThreshold.Text = "";
            //txtInjectionPressLimit.Text = "";
            txtInjectionPSIAtTransfer.Text = "";
            txtInjectionTimeAct.Text = "";
            txtHoldPress1_PSI.Text = "";
            txtHoldPress2_PSI.Text = "";
            //txtHoldPress1_Percent.Text = "";
            //txtHoldPress2_Percent.Text = "";
            //txtHoldPress1_Seconds.Text = "";
            //txtHoldPress2_Seconds.Text = "";
            txtHoldFlow1.Text = "";
            txtHoldFlow2.Text = "";
            txtHoldTime1.Text = "";
            txtHoldTime2.Text = "";
            //txtInjectionHoldTime.Text = "";
            txtFinalCushionMM.Text = "";
        }

        private string SaveMoldCoolingTemps()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postMoldCoolingTemps(_global.Fld2Int(hMoldCoolingTempsRecId.Value),
                                                    _global.Fld2Int(hMachineDataHeaderId.Value),
                                                    _global.Fld2Str(txtMoldGateTempF.Text),
                                                    _global.Fld2Str(txtMoldFixedHalfTempF.Text),
                                                    _global.Fld2Str(txtMoldMovingHalfTempF.Text),
                                                    _global.Fld2Str(txtStripperOrOtherTempF.Text));

            if (results.errorMessage == "") { hMoldCoolingTempsRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetMoldCoolingTemps()
        {
            InitMoldCoolingTemps();

            string strSQL = "get_MoldCoolingTemps ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hMoldCoolingTempsRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                txtMoldGateTempF.Text = _global.Fld2Str(oRow["MoldGateTempF"]);
                txtMoldFixedHalfTempF.Text = _global.Fld2Str(oRow["MoldFixedHalfTempF"]);
                txtMoldMovingHalfTempF.Text = _global.Fld2Str(oRow["MoldMovingHalfTempF"]);
                txtStripperOrOtherTempF.Text = _global.Fld2Str(oRow["StripperOrOtherTempF"]);

            }
            oDs.Dispose();
        }
        private void InitMoldCoolingTemps()
        {
            hMoldCoolingTempsRecId.Value = "";
            txtMoldGateTempF.Text = "";
            txtMoldFixedHalfTempF.Text = "";
            txtMoldMovingHalfTempF.Text = "";
            txtStripperOrOtherTempF.Text = "";
        }

        private string SaveNozzleInformation()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postNozzleInformation(_global.Fld2Int(hNozzleInformationRecId.Value),
                                                    _global.Fld2Int(hMachineDataHeaderId.Value),
                                                    txtNozzleTipType.Text,
                                                    txtNozzleTipLengthOALInches.Text,
                                                    txtNozzleTipOrificeSizeInches.Text,
                                                    txtNozzleBodyLengthOALInches.Text);

            if (results.errorMessage == "") { hNozzleInformationRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetNozzleInformation()
        {
            InitNozzleInformation();

            string strSQL = "get_NozzleInformation ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hNozzleInformationRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                txtNozzleTipType.Text = _global.Fld2Str(oRow["NozzleTipType"]);
                txtNozzleTipLengthOALInches.Text = _global.Fld2Str(oRow["NozzleTipLengthOALInches"]);
                txtNozzleTipOrificeSizeInches.Text = _global.Fld2Str(oRow["NozzleTipOrificeSizeInches"]);
                txtNozzleBodyLengthOALInches.Text = _global.Fld2Str(oRow["NozzleBodyLengthOALInches"]);

            }
            oDs.Dispose();
        }
        private void InitNozzleInformation()
        {
            hNozzleInformationRecId.Value = "";
            txtNozzleTipType.Text = "";
            txtNozzleTipLengthOALInches.Text = "";
            txtNozzleTipOrificeSizeInches.Text = "";
            txtNozzleBodyLengthOALInches.Text = "";
        }

        private string SaveRecoveryClampProfile()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postRecoveryClampProfile(_global.Fld2Int(hRecoveryClampProfileRecId.Value),
                                                        _global.Fld2Int(hMachineDataHeaderId.Value),
                                                        0, //_global.Fld2Dbl(txtPrePullback.Text),
                                                        0, //_global.Fld2Dbl(txtPrePullbackStroke.Text),
                                                        _global.Fld2Dbl(txtSuckbackBeforePosition.Text),
                                                        _global.Fld2Dbl(txtSuckbackAfterPosition.Text),
                                                        _global.Fld2Dbl(txtScrewStartDelay.Text),
                                                        _global.Fld2Dbl(txtChargePosition1.Text),
                                                        _global.Fld2Dbl(txtChargePosition2.Text),
                                                        _global.Fld2Int(txtChargePressure1.Text),
                                                        _global.Fld2Int(txtChargePressure2.Text),
                                                        _global.Fld2Int(txtChargeFlow1.Text),
                                                        _global.Fld2Int(txtChargeFlow2.Text),
                                                        0, //_global.Fld2Dbl(txtScrewRecovery1Percent.Text),
                                                        0, //_global.Fld2Dbl(txtScrewRecovery2Percent.Text),
                                                        0, //_global.Fld2Int(txtScrewRecovery1RPM.Text),
                                                        0, //_global.Fld2Int(txtScrewRecovery2RPM.Text),
                                                        _global.Fld2Dbl(txtBackPressure1.Text),
                                                        _global.Fld2Dbl(txtBackPressure2.Text),
                                                        0, //_global.Fld2Dbl(txtPostPullback.Text),
                                                        0, //_global.Fld2Dbl(txtPostPullbackSpeedPercent.Text),
                                                        0, //_global.Fld2Dbl(txtPostPullbackStroke.Text),
                                                        _global.Fld2Dbl(txtScrewPositonAfter.Text),
                                                        0, //_global.Fld2Dbl(txtScrewRecoveryTime.Text),
                                                        _global.Fld2Dbl(txtScrewChargeTime.Text),
                                                        _global.Fld2Dbl(txtCoolingTimeSeconds.Text),
                                                        _global.Fld2Dbl(txtMoldProtectPressure.Text),
                                                        _global.Fld2Int(txtMoldProtectFlow.Text),
                                                        _global.Fld2Dbl(txtMoldProtectTime.Text),
                                                        0, //_global.Fld2Int(txtClampTonnage.Text),
                                                        _global.Fld2Int(txtClampHighPressurePSI.Text),
                                                        _global.Fld2Int(txtClampHighPressureFlow.Text),
                                                        0, //_global.Fld2Dbl(txtClampOpenPosition.Text),
                                                        0, //_global.Fld2Dbl(txtEjectorStroke.Text),
                                                        _global.Fld2Int(txtInjectionPressureGaugePSI.Text),
                                                        _global.Fld2Int(txtHoldingGauge1_PSI.Text),
                                                        _global.Fld2Int(txtHoldingGauge2_PSI.Text),
                                                        _global.Fld2Int(txtBackPressureGauge1_PSI.Text),
                                                        _global.Fld2Int(txtBackPressureGauge2_PSI.Text),
                                                        0); //_global.Fld2Dbl(txtFinalCushion.Text));

            if (results.errorMessage == "") { hRecoveryClampProfileRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetRecoveryClampProfile()
        {
            InitRecoveryClampProfile();

            string strSQL = "get_RecoveryClampProfile ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hRecoveryClampProfileRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                //txtPrePullback.Text = _global.Fld2Num(oRow["PrePullback"], 2);
                //txtPrePullbackStroke.Text = _global.Fld2Num(oRow["PrePullbackStroke"], 2);
                txtSuckbackBeforePosition.Text = _global.Fld2Num(oRow["SuckbackBeforePosition"], 2);
                txtSuckbackAfterPosition.Text = _global.Fld2Num(oRow["SuckbackAfterPosition"], 2);
                txtScrewStartDelay.Text = _global.Fld2Num(oRow["ScrewStartDelay"], 2);
                txtChargePosition1.Text = _global.Fld2Num(oRow["ChargePosition1"], 2);
                txtChargePosition2.Text = _global.Fld2Num(oRow["ChargePosition2"], 2);
                txtChargePressure1.Text = _global.Fld2Num(oRow["ChargePressure1"]);
                txtChargePressure2.Text = _global.Fld2Num(oRow["ChargePressure2"]);
                txtChargeFlow1.Text = _global.Fld2Num(oRow["ChargeFlow1"]);
                txtChargeFlow2.Text = _global.Fld2Num(oRow["ChargeFlow2"]);
                //txtScrewRecovery1Percent.Text = _global.Fld2Num(oRow["ScrewRecovery1Percent"], 1);
                //txtScrewRecovery2Percent.Text = _global.Fld2Num(oRow["ScrewRecovery2Percent"], 1);
                //txtScrewRecovery1RPM.Text = _global.Fld2Num(oRow["ScrewRecovery1RPM"]);
                //txtScrewRecovery2RPM.Text = _global.Fld2Num(oRow["ScrewRecovery2RPM"]);
                txtBackPressure1.Text = _global.Fld2Num(oRow["BackPressure1"], 1);
                txtBackPressure2.Text = _global.Fld2Num(oRow["BackPressure2"], 1);
                //txtPostPullback.Text = _global.Fld2Num(oRow["PostPullback"], 2);
                //txtPostPullbackSpeedPercent.Text = _global.Fld2Num(oRow["PostPullbackSpeedPercent"], 2);
                //txtPostPullbackStroke.Text = _global.Fld2Num(oRow["PostPullbackStroke"], 2);
                txtScrewPositonAfter.Text = _global.Fld2Num(oRow["ScrewPositonAfter"], 2);
                //txtScrewRecoveryTime.Text = _global.Fld2Num(oRow["ScrewRecoveryTime"], 2);
                txtScrewChargeTime.Text = _global.Fld2Num(oRow["ScrewChargeTime"], 1);
                txtCoolingTimeSeconds.Text = _global.Fld2Num(oRow["CoolingTimeSeconds"], 1);
                txtMoldProtectPressure.Text = _global.Fld2Num(oRow["MoldProtectPressure"]);
                txtMoldProtectFlow.Text = _global.Fld2Num(oRow["MoldProtectFlow"]);
                txtMoldProtectTime.Text = _global.Fld2Num(oRow["MoldProtectTime"], 2);
                //txtClampTonnage.Text = _global.Fld2Num(oRow["ClampTonnage"]);
                txtClampHighPressurePSI.Text = _global.Fld2Num(oRow["ClampHighPressurePSI"]);
                txtClampHighPressureFlow.Text = _global.Fld2Num(oRow["ClampHighPressureFlow"]);
                //txtClampOpenPosition.Text = _global.Fld2Num(oRow["ClampOpenPosition"], 2);
                //txtEjectorStroke.Text = _global.Fld2Num(oRow["EjectorStroke"], 2);
                txtInjectionPressureGaugePSI.Text = _global.Fld2Num(oRow["InjectionPressureGaugePSI"]);
                txtHoldingGauge1_PSI.Text = _global.Fld2Num(oRow["HoldingGauge1_PSI"]);
                txtHoldingGauge2_PSI.Text = _global.Fld2Num(oRow["HoldingGauge2_PSI"]);
                txtBackPressureGauge1_PSI.Text = _global.Fld2Num(oRow["BackPressureGauge1_PSI"]);
                txtBackPressureGauge2_PSI.Text = _global.Fld2Num(oRow["BackPressureGauge2_PSI"]);
                //txtFinalCushion.Text = _global.Fld2Num(oRow["FinalCushion"], 2);
            }
            oDs.Dispose();
        }
        private void InitRecoveryClampProfile()
        {
            hRecoveryClampProfileRecId.Value = "";
            //txtPrePullback.Text = "";
            //txtPrePullbackStroke.Text = "";
            txtSuckbackBeforePosition.Text = "";
            txtSuckbackAfterPosition.Text = "";
            txtScrewStartDelay.Text = "";
            txtChargePosition1.Text = "";
            txtChargePosition2.Text = "";
            txtChargePressure1.Text = "";
            txtChargePressure2.Text = "";
            txtChargeFlow1.Text = "";
            txtChargeFlow2.Text = "";
            //txtScrewRecovery1Percent.Text = "";
            //txtScrewRecovery2Percent.Text = "";
            //txtScrewRecovery1RPM.Text = "";
            //txtScrewRecovery2RPM.Text = "";
            txtBackPressure1.Text = "";
            txtBackPressure2.Text = "";
            //txtPostPullback.Text = "";
            //txtPostPullbackSpeedPercent.Text = "";
            //txtPostPullbackStroke.Text = "";
            txtScrewPositonAfter.Text = "";
            //txtScrewRecoveryTime.Text = "";
            txtScrewChargeTime.Text = "";
            txtCoolingTimeSeconds.Text = "";
            txtMoldProtectPressure.Text = "";
            txtMoldProtectFlow.Text = "";
            txtMoldProtectTime.Text = "";
            //txtClampTonnage.Text = "";
            txtClampHighPressurePSI.Text = "";
            txtClampHighPressureFlow.Text = "";
            //txtClampOpenPosition.Text = "";
            //txtEjectorStroke.Text = "";
            txtInjectionPressureGaugePSI.Text = "";
            txtHoldingGauge1_PSI.Text = "";
            txtHoldingGauge2_PSI.Text = "";
            txtBackPressureGauge1_PSI.Text = "";
            txtBackPressureGauge2_PSI.Text = "";
            //txtFinalCushion.Text = "";
        }

        private string SaveReferenceData()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postReferenceData(_global.Fld2Int(hReferenceDataRecId.Value),
                                                    _global.Fld2Int(hMachineDataHeaderId.Value),
                                                    _global.Fld2Dbl(txtFillOnlyTime.Text),
                                                    _global.Fld2Dbl(txtFillOnlyWeight.Text),
                                                    _global.Fld2Int(txtSteelTempSide_A.Text),
                                                    _global.Fld2Int(txtSteelTempSide_B.Text),
                                                    _global.Fld2Int(txtMeltTemp.Text));

            if (results.errorMessage == "") { hReferenceDataRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetReferenceData()
        {
            InitReferenceData();

            string strSQL = "get_ReferenceData ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hReferenceDataRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                txtFillOnlyTime.Text = _global.Fld2Num(oRow["FillOnlyTime"], 2);
                txtFillOnlyWeight.Text = _global.Fld2Num(oRow["FillOnlyWeight"], 1);
                txtSteelTempSide_A.Text = _global.Fld2Num(oRow["SteelTempSide_A"]);
                txtSteelTempSide_B.Text = _global.Fld2Num(oRow["SteelTempSide_B"]);
                txtMeltTemp.Text = _global.Fld2Num(oRow["MeltTemp"]);

            }
            oDs.Dispose();
        }
        private void InitReferenceData()
        {
            hReferenceDataRecId.Value = "";
            txtFillOnlyTime.Text = "";
            txtFillOnlyWeight.Text = "";
            txtSteelTempSide_A.Text = "";
            txtSteelTempSide_B.Text = "";
            txtMeltTemp.Text = "";
        }

        private string SaveValveGateData()
        {
            _database.postResults results = new _database.postResults();
            results = _database.postValveGateData(_global.Fld2Int(hValveGateDataRecId.Value),
                                                    _global.Fld2Int(hMachineDataHeaderId.Value),
                                                    0, //_global.Fld2Dbl(txtOpenDelay_VG1.Text),
                                                    _global.Fld2Dbl(txtOpenTime_VG1.Text),
                                                    _global.Fld2Dbl(txtOpenPostion_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtOpenHold_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtCloseDelay_VG1.Text),
                                                    _global.Fld2Dbl(txtCloseTime_VG1.Text),
                                                    _global.Fld2Dbl(txtClosePostion_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtCloseHold_VG1.Text),
                                                    _global.Fld2Dbl(txtHoldDelay_OnOff_VG1.Text),
                                                    _global.Fld2Dbl(txtHoldActive_OnOff_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtAdvCloseTime_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionHPEndTime_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtTransfer_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtPSI_VG1.Text),
                                                    0, //_global.Fld2Dbl(txtOpenDelay_VG2.Text),
                                                    _global.Fld2Dbl(txtOpenTime_VG2.Text),
                                                    _global.Fld2Dbl(txtOpenPostion_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtOpenHold_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtCloseDelay_VG2.Text),
                                                    _global.Fld2Dbl(txtCloseTime_VG2.Text),
                                                    _global.Fld2Dbl(txtClosePostion_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtCloseHold_VG2.Text),
                                                    _global.Fld2Dbl(txtHoldDelay_OnOff_VG2.Text),
                                                    _global.Fld2Dbl(txtHoldActive_OnOff_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtAdvCloseTime_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionHPEndTime_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtTransfer_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtPSI_VG2.Text),
                                                    0, //_global.Fld2Dbl(txtOpenDelay_VG3.Text),
                                                    _global.Fld2Dbl(txtOpenTime_VG3.Text),
                                                    _global.Fld2Dbl(txtOpenPostion_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtOpenHold_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtCloseDelay_VG3.Text),
                                                    _global.Fld2Dbl(txtCloseTime_VG3.Text),
                                                    _global.Fld2Dbl(txtClosePostion_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtCloseHold_VG3.Text),
                                                    _global.Fld2Dbl(txtHoldDelay_OnOff_VG3.Text),
                                                    _global.Fld2Dbl(txtHoldActive_OnOff_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtAdvCloseTime_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionHPEndTime_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtTransfer_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtPSI_VG3.Text),
                                                    0, //_global.Fld2Dbl(txtOpenDelay_VG4.Text),
                                                    _global.Fld2Dbl(txtOpenTime_VG4.Text),
                                                    _global.Fld2Dbl(txtOpenPostion_VG4.Text),
                                                    0, //_global.Fld2Dbl(txtOpenHold_VG4.Text),
                                                    0, //_global.Fld2Dbl(txtCloseDelay_VG4.Text),
                                                    _global.Fld2Dbl(txtCloseTime_VG4.Text),
                                                    _global.Fld2Dbl(txtClosePostion_VG4.Text),
                                                    0, //_global.Fld2Dbl(txtCloseHold_VG4.Text),
                                                    _global.Fld2Dbl(txtHoldDelay_OnOff_VG4.Text),
                                                    _global.Fld2Dbl(txtHoldActive_OnOff_VG4.Text),
                                                    0, //_global.Fld2Dbl(txtAdvCloseTime_VG4.Text),
                                                    0, //_global.Fld2Dbl(txtInjectionHPEndTime_VG4.Text),
                                                    0, //_global.Fld2Dbl(txtTransfer_VG4.Text)
                                                    0); //_global.Fld2Dbl(txtPSI_VG4.Text);

            if (results.errorMessage == "") { hValveGateDataRecId.Value = results.recId.ToString(); }

            return results.errorMessage;
        }
        private void GetValveGateData()
        {
            InitValveGateData();

            string strSQL = "get_ValveGateData ";
            strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@MachineDataHeaderId = " + hMachineDataHeaderId.Value;

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL);
            foreach (DataRow oRow in oDs.Tables[0].Rows)
            {
                hValveGateDataRecId.Value = _global.Fld2IntStr(oRow["RecId"]);
                //txtOpenDelay_VG1.Text = _global.Fld2Num(oRow["OpenDelay_VG1"], 1);
                txtOpenTime_VG1.Text = _global.Fld2Num(oRow["OpenTime_VG1"], 1);
                txtOpenPostion_VG1.Text = _global.Fld2Num(oRow["OpenPostion_VG1"], 1);
                //txtOpenHold_VG1.Text = _global.Fld2Num(oRow["OpenHold_VG1"], 1);
                //txtCloseDelay_VG1.Text = _global.Fld2Num(oRow["CloseDelay_VG1"], 1);
                txtCloseTime_VG1.Text = _global.Fld2Num(oRow["CloseTime_VG1"], 1);
                txtClosePostion_VG1.Text = _global.Fld2Num(oRow["ClosePostion_VG1"], 1);
                //txtCloseHold_VG1.Text = _global.Fld2Num(oRow["CloseHold_VG1"], 1);
                txtHoldDelay_OnOff_VG1.Text = _global.Fld2Num(oRow["HoldDelay_OnOff_VG1"], 1);
                txtHoldActive_OnOff_VG1.Text = _global.Fld2Num(oRow["HoldActive_OnOff_VG1"], 1);
                //txtAdvCloseTime_VG1.Text = _global.Fld2Num(oRow["AdvCloseTime_VG1"], 1);
                //txtInjectionHPEndTime_VG1.Text = _global.Fld2Num(oRow["InjectionHPEndTime_VG1"], 1);
                //txtTransfer_VG1.Text = _global.Fld2Num(oRow["Transfer_VG1"], 1);
                //txtPSI_VG1.Text = _global.Fld2Num(oRow["PSI_VG1"], 1);
                //txtOpenDelay_VG2.Text = _global.Fld2Num(oRow["OpenDelay_VG2"], 1);
                txtOpenTime_VG2.Text = _global.Fld2Num(oRow["OpenTime_VG2"], 1);
                txtOpenPostion_VG2.Text = _global.Fld2Num(oRow["OpenPostion_VG2"], 1);
                //txtOpenHold_VG2.Text = _global.Fld2Num(oRow["OpenHold_VG2"], 1);
                //txtCloseDelay_VG2.Text = _global.Fld2Num(oRow["CloseDelay_VG2"], 1);
                txtCloseTime_VG2.Text = _global.Fld2Num(oRow["CloseTime_VG2"], 1);
                txtClosePostion_VG2.Text = _global.Fld2Num(oRow["ClosePostion_VG2"], 1);
                //txtCloseHold_VG2.Text = _global.Fld2Num(oRow["CloseHold_VG2"], 1);
                txtHoldDelay_OnOff_VG2.Text = _global.Fld2Num(oRow["HoldDelay_OnOff_VG2"], 1);
                txtHoldActive_OnOff_VG2.Text = _global.Fld2Num(oRow["HoldActive_OnOff_VG2"], 1);
                //txtAdvCloseTime_VG2.Text = _global.Fld2Num(oRow["AdvCloseTime_VG2"], 1);
                //txtInjectionHPEndTime_VG2.Text = _global.Fld2Num(oRow["InjectionHPEndTime_VG2"], 1);
                //txtTransfer_VG2.Text = _global.Fld2Num(oRow["Transfer_VG2"], 1);
                //txtPSI_VG2.Text = _global.Fld2Num(oRow["PSI_VG2"], 1);
                //txtOpenDelay_VG3.Text = _global.Fld2Num(oRow["OpenDelay_VG3"], 1);
                txtOpenTime_VG3.Text = _global.Fld2Num(oRow["OpenTime_VG3"], 1);
                txtOpenPostion_VG3.Text = _global.Fld2Num(oRow["OpenPostion_VG3"], 1);
                //txtOpenHold_VG3.Text = _global.Fld2Num(oRow["OpenHold_VG3"], 1);
                //txtCloseDelay_VG3.Text = _global.Fld2Num(oRow["CloseDelay_VG3"], 1);
                txtCloseTime_VG3.Text = _global.Fld2Num(oRow["CloseTime_VG3"], 1);
                txtClosePostion_VG3.Text = _global.Fld2Num(oRow["ClosePostion_VG3"], 1);
                //txtCloseHold_VG3.Text = _global.Fld2Num(oRow["CloseHold_VG3"], 1);
                txtHoldDelay_OnOff_VG3.Text = _global.Fld2Num(oRow["HoldDelay_OnOff_VG3"], 1);
                txtHoldActive_OnOff_VG3.Text = _global.Fld2Num(oRow["HoldActive_OnOff_VG3"], 1);
                //txtAdvCloseTime_VG3.Text = _global.Fld2Num(oRow["AdvCloseTime_VG3"], 1);
                //txtInjectionHPEndTime_VG3.Text = _global.Fld2Num(oRow["InjectionHPEndTime_VG3"], 1);
                //txtTransfer_VG3.Text = _global.Fld2Num(oRow["Transfer_VG3"], 1);
                //txtPSI_VG3.Text = _global.Fld2Num(oRow["PSI_VG3"], 1);
                //txtOpenDelay_VG4.Text = _global.Fld2Num(oRow["OpenDelay_VG4"], 1);
                txtOpenTime_VG4.Text = _global.Fld2Num(oRow["OpenTime_VG4"], 1);
                txtOpenPostion_VG4.Text = _global.Fld2Num(oRow["OpenPostion_VG4"], 1);
                //txtOpenHold_VG4.Text = _global.Fld2Num(oRow["OpenHold_VG4"], 1);
                //txtCloseDelay_VG4.Text = _global.Fld2Num(oRow["CloseDelay_VG4"], 1);
                txtCloseTime_VG4.Text = _global.Fld2Num(oRow["CloseTime_VG4"], 1);
                txtClosePostion_VG4.Text = _global.Fld2Num(oRow["ClosePostion_VG4"], 1);
                //txtCloseHold_VG4.Text = _global.Fld2Num(oRow["CloseHold_VG4"], 1);
                txtHoldDelay_OnOff_VG4.Text = _global.Fld2Num(oRow["HoldDelay_OnOff_VG4"], 1);
                txtHoldActive_OnOff_VG4.Text = _global.Fld2Num(oRow["HoldActive_OnOff_VG4"], 1);
                //txtAdvCloseTime_VG4.Text = _global.Fld2Num(oRow["AdvCloseTime_VG4"], 1);
                //txtInjectionHPEndTime_VG4.Text = _global.Fld2Num(oRow["InjectionHPEndTime_VG4"], 1);
                //txtTransfer_VG4.Text = _global.Fld2Num(oRow["Transfer_VG4"], 1);
                //txtPSI_VG4.Text = _global.Fld2Num(oRow["PSI_VG4"], 1);
            }
            oDs.Dispose();
        }
        private void InitValveGateData()
        {
            hValveGateDataRecId.Value = "";
            //txtOpenDelay_VG1.Text = "";
            txtOpenTime_VG1.Text = "";
            txtOpenPostion_VG1.Text = "";
            //txtOpenHold_VG1.Text = "";
            //txtCloseDelay_VG1.Text = "";
            txtCloseTime_VG1.Text = "";
            txtClosePostion_VG1.Text = "";
            //txtCloseHold_VG1.Text = "";
            txtHoldDelay_OnOff_VG1.Text = "";
            txtHoldActive_OnOff_VG1.Text = "";
            //txtAdvCloseTime_VG1.Text = "";
            //txtInjectionHPEndTime_VG1.Text = "";
            //txtTransfer_VG1.Text = "";
            //txtPSI_VG1.Text = "";
            //txtOpenDelay_VG2.Text = "";
            txtOpenTime_VG2.Text = "";
            txtOpenPostion_VG2.Text = "";
            //txtOpenHold_VG2.Text = "";
            //txtCloseDelay_VG2.Text = "";
            txtCloseTime_VG2.Text = "";
            txtClosePostion_VG2.Text = "";
            //txtCloseHold_VG2.Text = "";
            txtHoldDelay_OnOff_VG2.Text = "";
            txtHoldActive_OnOff_VG2.Text = "";
            //txtAdvCloseTime_VG2.Text = "";
            //txtInjectionHPEndTime_VG2.Text = "";
            //txtTransfer_VG2.Text = "";
            //txtPSI_VG2.Text = "";
            //txtOpenDelay_VG3.Text = "";
            txtOpenTime_VG3.Text = "";
            txtOpenPostion_VG3.Text = "";
            //txtOpenHold_VG3.Text = "";
            //txtCloseDelay_VG3.Text = "";
            txtCloseTime_VG3.Text = "";
            txtClosePostion_VG3.Text = "";
            //txtCloseHold_VG3.Text = "";
            txtHoldDelay_OnOff_VG3.Text = "";
            txtHoldActive_OnOff_VG3.Text = "";
            //txtAdvCloseTime_VG3.Text = "";
            //txtInjectionHPEndTime_VG3.Text = "";
            //txtTransfer_VG3.Text = "";
            //txtPSI_VG3.Text = "";
            //txtOpenDelay_VG4.Text = "";
            txtOpenTime_VG4.Text = "";
            txtOpenPostion_VG4.Text = "";
            //txtOpenHold_VG4.Text = "";
            //txtCloseDelay_VG4.Text = "";
            txtCloseTime_VG4.Text = "";
            txtClosePostion_VG4.Text = "";
            //txtCloseHold_VG4.Text = "";
            txtHoldDelay_OnOff_VG4.Text = "";
            txtHoldActive_OnOff_VG4.Text = "";
            //txtAdvCloseTime_VG4.Text = "";
            //txtInjectionHPEndTime_VG4.Text = "";
            //txtTransfer_VG4.Text = "";
            //txtPSI_VG4.Text = "";
        }

        private void SetTabOrder()
        {
            txtNozzleTipPercent.TabIndex = 100;
            txtNozzleBodyPercent.TabIndex = 101;
            txtAdapter.TabIndex = 102;
            //txtSHHeat.TabIndex = 103;
            //txtBarrelHead.TabIndex = 104;
            txtBarrelFront.TabIndex = 105;
            txtBarrelCenter1.TabIndex = 106;
            txtBarrelCenter2.TabIndex = 107;
            txtBarrelCenter3.TabIndex = 108;
            txtBarrelCenter4.TabIndex = 109;
            txtBarrelCenter5.TabIndex = 110;
            txtBarrelRear.TabIndex = 111;
            txtMoldGateTempF.TabIndex = 112;
            txtMoldFixedHalfTempF.TabIndex = 113;
            txtMoldMovingHalfTempF.TabIndex = 114;
            txtStripperOrOtherTempF.TabIndex = 115;
            txtNozzleTipType.TabIndex = 116;
            txtNozzleTipLengthOALInches.TabIndex = 117;
            txtNozzleTipOrificeSizeInches.TabIndex = 118;
            txtNozzleBodyLengthOALInches.TabIndex = 119;
            txtFillOnlyTime.TabIndex = 120;
            txtFillOnlyWeight.TabIndex = 121;
            txtSteelTempSide_A.TabIndex = 122;
            txtSteelTempSide_B.TabIndex = 123;
            txtMeltTemp.TabIndex = 124;
            //txtOpenDelay_VG1.TabIndex = 125;
            txtOpenTime_VG1.TabIndex = 126;
            txtOpenPostion_VG1.TabIndex = 127;
            //txtOpenHold_VG1.TabIndex = 128;
            //txtCloseDelay_VG1.TabIndex = 129;
            txtCloseTime_VG1.TabIndex = 130;
            txtClosePostion_VG1.TabIndex = 131;
            //txtCloseHold_VG1.TabIndex = 132;
            txtHoldDelay_OnOff_VG1.TabIndex = 133;
            txtHoldActive_OnOff_VG1.TabIndex = 134;
            //txtAdvCloseTime_VG1.TabIndex = 135;
            //txtInjectionHPEndTime_VG1.TabIndex = 136;
            //txtTransfer_VG1.TabIndex = 137;
            //txtPSI_VG1.TabIndex = 138;
            //txtOpenDelay_VG2.TabIndex = 139;
            txtOpenTime_VG2.TabIndex = 140;
            txtOpenPostion_VG2.TabIndex = 141;
            //txtOpenHold_VG2.TabIndex = 142;
            //txtCloseDelay_VG2.TabIndex = 143;
            txtCloseTime_VG2.TabIndex = 144;
            txtClosePostion_VG2.TabIndex = 145;
            //txtCloseHold_VG2.TabIndex = 146;
            txtHoldDelay_OnOff_VG2.TabIndex = 147;
            txtHoldActive_OnOff_VG2.TabIndex = 148;
            //txtAdvCloseTime_VG2.TabIndex = 149;
            //txtInjectionHPEndTime_VG2.TabIndex = 150;
            //txtTransfer_VG2.TabIndex = 151;
            //txtPSI_VG2.TabIndex = 152;
            //txtOpenDelay_VG3.TabIndex = 153;
            txtOpenTime_VG3.TabIndex = 154;
            txtOpenPostion_VG3.TabIndex = 155;
            //txtOpenHold_VG3.TabIndex = 156;
            //txtCloseDelay_VG3.TabIndex = 157;
            txtCloseTime_VG3.TabIndex = 158;
            txtClosePostion_VG3.TabIndex = 159;
            //txtCloseHold_VG3.TabIndex = 160;
            txtHoldDelay_OnOff_VG3.TabIndex = 161;
            txtHoldActive_OnOff_VG3.TabIndex = 162;
            //txtAdvCloseTime_VG3.TabIndex = 163;
            //txtInjectionHPEndTime_VG3.TabIndex = 164;
            //txtTransfer_VG3.TabIndex = 165;
            //txtPSI_VG3.TabIndex = 166;
            //txtOpenDelay_VG4.TabIndex = 167;
            txtOpenTime_VG4.TabIndex = 168;
            txtOpenPostion_VG4.TabIndex = 169;
            //txtOpenHold_VG4.TabIndex = 170;
            //txtCloseDelay_VG4.TabIndex = 171;
            txtCloseTime_VG4.TabIndex = 172;
            txtClosePostion_VG4.TabIndex = 173;
            //txtCloseHold_VG4.TabIndex = 174;
            txtHoldDelay_OnOff_VG4.TabIndex = 175;
            txtHoldActive_OnOff_VG4.TabIndex = 176;
            //txtAdvCloseTime_VG4.TabIndex = 177;
            //txtInjectionHPEndTime_VG4.TabIndex = 178;
            //txtTransfer_VG4.TabIndex = 179;
            //txtPSI_VG4.TabIndex = 180;
            txtShotSize.TabIndex = 181;
            //txtInjectionVelocity1.TabIndex = 182;
            //txtInjectionVelocity2.TabIndex = 183;
            //txtInjectionVelocity3.TabIndex = 184;
            //txtInjectionVelocity4.TabIndex = 185;
            //txtInjectionVelocity5.TabIndex = 186;
            txtInjectionChangePos1.TabIndex = 187;
            txtInjectionChangePos2.TabIndex = 188;
            txtInjectionChangePos3.TabIndex = 189;
            txtInjectionChangePos4.TabIndex = 190;
            txtInjectionPress1.TabIndex = 191;
            txtInjectionPress2.TabIndex = 192;
            txtInjectionPress3.TabIndex = 193;
            txtInjectionPress4.TabIndex = 194;
            txtInjectionPress5.TabIndex = 195;
            txtInjectionFlow1.TabIndex = 196;
            txtInjectionFlow2.TabIndex = 197;
            txtInjectionFlow3.TabIndex = 198;
            txtInjectionFlow4.TabIndex = 199;
            txtInjectionFlow5.TabIndex = 200;
            //txtTransMode.TabIndex = 201;
            //txtTransPosition.TabIndex = 202;
            //txtIPS_PSI.TabIndex = 203;
            txtInjectionPositionThreshold.TabIndex = 204;
            txtInjectionPressureThreshold.TabIndex = 205;
            txtInjectionTimeThreshold.TabIndex = 206;
            //txtInjectionPressLimit.TabIndex = 207;
            txtInjectionPSIAtTransfer.TabIndex = 208;
            txtInjectionTimeAct.TabIndex = 209;
            txtHoldPress1_PSI.TabIndex = 210;
            txtHoldPress2_PSI.TabIndex = 211;
            //txtHoldPress1_Percent.TabIndex = 212;
            //txtHoldPress2_Percent.TabIndex = 213;
            //txtHoldPress1_Seconds.TabIndex = 214;
            //txtHoldPress2_Seconds.TabIndex = 215;
            txtHoldFlow1.TabIndex = 216;
            txtHoldFlow2.TabIndex = 217;
            txtHoldTime1.TabIndex = 218;
            txtHoldTime2.TabIndex = 219;
            //txtInjectionHoldTime.TabIndex = 220;
            txtFinalCushionMM.TabIndex = 221;
            //txtPrePullback.TabIndex = 222;
            //txtPrePullbackStroke.TabIndex = 223;
            txtSuckbackBeforePosition.TabIndex = 224;
            txtScrewStartDelay.TabIndex = 225;
            txtChargePosition1.TabIndex = 226;
            txtChargePosition2.TabIndex = 227;
            txtChargePressure1.TabIndex = 228;
            txtChargePressure2.TabIndex = 229;
            txtChargeFlow1.TabIndex = 230;
            txtChargeFlow2.TabIndex = 231;
            //txtScrewRecovery1Percent.TabIndex = 232;
            //txtScrewRecovery2Percent.TabIndex = 233;
            //txtScrewRecovery1RPM.TabIndex = 234;
            //txtScrewRecovery2RPM.TabIndex = 235;
            txtBackPressure1.TabIndex = 236;
            txtBackPressure2.TabIndex = 237;
            txtSuckbackAfterPosition.TabIndex = 238;
            //txtPostPullback.TabIndex = 239;
            //txtPostPullbackSpeedPercent.TabIndex = 240;
            //txtPostPullbackStroke.TabIndex = 241;
            txtScrewPositonAfter.TabIndex = 242;
            //txtScrewRecoveryTime.TabIndex = 243;
            txtScrewChargeTime.TabIndex = 244;
            txtCoolingTimeSeconds.TabIndex = 245;
            txtMoldProtectPressure.TabIndex = 246;
            txtMoldProtectFlow.TabIndex = 247;
            txtMoldProtectTime.TabIndex = 248;
            //txtClampTonnage.TabIndex = 249;
            txtClampHighPressurePSI.TabIndex = 250;
            txtClampHighPressureFlow.TabIndex = 251;
            //txtClampOpenPosition.TabIndex = 252;
            //txtEjectorStroke.TabIndex = 253;
            txtInjectionPressureGaugePSI.TabIndex = 254;
            txtHoldingGauge1_PSI.TabIndex = 255;
            txtHoldingGauge2_PSI.TabIndex = 256;
            txtBackPressureGauge1_PSI.TabIndex = 257;
            txtBackPressureGauge2_PSI.TabIndex = 258;
            //txtFinalCushion.TabIndex = 259;
            txtDelay_C1.TabIndex = 260;
            txtHold_C1.TabIndex = 261;
            txtExhaust_C1.TabIndex = 262;
            txtPressure1_C1.TabIndex = 263;
            txtTime1_C1.TabIndex = 264;
            txtPressure2_C1.TabIndex = 265;
            txtTime2_C1.TabIndex = 266;
            txtDelay_C2.TabIndex = 267;
            txtHold_C2.TabIndex = 268;
            txtExhaust_C2.TabIndex = 269;
            txtPressure1_C2.TabIndex = 270;
            txtTime1_C2.TabIndex = 271;
            txtPressure2_C2.TabIndex = 272;
            txtTime2_C2.TabIndex = 273;
            txtDelay_C3.TabIndex = 274;
            txtHold_C3.TabIndex = 275;
            txtExhaust_C3.TabIndex = 276;
            txtPressure1_C3.TabIndex = 277;
            txtTime1_C3.TabIndex = 278;
            txtPressure2_C3.TabIndex = 279;
            txtTime2_C3.TabIndex = 280;
            txtDelay_C4.TabIndex = 281;
            txtHold_C4.TabIndex = 282;
            txtExhaust_C4.TabIndex = 283;
            txtPressure1_C4.TabIndex = 284;
            txtTime1_C4.TabIndex = 285;
            txtPressure2_C4.TabIndex = 286;
            txtTime2_C4.TabIndex = 287;
            txtGate_HB_Man_Pos1_Box1.TabIndex = 288;
            txtGate_HB_Man_Pos2_Box1.TabIndex = 289;
            txtGate_HB_Man_Pos3_Box1.TabIndex = 290;
            txtGate_HB_Man_Pos4_Box1.TabIndex = 291;
            txtGate_HB_Man_Pos5_Box1.TabIndex = 292;
            txtGate_HB_Man_Pos6_Box1.TabIndex = 293;
            txtGate_HB_Man_Pos1_Box2.TabIndex = 294;
            txtGate_HB_Man_Pos2_Box2.TabIndex = 295;
            txtGate_HB_Man_Pos3_Box2.TabIndex = 296;
            txtGate_HB_Man_Pos4_Box2.TabIndex = 297;
            txtGate_HB_Man_Pos5_Box2.TabIndex = 298;
            txtGate_HB_Man_Pos6_Box2.TabIndex = 299;
            txtGate_HB_Man_Pos1_Box3.TabIndex = 300;
            txtGate_HB_Man_Pos2_Box3.TabIndex = 301;
            txtGate_HB_Man_Pos3_Box3.TabIndex = 302;
            txtGate_HB_Man_Pos4_Box3.TabIndex = 303;
            txtGate_HB_Man_Pos5_Box3.TabIndex = 304;
            txtGate_HB_Man_Pos6_Box3.TabIndex = 305;
            txtGate_HB_Man_Pos1_Box4.TabIndex = 306;
            txtGate_HB_Man_Pos2_Box4.TabIndex = 307;
            txtGate_HB_Man_Pos3_Box4.TabIndex = 308;
            txtGate_HB_Man_Pos4_Box4.TabIndex = 309;
            txtGate_HB_Man_Pos5_Box4.TabIndex = 310;
            txtGate_HB_Man_Pos6_Box4.TabIndex = 311;
            txtGate_HB_Man_Pos1_Box5.TabIndex = 312;
            txtGate_HB_Man_Pos2_Box5.TabIndex = 313;
            txtGate_HB_Man_Pos3_Box5.TabIndex = 314;
            txtGate_HB_Man_Pos4_Box5.TabIndex = 315;
            txtGate_HB_Man_Pos5_Box5.TabIndex = 316;
            txtGate_HB_Man_Pos6_Box5.TabIndex = 317;
            txtGate_HB_Man_Pos1_Box6.TabIndex = 318;
            txtGate_HB_Man_Pos2_Box6.TabIndex = 319;
            txtGate_HB_Man_Pos3_Box6.TabIndex = 320;
            txtGate_HB_Man_Pos4_Box6.TabIndex = 321;
            txtGate_HB_Man_Pos5_Box6.TabIndex = 322;
            txtGate_HB_Man_Pos6_Box6.TabIndex = 323;
            txtComments.TabIndex = 324;

            btnSaveBottom.TabIndex = 325;
        }

        protected bool GetToolNumbers(string partNumber)
        {
            bool results = false;
            InitAutoLoad(1);

            //if blank load dummy valuse so correct dataset is returned
            partNumber = ((partNumber != "") ? partNumber : "*blank*");

            string strSQL = "get_MachineDefaults ";
            strSQL += "@userId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@PlantNumber = " + _global.Str2Fld(hPlantNumber.Value) + ", ";
            strSQL += "@MachineType = " + _global.Str2Fld(hMachineType.Value) + ", ";
            strSQL += "@PartNumber = " + _global.Str2Fld(partNumber);

            DataSet oDs = _shared.getDataSet(strSQL, _global.conString());
            if (oDs.Tables[0].Rows.Count != 0)
            {
                ddlToolNumber.DataTextField = "ToolNumber";
                ddlToolNumber.DataValueField = "ToolNumber";
                ddlToolNumber.DataSource = oDs;
                ddlToolNumber.DataBind();
                ddlToolNumber.Items.Insert(0, new ListItem("- Select One -", ""));

                results = true;
            }
            oDs.Dispose();

            return results;
        }
        protected bool GetMachineNumbers(string partNumber, string toolNumber)
        {
            bool results = false;
            InitAutoLoad(2);

            //if blank load dummy valuse so correct dataset is returned
            partNumber = ((partNumber != "") ? partNumber : "*blank*");
            toolNumber = ((toolNumber != "") ? toolNumber : "*blank*");

            string strSQL = "get_MachineDefaults ";
            strSQL += "@userId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
            strSQL += "@PlantNumber = " + _global.Str2Fld(hPlantNumber.Value) + ", ";
            strSQL += "@MachineType = " + _global.Str2Fld(hMachineType.Value) + ", ";
            strSQL += "@PartNumber = " + _global.Str2Fld(partNumber) + ", ";
            strSQL += "@ToolNumber = " + _global.Str2Fld(toolNumber);

            DataSet oDs = _shared.getDataSet(strSQL, _global.conString());
            if (oDs.Tables[0].Rows.Count != 0)
            {
                ddlMachineNumber.DataTextField = "MachineNumber";
                ddlMachineNumber.DataValueField = "MachineNumber";
                ddlMachineNumber.DataSource = oDs;
                ddlMachineNumber.DataBind();
                ddlMachineNumber.Items.Insert(0, new ListItem("- Select One -", ""));

                results = true;
            }
            oDs.Dispose();

            return results;
        }

        private void SecureForm(int action, string status)
        {
            if (action == runSheetActions.IsView)
            {
                tblHeader.Visible = false;
                btnSaveTop.Visible = false;
                btnSaveBottom.Visible = false;

                SetControls(true, pnlPage);

                // Access_Y can edit run comments
                if (status == "VALIDATING" && _security.isInRole("Machine Run Administrator"))
                {
                    SetControls(false, dvRunComments);

                    tblHeader.Visible = true;
                    btnSaveTop.Visible = true;
                    btnSaveBottom.Visible = true;
                }
                else
                {
                    //if protected and tblHeader not displayed disable date pickers
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "disableDatePickers", "disableDatePickers();", true);
                }
            }

            hProtect.Value = ((action == runSheetActions.IsView) ? "1" : "0");
        }

        private void SetControls(bool protect, Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox) { ((TextBox)ctrl).Enabled = !protect; }
                if (ctrl is DropDownList) { ((DropDownList)ctrl).Enabled = !protect; }
                //cascade through child containers
                if (ctrl.Controls.Count != 0) SetControls(protect, ctrl);
            }
        }

        private void InitAutoLoad(int step)
        {
            // part number selection
            if (step == 1)
            {
                ddlToolNumber.DataSource = null;
                ddlToolNumber.DataBind();
            }

            // tool number selection
            if (step <= 2)
            {
                ddlMachineNumber.DataSource = null;
                ddlMachineNumber.DataBind();
            }

            // machine number selection (dont clear once they've been saved)
            if (step <= 3 && hMachineDataHeaderId.Value == "")
            {
                lblMaterialNumber.InnerHtml = "";
                lblMaterialDescription.InnerHtml = "";
                lblResinAdditiveNumber.InnerHtml = "";
                lblResinAdditiveDesc.InnerHtml = "";
                lblColorantNumber.InnerHtml = "";
                lblColorantDesc.InnerHtml = "";
                lblColorPercentage.InnerHtml = "";
                lblToolCavities.InnerHtml = "";
                lblCycleTimeStd.InnerHtml = "";
                lblShotWeightStd.InnerHtml = "";
                lblMachineNumber.InnerHtml = "";
            }
        }
        private void ClearMessage()
        {
            ErrorMessage("");
        }
        private void ErrorMessage(string message)
        {
            lblErrorTop.Text = _global.FormatError(message);
            lblErrorBottom.Text = lblErrorTop.Text;
            lblErrorTop.ForeColor = System.Drawing.Color.Red;
            lblErrorBottom.ForeColor = System.Drawing.Color.Red;
        }
        private void SuccessMessage(string message)
        {
            lblErrorTop.Text = message;
            lblErrorBottom.Text = lblErrorTop.Text;
            lblErrorTop.ForeColor = System.Drawing.Color.DarkGreen;
            lblErrorBottom.ForeColor = System.Drawing.Color.DarkGreen;
        }
    }
}