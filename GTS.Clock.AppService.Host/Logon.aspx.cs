using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//using FormsAuthAD;
using GTS.Clock.Business.Security;


namespace ActiveDirectory
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string adPath =
                        "LDAP://ghadir.local/DC=ghadir,DC=local";
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
         
            string logoninfo = Request.ServerVariables["LOGON_USER"];
            
            if (logoninfo.Length > 0)
            {
                Response.Write("<br> LOGON_USER : " + logoninfo);
            }
            else 
            {
                Response.Write("<br>No User Info LOGON_USER");
            }

            logoninfo = Request.ServerVariables["REMOTE_HOST"];
            if (logoninfo.Length > 0)
            {
                Response.Write("<br> REMOTE_HOST : " + logoninfo);
            }
            else
            {
                Response.Write("<br>No User Info REMOTE_HOST");
            }
            logoninfo = Request.ServerVariables["AUTH_USER"];
            if (logoninfo.Length > 0)
            {
                Response.Write("<br> AUTH_USER :" + logoninfo);
            }
            else
            {
                Response.Write("<br>No User Info AUTH_USER");
            }

            logoninfo = Request.ServerVariables["HTTP_HOST"];
            if (logoninfo.Length > 0)
            {
                Response.Write("<br> HTTP_HOST :" + logoninfo);
            }
            else
            {
                Response.Write("<br>No User Info HTTP_HOST");
            }

            Response.Write("<br> Request.LogonUserIdentity.Name :" + Request.LogonUserIdentity.Name + "<hr><br>");
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {           
            string adPath =
              "LDAP://ghadir.local/DC=ghadir,DC=local";
            LdapAuthentication adAuth = new LdapAuthentication(adPath);

            try
            {
                if (true == adAuth.IsAuthenticated(txtDomainName.Text,
                                                  txtUserName.Text,
                                                  txtPassword.Text))
                {
                    // Redirect the user to the originally requested page
                    Response.Redirect(
                              FormsAuthentication.GetRedirectUrl(txtUserName.Text,
                                                                 false));
                }
                else
                {
                    lblError.Text =
                         "Authentication failed, check username and password.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error authenticating. " + ex.Message;
            }

        }    
    }
}
