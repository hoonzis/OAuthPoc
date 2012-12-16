using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OauthPoc.Code
{
    /// <summary>
    /// Represents a single user. In other words owner of the data, which authentifies and authorizes the applications to access the data.
    /// </summary>
    public class User
    {
        public String Login { get; set; }
        public String Password { get; set; }
    }
}