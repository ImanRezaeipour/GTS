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
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Model.Concepts;
using System.Web.Script.Serialization;

public partial class YearlyHolidays : GTSBasePage
{
    public BCalendarType YearlyHolidaysBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BCalendarType>();
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

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    internal class CurrentYearObj
    {
        public string Year { get; set; }
        public string UIYear { get; set; }
        public string Index { get; set; }
    }

    enum Scripts
    {
        YearlyHolidays_onPageLoad,
        tbYearlyHolidays_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridYearlyHolidays_YearlyHolidays.IsCallback)
        {
            Page YearlyHolidaysPage = this;
            Ajax.Utility.GenerateMethodScripts(YearlyHolidaysPage);
            this.Fill_cmbYear_YearlyHolidays();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckYearlyHolidaysLoadAccess_YearlyHolidays();
        }
    }

    private void CheckYearlyHolidaysLoadAccess_YearlyHolidays()
    {
        string[] retMessage = new string[4];
        try
        {
            this.YearlyHolidaysBusiness.CheckYearlyHolidaysLoadAccess();
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void Fill_cmbYear_YearlyHolidays()
    {
        int CurrentYear = DateTime.Now.Year;
        string CurrentUIYear = string.Empty;
        int CurrentYearIndex = 0;
        int YearCounter = 0;
        string cmbItemText = string.Empty;
        string SysLangID = this.LangProv.GetCurrentSysLanguage();
        PersianCalendar pCal = new PersianCalendar();
        for (int i = CurrentYear - 4; i <= (CurrentYear + 4); i++)
        {
            ComboBoxItem cmbItemYear = new ComboBoxItem(i.ToString());
            cmbItemYear.Value = i.ToString();
            switch (SysLangID)
            {
                case "fa-IR":
                    cmbItemText = pCal.GetYear(new DateTime(i, 12, 1)).ToString();
                    break;
                case "en-US":
                    cmbItemText = i.ToString();
                    break;
            }
            cmbItemYear.Text = cmbItemText;
            this.cmbYear_YearlyHolidays.Items.Add(cmbItemYear);
            ++YearCounter;
            if (i == CurrentYear)
            {
                CurrentUIYear = cmbItemText;
                CurrentYearIndex = YearCounter - 1;
            }
        }

        CurrentYearObj currentYearObj = new CurrentYearObj();
        currentYearObj.Year = CurrentYear.ToString();
        currentYearObj.UIYear = CurrentUIYear;
        currentYearObj.Index = CurrentYearIndex.ToString();
        this.hfCurrentYear_YearlyHolidays.Value = this.JsSerializer.Serialize(currentYearObj);
        this.cmbYear_YearlyHolidays.SelectedIndex = CurrentYearIndex;
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

    protected void CallBack_GridYearlyHolidays_YearlyHolidays_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridYearlyHolidays_YearlyHolidays();
        this.GridYearlyHolidays_YearlyHolidays.RenderControl(e.Output);
        this.ErrorHiddenField_YearlyHolidays.RenderControl(e.Output);
    }

    private void Fill_GridYearlyHolidays_YearlyHolidays()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<CalendarType> YearlyHolidaysGroupList = this.YearlyHolidaysBusiness.GetAll();
            this.GridYearlyHolidays_YearlyHolidays.DataSource = YearlyHolidaysGroupList;
            this.GridYearlyHolidays_YearlyHolidays.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateYearlyHolidaysGroup_YearlyHolidaysPage", "UpdateYearlyHolidaysGroup_YearlyHolidaysPage_onCallBack", null, null)]
    public string[] UpdateYearlyHolidaysGroup_YearlyHolidaysPage(string state, string SelectedYearlyHolidayGroupID, string YearlyHolidayGroupCustomCode, string YearlyHolidayGroupTitle, string YearlyHolidayGroupDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal YearlyHolidayGroupID = 0;
            decimal selectedYearlyHolidayGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedYearlyHolidayGroupID));
            YearlyHolidayGroupCustomCode = this.StringBuilder.CreateString(YearlyHolidayGroupCustomCode);
            YearlyHolidayGroupTitle = this.StringBuilder.CreateString(YearlyHolidayGroupTitle);
            YearlyHolidayGroupDescription = this.StringBuilder.CreateString(YearlyHolidayGroupDescription);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            CalendarType yearlyHolidaysGroup = new CalendarType();

            yearlyHolidaysGroup.ID = selectedYearlyHolidayGroupID;
            if (uam != UIActionType.DELETE)
            {
                yearlyHolidaysGroup.CustomCode = YearlyHolidayGroupCustomCode;
                yearlyHolidaysGroup.Name = YearlyHolidayGroupTitle;
                yearlyHolidaysGroup.Description = YearlyHolidayGroupDescription;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    YearlyHolidayGroupID = this.YearlyHolidaysBusiness.InsertYearlyHolidaysGroup(yearlyHolidaysGroup, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedYearlyHolidayGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoYearlyHolidaysGroupSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    YearlyHolidayGroupID = this.YearlyHolidaysBusiness.UpdateYearlyHolidaysGroup(yearlyHolidaysGroup, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedYearlyHolidayGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoYearlyHolidaysGroupSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    YearlyHolidayGroupID = this.YearlyHolidaysBusiness.DeleteYearlyHolidaysGroup(yearlyHolidaysGroup, uam);
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
            retMessage[3] = YearlyHolidayGroupID.ToString();
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