<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pageSelection.ascx.cs" Inherits="cambro.controls.pageSelection" %>
<asp:HiddenField ID="hMenuId" runat="server" />
<asp:DataList ID="dlPages" runat="server" class="checkBoxList" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" OnItemDataBound="dlPages_ItemDataBound">
    <ItemTemplate>
        <asp:HiddenField ID="hPageId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "pageId")%>'/>
        <asp:HiddenField ID="hSelected" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "_selected")%>'/>
        <asp:CheckBox ID="chkPageName" runat="server"   Text='<%#DataBinder.Eval(Container.DataItem, "title")%>' />
    </ItemTemplate>
</asp:DataList>