using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Collections;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business;
using GTS.Clock.Model.Concepts;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

namespace GTS.Clock.Presentaion.WebForms
{
    enum PageCaller
    {
        WorkGroups,
        Holidays,
        PeriodRepeat
    }

    enum CalViewState
    {
        Normal,
        PeriodRepeat
    }

    public class BaseType
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }

    public class CalDataView
    {
        public string M { get; set; }
        public string CPointer { get; set; }
        public string RPointer { get; set; }
        public string SColor { get; set; }
        public string Title { get; set; }
    }

    public partial class Calendar : GTSBasePage
    {
        public BCalendarType YearlyHolidaysBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BCalendarType>();
            }
        }

        public BWorkGroupCalendar CalendarBusiness
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
                return BusinessHelper.GetBusinessInstance<BWorkGroupCalendar>(new KeyValuePair<string, object>("sysCulture",Slr));
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
            DialogCalendar_Operations,
            CalendarForm_onKeyDown,
            CalendarForm_onPageLoad,
            DialogPeriodRepeat_onPageLoad,
            Alert_Box,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page CalendarPage = this;
            Ajax.Utility.GenerateMethodScripts(CalendarPage);
            this.GetAxises_Calendar();
            this.InitializePage_Calendar();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        private void InitializePage_Calendar()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PageCaller"))
            {
                PageCaller PG = (PageCaller)Enum.Parse(typeof(PageCaller), HttpContext.Current.Request.QueryString["PageCaller"]);
                switch (PG)
                {
                    case PageCaller.WorkGroups:
                        this.TlbCalendar_DialogCalendar.Items[1].Visible = true;
                        this.TlbCalendar_DialogCalendar.Items[2].Visible = false;
                        this.TlbCalendar_DialogCalendar.Items[3].Visible = false;
                        this.lblTypes_DialogCalendar.Text = this.lblTypesInCalendar_Calendar.Text = GetLocalResourceObject("Shift").ToString();
                        break;
                    case PageCaller.Holidays:
                        this.TlbCalendar_DialogCalendar.Items[1].Visible = false;
                        this.TlbCalendar_DialogCalendar.Items[2].Visible = true;
                        this.TlbCalendar_DialogCalendar.Items[3].Visible = true;
                        this.lblTypes_DialogCalendar.Text = lblTypesInCalendar_Calendar.Text = GetLocalResourceObject("State").ToString();
                        break;
                }
                this.FillCalData_Calendar((PageCaller)Enum.Parse(typeof(PageCaller), HttpContext.Current.Request.QueryString["PageCaller"].ToString()), (CalViewState)Enum.Parse(typeof(CalViewState), HttpContext.Current.Request.QueryString["CalViewState"].ToString()), decimal.Parse(HttpContext.Current.Request.QueryString["GroupID"]), int.Parse(HttpContext.Current.Request.QueryString["UIYear"]));
                this.Fill_cmbTypes_Calendar(PG);
            }
        }

        private void FillCalData_Calendar(PageCaller PG, CalViewState CVS, decimal groupID, int year)
        {
            string[] retMessage = new string[3];
            string StrCalDataList = string.Empty;
            List<CalDataView> CalDataViewList = new List<CalDataView>();
            Dictionary<int, int> firstDayOfMonthDic = this.GetFirstDayOfMinth_Calendar(year);
            try
            {
                IList<CalendarCellInfo> CalendarCellInfoList = null;
                switch (PG)
                {
                    case PageCaller.WorkGroups:
                        switch (CVS)
	                    {
                            case CalViewState.Normal:
                                CalendarCellInfoList = this.CalendarBusiness.GetAll(groupID, year);
                                break;
                            case CalViewState.PeriodRepeat:
                                if (Session["PeriodRepeatList_Calendar"] != null)
                                {
                                    CalendarCellInfoList = (IList<CalendarCellInfo>)Session["PeriodRepeatList_Calendar"];
                                    Session["PeriodRepeatList_Calendar"] = null;
                                }
                                break;
	                    }
                        break;
                    case PageCaller.Holidays:
                        CalendarCellInfoList = this.YearlyHolidaysBusiness.GetCalendarList(year, groupID);
                        break;
                }
                foreach (CalendarCellInfo calendarCellInfoItem in CalendarCellInfoList)
                {
                    string strCalDataPart = string.Empty;
                    switch (PG)
                    {
                        case PageCaller.WorkGroups:
                            strCalDataPart = "#SID=" + calendarCellInfoItem.ShiftID.ToString();
                            break;
                        case PageCaller.Holidays:
                            strCalDataPart = "#SID=1";
                            break;
                    }

                    StrCalDataList += "M=" + calendarCellInfoItem.Month.ToString() + "#D=" + calendarCellInfoItem.Day.ToString() + strCalDataPart + "%";

                    Dictionary<string, int> PointerDic = this.GetColPointer_Calendar(calendarCellInfoItem.Day, firstDayOfMonthDic[calendarCellInfoItem.Month]);
                    CalDataView calDataView = new CalDataView();
                    calDataView.M = calendarCellInfoItem.Month.ToString();
                    calDataView.CPointer = PointerDic["Cp"].ToString();
                    calDataView.RPointer = PointerDic["Rp"].ToString();
                    calDataView.Title = calendarCellInfoItem.Title;
                    calDataView.SColor = calendarCellInfoItem.Color;
                    CalDataViewList.Add(calDataView);
                }

                this.hfCalData_Calendar.Value = StrCalDataList;

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                this.hfCalDataView_Calendar.Value = jsSerializer.Serialize(CalDataViewList);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_CalData_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_CalData_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_CalData_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private Dictionary<int, int> GetFirstDayOfMinth_Calendar(int year)
        {
            Dictionary<int, int> firstDayOfMonthDic = new Dictionary<int, int>();
            GregorianCalendar gcal = new GregorianCalendar();
            PersianCalendar pCal = new PersianCalendar();
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    for (int i = 1; i < 13; i++)
                    {
                        firstDayOfMonthDic.Add(i, this.SetMonthRowPointer(gcal.GetDayOfWeek(pCal.ToDateTime(year, i, 1, 0, 0, 0, 0))));
                    }
                    break;
                case "en-US":
                    for (int i = 1; i < 13; i++)
                    {
                        firstDayOfMonthDic.Add(i, this.SetMonthRowPointer(gcal.GetDayOfWeek(new DateTime(year, i, 1))));
                    }
                    break;
            }
            return firstDayOfMonthDic;
        }

        private Dictionary<string, int> GetColPointer_Calendar(int day, int firstDayofMonthRowPointer)
        {
            Dictionary<string, int> PointerDic = new Dictionary<string, int>();
            int rowPointer = 0;
            int colPointer = Math.DivRem(day + firstDayofMonthRowPointer - 1, 7, out rowPointer);
            if (colPointer == 0)
                colPointer++;
            else
            {
                if (rowPointer == 0)
                    rowPointer = 7;
                else
                    colPointer++;
            }
            PointerDic.Add("Rp", rowPointer);
            PointerDic.Add("Cp", colPointer);
            return PointerDic;
        }


        private void Fill_cmbTypes_Calendar(PageCaller PG)
        {
            string[] retMessage = new string[3];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            try
            {
                switch (PG)
                {
                    case PageCaller.WorkGroups:
                        List<BaseType> BaseShiftList = new List<BaseType>();
                        int shiftCounter = 0;
                        string cmbItemNoShiftText = GetLocalResourceObject("cmbItemNoShiftText").ToString();
                        string cmbItemNoShiftColor = "#FFFFFF";

                        ComboBoxItem cmbItemNoShift = new ComboBoxItem(cmbItemNoShiftText);
                        cmbItemNoShift.Id = "0";
                        cmbItemNoShift.Value = cmbItemNoShiftColor;
                        this.cmbTypes_DialogCalendar.Items.Add(cmbItemNoShift);
                        this.CreateBaseList_Calendar(BaseShiftList, cmbItemNoShiftText, cmbItemNoShiftColor);
                        shiftCounter++;

                        IList<Shift> ShiftsList = this.CalendarBusiness.GetAllShifts();
                        foreach (Shift shiftItem in ShiftsList)
                        {
                            ComboBoxItem cmbItemShift = new ComboBoxItem(shiftItem.Name);
                            cmbItemShift.Id = shiftItem.ID.ToString();
                            cmbItemShift.Value = shiftItem.Color;
                            this.cmbTypes_DialogCalendar.Items.Add(cmbItemShift);
                            if (shiftCounter < 10)
                            {
                                this.CreateBaseList_Calendar(BaseShiftList, shiftItem.Name, shiftItem.Color);
                                shiftCounter++;
                            }
                        }
                        this.hfBaseLists_Calendar.Value = jsSerializer.Serialize(BaseShiftList);
                        break;
                    case PageCaller.Holidays:
                        List<BaseType> BaseStateList = new List<BaseType>();

                        string cmbItemNormalStateText = GetLocalResourceObject("cmbItemNormalStateText").ToString();
                        string cmbItemNormalStateColor = "#FFFFFF";
                        ComboBoxItem cmbItemNormalState = new ComboBoxItem(cmbItemNormalStateText);
                        cmbItemNormalState.Id = "0";
                        cmbItemNormalState.Value = cmbItemNormalStateColor;
                        this.cmbTypes_DialogCalendar.Items.Add(cmbItemNormalState);
                        this.CreateBaseList_Calendar(BaseStateList, cmbItemNormalStateText, cmbItemNormalStateColor);

                        string cmbItemHolidayStateText = GetLocalResourceObject("cmbItemHolidayStateText").ToString();
                        string cmbItemHolidayStateColor = "#FF0000";
                        ComboBoxItem cmbItemHolidayState = new ComboBoxItem(cmbItemHolidayStateText);
                        cmbItemHolidayState.Id = "1";
                        cmbItemHolidayState.Value = cmbItemHolidayStateColor;
                        this.cmbTypes_DialogCalendar.Items.Add(cmbItemHolidayState);
                        this.CreateBaseList_Calendar(BaseStateList, cmbItemHolidayStateText, cmbItemHolidayStateColor);

                        this.hfBaseLists_Calendar.Value = jsSerializer.Serialize(BaseStateList);
                        break;
                }

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_BasePanel_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_BasePanel_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_BasePanel_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void CreateBaseList_Calendar(List<BaseType> BaseList, string Name, string Color)
        {
            BaseType baseType = new BaseType();
            baseType.Name = Name;
            baseType.Color = Color;
            BaseList.Add(baseType);
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

        public void GetAxises_Calendar()
        {
            string[] retMessage = new string[3];
            try
            {
                string CurrentLangID = string.Empty;
                string SysLangID = string.Empty;
                string Identifier = string.Empty;
                string retAxises = string.Empty;
                this.InitializeCulture();
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
                        break;
                }

                string Splitter = "@";
                for (int i = 1; i <= 12; i++)
                {
                    if (i == 12)
                        Splitter = string.Empty;
                    retAxises += GetLocalResourceObject("lblMonth" + i + "" + Identifier + "").ToString() + Splitter;
                }
                retAxises += "$";
                Splitter = "@";
                for (int j = 1; j <= 7; j++)
                {
                    if (j == 7)
                        Splitter = string.Empty;
                    retAxises += GetLocalResourceObject("lblDay" + j + "" + Identifier + "").ToString() + Splitter;
                }
                this.hfCalAxises_Calendar.Value = retAxises;

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_CalAxises_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_CalAxises_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_CalAxises_Calendar.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("CalendarPage_FillCal", "CalendarPage_FillCal_onCallBack", null, null)]
        public string[] CalendarPage_FillCal(string CalYear)
        {
            int CurrentGregorianYear = Convert.ToInt32(CalYear);
            string[] arRet = new string[367];
            string LangID = this.LangProv.GetCurrentSysLanguage();
            switch (LangID)
            {
                case "fa-IR":
                    arRet = this.FillPersianCal(CurrentGregorianYear + 1);
                    break;
                case "en-US":
                    arRet = this.FillGregorianCal(CurrentGregorianYear);
                    break;
                default:
                    arRet = this.FillGregorianCal(CurrentGregorianYear);
                    break;
            }
            return arRet;
        }

        public string[] FillGregorianCal(int CurrentGregorianYear)
        {
            int DayCount;
            DayOfWeek DayNumber;
            string[] arPMonths = new string[367];

            for (int i = 1; i <= 12; i++)
            {
                GregorianCalendar gnCalendar = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
                DayCount = gnCalendar.GetDaysInMonth(CurrentGregorianYear, i);

                for (int j = 1; j <= DayCount; j++)
                {
                    DateTime gDate = new DateTime(CurrentGregorianYear, i, j);
                    DayNumber = gnCalendar.GetDayOfWeek(gDate);
                    this.FillMonthsArray(arPMonths, CurrentGregorianYear, i, j, DayNumber);
                }
            }
            if (arPMonths.Length > 0 && this.LangProv.GetCurrentLanguage() != null)
                arPMonths[DayCounter] = this.LangProv.GetCurrentLanguage();
            return arPMonths;
        }

        public string[] FillPersianCal(int CurrentGregorianYear)
        {
            int DayCount;
            int CurrentOperationYear;
            DayOfWeek DayNumber;
            PersianCalendar pnCal = null;
            int pnYear = -1;
            string[] arPMonths = new string[367];

            PersianCalendar pCal = new PersianCalendar();
            int CurrentPersianYear = pCal.GetYear(new DateTime(CurrentGregorianYear, 1, 1));

            while (pnCal == null || pnYear == CurrentPersianYear)
            {
                if (pnCal == null)
                    CurrentOperationYear = CurrentGregorianYear - 1;
                else
                    CurrentOperationYear = CurrentGregorianYear;

                for (int i = 1; i <= 12; i++)
                {
                    DayCount = new GregorianCalendar(GregorianCalendarTypes.USEnglish).GetDaysInMonth(CurrentOperationYear, i);

                    for (int j = 1; j <= DayCount; j++)
                    {
                        DateTime gDate = new DateTime(CurrentOperationYear, i, j);
                        pnCal = new PersianCalendar();
                        pnYear = pnCal.GetYear(gDate);
                        int pnMonth = pnCal.GetMonth(gDate);
                        int pnDay = pnCal.GetDayOfMonth(gDate);
                        DayNumber = pnCal.GetDayOfWeek(gDate);

                        if (pnYear == CurrentPersianYear)
                            this.FillMonthsArray(arPMonths, pnYear, pnMonth, pnDay, DayNumber);
                        if (pnYear == CurrentPersianYear + 1)
                            break;
                    }
                }
            }
            if (arPMonths.Length > 0 && this.LangProv.GetCurrentLanguage() != null)
                arPMonths[DayCounter] = this.LangProv.GetCurrentLanguage();
            return arPMonths;
        }

        int bm = -1;
        int MonthColPointer;
        int DayCounter = 0;
        private string[] FillMonthsArray(string[] arPMonths, int pYear, int pMonth, int pDay, DayOfWeek DayNumber)
        {
            if (bm < pMonth || bm == -1)
                MonthColPointer = 1;
            bm = pMonth;

            int MonthRowPointer = SetMonthRowPointer(DayNumber);
            arPMonths[DayCounter] = "txtcal" + pMonth + "" + MonthColPointer + "" + MonthRowPointer + "=" + pDay + "";
            DayCounter++;
            if (MonthRowPointer == 7)
                MonthColPointer++;

            return arPMonths;
        }

        private int SetMonthRowPointer(DayOfWeek DayNumber)
        {
            int MonthRowPointer = -1;
            string LangID = this.LangProv.GetCurrentLanguage();
            switch (LangID)
            {
                case "fa-IR":
                    switch (DayNumber)
                    {
                        case DayOfWeek.Friday:
                            MonthRowPointer = 7;
                            break;
                        case DayOfWeek.Monday:
                            MonthRowPointer = 3;
                            break;
                        case DayOfWeek.Saturday:
                            MonthRowPointer = 1;
                            break;
                        case DayOfWeek.Sunday:
                            MonthRowPointer = 2;
                            break;
                        case DayOfWeek.Thursday:
                            MonthRowPointer = 6;
                            break;
                        case DayOfWeek.Tuesday:
                            MonthRowPointer = 4;
                            break;
                        case DayOfWeek.Wednesday:
                            MonthRowPointer = 5;
                            break;
                        default:
                            break;
                    }

                    break;
                case "en-US":
                    switch (DayNumber)
                    {
                        case DayOfWeek.Friday:
                            MonthRowPointer = 5;
                            break;
                        case DayOfWeek.Monday:
                            MonthRowPointer = 1;
                            break;
                        case DayOfWeek.Saturday:
                            MonthRowPointer = 6;
                            break;
                        case DayOfWeek.Sunday:
                            MonthRowPointer = 7;
                            break;
                        case DayOfWeek.Thursday:
                            MonthRowPointer = 4;
                            break;
                        case DayOfWeek.Tuesday:
                            MonthRowPointer = 2;
                            break;
                        case DayOfWeek.Wednesday:
                            MonthRowPointer = 3;
                            break;
                        default:
                            break;
                    }
                    break;
            }
            return MonthRowPointer;
        }

        [Ajax.AjaxMethod("UpdateCalendar_CalendarPage", "UpdateCalendar_CalendarPage_onCallBack", null, null)]
        public string[] UpdateCalendar_CalendarPage(string sender, string GroupID, string Year, string StrCellInfoList)
        {
            this.InitializeCulture();

            PageCaller pageCaller = (PageCaller)Enum.Parse(typeof(PageCaller), this.StringBuilder.CreateString(sender));
            decimal groupID = decimal.Parse(this.StringBuilder.CreateString(GroupID));
            int year = int.Parse(this.StringBuilder.CreateString(Year));
            IList<CalendarCellInfo> calendarCellInfoList = this.CreateCalendarCellInfoList_Calendar(pageCaller, this.StringBuilder.CreateString(StrCellInfoList));

            string[] retMessage = new string[4];
            try
            {
                switch (pageCaller)
                {
                    case PageCaller.WorkGroups:
                        this.CalendarBusiness.SaveChanges(calendarCellInfoList, groupID, year);
                        break;
                    case PageCaller.Holidays:
                        this.YearlyHolidaysBusiness.InsertCalendars(groupID, year, calendarCellInfoList);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
                retMessage[2] = "success";
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

        private List<CalendarCellInfo> CreateCalendarCellInfoList_Calendar(PageCaller PG, string StrCellInfoList)
        {
            List<CalendarCellInfo> CalendarCellInfoList = new List<CalendarCellInfo>();
            string[] ArStrDateShiftList = StrCellInfoList.Split(new char[] { '%' });
            string TempArStrDateShiftListPart = string.Empty;
            foreach (string ArStrDateShiftListPart in ArStrDateShiftList)
            {
                if (ArStrDateShiftListPart != string.Empty && ArStrDateShiftListPart.Contains("SID"))
                {
                    TempArStrDateShiftListPart = ArStrDateShiftListPart.Replace("SID=", string.Empty).Replace("M=", string.Empty).Replace("D=", string.Empty);
                    string[] ArDateShift = TempArStrDateShiftListPart.Split(new char[] { '#' });
                    int month = int.Parse(ArDateShift[0]);
                    int day = int.Parse(ArDateShift[1]);

                    CalendarCellInfo calendarCellInfo = new CalendarCellInfo();
                    calendarCellInfo.Month = month;
                    calendarCellInfo.Day = day;
                    if (PG == PageCaller.WorkGroups)
                    {
                        decimal shiftID = decimal.Parse(ArDateShift[2]);
                        calendarCellInfo.ShiftID = shiftID;
                    }

                    CalendarCellInfoList.Add(calendarCellInfo);
                }
            }
            return CalendarCellInfoList;
        }

        [Ajax.AjaxMethod("UpdatePeriodRepeat_CalendarPage", "UpdatePeriodRepeat_CalendarPage_onCallBack", null, null)]
        public string[] UpdatePeriodRepeat_CalendarPage(string sender, string Year, string GroupID, string StrCellInfoList, string FromMonth, string FromDay, string ToMonth, string ToDay, string StrHolidaysList)
        {
            string[] retMessage = new string[4];
            try
            {
                PageCaller pageCaller = (PageCaller)Enum.Parse(typeof(PageCaller), this.StringBuilder.CreateString(sender));
                int year = int.Parse(this.StringBuilder.CreateString(Year));
                decimal groupID = decimal.Parse(this.StringBuilder.CreateString(GroupID));
                IList<CalendarCellInfo> calendarCellInfoList = this.CreateCalendarCellInfoList_Calendar(pageCaller, this.StringBuilder.CreateString(StrCellInfoList));
                int fromMonth = int.Parse(this.StringBuilder.CreateString(FromMonth));
                int fromDay = int.Parse(this.StringBuilder.CreateString(FromDay));
                int toMonth = int.Parse(this.StringBuilder.CreateString(ToMonth));
                int toDay = int.Parse(this.StringBuilder.CreateString(ToDay));
                IList<decimal> HolidaysList = this.CreateHolidaysList_Calendar(this.StringBuilder.CreateString(StrHolidaysList));

                IList<CalendarCellInfo> PeriodRepeatList = this.CalendarBusiness.RepetitionPeriod(year, fromMonth, fromDay, toMonth, toDay, HolidaysList, calendarCellInfoList);
                Session.Add("PeriodRepeatList_Calendar", PeriodRepeatList);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
                retMessage[2] = "success";

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

        private IList<decimal> CreateHolidaysList_Calendar(string StrHolidaysList)
        {
            IList<decimal> HolidaysList = new List<decimal>();
            if (StrHolidaysList != string.Empty)
            {
                string[] HolidaysListCol = StrHolidaysList.Split(new char[] { '#' });
                foreach (string HolidaysListItem in HolidaysListCol)
                {
                    if (HolidaysListItem != string.Empty)
                        HolidaysList.Add(decimal.Parse(HolidaysListItem));
                }
            }
            return HolidaysList;
        }
    }


}