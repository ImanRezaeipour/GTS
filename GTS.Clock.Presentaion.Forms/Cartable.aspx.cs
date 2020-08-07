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

public partial class Cartable : GTSBasePage
{
    public enum RequestCaller
    {
        Cartable,
        Survey,
        Sentry
    }

    public IKartablRequests CartableBusiness
    {
        get
        {
            return (IKartablRequests)(new BKartabl());

        }
    }

    public IReviewedRequests SurveyBusiness
    {
        get
        {
            return (IReviewedRequests)(new BKartabl());
        }
    }

    public BSentryPermits SentryBusiness
    {
        get
        {
            return new BSentryPermits();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridCartable_Cartable.IsCallback && !this.CallBack_GridCartable_Cartable.CausedCallback)
        {
            Page CartablePage = this;
            Ajax.Utility.GenerateMethodScripts(this.GetType(), ref CartablePage);

            this.Fill_DateControls_Cartable();
            this.CustomizeControls_Cartable();
            this.Fill_cmbSortBy_Cartable();
            this.SetRequestStatesStr_Cartable();
            this.SetRequestTypesStr_Cartable();
            this.SetRequestSourcesStr_Cartable();
            this.SetCartablePageSize_Personnel();
            this.CustomizeTlbCartable_Cartable();
            this.CustomizeTlbCartableFilter_Cartable();
            SkinHelper.InitializeSkin(this.Page);
        }
    }

