using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OauthPoc.Code
{
    public enum TokenAuthorizationState : int
    {
        /// <summary>
        /// An unauthorized request token.
        /// </summary>
        UnauthorizedRequestToken = 0,

        /// <summary>
        /// An authorized request token.
        /// </summary>
        AuthorizedRequestToken = 1,

        /// <summary>
        /// An authorized access token.
        /// </summary>
        AccessToken = 2,
    }
}