using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CultureConditionalDateTime : GTSBasePage
{
    public BControlParameter_DateTime CultureConditionalDateTimeBusiness
    {
        get
        {
            return new BControlParameter_DateTime();
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

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    enum Scripts
    {
        CultureConditionalDateTime_onPageLoad,
        CultureConditionalDateTime_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page CultureConditionalDateTimeForm = this;
        Ajax.Utility.GenerateMethodScripts(CultureConditionalDateTimeForm);

        this.ViewCurrentLangCalendars_CultureConditionalDateTime();
        this.SetCurrentDate_CultureConditionalDateTime();
        this.SetReportParameterID_CultureConditionalDateTime();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));

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

    private void SetCurrentDate_CultureConditionalDateTime()
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
        this.hfCurrentDate_CultureConditionalDateTime.Value = strCurrentDate;
        this.hfCurrentDate_CultureConditionalDateTime.Value = strCurrentDate;
    }

    private void ViewCurrentLangCalendars_CultureConditionalDateTime()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpfromDate_CultureConditionalDateTime.Visible = true;
                this.Container_pdptoDate_CultureConditionalDateTime.Visible = true;
                break;
            case "en-US":
                this.Container_gdpfromDate_CultureConditionalDateTime.Visible = true;
                this.Container_gdptoDate_CultureConditionalDateTime.Visible = true;
                break;
        }
    }

    private void SetReportParameterID_CultureConditionalDateTime()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportParametersID"))
            this.ReportParameterID.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportParametersID"]);
    }

    [Ajax.AjaxMethod("Register_CultureConditionalDateTimePage", "Register_CultureConditionalDateTimePage_onCallBack", null, null)]
    public string[] Register_CultureConditionalDateTimePage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string FromDate, string ToDate, string Hour, string Minute)
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            string RetValue = string.Empty;
            decimal reportParameterID = decimal.Parse(this.StringBuilder.CreateString(ReportParameterID));
            ReportParameterActionID = this.StringBuilder.CreateString(ReportParameterActionID);
            decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID));
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            int hour = int.Parse(this.StringBuilder.CreateString(Hour));
            int minute = int.Parse(this.StringBuilder.CreateString(Minute));

            RetValue = this.GetParameterValue(FromDate, ToDate, hour, minute);

            retMessage[0] = HttpContext.GetLocalResourceObject("~/CultureConditionalDateTime.aspx", "RetSuccessType").ToString();
            retMessage[1] = HttpContext.GetLocalResourceObject("~/CultureConditionalDateTime.aspx", "EditComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = RetValue;

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

    public string GetParameterValue(string FromDate, string ToDate, int Hour, int Minute)
    {
        DateTime fromDate = new DateTime();
        DateTime toDate = new DateTime();
        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        {
            fromDate = Utility.ToMildiDate(FromDate);
            toDate = Utility.ToMildiDate(ToDate);
        }
        else
        {
            fromDate = DateTime.Parse(FromDate);
            toDate = DateTime.Parse(ToDate);
        }

        int TimeMinutes = Hour * 60 + Minute;

        string result = String.Format("@fromDate={0};@toDate={1};@Value={2};", Utility.ToString(fromDate), Utility.ToString(toDate), TimeMinutes);
        return result;
    }






}