<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="runDataJSW.aspx.cs" Inherits="cambro._runDataJSW" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JSW</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <link href=<%="'../css/runDataForms.css?v="+ cambro._global.cachedVersion() +"'"%> rel="stylesheet" />
    <link href="../css/bootstrap/jquery-ui.css" rel="stylesheet" />

    <script src="../scripts/jquery/jquery-3.3.1.min.js"></script>
    <script src="../scripts/jquery/jquery-ui.js"></script>
    <script src=<%="'../scripts/global.js?v="+ cambro._global.cachedVersion() +"'"%>></script>
    <script>
        $(document).ready(function(){
            $('#txtCertPartsWeightGm').focusout(function () {
                if ($('#txtFinalPartsWeightMid').val().length == 0) {
                    $('#txtFinalPartsWeightMid').val($("#txtCertPartsWeightGm").val());
                    CalcPartsWeight(this);
                }
                return false;
                });
        });

        $(function () {
            $("#txtRunCommentsDate1").datepicker();
            $("#txtRunCommentsDate2").datepicker();
            $("#txtRunCommentsDate3").datepicker();
        });

        function disableDatePickers()
        {
            $("#txtRunCommentsDate1").datepicker().datepicker('disable');
            $("#txtRunCommentsDate2").datepicker().datepicker('disable');
            $("#txtRunCommentsDate3").datepicker().datepicker('disable');
        }

        function CalcCycleTimes(e) {
            var max = parseFloat(e.value);
            var mid = (max - .5).toFixed(2);
            var min = (max - 1.5).toFixed(2);
            $('#lblCycleTimeMid').html(mid);
            $('#lblCycleTimeMin').html(min);

            $('#hCycleTimeMid').val(mid);
            $('#hCycleTimeMin').val(min);
        }

        function CalcPartsWeight(e) {
            var mid = parseFloat(e.value);
            var min = (mid * .995).toFixed(3);
            var max = (mid * 1.005).toFixed(3);
            $('#lblFinalPartsWeightMin').html(min);
            $('#lblFinalPartsWeightMax').html(max);

            $('#hFinalPartsWeightMin').val(min);
            $('#hFinalPartsWeightMax').val(max);
        }
        function getTools() {
            var _id = $$("btnGetTools");
            __doPostBack(_id.attr("name"), 'OnClick');
            return true;
        }
    </script>
