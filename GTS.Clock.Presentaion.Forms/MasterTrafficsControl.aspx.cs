using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using System.Threading;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using System.Reflection;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions;

public partial class MasterTrafficsControl : GTSBasePage
{
    public enum LoadState
    {
        Normal,
        Search,
        AdvancedSearch
    }

    public BTraffic TrafficsControlBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BTraffic>();
        }
    }

    public ISearchPerson PersonnelBusiness
    {
        get
        {
            return (ISearchPerson)(new BPerson());
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

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
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
        MasterTrafficsControl_onPageLoad,
        tbMasterTrafficsControl_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbPersonnel_MasterTrafficsControl.IsCallback && !CallBack_cmbPrecards_MasterTrafficsControl.IsCallback)
        {
            Page MasterTrafficsControlPage = this;
            Ajax.Utility.GenerateMethodScripts(MasterTrafficsControlPage);

            this.CheckTrafficsControlLoadAccess_MasterTrafficsControl();
            this.ViewCurrentLangCalendars_MasterTrafficsControl();
            this.Fill_cmbYear_MasterTrafficsControl();
            this.Fill_cmbMonth_MasterTrafficsControl();
            this.SetCurrentDate_MasterTrafficsControl();
            this.SetPersonnelPageSize_cmbPersonnel_MasterTrafficsControl();
            this.SetPersonnelPageCount_cmbPersonnel_MasterTrafficsControl(LoadState.Normal, this.cmbPersonnel_MasterTrafficsControl.DropDownPageSize, string.Empty);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckTrafficsControlLoadAccess_MasterTrafficsControl()
    {
        string[] retMessage = new string[4];
        try
        {
            this.TrafficsControlBusiness.CheckTrafficsControlLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void ViewCurrentLangCalendars_MasterTrafficsControl()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_MasterTrafficsControl.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_MasterTrafficsControl.Visible = true;
                break;
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

    private void SetCurrentDate_MasterTrafficsControl()
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
        this.hfCurrentDate_MasterTrafficsControl.Value = strCurrentDate;
    }


    private void Fill_cmbYear_MasterTrafficsControl()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_MasterTrafficsControl, this.hfCurrentYear_MasterTrafficsControl);
    }

    private void Fill_cmbMonth_MasterTrafficsControl()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_MasterTrafficsControl, this.hfCurrentMonth_MasterTrafficsControl);
    }

    private void SetPersonnelPageSize_cmbPersonnel_MasterTrafficsControl()
    {
        this.hfPersonnelPageSize_MasterTrafficsControl.Value = this.cmbPersonnel_MasterTrafficsControl.DropDownPageSize.ToString();
    }

    private void SetPersonnelPageCount_cmbPersonnel_MasterTrafficsControl(LoadState Ls, int pageSize, string SearchTerm)
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
            this.hfPersonnelCount_MasterTrafficsControl.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_MasterTrafficsControl.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_MasterTrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_MasterTrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_MasterTrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbPersonnel_MasterTrafficsControl_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbPersonnel_MasterTrafficsControl.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_MasterTrafficsControl((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_MasterTrafficsControl((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.hfPersonnelCount_MasterTrafficsControl.RenderControl(e.Output);
        this.hfPersonnelPageCount_MasterTrafficsControl.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_MasterTrafficsControl.RenderControl(e.Output);
        this.cmbPersonnel_MasterTrafficsControl.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_MasterTrafficsControl(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                this.cmbPersonnel_MasterTrafficsControl.Items.Add(personCmbItem);
            }
            this.cmbPersonnel_MasterTrafficsControl.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_MasterTrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_MasterTrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_MasterTrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbPrecards_MasterTrafficsControl_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbPrecards_MasterTrafficsControl.Dispose();
        this.Fill_cmbPrecards_MasterTrafficsControl();
        this.ErrorHiddenField_TrafficType.RenderControl(e.Output);
        this.cmbPrecards_MasterTrafficsControl.RenderControl(e.Output);
    }

    private void Fill_cmbPrecards_MasterTrafficsControl()
    {
        string[] retMessage = new string[4];
        IList<Precard> PrecardsList = null;
        try
        {
            PrecardsList = this.TrafficsControlBusiness.GetTrafficTypes();
            this.cmbPrecards_MasterTrafficsControl.DataSource = PrecardsList;
            this.cmbPrecards_MasterTrafficsControl.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_TrafficType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_TrafficType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_TrafficType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateTraffic_MasterTrafficsControlPage", "UpdateTraffic_MasterTrafficsControlPage_onCallBack", null, null)]
    public string[] UpdateTraffic_MasterTrafficsControlPage(string state, string SelectedTrafficID, string PersonnelID, string PrecardID, string Time, string Date, string Description)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal TrafficID = 0;
            decimal selectedTrafficID = decimal.Parse(this.StringBuilder.CreateString(SelectedTrafficID));
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
            decimal precardID = decimal.Parse(this.StringBuilder.CreateString(PrecardID));
            Time = this.StringBuilder.CreateString(Time);
            Date = this.StringBuilder.CreateString(Date);
            Description = this.StringBuilder.CreateString(Description);

            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            switch (uam)
            {
                case UIActionType.ADD:
                    TrafficID = this.TrafficsControlBusiness.InsertTraffic(personnelID, precardID, Date, Time, Description);
                    break;
                case UIActionType.DELETE:
                    if (selectedTrafficID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoItemSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.TrafficsControlBusiness.DeleteTraffic(selectedTrafficID);
                    TrafficID = precardID;
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.ADD:
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
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

    protected void CallBack_bulletedListCalculation_MasterTrafficsControl_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Calculate_MasterTrafficsControlPage(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]));
        this.ErrorHiddenField_CalculationResult.RenderControl(e.Output);
        this.bulletedListCalculation_MasterTrafficsControl.RenderControl(e.Output);
    }

    public void Calculate_MasterTrafficsControlPage(decimal PersonnelID, string CalculationDate)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            DailyReportProxy dailyReportProxy = this.TrafficsControlBusiness.GetPersonDailyReport(PersonnelID, CalculationDate);
            foreach (PropertyInfo pInfo in typeof(DailyReportProxy).GetProperties())
            {
                string pInfoName = pInfo.Name;
                if (GetLocalResourceObject(pInfo.Name) != null)
                    pInfoName = GetLocalResourceObject(pInfoName).ToString();
                string pInfoValue = pInfo.GetValue(dailyReportProxy, null).ToString();
                this.bulletedListCalculation_MasterTrafficsControl.Items.Add(" - " + pInfoName + " : " + pInfoValue);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_CalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_CalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_CalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


}