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
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.UI;

public partial class History : GTSBasePage
{
    internal class RequestHistory
    {
        public bool IsLeave { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string UesedInMonth { get; set; }
        public string UesedInYear { get; set; }
        public string RemainLeaveInMonth { get; set; }
        public string RemainLeaveInYear { get; set; }
    }

    public IKartablRequests KartableBusiness
    {
        get
        {
            return (IKartablRequests)(new BKartabl());

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
        History_onPageLoad,
        DialogHistory_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page HistoryPage = this;
        Ajax.Utility.GenerateMethodScripts(HistoryPage);
        this.SetRequestHistory_History();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }

    private void SetRequestHistory_History()
    {
        string[] retMessage = new string[4];
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestID"))
            {
                decimal requestID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestID"]));
                KartablRequestHistoryProxy kartablRequestHistoryProxy = this.KartableBusiness.GetRequestHistory(requestID);
                RequestHistory requestHistory = new RequestHistory();
                requestHistory.IsLeave = kartablRequestHistoryProxy.IsLeave;
                requestHistory.From = kartablRequestHistoryProxy.From != null ? kartablRequestHistoryProxy.From : string.Empty;
                requestHistory.To = kartablRequestHistoryProxy.To != null ? kartablRequestHistoryProxy.To : string.Empty;
                requestHistory.UesedInMonth = kartablRequestHistoryProxy.UesedInMonth != null ? kartablRequestHistoryProxy.UesedInMonth : string.Empty;
                requestHistory.UesedInYear = kartablRequestHistoryProxy.UesedInYear != null ? kartablRequestHistoryProxy.UesedInYear : string.Empty;
                requestHistory.RemainLeaveInMonth = kartablRequestHistoryProxy.RemainLeaveInMonth != null ? kartablRequestHistoryProxy.RemainLeaveInMonth : string.Empty;
                requestHistory.RemainLeaveInYear = kartablRequestHistoryProxy.RemainLeaveInYear != null ? kartablRequestHistoryProxy.RemainLeaveInYear : string.Empty;
                this.hfHistory_History.Value = JsSerializer.Serialize(requestHistory);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_History.Value = JsSerializer.Serialize(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_History.Value = JsSerializer.Serialize(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_History.Value = JsSerializer.Serialize(retMessage);
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

}