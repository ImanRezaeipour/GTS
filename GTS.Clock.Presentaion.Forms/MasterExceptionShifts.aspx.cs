using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class MasterExceptionShifts : GTSBasePage
    {
        public BExceptionShift ExceptionShiftsBusiness
        {
            get
            {
               return BusinessHelper.GetBusinessInstance<BExceptionShift>();
            }
        }

        public ISearchPerson PersonnelBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }


        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }

        }

        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
        }

        enum Scripts
        {
            MasterExceptionShifts_onPageLoad,
            tbMasterExceptionShifts_TabStripMenus_Operations,
            Alert_Box,
            DropDownDive,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbPersonnel_MasterExceptionShifts.IsCallback && !CallBack_GridMasterExceptionShifts_MasterExceptionShifts.IsCallback)
            {
                Page MasterExceptionShiftsPage = this;
                Ajax.Utility.GenerateMethodScripts(MasterExceptionShiftsPage);

                this.CheckMasterExceptionShiftsLoadAccess_MasterExceptionShifts();
                this.ViewCurrentLangCalendars_MasterExceptionShifts();
                this.SetCurrentDate_MasterExceptionShifts();
                this.SetPersonnelPageSize_cmbPersonnel_MasterExceptionShifts();
                this.SetPersonnelPageCount_cmbPersonnel_MasterExceptionShifts(LoadState.Normal, this.cmbPersonnel_MasterExceptionShifts.DropDownPageSize, string.Empty);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckMasterExceptionShiftsLoadAccess_MasterExceptionShifts()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ExceptionShiftsBusiness.CheckExceptionShiftsLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            } 
        }

        private void ViewCurrentLangCalendars_MasterExceptionShifts()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "en-US":
                    this.Container_gdpMasterFromDate_MasterExceptionShifts.Visible = true;
                    this.Container_gdpMasterToDate_MasterExceptionShifts.Visible = true;
                    break;
                case "fa-IR":
                    this.Container_pdpMasterFromDate_MasterExceptionShifts.Visible = true;
                    this.Container_pdpMasterToDate_MasterExceptionShifts.Visible = true;
                    break;
            }
        }

        private void SetCurrentDate_MasterExceptionShifts()
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
            this.hfCurrentDate_MasterExceptionShifts.Value = strCurrentDate;
        }

        private void SetPersonnelPageSize_cmbPersonnel_MasterExceptionShifts()
        {
            this.hfPersonnelPageSize_MasterExceptionShifts.Value = this.cmbPersonnel_MasterExceptionShifts.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnel_MasterExceptionShifts(LoadState Ls, int pageSize, string SearchTerm)
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
                this.hfPersonnelCount_MasterExceptionShifts.Value = PersonnelCount.ToString();
                this.hfPersonnelPageCount_MasterExceptionShifts.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
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

        protected void CallBack_cmbPersonnel_MasterExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel_MasterExceptionShifts.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_MasterExceptionShifts((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnel_MasterExceptionShifts((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_MasterExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelCount_MasterExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelPageCount_MasterExceptionShifts.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_MasterExceptionShifts.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnel_MasterExceptionShifts(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                foreach (Person personItem in PersonnelList)
                {
                    ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                    personCmbItem["BarCode"] = personItem.BarCode;
                    personCmbItem["CardNum"] = personItem.CardNum;
                    personCmbItem.Id = personItem.ID.ToString();
                    this.cmbPersonnel_MasterExceptionShifts.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridMasterExceptionShifts_MasterExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridMasterExceptionShifts_MasterExceptionShifts(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_ExceptionShifts.RenderControl(e.Output);
            this.GridMasterExceptionShifts_MasterExceptionShifts.RenderControl(e.Output);
        }

        private void Fill_GridMasterExceptionShifts_MasterExceptionShifts(decimal PersonnelID, string FromDate, string ToDate)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<ShiftException> ExceptionShiftsList = PersonnelID != 0 ? this.ExceptionShiftsBusiness.GetExceptionShiftList(PersonnelID, FromDate, ToDate) : new List<ShiftException>();
                this.GridMasterExceptionShifts_MasterExceptionShifts.DataSource = ExceptionShiftsList;
                this.GridMasterExceptionShifts_MasterExceptionShifts.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateExceptionShift_MasterExceptionShiftsPage", "UpdateExceptionShift_MasterExceptionShiftsPage_onCallBack", null, null)]
        public string[] UpdateExceptionShift_MasterExceptionShiftsPage(string state, string SelectedExceptionShiftID)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            decimal selectedExceptionShiftID = decimal.Parse(this.StringBuilder.CreateString(SelectedExceptionShiftID));
            UIActionType uam = UIActionType.ADD;
            try
            {
                switch (this.StringBuilder.CreateString(state))
                {
                    case "Delete":
                        uam = UIActionType.DELETE;
                        if (selectedExceptionShiftID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoExceptionShiftSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        this.ExceptionShiftsBusiness.DeleteExceptionShift(selectedExceptionShiftID);
                        break;
                    default:
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
                retMessage[3] = selectedExceptionShiftID.ToString();
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