using System;
using System.Threading;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Business.Security;
using GTS.Clock.RuleDesigner.UI.Web.Classes;

namespace GTS.Clock.RuleDesigner.UI.Web
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

		#region interface implement

		public System.Web.UI.WebControls.Login Logincontrol
		{
			get { return theLogincontrol; }
		}

		#endregion

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