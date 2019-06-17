using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Management;
using System.Drawing.Printing;

namespace cambro
{
    public partial class _machineRunData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // redirect is not authorized
            if (!_security.checkSecurity("forms/machineRunData.aspx").isAuthorized)
            {
                Response.Redirect(_global.rootURL() + "default.aspx?target=NOTAUTHORIZED", true);
            }

            if (!Page.IsPostBack) { BindData(); }

            //only these roles can add new forms
            bool bolShowControl = _security.isInRole("Machine Run Administrator,Operations");
            thAddNew.Visible = bolShowControl;
            tdAddNew.Visible = bolShowControl;

            _global.ButtonClickEventGovenor(btnSearch);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void Print(string machineType, int machineDataHeaderId)
        {
            string strResults = _shared.renderReport(machineType, machineDataHeaderId);

            if (strResults.Contains("failed"))
            {
                ErrorMessage(strResults);
            }
            else
            {
                string strScript = "window.open('" + strResults + "','_blank');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "viewPDF", strScript, true);
            }

        }

        protected void rpRunData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //apply role based security
                bool bolAllowEdit = _global.Int2Bol(((HiddenField)e.Item.FindControl("hAllowEdit")).Value);
                bool bolAllowView = _global.Int2Bol(((HiddenField)e.Item.FindControl("hAllowView")).Value);
                bool bolAllowCopy = _global.Int2Bol(((HiddenField)e.Item.FindControl("hAllowCopy")).Value);
                bool bolAllowDelete = _global.Int2Bol(((HiddenField)e.Item.FindControl("hAllowDelete")).Value);
                bool bolAllowPrint = _global.Int2Bol(((HiddenField)e.Item.FindControl("hAllowPrint")).Value);
                bool bolAllowStatusUpdate = _global.Int2Bol(((HiddenField)e.Item.FindControl("hAllowStatusUpdate")).Value);

                ((HtmlGenericControl)e.Item.FindControl("dvEdit")).Visible = bolAllowEdit;
                ((HtmlGenericControl)e.Item.FindControl("dvCopy")).Visible = bolAllowCopy;
                ((HtmlGenericControl)e.Item.FindControl("dvView")).Visible = bolAllowView;
                ((HtmlGenericControl)e.Item.FindControl("dvDelete")).Visible = bolAllowDelete;
                ((HtmlGenericControl)e.Item.FindControl("dvPrint")).Visible = bolAllowPrint;
                ((HtmlGenericControl)e.Item.FindControl("dvStatusUpdate")).Visible = bolAllowStatusUpdate;
            }
        }

        protected void ddlPlantNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hPlantNumber.Value != ddlPlantNumber.Text)
            {
                hPlantNumber.Value = ((ddlPlantNumber.SelectedIndex > 0) ? ddlPlantNumber.Text : "");
                HttpContext.Current.Session["_selectedPlantNumber"] = hPlantNumber.Value;
                ResetForm();
            }
        }

        protected void ddlPrintBlank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPrintBlank.Text != "- Select One -")
            {
                Print(ddlPrintBlank.Text, -1);
                ddlPrintBlank.SelectedIndex = 0;
            }
        }
        protected void ddlAddNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            //plant number must be selected
            if (_security.plantNumber() == "")
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Plant selection is required.";
                ddlAddNew.SelectedIndex = 0;
                return;
            }

            // pop appropriate form for adding
            if (ddlAddNew.Text != "- Select One -")
            {
                string strScript = "showForm('" + _global.rootURL() + "forms/', '" + ddlAddNew.Text + "', '0', '1');";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "addNew", strScript, true);
                ddlAddNew.SelectedIndex = 0;
            }
        }

        public void LoadGrid()
        {
            lblError.Text = "";

            //replace not selection with blank
            string strMachintType = ((ddlSearchMachineType.SelectedIndex <= 0) ? "" : ddlSearchMachineType.Text);

            DataSet oDs = new DataSet();
            try
            {
                string strSQL = "get_MachineDataHeader ";
                strSQL += "@UserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
                strSQL += "@SearchPlantNumber = " + _global.Str2Fld(hPlantNumber.Value) + ", ";
                strSQL += "@SearchPartNumber = " + _global.Str2Fld(txtSearchPartNumber.Text) + ", ";
                strSQL += "@SearchToolNumber = " + _global.Str2Fld(txtSearchToolNumber.Text) + ", ";
                strSQL += "@SearchMachineType = " + _global.Str2Fld(strMachintType);

                oDs = _shared.getDataSet(strSQL, _global.conString());
                rpRunData.DataSource = oDs.Tables[0];
                rpRunData.DataBind();

                //load message if any, from client side updateStatus() and deleteRunSheet() functions
                if (hPostLoadMessage.Value != "")
                {
                    if (hPostLoadMessage.Value.StartsWith("ERROR")) { ErrorMessage(hPostLoadMessage.Value.Replace("ERROR", "")); }
                    if (hPostLoadMessage.Value.StartsWith("SUCCESS")) { SuccessMessage(hPostLoadMessage.Value.Replace("SUCCESS", "")); }

                    hPostLoadMessage.Value = "";
                }
            }
            catch (Exception ex)
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = ex.Message;
            }
            // Clean up
            oDs.Dispose();
        }

        private void ResetForm() 
        {
            ClearMessage();

            txtSearchPartNumber.Text = "";
            txtSearchToolNumber.Text = "";
            ddlSearchMachineType.SelectedIndex = 0;

            rpRunData.DataSource = null;
            rpRunData.DataBind();
        }

        private void BindData()
        {
            ddlPrintBlank.Items.Clear();
            ddlPrintBlank.Items.Add("- Select One -");
            ddlPrintBlank.Items.Add("HAITIAN");
            ddlPrintBlank.Items.Add("HUSKY");
            ddlPrintBlank.Items.Add("JSW");

            ddlAddNew.Items.Clear();
            ddlAddNew.Items.Add("- Select One -");
            ddlAddNew.Items.Add("HAITIAN");
            ddlAddNew.Items.Add("HUSKY");
            ddlAddNew.Items.Add("JSW");

            ddlSearchMachineType.Items.Clear();
            ddlSearchMachineType.Items.Add("- Select One -");
            ddlSearchMachineType.Items.Add("HAITIAN");
            ddlSearchMachineType.Items.Add("HUSKY");
            ddlSearchMachineType.Items.Add("JSW");

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet("[shared].[get_constants] @ConType = 'PLANTS'", _global.conString());
            ddlPlantNumber.DataTextField = "conText";
            ddlPlantNumber.DataValueField = "conValue";
            ddlPlantNumber.DataSource = oDs;
            ddlPlantNumber.DataBind();
            oDs.Dispose();
            ddlPlantNumber.Items.Insert(0, "- Select One -");

            //default to user's plant, no user default allow selection
            HttpContext.Current.Session["_selectedPlantNumber"] = null;
            hPlantNumber.Value = _security.plantNumber();
            if (hPlantNumber.Value != "") {
                thPlantNumber.Visible = false;
                tdPlantNumber.Visible = false;
            }
        }

        private void ClearMessage()
        {
            ErrorMessage("");
        }
        private void ErrorMessage(string message)
        {
            lblError.Text = _global.FormatError(message);
            lblError.ForeColor = System.Drawing.Color.Red;
        }
        private void SuccessMessage(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = System.Drawing.Color.DarkGreen;
        }
    }
}