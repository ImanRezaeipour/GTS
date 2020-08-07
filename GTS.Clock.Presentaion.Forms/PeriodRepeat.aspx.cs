using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business;
using GTS.Clock.Model.Concepts;

public partial class PeriodRepeat : GTSBasePage
{
    public BWorkGroupCalendar PeriodRepeatBusiness
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
            return new BWorkGroupCalendar(Slr);
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
        PeriodRepeat_onPageLoad,
        DialogPeriodRepeat_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CallBack_cmbFromDay_PeriodRepeat.IsCallback && !CallBack_cmbToDay_PeriodRepeat.IsCallback && !CallBack_cmbHolidays_PeriodRepeat.IsCallback)
        {
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Year"))
            {
                int Year = int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Year"]));
                this.Fill_cmbMonthControls_PeriodRepeat();
                this.Fill_cmbDayControl_PeriodRepeat(this.cmbFromDay_PeriodRepeat, Year, 1);
                this.Fill_cmbDayControl_PeriodRepeat(this.cmbToDay_PeriodRepeat, Year, 1);
            }
        }
    }

    private void Fill_cmbMonthControls_PeriodRepeat()
    {
        string CurrentLangID = string.Empty;
        string SysLangID = string.Empty;
        string Identifier = string.Empty;
        int currentMonth = 0;
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
                currentMonth = currentDateTime.Month;
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
                currentMonth = pCal.GetMonth(currentDateTime);
                break;
        }

        for (int i = 1; i <= 12; i++)
        {
            ComboBoxItem cmbItemMonth = new ComboBoxItem(GetLocalResourceObject("Month" + i + "" + Identifier + "").ToString());
            cmbItemMonth.Value = i.ToString();
            this.cmbFromMonth_PeriodRepeat.Items.Add(cmbItemMonth);
            this.cmbToMonth_PeriodRepeat.Items.Add(cmbItemMonth);
        }

        this.cmbFromMonth_PeriodRepeat.SelectedIndex = this.cmbToMonth_PeriodRepeat.SelectedIndex = 0;
    }

    private void Fill_cmbDayControl_PeriodRepeat(ComboBox cmbDay, int Year, int Month)
    {
        int DayCount = 0;
        GregorianCalendar gCal = new GregorianCalendar();
        PersianCalendar pCal = new PersianCalendar();
        int year = pCal.GetYear(new DateTime(Year, 1, 1));
        switch (this.LangProv.GetCurrentLanguage())
        {
            case "fa-IR":
                DayCount = pCal.GetDaysInMonth(year, Month);
                break;
            case "en-US":
                DayCount = gCal.GetDaysInMonth(year, Month);
                break;
        }
        for (int i = 1; i <= DayCount; i++)
        {
            ComboBoxItem cmbItemDay = new ComboBoxItem();
            cmbItemDay.Text = cmbItemDay.Value = i.ToString();
            cmbDay.Items.Add(cmbItemDay);
        }
        cmbDay.SelectedIndex = 0;
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

    protected void CallBack_cmbHolidays_PeriodRepeat_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbHolidays_PeriodRepeat.Dispose();
        this.Fill_cmbHolidays_PeriodRepeat();
        this.ErrorHiddenField_Holidays.RenderControl(e.Output);
        this.cmbHolidays_PeriodRepeat.RenderControl(e.Output);
    }

    private void Fill_cmbHolidays_PeriodRepeat()
    {
        this.Fill_trvHolidays_PeriodRepeat();
    }

    private void Fill_trvHolidays_PeriodRepeat()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<CalendarType> HolidaysList = this.PeriodRepeatBusiness.GetAllHolidayTypes();
            foreach (CalendarType  holidayItem in HolidaysList)
            {
                TreeViewNode trvNodeHoliday = new TreeViewNode();
                trvNodeHoliday.Text = holidayItem.Name;
                trvNodeHoliday.Value = holidayItem.ID.ToString();
                trvNodeHoliday.ShowCheckBox = true;
                this.trvHolidays_PeriodRepeat.Nodes.Add(trvNodeHoliday);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Holidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Holidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Holidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }

    protected void CallBack_cmbFromDay_PeriodRepeat_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbFromDay_PeriodRepeat.Dispose();
        this.Fill_cmbFromDay_PeriodRepeat(int.Parse(this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_FromDay_PeriodRepeat.RenderControl(e.Output);
        this.cmbFromDay_PeriodRepeat.RenderControl(e.Output);
    }

    private void Fill_cmbFromDay_PeriodRepeat(int Year, int Month)
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_cmbDayControl_PeriodRepeat(this.cmbFromDay_PeriodRepeat, Year, Month);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_FromDay_PeriodRepeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_FromDay_PeriodRepeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_FromDay_PeriodRepeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbToDay_PeriodRepeat_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbToDay_PeriodRepeat.Dispose();
        this.Fill_cmbToDay_PeriodRepeat(int.Parse(this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_ToDay_PeriodRepeat.RenderControl(e.Output);
        this.cmbToDay_PeriodRepeat.RenderControl(e.Output);
    }

    private void Fill_cmbToDay_PeriodRepeat(int Year, int Month)
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_cmbDayControl_PeriodRepeat(this.cmbToDay_PeriodRepeat, Year, Month);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ToDay_PeriodRepeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ToDay_PeriodRepeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ToDay_PeriodRepeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


}