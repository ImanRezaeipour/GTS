using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.RequestFlow;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Engine;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using System.Web.UI.HtmlControls;

public partial class Calculations : GTSBasePage
{

    public BEngineCalculator EngineCalculateBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BEngineCalculator>();
        }
    }
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
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

    public enum CalculationPersonnelCountState
    {
        Single,
        Group
    }

    public JavaScriptSerializer JsSeializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public ISearchPerson PersonnelBusiness
    {
        get
        {
            return (ISearchPerson)(new BPerson());
        }
    }

    internal class CalculationsProgress
    {
        public int AllPersonnelCount { get; set; }
        public int CalculatedPersonnelCount { get; set; }
        public int Progress { get; set; }
        public bool InProgress { get; set; }
    }

    enum Scripts
    {
        Calculations_onPageLoad,
        tbCalculations_TabStripMenus_Operations,
        HelpForm_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    private void SetCurrentDate_Calculations()
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
        this.hfCurrentDate_Calculations.Value = strCurrentDate;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbPersonnel_Calculations.IsCallback && !CallBack_Container_CalculationProgressFeatures.IsCallback)
        {
            Page CalculationsPage = this;
            Ajax.Utility.GenerateMethodScripts(CalculationsPage);

            this.CheckCalculationsLoadAccess_Calculations();
            this.ViewCurrentLangCalendars_Calculations();
            this.SetCurrentDate_Calculations();
            this.SetPersonnelPageSize_cmbPersonnel_Calculations();
            this.SetPersonnelPageCount_cmbPersonnel_Calculations(LoadState.Normal, this.cmbPersonnel_Calculations.DropDownPageSize, string.Empty);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckCalculationsLoadAccess_Calculations()
    {
        string[] retMessage = new string[4];
        try
        {
            this.EngineCalculateBusiness.CheckCalculationsLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void ViewCurrentLangCalendars_Calculations()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_tbDaily_Calculations.Visible = true;
                this.Container_pdpToDate_tbDaily_Calculations.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_tbDaily_Calculations.Visible = true;
                this.Container_gdpToDate_tbDaily_Calculations.Visible = true;
                break;
        }
    }

    private void SetPersonnelPageSize_cmbPersonnel_Calculations()
    {
        this.hfPersonnelPageSize_Calculations.Value = this.cmbPersonnel_Calculations.DropDownPageSize.ToString();
    }
    private void SetPersonnelPageCount_cmbPersonnel_Calculations(LoadState Ls, int pageSize, string SearchTerm)
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
            this.hfPersonnelCount_Calculations.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_Calculations.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


    protected void CallBack_cmbPersonnel_Calculations_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.SetPersonnelPageCount_cmbPersonnel_Calculations((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_Calculations((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.cmbPersonnel_Calculations.RenderControl(e.Output);
        this.hfPersonnelCount_Calculations.RenderControl(e.Output);
        this.hfPersonnelPageCount_Calculations.RenderControl(e.Output);
        this.hfPersonnelSelectedCount_Calculations.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_Calculations.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_Calculations(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.GetAllPerson(pageIndex, pageSize);
                    hfPersonnelSelectedCount_Calculations.Value = PersonnelBusiness.GetPersonInQuickSearchCount(string.Empty).ToString();
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm);
                    hfPersonnelSelectedCount_Calculations.Value = PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm).ToString();
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                    hfPersonnelSelectedCount_Calculations.Value = PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm)).ToString();
                    break;
            }
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                personCmbItem.Id = personItem.ID.ToString();
                this.cmbPersonnel_Calculations.Items.Add(personCmbItem);
            }

        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("Calculate_CalculationsPage", "Calculate_CalculationsPage_onCallBack", null, null)]
    public string[] Calculate_CalculationsPage(string state, string SearchTerm, string FromDate, string ToDate, string PersonnelID, string calculationPersonnelCountState, string IsForcibleCalculate)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            bool ResultCalculate = false;
            decimal personnelID = decimal.Parse(StringBuilder.CreateString(PersonnelID));
            SearchTerm = StringBuilder.CreateString(SearchTerm);
            LoadState LS = (LoadState)Enum.Parse(typeof(LoadState), StringBuilder.CreateString(state));
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            CalculationPersonnelCountState CPCS = (CalculationPersonnelCountState)Enum.Parse(typeof(CalculationPersonnelCountState), this.StringBuilder.CreateString(calculationPersonnelCountState));
            bool isForcibleCalculate = bool.Parse(this.StringBuilder.CreateString(IsForcibleCalculate));

            switch (CPCS)
            {
                case CalculationPersonnelCountState.Single:
                    {
                        ResultCalculate = EngineCalculateBusiness.Calculate(personnelID, FromDate, ToDate, isForcibleCalculate);
                        int AllPersonnelCount = this.EngineCalculateBusiness.GetTotalCountInCalculating();
                        break;
                    }
                case CalculationPersonnelCountState.Group:
                    {
                        switch (LS)
                        {
                            case LoadState.Normal:
                                ResultCalculate = EngineCalculateBusiness.Calculate(FromDate, ToDate, isForcibleCalculate);
                                break;
                            case LoadState.Search:
                                ResultCalculate = EngineCalculateBusiness.Calculate(SearchTerm, FromDate, ToDate, isForcibleCalculate);
                                break;
                            case LoadState.AdvancedSearch:
                                ResultCalculate = EngineCalculateBusiness.Calculate(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), FromDate, ToDate, isForcibleCalculate);
                                break;
                        }
                        break;
                    }
            }

            string RetMessageType = string.Empty;
            string MessageBody = string.Empty;
            string MessageType = string.Empty;

            if (ResultCalculate)
            {
                RetMessageType = GetLocalResourceObject("RetSuccessType").ToString();
                MessageBody = GetLocalResourceObject("CalculationsRunning").ToString();
                MessageType = "success";
            }
            else
            {
                RetMessageType = GetLocalResourceObject("RetErrorType").ToString();
                MessageBody = GetLocalResourceObject("CalculationsRunningError").ToString();
                MessageType = "success";
            }

            retMessage[0] = RetMessageType;
            retMessage[1] = MessageBody;
            retMessage[2] = MessageType;

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

    protected void CallBack_Container_CalculationProgressFeatures_onCallBack(object sender, CallBackEventArgs e)
    {
        GetCalculationsPercentage_CalculationsPage();
        this.hfCalculationProgress_Calculations.RenderControl(e.Output);
        this.ErrorHiddenField_Calculations.RenderControl(e.Output);
        this.Container_CalculationProgressFeatures.RenderControl(e.Output);
    }

    public void GetCalculationsPercentage_CalculationsPage()
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        try
        {
            int AllPersonnelCount = this.EngineCalculateBusiness.GetTotalCountInCalculating();
            int NotCalculatedPersonnelCount = this.EngineCalculateBusiness.GetRemainCountInCalculating();
            int CalculatedPersonnelCount = AllPersonnelCount - NotCalculatedPersonnelCount;
            int Progress = 0;
            bool InProgress = false;

            if (AllPersonnelCount > 0)
            {
                InProgress = true;
                Progress = AllPersonnelCount > 0 ? Math.DivRem(CalculatedPersonnelCount * 100, AllPersonnelCount, out Progress) : 0;
                DrawProgressBar_Calculations(Progress, AllPersonnelCount, CalculatedPersonnelCount);
            }
            if (Progress == 100)
            {
                InProgress = false;
                this.EngineCalculateBusiness.ClearTotalCountInCalculating();
            }

            CalculationsProgress calculationProgress = new CalculationsProgress();
            calculationProgress.AllPersonnelCount = AllPersonnelCount;
            calculationProgress.CalculatedPersonnelCount = CalculatedPersonnelCount;
            calculationProgress.InProgress = InProgress;
            calculationProgress.Progress = Progress;
            this.hfCalculationProgress_Calculations.Value = this.JsSeializer.Serialize(calculationProgress);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Calculations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void DrawProgressBar_Calculations(int ProgressPercent, int AllPersonnelCount, int CalculatedPersonnelCount)
    {
        this.Container_CalculationProgressFeatures.Visible = true;
        for (int i = 1; i <= ProgressPercent; i++)
        {
            ((HtmlTableCell)this.Progressbar_Calculations.FindControl("p" + i + "_Progressbar_Calculations")).BgColor = "#4B0082";
        }
        this.lblAllPersonnelCount_Calculations.Text = GetLocalResourceObject("AllPersonnelCount_Calculations").ToString() + " " + AllPersonnelCount;
        this.lblCalculatedPersonnelCount_Calculations.Text = GetLocalResourceObject("CalculatedPersonnelCount_Calculations").ToString() + " " + CalculatedPersonnelCount;
        this.lblProgressPercentage_Calculations.Text = GetLocalResourceObject("ProgressPercentage_Calculations").ToString() + " " + ProgressPercent + "%";
    }




}