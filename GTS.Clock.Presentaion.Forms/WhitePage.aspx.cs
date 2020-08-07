using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;

public partial class WhitePage : GTSBasePage
{
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
        WhiteForm_onPageLoad
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetError_WhitePage();
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

    private void GetError_WhitePage()
    {
        LiteralControl liError = null;
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Error"))
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            object[] ErrorMessage = (object[])JsSerializer.DeserializeObject(HttpContext.Current.Request.QueryString["Error"]);
            liError = new LiteralControl("<table><tr><td>" + ErrorMessage[0].ToString() + ":" + "</td></tr><tr><td>" + ErrorMessage[1].ToString() + "</td></tr></table>");
        }
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("DownloadError"))
            liError = new LiteralControl("<table><tr><td>" + GetLocalResourceObject("DownloadIllegalAccess").ToString() + "</td></tr></table>");
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("IllegalServiceAccess"))
            liError = new LiteralControl("<table><tr><td>" + HttpContext.Current.Request.QueryString["IllegalServiceAccess"] + "</td></tr></table>");
        if(liError != null)
           this.Form.Controls.Add(liError);
    }
}