</head>
<body>
    <form id="runDataform" runat="server">
        <asp:HiddenField ID="hProtect" runat="server" />
        <asp:HiddenField ID="hPlantNumber" runat="server" />
        <asp:HiddenField ID="hMachineType" runat="server" />
        <asp:HiddenField ID="hMachineDataHeaderId" runat="server" />

        <asp:HiddenField ID="hBarrelTemperaturesRecId" runat="server" />
        <asp:HiddenField ID="hGasAssistDataRecId" runat="server" />
        <asp:HiddenField ID="hHotTipControllerRecId" runat="server" />
        <asp:HiddenField ID="hInjectionProfileRecId" runat="server" />
        <asp:HiddenField ID="hMoldCoolingTempsRecId" runat="server" />
        <asp:HiddenField ID="hNozzleInformationRecId" runat="server" />
        <asp:HiddenField ID="hRecoveryClampProfileRecId" runat="server" />
        <asp:HiddenField ID="hReferenceDataRecId" runat="server" />
        <asp:HiddenField ID="hValveGateDataRecId" runat="server" />

        <asp:HiddenField ID="hCycleTimeMid" runat="server" />
        <asp:HiddenField ID="hCycleTimeMin" runat="server" />

        <asp:HiddenField ID="hFinalPartsWeightMin" runat="server" />
        <asp:HiddenField ID="hFinalPartsWeightMax" runat="server" />

        <asp:Button ID="btnGetTools" runat="server" Style="display: none;" OnClick="btnGetTools_Click" />
        <asp:ScriptManager ID="scScriptManager" runat="server"></asp:ScriptManager>
        <asp:Panel ID="pnlPage" runat="server" class="page" DefaultButton="btnSaveTop">
            <div>
                <table class="table" style="width: 100%">
                    <tr>
                        <td style="width: 33%;">
                            <img src="../images/logo.png" /></td>
                        <th class="heading" style="width: 33%; text-align: center;">
                            <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label></th>
                        <td style="width: 33%; text-align: right; padding-right: 10px;">
                            <asp:Button ID="btnSaveTop" runat="server" Text="Save" class="btn-save btn-lg" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <table id="tblHeader" runat="server" class="table" style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="error">
                            <asp:Label ID="lblErrorTop" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                </table>
            </div>

            <%--Run Comments--%>
            <div id="dvRunComments" runat="server" class="div-runComments">
                <table class="table">
                    <tr>
                        <th class="table-default"></th>
                        <th class="table-header-row">Run Date</th>
                        <th class="table-header-row">Run Comment</th>
                        <th class="table-header-row">Cycle Time</th>
                        <th class="table-header-row">Shot Weight</th>
                    </tr>
                    <tr>
                        <td>1.</td>
                        <td>
                            <asp:TextBox ID="txtRunCommentsDate1" runat="server" cssClass="textBox-date"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunComments1" runat="server" cssClass="textBox-runComments"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunCycleTime1" runat="server" cssClass="numberBox-15" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunShotWeight1" runat="server" cssClass="numberBox-15" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>2.</td>
                        <td>
                            <asp:TextBox ID="txtRunCommentsDate2" runat="server" cssClass="textBox-date"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunComments2" runat="server" cssClass="textBox-runComments"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunCycleTime2" runat="server" cssClass="numberBox-15" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunShotWeight2" runat="server" cssClass="numberBox-15" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>3.</td>
                        <td>
                            <asp:TextBox ID="txtRunCommentsDate3" runat="server" cssClass="textBox-date"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunComments3" runat="server" cssClass="textBox-runComments"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunCycleTime3" runat="server" cssClass="numberBox-15" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunShotWeight3" runat="server" cssClass="numberBox-15" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <%--Header--%>
            <table class="table" style="width: 100%;">
                <tr>
                    <th>Part Number:</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtPartNumber" runat="server" cssClass="textBox-20" Style="text-transform: uppercase;" onblur="getTools();"></asp:TextBox>
                    </td>
                    <th>Tool Number:</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlToolNumber" runat="server" cssClass="ddlBox-20" AutoPostBack="true" OnSelectedIndexChanged="ddlToolNumber_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th>Machine Number:</th>
                    <td>
                        <asp:DropDownList ID="ddlMachineNumber" runat="server" cssClass="ddlBox-20" AutoPostBack="true" OnSelectedIndexChanged="ddlMachineNumber_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="text-red">Material Number:</th>
                    <td colspan="3">
                        <div id="lblMaterialNumber" runat="server" class="labelBox-10"></div>
                    </td>
                    <th class="text-red">Resin/Additive Number:</th>
                    <td colspan="2">
                        <div id="lblResinAdditiveNumber" runat="server" class="labelBox-10"></div>
                    </td>
                    <th class="text-red">Colorant Number:</th>
                    <td>
                        <div id="lblColorantNumber" runat="server" class="labelBox-10"></div>
                    </td>
                </tr>
                <tr>
                    <th class="text-red">Material Desc.:</th>
                    <td colspan="3">
                        <div id="lblMaterialDescription" runat="server" class="labelBox-30"></div>
                    </td>
                    <th class="text-red">Resin/Additive Desc.:</th>
                    <td colspan="2">
                        <div id="lblResinAdditiveDesc" runat="server" class="labelBox-20"></div>
                    </td>
                    <th class="text-red">Colorant Desc.:</th>
                    <td>
                        <div id="lblColorantDesc" runat="server" class="labelBox-30"></div>
                    </td>
                </tr>
                <tr>
                    <th>Technicians Name:</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtTechName" runat="server" cssClass="textBox-20"></asp:TextBox></td>
                    <th class="text-red">Tool Cavities:</th>
                    <td colspan="2">
                        <div id="lblToolCavities" runat="server" class="labelBox-10"></div>
                    </td>
                    <th class="text-red">Color Percentage:</th>
                    <td>
                        <div id="lblColorPercentage" runat="server" class="labelNumberBox-10"></div>
                    </td>
                </tr>
                <tr>
                    <th class="text-red">Date Created:</th>
                    <td colspan="3">
                        <div id="lblDateCreated" runat="server" class="dateBox-10"></div>
                    </td>
                    <th></th>
                    <th class="text-red" style="padding-left: 15px;">Std.</th>
                    <th style="padding-left: 15px;">Cert.</th>
                    <th>McGuire Setting:</th>
                    <td>
                        <asp:TextBox ID="txtMcGuireSetting" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Cert Part(s) Weight (g):</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCertPartsWeightGm" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                    </td>
                    <th class="text-red">Cycle Time (sec):</th>
                    <td>
                        <div id="lblCycleTimeStd" runat="server" class="labelNumberBox-10"></div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCycleTimeCert" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                    </td>
                    <th>Runner Weight (gm):</th>
                    <td>
                        <asp:TextBox ID="txtRunnerWeightGm" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Standardization Info</th>
                    <th style="text-align: center;">Min.</th>
                    <th style="text-align: center;">Mid.</th>
                    <th style="text-align: center;">Max</th>
                    <th class="text-red">Shot Weight (gm):</th>
                    <td>
                        <div id="lblShotWeightStd" runat="server" class="labelNumberBox-10"></div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtShotWeightCert" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <th>Certified Cycle Time</th>
                    <td>
                        <div id="lblCycleTimeMin" runat="server" class="labelNumberBox-10"></div>
                    </td>
                    <td>
                        <div id="lblCycleTimeMid" runat="server" class="labelNumberBox-10"></div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCycleTimeMax" runat="server" cssClass="numberBox-10" onchange="CalcCycleTimes(this);" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox></td>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <th>Final Part(s) Weight (g)</th>
                    <td style="text-align: center;">
                        <div id="lblFinalPartsWeightMin" runat="server" class="labelNumberBox-10"></div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFinalPartsWeightMid" runat="server" cssClass="numberBox-10" onchange="CalcPartsWeight(this);" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                    </td>
                    <td>
                        <div id="lblFinalPartsWeightMax" runat="server" class="labelNumberBox-10"></div>
                    </td>
                    <td colspan="5"></td>
                </tr>
            </table>

            <div class="childGroup">
                <div class="float-left">
                    <%--Barrel Tempature--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Barrel Tempature</th>
                            <th style="text-align: center;">Set</th>
                        </tr>
                        <tr>
                            <th>Nozzle Tip %:</th>
                            <td>
                                <asp:TextBox ID="txtNozzleTipPercent" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Nozzle Body %:</th>
                            <td>
                                <asp:TextBox ID="txtNozzleBodyPercent" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Adapter (NH):</th>
                            <td>
                                <asp:TextBox ID="txtAdapter" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Barrel Front (H4):</th>
                            <td>
                                <asp:TextBox ID="txtBarrelFront" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <th>Barrel Center (H3):</th>
                            <td>
                                <asp:TextBox ID="txtBarrelCenter1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Barrel Center (H2):</th>
                            <td>
                                <asp:TextBox ID="txtBarrelCenter2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Barrel Rear (H1):</th>
                            <td>
                                <asp:TextBox ID="txtBarrelRear" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="float-left">
                    <%--Mold Cooling Temps--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Mold Cooling Temps</th>
                            <th style="text-align: center;">Set</th>
                        </tr>
                        <tr>
                            <th>Mold Gate Temp &#176;F:</th>
                            <td>
                                <asp:TextBox ID="txtMoldGateTempF" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Mold Fixed 1/2 Temp &#176;F:</th>
                            <td>
                                <asp:TextBox ID="txtMoldFixedHalfTempF" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Mold Moving 1/2 &#176;F:</th>
                            <td>
                                <asp:TextBox ID="txtMoldMovingHalfTempF" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Stripper or Other &#176;F:</th>
                            <td>
                                <asp:TextBox ID="txtStripperOrOtherTempF" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <%--Nozzle Tip Information--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Nozzle Tip Information</th>
                            <th style="text-align: center;">Set</th>
                        </tr>
                        <tr>
                            <th>Type GP, FT, NYL (styles):</th>
                            <td>
                                <asp:TextBox ID="txtNozzleTipType" runat="server" Style="text-transform: uppercase;" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Length OAL (in.):</th>
                            <td>
                                <asp:TextBox ID="txtNozzleTipLengthOALInches" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Orifice Size (in.):</th>
                            <td>
                                <asp:TextBox ID="txtNozzleTipOrificeSizeInches" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table-header-row">
                            <th>Nozzle Body Information</th>
                            <th style="text-align: center;">Set</th>
                        </tr>
                        <tr>
                            <th>Length OAL (in.):</th>
                            <td>
                                <asp:TextBox ID="txtNozzleBodyLengthOALInches" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="float-left">
                    <%--Reference Data--%>
                    <table class="table">
                        <tr>
                            <th class="table-header-row">Reference Data</th>
                            <th class="table-header-row"></th>
                            <td colspan="4" style="width: 200px;"></td>
                            <th class="text-red">Machine Number:</th>
                            <td>
                                <div id="lblMachineNumber" runat="server" class="labelBox-10"></div>
                            </td>
                        </tr>
                        <tr>
                            <th>Fill Only Time:</th>
                            <td>
                                <asp:TextBox ID="txtFillOnlyTime" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td colspan="4"></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>Fill Only Weight:</th>
                            <td>
                                <asp:TextBox ID="txtFillOnlyWeight" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td colspan="4"></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>Steel Temp "A" Side &#176;F:</th>
                            <td>
                                <asp:TextBox ID="txtSteelTempSide_A" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td colspan="4"></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>Steel Temp "B" Side &#176;F:</th>
                            <td>
                                <asp:TextBox ID="txtSteelTempSide_B" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td colspan="4"></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>Melt Temp &#176;F:</th>
                            <td>
                                <asp:TextBox ID="txtMeltTemp" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td colspan="4"></td>
                            <td colspan="2"></td>
                        </tr>
                    </table>

                    <%--Valve Gate--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Valve Gate</th>
                            <th style="text-align: center;">VG1</th>
                            <th style="text-align: center;">VG2</th>
                            <th style="text-align: center;">VG3</th>
                            <th style="text-align: center;">VG4</th>
                        </tr>
                        <tr>
                            <th>Open Delay:</th>
                            <td>
                                <asp:TextBox ID="txtOpenDelay_VG1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpenDelay_VG2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpenDelay_VG3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpenDelay_VG4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Open Time:</th>
                            <td>
                                <asp:TextBox ID="txtOpenTime_VG1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpenTime_VG2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpenTime_VG3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpenTime_VG4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Adv Cls Tm:</th>
                            <td>
                                <asp:TextBox ID="txtAdvCloseTime_VG1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdvCloseTime_VG2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdvCloseTime_VG3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdvCloseTime_VG4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Close Delay:</th>
                            <td>
                                <asp:TextBox ID="txtCloseDelay_VG1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCloseDelay_VG2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCloseDelay_VG3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCloseDelay_VG4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Close Time:</th>
                            <td>
                                <asp:TextBox ID="txtCloseTime_VG1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCloseTime_VG2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCloseTime_VG3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCloseTime_VG4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Inj HP End:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionHPEndTime_VG1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInjectionHPEndTime_VG2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInjectionHPEndTime_VG3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInjectionHPEndTime_VG4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Transfer:</th>
                            <td>
                                <asp:TextBox ID="txtTransfer_VG1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTransfer_VG2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTransfer_VG3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTransfer_VG4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="float-clear"></div>
            </div>
            <br />
            <div class="childGroup">
                <div class="float-left">
                    <%--Injection Profile--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Injection Profile</th>
                            <th style="text-align: center;">Set</th>
                        </tr>
                        <tr>
                            <th>Shot Size:</th>
                            <td>
                                <asp:TextBox ID="txtShotSize" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Press 1:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionPress1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Press 2:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionPress2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Press 3:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionPress3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Press 4:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionPress4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Press 5:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionPress5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Velocity 1:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionVelocity1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Velocity 2:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionVelocity2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Velocity 3:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionVelocity3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Velocity 4:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionVelocity4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Velocity 5:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionVelocity5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Change Pos 1:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionChangePos1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Change Pos 2:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionChangePos2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Change Pos 3:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionChangePos3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Change Pos 4:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionChangePos4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Trans Mode IVSH/IPS:</th>
                            <td>
                                <asp:TextBox ID="txtTransMode" runat="server" cssClass="textBox-10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Trans Pos In.:</th>
                            <td>
                                <asp:TextBox ID="txtTransPosition" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Ips PSI:</th>
                            <td>
                                <asp:TextBox ID="txtIPS_PSI" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Inj PSI at Transfer:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionPSIAtTransfer" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Injection Time Act.:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionTimeAct" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Hold Press 1 %:</th>
                            <td>
                                <asp:TextBox ID="txtHoldPress1_Percent" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Hold Press 2 %:</th>
                            <td>
                                <asp:TextBox ID="txtHoldPress2_Percent" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Hold Time 1:</th>
                            <td>
                                <asp:TextBox ID="txtHoldTime1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Hold Time 2:</th>
                            <td>
                                <asp:TextBox ID="txtHoldTime2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Injection Hold Time:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionHoldTime" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="float-left">
                    <%--Recovery and Clamp Profile--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Recovery and Clamp Profile</th>
                            <th style="text-align: center;">Set</th>
                        </tr>
                        <tr>
                            <th>Pre-Pullback (P.B.2) Speed %:</th>
                            <td>
                                <asp:TextBox ID="txtPrePullback" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Pre-Pullback Before Stroke In:</th>
                            <td>
                                <asp:TextBox ID="txtPrePullbackStroke" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Screw Start Delay:</th>
                            <td>
                                <asp:TextBox ID="txtScrewStartDelay" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Screw Chg Position 1:</th>
                            <td>
                                <asp:TextBox ID="txtChargePosition1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Screw Chg Position 2:</th>
                            <td>
                                <asp:TextBox ID="txtChargePosition2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Screw Recovery % 1:</th>
                            <td>
                                <asp:TextBox ID="txtScrewRecovery1Percent" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Screw Recovery % 2:</th>
                            <td>
                                <asp:TextBox ID="txtScrewRecovery2Percent" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Back Pressure 1:</th>
                            <td>
                                <asp:TextBox ID="txtBackPressure1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Back Pressure 2:</th>
                            <td>
                                <asp:TextBox ID="txtBackPressure2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Post Pullback (P.B.2) Speed %:</th>
                            <td>
                                <asp:TextBox ID="txtPostPullbackSpeedPercent" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Post-Pullback Stroke In:</th>
                            <td>
                                <asp:TextBox ID="txtPostPullbackStroke" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Screw position after SB:</th>
                            <td>
                                <asp:TextBox ID="txtScrewPositonAfter" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Screw recover time:</th>
                            <td>
                                <asp:TextBox ID="txtScrewRecoveryTime" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Cooling time secs:</th>
                            <td>
                                <asp:TextBox ID="txtCoolingTimeSeconds" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Mold Protect Pressure:</th>
                            <td>
                                <asp:TextBox ID="txtMoldProtectPressure" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Mold Protect Time:</th>
                            <td>
                                <asp:TextBox ID="txtMoldProtectTime" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Clamp tonage:</th>
                            <td>
                                <asp:TextBox ID="txtClampTonnage" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Injection Pressure Gage PSI:</th>
                            <td>
                                <asp:TextBox ID="txtInjectionPressureGaugePSI" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Holding 1 Gage PSI:</th>
                            <td>
                                <asp:TextBox ID="txtHoldingGauge1_PSI" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Holding 2 Gage PSI:</th>
                            <td>
                                <asp:TextBox ID="txtHoldingGauge2_PSI" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Back Pressure Gage PSI 1:</th>
                            <td>
                                <asp:TextBox ID="txtBackPressureGauge1_PSI" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Back Pressure Gage PSI 2:</th>
                            <td>
                                <asp:TextBox ID="txtBackPressureGauge2_PSI" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="text-blue">Final Cushion:</th>
                            <td>
                                <asp:TextBox ID="txtFinalCushion" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="float-left">
                    <%--Hot Tip Controller Assist--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Hot Tip Controller Assist</th>
                            <th style="text-align: center;">Box 1</th>
                            <th style="text-align: center;">Box 2</th>
                            <th style="text-align: center;">Box 3</th>
                            <th style="text-align: center;">Box 4</th>
                            <th style="text-align: center;">Box 5</th>
                            <th style="text-align: center;">Box 6</th>
                        </tr>
                        <tr>
                            <th>Pos. 1 Gate/HB/Man.:</th>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos1_Box1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos1_Box2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos1_Box3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos1_Box4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos1_Box5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos1_Box6" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Pos. 2 Gate/HB/Man.:</th>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos2_Box1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos2_Box2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos2_Box3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos2_Box4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos2_Box5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos2_Box6" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Pos. 3 Gate/HB/Man.:</th>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos3_Box1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos3_Box2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos3_Box3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos3_Box4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos3_Box5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos3_Box6" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Pos. 4 Gate/HB/Man.:</th>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos4_Box1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos4_Box2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos4_Box3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos4_Box4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos4_Box5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos4_Box6" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Pos. 5 Gate/HB/Man.:</th>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos5_Box1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos5_Box2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos5_Box3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos5_Box4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos5_Box5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos5_Box6" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Pos. 6 Gate/HB/Man.:</th>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos6_Box1" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos6_Box2" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos6_Box3" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos6_Box4" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos6_Box5" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGate_HB_Man_Pos6_Box6" runat="server" cssClass="numberBox-10" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <%--Notes--%>
                    <table class="table">
                        <tr class="table-header-row">
                            <th>Start up Instructions / Comments</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" cssClass="textBox-comments"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="float-clear"></div>
            </div>

            <div>
                <table class="table" style="width: 100%; padding-top: 10px;">
                    <tr>
                        <td class="error">
                            <asp:Label ID="lblErrorBottom" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; padding-right: 10px;">
                            <asp:Button ID="btnSaveBottom" runat="server" Text="Save" class="btn-save btn-lg" OnClick="btnSave_Click" />
                        </td>
                    </tr>

                </table>
            </div>
        </asp:Panel>
        <div class="footer"></div>
    </form>
</body>
</html>
