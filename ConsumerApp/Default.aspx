<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ConsumerApp._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>Testing OAuth Consumer</h1>
    The consumer application needs to obtain the rights to access the data. Here for the demonstration purpouses the steps "Getting authorization" and
    "Getting the date" are separated, but in real world applications the process of getting the rights is incorporated in the request for data - in order to 
    be more transparent to the user.
	<fieldset title="Authorization">
        <legend>Getting the authorization token:</legend>
        <p>Specify the provider OAuth endpoint:</p>
        <asp:TextBox ID="authHandleTextBox" runat="server" Text="http://localhost:51489/OAuth.ashx" />
        <p>The clients application key:</p>
        <asp:TextBox ID="consumerKeyTextBox" runat="server" Text="key1" />
        <p>The clients application secret:</p>
        <asp:TextBox ID="consumerSecretTextBox" runat="server" Text="secret1" />
        <br />
		<asp:Button ID="getAuthorizationButton" runat="server" Text="Get Authorization" OnClick="getAuthorizationButton_Click" />
		<asp:Label ID="authorizationLabel" runat="server" />
	</fieldset>
	<br />
    <fieldset>
        <legend>Getting the data:</legend>
        <asp:TextBox ID="serviceTextBox" runat="server" Text="http://localhost:51489/API/Service1.svc/GetAccounts" />
        <asp:Button ID="getDataButton" runat="server" Text="Get Data" OnClick="getData_Click" />
        <br />
        <asp:ListBox ID="accountListBox" runat="server" />
        <br />
        <asp:Label ID="dataLabel" runat="server" />
    </fieldset>    
</asp:Content>