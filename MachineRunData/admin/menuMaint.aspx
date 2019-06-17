<%@ Page Title="Menu Maintenance" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="menuMaint.aspx.cs" Inherits="cambro._menuMaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="controls/roleSelection.ascx" TagName="roleSelection" TagPrefix="uc1" %>
<%@ Register Src="controls/pageSelection.ascx" TagName="pageSelection" TagPrefix="uc2" %>

<asp:Content ID="phHead" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>
<asp:Content ID="phContent" runat="server" ContentPlaceHolderID="phContent">
    <asp:HiddenField ID="hMenuId" runat="server" />
    <div class="pageInner">
        <h4>Menu Maintenance</h4>
        <div>
            <div style="padding: 5px 0 5px 10px; height: 30px;">
                <asp:Label ID="lblError" runat="server" class="error"></asp:Label>
            </div>
            <div id="dvGrid" runat="server" class="table-responsive" style="display: none;">
                <asp:Repeater ID="rpData" runat="server" OnItemCommand="rpData_ItemCommand" OnItemDataBound="rpData_ItemDataBound">
                    <HeaderTemplate>
                         <table class="table table-striped table-bordered table-hover table-condensed">
                            <tr>
                                <th>Order
                                </th>
                                <th>Title
                                </th>
                                <th>Target
                                </th>
                                <th>Is Public
                                </th>
                                <th>Page(s)
                                </th>
                                <th>Role(s)
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Label ID="lblDisplayOrder" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "displayOrder")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkTitle" CssClass="smallLinkButton" runat="server" CommandName="edit"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "menuId")%>'
                                    Text='<%#DataBinder.Eval(Container.DataItem, "title")%>'></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label ID="lblTarget" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "target")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIsPublic" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPageCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "_pageCount")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRoles" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "_roleNames")%>'></asp:Label>
                                <asp:HiddenField ID="hIsPublic" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "isPublic")%>' />
                                <asp:HiddenField ID="hIsDisabled" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "isDisabled")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td id="tdFooter" runat="server" colspan="6">
                                <asp:LinkButton ID="lnkAddNew" CssClass="smallLinkButton" runat="server" CommandName="add_new"
                                    CommandArgument='-1' Text="add new menu"></asp:LinkButton>
                            </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="lnkMenuOpen" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender BehaviorID="mpMenu" ID="mpMenu" runat="server" Drag="true" PopupDragHandleControlID="pnlMenuDrag"
        TargetControlID="lnkMenuOpen" CancelControlID="lnkMenuClose" PopupControlID="pnlMenu"
        BackgroundCssClass="modal">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlMenu" CssClass="modalPopUp" runat="server" DefaultButton="btnSave" Style="width: 675px; display: none;">
        <asp:Panel ID="pnlMenuDrag" runat="server">
            <div class="modalHeader" style="width: 100%;">
                <div style="float: left; padding: 2px 5px 2px;">
                    <asp:Label ID="lblModalHeader" runat="server" Style="color: #fff;"></asp:Label>
                </div>
                <div style="float: right; padding: 2px 5px 2px;">
                    <asp:LinkButton ID="lnkMenuClose" runat="server" Style="color: #fff;">[close]</asp:LinkButton>
                </div>
                <div style="clear: both;">
                </div>
            </div>
            <div style="padding: 10px 10px 10px 10px;">
                <table style="width: 100%;">
                    <tr>
                        <th style="text-align: right;"></th>
                        <td>
                            <asp:CheckBox ID="chkIsDisabled" runat="server" class="checkbox" Text="Disabled" />
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align: right;"></th>
                        <td>
                            <asp:CheckBox ID="chkIsPublic" runat="server" class="checkbox" Text="Is Public" />
                        </td>
                    </tr>
                    <tr>
                        <th class="mandatory" style="text-align: right;">Display Order:
                        </th>
                        <td>
                            <asp:TextBox ID="txtDisplayOrder" runat="server" Style="width: 50px;" onKeyPress="return CheckNumericInput(event.keyCode, event.which, false, false);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="mandatory" style="text-align: right;">Title:
                        </th>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" Style="width: 350px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align: right;">Target:
                        </th>
                        <td>
                            <asp:TextBox ID="txtTarget" runat="server" Style="width: 350px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <th colspan="2">Security Roles:
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <uc1:roleSelection ID="ucRoleSelection" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <th colspan="2">Pages:
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <uc2:pageSelection ID="ucPageSelection" runat="server" />
                        </td>
                    </tr>
                </table>
                <div style="padding: 5px 5px 0 5px; height: 30px;">
                    <asp:Label ID="lblMenuError" runat="server" class="error"></asp:Label>
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
