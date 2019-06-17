<%@ Page Title="User Maintenance" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="userMaint.aspx.cs" Inherits="cambro._userMaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="controls/roleSelection.ascx" TagName="roleSelection" TagPrefix="uc1" %>
<asp:Content ID="phHead" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>
<asp:Content ID="phContent" runat="server" ContentPlaceHolderID="phContent">
    <asp:HiddenField ID="hUserId" runat="server" />
    <div class="pageInner">
        <h4>User Maintenance</h4>
        <div>
            <table>
                <tr>
                    <td style="text-align: right;">Search:
                    </td>
                    <td style="padding-left: 30px;">
                        <asp:TextBox ID="txtSearch" runat="server" class="form-control" Style="width: 200px;"></asp:TextBox>
                    </td>
                    <td style="padding-left: 30px;">
                        <asp:Button ID="btnSearch" class="wbtn btn btn-primary" runat="server" Text="Load Grid" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <div style="padding: 5px 0 5px 10px; height: 30px;">
                <asp:Label ID="lblError" runat="server" class="error"></asp:Label>
            </div>
            <div id="dvGrid" runat="server" class="table-responsive" style="display: none;">
                <asp:Repeater ID="rpData" runat="server" OnItemCommand="rpData_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover table-condensed">
                            <tr>
                                <th>Windows UserId</th>
                                <th>Friendly Name</th>
                                <th>Plant Number</th>
                                <th style="text-align: Center; width: 90px;">Is Site Admin</th>
                                <th>Role(s)
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: left;">
                                <asp:LinkButton ID="lnkWindowsUserId" CssClass="smallLinkButton" runat="server" CommandName="edit"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "userId")%>'
                                    Text='<%#DataBinder.Eval(Container.DataItem, "windowsUserId")%>'></asp:LinkButton>
                            </td>

                            <td style="text-align: left;">
                                <asp:Label ID="lblFriendlyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "friendlyName")%>'></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblPlantNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plantNumber")%>'></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblIsSiteAdmin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "_isSiteAdmin")%>'></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblRoles" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "_roleNames")%>'></asp:Label>
                                <asp:HiddenField ID="hRoles" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_roles")%>' />
                                <asp:HiddenField ID="hIsDisabled" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "isDisabled")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="5">
                                <asp:LinkButton ID="linAddNew" CssClass="smallLinkButton" runat="server" CommandName="add_new"
                                    CommandArgument='-1' Text="add new user"></asp:LinkButton>
                            </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="lnkUserOpen" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender BehaviorID="mpUser" ID="mpUser" runat="server" Drag="true" PopupDragHandleControlID="pnlUserDrag"
        TargetControlID="lnkUserOpen" CancelControlID="lnkUserClose" PopupControlID="pnlUser"
        BackgroundCssClass="modal">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlUser" CssClass="modalPopUp" runat="server" DefaultButton="btnSave" Style="width: 675px; display: none;">
        <asp:Panel ID="pnlUserDrag" runat="server">
            <div class="modalHeader" style="width: 100%;">
                <div style="float: left; padding: 2px 0 2px 5px; color: #fff;">
                    User Maintenance
                </div>
                <div style="float: right; padding: 2px 5px 2px;">
                    <asp:LinkButton ID="lnkUserClose" runat="server" Style="color: #fff;">[close]</asp:LinkButton>
                </div>
                <div style="clear: both;">
                </div>
            </div>
            <div style="padding: 10px 10px 10px 10px;">
                <table style="width: 100%;">
                    <tr>
                        <th style="text-align: right;"></th>
                        <td>
                            <asp:CheckBox ID="chkIsDisabled" class="checkBox" runat="server" Text="Disabled" />
                        </td>
                    </tr>
                    <tr>
                        <th class="mandatory" style="text-align: right;">Windows UserId:
                        </th>
                        <td>
                            <asp:TextBox ID="txtWindowsUserId" runat="server" Style="width: 150px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="mandatory" style="text-align: right;">Friendly Name:
                        </th>
                        <td>
                            <asp:TextBox ID="txtFriendlyName" runat="server" Style="width: 300px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align: right;">Plant No.:
                        </th>
                        <td>
                            <asp:TextBox ID="txtPlantNumber" runat="server" Style="width: 100px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="2">
                            <hr />
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="mandatory">Security Roles:
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <uc1:roleSelection ID="ucRoleSelection" runat="server" />
                        </td>
                    </tr>
                </table>
                <div style="padding: 5px 0 5px 10px; height: 30px;">
                    <asp:Label ID="lblUserError" runat="server" class="error"></asp:Label>
                </div>
                <div style="width: 100%;">
                    <div style="float: right; padding-right: 10px;">
                        <asp:Button ID="btnSave" runat="server" class="wbtn btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    </div>
                    <div style="float: right; padding-right: 10px;">
                        <asp:Button ID="btnDelete" runat="server" class="wbtn btn btn-danger" Text="Delete" OnClick="btnDelete_Click" OnClientClick="javascript:return verifyDelete();"/>
                    </div>
                    <div style="clear:both;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
