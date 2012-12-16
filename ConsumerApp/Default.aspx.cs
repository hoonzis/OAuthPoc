using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Net;
using System.IO;
using DotNetOpenAuth.OAuth.Messages;
using System.Web.Script.Serialization;

namespace ConsumerApp
{
    public partial class _Default : System.Web.UI.Page
    {
        WebRequest httpRequest;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get the values from authHandler and service (or the scope which will be accessed)
                if (Session["authHandler"] != null)
                {
                    authHandleTextBox.Text = (String)Session["authHandler"];
                }

                if (Session["serviceScope"] != null)
                {
                    serviceTextBox.Text = (String)Session["serviceScope"];
                }

                if (Session["WcfTokenManager"] != null)
                {
                    WebConsumer consumer = this.CreateConsumer();
                    var accessTokenMessage = consumer.ProcessUserAuthorization();
                    if (accessTokenMessage != null)
                    {
                        Session["WcfAccessToken"] = accessTokenMessage.AccessToken;
                        this.authorizationLabel.Text = "Authorized!  Access token: " + accessTokenMessage.AccessToken;
                    }
                }
            }
        }

        /// <summary>
        /// Handler for the 'Authorize' button. The consumer application will ask the provider the rights to access some data.
        /// For this PoC the "scope" of the demand is the same as the URL of the service which will be accessed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void getAuthorizationButton_Click(object sender, EventArgs e)
        {
           
            var serviceScope = serviceTextBox.Text;
            //the scope is added to the session.
            Session["serviceScope"] = serviceScope;

            //creating the DotNetOPenAuth Consumer class
            WebConsumer consumer = this.CreateConsumer();
            UriBuilder callback = new UriBuilder(Request.Url);
            callback.Query = null;
            var requestParams = new Dictionary<string, string> { { "scope", serviceScope }, };
            var response = consumer.PrepareRequestUserAuthorization(callback.Uri, requestParams, null);
            consumer.Channel.Send(response);
        }

        protected void getData_Click(object sender, EventArgs e)
        {
            WebConsumer consumer = CreateConsumer();
            
            var serviceEndpoint = new MessageReceivingEndpoint(serviceTextBox.Text, HttpDeliveryMethods.GetRequest);
            var accessToken = Session["WcfAccessToken"] as string;
            if (accessToken == null)
            {
                throw new InvalidOperationException("No access token!");
            }

            try
            {
                httpRequest = consumer.PrepareAuthorizedRequest(serviceEndpoint, accessToken);
                var response = httpRequest.GetResponse();
                using (var stream = response.GetResponseStream())
                {


                    using (StreamReader reader = new StreamReader(stream))
                    {
                        String data = reader.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var dataObject = js.Deserialize<dynamic>(data);
                        for (int i = 0; i < dataObject.Length; i++)
                        {
                            accountListBox.Items.Add(String.Format("{0} - {1} - {2} - {3}", dataObject[i]["Name"], dataObject[i]["Iban"], dataObject[i]["Balance"], dataObject[i]["Currency"]));
                        }
                        dataLabel.Text = data;
                    }
                }
            }
            catch (WebException ex)
            {
                var message = String.Format("Could not access the data: {0}", ex.Message);
                dataLabel.Text = message;
            }
        }

        private WebConsumer CreateConsumer()
        {

            String authHandler = authHandleTextBox.Text;
            string consumerKey = consumerKeyTextBox.Text;
            string consumerSecret = consumerSecretTextBox.Text;

            Session["authHandler"] = authHandler;

            var tokenManager = Session["WcfTokenManager"] as InMemoryTokenManager;
            if (tokenManager == null)
            {
                tokenManager = new InMemoryTokenManager(consumerKey, consumerSecret);
                Session["WcfTokenManager"] = tokenManager;
            }
            MessageReceivingEndpoint oauthEndpoint = new MessageReceivingEndpoint(
                new Uri(authHandler),
                HttpDeliveryMethods.PostRequest);
            WebConsumer consumer = new WebConsumer(
                new ServiceProviderDescription
                {
                    RequestTokenEndpoint = oauthEndpoint,
                    UserAuthorizationEndpoint = oauthEndpoint,
                    AccessTokenEndpoint = oauthEndpoint,
                    TamperProtectionElements = new DotNetOpenAuth.Messaging.ITamperProtectionChannelBindingElement[] {
					new HmacSha1SigningBindingElement(),
				},
                },
                tokenManager);

            return consumer;
        }
    }
}
