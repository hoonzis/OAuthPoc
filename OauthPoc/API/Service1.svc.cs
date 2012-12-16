using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OauthPoc.Model;

namespace OauthPoc
{
    public class Service1 : IService1
    {
        public string GetData()
        {
            return "Some secret data";
        }

        /// <summary>
        /// This service returns some data in JSON protected by OAuth. Imagine your bank having open OAuth API which developers could 
        /// use to build cool applications, wouldn't it be cool.
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account> {
                new Account { Name = "Current account", Balance = 1400, Currency = "EUR", Iban = "13243-343043i-45"},
                new Account { Name = "Savings account", Balance = 2500, Currency = "EUR", Iban = "1235485609454000"}
            };

            return accounts;
        }
    }
}
