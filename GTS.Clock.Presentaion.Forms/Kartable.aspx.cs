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
using System.Collections;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;

public partial class Kartable : GTSBasePage
{
    public enum RequestCaller
    {
        Kartable,
        Survey,
        Sentry
    }

    public IKartablRequests KartableBusiness
    {
        get
        {
            return (IKartablRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());

        }
    }

    public IReviewedRequests SurveyBusiness
    {
        get
        {
            return (IReviewedRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
        }
    }

    public BSentryPermits SentryBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BSentryPermits>();
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

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get        
        {
            return new OperationYearMonthProvider();
        }
    }

    enum Scripts
    {
        DialogKartable_Operations,
        Kartable_onPageLoad,
        DialogKartableFilter_onPageLoad,
        DialogHistory_onPageLoad,
        DialogRequestsState_onPageLoad,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridKartable_Kartable.IsCallback && !this.CallBack_GridKartable_Kartable.CausedCallback)
        {
            Page KartablePage = this;
            Ajax.Utility.GenerateMethodScripts(KartablePage);

            this.CheckLoadAccess_Kartable();
            this.Fill_DateControls_Kartable();
            this.CustomizeControls_Kartable();
            this.Fill_cmbSortBy_Kartable();
            this.SetRequestStatesStr_Kartable();
            this.SetRequestTypesStr_Kartable();
            this.SetRequestSourcesStr_Kartable();
            this.SetKartablePageSize_Personnel();
            this.CustomizeTlbKartable_Kartable();
            this.CustomizeTlbKartableFilter_Kartable();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckLoadAccess_Kartable()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
            {
                RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        this.KartableBusiness.CheckKartableLoadAccess();
                        break;
                    case RequestCaller.Survey:
                        this.SurveyBusiness.CheckSurveyedRequestsLoadAccess();
                        break;
                    case RequestCaller.Sentry:
                        this.SentryBusiness.CheckSentryLoadAccess();
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

    private void Fill_DateControls_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Survey)
            {
                this.Fill_cmbYear_Kartable();
                this.Fill_cmbMonth_Kartable();
            }
            if (RC == RequestCaller.Sentry)
                this.SetCurrentDate_Kartable();             
        }
    }

    private void SetCurrentDate_Kartable()
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
        this.hfCurrentDate_Kartable.Value = strCurrentDate;
    }

    private void CustomizeControls_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Survey)
            {
                this.lblYear_Kartable.Visible = true;
                this.lblMonth_Kartable.Visible = true;
                this.cmbYear_Kartable.Visible = true;
                this.cmbMonth_Kartable.Visible = true;
                if (RC == RequestCaller.Kartable)
                    this.SelectAllinthisPageBox_Kartable.Visible = true;
            }
            if (RC == RequestCaller.Sentry)
            {
                this.lblDate_Kartable.Visible = true;
                this.ViewCurrentLangCalendars_Kartable();
            }
        }
    }

    private void ViewCurrentLangCalendars_Kartable()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_Kartable.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_Kartable.Visible = true;
                break;
        }
    }

    private void SetKartablePageSize_Personnel()
    {
        this.hfKartablePageSize_Kartable.Value = this.GridKartable_Kartable.PageSize.ToString();
    }

    private void SetKartablePageCount_Kartable(CallBackEventArgs e)
    {
        RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0]));
        string LoadState = this.StringBuilder.CreateString(e.Parameters[1]);
        switch (LoadState)
        {
            case "CustomFilter":
                this.SetKartablePageCount_Kartable(LoadState, RC, int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
                break;
            case "Search":
                this.SetKartablePageCount_Kartable(LoadState, RC, int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
                break;
            default:
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        this.SetKartablePageCount_Kartable((RequestType)Enum.Parse(typeof(RequestType), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])));
                        break;
                    case RequestCaller.Survey:
                        this.SetKartablePageCount_Kartable((RequestState)Enum.Parse(typeof(RequestState), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])));
                        break;
                    case RequestCaller.Sentry:
                        this.SetKartablePageCount_Kartable((RequestType)Enum.Parse(typeof(RequestType), LoadState), this.StringBuilder.CreateString(e.Parameters[4]));
                        break;
                }
                break;
        }
    }

    private void SetKartablePageCount_Kartable(RequestType RT, int Year, int Month)
    {
        int KartableCount = this.KartableBusiness.GetRequestCount(RT, Year, Month);
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private void SetKartablePageCount_Kartable(RequestState RS, int Year, int Month)
    {
        int KartableCount = this.SurveyBusiness.GetRequestCount(RS, Year, Month);
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private void SetKartablePageCount_Kartable(RequestType RT, string date)
    {
        int KartableCount = this.SentryBusiness.GetPermitCount(RT, date);
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private void SetKartablePageCount_Kartable(string LoadState, RequestCaller RC, int year, int month, string date, string StrFilterConditions)
    {
        int KartableCount = 0;
        switch (LoadState)
        {
            case "CustomFilter":
                IList<RequestFliterProxy> CustomFilterList = GetKartableCustomFilterList_Kartable(StrFilterConditions);
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        KartableCount = this.KartableBusiness.GetRequestsByFilterCount(CustomFilterList);
                        break;
                    case RequestCaller.Survey:
                    //    KartableCount = this.SurveyBusiness.GetRequestsByFilterCount(CustomFilterList);
                        break;
                }
                break;
            case "Search":
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        KartableCount = this.KartableBusiness.GetRequestCount(StrFilterConditions, year, month);
                        break;
                    case RequestCaller.Survey:
                        KartableCount = this.SurveyBusiness.GetRequestCount(StrFilterConditions, year, month);
                        break;
                    case RequestCaller.Sentry:
                        KartableCount = this.SentryBusiness.GetPermitCount(StrFilterConditions, date);
                        break;
                }
                break;
        }
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private IList<RequestFliterProxy> GetKartableCustomFilterList_Kartable(string StrRequestFliterProxyList)
    {
        IList<RequestFliterProxy> CustomFilterList = new List<RequestFliterProxy>();
        return CustomFilterList;
    }

    private void SetRequestStatesStr_Kartable()
    {
        string strRequestStates = string.Empty;
        foreach (RequestState requestStateItem in Enum.GetValues(typeof(RequestState)))
        {
            strRequestStates += "#" + GetLocalResourceObject(requestStateItem.ToString()).ToString() + ":" + ((int)requestStateItem).ToString();
        }
        this.hfRequestStates_Kartable.Value = strRequestStates;
    }

    private void SetRequestTypesStr_Kartable()
    {
        string strRequestTypes = string.Empty;
        foreach (RequestType requestTypeItem in Enum.GetValues(typeof(RequestType)))
        {
            strRequestTypes += "#" + GetLocalResourceObject(requestTypeItem.ToString()).ToString() + ":" + ((int)requestTypeItem).ToString();
        }
        this.hfRequestTypes_Kartable.Value = strRequestTypes;
    }

    private void SetRequestSourcesStr_Kartable()
    {
        string strRequestSources = string.Empty;
        foreach (RequestSource requestSourceItem in Enum.GetValues(typeof(RequestSource)))
        {
            strRequestSources += "#" + GetLocalResourceObject(requestSourceItem.ToString()).ToString() + ":" + ((int)requestSourceItem).ToString();
        }
        this.hfRequestSources_Kartable.Value = strRequestSources;
    }

    private void Fill_cmbYear_Kartable()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_Kartable, this.hfCurrentYear_Kartable);
    }

    private void Fill_cmbMonth_Kartable()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_Kartable, this.hfCurrentMonth_Kartable);
    }

    private void Fill_cmbSortBy_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            ComboBoxItem cmbItemSortBy = null;
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Survey)
            {
                foreach (KartablOrderBy cartablOrderByItem in Enum.GetValues(typeof(KartablOrderBy)))
                {
                    cmbItemSortBy = new ComboBoxItem();
                    cmbItemSortBy.Text = GetLocalResourceObject(cartablOrderByItem.ToString()).ToString();
                    cmbItemSortBy.Value = cartablOrderByItem.ToString();
                    cmbItemSortBy.Id = ((int)cartablOrderByItem).ToString();
                    this.cmbSortBy_Kartable.Items.Add(cmbItemSortBy);
                }
            }
            if (RC == RequestCaller.Sentry)
            {
                foreach (SentryPermitsOrderBy sentryPermitsOrderByItem in Enum.GetValues(typeof(SentryPermitsOrderBy)))
                {
                    cmbItemSortBy = new ComboBoxItem();
                    cmbItemSortBy.Text = GetLocalResourceObject(sentryPermitsOrderByItem.ToString()).ToString();
                    cmbItemSortBy.Value = sentryPermitsOrderByItem.ToString();
                    cmbItemSortBy.Id = ((int)sentryPermitsOrderByItem).ToString();
                    this.cmbSortBy_Kartable.Items.Add(cmbItemSortBy);                    
                }
            }
            this.cmbSortBy_Kartable.SelectedIndex = 0;
            this.hfCurrentSortBy_Kartable.Value = this.cmbSortBy_Kartable.SelectedItem.Value;
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

    private void CustomizeTlbKartableFilter_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Sentry)
            {
                ToolBarItem tlbItemAllRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemAllRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemAllRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemAllRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemAllRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemAllRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemAllRequests_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemAllRequests_TlbKartableFilter_Kartable.ImageUrl = "all.png";

                ToolBarItem tlbItemDailyRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemDailyRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemDailyRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemDailyRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemDailyRequests_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ImageUrl = "day.png";

                ToolBarItem tlbItemHourlyRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemHourlyRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemHourlyRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ImageUrl = "clock.png";

                ToolBarItem tlbItemOverTimeJustification_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemOverTimeJustification_TlbKartableFilter_Kartable").ToString();
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemOverTimeJustification_TlbKartableFilter_Kartable_onClick();";
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ImageUrl = "Permission.png";

                ToolBarItem tlbItemImperative_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemImperative_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemImperative_TlbKartableFilter_Kartable").ToString();
                tlbItemImperative_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemImperative_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemImperative_TlbKartableFilter_Kartable_onClick();";
                tlbItemImperative_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemImperative_TlbKartableFilter_Kartable.ImageUrl = "imperative.png";


                TlbKartableFilter_Kartable.Items.Add(tlbItemAllRequests_TlbKartableFilter_Kartable);
                TlbKartableFilter_Kartable.Items.Add(tlbItemDailyRequests_TlbKartableFilter_Kartable);
                TlbKartableFilter_Kartable.Items.Add(tlbItemHourlyRequests_TlbKartableFilter_Kartable);
                TlbKartableFilter_Kartable.Items.Add(tlbItemOverTimeJustification_TlbKartableFilter_Kartable);
                TlbKartableFilter_Kartable.Items.Add(tlbItemImperative_TlbKartableFilter_Kartable);
            }
            if (RC == RequestCaller.Survey)
            {
                ToolBarItem tlbItemConfirmedRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemConfirmedRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemConfirmedRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ImageUrl = "save.png";

                ToolBarItem tlbItemRejectedRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemRejectedRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemRejectedRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ImageUrl = "cancel.png";

                ToolBarItem tlbItemDeletedRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemDeletedRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemDeletedRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ImageHeight = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ImageWidth = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ImageUrl = "remove.png";

                TlbKartableFilter_Kartable.Items.Add(tlbItemConfirmedRequests_TlbKartableFilter_Kartable);
                TlbKartableFilter_Kartable.Items.Add(tlbItemRejectedRequests_TlbKartableFilter_Kartable);
                TlbKartableFilter_Kartable.Items.Add(tlbItemDeletedRequests_TlbKartableFilter_Kartable);
            }
        }
    }

    private void CustomizeTlbKartable_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            switch (RC)
            {
                case RequestCaller.Kartable:
                    ToolBarItem tlbItemEndorsement_TlbKartable = new ToolBarItem();
                    tlbItemEndorsement_TlbKartable.Text = GetLocalResourceObject("tlbItemEndorsement_TlbKartable").ToString();
                    tlbItemEndorsement_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemEndorsement_TlbKartable.ClientSideCommand = "tlbItemEndorsement_TlbKartable_onClick();";
                    tlbItemEndorsement_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.ImageHeight = Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.ImageWidth = Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.TextImageSpacing = 5;
                    tlbItemEndorsement_TlbKartable.ImageUrl = "save.png";

                    ToolBarItem tlbItemReject_TlbKartable = new ToolBarItem();
                    tlbItemReject_TlbKartable.Text = GetLocalResourceObject("tlbItemReject_TlbKartable").ToString();
                    tlbItemReject_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemReject_TlbKartable.ClientSideCommand = "tlbItemReject_TlbKartable_onClick();";
                    tlbItemReject_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
                    tlbItemReject_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
                    tlbItemReject_TlbKartable.ImageHeight = Unit.Pixel(16);
                    tlbItemReject_TlbKartable.ImageWidth = Unit.Pixel(16);
                    tlbItemReject_TlbKartable.TextImageSpacing = 5;
                    tlbItemReject_TlbKartable.ImageUrl = "cancel.png";

                    TlbKartable.Items.Add(tlbItemEndorsement_TlbKartable);
                    TlbKartable.Items.Add(tlbItemReject_TlbKartable);
                    break;
                case RequestCaller.Survey:
                    ToolBarItem tlbItemDelete_TlbKartable = new ToolBarItem();
                    tlbItemDelete_TlbKartable.Text = GetLocalResourceObject("tlbItemDelete_TlbKartable").ToString();
                    tlbItemDelete_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemDelete_TlbKartable.ClientSideCommand = "tlbItemDelete_TlbKartable_onClick();";
                    tlbItemDelete_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.ImageHeight = Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.ImageWidth = Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.TextImageSpacing = 5;
                    tlbItemDelete_TlbKartable.ImageUrl = "remove.png";

                    TlbKartable.Items.Add(tlbItemDelete_TlbKartable);
                    break;
            }

            ToolBarItem tlbItemHistory_TlbKartable = new ToolBarItem();
            tlbItemHistory_TlbKartable.Text = GetLocalResourceObject("tlbItemHistory_TlbKartable").ToString();
            tlbItemHistory_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemHistory_TlbKartable.ClientSideCommand = "tlbItemHistory_TlbKartable_onClick();";
            tlbItemHistory_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemHistory_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemHistory_TlbKartable.ImageHeight = Unit.Pixel(16);
            tlbItemHistory_TlbKartable.ImageWidth = Unit.Pixel(16);
            tlbItemHistory_TlbKartable.TextImageSpacing = 5;
            tlbItemHistory_TlbKartable.ImageUrl = "history.png";

            ToolBarItem tlbItemFilter_TlbKartable = new ToolBarItem();
            tlbItemFilter_TlbKartable.Text = GetLocalResourceObject("tlbItemFilter_TlbKartable").ToString();
            tlbItemFilter_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemFilter_TlbKartable.ClientSideCommand = "tlbItemFilter_TlbKartable_onClick();";
            tlbItemFilter_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemFilter_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemFilter_TlbKartable.ImageHeight = Unit.Pixel(16);
            tlbItemFilter_TlbKartable.ImageWidth = Unit.Pixel(16);
            tlbItemFilter_TlbKartable.TextImageSpacing = 5;
            tlbItemFilter_TlbKartable.ImageUrl = "filter.png";
            tlbItemFilter_TlbKartable.Enabled = false;

            ToolBarItem tlbItemHelp_TlbKartable = new ToolBarItem();
            tlbItemHelp_TlbKartable.Text = GetLocalResourceObject("tlbItemHelp_TlbKartable").ToString();
            tlbItemHelp_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemHelp_TlbKartable.ClientSideCommand = "tlbItemHelp_TlbKartable_onClick();";
            tlbItemHelp_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemHelp_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemHelp_TlbKartable.ImageHeight = Unit.Pixel(16);
            tlbItemHelp_TlbKartable.ImageWidth = Unit.Pixel(16);
            tlbItemHelp_TlbKartable.TextImageSpacing = 5;
            tlbItemHelp_TlbKartable.ImageUrl = "help.gif";

            ToolBarItem tlbItemFormReconstruction_TlbKartable = new ToolBarItem();
            tlbItemFormReconstruction_TlbKartable.Text = GetLocalResourceObject("tlbItemFormReconstruction_TlbKartable").ToString();
            tlbItemFormReconstruction_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemFormReconstruction_TlbKartable.ClientSideCommand = "tlbItemFormReconstruction_TlbKartable_onClick();";
            tlbItemFormReconstruction_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.ImageHeight = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.ImageWidth = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.TextImageSpacing = 5;
            tlbItemFormReconstruction_TlbKartable.ImageUrl = "refresh.png";


            ToolBarItem tlbItemExit_TlbKartable = new ToolBarItem();
            tlbItemExit_TlbKartable.Text = GetLocalResourceObject("tlbItemExit_TlbKartable").ToString();
            tlbItemExit_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemExit_TlbKartable.ClientSideCommand = "tlbItemExit_TlbKartable_onClick();";
            tlbItemExit_TlbKartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemExit_TlbKartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemExit_TlbKartable.ImageHeight = Unit.Pixel(16);
            tlbItemExit_TlbKartable.ImageWidth = Unit.Pixel(16);
            tlbItemExit_TlbKartable.TextImageSpacing = 5;
            tlbItemExit_TlbKartable.ImageUrl = "exit.png";

            if (RC == RequestCaller.Kartable || RC == RequestCaller.Survey)
            {
                TlbKartable.Items.Add(tlbItemHistory_TlbKartable);
                //TlbKartable.Items.Add(tlbItemFilter_TlbKartable);
            }
            TlbKartable.Items.Add(tlbItemHelp_TlbKartable);
            TlbKartable.Items.Add(tlbItemFormReconstruction_TlbKartable);
            TlbKartable.Items.Add(tlbItemExit_TlbKartable);
        }

    }

    protected void CallBack_GridKartable_Kartable_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Customize_GridKartable_Kartable((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])));
        this.Fill_GridKartable_Kartable((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]), this.StringBuilder.CreateString(e.Parameters[6]), int.Parse(this.StringBuilder.CreateString(e.Parameters[7])), int.Parse(this.StringBuilder.CreateString(e.Parameters[8])));
        this.SetKartablePageCount_Kartable(e);
        this.GridKartable_Kartable.RenderControl(e.Output);
        this.hfKartableCount_Kartable.RenderControl(e.Output);
        this.hfKartablePageCount_Kartable.RenderControl(e.Output);
        this.ErrorHiddenField_Kartable.RenderControl(e.Output);
    }

    private void  Fill_GridKartable_Kartable(RequestCaller RC, string LoadState, int year, int month, string date, string filterString, string sortBy, int pageSize, int pageIndex)
    {
        string[] retMessage = new string[4];
        IList<KartablProxy> KartableList = null;
        try
        {
            switch (LoadState)
            {
                case "CustomFilter":
                    IList<RequestFliterProxy> CustomFilterList = this.GetKartableCustomFilterList_Kartable(filterString);
                    switch (RC)
                    {
                        case RequestCaller.Kartable:
                            KartableList = this.KartableBusiness.GetAllRequestsByFilter(CustomFilterList, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Survey:
                          //  KartableList = this.SurveyBusiness.GetAllRequestsByFilter(CustomFilterList, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                    }
                    break;
                case "Search":
                    switch (RC)
                    {
                        case RequestCaller.Kartable:
                            KartableList = this.KartableBusiness.GetAllRequests(filterString, year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Survey:
                            KartableList = this.SurveyBusiness.GetAllRequests(filterString, year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Sentry:
                            KartableList = this.SentryBusiness.GetAllPermits(filterString, date, pageIndex, pageSize, (SentryPermitsOrderBy)Enum.Parse(typeof(SentryPermitsOrderBy), sortBy));
                            break;
                    }
                    break;
                default:
                    switch (RC)
                    {
                        case RequestCaller.Kartable:
                            KartableList = this.KartableBusiness.GetAllRequests((RequestType)Enum.Parse(typeof(RequestType), LoadState), year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Survey:
                            KartableList = this.SurveyBusiness.GetAllRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Sentry:
                            KartableList = this.SentryBusiness.GetAllPermits((RequestType)Enum.Parse(typeof(RequestType), LoadState), date, pageIndex, pageSize, (SentryPermitsOrderBy)Enum.Parse(typeof(SentryPermitsOrderBy), sortBy));
                            break;
                    }
                    break;
            }
            if(RC != RequestCaller.Sentry)
               this.operationYearMonthProvider.SetOperationYearMonth(year, month);
            this.GridKartable_Kartable.DataSource = KartableList;
            this.GridKartable_Kartable.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Customize_GridKartable_Kartable(RequestCaller RC)
    {
        switch (RC)
        {
            case RequestCaller.Kartable:
                this.GridKartable_Kartable.Levels[0].Columns[2].Visible = false;
                break;
            case RequestCaller.Survey:
                this.GridKartable_Kartable.Levels[0].Columns[6].Visible = false;
                break;
            case RequestCaller.Sentry:
                int[] hiddenSentryColumnsIndexList = new int[] {2,3,5,6,15,16,17,19};
                foreach (int hiddenSentryColumnsIndexListItem in hiddenSentryColumnsIndexList)
                {
                    this.GridKartable_Kartable.Levels[0].Columns[hiddenSentryColumnsIndexListItem].Visible = false;    
                }
                break;
        }
    }

    [Ajax.AjaxMethod("UpdateKartable_KartablePage", "UpdateKartable_KartablePage_onCallBack", null, null)]
    public string[] UpdateKartable_KartablePage(string Caller, string PageState, string StrSelectedRequestsList, string ActionDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        bool State = false;
        ActionDescription = this.StringBuilder.CreateString(ActionDescription);
        try
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(Caller));
            RequestState RS = (RequestState)Enum.Parse(typeof(RequestState), this.StringBuilder.CreateString(PageState));
            if (StrSelectedRequestsList == string.Empty || StrSelectedRequestsList == null)
            {
                switch (RS)
                {
                    case RequestState.Confirmed:
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRequestSelectedforConfirm").ToString()), retMessage);
                        return retMessage;
                    case RequestState.Unconfirmed:
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRequestSelectedforReject").ToString()), retMessage);
                        return retMessage;
                    case RequestState.Deleted:
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRequestSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                }
            }

            IList<KartableSetStatusProxy> KartableSetStatusProxyList = this.CreateSelectedRequestsList_Kartable(this.StringBuilder.CreateString(StrSelectedRequestsList));
            switch (RC)
            {
                case RequestCaller.Kartable:
                    switch (RS)
	                {
                        case RequestState.Confirmed:
                            State = this.KartableBusiness.ConfirmRequest(KartableSetStatusProxyList, RS, ActionDescription);
                            break;
                        case RequestState.Unconfirmed:
                            State = this.KartableBusiness.UnconfirmRequest(KartableSetStatusProxyList, RS, ActionDescription);
                            break;
	                }
                    break;
                case RequestCaller.Survey:
                    SurveyBusiness.DeleteRequst(KartableSetStatusProxyList[0].RequestID, ActionDescription);
                    State = true;
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (RS)
            {
                case RequestState.Confirmed:
                    SuccessMessageBody = GetLocalResourceObject("ConfirmComplete").ToString();
                    break;
                case RequestState.Unconfirmed:
                    SuccessMessageBody = GetLocalResourceObject("RejectComplete").ToString();
                    break;
                case RequestState.Deleted:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = State.ToString().ToLower();
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

    private IList<KartableSetStatusProxy> CreateSelectedRequestsList_Kartable(string StrSelectedRequestsList)
    {
        IList<KartableSetStatusProxy> KartableSetStatusProxyList = new List<KartableSetStatusProxy>();
        StrSelectedRequestsList = StrSelectedRequestsList.Replace("RID=", string.Empty).Replace("MFID=", string.Empty);
        string[] SelectedRequestsCol = StrSelectedRequestsList.Split(new char[] { '#' });
        foreach (string SelectedRequestsColPart in SelectedRequestsCol)
        {
            if (SelectedRequestsColPart != string.Empty)
            {
                string[] SelectedRequestsColPartDetails = SelectedRequestsColPart.Split(new char[] { '%' });
                KartableSetStatusProxy kartableSetStatusProxy = new KartableSetStatusProxy();
                kartableSetStatusProxy.RequestID = decimal.Parse(SelectedRequestsColPartDetails[0]);
                kartableSetStatusProxy.ManagerFlowID = decimal.Parse(SelectedRequestsColPartDetails[1]);
                KartableSetStatusProxyList.Add(kartableSetStatusProxy);
            }
        }
        return KartableSetStatusProxyList;
    }



}