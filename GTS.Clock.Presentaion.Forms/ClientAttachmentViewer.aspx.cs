using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using GTS.Clock.Business.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using System.IO;
using GTS.Clock.Infrastructure;

public partial class ClientAttachmentViewer : GTSBasePage
{
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

    enum Scripts
    {
        ClientAttachmentViewer_Operations,
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        try
        {
            string attachmentKey = string.Empty;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("AttachmentType") && HttpContext.Current.Request.QueryString.AllKeys.Contains("ClientAttachment"))
            {
                string ClientAttachment = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ClientAttachment"]);
                AttachmentType attachmentType = (AttachmentType)Enum.Parse(typeof(AttachmentType), HttpContext.Current.Request.QueryString["AttachmentType"]);
                switch (attachmentType)
                {
                    case AttachmentType.Request:
                        attachmentKey = "RequestAttachmentsPath";
                        if (File.Exists(WebConfigurationManager.AppSettings[attachmentKey] + "\\" + ClientAttachment))
                        {
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.ContentType = "application/octet-stream";
                            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + ClientAttachment + "");
                            HttpContext.Current.Response.TransmitFile(WebConfigurationManager.AppSettings[attachmentKey] + "\\" + ClientAttachment);
                            HttpContext.Current.Response.End();
                        }
                        else
                            Response.Write(GetLocalResourceObject("FileNotFound").ToString());
                        break;
                }
            }
        }
        catch (Exception)
        {
            Response.Write(GetLocalResourceObject("FileNotFound").ToString());
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