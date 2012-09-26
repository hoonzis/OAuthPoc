using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace OauthPoc.Code
{
    public static class Constants
    {
        private static Uri _webRootUrl;
        public static Uri WebRootUrl
        {
            get
            {
                if (_webRootUrl == null)
                {
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    if (!appPath.EndsWith("/"))
                    {
                        appPath += "/";
                    }

                    _webRootUrl = new Uri(HttpContext.Current.Request.Url, appPath);
                }
                return _webRootUrl;
            }
        }

        public static ServiceProviderDescription SelfDescription
        {
            get
            {
                ServiceProviderDescription description = new ServiceProviderDescription
                {
                    AccessTokenEndpoint = new MessageReceivingEndpoint(new Uri(WebRootUrl, "/OAuth.ashx"), HttpDeliveryMethods.PostRequest),
                    RequestTokenEndpoint = new MessageReceivingEndpoint(new Uri(WebRootUrl, "/OAuth.ashx"), HttpDeliveryMethods.PostRequest),
                    UserAuthorizationEndpoint = new MessageReceivingEndpoint(new Uri(WebRootUrl, "/OAuth.ashx"), HttpDeliveryMethods.PostRequest),
                    TamperProtectionElements = new ITamperProtectionChannelBindingElement[] {
					new HmacSha1SigningBindingElement(),
				},
                };

                return description;
            }
        }

        public static ServiceProvider CreateServiceProvider()
        {
            return new ServiceProvider(SelfDescription, Global.TokenManager, Global.NonceStore);
        }
    }
}