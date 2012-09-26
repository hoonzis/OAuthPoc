using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OAuth.ChannelElements;
using System.Security.Cryptography.X509Certificates;
using DotNetOpenAuth.OAuth;

namespace OauthPoc.Code
{
    public class OAuthConsumer : IConsumerDescription
    {
        private Uri _callback;

        public Uri Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        Uri IConsumerDescription.Callback
        {
            get { return _callback; }
        }


        private X509Certificate2 _certificate;
        X509Certificate2 IConsumerDescription.Certificate
        {
            get { return _certificate; }
        }

        private string _key;
        string IConsumerDescription.Key
        {
            get { return _key; }
        }

        private string _secret;

        public string Secret
        {
            get { return _secret; }
            set { _secret = value; }
        }
        string IConsumerDescription.Secret
        {
            get { return _secret; }
        }

        private VerificationCodeFormat _verCodeFormat;
        VerificationCodeFormat IConsumerDescription.VerificationCodeFormat
        {
            get { return _verCodeFormat; }
        }

        private int _verCodeLength;
        int IConsumerDescription.VerificationCodeLength
        {
            get { return _verCodeLength; }
        }

        public string Key
        {
            set { _key = value; }
            get { return _key; }
        }


        
    }

    
}