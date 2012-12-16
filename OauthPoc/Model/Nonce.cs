using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OauthPoc.Tools
{
    public class Nonce
    {
        public String Context { get; set; }
        public String Code { get; set; }

        public DateTime Timestamp { get; set; }
    }
}