using GTS.Clock.Business.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;

public partial class MonthlyExceptionShifts : GTSBasePage
{
    public BExceptionShift ExceptionShiftsBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BExceptionShift>();
        }
    }

    public ISearchPerson PersonSearchBusiness
    {
        get
        {
            return (ISearchPerson)BusinessHelper.GetBusinessInstance<BPerson>();
        }
    }

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
        }
    }

    public enum PersonnelLoadState
    {
        Normal,
        Search,
        AdvancedSearch
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

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get
        {
            return new OperationYearMonthProvider();
        }
    }

    internal class PersonnelLoadStateConditions
    {
        public string PersonnelLoadState { get; set; }
        public string PersonnelSearchTerm { get; set; }
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
        MonthlyExceptionShifts_onPageLoad,
        DialogMonthlyExceptionShifts_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts.IsCallback)
        {
            Page MonthlyExceptionShiftsPage = this;
            Ajax.Utility.GenerateMethodScripts(MonthlyExceptionShiftsPage);

            this.CheckMonthlyExceptionShiftsLoadAccess_MonthlyExceptionShifts();
            this.SetMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.Fill_cmbYear_MonthlyExceptionShifts();
            this.Fill_cmbMonth_MonthlyExceptionShifts();
        }
    }

    private void CheckMonthlyExceptionShiftsLoadAccess_MonthlyExceptionShifts()
    {
        string[] retMessage = new string[4];
        try
        {
            this.ExceptionShiftsBusiness.CheckMonthlyExceptionShiftsLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
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

    private void SetMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts()
    {
        this.hfMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts.Value = this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.PageSize.ToString();
    }

    private void Fill_cmbYear_MonthlyExceptionShifts()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_MonthlyExceptionShifts, this.hfCurrentYear_MonthlyExceptionShifts);
    }

    private void Fill_cmbMonth_MonthlyExceptionShifts()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_MonthlyExceptionShifts, this.hfCurrentMonth_MonthlyExceptionShifts);
    }

    protected void CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
    {
        int Year = int.Parse(this.StringBuilder.CreateString(e.Parameters[0]));
        int Month = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]));
        int PageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
        int PageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
        PersonnelLoadState PLS = (PersonnelLoadState)Enum.Parse(typeof(PersonnelLoadState), this.StringBuilder.CreateString(e.Parameters[4]));
        string PersonnelSearchTerm = this.StringBuilder.CreateString(e.Parameters[5]);

        this.CustomizeGridMonthlyExceptionShifts_MonthlyExceptionShifts(Year, Month);
        this.SetMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts(Year, Month, PLS, PersonnelSearchTerm, PageSize);
        this.Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(Year, Month, PLS, PersonnelSearchTerm, PageSize, PageIndex);
        this.hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts.RenderControl(e.Output);
        this.ErrorHiddenField_MonthlyExceptionShifts.RenderControl(e.Output);
        this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.RenderControl(e.Output);
    }

    private void CustomizeGridMonthlyExceptionShifts_MonthlyExceptionShifts(int year, int month)
    {
        int monthDaysCount = this.ExceptionShiftsBusiness.GetMonthDatesList(year, month).Count();
        for (int i = 0; i < 31 - monthDaysCount; i++)
        {
            this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.Levels[0].Columns[monthDaysCount + 5 + i].Visible = false;
        }
    }

    private void SetMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts(int Year, int Month, PersonnelLoadState PLS, string PersonnelSearchTerm, int PageSize)
    {
        int PersonnelCount = 0;
        switch (PLS)
        {
            case PersonnelLoadState.Normal:
                PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                break;
            case PersonnelLoadState.Search:
                PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(PersonnelSearchTerm);
                break;
            case PersonnelLoadState.AdvancedSearch:
                PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm));
                break;
        }
        this.hfMonthlyExceptionShiftsCount_MonthlyExceptionShifts.Value = PersonnelCount.ToString();
        this.hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts.Value = Utility.GetPageCount(PersonnelCount, this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.PageSize).ToString();

    }

    private void Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(int Year, int Month, PersonnelLoadState PLS, string PersonnelSearchTerm, int PageSize, int PageIndex)
    {
        string[] retMessage = new string[4];
        IList<MonthlyExceptionShiftProxy> MonthlyExceptionShiftProxyList = null;
        try
        {
            this.InitializeCulture();
            switch (PLS)
            {
                case PersonnelLoadState.Normal:
                    MonthlyExceptionShiftProxyList = this.ExceptionShiftsBusiness.GetMonthlyExceptionShiftsList(Year, Month, PageIndex, PageSize);
                    break;
                case PersonnelLoadState.Search:
                    MonthlyExceptionShiftProxyList = this.ExceptionShiftsBusiness.GetMonthlyExceptionShiftsListByQuickSerch(Year, Month, PersonnelSearchTerm, PageIndex, PageSize);
                    break;
                case PersonnelLoadState.AdvancedSearch:
                    MonthlyExceptionShiftProxyList = this.ExceptionShiftsBusiness.GetMonthlyExceptionShiftsListByAdvancedSearch(Year, Month, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm), PageIndex, PageSize);
                    break;
            }
            this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.DataSource = MonthlyExceptionShiftProxyList;
            this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage", "UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage_onCallBack", null, null)]
    public string[] UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage(string state, string PersonnelID, string Year, string Month, string StrDayShiftCol)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
            int year = int.Parse(this.StringBuilder.CreateString(Year));
            int month = int.Parse(this.StringBuilder.CreateString(Month));
            IList<DateTime> MonthDatesList = this.ExceptionShiftsBusiness.GetMonthDatesList(year, month);
            switch (uam)
            {
                case UIActionType.EDIT:
                    IList<string> DaysShiftList = this.CreateDaysShiftList_MonthlyExceptionShifts(this.StringBuilder.CreateString(StrDayShiftCol), MonthDatesList.Count());
                    this.ExceptionShiftsBusiness.UpdatePersonnelMonthlyExceptionShifts(personnelID, MonthDatesList, DaysShiftList);
                    break;
                case UIActionType.DELETE:
                    this.ExceptionShiftsBusiness.DeletePersonnelMonthlyExceptionShifts(personnelID, MonthDatesList);
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
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

    private IList<string> CreateDaysShiftList_MonthlyExceptionShifts(string StrDayShiftCol, int MonthDaysCount)
    {
        Dictionary<string, object> DaysShiftDic = (Dictionary<string, object>)this.JsSerializer.DeserializeObject(StrDayShiftCol);
        IList<string> DaysShiftList = new List<string>();
        int dayCounter = 0;
        foreach (string key in DaysShiftDic.Keys)
        {
            if(dayCounter < MonthDaysCount)
            {
                DaysShiftList.Add(DaysShiftDic[key].ToString());
                dayCounter++;
            }
        }
        return DaysShiftList;
    }


}