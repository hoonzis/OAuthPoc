namespace OauthPoc.Code {
	using System;
	using System.Collections.Generic;
	using System.IdentityModel.Policy;
	using System.Linq;
	using System.Security.Principal;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Security;
	using DotNetOpenAuth;
	using DotNetOpenAuth.OAuth;
    using OauthPoc.Code;
    using OauthPoc;
    using System.Diagnostics;

	/// <summary>
	/// A WCF extension to authenticate incoming messages using OAuth.
	/// </summary>
	public class OAuthAuthorizationManager : ServiceAuthorizationManager {
		public OAuthAuthorizationManager() {
		}

		protected override bool CheckAccessCore(OperationContext operationContext) {
			if (!base.CheckAccessCore(operationContext)) {
				return false;
			}

			HttpRequestMessageProperty httpDetails = operationContext.RequestContext.RequestMessage.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
			Uri requestUri = operationContext.RequestContext.RequestMessage.Properties.Via;
			ServiceProvider sp = Constants.CreateServiceProvider();
			try {
				var auth = sp.ReadProtectedResourceAuthorization(httpDetails, requestUri);
				if (auth != null) {
					var accessToken = Global.AuthTokens.Single(token => token.Token == auth.AccessToken);

					var principal = sp.CreatePrincipal(auth);
					var policy = new OAuthPrincipalAuthorizationPolicy(principal);
					var policies = new List<IAuthorizationPolicy> {
					policy,
				};

					var securityContext = new ServiceSecurityContext(policies.AsReadOnly());
					if (operationContext.IncomingMessageProperties.Security != null) {
						operationContext.IncomingMessageProperties.Security.ServiceSecurityContext = securityContext;
					} else {
						operationContext.IncomingMessageProperties.Security = new SecurityMessageProperty {
							ServiceSecurityContext = securityContext,
						};
					}

					securityContext.AuthorizationContext.Properties["Identities"] = new List<IIdentity> {
					principal.Identity,
				};

					// Only allow this method call if the access token scope permits it.
					string[] scopes = accessToken.Scope.Split('|');

                    //originally this was ment to be used: operationContext.IncomingMessageHeaders.Action
                    //var action = operationContext.Host.BaseAddresses + operationContext.IncomingMessageProperties.Via.AbsolutePath;
                    var action = "http://" + operationContext.IncomingMessageProperties.Via.Authority + operationContext.IncomingMessageProperties.Via.AbsolutePath;
					if (scopes.Contains(action)) {
						return true;
					}
				}
			} catch (ProtocolException ex) {
                Debug.WriteLine(ex.Message);
			}

			return false;
		}
	}
}