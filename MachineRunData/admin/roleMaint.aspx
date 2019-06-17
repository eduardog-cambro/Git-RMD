<%@ Page Title="Role Maintenance" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="roleMaint.aspx.cs" Inherits="cambro._roleMaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="phHead" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>
<asp:Content ID="phContent" runat="server" ContentPlaceHolderID="phContent">
    <asp:HiddenField ID="hRoleId" runat="server" />
    <div class="pageInner">
        <h4>Role Maintenance</h4>
        <div>
            <div style="padding: 5px 0 5px 10px; height: 30px;">
                <asp:Label ID="lblError" runat="server" class="error"></asp:Label>
            </div>
            <div id="dvGrid" runat="server" class="table-responsive" style="display: none;">
                <asp:Repeater ID="rpData" runat="server" OnItemCommand="rpData_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover table-condensed">
                            <tr>
                                <th>Role Name
                                </th>
                                <th>users
                                </th>
                                <th>pages
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkRoleName" CssClass="smallLinkButton" runat="server" CommandName="edit"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "roleId")%>'
                                    Text='<%#DataBinder.Eval(Container.DataItem, "roleName")%>'></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkUsers" CssClass="smallLinkButton" runat="server" CommandName="users"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "roleId")%>'
                                    Text='users'></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkPages" CssClass="smallLinkButton" runat="server" CommandName="pages"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "roleId")%>'
                                    Text='pages'></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td id="tdFooter" colspan="3" runat="server">
                                <asp:LinkButton ID="lnkAddNew" CssClass="smallLinkButton" runat="server" CommandName="add_new"
                                    CommandArgument='-1' Text="add new role"></asp:LinkButton>
                            </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="lnkRoleOpen" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender BehaviorID="mpRole" ID="mpRole" runat="server" Drag="true" PopupDragHandleControlID="pnlRoleDrag"
        TargetControlID="lnkRoleOpen" CancelControlID="lnkRoleClose" PopupControlID="pnlRole"
        BackgroundCssClass="modal">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlRole" CssClass="modalPopUp" runat="server" DefaultButton="btnSave" Style="width: 575px; display: none;">
        <asp:Panel ID="pnlRoleDrag" runat="server">
            <div class="modalHeader" style="width: 575px;">
                <div style="float: left; padding: 2px 5px 2px;">
                    <asp:Label ID="lblModalHeader" runat="server" Style="color: #fff;"></asp:Label>
                </div>
                <div style="float: right; padding: 2px 5px 2px;">
                    <asp:LinkButton ID="lnkRoleClose" runat="server" Style="color: #fff;">[close]</asp:LinkButton>
                </div>
                <div style="clear: both;">
                </div>
            </div>
            <div style="padding: 10px 10px 10px 10px;">
                <table style="width: 100%;">
                    <tr>
                        <th class="mandatory" style="text-align: right;">Role Name:
                        </th>
                        <td>
                            <asp:TextBox ID="txtRoleName" runat="server" Style="width: 350px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="padding: 5px 0 5px 10px; height: 30px;">
                    <asp:Label ID="lblRoleError" runat="server" class="error"></asp:Label>
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

    <asp:LinkButton ID="lnkUserOpen" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender BehaviorID="mpUser" ID="mpUser" runat="server" Drag="true" PopupDragHandleControlID="pnlUserDrag"
        TargetControlID="lnkUserOpen" CancelControlID="lnkUserClose" PopupControlID="pnlUser"
        BackgroundCssClass="modal">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlUser" CssClass="modalPopUp" runat="server" DefaultButton="btnUserSave" Style="width: 800px; display: none;">
        <asp:Panel ID="pnlUserDrag" runat="server">
            <div class="modalHeader" style="width: 100%;">
                <div style="float: left; padding: 2px 5px 2px;">
                    <asp:Label ID="lblUserHeader" runat="server" Style="color: #fff;"></asp:Label>
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
                        <td>
                            <asp:CheckBoxList ID="cblRoleUser" runat="server" RepeatColumns="4" Layout="Table" class="checkBoxList">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <div style="padding: 5px 0 5px 10px; height: 30px;">
                    <asp:Label ID="lblUserError" runat="server" class="error"></asp:Label>
                </div>
                <div style="width: 100%;">
                    <div style="float: right; padding-right: 10px;">
                        <asp:Button ID="btnUserSave" runat="server" class="wbtn btn btn-primary" Text="Save" OnClick="btnUserSave_Click" />
                    </div>
                    <div style="clear:both;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>

    <asp:LinkButton ID="lnkPageOpen" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender BehaviorID="mpPage" ID="mpPage" runat="server" Drag="true" PopupDragHandleControlID="pnlPageDrag"
        TargetControlID="lnkPageOpen" CancelControlID="lnkPageClose" PopupControlID="pnlPage"
        BackgroundCssClass="modal">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPage" CssClass="modalPopUp" runat="server" DefaultButton="btnPageSave" Style="width: 700px; display: none;">
        <asp:Panel ID="pnlPageDrag" runat="server">
            <div class="modalHeader" style="width: 100%;">
                <div style="float: left; padding: 2px 5px 2px;">
                    <asp:Label ID="lblPageHeader" runat="server" Style="color: #fff;"></asp:Label>
                </div>
                <div style="float: right; padding: 2px 5px 2px;">
                    <asp:LinkButton ID="lnkPageClose" runat="server" Style="color: #fff;">[close]</asp:LinkButton>
                </div>
                <div style="clear: both;">
                </div>
            </div>
            <div style="padding: 10px 10px 10px 10px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="cblRolePage" runat="server" RepeatColumns="4" RepeatLayout="Table" class="checkBoxList">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                     <div style="padding: 5px 0 5px 10px; height: 30px;">
                    <asp:Label ID="lblPageError" runat="server" class="error"></asp:Label>
                </div>
                <div style="width: 100%;">
                    <div style="float: right; padding-right: 10px;">
                        <asp:Button ID="btnPageSave" runat="server" class="wbtn btn btn-primary" Text="Save" OnClick="btnPageSave_Click" />
                    </div>
                    <div style="clear:both;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
