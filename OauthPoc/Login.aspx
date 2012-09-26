<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OauthPoc.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txbLogin" runat="server"/>
        <br />
        <asp:TextBox ID="txbPass" runat="server"/>
        <br />
        <asp:Button ID="btnLogin"  runat="server" OnClick="Logon_Click" Text="Login"/>
        <p>
        <asp:Label ID="Msg" ForeColor="red" runat="server" />
        </p>
    </div>
    </form>
</body>
</html>
