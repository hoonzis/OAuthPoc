using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using OauthPoc.Tools;
using System.Diagnostics;

namespace OauthPoc.Code
{
    public class DatabaseTokenManager : IServiceProviderTokenManager
    {
        #region IServiceProviderTokenManager

        
        public IConsumerDescription GetConsumer(string consumerKey)
        {
            var consumerRow = Global.Consumers.SingleOrDefault(
                consumerCandidate => consumerCandidate.Key == consumerKey);
            if (consumerRow == null)
            {
                throw new KeyNotFoundException();
            }

            return consumerRow;
        }

        public IServiceProviderRequestToken GetRequestToken(string token)
        {

            var foundToken = Global.AuthTokens.FirstOrDefault(t => t.Token == token && t.State != TokenAuthorizationState.AccessToken);
            
            if(foundToken==null)
            {
                throw new KeyNotFoundException("Unrecognized token");
            }
            return foundToken;
        }

        public IServiceProviderAccessToken GetAccessToken(string token)
        {
            try
            {
                return Global.AuthTokens.First(t => t.Token == token && t.State == TokenAuthorizationState.AccessToken);
            }
            catch (InvalidOperationException ex)
            {
                throw new KeyNotFoundException("Unrecognized token", ex);
            }
        }

        public void UpdateToken(IServiceProviderRequestToken token)
        {
            var tokenInDb = Global.AuthTokens.SingleOrDefault(x => x.Token == token.Token);
            if (tokenInDb != null)
            {
                tokenInDb.VerificationCode = token.VerificationCode;
                tokenInDb.Callback = token.Callback;
                //tokenInDb.ConsumerKey = token.ConsumerKey;
                tokenInDb.Version = token.ConsumerVersion;
                tokenInDb.Token = token.Token;
                tokenInDb.VerificationCode = token.VerificationCode;
            }
        }

        #endregion

        #region ITokenManager Members

        public string GetTokenSecret(string token)
        {
            var tokenRow = Global.AuthTokens.SingleOrDefault(
                tokenCandidate => tokenCandidate.Token == token);
            if (tokenRow == null)
            {
                throw new ArgumentException();
            }

            return tokenRow.TokenSecret;
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            RequestScopedTokenMessage scopedRequest = (RequestScopedTokenMessage)request;
            var consumer = Global.Consumers.Single(consumerRow => consumerRow.Key == request.ConsumerKey);
            string scope = scopedRequest.Scope;
            OAuthToken newToken = new OAuthToken
            {
                Consumer = consumer,
                Token = response.Token,
                TokenSecret = response.TokenSecret,
                IssueDate = DateTime.UtcNow,
                Scope = scope,
            };

            Global.AuthTokens.Add(newToken);
        }

        /// <summary>
        /// Checks whether a given request token has already been authorized
        /// by some user for use by the Consumer that requested it.
        /// </summary>
        /// <param name="requestToken">The Consumer's request token.</param>
        /// <returns>
        /// True if the request token has already been fully authorized by the user
        /// who owns the relevant protected resources.  False if the token has not yet
        /// been authorized, has expired or does not exist.
        /// </returns>
        public bool IsRequestTokenAuthorized(string requestToken)
        {
            var tokenFound = Global.AuthTokens.SingleOrDefault(
                token => token.Token == requestToken &&
                token.State == TokenAuthorizationState.AuthorizedRequestToken);
            return tokenFound != null;
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            
            var consumerRow = Global.Consumers.Single(consumer => consumer.Key == consumerKey);
            var tokenRow = Global.AuthTokens.Single(token => token.Token == requestToken && token.Consumer == consumerRow);
            Debug.Assert(tokenRow.State == TokenAuthorizationState.AuthorizedRequestToken, "The token should be authorized already!");

            // Update the existing row to be an access token.
            tokenRow.IssueDate = DateTime.UtcNow;
            tokenRow.State = TokenAuthorizationState.AccessToken;
            tokenRow.Token = accessToken;
            tokenRow.TokenSecret = accessTokenSecret;
        }

        /// <summary>
        /// Classifies a token as a request token or an access token.
        /// </summary>
        /// <param name="token">The token to classify.</param>
        /// <returns>Request or Access token, or invalid if the token is not recognized.</returns>
        public TokenType GetTokenType(string token)
        {
            var tokenRow = Global.AuthTokens.SingleOrDefault(tokenCandidate => tokenCandidate.Token == token);
            if (tokenRow == null)
            {
                return TokenType.InvalidToken;
            }
            else if (tokenRow.State == TokenAuthorizationState.AccessToken)
            {
                return TokenType.AccessToken;
            }
            else
            {
                return TokenType.RequestToken;
            }
        }

        #endregion

        public void AuthorizeRequestToken(string requestToken, User user)
        {
            if (requestToken == null)
            {
                throw new ArgumentNullException("requestToken");
            }
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var tokenRow = Global.AuthTokens.SingleOrDefault(
                tokenCandidate => tokenCandidate.Token == requestToken &&
                tokenCandidate.State == TokenAuthorizationState.UnauthorizedRequestToken);
            if (tokenRow == null)
            {
                throw new ArgumentException();
            }

            tokenRow.State = TokenAuthorizationState.AuthorizedRequestToken;
            tokenRow.User = user;
        }

        public OAuthConsumer GetConsumerForToken(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("requestToken");
            }

            var tokenRow = Global.AuthTokens.SingleOrDefault(
                tokenCandidate => tokenCandidate.Token == token);
            if (tokenRow == null)
            {
                throw new ArgumentException();
            }

            return tokenRow.Consumer;
        }
    }
}