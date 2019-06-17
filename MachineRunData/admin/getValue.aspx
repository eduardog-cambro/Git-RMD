<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getValue.aspx.cs" Inherits="cambro._getValues" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="float:left; padding-right:10px;">
                <asp:TextBox ID="TextBox1" runat="server" style="width:300px;"></asp:TextBox>
            </div>
            <div style="float:left;">
                <asp:TextBox ID="TextBox2" runat="server" style="width:600px;"></asp:TextBox>
            </div>
            <div style="clear:both;"></div>
        </div>
        <div style="padding-top:5px;">
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
