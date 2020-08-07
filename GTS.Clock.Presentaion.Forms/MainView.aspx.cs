using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Business.UI;

public partial class MainView : GTSBasePage
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
        MainView_onPageLoad,
        tbMainView_Operations,
        DialogMainViewMaximizedPart_onPageLoad
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
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


}