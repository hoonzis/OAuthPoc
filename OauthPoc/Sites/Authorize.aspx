<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authorize.aspx.cs" Inherits="OauthPoc.Authorize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:MultiView runat="server" ActiveViewIndex="0" ID="multiView">
        <asp:View runat="server">
            <asp:Panel runat="server" BackColor="Red" ForeColor="White" Font-Bold="true" Visible="false" ID="OAuth10ConsumerWarning">
				    This website is registered with service_PROVIDER_DOMAIN_NAME to make authorization requests, but has not been configured to send requests securely. If you grant access but you did not initiate this request at consumer_DOMAIN_NAME, it may be possible for other users of consumer_DOMAIN_NAME to access your data. We recommend you deny access unless you are certain that you initiated this request directly with consumer_DOMAIN_NAME.
		    </asp:Panel>
            <asp:Label ID="consumerLabel" Font-Bold="true" runat="server" Text="[consumer]" /> wants access to your <asp:Label ID="desiredAccessLabel"
					    Font-Bold="true" runat="server" Text="[protected resource]" />.
            <asp:Button ID="btnAuthorize" runat="server" OnClick="Authtorize_Click" Text="Authorize"/>
            <asp:HiddenField runat="server" ID="OAuthAuthorizationSecToken" EnableViewState="false" />
        </asp:View>
        <asp:View ID="View1" runat="server">
			<p>Authorization has been granted.</p>
			<asp:MultiView runat="server" ID="verifierMultiView" ActiveViewIndex="0">
				<asp:View ID="View2" runat="server">
					<p>You must enter this verification code at the Consumer: <asp:Label runat="server"
						ID="verificationCodeLabel" /> </p>
				</asp:View>
				<asp:View ID="View3" runat="server">
					<p>You may now close this window and return to the Consumer. </p>
				</asp:View>
			</asp:MultiView>
		</asp:View>
    </asp:MultiView>
    </div>
    </form>
</body>
</html>
