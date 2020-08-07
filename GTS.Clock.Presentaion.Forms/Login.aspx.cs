using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Threading;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Security;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Login : System.Web.UI.Page
    {

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        public CacheSettingsProvider CSP
        {
            get
            {
                return new CacheSettingsProvider();
            }
        }

        public SecurityImageProvider SIP
        {
            get
            {
                return new SecurityImageProvider();
            }
        }

        enum Scripts
        {
            Login_onPageLoad,
            Login_Operations,
            keyboard
        }

        #region interface implement

        public System.Web.UI.WebControls.Login Logincontrol
        {
            get { return theLogincontrol; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(LangProv.GetCurrentSysLanguage());
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckLoginPageReferer();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckSecurityCode();            
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            BUser.ClearUserCach();
            Session.Clear();
        }

        private void CheckSecurityCode()
        {
            HtmlTableRow SecurityCodeContainer = (HtmlTableRow)this.theLogincontrol.FindControl("SecurityCodeContainer");
            try
            {
                bool SecurityCodeEnabled = false;
                if (WebConfigurationManager.AppSettings["SecurityCodeEnabled"] != null)
                    SecurityCodeEnabled = bool.Parse(WebConfigurationManager.AppSettings["SecurityCodeEnabled"]);
                if (SecurityCodeEnabled)
                {
                    SecurityCodeContainer.Visible = true;
                    TextBox txtSecurityCode = (TextBox)this.theLogincontrol.FindControl("SecurityCode");
                    if (!this.IsPostBack)
                    {
                        if (Session["SecurityCode"] != null && HttpContext.Current.Request.QueryString.AllKeys.Contains("ISC") && bool.Parse(HttpContext.Current.Request.QueryString["ISC"]))
                        {
                            Session["SecurityCode"] = null;
                            ((Literal)this.Logincontrol.FindControl("FailureText")).Text = GetLocalResourceObject("IncorrectSecurityCode").ToString();
                            txtSecurityCode.Text = "";
                        }
                        Session["SecurityCode"] = this.SIP.GenerateRandomCode();
                    }
                    else
                    {
                        if (Session["SecurityCode"] != null && txtSecurityCode.Text != Session["SecurityCode"].ToString())
                            HttpContext.Current.Response.Redirect("Login.aspx?ISC=true");
                    }
                }
                else
                    SecurityCodeContainer.Visible = false;

            }
            catch (Exception)
            {
                SecurityCodeContainer.Visible = false;
            }
        }
    }
}