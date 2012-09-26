using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace OauthPoc.Code
{
    public class OAuthToken : IServiceProviderRequestToken, IServiceProviderAccessToken
    {

        #region IServiceProviderRequestToken

        private String _spRequesttoken;
        String IServiceProviderRequestToken.Token
        {
            get { return _spRequesttoken; }
        }

        private Uri _callback;

        public Uri Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        Uri IServiceProviderRequestToken.Callback
        {
            get
            {
                return _callback;
            }
            set
            {
                _callback = value;
            }
        }

        string IServiceProviderRequestToken.ConsumerKey
        {
            get { return Consumer.Key; }
        }

        private Version _version;

        public Version Version
        {
            get { return _version; }
            set { _version = value; }
        }
        Version IServiceProviderRequestToken.ConsumerVersion
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }

        //private DateTime _createdOn;

        //public DateTime CreatedOn
        //{
        //    get { return _createdOn; }
        //    set { _createdOn = value; }
        //}

        DateTime IServiceProviderRequestToken.CreatedOn
        {
            get { return IssueDate; }
        }


        private string _verificationCode;

        public string VerificationCode
        {
            get { return _verificationCode; }
            set { _verificationCode = value; }
        }
        string IServiceProviderRequestToken.VerificationCode
        {
            get
            {
                return _verificationCode;
            }
            set
            {
                _verificationCode = value;
            }
        }

        #endregion

        #region IServiceProviderAccessToken

        private DateTime? _expirationDate;
        DateTime? IServiceProviderAccessToken.ExpirationDate
        {
            get { return _expirationDate; }
        }

        private string[] _roles;
        string[] IServiceProviderAccessToken.Roles
        {
            get { return _roles; }
        }

        string IServiceProviderAccessToken.Username
        {
            get { return User.Login; }
        }

        private String _spaccessToken;
        String IServiceProviderAccessToken.Token 
        {
            get { return _spaccessToken; }
        }

        #endregion

        private OAuthConsumer _consumer;

        public OAuthConsumer Consumer
        {
          get { return _consumer; }
          set { _consumer = value; }
        }

        private TokenAuthorizationState _state;

        public TokenAuthorizationState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private DateTime _issueDate;

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set { _issueDate = value; }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        private String _tokenSecret;

        public String TokenSecret
        {
            get { return _tokenSecret; }
            set { _tokenSecret = value; }
        }

        private String _scope;

        public String Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        public String Token { 
            set { 
                _spaccessToken = value; 
                _spRequesttoken = value; 
            }

            get
            {
                return _spaccessToken;
            }
        }
    }
}