using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using System.Data;
using System.Configuration;
using System.Collections;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Business.GridSettings;
using GTS.Clock.Model.UI;
using System.Reflection;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Leave;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class MonthlyOperationGridSchema : GTSBasePage
    {
        string LngID;

        public BGridMonthlyOperationClientSettings MonthlyOperationGridClientSettingsBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BGridMonthlyOperationClientSettings>();                
            }
        }

        internal class DateRangeDetails
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Order { get; set; }
        }

        public class SummaryMonthlyOperation
        {
            public string AllowableOverTime { get; set; }
            public string DailyAbsence { get; set; }
            public string DailyMeritoriouslyLeave { get; set; }
            public string DailyMission { get; set; }
            public string DailyPureOperation { get; set; }
            public string DailySickLeave { get; set; }
            public string DailyWithoutPayLeave { get; set; }
            public string DailyWithPayLeave { get; set; }
            public string HostelryMission { get; set; }
            public string HourlyAllowableAbsence { get; set; }
            public string HourlyMeritoriouslyLeave { get; set; }
            public string HourlyMission { get; set; }
            public string HourlyPureOperation { get; set; }
            public string HourlySickLeave { get; set; }
            public string HourlyUnallowableAbsence { get; set; }
            public string HourlyWithoutPayLeave { get; set; }
            public string ImpureOperation { get; set; }
            public string NecessaryOperation { get; set; }
            public string PresenceDuration { get; set; }
            public string UnallowableOverTime { get; set; }
            public string ReserveField1 { get; set; }
            public string ReserveField2 { get; set; }
            public string ReserveField3 { get; set; }
            public string ReserveField4 { get; set; }
            public string ReserveField5 { get; set; }
            public string ReserveField6 { get; set; }
            public string ReserveField7 { get; set; }
            public string ReserveField8 { get; set; }
            public string ReserveField9 { get; set; }
            public string ReserveField10 { get; set; }
        }

        enum LoadState
        {
            None,
            Manager,
            Personnel
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

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        internal class CurrentUserObj
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string PersonnelID { get; set; }
            public string PersonnelName { get; set; }
        }

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        public OperationYearMonthProvider operationYearMonthProvider
        {
            get
            {
                return new OperationYearMonthProvider();
            }
        }

        enum Scripts
        {
            MonthlyOperationGridSchema_onPageLoad,
            Alert_Box,
            DialogMonthlyOperationGridSchema_Operations,
            DialogRequestOnTraffic_onPageLoad,
            DialogHourlyRequestOnAbsence_onPageLoad,
            DialogDailyRequestOnAbsence_onPageLoad,
            DialogRequestOnUnallowableOverTime_onPageLoad,
            DialogOvertimeJustificationRequest_onPageLoad,
            DialogUserInformation_onPageLoad,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation.IsCallback && !this.CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation.IsCallback && !this.CallBack_GridSettings_MasterMonthlyOperation.IsCallback && !this.CallBack_cmbMonth_MasterMonthlyOperation.IsCallback)
            {
                Page MasterMonthlyOperationPage = this;

                this.CheckMonthlyOperationGridSchemaLoadAccess_MonthlyOperationGridSchema();
                this.SetCurrentUser_MasterMonthlyOperation();
                this.ClearSessions_MasterMonthlyOperation();
                this.Fill_cmbYear_MasterMonthlyOperation();
                this.CustomizeTlbMasterMonthlyOperation_MasterMonthlyOperation();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
            else
                if (this.GridMasterMonthlyOperation_MasterMonthlyOperation.CausedCallback)
                {
                    Dictionary<string, object> ParametersDic = this.GetParameters_GridMasterMonthlyOperation_MasterMonthlyOperation();
                    if (ParametersDic.Count > 0)
                        this.Fill_GridMasterMonthlyOperation_MasterMonthlyOperation((LoadState)ParametersDic["LoadState"], (decimal)ParametersDic["PersonnelID"], (int)ParametersDic["Year"], (int)ParametersDic["Month"], ParametersDic["FromDate"].ToString(), ParametersDic["ToDate"].ToString());
                }
            this.CheckMonthlyOperationGridSchemaDetailsRowsAccess_MonthlyOperationGridSchema();


            GridMasterMonthlyOperation_MasterMonthlyOperation.NeedRebind += new Grid.NeedRebindEventHandler(GridMasterMonthlyOperation_MasterMonthlyOperation_NeedRebind);
            GridMasterMonthlyOperation_MasterMonthlyOperation.NeedDataSource += new Grid.NeedDataSourceEventHandler(GridMasterMonthlyOperation_MasterMonthlyOperation_NeedDataSource);
            GridMasterMonthlyOperation_MasterMonthlyOperation.NeedChildDataSource += new Grid.NeedChildDataSourceEventHandler(GridMasterMonthlyOperation_MasterMonthlyOperation_NeedChildDataSource);
        }

        private void CheckMonthlyOperationGridSchemaLoadAccess_MonthlyOperationGridSchema()
        {
            string[] retMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PID") && HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
                {
                    BPersonMonthlyWorkedTime MonthlyOperationBusiness = BusinessHelper.GetBusinessInstance<BPersonMonthlyWorkedTime>(new KeyValuePair<string, object>("personId", decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PID"]))));
                    LoadState LS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));
                    switch (LS)
                    {
                        case LoadState.Manager:
                            MonthlyOperationBusiness.CheckMonthlyOperationGridSchemaLoadAccess_onManagerState();
                            break;
                        case LoadState.Personnel:
                            MonthlyOperationBusiness.CheckMonthlyOperationGridSchemaLoadAccess_onPersonnelState();
                            break;
                    }
                }
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        private void CheckMonthlyOperationGridSchemaDetailsRowsAccess_MonthlyOperationGridSchema()
        {
            string[] retMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PID") && HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
                {
                    BPersonMonthlyWorkedTime MonthlyOperationBusiness = BusinessHelper.GetBusinessInstance<BPersonMonthlyWorkedTime>(new KeyValuePair<string, object>("personId", decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PID"]))));
                    LoadState LS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));
                    switch (LS)
                    {
                        case LoadState.Manager:
                            break;
                        case LoadState.Personnel:
                            MonthlyOperationBusiness.CheckMonthlyOperationGridSchemaDetailsRowsAccess_onPersonnelState();
                            break;
                    }
                }
            }
            catch (BaseException ex)
            {
                this.GridMasterMonthlyOperation_MasterMonthlyOperation.Levels.RemoveAt(1);
            }
        }


        private void CustomizeTlbMasterMonthlyOperation_MasterMonthlyOperation()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
            {
                LoadState LS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));
                switch (LS)
                {
                    case LoadState.Manager:
                        this.TlbMasterMonthlyOperation.Items[0].Visible = false;
                        this.TlbMasterMonthlyOperation.Items[2].Visible = false;
                        this.TlbMasterMonthlyOperation.Items[3].Visible = false;
                        break;
                    case LoadState.Personnel:
                        break;
                }
            }
        }

        private void SetCurrentUser_MasterMonthlyOperation()
        {
            CurrentUserObj currentUserObj = new CurrentUserObj();
            currentUserObj.UserID = BUser.CurrentUser.ID.ToString();
            currentUserObj.UserName = BUser.CurrentUser.UserName;
            currentUserObj.PersonnelID = BUser.CurrentUser.Person.ID.ToString();
            currentUserObj.PersonnelName = BUser.CurrentUser.Person.Name;

            this.hfCurrentUser_MasterMonthlyOperation.Value = this.JsSerializer.Serialize(currentUserObj);
        }

        private void ClearSessions_MasterMonthlyOperation()
        {
            Session["Year_MasterMonthlyOperation"] = null;
            Session["Month_MasterMonthlyOperation"] = null;
            Session["MonthlyOperationSource_MasterMonthlyOperation"] = null;
        }

        private Dictionary<string, object> GetParameters_GridMasterMonthlyOperation_MasterMonthlyOperation()
        {
            Dictionary<string, object> ParametersDic = new Dictionary<string, object>();
            LoadState LS = LoadState.Personnel;
            decimal PersonnelID = -1;
            int Year = -1;
            int Month = -1;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
            {
                LS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));
                ParametersDic.Add("LoadState", LS);
            }
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PersonnelID"))
            {
                PersonnelID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PersonnelID"]));
                switch (LS)
                {
                    case LoadState.Manager:
                        break;
                    case LoadState.Personnel:
                        PersonnelID = BUser.CurrentUser.Person.ID;
                        break;
                }
                ParametersDic.Add("PersonnelID", PersonnelID);
            }
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Year"))
            {
                Year = int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Year"]));
                ParametersDic.Add("Year", Year);
            }
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Month"))
            {
                Month = int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Month"]));
                ParametersDic.Add("Month", Month);
            }
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("FromDate"))
            {
                FromDate = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["FromDate"]);
                ParametersDic.Add("FromDate", FromDate);
            }
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ToDate"))
            {
                ToDate = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ToDate"]);
                ParametersDic.Add("ToDate", Month);
            }
            return ParametersDic;
        }

        private void Fill_cmbYear_MasterMonthlyOperation()
        {
            this.operationYearMonthProvider.GetOperationYear(this.cmbYear_MasterMonthlyOperation, this.hfCurrentYear_MasterMonthlyOperation);
        }

        protected void CallBack_cmbMonth_MasterMonthlyOperation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMonth_MasterMonthlyOperation.Dispose();
            this.Fill_cmbMonth_MasterMonthlyOperation((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2])));
            this.hfCurrentMonth_MasterMonthlyOperation.RenderControl(e.Output);
            this.ErrorHiddenField_Months_MasterMonthlyOperation.RenderControl(e.Output);
            this.cmbMonth_MasterMonthlyOperation.RenderControl(e.Output);
        }

        private void Fill_cmbMonth_MasterMonthlyOperation(LoadState LS, int Year, decimal PersonnelID)
        {
            string[] retMessage = new string[4];
            try
            {
                string CurrentLangID = string.Empty;
                string SysLangID = string.Empty;
                string Identifier = string.Empty;
                int month = 0;
                DateTime currentDateTime = DateTime.Now;
                CurrentLangID = this.LangProv.GetCurrentLanguage();
                SysLangID = this.LangProv.GetCurrentSysLanguage();
                switch (SysLangID)
                {
                    case "en-US":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "ee";
                                break;
                            case "fa-IR":
                                Identifier = "ef";
                                break;
                        }
                        month = currentDateTime.Month;
                        break;
                    case "fa-IR":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "fe";
                                break;
                            case "fa-IR":
                                Identifier = "ff";
                                break;
                        }
                        PersianCalendar pCal = new PersianCalendar();
                        month = pCal.GetMonth(currentDateTime);
                        break;
                }
                switch (LS)
                {
                    case LoadState.Manager:
                        break;
                    case LoadState.Personnel:
                        PersonnelID = BUser.CurrentUser.Person.ID;
                        break;
                }
                //DateRangeHelper drh = new DateRangeHelper();
                //IList<DateRangeHelper.DateRangeOrderProxy> DateRangeOrderProxyList = drh.GetDateRangeOrder(Year);
                BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(PersonnelID);
                IList<DateRangeOrderProxy> DateRangeOrderProxyList = MonthlyOperationBusiness.GetDateRangeOrder(Year);
                DateRangeDetails dateRangeDetails = new DateRangeDetails();
                for (int i = 1; i <= DateRangeOrderProxyList.Count; i++)
                {
                    dateRangeDetails.FromDate = DateRangeOrderProxyList[i - 1].FromDate;
                    dateRangeDetails.ToDate = DateRangeOrderProxyList[i - 1].ToDate;
                    dateRangeDetails.Order = DateRangeOrderProxyList[i - 1].Order.ToString();

                    ComboBoxItem cmbItemMonth = new ComboBoxItem(GetLocalResourceObject("Month" + i + "" + Identifier + "").ToString());
                    cmbItemMonth.Value = this.JsSerializer.Serialize(dateRangeDetails);
                    this.cmbMonth_MasterMonthlyOperation.Items.Add(cmbItemMonth);
                    if (DateRangeOrderProxyList[i - 1].Selected && Session["CurrentOperationMonth"] == null)
                    {
                        this.cmbMonth_MasterMonthlyOperation.SelectedIndex = i - 1;
                        this.hfCurrentMonth_MasterMonthlyOperation.Value = cmbItemMonth.Value;
                    }                    
                }
                if (Session["CurrentOperationMonth"] != null)
                {
                    OperationYearMonthProvider.OperationMonthObj operationMonthObj = (OperationYearMonthProvider.OperationMonthObj)Session["CurrentOperationMonth"];
                    dateRangeDetails.FromDate = DateRangeOrderProxyList[operationMonthObj.Index].FromDate;
                    dateRangeDetails.ToDate = DateRangeOrderProxyList[operationMonthObj.Index].ToDate;
                    dateRangeDetails.Order = DateRangeOrderProxyList[operationMonthObj.Index].Order.ToString();
                    this.cmbMonth_MasterMonthlyOperation.Items[operationMonthObj.Index].Value = this.JsSerializer.Serialize(dateRangeDetails);
                    this.hfCurrentMonth_MasterMonthlyOperation.Value = this.cmbMonth_MasterMonthlyOperation.Items[operationMonthObj.Index].Value;
                    this.cmbMonth_MasterMonthlyOperation.SelectedIndex = operationMonthObj.Index;
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Months_MasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Months_MasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Months_MasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        public void GridMasterMonthlyOperation_MasterMonthlyOperation_NeedChildDataSource(object sender, GridNeedChildDataSourceEventArgs e)
        {
            FillChilds_GridMasterMonthlyOperation_MasterMonthlyOperation(e);
        }

        private void FillChilds_GridMasterMonthlyOperation_MasterMonthlyOperation(GridNeedChildDataSourceEventArgs e)
        {
            if (e.Item.Level == 0)
            {
                Dictionary<string, object> obj = this.GetParameters_GridMasterMonthlyOperation_MasterMonthlyOperation();
                LoadState LS = (LoadState)obj["LoadState"];
                decimal PersonnelID = (decimal)obj["PersonnelID"];
                switch (LS)
                {
                    case LoadState.Manager:
                        break;
                    case LoadState.Personnel:
                        PersonnelID = BUser.CurrentUser.Person.ID;
                        break;
                }
                BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(PersonnelID);
                IList<MonthlyDetailReportProxy> MonthlyDetailReportProxyList = MonthlyOperationBusiness.GetPersonMonthlyRowDetail((DateTime)e.Item["Date"]);
                e.DataSource = MonthlyDetailReportProxyList;
            }
        }

        private void BuildGrid_MasterMonthlyOperation(ComponentArt.Web.UI.Grid grid)
        {
            this.SetGridColumnsSize_MasterMonthlyOperation(grid);
            this.SetVisibleColumns_MasterMonthlyOperation(grid);
            this.SetReserveFieldsHeaderColumnsCaption_MasterMonthlyOperation(grid);
        }

        public void GridMasterMonthlyOperation_MasterMonthlyOperation_NeedDataSource(object sender, EventArgs e)
        {
            Dictionary<string, object> ParametersDic = this.GetParameters_GridMasterMonthlyOperation_MasterMonthlyOperation();
            if (ParametersDic.Count > 0 && ParametersDic.ContainsKey("PersonnelID"))
                this.Fill_GridMasterMonthlyOperation_MasterMonthlyOperation((LoadState)ParametersDic["LoadState"], (decimal)ParametersDic["PersonnelID"], (int)ParametersDic["FromDate"], (int)ParametersDic["Month"], ParametersDic["FromDate"].ToString(), ParametersDic["ToDate"].ToString());
        }

        public void GridMasterMonthlyOperation_MasterMonthlyOperation_NeedRebind(object sender, EventArgs e)
        {
            GridMasterMonthlyOperation_MasterMonthlyOperation.DataBind();
        }

        private void Fill_GridMasterMonthlyOperation_MasterMonthlyOperation(LoadState LS, decimal PersonnelID, int Year, int Month, string FromDate, string ToDate)
        {
            string[] retMessage = new string[4];
            IList<PersonalMonthlyReportRow> PersonnelMonthlyOperationList = null;
            PersonalMonthlyReportRow PersonnelSummaryMonthlyOperation = null;
            try
            {
                if (Session["Year_MasterMonthlyOperation"] != null)
                    if ((int)Session["Year_MasterMonthlyOperation"] != Year)
                        Session["MonthlyOperationSource_MasterMonthlyOperation"] = null;
                if (Session["Month_MasterMonthlyOperation"] != null)
                    if ((int)Session["Month_MasterMonthlyOperation"] != Month)
                        Session["MonthlyOperationSource_MasterMonthlyOperation"] = null;
                if (Session["MonthlyOperationSource_MasterMonthlyOperation"] == null)
                {
                    switch (LS)
                    {
                        case LoadState.Manager:
                            break;
                        case LoadState.Personnel:
                            PersonnelID = BUser.CurrentUser.Person.ID;
                            break;
                    }

                    BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(PersonnelID);
                    MonthlyOperationBusiness.GetPersonMonthlyReport(Year, Month, FromDate, ToDate, out PersonnelMonthlyOperationList, out PersonnelSummaryMonthlyOperation);
                    //DateRangeHelper drh = new DateRangeHelper();
                    //drh.GetPersonMonthlyReport(Year, Month, FromDate, ToDate, out PersonnelMonthlyOperationList, out PersonnelSummaryMonthlyOperation);                    

                    Session.Add("Year_MasterMonthlyOperation", Year);
                    Session.Add("Month_MasterMonthlyOperation", Month);
                    Dictionary<string, object> MonthlyOperationSourceDic = new Dictionary<string, object>();
                    MonthlyOperationSourceDic.Add("Details", PersonnelMonthlyOperationList);
                    MonthlyOperationSourceDic.Add("Summary", PersonnelSummaryMonthlyOperation);
                    Session.Add("MonthlyOperationSource_MasterMonthlyOperation", MonthlyOperationSourceDic);
                }
                this.operationYearMonthProvider.SetOperationYearMonth(Year, Month);
                GridMasterMonthlyOperation_MasterMonthlyOperation.DataSource = (IList<PersonalMonthlyReportRow>)((Dictionary<string, object>)Session["MonthlyOperationSource_MasterMonthlyOperation"])["Details"];
                GridMasterMonthlyOperation_MasterMonthlyOperation.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void Fill_GridSummaryMonthlyOperation_MasterMonthlyOperation()
        {
            if (Session["MonthlyOperationSource_MasterMonthlyOperation"] != null)
            {
                PersonalMonthlyReportRow personalMonthlyReportRow = (PersonalMonthlyReportRow)((Dictionary<string, object>)Session["MonthlyOperationSource_MasterMonthlyOperation"])["Summary"];
                SummaryMonthlyOperation summaryMonthlyOperation = new SummaryMonthlyOperation();
                IList<SummaryMonthlyOperation> SummaryMonthLyOperationList = new List<SummaryMonthlyOperation>();
                GridColumnCollection Gcc = GridSummaryMonthlyOperation_MasterMonthlyOperation.Levels[0].Columns;
                for (int i = 1; i < Gcc.Count; i++)
                {
                    PropertyInfo PInfo = typeof(SummaryMonthlyOperation).GetProperty(Gcc[i].DataField);
                    if (PInfo != null)
                        PInfo.SetValue(summaryMonthlyOperation, (string)typeof(PersonalMonthlyReportRow).GetProperty("Periodic" + Gcc[i].DataField).GetValue(personalMonthlyReportRow, null), null);
                }
                SummaryMonthLyOperationList.Add(summaryMonthlyOperation);
                this.GridSummaryMonthlyOperation_MasterMonthlyOperation.DataSource = SummaryMonthLyOperationList;
                this.GridSummaryMonthlyOperation_MasterMonthlyOperation.DataBind();
            }
        }

        protected override void InitializeCulture()
        {
            LngID = this.LangProv.GetCurrentLanguage();
            this.SetCurrentCultureResObjs(LngID);
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        private void SetGridColumnsSize_MasterMonthlyOperation(ComponentArt.Web.UI.Grid grid)
        {
            MonthlyOperationGridClientGeneralSettings monthlyOperationGridClientGeneralSettings = this.MonthlyOperationGridClientSettingsBusiness.GetMonthlyOperationGridGeneralClientSettings();
            GridColumnCollection Gcc = grid.Levels[0].Columns;
            for (int i = 1; i < Gcc.Count; i++)
            {
                PropertyInfo PInfo = typeof(MonthlyOperationGridClientGeneralSettings).GetProperty(Gcc[i].DataField);
                if (PInfo != null)
                    Gcc[i].Width = (int)PInfo.GetValue(monthlyOperationGridClientGeneralSettings, null);
            }
        }

        private void SetVisibleColumns_MasterMonthlyOperation(ComponentArt.Web.UI.Grid grid)
        {
            MonthlyOperationGridClientSettings monthlyOperationGridClientSettings = this.MonthlyOperationGridClientSettingsBusiness.GetMonthlyOperationGridClientSettings();
            GridColumnCollection Gcc = grid.Levels[0].Columns;
            for (int i = 1; i < Gcc.Count; i++)
            {
                PropertyInfo PInfo = typeof(MonthlyOperationGridClientSettings).GetProperty(Gcc[i].DataField);
                if (PInfo != null)
                    Gcc[i].Visible = (bool)PInfo.GetValue(monthlyOperationGridClientSettings, null);
            }                
        }

        private void SetReserveFieldsHeaderColumnsCaption_MasterMonthlyOperation(ComponentArt.Web.UI.Grid grid)
        {
            BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(0);
            GridColumnCollection Gcc = grid.Levels[0].Columns;
            IDictionary<ConceptReservedFields, string> ConceptsReservedFieldsReservedFieldsDic = MonthlyOperationBusiness.GetReservedFieldsNames();
            foreach (string conceptReservedFieldName in Enum.GetNames(typeof(ConceptReservedFields)))
            {
                for (int i = 0; i < Gcc.Count; i++)
                {
                    if (Gcc[i].DataField == conceptReservedFieldName)
                    {
                        //Gcc[i].HeadingText = MonthlyOperationBusiness.GetReservedFieldsName((ConceptReservedFields)Enum.Parse(typeof(ConceptReservedFields), conceptReservedFieldName));
                        Gcc[i].HeadingText = ConceptsReservedFieldsReservedFieldsDic[(ConceptReservedFields)Enum.Parse(typeof(ConceptReservedFields), conceptReservedFieldName)];
                        break;
                    }
                }
            }
        }        

        protected void CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            string[] retMessage = new string[4];
            try
            {
                this.BuildGrid_MasterMonthlyOperation(this.GridMasterMonthlyOperation_MasterMonthlyOperation);
                this.Fill_GridMasterMonthlyOperation_MasterMonthlyOperation((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
                this.ErrorHiddenField_MonthlyOperation.RenderControl(e.Output);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
                this.ErrorHiddenField_MonthlyOperation.RenderControl(e.Output);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
                this.ErrorHiddenField_MonthlyOperation.RenderControl(e.Output);
            }
            this.ErrorHiddenField_MonthlyOperation.RenderControl(e.Output);
            this.GridMasterMonthlyOperation_MasterMonthlyOperation.RenderControl(e.Output);
        }

        protected void CallBack_cmbPersonnel_MasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
        }

        protected void CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            string[] retMessage = new string[4];
            try
            {
                this.BuildGrid_MasterMonthlyOperation(this.GridSummaryMonthlyOperation_MasterMonthlyOperation);
                this.Fill_GridSummaryMonthlyOperation_MasterMonthlyOperation();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErroHiddenField_SummaryMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErroHiddenField_SummaryMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErroHiddenField_SummaryMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

            this.ErroHiddenField_SummaryMonthlyOperation.RenderControl(e.Output);
            this.GridSummaryMonthlyOperation_MasterMonthlyOperation.RenderControl(e.Output);
        }

        protected void CallBack_GridSettings_MasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            string[] retMessage = new string[4];
            try
            {
                Dictionary<string, string> MasterColArray = this.CreateRecievedColumnsArray_MasterMonthlyOperation(e.Parameters[0], "Get");
                Dictionary<string, string> SettingsColArray = null;
                if (e.Parameters[2] != string.Empty)
                    SettingsColArray = this.CreateRecievedColumnsArray_MasterMonthlyOperation(e.Parameters[2], "Set");

                MonthlyOperationGridClientSettings monthlyOperationGridClientSettings = this.GetVisibleColumns_GridSettings_MasterMonthlyOperation(MasterColArray, e.Parameters[1], SettingsColArray, decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3])), (LoadState)Enum.Parse(typeof(LoadState), e.Parameters[4]));

                switch (e.Parameters[1])
                {
                    case "Get":
                        this.GridSettings_MasterMonthlyOperation.DataSource = this.CreateDs_MasterMonthlyOperationGrid_SettingsCode(monthlyOperationGridClientSettings, MasterColArray, e.Parameters[1]).Tables[0];
                        this.GridSettings_MasterMonthlyOperation.DataBind();
                        this.hfCurrentSettingID_GridSettings_MasterMonthlyOperation.Value = monthlyOperationGridClientSettings.ID.ToString();
                        break;
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

            this.ErrorHiddenField_GridSettings_MasterMonthlyOperation.RenderControl(e.Output);
            this.hfCurrentSettingID_GridSettings_MasterMonthlyOperation.RenderControl(e.Output);
            this.GridSettings_MasterMonthlyOperation.RenderControl(e.Output);
        }

        private Dictionary<string, string> CreateRecievedColumnsArray_MasterMonthlyOperation(string RecievedStr, string State)
        {
            string[] ColStrArray = RecievedStr.Split(new char[] { ':' });
            Dictionary<string, string> ColArray = new Dictionary<string, string>();
            string[] ColStr;
            foreach (string Col in ColStrArray)
            {
                switch (State)
                {
                    case "Get":
                        ColStr = Col.Split(new char[] { '%' });
                        ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), this.StringBuilder.CreateString(ColStr[1]));
                        break;
                    case "Set":
                        ColStr = Col.Split(new char[] { '%' });
                        ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), ColStr[1]);
                        break;
                }
            }
            return ColArray;
        }

        private MonthlyOperationGridClientSettings GetVisibleColumns_GridSettings_MasterMonthlyOperation(Dictionary<string, string> MasterColArray, string State, Dictionary<string, string> SettingsColArray, decimal CurrentSettingID, LoadState LS)
        {
            MonthlyOperationGridClientSettings monthlyOperationGridClientSettings = new MonthlyOperationGridClientSettings();
            switch (State)
            {
                case "Get":
                    monthlyOperationGridClientSettings = this.MonthlyOperationGridClientSettingsBusiness.GetMonthlyOperationGridClientSettings();
                    break;
                case "Set":
                    foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridClientSettings).GetProperties())
                    {
                        monthlyOperationGridClientSettings.ID = CurrentSettingID;
                        if (MasterColArray.ContainsKey(pInfo.Name))
                            pInfo.SetValue(monthlyOperationGridClientSettings, Boolean.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                    }
                    switch (LS)
	                {
                        case LoadState.Manager:
                            MonthlyOperationGridClientSettingsBusiness.SaveChanges_onManagerState(monthlyOperationGridClientSettings, Business.UIActionType.EDIT);
                            break;
                        case LoadState.Personnel:
                            MonthlyOperationGridClientSettingsBusiness.SaveChanges_onPersonnelState(monthlyOperationGridClientSettings, Business.UIActionType.EDIT);
                            break;
	                }
                    break;
            }
            return monthlyOperationGridClientSettings;
        }

        private DataSet CreateDs_MasterMonthlyOperationGrid_SettingsCode(MonthlyOperationGridClientSettings monthlyOperationGridClientSettings, Dictionary<string, string> ColArray, string State)
        {
            DataSet dsSettings = new DataSet();
            DataTable dtSettings = new DataTable();
            DataColumn dcID = new DataColumn("ID", typeof(int));
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 0;
            dcID.AutoIncrementStep = 1;
            DataColumn dcViewState = new DataColumn("ViewState", typeof(bool));
            DataColumn dcGridColumn = new DataColumn("GridColumn", typeof(string));
            dtSettings.Columns.Add(dcID);
            dtSettings.Columns.Add(dcViewState);
            dtSettings.Columns.Add(dcGridColumn);
            dsSettings.Tables.Add(dtSettings);

            foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridClientSettings).GetProperties())
            {
                bool ViewState = false;
                string GridColumn = string.Empty;
                switch (State)
                {
                    case "Get":
                        try
                        {
                            ViewState = (bool)pInfo.GetValue(monthlyOperationGridClientSettings, null);
                            GridColumn = ColArray[pInfo.Name];
                            DataRow dr = dsSettings.Tables[0].NewRow();
                            dr["ViewState"] = ViewState;
                            dr["GridColumn"] = GridColumn;
                            dsSettings.Tables[0].Rows.Add(dr);
                        }
                        catch
                        { }
                        break;
                }
            }
            return dsSettings;
        }

        private void SetHeaderColumnsVisible_MasterMonthlyOperation(Table table)
        {
            MonthlyOperationGridClientSettings monthlyOperationGridClientSettings = this.MonthlyOperationGridClientSettingsBusiness.GetMonthlyOperationGridClientSettings();
            TableCellCollection gcc = table.Rows[0].Cells;
            for (int i = 1; i < gcc.Count; i++)
            {
                if (gcc[i].ID != null)
                {
                    PropertyInfo pinfo = typeof(MonthlyOperationGridClientSettings).GetProperty(gcc[i].ID);
                    if (pinfo != null)
                        gcc[i].Visible = (bool)pinfo.GetValue(monthlyOperationGridClientSettings, null);
                }

            }
        }

        private void SetHeaderColumnsSize_MasterMonthlyOperation(Table table)
        {
            MonthlyOperationGridClientGeneralSettings monthlyOperationGridClientGeneralSettings = this.MonthlyOperationGridClientSettingsBusiness.GetMonthlyOperationGridGeneralClientSettings();
            TableCellCollection gcc = table.Rows[0].Cells;
            for (int i = 1; i < gcc.Count; i++)
            {
                if (gcc[i].ID != null)
                {
                    PropertyInfo PInfo = typeof(MonthlyOperationGridClientGeneralSettings).GetProperty(gcc[i].ID);
                    if (PInfo != null)
                        gcc[i].Width = (int)PInfo.GetValue(monthlyOperationGridClientGeneralSettings, null);
                }

            }
        }

        private void SetReserveFieldsHeaderColumnsCaption_MasterMonthlyOperation(Table table)
        {
            BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(0);
            TableCellCollection gcc = table.Rows[0].Cells;
            IDictionary<ConceptReservedFields, string> ConceptsReservedFieldsReservedFieldsDic = MonthlyOperationBusiness.GetReservedFieldsNames();
            foreach (string conceptReservedFieldName in Enum.GetNames(typeof(ConceptReservedFields)))
            {
                for (int i = 0; i < gcc.Count; i++)
                {
                    if (gcc[i].ID == conceptReservedFieldName)
                    {
                        //gcc[i].Text = MonthlyOperationBusiness.GetReservedFieldsName((ConceptReservedFields)Enum.Parse(typeof(ConceptReservedFields), conceptReservedFieldName));
                        gcc[i].Text = ConceptsReservedFieldsReservedFieldsDic[(ConceptReservedFields)Enum.Parse(typeof(ConceptReservedFields), conceptReservedFieldName)];
                        break;
                    }
                }
            }
        }

        private void SetHeaderColumnscss_MasterMonthlyOperation(Table table)
        {
            foreach (TableCell item in table.Rows[0].Cells)
            {
                item.CssClass = "HHeadingFloatTextClass";
            }
        }

        private void BuildContainer_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation(Table table)
        {
            this.SetHeaderColumnsSize_MasterMonthlyOperation(table);
            this.SetHeaderColumnsVisible_MasterMonthlyOperation(table);
            this.SetReserveFieldsHeaderColumnsCaption_MasterMonthlyOperation(table);
            this.SetHeaderColumnscss_MasterMonthlyOperation(table);
        }

        protected void CallBack_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallback(object sender, CallBackEventArgs e)
        {
            this.BuildContainer_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation(tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation);
            this.tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation.RenderControl(e.Output);
        }



        

 

 


    }
}

