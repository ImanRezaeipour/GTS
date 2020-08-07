using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;

public partial class ImageViewer : GTSBasePage
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
        ImageViewer_onPageLoad
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        this.ImageViewerControl.ImageUrl = "ImageLoader.aspx?&AttachmentType=Personnel&reload=" + DateTime.Now.ToLongTimeString() + "&ClientAttachment=" + HttpContext.Current.Request.QueryString["ClientAttachment"];
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


