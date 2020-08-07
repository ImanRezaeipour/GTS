using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Substitute : GTSBasePage
    {
        public BSubstitute SubstituteBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BSubstitute>();
            }
        }

        public ISearchPerson PersonnelBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }


        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }

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

        public JavaScriptSerializer JsSeializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        internal class PersonnelDetails
        {
            public string ID { get; set; }
            public string OrganizationPostID { get; set; }
            public string OrganizationPostName { get; set; }
        }

        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
        }

        enum Scripts
        {
            Substitute_onPageLoad,
            tbSubstituteIntroduction_TabStripMenus_Operations,
            DropDownDive,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbManagers_Substitute.IsCallback && !CallBack_cmbPersonnel_Substitute.IsCallback && !CallBack_GridSubstitutes_Substitute.IsCallback)
            {
                Page SubstitutePage = this;
                Ajax.Utility.GenerateMethodScripts(SubstitutePage);

                this.CheckSubstitiuteLoadAccess_Substitute();
                this.ViewCurrentLangCalendars_Substitute();
                this.SetCurrentDate_Substitute();
                this.SetManagersPageSize_cmbManagers_Substitute();
                this.SetManagersPageCount_cmbManagers_Substitute(LoadState.Normal, this.cmbManagers_Substitute.DropDownPageSize, string.Empty);
                this.SetPersonnelPageSize_cmbPersonnel_Substitute();
                this.SetPersonnelPageCount_cmbPersonnel_Substitute(LoadState.Normal, this.cmbPersonnel_Substitute.DropDownPageSize, string.Empty);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckSubstitiuteLoadAccess_Substitute()
        {
            string[] retMessage = new string[4];
            try
            {
                this.SubstituteBusiness.CheckSubstitiuteLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            } 
        }

        private void ViewCurrentLangCalendars_Substitute()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpMasterFromDate_Substitute.Visible = true;
                    this.Container_pdpMasterToDate_Substitute.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpMasterFromDate_Substitute.Visible = true;
                    this.Container_gdpMasterToDate_Substitute.Visible = true;
                    break;
            }
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

        private void SetCurrentDate_Substitute()
        {
            string strCurrentDate = string.Empty;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "en-US":
                    strCurrentDate = DateTime.Now.ToShortDateString();
                    break;
                case "fa-IR":
                    strCurrentDate = this.LangProv.GetSysDateString(DateTime.Now);
                    break;
            }
            this.hfCurrentDate_Substitute.Value = strCurrentDate;
        }

        private void SetManagersPageSize_cmbManagers_Substitute()
        {
            this.hfManagerPageSize_Substitute.Value = this.cmbManagers_Substitute.DropDownPageSize.ToString();
        }

        private void SetManagersPageCount_cmbManagers_Substitute(LoadState Ls, int pageSize, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int ManagersCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        ManagersCount = this.SubstituteBusiness.GetAllManagerCount();
                        break;
                    case LoadState.Search:
                        ManagersCount = this.SubstituteBusiness.GetAllManagerCount(SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        ManagersCount = this.SubstituteBusiness.GetAllManagerCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                        break;
                    default:
                        break;
                }
                this.hfManagerPageCount_Substitute.Value = Utility.GetPageCount(ManagersCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Managers_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Managers_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Managers_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void SetPersonnelPageSize_cmbPersonnel_Substitute()
        {
            this.hfPersonnelPageSize_Substitute.Value = this.cmbPersonnel_Substitute.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnel_Substitute(LoadState Ls, int pageSize, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int PersonnelCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelCount = this.PersonnelBusiness.GetPersonCount();
                        break;
                    case LoadState.Search:
                        PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                        break;
                    default:
                        break;
                }
                this.hfPersonnelPageCount_Substitute.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbManagers_Substitute_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbManagers_Substitute.Dispose();
            this.SetManagersPageCount_cmbManagers_Substitute((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbManagers_Substitute((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbManagers_Substitute.RenderControl(e.Output);
            this.hfManagerPageCount_Substitute.RenderControl(e.Output);
            this.ErrorHiddenField_Managers_Substitute.RenderControl(e.Output);
        }

        private void Fill_cmbManagers_Substitute(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        { 
            string[] retMessage = new string[4];
            try
            {
                IList<Person> ManagerList = null;
                switch (Ls)
                {
                    case LoadState.Normal:
                        ManagerList = this.SubstituteBusiness.GetAllManager(pageIndex, pageSize);
                        break;
                    case LoadState.Search:
                        ManagerList = this.SubstituteBusiness.GetAllManager(SearchTerm, pageIndex, pageSize);
                        break;
                    case LoadState.AdvancedSearch:
                        ManagerList = this.SubstituteBusiness.GetAllManager(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                        break;
                }
                this.Fill_LookUpCmb_Substitute(this.cmbManagers_Substitute, ManagerList);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Managers_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Managers_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Managers_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void Fill_LookUpCmb_Substitute(ComboBox LookUpCmb, IList<Person> PersonnelList)
        {
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                PersonnelDetails personnelDetails = new PersonnelDetails();
                personnelDetails.ID = personItem.ID.ToString();
                personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                personCmbItem.Value = this.JsSeializer.Serialize(personnelDetails);
                LookUpCmb.Items.Add(personCmbItem);
            }
        }

        protected void CallBack_cmbPersonnel_Substitute_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel_Substitute.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_Substitute((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnel_Substitute((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_Substitute.RenderControl(e.Output);
            this.hfPersonnelPageCount_Substitute.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_Substitute.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnel_Substitute(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Person> PersonnelList = null;
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelList = this.PersonnelBusiness.GetAllPerson(pageIndex, pageSize);
                        break;
                    case LoadState.Search:
                        PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                        break;
                }
                this.Fill_LookUpCmb_Substitute(this.cmbPersonnel_Substitute, PersonnelList);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridSubstitutes_Substitute_onCallback(object sender, CallBackEventArgs e)
        {
            this.Fill_GridSubstitutes_Substitute((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_Substitute.RenderControl(e.Output);
            this.GridSubstitutes_Substitute.RenderControl(e.Output);
        }

        private void Fill_GridSubstitutes_Substitute(LoadState Ls, decimal ManagerPersonnelID, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Model.RequestFlow.Substitute> SubstitutesList = null; 
                switch (Ls)
                {
                    case LoadState.Normal:
                        SubstitutesList = this.SubstituteBusiness.GetAllByManager(ManagerPersonnelID, string.Empty);
                        break;
                    case LoadState.Search:
                        SubstitutesList = this.SubstituteBusiness.GetAllByManager(ManagerPersonnelID, SearchTerm);
                        break;
                }
                this.GridSubstitutes_Substitute.DataSource = SubstitutesList;
                this.GridSubstitutes_Substitute.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Substitute.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateSubstitute_SubstitutePage", "UpdateSubstitute_SubstitutePage_onCallBack", null, null)]
        public string[] UpdateSubstitute_SubstitutePage(string state, string SelectedSubstituteID, string ManagerID, string PersonnelID, string FromDate, string ToDate)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal SubstituteID = 0;
                decimal selectedSubstituteID = decimal.Parse(this.StringBuilder.CreateString(SelectedSubstituteID));
                decimal managerID = decimal.Parse(this.StringBuilder.CreateString(ManagerID));
                decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
                FromDate = this.StringBuilder.CreateString(FromDate);
                ToDate = this.StringBuilder.CreateString(ToDate);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Model.RequestFlow.Substitute substitute = new Model.RequestFlow.Substitute();
                substitute.ID = selectedSubstituteID;
                if (uam != UIActionType.DELETE)
                {
                    substitute.ManagerPersonId = managerID;
                    Person personnel = new Person();
                    personnel.ID = personnelID;
                    substitute.Person = personnel;
                    substitute.TheFromDate = FromDate;
                    substitute.TheToDate = ToDate;
                    substitute.Active = true;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        SubstituteID = this.SubstituteBusiness.InsertSubstitute(substitute, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedSubstituteID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoSubstituteSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        SubstituteID = this.SubstituteBusiness.UpdateSubstitute(substitute, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedSubstituteID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoSubstituteSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        SubstituteID = this.SubstituteBusiness.DeleteSubstitute(substitute, uam);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = SubstituteID.ToString();
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