using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OauthPoc.Model
{
    /// <summary>
    /// Class that represents simple bank account. This is only for demonstration pourposes.
    /// </summary>
    public class Account
    {
        public String Name { get; set; }
        public String Iban { get; set; }
        public Decimal Balance { get; set; }
        public String Currency { get; set; }
    }
}