using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using GTS.Clock.Business.Security;
using System.Web.Configuration;

namespace GTS.Clock.Presentaion.WebForms
{

    public partial class MasterPersonnel : GTSBasePage
    {
        enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
        }

        public BPerson PersonBusiness
        {
            get
            {
                SysLanguageResource Slr = SysLanguageResource.Parsi;
                switch (this.LangProv.GetCurrentSysLanguage())
                {
                    case "fa-IR":
                        Slr = SysLanguageResource.Parsi;
                        break;
                    case "en-US":
                        Slr = SysLanguageResource.English;
                        break;
                }
                LocalLanguageResource Llr = LocalLanguageResource.Parsi;
                switch (this.LangProv.GetCurrentLanguage())
                {
                    case "fa-IR":
                        Llr = LocalLanguageResource.Parsi;
                        break;
                    case "en-US":
                        Llr = LocalLanguageResource.English;
                        break;
                }
                return BusinessHelper.GetBusinessInstance<BPerson>(new KeyValuePair<string, object>("sysLanguage", Slr), new KeyValuePair<string, object>("localLanguage", Llr));
            }
        }

        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }

        public ISearchPerson PersonSearchBusiness
        {
            get
            {
                return (ISearchPerson)BusinessHelper.GetBusinessInstance<BPerson>();
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

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        enum Scripts
        {
            MasterPersonnel_onPageLoad,
            tbPersonnelIntroduction_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridPersonnel_Personnel.IsCallback)
            {
                Page MasterPersonnelMainInformationPage = this;
                Ajax.Utility.GenerateMethodScripts(MasterPersonnelMainInformationPage);

                this.SetSexTitlesStr_Personnel();
                this.SetMilitaryStatusTitlesStr_Personnel();
                this.SetMaritalStatusTitlesStr_Personnel();
                this.GetBaseDateSring_PersonnelMainInformation();
                this.SetPersonnelPageSize_Personnel();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckPersonnelLoadAccess_MasterPersonnel();
            }
        }

        private void CheckPersonnelLoadAccess_MasterPersonnel()
        {
            string[] retMessage = new string[4];
            try
            {
                this.PersonBusiness.CheckPersonnelLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            } 
        }

        private void GetBaseDateSring_PersonnelMainInformation()
        {
            string BaseDateString = string.Empty;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    BaseDateString = GTS.Clock.Infrastructure.Utility.Utility.GTSMinStandardShamsiDateTime;
                    break;
                case "en-US":
                    BaseDateString = GTS.Clock.Infrastructure.Utility.Utility.GTSMinStandardDateTime.ToShortDateString();
                    break;
            }
            this.hfBaseDateString_Personnel.Value = BaseDateString;
        }


        private void SetPersonnelPageSize_Personnel()
        {
            this.hfPersonnelPageSize_Personnel.Value = this.GridPersonnel_Personnel.PageSize.ToString();
        }

        private void SetPersonnelPageCount_Personnel(LoadState Ls, string SearchTerm)
        {
            int PersonnelCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                    break;
                case LoadState.Search:
                    PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(SearchTerm);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                    break;
            }
            this.hfPersonnelCount_Personnel.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_Personnel.Value = Utility.GetPageCount(PersonnelCount, this.GridPersonnel_Personnel.PageSize).ToString();
        }

        private void SetSexTitlesStr_Personnel()
        {
            string strSexTitles = string.Empty;
            foreach (PersonSex personSexItem in Enum.GetValues(typeof(PersonSex)))
            {
                strSexTitles += "#" + GetLocalResourceObject(personSexItem.ToString()).ToString() + ":" + ((int)personSexItem).ToString();
            }
            this.hfSexList_Personnel.Value = strSexTitles;
        }

        private void SetMilitaryStatusTitlesStr_Personnel()
        {
            string strMilitaryStatusTitles = string.Empty;
            foreach (MilitaryStatus militaryStatusItem in Enum.GetValues(typeof(MilitaryStatus)))
            {
                strMilitaryStatusTitles += "#" + GetLocalResourceObject(militaryStatusItem.ToString()).ToString() + ":" + ((int)militaryStatusItem).ToString();
            }
            this.hfMilitaryStatusList_Personnel.Value = strMilitaryStatusTitles;
        }

        private void SetMaritalStatusTitlesStr_Personnel()
        {
            string strMaritalStatusTitles = string.Empty;
            foreach (MaritalStatus maritalStatusItem in Enum.GetValues(typeof(MaritalStatus)))
            {
                strMaritalStatusTitles += "#" + GetLocalResourceObject(maritalStatusItem.ToString()).ToString() + ":" + ((int)maritalStatusItem).ToString();
            }
            this.hfMaritalStatusList_Personnel.Value = strMaritalStatusTitles;
        }

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        protected void CallBack_GridPersonnel_Personnel_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.SetPersonnelPageCount_Personnel((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), e.Parameters.Length > 3 ? this.StringBuilder.CreateString(e.Parameters[3]) : string.Empty);
            this.Fill_GridPersonnel_Personnel((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), e.Parameters.Length > 3 ? this.StringBuilder.CreateString(e.Parameters[3]) : string.Empty);
            this.GridPersonnel_Personnel.RenderControl(e.Output);
            this.hfPersonnelCount_Personnel.RenderControl(e.Output);
            this.hfPersonnelPageCount_Personnel.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel.RenderControl(e.Output);
        }

        private void Fill_GridPersonnel_Personnel(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            IList<Person> PersonnelList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        PersonnelList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelList = this.PersonSearchBusiness.GetPersonInAdvanceSearchApplyCulture(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                        break;
                }
                this.GridPersonnel_Personnel.DataSource = PersonnelList;
                this.GridPersonnel_Personnel.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Personnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdatePersonnel_PersonnelPage", "UpdatePersonnel_PersonnelPage_onCallBack", null, null)]
        public string[] UpdatePersonnel_PersonnelPage(string state, string SelectedPersonnelID, string PersonnelImageFile)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal PersonnelID = 0;
                decimal selectedPersonnelID = decimal.Parse(this.StringBuilder.CreateString(SelectedPersonnelID));
                PersonnelImageFile = this.StringBuilder.CreateString(PersonnelImageFile);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Person person = new Person();

                switch (uam)
                {
                    case UIActionType.DELETE:
                        if (selectedPersonnelID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPersonnelSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        person.ID = selectedPersonnelID;
                        PersonnelID = this.PersonBusiness.DeletePerson(person, uam);
                        this.PersonBusiness.DeletePersonnelImage(WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + PersonnelImageFile);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = PersonnelID.ToString();
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