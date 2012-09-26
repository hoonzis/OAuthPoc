using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DotNetOpenAuth.OAuth.ChannelElements;
using OauthPoc.Tools;
using OauthPoc.Code;
using DotNetOpenAuth.OAuth.Messages;
using System.Text;

namespace OauthPoc
{
    public class Global : System.Web.HttpApplication
    {

        /// <summary>
        /// An application memory cache of recent log messages.
        /// </summary>
        public static StringBuilder LogMessages = new StringBuilder();

        /// <summary>
        /// The logger for this sample to use.
        /// </summary>
        public static log4net.ILog Logger = log4net.LogManager.GetLogger("DotNetOpenAuth.OAuthServiceProvider");


        public static User LoggedInUser
        {
            get { return Global.Users.SingleOrDefault(user => user.Login == HttpContext.Current.User.Identity.Name); }
        }

        public static void AuthorizePendingRequestToken()
        {
            ITokenContainingMessage tokenMessage = PendingOAuthAuthorization;
            TokenManager.AuthorizeRequestToken(tokenMessage.Token, LoggedInUser);
            PendingOAuthAuthorization = null;
        }

        public static List<OAuthConsumer> Consumers { get; set; }
        public static List<OAuthToken> AuthTokens { get; set; }
        public static List<Nonce> Nonces { get; set; }
        public static List<User> Users { get; set; }

        public static DatabaseNonceStore NonceStore { get; set; }

        public static DatabaseTokenManager TokenManager { get; set; }

        public static UserAuthorizationRequest PendingOAuthAuthorization
        {
            get { return HttpContext.Current.Session["authrequest"] as UserAuthorizationRequest; }
            set { HttpContext.Current.Session["authrequest"] = value; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {

            log4net.Config.XmlConfigurator.Configure();
            Logger.Info("Sample starting...");
			
            Consumers = new List<OAuthConsumer>();
            Consumers.Add(new OAuthConsumer { Key = "key1", Secret = "secret1", Callback = new Uri("http://localhost:51439/") });

            Nonces = new List<Nonce>();
            
            Users = new List<User>();
            Users.Add(new User { Login = "test", Password = "test" });

            AuthTokens = new List<OAuthToken>();


            string appPath = HttpContext.Current.Request.ApplicationPath;
            if (!appPath.EndsWith("/"))
            {
                appPath += "/";
            }

            Global.TokenManager = new DatabaseTokenManager();
            Global.NonceStore = new DatabaseNonceStore();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.Info("Sample shutting down...");

            // this would be automatic, but in partial trust scenarios it is not.
            log4net.LogManager.Shutdown();
        }

        
    }
}