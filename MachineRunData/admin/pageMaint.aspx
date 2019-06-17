<%@ Page Title="Page Maintenance" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="pageMaint.aspx.cs" Inherits="cambro._pageMaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="controls/roleSelection.ascx" tagname="roleSelection" tagprefix="uc1" %>
<asp:Content ID="phHead" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>
<asp:Content ID="phContent" runat="server" ContentPlaceHolderID="phContent">
    <asp:HiddenField ID="hPageId" runat="server" />
    <div class="pageInner">
        <h4>Page Maintenance</h4>
        <div>
            <div style="padding: 5px 0 5px 10px; height: 30px;">
                <asp:Label ID="lblError" runat="server" class="error"></asp:Label>
            </div>
            <div id="dvGrid" runat="server" class="table-responsive" style="display: none;">
                <asp:Repeater ID="rpData" runat="server" OnItemCommand="rpData_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover table-condensed">
                            <tr>
                                <th>Title
                                </th>
                                <th>Target
                                </th>
                                <th>Role(s)
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: left; padding-right:5px;">
                                <asp:LinkButton ID="lnkTitle" CssClass="smallLinkButton" runat="server" CommandName="edit"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "pageId")%>'
                                    Text='<%#DataBinder.Eval(Container.DataItem, "title")%>'></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label ID="lblTarget" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "target")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRoles" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "_roleNames")%>'></asp:Label>
                                <asp:HiddenField ID="hIsDisabled" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "isDisabled")%>' />
                                <asp:HiddenField ID="hShowLoadProgress" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "showLoadProgress")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td id="tdFooter" runat="server" colspan="3">
                                <asp:LinkButton ID="lnkAddNew" CssClass="smallLinkButton" runat="server" CommandName="add_new"
                                    CommandArgument='-1' Text="add new page"></asp:LinkButton>
                            </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="lnkPageOpen" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender BehaviorID="mpPage" ID="mpPage" runat="server" Drag="true" PopupDragHandleControlID="pnlPageDrag"
        TargetControlID="lnkPageOpen" CancelControlID="lnkPageClose" PopupControlID="pnlPage"
        BackgroundCssClass="modal">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPage" CssClass="modalPopUp" runat="server" DefaultButton="btnSave" Style="width: 675px; display: none;">
        <asp:Panel ID="pnlPageDrag" runat="server">
            <div class="modalHeader" style="width: 100%;">
                <div style="float: left; padding: 2px 5px 2px; color: #fff;">
                    <asp:Label ID="lblModalHeader" runat="server"></asp:Label>       
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
                        <th style="text-align: right;"></th>
                        <td>
                            <asp:CheckBox ID="chkIsDisabled" runat="server" class="checkBox" Text="Disabled" />
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
                        <th class="mandatory" style="text-align: right;">Target:
                        </th>
                        <td>
                            <asp:TextBox ID="txtTarget" runat="server" Style="width: 350px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align: right;"></th>
                        <td>
                            <div>
                                <asp:CheckBox ID="chkShowLoadProgress" runat="server" class="checkBox" Text="Show spinner during page load" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><hr /></td>
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
                    <asp:Label ID="lblPageError" runat="server" class="error"></asp:Label>
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
