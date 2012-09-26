<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ConsumerApp._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>Testing OAuth Consumer</h1>
	<fieldset title="Authorization">
        <asp:TextBox ID="authHandleTextBox" runat="server" Text="http://localhost:49830/OAuth.ashx" />
        <asp:TextBox ID="consumerKeyTextBox" runat="server" Text="key1" />
        <asp:TextBox ID="consumerSecretTextBox" runat="server" Text="secret1" />
		<asp:Button ID="getAuthorizationButton" runat="server" Text="Get Authorization" OnClick="getAuthorizationButton_Click" />
		<asp:Label ID="authorizationLabel" runat="server" />
	</fieldset>
	<br />
    <asp:TextBox ID="serviceTextBox" runat="server" Text="http://localhost:49830/OpenServices/DataService.svc/GetAccounts" />
	<asp:Button ID="getDataButton" runat="server" Text="Get Data" OnClick="getData_Click" />
	<br />
    <asp:ListBox ID="accountListBox" runat="server" />
	<br />
    <asp:Label ID="dataLabel" runat="server" />
</asp:Content>