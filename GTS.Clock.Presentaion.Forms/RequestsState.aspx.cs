using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

public partial class RequestsState : GTSBasePage
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
        RequestsState_onPageLoad,
        DialogRequestsState_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page RequestsStatePage = this;
        Ajax.Utility.GenerateMethodScripts(RequestsStatePage);
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

    [Ajax.AjaxMethod("GetBoxesHeaders_RequestsStatePage", "GetBoxesHeaders_RequestsStatePage_onCallBack", null, null)]
    public string[] GetBoxesHeaders_RequestsStatePage()
    {
        this.InitializeCulture();
        string[] retMessage = new string[1];
        retMessage[0] = GetLocalResourceObject("Title_DialogRequestsState").ToString();
        return retMessage;
    }

}