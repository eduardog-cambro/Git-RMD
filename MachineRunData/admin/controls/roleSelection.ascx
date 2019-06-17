<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="roleSelection.ascx.cs" Inherits="cambro.controls.roleSelection" %>
<asp:HiddenField ID="hRecId" runat="server" />
<asp:DataList ID="dlRoles" runat="server" class="checkBoxList" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" OnItemDataBound="dlRoles_ItemDataBound">
    <ItemTemplate>
        <asp:HiddenField ID="hRoleId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "roleId")%>'/>
        <asp:HiddenField ID="hSelected" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_selected")%>'/>
        <asp:CheckBox ID="chkRoleName" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "roleName")%>' />
    </ItemTemplate>
</asp:DataList>