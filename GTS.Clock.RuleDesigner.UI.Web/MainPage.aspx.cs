using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GTS.Clock.Infrastructure;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Proxy;
using System.Web.Security;
using GTS.Clock.Business.AppSettings;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business.UI;
using GTS.Clock.RuleDesigner.UI.Web.Classes;

namespace GTS.Clock.RuleDesigner.UI.Web
{
	public partial class MainPage : GTSBasePage
	{
		public BLanguage LangProv
		{
			get
			{
				return new BLanguage();
			}
		}

        public StringGenerator StringBuilder
		{
			get
			{
				return new StringGenerator();
			}
		}

		public ExceptionHandler exceptionHandler
		{
			get
			{
				return new ExceptionHandler();
			}
		}

		public CacheSettingsProvider CSP
		{
			get
			{
				return new CacheSettingsProvider();
			}
		}

		enum MenuItemType
		{
			Top,
			SkinsGroup,
			Skin
		}

		public string CurrentUILangID { get; set; }
		public string CurrentSysLangID { get; set; }

		public OperationYearMonthProvider operationYearMonthProvider
		{
			get
			{
				return new OperationYearMonthProvider();
			}
		}

        private enum Scripts
        {
            NavBarMain_Operations,
            MainTabStrip_Operations,
            MainForm_onPageLoad,
            MainForm_Operations,
            TopToolBar_Operations,
            DockedMenu,
            Alert_Box,
            DialogLoading_Operations,
            DockMenuOperations,
            DialogConceptsManagement_onPageLoad,
            DialogRulesManagement_onPageLoad,
            DialogConceptRuleEditor_onPageLoad,
            DialogExpressionsManagement_onPageLoad,

        }

		protected void Page_Load(object sender, EventArgs e)
		{
			this.CheckApplicationIsAvailable();
			if (!Page.IsPostBack)
			{
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                RefererValidationProvider.CheckMainPageReferer();

                Ajax.Utility.GenerateMethodScripts(this);
				this.SetCurrentUser();
				this.CSP.ClearCache();
				this.GetCurrentLangID();
				this.InitializeSkin();
			}
			tlbLogout.ItemCommand += new ComponentArt.Web.UI.ToolBar.ItemCommandEventHandler(tlbLogout_ItemCommand);
		}

		private void InitializeSkin()
		{
			SkinHelper.InitializeSkin(this.Page);
			this.SetRelativeSkinHeaderFlash();
			SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripMenus);
		}

		private void SetRelativeSkinHeaderFlash()
		{
			//this.HeaderFlashControl.MovieUrl = SkinHelper.GetRelativeHeaderFlash(this.Page);
		}

		private void GetCurrentLangID()
		{
			this.hfCurrentUILangID.Value = CurrentUILangID;
			this.hfCurrentSysLangID.Value = CurrentSysLangID;
		}

		void tlbLogout_ItemCommand(object sender, ToolBarItemEventArgs e)
		{
			BUser.ClearUserCach();
			FormsAuthentication.SignOut();
			FormsAuthentication.RedirectToLoginPage();
		}

		private void SetCurrentUser()
		{
			lblCurrentUser.Text = GetLocalResourceObject("WelcomeMessage").ToString() + " " + this.GetLocalResourceObject(BUser.CurrentUser.Person.Sex.ToString()).ToString() + " " + BUser.CurrentUser.Person.LastName;
		}
        
		protected override void InitializeCulture()
		{
			this.CurrentUILangID = this.LangProv.GetCurrentLanguage();
			this.CurrentUILangID = this.CurrentUILangID;
			this.CurrentSysLangID = this.LangProv.GetCurrentSysLanguage();
			this.SetCurrentCultureResObjs(this.CurrentUILangID);
			base.InitializeCulture();
		}

		private void SetCurrentCulture()
		{
			if (this.CurrentUILangID != null)
			{
				try
				{
					GTS.Clock.Business.AppSettings.SupportedLangs sl = (GTS.Clock.Business.AppSettings.SupportedLangs)Enum.Parse(typeof(GTS.Clock.Business.AppSettings.SupportedLangs), this.CurrentUILangID.Replace("-", string.Empty));
					this.LangProv.SetCurrentLanguage(this.CurrentUILangID);
					Response.Redirect("MainPage.aspx");
				}
				catch
				{
				}
			}
		}

		private void SetCurrentCultureResObjs(string LangID)
		{
			try
			{
				Session["LangID"] = LangID;
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
			}
			catch
			{ }
		}

		private void CheckApplicationIsAvailable()
		{
			var isAvailable = true;
			var filemap = new ExeConfigurationFileMap {ExeConfigFilename = Server.MapPath("~/web.config")};
		    var config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
			if (config.AppSettings.Settings["IsUnderConstraction"] != null)
			{
				try
				{
					bool IsUnderConstructon = bool.Parse(config.AppSettings.Settings["IsUnderConstraction"].Value);
					isAvailable = !IsUnderConstructon || BUser.CurrentUser.Role.CustomCode == "1";
				}
				catch (Exception)
				{
				}
			}
			if (!isAvailable)
				Response.Redirect("UnderConstruction.aspx");
		}
        
		protected void imgPersian_onClick(object sender, ImageClickEventArgs e)
		{
			this.CurrentUILangID = "fa-IR";
			this.SetCurrentCulture();
			this.operationYearMonthProvider.ResetOperationYearMonth();
		}

		protected void ImgbtnEnglish_onClick(object sender, ImageClickEventArgs e)
		{
			this.CurrentUILangID = "en-US";
			this.SetCurrentCulture();
			this.operationYearMonthProvider.ResetOperationYearMonth();
		}

		[Ajax.AjaxMethod("ChangeSkin_MainPage", "ChangeSkin_MainPage_onCallBack", null, null)]
		public string[] ChangeSkin_MainPage(string SkinID)
		{
			this.InitializeCulture();

			string[] retMessage = new string[4];

			decimal skinID = decimal.Parse(this.StringBuilder.CreateString(SkinID));

			try
			{
				SkinHelper.SetCurrentSkin(skinID);

				retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
				retMessage[1] = GetLocalResourceObject("SkinChangeComplete").ToString();
				retMessage[2] = "success";
				retMessage[3] = skinID.ToString();
				return retMessage;
			}
			catch (UIValidationExceptions ex)
			{
				retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
				return retMessage;
			}
			catch (UIBaseException ex)
			{
				retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
				return retMessage;
			}
			catch (Exception ex)
			{
				retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
				return retMessage;
			}
		}

	}
    
}
