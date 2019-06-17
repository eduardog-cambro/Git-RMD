<%@ Page Title="Machine Run Data" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="machineRunData.aspx.cs" Inherits="cambro._machineRunData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="phHead" runat="server">

    <script src="../scripts/jquery/jquery.blockUI.js"></script>

    <link href="../tableSorter/theme.bootstrap_4.css" rel="stylesheet" />
    <script src="../tableSorter/jquery.tablesorter.combined.js"></script>

    <style>
        .tablesorter thead .disabled {
            display: none;
        }

        .ui-dialog {
            position: absolute;
            box-shadow: 1px 1px 20px 2px;
            padding: 5px 5px 5px 5px;
            border-radius: 10px;
            outline: none;
            overflow: hidden;
        }
    </style>

    <script type="text/javascript"> 
        $(document).ready(function () {
            $("#gridTable").tablesorter({
                headers: { 0: { sorter: false, parser: false, filter: false } },
                widgets: ["zebra", 'stickyHeaders', "filter"],
                widgetOptions: {
                    filter_searchDelay: 300,
                    filter_startsWith: false,
                    filter_ignoreCase: true,
                    filter_cssFilter: "form-control",

                    stickyHeaders: '',
                    stickyHeaders_offset: 0,
                    stickyHeaders_addResizeEvent: true,
                    stickyHeaders_includeCaption: true,
                    stickyHeaders_zIndex: 2,
                    stickyHeaders_attachTo: null,
                    stickyHeaders_xScroll: null,
                    stickyHeaders_yScroll: null,
                    stickyHeaders_filteredToTop: true
                }
            });

            $("#updateStatus-dialog").dialog({
                autoOpen: false,
                modal: true,
                width: 350,
                resizable: false,
                height: "auto",
                buttons: [
                    {
                        text: "Save",
                        class: "btn btn-primary",
                        click: function () {
                            var results = updateStatus();
                            if (results != "") {
                                $("#lblStatusUpdateError").html(results);
                            }
                            else {
                                $(this).dialog('close');
                            }
                        },
                    },
                    {
                        id: "update-cancel-button",
                        text: "Cancel",
                        class: "btn btn-secondary",
                        click: function () {
                            $(this).dialog('close');
                        }
                    }
                ],
                open: function () {
                    $("#update-cancel-button").focus();
                },
                close: function () {
                    $(this).dialog('close');
                }
            });

            $("#delete-dialog").dialog({
                autoOpen: false,
                modal: true,
                width: 350,
                resizable: false,
                height: "auto",
                buttons: [
                    {
                        text: "Delete",
                        class: "btn btn-primary",
                        click: function () {
                            var results = deleteRunSheet();
                            if (results != "") {
                                $("#lblDeleteError").html(results);
                            }
                            else {
                                $(this).dialog('close');
                            }
                        },
                    },
                    {
                        id: "delete-cancel-button",
                        text: "Cancel",
                        class: "btn btn-secondary",
                        click: function () {
                            $(this).dialog('close');
                        }
                    }
                ],
                open: function () {
                    $("#delete-cancel-button").focus();
                },
                close: function () {
                    $(this).dialog('close');
                }
            });
        });

        function unblockUI() {
            $.unblockUI();
        }
        function blockUI() {
            $.blockUI({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
            return true;
        }

        function reloadGrid() {
            var _id = $$("btnSearch");
            __doPostBack(_id.attr("name"), 'OnClick');
            return true;
        }

        function print(type, id) {

            showMessage("ERROR", "");
            blockUI();

            $.ajax({
                type: "POST",
                async: false,
                url: "webservices.asmx/renderReport",
                data: JSON.stringify({ machineType: type, machineDataHeaderId: id }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d.startsWith("renderReport() failed!")) {
                        showMessage("ERROR", data.d);
                    }
                    else {
                        window.open(data.d, '_blank');
                    }
                },
				error: function (jqXHR, textStatus, errorThrown) {
                    showMessage("ERROR", errorThrown);
                }
            });

            unblockUI();
        }

        function showStatusDialog(id, status) {

            $('#ddlNewStatus').html('');
            $("#lblStatusUpdateError").html('');
            $$('hMachineDataHeaderId').val(id);

            $.ajax({
                type: "POST",
                async: false,
                url: "webservices.asmx/getStatuses",
                data: JSON.stringify({ currentStatus: status }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var options = '';
                    $.each(data.d, function (key, value) {
                        $('#ddlNewStatus').append('<option value="' + key + '">' + value + '</option>');
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#lblStatusUpdateError").html(errorThrown);
                }
            });

            $('#updateStatus-dialog').dialog('open');
        }
        function updateStatus() {

            var results = "";
            var id = $$('hMachineDataHeaderId').val();
            var newStatus = $('#ddlNewStatus').find(":selected").text();

            $.ajax({
                type: "POST",
                async: false,
                url: "webservices.asmx/updateStatus",
                data: JSON.stringify({ machineDataHeaderId: id, newStatus: newStatus }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    results = data.d;
                    if (results == "") {
                        showMessage("SUCCESS", "Status updated successfully!");
                        reloadGrid();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    showMessage("ERROR", "Status updated failed! Error is: " + errorThrown);
                }
            });
            return results;
        }

        function showDeleteDialog(id) {

            $("#lblDeleteError").html('');
            $$('hMachineDataHeaderId').val(id);

            $('#delete-dialog').dialog('open');
        }
        function deleteRunSheet() {

            var results = "";
            var id = $$('hMachineDataHeaderId').val();

            $.ajax({
                type: "POST",
                async: false,
                url: "webservices.asmx/deleteRunSheet",
                data: JSON.stringify({ machineDataHeaderId: id }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    results = data.d;
                    if (results == "") {
                        showMessage("SUCCESS", "Run sheet deleted successfully!");
                        reloadGrid();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    showMessage("ERROR", "Delete failed! Error is: " + errorThrown);
                }
            });
            return results;
        }

        function showMessage(type, msg) {
            $$('hPostLoadMessage').val(type + msg);
        }
    </script>
</asp:Content>
<asp:Content ID="phContent" runat="server" ContentPlaceHolderID="phContent">
    <asp:HiddenField ID="hUserId" runat="server" />
    <asp:HiddenField ID="hPostLoadMessage" runat="server" />
    <asp:HiddenField ID="hMachineDataHeaderId" runat="server" />
    <asp:HiddenField ID="hPlantNumber" runat="server" />
    <div class="pageInner">
        <h4>Machine Run Data</h4>
        <asp:Panel ID="pnlGrid" runat="server">
            <table id="selection">
                <tr>
                    <th id="thPlantNumber" runat="server">Plant No.</th>
                    <th>Part No.</th>
                    <th>Tool No.</th>
                    <th>Machine Type</th>
                    <th></th>
                    <th id="thPrintBlank" runat="server">Print Blank Form</th>
                    <th id="thAddNew" runat="server">Add New Validation Form</th>
                </tr>
                <tr>
                    <td id="tdPlantNumber" runat="server">
                        <asp:DropDownList ID="ddlPlantNumber" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPlantNumber_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchPartNumber" runat="server" class="form-control" Style="width: 200px;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchToolNumber" runat="server" class="form-control" Style="width: 200px;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSearchMachineType" runat="server" class="form-control"></asp:DropDownList>
                    </td>

                    <td style="padding-left: 10px; padding-right: 20px;">
                        <asp:Button ID="btnSearch" class="wbtn btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>

                    <td id="tdPrintBlank" runat="server">
                        <asp:DropDownList ID="ddlPrintBlank" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPrintBlank_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td id="tdAddNew" runat="server">
                        <asp:DropDownList ID="ddlAddNew" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAddNew_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div style="padding: 5px 0 5px 10px; height: 30px;">
                <asp:Label ID="lblError" runat="server" class="error"></asp:Label>
            </div>
            <div id="dvGrid" runat="server" class="table-responsive">
                <asp:Repeater ID="rpRunData" runat="server" OnItemDataBound="rpRunData_ItemDataBound">
                    <HeaderTemplate>
                        <table id="gridTable" class="tablesorter-bootstrap table table-bordered table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th style="text-align: center;">Options</th>
                                    <th>Plant No.</th>
                                    <th>Part No.</th>
                                    <th>Machine No.</th>
                                    <th>No. Cavities</th>
                                    <th>Tool No.</th>
                                    <th>Machine Type</th>
                                    <th>Status</th>
                                    <th>Validated Cycle Time (sec)</th>
                                    <th>Validated Shot Weight (gm)</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="padding-right: 0; padding-left: 2px;">
                                <div style="text-align: center; display: inline-flex; margin: 0 auto;">
                                    <div id="dvEdit" runat="server" style="float: left; padding-right: 2px; padding-left: 2px;">
                                        <img src="../images/edit.png" alt="Edit" title="Edit this entry" onclick="javascript:showForm('','<%#DataBinder.Eval(Container.DataItem, "MachineType")%>','<%#DataBinder.Eval(Container.DataItem, "MachineDataHeaderId")%>','1');" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" />
                                    </div>
                                    <div id="dvCopy" runat="server" style="float: left; padding-right: 2px; padding-left: 2px;">
                                        <img src="../images/copy.png" alt="Copy" title="Copy this entry" onclick="javascript:showForm('','<%#DataBinder.Eval(Container.DataItem, "MachineType")%>','<%#DataBinder.Eval(Container.DataItem, "MachineDataHeaderId")%>','2');" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" />
                                    </div>
                                    <div id="dvView" runat="server" style="float: left; padding-right: 2px; padding-left: 2px;">
                                        <img src="../images/view.png" alt="Edit" title="View this entry" onclick="javascript:showForm('','<%#DataBinder.Eval(Container.DataItem, "MachineType")%>','<%#DataBinder.Eval(Container.DataItem, "MachineDataHeaderId")%>','3');" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" />
                                    </div>
                                    <div id="dvDelete" runat="server" style="float: left; padding-right: 2px; padding-left: 2px;">
                                        <img src="../images/delete.png" alt="Edit" title="Delete this entry" onclick="javascript:showDeleteDialog('<%#DataBinder.Eval(Container.DataItem, "MachineDataHeaderId")%>')" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" />
                                    </div>
                                    <div id="dvPrint" runat="server" style="float: left; padding-right: 2px; padding-left: 2px;">
                                        <img src="../images/print.png" alt="Edit" title="Print this entry" onclick="javascript:print('<%#DataBinder.Eval(Container.DataItem, "MachineType")%>',<%#DataBinder.Eval(Container.DataItem, "MachineDataHeaderId")%>);" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" />
                                    </div>
                                    <div id="dvStatusUpdate" runat="server" style="float: left; padding-right: 2px; padding-left: 2px;">
                                        <img src="../images/statusUpdate.png" alt="Edit" title="Update status" onclick="javascript:showStatusDialog('<%#DataBinder.Eval(Container.DataItem, "MachineDataHeaderId")%>','<%#DataBinder.Eval(Container.DataItem, "Status")%>');" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" />
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>
                            </td>
                            <td>
                                <asp:Label ID="lblPlantId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlantNumber")%>' /></td>
                            <td>
                                <asp:Label ID="lblProductId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PartNumber")%>' /></td>
                            <td>
                                <asp:Label ID="lblMachineNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MachineNumber")%>' /></td>
                            <td>
                                <asp:Label ID="lblStdCavities" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ToolCavities")%>' /></td>
                            <td>
                                <asp:Label ID="lblToolNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ToolNumber")%>' /></td>
                            <td>
                                <asp:Label ID="lblMachineType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MachineType")%>' /></td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "_Status")%>' /></td>
                            <td class="text-right">
                                <asp:Label ID="lblCycleTimeCert" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CycleTimeCert")%>' /></td>
                            <td class="text-right">
                                <asp:Label ID="lblShotWeightCert" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ShotWeightCert")%>' />
                                <asp:HiddenField ID="hAllowView" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_allowView")%>' />
                                <asp:HiddenField ID="hAllowEdit" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_allowEdit")%>' />
                                <asp:HiddenField ID="hAllowCopy" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_allowCopy")%>' />
                                <asp:HiddenField ID="hAllowDelete" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_allowDelete")%>' />
                                <asp:HiddenField ID="hAllowPrint" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_allowPrint")%>' />
                                <asp:HiddenField ID="hAllowStatusUpdate" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_allowStatusUpdate")%>' />
                            </td>
                        </tr>

                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>

    <div id="updateStatus-dialog" title="Update Status" style="display: none;">
        <div>
            <div class="float-left">New Status:</div>
            <div class="float-left ml-1">
                <select id="ddlNewStatus" class="form-control">
                    <option value="-1" selected="selected">- Select One -</option>
                </select>
            </div>
            <div class="clearfix"></div>
        </div>

        <div style="padding: 5px 0 5px 10px; height: 40px;">
            <label id="lblStatusUpdateError" class="error-small"></label>
        </div>
    </div>

    <div id="delete-dialog" title="Delete Entry" style="display: none;">
        <div>
            Are you sure you want to delete this run sheet?
        </div>

        <div style="padding: 5px 0 5px 10px; height: 40px;">
            <label id="lblDeleteError" class="error-small"></label>
        </div>
    </div>
</asp:Content>
