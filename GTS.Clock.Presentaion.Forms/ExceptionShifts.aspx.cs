using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using ComponentArt.Web.UI;
using NHibernate.Mapping;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ExceptionShifts : GTSBasePage
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

        public enum ReplacementState
        {
            NotReplacement,
            Personnel,
            WorkGroup
        }

        public enum ShiftViewState
        {
            NotView,
            First,
            Second
        }

        public enum OperationState
        {
            Add,
            Edit,
            TwoDayReplacement,
            TwoPersonnelReplacement
        }

        enum Scripts
        {
            ExceptionShifts_onPageLoad,
            DialogExceptionShifts_Operations,
            DialogShiftsView_onPageLoad,
            DropDownDive,
            Alert_Box,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.IsCallback && !CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts.IsCallback && !CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.IsCallback && !CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.IsCallback && !CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.IsCallback && !CallBack_cmbShift_ExceptionShifts.IsCallback && !CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts.IsCallback)
            {
                Page ExceptionShiftsPage = this;
                Ajax.Utility.GenerateMethodScripts(ExceptionShiftsPage);

                this.ViewCurrentLangCalendars_ExceptionShifts();
                this.SetCurrentDate_ExceptionShifts();
                this.SetPersonnelPageSize_cmbPersonnelControls_ExceptionShifts(this.cmbPersonnel_TwoDayReplacement_ExceptionShifts, this.hfPersonnelPageSize_TwoDayReplacement_ExceptionShifts);
                this.SetPersonnelPageCount_cmbPersonnelControls_ExceptionShifts(this.hfPersonnelPageCount_TwoDayReplacement_ExceptionShifts, this.hfPersonnelPageCount_TwoDayReplacement_ExceptionShifts, this.ErrorHiddenField_TwoDayReplacement_ExceptionShifts, LoadState.Normal, this.cmbPersonnel_TwoDayReplacement_ExceptionShifts.DropDownPageSize, string.Empty);
                this.SetPersonnelPageSize_cmbPersonnelControls_ExceptionShifts(this.cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts, this.hfPersonnelPageSize_Personnel1_TwoPersonnelReplacement_ExceptionShifts);
                this.SetPersonnelPageCount_cmbPersonnelControls_ExceptionShifts(this.hfPersonnelCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts, this.hfPersonnelPageCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts, this.ErrorHiddenField_Personnel1_TwoPersonnelReplacement_ExceptionShifts, LoadState.Normal, this.cmbPersonnel_TwoDayReplacement_ExceptionShifts.DropDownPageSize, string.Empty);
                this.SetPersonnelPageSize_cmbPersonnelControls_ExceptionShifts(this.cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts, this.hfPersonnelPageSize_Personnel2_TwoPersonnelReplacement_ExceptionShifts);
                this.SetPersonnelPageCount_cmbPersonnelControls_ExceptionShifts(this.hfPersonnelCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts, this.hfPersonnelPageCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts, this.ErrorHiddenField_Personnel2_TwoPersonnelReplacement_ExceptionShifts, LoadState.Normal, this.cmbPersonnel_TwoDayReplacement_ExceptionShifts.DropDownPageSize, string.Empty);
                this.InitializeSkin();
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void ViewCurrentLangCalendars_ExceptionShifts()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpDetailsFromDate_ExceptionShifts.Visible = true;
                    this.Container_pdpDetailsToDate_ExceptionShifts.Visible = true;
                    this.Container_pdpFromDate_TwoDayReplacement_ExceptionShifts.Visible = true;
                    this.Container_pdpToDate_TwoDayReplacement_ExceptionShifts.Visible = true;
                    this.Container_pdpTwoPersonnelReplacement1_ExceptionShifts.Visible = true;
                    this.Container_pdpTwoPersonnelReplacement2_ExceptionShifts.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpDetailsFromDate_ExceptionShifts.Visible = true;
                    this.Container_gdpDetailsToDate_ExceptionShifts.Visible = true;
                    this.Container_gdpFromDate_TwoDayReplacement_ExceptionShifts.Visible = true;
                    this.Container_gdpToDate_TwoDayReplacement_ExceptionShifts.Visible = true;
                    this.Container_gdpTwoPersonnelReplacement1_ExceptionShifts.Visible = true;
                    this.Container_gdpTwoPersonnelReplacement2_ExceptionShifts.Visible = true;
                    break;
            }
        }

        private void InitializeSkin()
        {
            SkinHelper.InitializeSkin(this.Page);
            SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripExceptionShifts);
        }

        private void SetCurrentDate_ExceptionShifts()
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
            this.hfCurrentDate_ExceptionShifts.Value = strCurrentDate;
        }

        private void SetPersonnelPageSize_cmbPersonnelControls_ExceptionShifts(ComponentArt.Web.UI.ComboBox cmbPersonnel, HiddenField hfPersonnelPageSize)
        {
            hfPersonnelPageSize.Value = cmbPersonnel.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnelControls_ExceptionShifts(HiddenField hfPersonnelCount, HiddenField hfPersonnelPageCount, HiddenField ErrorHiddenField, LoadState Ls, int pageSize, string SearchTerm)
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
                hfPersonnelPageCount.Value = PersonnelCount.ToString();
                hfPersonnelPageCount.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
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

        protected void CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel_TwoDayReplacement_ExceptionShifts.Dispose();
            this.SetPersonnelPageCount_cmbPersonnelControls_ExceptionShifts(this.hfPersonnelCount_TwoDayReplacement_ExceptionShifts, this.hfPersonnelPageCount_TwoDayReplacement_ExceptionShifts, this.ErrorHiddenField_TwoDayReplacement_ExceptionShifts, (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnelControls_ExceptionShifts(this.cmbPersonnel_TwoDayReplacement_ExceptionShifts, this.ErrorHiddenField_TwoDayReplacement_ExceptionShifts, (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_TwoDayReplacement_ExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelCount_TwoDayReplacement_ExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelPageCount_TwoDayReplacement_ExceptionShifts.RenderControl(e.Output);
            this.ErrorHiddenField_TwoDayReplacement_ExceptionShifts.RenderControl(e.Output);
        }

        protected void CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.Dispose();
            this.SetPersonnelPageCount_cmbPersonnelControls_ExceptionShifts(this.hfPersonnelCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts, this.hfPersonnelPageCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts, this.ErrorHiddenField_Personnel1_TwoPersonnelReplacement_ExceptionShifts, (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnelControls_ExceptionShifts(this.cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts, ErrorHiddenField_Personnel1_TwoPersonnelReplacement_ExceptionShifts, (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelPageCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel1_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
        }


        protected void CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.Dispose();
            this.SetPersonnelPageCount_cmbPersonnelControls_ExceptionShifts(this.hfPersonnelCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts, this.hfPersonnelPageCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts, this.ErrorHiddenField_Personnel2_TwoPersonnelReplacement_ExceptionShifts, (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnelControls_ExceptionShifts(this.cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts, ErrorHiddenField_Personnel2_TwoPersonnelReplacement_ExceptionShifts, (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.hfPersonnelPageCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel2_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnelControls_ExceptionShifts(ComponentArt.Web.UI.ComboBox cmbPersonnel, HiddenField ErrorHiddenField, LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                    cmbPersonnel.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbShift_ExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbShift_ExceptionShifts.Dispose();
            this.Fill_cmbShift_ExceptionShifts();
            this.ErrorHiddenField_Shifts.RenderControl(e.Output);
            this.cmbShift_ExceptionShifts.RenderControl(e.Output);
        }

        private void Fill_cmbShift_ExceptionShifts()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Shift> ShiftsList = this.ExceptionShiftsBusiness.GetShiftList();
                this.cmbShift_ExceptionShifts.DataSource = ShiftsList;
                this.cmbShift_ExceptionShifts.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Shifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Shifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Shifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbWorkGroup_TwoDayReplacement_ExceptionShifts.Dispose();
            this.Fill_cmbWorkGroupControls_ExceptionShifts(this.cmbWorkGroup_TwoDayReplacement_ExceptionShifts, this.ErrorHiddenField_WorkGroups_TwoDayReplacement_ExceptionShifts);
            this.ErrorHiddenField_WorkGroups_TwoDayReplacement_ExceptionShifts.RenderControl(e.Output);
            this.cmbWorkGroup_TwoDayReplacement_ExceptionShifts.RenderControl(e.Output);
        }

        protected void CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.Dispose();
            this.Fill_cmbWorkGroupControls_ExceptionShifts(this.cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts, this.ErrorHiddenField_Personnel1_TwoPersonnelReplacement_ExceptionShifts);
            this.ErrorHiddenField_FirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
        }

        protected void CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.Dispose();
            this.Fill_cmbWorkGroupControls_ExceptionShifts(this.cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts, this.ErrorHiddenField_Personnel2_TwoPersonnelReplacement_ExceptionShifts);
            this.ErrorHiddenField_SecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
            this.cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.RenderControl(e.Output);
        }

        private void Fill_cmbWorkGroupControls_ExceptionShifts(ComponentArt.Web.UI.ComboBox cmbWorkGroup, HiddenField ErrorHiddenField)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<WorkGroup> WorkGroupsList = this.ExceptionShiftsBusiness.GetAllWorkGroups();
                cmbWorkGroup.DataSource = WorkGroupsList;
                cmbWorkGroup.DataBind();
                cmbWorkGroup.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                ErrorHiddenField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateExceptionShift_ExceptionShiftsPage", "UpdateExceptionShift_ExceptionShiftsPage_onCallBack", null, null)]
        public string[] UpdateExceptionShift_ExceptionShiftsPage(string operationState, string ShiftID, string FirstPersonnelID, string SecondPersonnelID, string FirstWorkGroupID, string SecondWorkGroupID, string FirstDate, string SecondDate, string replacementState, string shiftViewState)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];
            
            try
            {
                OperationState OS = (OperationState)Enum.Parse(typeof(OperationState), this.StringBuilder.CreateString(operationState));
                decimal shiftID = decimal.Parse(this.StringBuilder.CreateString(ShiftID));
                decimal firstPersonnelID = decimal.Parse(this.StringBuilder.CreateString(FirstPersonnelID));
                decimal secondPersonnelID = decimal.Parse(this.StringBuilder.CreateString(SecondPersonnelID));
                decimal firstWorkGroupID = decimal.Parse(this.StringBuilder.CreateString(FirstWorkGroupID));
                decimal secondWorkGroupID = decimal.Parse(this.StringBuilder.CreateString(SecondWorkGroupID));
                FirstDate = this.StringBuilder.CreateString(FirstDate);
                SecondDate = this.StringBuilder.CreateString(SecondDate);
                ReplacementState RS = (ReplacementState)Enum.Parse(typeof(ReplacementState), this.StringBuilder.CreateString(replacementState));
                ShiftViewState SVS = (ShiftViewState)Enum.Parse(typeof(ShiftViewState), this.StringBuilder.CreateString(shiftViewState));
                string shift = string.Empty;
                string SuccessMessageBody = string.Empty;

                switch (OS)
                {
                    case OperationState.Add:
                        this.ExceptionShiftsBusiness.InsertExceptionShift(firstPersonnelID, shiftID, FirstDate, SecondDate);
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case OperationState.Edit:
                        this.ExceptionShiftsBusiness.UpdateExceptionShift(firstPersonnelID, shiftID, FirstDate, SecondDate);
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case OperationState.TwoDayReplacement:
                        switch (SVS)
                        {
                            case ShiftViewState.NotView:
                                switch (RS)
                                {
                                    case ReplacementState.NotReplacement:
                                        break;
                                    case ReplacementState.Personnel:
                                        this.ExceptionShiftsBusiness.ExchangeDayByPerson(firstPersonnelID, FirstDate, SecondDate);
                                        break;
                                    case ReplacementState.WorkGroup:
                                        this.ExceptionShiftsBusiness.ExchangeDayByWorkGroup(firstWorkGroupID, FirstDate, SecondDate);
                                        break;
                                }
                                SuccessMessageBody = GetLocalResourceObject("TwoDayReplacementComplete").ToString();
                                break;
                            case ShiftViewState.First:
                                switch (RS)
                                {
                                    case ReplacementState.NotReplacement:
                                        break;
                                    case ReplacementState.Personnel:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByPersonId(firstPersonnelID, FirstDate);
                                        break;
                                    case ReplacementState.WorkGroup:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByWorkGroup(firstWorkGroupID, FirstDate);
                                        break;
                                }
                                break;
                            case ShiftViewState.Second:
                                switch (RS)
                                {
                                    case ReplacementState.NotReplacement:
                                        break;
                                    case ReplacementState.Personnel:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByPersonId(firstPersonnelID, SecondDate);
                                        break;
                                    case ReplacementState.WorkGroup:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByWorkGroup(firstWorkGroupID, SecondDate);
                                        break;
                                }
                                break;
                        }
                        break;
                    case OperationState.TwoPersonnelReplacement:
                        switch (SVS)
                        {
                            case ShiftViewState.NotView:
                                switch (RS)
                                {
                                    case ReplacementState.NotReplacement:
                                        break;
                                    case ReplacementState.Personnel:
                                        this.ExceptionShiftsBusiness.ExchangePerson(firstPersonnelID, secondPersonnelID, FirstDate, SecondDate);
                                        break;
                                    case ReplacementState.WorkGroup:
                                        this.ExceptionShiftsBusiness.ExchangeWorkGroup(firstWorkGroupID, secondWorkGroupID, FirstDate, SecondDate);
                                        break;
                                }
                                SuccessMessageBody = GetLocalResourceObject("TwoPersonnelReplacementComplete").ToString();
                                break;
                            case ShiftViewState.First:
                                switch (RS)
                                {
                                    case ReplacementState.NotReplacement:
                                        break;
                                    case ReplacementState.Personnel:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByPersonId(firstPersonnelID, FirstDate);
                                        break;
                                    case ReplacementState.WorkGroup:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByWorkGroup(firstWorkGroupID, FirstDate);
                                        break;
                                }
                                break;
                            case ShiftViewState.Second:
                                switch (RS)
                                {
                                    case ReplacementState.NotReplacement:
                                        break;
                                    case ReplacementState.Personnel:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByPersonId(secondPersonnelID, SecondDate);
                                        break;
                                    case ReplacementState.WorkGroup:
                                        shift = this.ExceptionShiftsBusiness.GetDayShiftByWorkGroup(secondWorkGroupID, SecondDate);
                                        break;
                                }
                                break;
                        }
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                if (SVS != ShiftViewState.NotView)
                    retMessage[3] = shift != string.Empty ? shift : GetLocalResourceObject("NoShift").ToString();
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

