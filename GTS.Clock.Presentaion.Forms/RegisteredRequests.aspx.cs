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
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business;
using GTS.Clock.Model;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using GTS.Clock.Business.Security;
using System.Web.Configuration;

public partial class RegisteredRequests : GTSBasePage
{
    public IRegisteredRequests RegisteredRequestsBusiness
    {
        get
        {
            return (IRegisteredRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
        }
    }

    public BRequest MasterRequestBusiness
    {
        get
        {
            return new BRequest();
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

    enum CurrentUserState
    {
        NormalUser,
        Operator
    }

    public enum LoadState
    {
        Normal,
        Search,
        AdvancedSearch
    }

    internal class PersonnelDetails
    {
        public string ID { get; set; }
        public string OrganizationPostID { get; set; }
        public string OrganizationPostName { get; set; }
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

    enum RegisteredRequestsCaller
    {
        MainPage,
        MonthlyOperationGridSchema,
        MonthlyOperationGanttChartSchema
    }

    enum Scripts
    {
        RegisteredRequests_onPageLoad,
        DialogRegisteredRequests_Operations,
        DialogRequestRegister_onPageLoad,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridRegisteredRequests_RegisteredRequests.IsCallback && !CallBack_cmbRequestType_RegisteredRequests.IsCallback && !CallBack_cmbExporter_RegisteredRequests.IsCallback && !CallBack_cmbPersonnel_RegisteredRequests.IsCallback)
        {
            Page RegisteredRequestsPage = this;
            Ajax.Utility.GenerateMethodScripts(RegisteredRequestsPage);

            this.CheckRegisteredRequestsLoadAccess_RegisteredRequests();
            this.ViewCurrentLangCalendars_RegisteredRequests();
            this.Customize_TlbRegisteredRequests_RegisteredRequests(this.GetCurrentUserState_RegisteredRequests());
            this.Fill_cmbYear_RegisteredRequests();
            this.Fill_cmbMonth_RegisteredRequests();
            this.SetRequestStatesStr_RegisteredRequests();
            this.SetRequestTypesStr_RegisteredRequests();
            this.SetRegisteredRequestsPageSize_RegisteredRequests();
            this.SetPersonnelPageSize_cmbPersonnel_RegisteredRequests();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckRegisteredRequestsLoadAccess_RegisteredRequests()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
            {
                RegisteredRequestsCaller RRC = (RegisteredRequestsCaller)Enum.Parse(typeof(RegisteredRequestsCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));
                switch (RRC)
                {
                    case RegisteredRequestsCaller.MainPage:
                        this.RegisteredRequestsBusiness.CheckRegisteredRequestsLoadAccess_onMainPage();
                        break;
                    case RegisteredRequestsCaller.MonthlyOperationGridSchema:
                        this.RegisteredRequestsBusiness.CheckRegisteredRequestsLoadAccess_onMonthlyOperationGridSchema();
                        break;
                    case RegisteredRequestsCaller.MonthlyOperationGanttChartSchema:
                        this.RegisteredRequestsBusiness.CheckRegisteredRequestsLoadAccess_onMonthlyOperationGanttChartSchema();
                        break;
                }
            }
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void ViewCurrentLangCalendars_RegisteredRequests()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_RegisteredRequests.Visible = true;
                this.Container_pdpToDate_RegisteredRequests.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_RegisteredRequests.Visible = true;
                this.Container_gdpToDate_RegisteredRequests.Visible = true;
                break;
        }
    }

    private CurrentUserState GetCurrentUserState_RegisteredRequests()
    {
        bool IsOperator = this.RegisteredRequestsBusiness.IsCurrentUserOperator;
        CurrentUserState CUS = CurrentUserState.NormalUser;
        if (IsOperator)
            CUS = CurrentUserState.Operator;
        else
            CUS = CurrentUserState.NormalUser;

        CurrentUserState_RegisteredRequests.Value = CUS.ToString();
        return CUS;
    }

    private void Customize_TlbRegisteredRequests_RegisteredRequests(CurrentUserState CUS)
    {
        ToolBarItem tlbItemInsert_TlbRegisteredRequests = new ToolBarItem();
        tlbItemInsert_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemInsert_TlbRegisteredRequests").ToString();
        tlbItemInsert_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemInsert_TlbRegisteredRequests.ClientSideCommand = "tlbItemInsert_TlbRegisteredRequests_onClick();";
        tlbItemInsert_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemInsert_TlbRegisteredRequests.ImageUrl = "add.png";

        ToolBarItem tlbItemDelete_TlbRegisteredRequests = new ToolBarItem();
        tlbItemDelete_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemDelete_TlbRegisteredRequests").ToString();
        tlbItemDelete_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemDelete_TlbRegisteredRequests.ClientSideCommand = "tlbItemDelete_TlbRegisteredRequests_onClick();";
        tlbItemDelete_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemDelete_TlbRegisteredRequests.ImageUrl = "remove.png";

        ToolBarItem tlbItemFilter_TlbRegisteredRequests = new ToolBarItem();
        tlbItemFilter_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemFilter_TlbRegisteredRequests").ToString();
        tlbItemFilter_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemFilter_TlbRegisteredRequests.ClientSideCommand = "tlbItemFilter_TlbRegisteredRequests_onClick();";
        tlbItemFilter_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemFilter_TlbRegisteredRequests.ImageUrl = "filter.png";

        ToolBarItem tlbItemRequestByOperator_TlbRegisteredRequests = new ToolBarItem();
        tlbItemRequestByOperator_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemRequestByOperator_TlbRegisteredRequests").ToString();
        tlbItemRequestByOperator_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemRequestByOperator_TlbRegisteredRequests.ClientSideCommand = "tlbItemRequestByOperator_TlbRegisteredRequests_onClick();";
        tlbItemRequestByOperator_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemRequestByOperator_TlbRegisteredRequests.ImageUrl = "operator.png";

        ToolBarItem tlbItemHelp_TlbRegisteredRequests = new ToolBarItem();
        tlbItemHelp_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemHelp_TlbRegisteredRequests").ToString();
        tlbItemHelp_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemHelp_TlbRegisteredRequests.ClientSideCommand = "tlbItemHelp_TlbRegisteredRequests_onClick();";
        tlbItemHelp_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemHelp_TlbRegisteredRequests.ImageUrl = "help.gif";

        ToolBarItem tlbItemFormReconstruction_TlbRegisteredRequests = new ToolBarItem();
        tlbItemFormReconstruction_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemFormReconstruction_TlbRegisteredRequests").ToString();
        tlbItemFormReconstruction_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemFormReconstruction_TlbRegisteredRequests.ClientSideCommand = "tlbItemFormReconstruction_TlbRegisteredRequests_onClick();";
        tlbItemFormReconstruction_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemFormReconstruction_TlbRegisteredRequests.ImageUrl = "refresh.png";

        ToolBarItem tlbItemExit_TlbRegisteredRequests = new ToolBarItem();
        tlbItemExit_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemExit_TlbRegisteredRequests").ToString();
        tlbItemExit_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemExit_TlbRegisteredRequests.ClientSideCommand = "tlbItemExit_TlbRegisteredRequests_onClick();";
        tlbItemExit_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemExit_TlbRegisteredRequests.ImageUrl = "exit.png";

        TlbRegisteredRequests.Items.Add(tlbItemInsert_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemDelete_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemFilter_TlbRegisteredRequests);
        if (CUS == CurrentUserState.Operator)
            TlbRegisteredRequests.Items.Add(tlbItemRequestByOperator_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemHelp_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemFormReconstruction_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemExit_TlbRegisteredRequests);
    }

    private void SetPersonnelPageSize_cmbPersonnel_RegisteredRequests()
    {
        this.hfPersonnelPageSize_RegisteredRequests.Value = this.cmbPersonnel_RegisteredRequests.DropDownPageSize.ToString();
    }

    private void SetPersonnelPageCount_cmbPersonnel_RegisteredRequests(LoadState Ls, int pageSize, string SearchTerm)
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
            this.hfPersonnelCount_RegisteredRequests.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_RegisteredRequests.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void SetRegisteredRequestsPageSize_RegisteredRequests()
    {
        this.hfRegisteredRequestsPageSize_RegisteredRequests.Value = this.GridRegisteredRequests_RegisteredRequests.PageSize.ToString();
    }

    private void Fill_cmbYear_RegisteredRequests()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_RegisteredRequests, this.hfCurrentYear_RegisteredRequests);
    }

    private void Fill_cmbMonth_RegisteredRequests()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_RegisteredRequests, this.hfCurrentMonth_RegisteredRequests);
    }

    private void SetRequestStatesStr_RegisteredRequests()
    {
        string strRequestStates = string.Empty;
        foreach (RequestState requestStateItem in Enum.GetValues(typeof(RequestState)))
        {
            strRequestStates += "#" + GetLocalResourceObject(requestStateItem.ToString()).ToString() + ":" + ((int)requestStateItem).ToString();
        }
        this.hfRequestStates_RegisteredRequests.Value = strRequestStates;
    }

    private void SetRequestTypesStr_RegisteredRequests()
    {
        string strRequestTypes = string.Empty;
        foreach (RequestType requestTypeItem in Enum.GetValues(typeof(RequestType)))
        {
            strRequestTypes += "#" + GetLocalResourceObject(requestTypeItem.ToString()).ToString() + ":" + ((int)requestTypeItem).ToString();
        }
        this.hfRequestTypes_RegisteredRequests.Value = strRequestTypes;
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

    protected void CallBack_GridRegisteredRequests_RegisteredRequests_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridRegisteredRequests_RegisteredRequests((CurrentUserState)Enum.Parse(typeof(CurrentUserState), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), int.Parse(this.StringBuilder.CreateString(e.Parameters[5])), int.Parse(this.StringBuilder.CreateString(e.Parameters[6])));
        this.SetRegisteredRequestsPageCount_RegisteredRequests(e);
        this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
        this.GridRegisteredRequests_RegisteredRequests.RenderControl(e.Output);
        this.hfRegisteredRequestsCount_RegisteredRequests.RenderControl(e.Output);
        this.hfRegisteredRequestsPageCount_RegisteredRequests.RenderControl(e.Output);
    }

    private void SetRegisteredRequestsPageCount_RegisteredRequests(CallBackEventArgs e)
    {
        string LoadState = this.StringBuilder.CreateString(e.Parameters[1]);
        switch (LoadState)
        {
            case "CustomFilter":
                this.SetRegisteredRequestsPageCount_RegisteredRequests(this.StringBuilder.CreateString(e.Parameters[4]));
                break;
            default:
                this.SetRegisteredRequestsPageCount_RegisteredRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])));
                break;
        }
    }

    private void SetRegisteredRequestsPageCount_RegisteredRequests(RequestState RS, int Year, int Month)
    {
        int KartableCount = this.RegisteredRequestsBusiness.GetUserRequestCount(RS, Year, Month);
        this.hfRegisteredRequestsCount_RegisteredRequests.Value = KartableCount.ToString();
        this.hfRegisteredRequestsPageCount_RegisteredRequests.Value = Utility.GetPageCount(KartableCount, this.GridRegisteredRequests_RegisteredRequests.PageSize).ToString();
    }

    private void SetRegisteredRequestsPageCount_RegisteredRequests(string StrRequestFliterProxyList)
    {
        UserRequestFilterProxy CustomFilterProxy = GetRegisteredRequestsCustomFilterProxy_RegisteredRequests(StrRequestFliterProxyList);
        int RegisteredRequestsCount = this.RegisteredRequestsBusiness.GetFilterUserRequestsCount(CustomFilterProxy);
        this.hfRegisteredRequestsCount_RegisteredRequests.Value = RegisteredRequestsCount.ToString();
        this.hfRegisteredRequestsPageCount_RegisteredRequests.Value = Utility.GetPageCount(RegisteredRequestsCount, this.GridRegisteredRequests_RegisteredRequests.PageSize).ToString();
    }

    private void Fill_GridRegisteredRequests_RegisteredRequests(CurrentUserState CUS, string LoadState, int year, int month, string filterString, int pageSize, int pageIndex)
    {
        string[] retMessage = new string[4];
        IList<KartablProxy> RegisteredRequestsList = null;
        try
        {
            switch (LoadState)
            {
                case "CustomFilter":
                    UserRequestFilterProxy CustomFilterProxy = this.GetRegisteredRequestsCustomFilterProxy_RegisteredRequests(filterString);
                    RegisteredRequestsList = this.RegisteredRequestsBusiness.GetFilterUserRequests(CustomFilterProxy, pageIndex, pageSize);
                    break;
                default:
                    RegisteredRequestsList = this.RegisteredRequestsBusiness.GetAllUserRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), year, month, pageIndex, pageSize);
                    break;
            }
            this.operationYearMonthProvider.SetOperationYearMonth(year, month);
            this.GridRegisteredRequests_RegisteredRequests.DataSource = RegisteredRequestsList;
            this.GridRegisteredRequests_RegisteredRequests.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private UserRequestFilterProxy GetRegisteredRequestsCustomFilterProxy_RegisteredRequests(string strCustomFilter)
    {
        UserRequestFilterProxy customFilterProxy = new UserRequestFilterProxy();
        if (strCustomFilter != string.Empty)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Dictionary<string, object> ParamsDic = (Dictionary<string, object>)jsSerializer.DeserializeObject(strCustomFilter);
            CurrentUserState CUS = (CurrentUserState)Enum.Parse(typeof(CurrentUserState), ParamsDic["currentUserState"].ToString());
            int personnelID = int.Parse(ParamsDic["personnelID"].ToString());
            int requestTypeID = int.Parse(ParamsDic["requestTypeID"].ToString());
            int requestExporter = int.Parse(ParamsDic["requestExporter"].ToString());
            string fromDate = ParamsDic["fromDate"].ToString();
            string toDate = ParamsDic["toDate"].ToString();
            switch (CUS)
            {
                case CurrentUserState.NormalUser:
                    break;
                case CurrentUserState.Operator:
                    customFilterProxy.UnderManagmentPersonId = personnelID;
                    break;
            }
            if (requestTypeID != -1)
                customFilterProxy.RequestType = (RequestType)Enum.ToObject(typeof(RequestType), requestTypeID);
            if (requestExporter != -1)
                customFilterProxy.RequestSubmiter = (RequestSubmiter)Enum.ToObject(typeof(RequestSubmiter), requestExporter);
            if (fromDate != string.Empty)
                customFilterProxy.FromDate = fromDate;
            if (toDate != string.Empty)
                customFilterProxy.ToDate = toDate;
        }
        return customFilterProxy;
    }

    protected void CallBack_cmbRequestType_RegisteredRequests_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbRequestType_RegisteredRequests.Dispose();
        this.Fill_cmbRequestType_RegisteredRequests();
        this.ErrorHiddenField_RequestsTypes.RenderControl(e.Output);
        this.cmbRequestType_RegisteredRequests.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_RegisteredRequests()
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            foreach (RequestType requestTypeItem in Enum.GetValues(typeof(RequestType)))
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(GetLocalResourceObject(requestTypeItem.ToString()).ToString());
                cmbItemRequestType.Value = requestTypeItem.ToString();
                cmbItemRequestType.Id = ((int)requestTypeItem).ToString();
                this.cmbRequestType_RegisteredRequests.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestsTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestsTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestsTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbExporter_RegisteredRequests_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbExporter_RegisteredRequests.Dispose();
        this.Fill_cmbExporter_RegisteredRequests();
        this.ErrorHiddenField_Exporters.RenderControl(e.Output);
        this.cmbExporter_RegisteredRequests.RenderControl(e.Output);
    }

    private void Fill_cmbExporter_RegisteredRequests()
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            foreach (RequestSubmiter requestExporterItem in Enum.GetValues(typeof(RequestSubmiter)))
            {
                ComboBoxItem cmbItemExporter = new ComboBoxItem(GetLocalResourceObject(requestExporterItem.ToString()).ToString());
                cmbItemExporter.Value = requestExporterItem.ToString();
                cmbItemExporter.Id = ((int)requestExporterItem).ToString();
                this.cmbExporter_RegisteredRequests.Items.Add(cmbItemExporter);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Exporters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Exporters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Exporters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateRegisteredRequest_RegisteredRequestsPage", "UpdateRegisteredRequest_RegisteredRequestsPage_onCallBack", null, null)]
    public string[] UpdateRegisteredRequest_RegisteredRequestsPage(string state, string SelectedRegisteredRequestID, string SelectedRegisteredRequestAttachmentFile)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            decimal selectedRegisteredRequestID = decimal.Parse(this.StringBuilder.CreateString(SelectedRegisteredRequestID));
            SelectedRegisteredRequestAttachmentFile = this.StringBuilder.CreateString(SelectedRegisteredRequestAttachmentFile);

            switch (uam)
            {
                case UIActionType.DELETE:
                    if (selectedRegisteredRequestID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRegisteredRequestsSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.RegisteredRequestsBusiness.DeleteRequest(selectedRegisteredRequestID);
                    if(SelectedRegisteredRequestAttachmentFile != null && SelectedRegisteredRequestAttachmentFile != string.Empty)
                       this.MasterRequestBusiness.DeleteRequestAttachment(WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + SelectedRegisteredRequestAttachmentFile);
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = selectedRegisteredRequestID.ToString();
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

    protected void CallBack_cmbPersonnel_RegisteredRequests_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbPersonnel_RegisteredRequests.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_RegisteredRequests((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_RegisteredRequests((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.hfPersonnelCount_RegisteredRequests.RenderControl(e.Output);
        this.hfPersonnelPageCount_RegisteredRequests.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_RegisteredRequests.RenderControl(e.Output);
        this.cmbPersonnel_RegisteredRequests.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_RegisteredRequests(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                PersonnelDetails personnelDetails = new PersonnelDetails();
                personnelDetails.ID = personItem.ID.ToString();
                personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                personCmbItem.Value = this.JsSeializer.Serialize(personnelDetails);
                this.cmbPersonnel_RegisteredRequests.Items.Add(personCmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }



}