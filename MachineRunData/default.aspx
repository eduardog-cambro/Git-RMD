<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="cambro._default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>
<asp:Content ID="bodyContent" runat="server" ContentPlaceHolderID="phContent">
    <div style="width: 100%;">
        <div style="margin: 0 auto;">
            <asp:Literal ID="ltHTML" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>

