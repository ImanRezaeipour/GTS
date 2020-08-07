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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class MasterLeaveRemains : GTSBasePage
    {
        public BRemainLeave LeaveRemainsBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BRemainLeave>();
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

        public JavaScriptSerializer JsSeializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
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

        enum Scripts
        {
            MasterLeaveRemains_onPageLoad,
            tbMasterLeaveRemains_TabStripMenus_Opeations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbPersonnel_MasterLeaveRemains.IsCallback && !CallBack_GridMasterLeaveRemains_MasterLeaveRemains.IsCallback)
            {
                Page MasterLeaveRemainsPage = this;
                Ajax.Utility.GenerateMethodScripts(MasterLeaveRemainsPage);

                this.CheckLeaveRemainsLoadAccess_MasterLeaveRemains();
                int currentYear = this.Fill_cmbYearCmbs_MasterLeaveRemains();
                this.SetPersonnelPageSize_cmbPersonnel_MasterLeaveRemains();
                this.SetPersonnelPageCount_cmbPersonnel_MasterLeaveRemains(LoadState.Normal, this.cmbPersonnel_MasterLeaveRemains.DropDownPageSize, string.Empty);
                this.SetLeaveRemainsPageSize_GridMasterLeaveRemains_MasterLeaveRemains();
                this.SetLeaveRemainsPageCount_GridMasterLeaveRemains_MasterLeaveRemains(LoadState.Normal, this.GridMasterLeaveRemains_MasterLeaveRemains.PageSize, 0, string.Empty, currentYear-1, currentYear);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckLeaveRemainsLoadAccess_MasterLeaveRemains()
        {
            string[] retMessage = new string[4];
            try
            {
                this.LeaveRemainsBusiness.CheckLeaveRemainsLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            } 
        }

        private int Fill_cmbYearCmbs_MasterLeaveRemains()
        {
            int CurrentYear = 0;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "en-US":
                    CurrentYear = DateTime.Now.Year;
                    break;
                case "fa-IR":
                    PersianCalendar pCal = new PersianCalendar();
                    CurrentYear = pCal.GetYear(DateTime.Now);
                    break;
            }
            for (int i = CurrentYear - 10; i <= (CurrentYear + 1); i++)
            {
                ComboBoxItem cmbItemYear = new ComboBoxItem(i.ToString());
                cmbItemYear.Value = i.ToString();
                this.cmbFromYear_MasterMonthlyOperation.Items.Add(cmbItemYear);
                this.cmbToYear_MasterMonthlyOperation.Items.Add(cmbItemYear);
                if (i == CurrentYear - 1)
                    this.cmbTransferFromYear_MasterLeaveRemains.Items.Add(cmbItemYear);
                if (i == CurrentYear)
                {
                    this.cmbTransferFromYear_MasterLeaveRemains.Items.Add(cmbItemYear);
                    this.cmbTransferToYear_MasterLeaveRemains.Items.Add(cmbItemYear);
                }
                if (i == CurrentYear + 1)
                    this.cmbTransferToYear_MasterLeaveRemains.Items.Add(cmbItemYear);
            }
            this.hfFromYear_MasterLeaveRemains.Value = (CurrentYear - 1).ToString();
            this.cmbFromYear_MasterMonthlyOperation.SelectedIndex = this.cmbFromYear_MasterMonthlyOperation.Items.Count - 3;
            this.hfToYear_MasterLeaveRemains.Value = CurrentYear.ToString();
            this.cmbToYear_MasterMonthlyOperation.SelectedIndex = this.cmbToYear_MasterMonthlyOperation.ItemCount - 2;
            this.cmbTransferFromYear_MasterLeaveRemains.SelectedIndex = this.cmbTransferToYear_MasterLeaveRemains.SelectedIndex = 1;
            return CurrentYear;
        }

        private void SetPersonnelPageSize_cmbPersonnel_MasterLeaveRemains()
        {
            this.hfPersonnelPageSize_MasterLeaveRemains.Value = this.cmbPersonnel_MasterLeaveRemains.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnel_MasterLeaveRemains(LoadState Ls, int pageSize, string SearchTerm)
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
                this.hfPersonnelCount_MasterLeaveRemains.Value = PersonnelCount.ToString();
                this.hfPersonnelPageCount_MasterLeaveRemains.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void SetLeaveRemainsPageSize_GridMasterLeaveRemains_MasterLeaveRemains()
        {
            this.hfLeaveRemainsPageSize_MasterLeaveRemains.Value = this.GridMasterLeaveRemains_MasterLeaveRemains.PageSize.ToString();
        }

        private void SetLeaveRemainsPageCount_GridMasterLeaveRemains_MasterLeaveRemains(LoadState Ls, int pageSize, decimal PersonnelID, string SearchTerm, int FromYear, int ToYear)
        {
            string[] retMessage = new string[4];
            int LeaveRemainsCount = 0;

            try
            {
                if (PersonnelID == -1)
                {
                    switch (Ls)
                    {
                        case LoadState.Normal:
                            LeaveRemainsCount = this.LeaveRemainsBusiness.GetRemainLeaveCount(FromYear, ToYear);
                            break;
                        case LoadState.Search:
                            LeaveRemainsCount = this.LeaveRemainsBusiness.GetRemainLeaveCount(SearchTerm, FromYear, ToYear);
                            break;
                        case LoadState.AdvancedSearch:
                            LeaveRemainsCount = this.LeaveRemainsBusiness.GetRemainLeaveCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), FromYear, ToYear);
                            break;
                        default:
                            break;
                    }
                }
                else
                    LeaveRemainsCount = this.LeaveRemainsBusiness.GetRemainLeaveCount(PersonnelID, FromYear, ToYear);

                this.hfLeaveRemainsCount_LeaveRemains.Value = LeaveRemainsCount.ToString();
                this.hfLeaveRemainsPageCount_MasterLeaveRemains.Value = Utility.GetPageCount(LeaveRemainsCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
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

        protected void CallBack_cmbPersonnel_MasterLeaveRemains_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel_MasterLeaveRemains.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_MasterLeaveRemains((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnel_MasterLeaveRemains((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.hfPersonnelCount_MasterLeaveRemains.RenderControl(e.Output);
            this.hfPersonnelPageCount_MasterLeaveRemains.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_MasterLeaveRemains.RenderControl(e.Output);
            this.cmbPersonnel_MasterLeaveRemains.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnel_MasterLeaveRemains(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                    this.cmbPersonnel_MasterLeaveRemains.Items.Add(personCmbItem);
                }
                this.cmbPersonnel_MasterLeaveRemains.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridMasterLeaveRemains_MasterLeaveRemains_onCallback(object sender, CallBackEventArgs e)
        {
            this.SetLeaveRemainsPageCount_GridMasterLeaveRemains_MasterLeaveRemains((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), int.Parse(this.StringBuilder.CreateString(e.Parameters[5])), int.Parse(this.StringBuilder.CreateString(e.Parameters[6])));
            this.Fill_GridMasterLeaveRemains_MasterLeaveRemains((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3])),  this.StringBuilder.CreateString(e.Parameters[4]), int.Parse(this.StringBuilder.CreateString(e.Parameters[5])), int.Parse(this.StringBuilder.CreateString(e.Parameters[6])));
            this.hfLeaveRemainsCount_LeaveRemains.RenderControl(e.Output);
            this.hfLeaveRemainsPageCount_MasterLeaveRemains.RenderControl(e.Output);
            this.ErrorHiddenField_MasterLeaveRemains.RenderControl(e.Output);
            this.GridMasterLeaveRemains_MasterLeaveRemains.RenderControl(e.Output);
        }

        private void Fill_GridMasterLeaveRemains_MasterLeaveRemains(LoadState Ls, int pageSize, int pageIndex, decimal PersonnelID, string SearchTerm, int FromYear, int ToYear)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<RemainLeaveProxy> RemainLeaveProxyList = null;
                if (PersonnelID == -1)
                {
                    switch (Ls)
                    {
                        case LoadState.Normal:
                            RemainLeaveProxyList = this.LeaveRemainsBusiness.GetRemainLeave(FromYear, ToYear, pageIndex, pageSize);
                            break;
                        case LoadState.Search:
                            RemainLeaveProxyList = this.LeaveRemainsBusiness.GetRemainLeave(SearchTerm, FromYear, ToYear, pageIndex, pageSize);
                            break;
                        case LoadState.AdvancedSearch:
                            RemainLeaveProxyList = this.LeaveRemainsBusiness.GetRemainLeave(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), FromYear, ToYear, pageIndex, pageSize);
                            break;
                    }
                }
                else
                    RemainLeaveProxyList = this.LeaveRemainsBusiness.GetRemainLeave(PersonnelID, FromYear, ToYear, pageIndex, pageSize);

                this.GridMasterLeaveRemains_MasterLeaveRemains.DataSource = RemainLeaveProxyList;
                this.GridMasterLeaveRemains_MasterLeaveRemains.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MasterLeaveRemains.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            } 
        }

        [Ajax.AjaxMethod("UpdateLeaveRemain_MasterLeaveRemainsPage", "UpdateLeaveRemain_MasterLeaveRemainsPage_onCallBcak", null, null)]
        public string[] UpdateLeaveRemain_MasterLeaveRemainsPage(string state, string SelectedLeaveRemainID, string PersonnelID, string Year, string RealDay, string RealHour, string ConfirmedDay, string ConfirmedHour, string PersonnelLoadState, string TransferFromYear, string TransferToYear, string PersonnelSearchTerm)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal LeaveRemainID = 0;
                decimal selectedLeaveRemainID = decimal.Parse(this.StringBuilder.CreateString(SelectedLeaveRemainID));
                decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
                int year = int.Parse(this.StringBuilder.CreateString(Year));
                RealDay = this.StringBuilder.CreateString(RealDay);
                RealHour = this.StringBuilder.CreateString(RealHour);
                ConfirmedDay = this.StringBuilder.CreateString(ConfirmedDay);
                ConfirmedHour = this.StringBuilder.CreateString(ConfirmedHour);
                LoadState PLS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(PersonnelLoadState));
                int transferFromYear = int.Parse(this.StringBuilder.CreateString(TransferFromYear));
                int transferToYear = int.Parse(this.StringBuilder.CreateString(TransferToYear));
                PersonnelSearchTerm = this.StringBuilder.CreateString(PersonnelSearchTerm);
                UIActionType uam = UIActionType.ADD;

                switch (this.StringBuilder.CreateString(state))
                {
                    case "Add":
                        uam = UIActionType.ADD;
                        LeaveRemainID = this.LeaveRemainsBusiness.InsertLeaveYear(year, personnelID, ConfirmedDay, ConfirmedHour);
                        break;
                    case "Edit":
                        uam = UIActionType.EDIT;
                        if (selectedLeaveRemainID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoLeaveRemainSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        LeaveRemainID = this.LeaveRemainsBusiness.UpdateLeaveYear(selectedLeaveRemainID, ConfirmedDay, ConfirmedHour);
                        break;
                    case "Transfer":
                        this.LeaveRemainsBusiness.TransferToNextYear();
                        if (personnelID == -1)
                        {
                            switch (PLS)
                            {
                                case LoadState.Normal:
                                    this.LeaveRemainsBusiness.TransferToNextYear(string.Empty, transferFromYear, transferToYear);
                                    break;
                                case LoadState.Search:
                                    this.LeaveRemainsBusiness.TransferToNextYear(PersonnelSearchTerm, transferFromYear, transferToYear);
                                    break;
                                case LoadState.AdvancedSearch:
                                    this.LeaveRemainsBusiness.TransferToNextYear(this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm), transferFromYear, transferToYear);
                                    break;
                            }
                        }
                        else
                            this.LeaveRemainsBusiness.TransferToNextYear(personnelID, transferFromYear, transferToYear);
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
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = LeaveRemainID.ToString();
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