    private void Fill_DateControls_Cartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Cartable || RC == RequestCaller.Survey)
            {
                this.Fill_cmbYear_Cartable();
                this.Fill_cmbMonth_Cartable();
            }
            if (RC == RequestCaller.Sentry)
                this.SetCurrentDate_Cartable();
             
        }
    }

    private void SetCurrentDate_Cartable()
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
        this.hfCurrentDate_Cartable.Value = strCurrentDate;
    }

    private void CustomizeControls_Cartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Cartable || RC == RequestCaller.Survey)
            {
                this.lblYear_Cartable.Visible = true;
                this.lblMonth_Cartable.Visible = true;
                this.cmbYear_Cartable.Visible = true;
                this.cmbMonth_Cartable.Visible = true;
                if (RC == RequestCaller.Cartable)
                    this.SelectAllinthisPageBox_Cartable.Visible = true;
            }
            if (RC == RequestCaller.Sentry)
            {
                this.lblDate_Cartable.Visible = true;
                this.ViewCurrentLangCalendars_Cartable();
            }
        }
    }

    private void ViewCurrentLangCalendars_Cartable()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_Cartable.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_Cartable.Visible = true;
                break;
        }
    }

    private void SetCartablePageSize_Personnel()
    {
        this.hfCartablePageSize_Cartable.Value = this.GridCartable_Cartable.PageSize.ToString();
    }

    private void SetCartablePageCount_Cartable(CallBackEventArgs e)
    {
        RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0]));
        string LoadState = this.StringBuilder.CreateString(e.Parameters[1]);
        switch (LoadState)
        {
            case "CustomFilter":
                this.SetCartablePageCount_Cartable(LoadState, RC, int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
                break;
            case "Search":
                this.SetCartablePageCount_Cartable(LoadState, RC, int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
                break;
            default:
                switch (RC)
                {
                    case RequestCaller.Cartable:
                        this.SetCartablePageCount_Cartable((RequestType)Enum.Parse(typeof(RequestType), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])));
                        break;
                    case RequestCaller.Survey:
                        this.SetCartablePageCount_Cartable((RequestState)Enum.Parse(typeof(RequestState), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])));
                        break;
                    case RequestCaller.Sentry:
                        this.SetCartablePageCount_Cartable((RequestType)Enum.Parse(typeof(RequestType), LoadState), this.StringBuilder.CreateString(e.Parameters[4]));
                        break;
                }
                break;
        }
    }

    private void SetCartablePageCount_Cartable(RequestType RT, int Year, int Month)
    {
        int CartableCount = this.CartableBusiness.GetRequestCount(RT, Year, Month);
        this.hfCartableCount_Cartable.Value = CartableCount.ToString();
        this.hfCartablePageCount_Cartable.Value = Utility.GetPageCount(CartableCount, this.GridCartable_Cartable.PageSize).ToString();
    }

    private void SetCartablePageCount_Cartable(RequestState RS, int Year, int Month)
    {
        int CartableCount = this.SurveyBusiness.GetRequestCount(RS, Year, Month);
        this.hfCartableCount_Cartable.Value = CartableCount.ToString();
        this.hfCartablePageCount_Cartable.Value = Utility.GetPageCount(CartableCount, this.GridCartable_Cartable.PageSize).ToString();
    }

    private void SetCartablePageCount_Cartable(RequestType RT, string date)
    {
        int CartableCount = this.SentryBusiness.GetPermitCount(RT, date);
        this.hfCartableCount_Cartable.Value = CartableCount.ToString();
        this.hfCartablePageCount_Cartable.Value = Utility.GetPageCount(CartableCount, this.GridCartable_Cartable.PageSize).ToString();
    }

    private void SetCartablePageCount_Cartable(string LoadState, RequestCaller RC, int year, int month, string date, string StrFilterConditions)
    {
        int CartableCount = 0;
        switch (LoadState)
        {
            case "CustomFilter":
                IList<RequestFliterProxy> CustomFilterList = GetCartableCustomFilterList_Cartable(StrFilterConditions);
                switch (RC)
                {
                    case RequestCaller.Cartable:
                        CartableCount = this.CartableBusiness.GetRequestsByFilterCount(CustomFilterList);
                        break;
                    case RequestCaller.Survey:
                        CartableCount = this.SurveyBusiness.GetRequestsByFilterCount(CustomFilterList);
                        break;
                }
                break;
            case "Search":
                switch (RC)
                {
                    case RequestCaller.Cartable:
                        CartableCount = this.CartableBusiness.GetRequestCount(StrFilterConditions, year, month);
                        break;
                    case RequestCaller.Survey:
                        CartableCount = this.SurveyBusiness.GetRequestCount(StrFilterConditions, year, month);
                        break;
                    case RequestCaller.Sentry:
                        CartableCount = this.SentryBusiness.GetPermitCount(StrFilterConditions, date);
                        break;
                }
                break;
        }
        this.hfCartableCount_Cartable.Value = CartableCount.ToString();
        this.hfCartablePageCount_Cartable.Value = Utility.GetPageCount(CartableCount, this.GridCartable_Cartable.PageSize).ToString();
    }

    private IList<RequestFliterProxy> GetCartableCustomFilterList_Cartable(string StrRequestFliterProxyList)
    {
        IList<RequestFliterProxy> CustomFilterList = new List<RequestFliterProxy>();
        return CustomFilterList;
    }

    private void SetRequestStatesStr_Cartable()
    {
        string strRequestStates = string.Empty;
        foreach (RequestState requestStateItem in Enum.GetValues(typeof(RequestState)))
        {
            strRequestStates += "#" + GetLocalResourceObject(requestStateItem.ToString()).ToString() + ":" + ((int)requestStateItem).ToString();
        }
        this.hfRequestStates_Cartable.Value = strRequestStates;
    }

    private void SetRequestTypesStr_Cartable()
    {
        string strRequestTypes = string.Empty;
        foreach (RequestType requestTypeItem in Enum.GetValues(typeof(RequestType)))
        {
            strRequestTypes += "#" + GetLocalResourceObject(requestTypeItem.ToString()).ToString() + ":" + ((int)requestTypeItem).ToString();
        }
        this.hfRequestTypes_Cartable.Value = strRequestTypes;
    }

    private void SetRequestSourcesStr_Cartable()
    {
        string strRequestSources = string.Empty;
        foreach (RequestSource requestSourceItem in Enum.GetValues(typeof(RequestSource)))
        {
            strRequestSources += "#" + GetLocalResourceObject(requestSourceItem.ToString()).ToString() + ":" + ((int)requestSourceItem).ToString();
        }
        this.hfRequestSources_Cartable.Value = strRequestSources;
    }

    private void Fill_cmbYear_Cartable()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_Cartable, this.hfCurrentYear_Cartable);
    }

    private void Fill_cmbMonth_Cartable()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_Cartable, this.hfCurrentMonth_Cartable);
    }

    private void Fill_cmbSortBy_Cartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            ComboBoxItem cmbItemSortBy = null;
            if (RC == RequestCaller.Cartable || RC == RequestCaller.Survey)
            {
                foreach (KartablOrderBy cartablOrderByItem in Enum.GetValues(typeof(KartablOrderBy)))
                {
                    cmbItemSortBy = new ComboBoxItem();
                    cmbItemSortBy.Text = GetLocalResourceObject(cartablOrderByItem.ToString()).ToString();
                    cmbItemSortBy.Value = cartablOrderByItem.ToString();
                    cmbItemSortBy.Id = ((int)cartablOrderByItem).ToString();
                    this.cmbSortBy_Cartable.Items.Add(cmbItemSortBy);
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
                    this.cmbSortBy_Cartable.Items.Add(cmbItemSortBy);                    
                }
            }
            this.cmbSortBy_Cartable.SelectedIndex = 0;
            this.hfCurrentSortBy_Cartable.Value = this.cmbSortBy_Cartable.SelectedItem.Value;
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

    private void CustomizeTlbCartableFilter_Cartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Cartable || RC == RequestCaller.Sentry)
            {
                ToolBarItem tlbItemAllRequests_TlbCartableFilter_Cartable = new ToolBarItem();
                tlbItemAllRequests_TlbCartableFilter_Cartable.Text = GetLocalResourceObject("tlbItemAllRequests_TlbCartableFilter_Cartable").ToString();
                tlbItemAllRequests_TlbCartableFilter_Cartable.ItemType = ToolBarItemType.Command;
                tlbItemAllRequests_TlbCartableFilter_Cartable.ClientSideCommand = "tlbItemAllRequests_TlbCartableFilter_Cartable_onClick();";
                tlbItemAllRequests_TlbCartableFilter_Cartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemAllRequests_TlbCartableFilter_Cartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemAllRequests_TlbCartableFilter_Cartable.ImageHeight = Unit.Pixel(16);
                tlbItemAllRequests_TlbCartableFilter_Cartable.ImageWidth = Unit.Pixel(16);
                tlbItemAllRequests_TlbCartableFilter_Cartable.TextImageSpacing = 5;
                tlbItemAllRequests_TlbCartableFilter_Cartable.ImageUrl = "all.png";

                ToolBarItem tlbItemDailyRequests_TlbCartableFilter_Cartable = new ToolBarItem();
                tlbItemDailyRequests_TlbCartableFilter_Cartable.Text = GetLocalResourceObject("tlbItemDailyRequests_TlbCartableFilter_Cartable").ToString();
                tlbItemDailyRequests_TlbCartableFilter_Cartable.ItemType = ToolBarItemType.Command;
                tlbItemDailyRequests_TlbCartableFilter_Cartable.ClientSideCommand = "tlbItemDailyRequests_TlbCartableFilter_Cartable_onClick();";
                tlbItemDailyRequests_TlbCartableFilter_Cartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemDailyRequests_TlbCartableFilter_Cartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemDailyRequests_TlbCartableFilter_Cartable.ImageHeight = Unit.Pixel(16);
                tlbItemDailyRequests_TlbCartableFilter_Cartable.ImageWidth = Unit.Pixel(16);
                tlbItemDailyRequests_TlbCartableFilter_Cartable.TextImageSpacing = 5;
                tlbItemDailyRequests_TlbCartableFilter_Cartable.ImageUrl = "day.png";

                ToolBarItem tlbItemHourlyRequests_TlbCartableFilter_Cartable = new ToolBarItem();
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.Text = GetLocalResourceObject("tlbItemHourlyRequests_TlbCartableFilter_Cartable").ToString();
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.ItemType = ToolBarItemType.Command;
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.ClientSideCommand = "tlbItemHourlyRequests_TlbCartableFilter_Cartable_onClick();";
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.ImageHeight = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.ImageWidth = Unit.Pixel(16);
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.TextImageSpacing = 5;
                tlbItemHourlyRequests_TlbCartableFilter_Cartable.ImageUrl = "clock.png";

                ToolBarItem tlbItemOverTimeJustification_TlbCartableFilter_Cartable = new ToolBarItem();
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.Text = GetLocalResourceObject("tlbItemOverTimeJustification_TlbCartableFilter_Cartable").ToString();
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.ItemType = ToolBarItemType.Command;
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.ClientSideCommand = "tlbItemOverTimeJustification_TlbCartableFilter_Cartable_onClick();";
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.ImageHeight = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.ImageWidth = Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.TextImageSpacing = 5;
                tlbItemOverTimeJustification_TlbCartableFilter_Cartable.ImageUrl = "Permission.png";

                TlbCartableFilter_Cartable.Items.Add(tlbItemAllRequests_TlbCartableFilter_Cartable);
                TlbCartableFilter_Cartable.Items.Add(tlbItemDailyRequests_TlbCartableFilter_Cartable);
                TlbCartableFilter_Cartable.Items.Add(tlbItemHourlyRequests_TlbCartableFilter_Cartable);
                TlbCartableFilter_Cartable.Items.Add(tlbItemOverTimeJustification_TlbCartableFilter_Cartable);
            }
            if (RC == RequestCaller.Survey)
            {
                ToolBarItem tlbItemConfirmedRequests_TlbCartableFilter_Cartable = new ToolBarItem();
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.Text = GetLocalResourceObject("tlbItemConfirmedRequests_TlbCartableFilter_Cartable").ToString();
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.ItemType = ToolBarItemType.Command;
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.ClientSideCommand = "tlbItemConfirmedRequests_TlbCartableFilter_Cartable_onClick();";
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.ImageHeight = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.ImageWidth = Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.TextImageSpacing = 5;
                tlbItemConfirmedRequests_TlbCartableFilter_Cartable.ImageUrl = "save.png";

                ToolBarItem tlbItemRejectedRequests_TlbCartableFilter_Cartable = new ToolBarItem();
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.Text = GetLocalResourceObject("tlbItemRejectedRequests_TlbCartableFilter_Cartable").ToString();
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.ItemType = ToolBarItemType.Command;
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.ClientSideCommand = "tlbItemRejectedRequests_TlbCartableFilter_Cartable_onClick();";
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.ImageHeight = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.ImageWidth = Unit.Pixel(16);
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.TextImageSpacing = 5;
                tlbItemRejectedRequests_TlbCartableFilter_Cartable.ImageUrl = "cancel.png";

                ToolBarItem tlbItemDeletedRequests_TlbCartableFilter_Cartable = new ToolBarItem();
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.Text = GetLocalResourceObject("tlbItemDeletedRequests_TlbCartableFilter_Cartable").ToString();
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.ItemType = ToolBarItemType.Command;
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.ClientSideCommand = "tlbItemDeletedRequests_TlbCartableFilter_Cartable_onClick();";
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.DropDownImageHeight = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.DropDownImageWidth = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.ImageHeight = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.ImageWidth = Unit.Pixel(16);
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.TextImageSpacing = 5;
                tlbItemDeletedRequests_TlbCartableFilter_Cartable.ImageUrl = "remove.png";

                TlbCartableFilter_Cartable.Items.Add(tlbItemConfirmedRequests_TlbCartableFilter_Cartable);
                TlbCartableFilter_Cartable.Items.Add(tlbItemRejectedRequests_TlbCartableFilter_Cartable);
                TlbCartableFilter_Cartable.Items.Add(tlbItemDeletedRequests_TlbCartableFilter_Cartable);
            }
        }
    }

    private void CustomizeTlbCartable_Cartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            switch (RC)
            {
                case RequestCaller.Cartable:
                    ToolBarItem tlbItemEndorsement_TlbCartable = new ToolBarItem();
                    tlbItemEndorsement_TlbCartable.Text = GetLocalResourceObject("tlbItemEndorsement_TlbCartable").ToString();
                    tlbItemEndorsement_TlbCartable.ItemType = ToolBarItemType.Command;
                    tlbItemEndorsement_TlbCartable.ClientSideCommand = "tlbItemEndorsement_TlbCartable_onClick();";
                    tlbItemEndorsement_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
                    tlbItemEndorsement_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
                    tlbItemEndorsement_TlbCartable.ImageHeight = Unit.Pixel(16);
                    tlbItemEndorsement_TlbCartable.ImageWidth = Unit.Pixel(16);
                    tlbItemEndorsement_TlbCartable.TextImageSpacing = 5;
                    tlbItemEndorsement_TlbCartable.ImageUrl = "save.png";

                    ToolBarItem tlbItemReject_TlbCartable = new ToolBarItem();
                    tlbItemReject_TlbCartable.Text = GetLocalResourceObject("tlbItemReject_TlbCartable").ToString();
                    tlbItemReject_TlbCartable.ItemType = ToolBarItemType.Command;
                    tlbItemReject_TlbCartable.ClientSideCommand = "tlbItemReject_TlbCartable_onClick();";
                    tlbItemReject_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
                    tlbItemReject_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
                    tlbItemReject_TlbCartable.ImageHeight = Unit.Pixel(16);
                    tlbItemReject_TlbCartable.ImageWidth = Unit.Pixel(16);
                    tlbItemReject_TlbCartable.TextImageSpacing = 5;
                    tlbItemReject_TlbCartable.ImageUrl = "cancel.png";

                    TlbCartable.Items.Add(tlbItemEndorsement_TlbCartable);
                    TlbCartable.Items.Add(tlbItemReject_TlbCartable);
                    break;
                case RequestCaller.Survey:
                    ToolBarItem tlbItemDelete_TlbCartable = new ToolBarItem();
                    tlbItemDelete_TlbCartable.Text = GetLocalResourceObject("tlbItemDelete_TlbCartable").ToString();
                    tlbItemDelete_TlbCartable.ItemType = ToolBarItemType.Command;
                    tlbItemDelete_TlbCartable.ClientSideCommand = "tlbItemDelete_TlbCartable_onClick();";
                    tlbItemDelete_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
                    tlbItemDelete_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
                    tlbItemDelete_TlbCartable.ImageHeight = Unit.Pixel(16);
                    tlbItemDelete_TlbCartable.ImageWidth = Unit.Pixel(16);
                    tlbItemDelete_TlbCartable.TextImageSpacing = 5;
                    tlbItemDelete_TlbCartable.ImageUrl = "remove.png";

                    TlbCartable.Items.Add(tlbItemDelete_TlbCartable);
                    break;
            }

            ToolBarItem tlbItemHistory_TlbCartable = new ToolBarItem();
            tlbItemHistory_TlbCartable.Text = GetLocalResourceObject("tlbItemHistory_TlbCartable").ToString();
            tlbItemHistory_TlbCartable.ItemType = ToolBarItemType.Command;
            tlbItemHistory_TlbCartable.ClientSideCommand = "tlbItemHistory_TlbCartable_onClick();";
            tlbItemHistory_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemHistory_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemHistory_TlbCartable.ImageHeight = Unit.Pixel(16);
            tlbItemHistory_TlbCartable.ImageWidth = Unit.Pixel(16);
            tlbItemHistory_TlbCartable.TextImageSpacing = 5;
            tlbItemHistory_TlbCartable.ImageUrl = "history.png";

            ToolBarItem tlbItemFilter_TlbCartable = new ToolBarItem();
            tlbItemFilter_TlbCartable.Text = GetLocalResourceObject("tlbItemFilter_TlbCartable").ToString();
            tlbItemFilter_TlbCartable.ItemType = ToolBarItemType.Command;
            tlbItemFilter_TlbCartable.ClientSideCommand = "tlbItemFilter_TlbCartable_onClick();";
            tlbItemFilter_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemFilter_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemFilter_TlbCartable.ImageHeight = Unit.Pixel(16);
            tlbItemFilter_TlbCartable.ImageWidth = Unit.Pixel(16);
            tlbItemFilter_TlbCartable.TextImageSpacing = 5;
            tlbItemFilter_TlbCartable.ImageUrl = "filter.png";
            tlbItemFilter_TlbCartable.Enabled = false;

            ToolBarItem tlbItemHelp_TlbCartable = new ToolBarItem();
            tlbItemHelp_TlbCartable.Text = GetLocalResourceObject("tlbItemHelp_TlbCartable").ToString();
            tlbItemHelp_TlbCartable.ItemType = ToolBarItemType.Command;
            tlbItemHelp_TlbCartable.ClientSideCommand = "tlbItemHelp_TlbCartable_onClick();";
            tlbItemHelp_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemHelp_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemHelp_TlbCartable.ImageHeight = Unit.Pixel(16);
            tlbItemHelp_TlbCartable.ImageWidth = Unit.Pixel(16);
            tlbItemHelp_TlbCartable.TextImageSpacing = 5;
            tlbItemHelp_TlbCartable.ImageUrl = "help.gif";

            ToolBarItem tlbItemFormReconstruction_TlbCartable = new ToolBarItem();
            tlbItemFormReconstruction_TlbCartable.Text = GetLocalResourceObject("tlbItemFormReconstruction_TlbCartable").ToString();
            tlbItemFormReconstruction_TlbCartable.ItemType = ToolBarItemType.Command;
            tlbItemFormReconstruction_TlbCartable.ClientSideCommand = "tlbItemFormReconstruction_TlbCartable_onClick();";
            tlbItemFormReconstruction_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbCartable.ImageHeight = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbCartable.ImageWidth = Unit.Pixel(16);
            tlbItemFormReconstruction_TlbCartable.TextImageSpacing = 5;
            tlbItemFormReconstruction_TlbCartable.ImageUrl = "refresh.png";


            ToolBarItem tlbItemExit_TlbCartable = new ToolBarItem();
            tlbItemExit_TlbCartable.Text = GetLocalResourceObject("tlbItemExit_TlbCartable").ToString();
            tlbItemExit_TlbCartable.ItemType = ToolBarItemType.Command;
            tlbItemExit_TlbCartable.ClientSideCommand = "tlbItemExit_TlbCartable_onClick();";
            tlbItemExit_TlbCartable.DropDownImageHeight = Unit.Pixel(16);
            tlbItemExit_TlbCartable.DropDownImageWidth = Unit.Pixel(16);
            tlbItemExit_TlbCartable.ImageHeight = Unit.Pixel(16);
            tlbItemExit_TlbCartable.ImageWidth = Unit.Pixel(16);
            tlbItemExit_TlbCartable.TextImageSpacing = 5;
            tlbItemExit_TlbCartable.ImageUrl = "exit.png";

            if (RC == RequestCaller.Cartable || RC == RequestCaller.Survey)
            {
                TlbCartable.Items.Add(tlbItemHistory_TlbCartable);
                //TlbCartable.Items.Add(tlbItemFilter_TlbCartable);
            }
            TlbCartable.Items.Add(tlbItemHelp_TlbCartable);
            TlbCartable.Items.Add(tlbItemFormReconstruction_TlbCartable);
            TlbCartable.Items.Add(tlbItemExit_TlbCartable);
        }

    }

    protected void CallBack_GridCartable_Cartable_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Customize_GridCartable_Cartable((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])));
        this.Fill_GridCartable_Cartable((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]), this.StringBuilder.CreateString(e.Parameters[6]), int.Parse(this.StringBuilder.CreateString(e.Parameters[7])), int.Parse(this.StringBuilder.CreateString(e.Parameters[8])));
        this.SetCartablePageCount_Cartable(e);
        this.GridCartable_Cartable.RenderControl(e.Output);
        this.hfCartableCount_Cartable.RenderControl(e.Output);
        this.hfCartablePageCount_Cartable.RenderControl(e.Output);
        this.ErrorHiddenField_Cartable.RenderControl(e.Output);
    }

    private void Fill_GridCartable_Cartable(RequestCaller RC, string LoadState, int year, int month, string date, string filterString, string sortBy, int pageSize, int pageIndex)
    {
        string[] retMessage = new string[4];
        IList<KartablProxy> CartableList = null;
        try
        {
            switch (LoadState)
            {
                case "CustomFilter":
                    IList<RequestFliterProxy> CustomFilterList = this.GetCartableCustomFilterList_Cartable(filterString);
                    switch (RC)
                    {
                        case RequestCaller.Cartable:
                            CartableList = this.CartableBusiness.GetAllRequestsByFilter(CustomFilterList, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Survey:
                            CartableList = this.SurveyBusiness.GetAllRequestsByFilter(CustomFilterList, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                    }
                    break;
                case "Search":
                    switch (RC)
                    {
                        case RequestCaller.Cartable:
                            CartableList = this.CartableBusiness.GetAllRequests(filterString, year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Survey:
                            CartableList = this.SurveyBusiness.GetAllRequests(filterString, year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Sentry:
                            CartableList = this.SentryBusiness.GetAllPermits(filterString, date, pageIndex, pageSize, (SentryPermitsOrderBy)Enum.Parse(typeof(SentryPermitsOrderBy), sortBy));
                            break;
                    }
                    break;
                default:
                    switch (RC)
                    {
                        case RequestCaller.Cartable:
                            CartableList = this.CartableBusiness.GetAllRequests((RequestType)Enum.Parse(typeof(RequestType), LoadState), year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Survey:
                            CartableList = this.SurveyBusiness.GetAllRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                        case RequestCaller.Sentry:
                            CartableList = this.SentryBusiness.GetAllPermits((RequestType)Enum.Parse(typeof(RequestType), LoadState), date, pageIndex, pageSize, (SentryPermitsOrderBy)Enum.Parse(typeof(SentryPermitsOrderBy), sortBy));
                            break;
                    }
                    break;
            }
            if(RC != RequestCaller.Sentry)
               this.operationYearMonthProvider.SetOperationYearMonth(year, month);
            this.GridCartable_Cartable.DataSource = CartableList;
            this.GridCartable_Cartable.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Cartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Cartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_Cartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Cartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Customize_GridCartable_Cartable(RequestCaller RC)
    {
        switch (RC)
        {
            case RequestCaller.Cartable:
                this.GridCartable_Cartable.Levels[0].Columns[2].Visible = false;
                break;
            case RequestCaller.Survey:
                this.GridCartable_Cartable.Levels[0].Columns[6].Visible = false;
                break;
            case RequestCaller.Sentry:
                int[] hiddenSentryColumnsIndexList = new int[] {2,3,5,6,15,16,17};
                foreach (int hiddenSentryColumnsIndexListItem in hiddenSentryColumnsIndexList)
                {
                    this.GridCartable_Cartable.Levels[0].Columns[hiddenSentryColumnsIndexListItem].Visible = false;    
                }
                break;
        }
    }

    [Ajax.AjaxMethod("UpdateCartable_CartablePage", "UpdateCartable_CartablePage_onCallBack", null, false, null)]
    public string[] UpdateCartable_CartablePage(string Caller, string PageState, string StrSelectedRequestsList, string ActionDescription)
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

            IList<KartableSetStatusProxy> KartableSetStatusProxyList = this.CreateSelectedRequestsList_Cartable(this.StringBuilder.CreateString(StrSelectedRequestsList));
            switch (RC)
            {
                case RequestCaller.Cartable:
                    State = this.CartableBusiness.SetStatusOfRequest(KartableSetStatusProxyList, RS, ActionDescription);
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

    private IList<KartableSetStatusProxy> CreateSelectedRequestsList_Cartable(string StrSelectedRequestsList)
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