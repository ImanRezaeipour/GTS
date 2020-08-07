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
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            BUser.ClearUserCach();
            Session.Clear();
        }
    }
}