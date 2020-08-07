using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.Linq;
using WebControls = System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using Subgurim.Controles;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Web.Security;
using System.Net;
using System.Data;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.Exceptions.UI;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using GTS.Clock.Business.RequestFlow;


namespace GTS.Clock.Presentaion.WebForms
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

        enum MenuItemType
        {
            Top,
            SkinsGroup,
            Skin
        }

        enum NavBarItemType
        {
            UsingBySubstitiute,
            Normal
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

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        enum Scripts
        {
            NavBarMain_Operations,
            MainTabStrip_Operations,
            MainForm_onPageLoad,
            MainForm_Operations,
            DialogPersonnelMainInformation_onPageLoad,
            DialogCalendar_onPageLoad,
            DialogPersonnelSearch_onPageLoad,
            DialogExceptionShifts_onPageLoad,
            DialogLeaveBudget_onPageLoad,
            DialogUserInterfaceAccessLevels_onPageLoad,
            DialogMonthlyOperationGridSchema_onPageLoad,
            DialogMonthlyOperationGanttChartSchema_onPageLoad,
            DialogUnderManagementPersonnel_onPageLoad,
            DialogKartable_onPageLoad,
            DialogLoading_Operations,
            DialogEndorsementFlowState_onPageLoad,
            DialogRegisteredRequests_onPageLoad,
            DialogCalculationRange_onPageLoad,
            DialogRulesGroupUpdate_onPageLoad,
            DialogMasterRulesView_onPageLoad,
            DialogReportParameters_onPageLoad,
            DialogSubstituteSettings_onPageLoad,
            DialogOperators_onPageLoad,
            DialogLeaveReserve_onPageLoad,
            DialogMasterDataAccessLevels_onPageLoad,
            DialogSendPrivateMessage_onPageLoad,
            DialogUiValidationRules_onPageLoad,
            DialogUserSettings_onPageLoad,
            DialogTrafficsTransfer_onPageLoad,
            DialogSystemReports_onPageLoad,
            DialogMonthlyExceptionShifts_onPageLoad,
            TopToolBar_Operations,
            DockedMenu,
            DockMenuOperations,
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                RefererValidationProvider.CheckMainPageReferer();
                Page MainForm = this;
                Ajax.Utility.GenerateMethodScripts(MainForm);
                this.SetCurrentUser();
                CacheSettingsProvider.ClearCache();
                this.CreateAccessNotAllowdNavBarItemsList();
                this.GetCurrentLangID();
                this.InitializeSkin();
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void InitializeSkin()
        {            
            SkinHelper.InitializeSkin(this.Page);
            //this.SetRelativeSkinHeaderFlash();
            this.SetRelativeSkinHeaderLogo();
            SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripMenus);
        }

        //private void SetRelativeSkinHeaderFlash()
        //{
        //    this.HeaderFlashControl.MovieUrl = SkinHelper.GetRelativeHeaderFlash(this.Page);
        //}

        private void SetRelativeSkinHeaderLogo()
        {
            this.imgHeaderLogo.Src = SkinHelper.GetRelativeHeaderLogo(this.Page);
        }


        private void GetCurrentLangID()
        {
            this.hfCurrentUILangID.Value = CurrentUILangID;
            this.hfCurrentSysLangID.Value = CurrentSysLangID;
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
            bool IsAvailable = true;
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = Server.MapPath("~/web.config");
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["IsUnderConstraction"] != null)
            {
                try
                {
                    bool IsUnderConstructon = bool.Parse(config.AppSettings.Settings["IsUnderConstraction"].Value);
                    IsAvailable = IsUnderConstructon && BUser.CurrentUser.Role.CustomCode != "1" ? false : true;
                }
                catch (Exception)
                {
                }
            }
            if (!IsAvailable)
                Response.Redirect("UnderConstruction.aspx");
        }

        protected void imgbtnPersian_onClick(object sender, ImageClickEventArgs e)
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

        protected void imgbtnLogOut_onClick(object sender, ImageClickEventArgs e)
        {
            BUser.ClearUserCach();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
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

        private void CreateAccessNotAllowdNavBarItemsList()
        {
            ArrayList AccessNotAllowdNavBarItemsList = new ArrayList();
            foreach (NavBarItem nvbItem in base.AccessNotAllowdNavBarItemsList)
            {
                AccessNotAllowdNavBarItemsList.Add(nvbItem.ID);
            }
            this.hfAccessNoAllowdNavBarItemsList.Value = this.JsSerializer.Serialize(AccessNotAllowdNavBarItemsList);
        }

    }
}