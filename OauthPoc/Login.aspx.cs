using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace OauthPoc
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logon_Click(object sender, EventArgs e)
        {
            if ((txbLogin.Text == "test") &&
                    (txbPass.Text == "test"))
            {
                FormsAuthentication.RedirectFromLoginPage(txbLogin.Text, true);
            }
            else
            {
                Msg.Text = "Invalid credentials. Please try again.";
            }
        }
    }
}