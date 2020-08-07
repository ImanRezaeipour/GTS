using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;

public partial class LocalDateTime : GTSBasePage
{
    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    enum Scripts
    {
        LocalDateTime_onPageLoad,
        LocalDateTime_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetCurrentDateTime_LocalDateTime();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }

    protected override void InitializeCulture()
    {
        RefererValidationProvider.CheckReferer();
        this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    private void SetCurrentCultureResObjs(string LangID)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    public void GetCurrentDateTime_LocalDateTime()
    {
        string CurrentLangID = string.Empty;
        string SysLangID = string.Empty;
        string Identifier = string.Empty;
        string retAxises = string.Empty;
        int year = 0;
        int month = 0;
        int day = 0;
        DayOfWeek dayOfWeek = 0;
        string dayName = string.Empty;
        string monthName = string.Empty;
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
                GregorianCalendar gCal = new GregorianCalendar();
                year = gCal.GetYear(DateTime.Now);
                month = gCal.GetMonth(DateTime.Now);
                day = gCal.GetDayOfMonth(DateTime.Now);
                dayOfWeek = gCal.GetDayOfWeek(DateTime.Now);
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
                year = pCal.GetYear(DateTime.Now);
                month = pCal.GetMonth(DateTime.Now);
                day = pCal.GetDayOfMonth(DateTime.Now);
                dayOfWeek = pCal.GetDayOfWeek(DateTime.Now);
                break;
        }
        monthName = GetLocalResourceObject("lblMonth" + month + Identifier).ToString();
        dayName = GetLocalResourceObject("lblDay" + this.GetDayOfWeek_LocalDateTime(dayOfWeek).ToString() + Identifier).ToString();

        lblCurrentDateTime_LocalDateTime.Text = dayName + " " + day + " " + monthName + " " + year;
    }

    private int GetDayOfWeek_LocalDateTime(DayOfWeek DayNumber)
    {
        int dayOfWeek = -1;
        string LangID = this.LangProv.GetCurrentLanguage();
        switch (LangID)
        {
            case "fa-IR":
                switch (DayNumber)
                {
                    case DayOfWeek.Friday:
                        dayOfWeek = 7;
                        break;
                    case DayOfWeek.Monday:
                        dayOfWeek = 3;
                        break;
                    case DayOfWeek.Saturday:
                        dayOfWeek = 1;
                        break;
                    case DayOfWeek.Sunday:
                        dayOfWeek = 2;
                        break;
                    case DayOfWeek.Thursday:
                        dayOfWeek = 6;
                        break;
                    case DayOfWeek.Tuesday:
                        dayOfWeek = 4;
                        break;
                    case DayOfWeek.Wednesday:
                        dayOfWeek = 5;
                        break;
                    default:
                        break;
                }

                break;
            case "en-US":
                switch (DayNumber)
                {
                    case DayOfWeek.Friday:
                        dayOfWeek = 5;
                        break;
                    case DayOfWeek.Monday:
                        dayOfWeek = 1;
                        break;
                    case DayOfWeek.Saturday:
                        dayOfWeek = 6;
                        break;
                    case DayOfWeek.Sunday:
                        dayOfWeek = 7;
                        break;
                    case DayOfWeek.Thursday:
                        dayOfWeek = 4;
                        break;
                    case DayOfWeek.Tuesday:
                        dayOfWeek = 2;
                        break;
                    case DayOfWeek.Wednesday:
                        dayOfWeek = 3;
                        break;
                    default:
                        break;
                }
                break;
        }
        return dayOfWeek;
    }



}