<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OauthPoc.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OAuth provider login</title>
</head>
<body>
    <p>This is the OAuth provider login page. The user has been redirected to this page in order to log in and later 
    be able to authorize the consumer application which demands the rights to access the data.</p>
    <p>This is PoC, so the login just takes in these credetinals : </p>
    <ul>
        <li>Login: test</li>
        <li>Password: test</li>
    </ul>
    <form id="form1" runat="server">
    <div>
        <fieldset>
        <legend>Login</legend>
        <p>Login:</p>
        <asp:TextBox ID="txbLogin" runat="server"/>
        <br />
        <p>Password:</p>
        <asp:TextBox ID="txbPass" runat="server"/>
        <br />
        <asp:Button ID="btnLogin"  runat="server" OnClick="Logon_Click" Text="Login"/>
        <p>
        <asp:Label ID="Msg" ForeColor="red" runat="server" />
        </p>
        </fieldset>
    </div>
    </form>
</body>
</html>
