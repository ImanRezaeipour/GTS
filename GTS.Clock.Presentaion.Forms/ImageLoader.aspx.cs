using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using GTS.Clock.Business;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.IO;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using System.Web.Configuration;

public partial class ImageLoader : GTSBasePage
{
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public BPerson PersonBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            LocalLanguageResource Llr = LocalLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentLanguage())
            {
                case "fa-IR":
                    Llr = LocalLanguageResource.Parsi;
                    break;
                case "en-US":
                    Llr = LocalLanguageResource.English;
                    break;
            }
            return new BPerson(Slr, Llr);
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("AttachmentType") && HttpContext.Current.Request.QueryString.AllKeys.Contains("ClientAttachment"))
        {
            byte[] buffer = null;
            string attachmentKey = string.Empty;
            AttachmentType attachmentType = (AttachmentType)Enum.Parse(typeof(AttachmentType), HttpContext.Current.Request.QueryString["AttachmentType"]);
            string ClientAttachment = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ClientAttachment"]);
            switch (attachmentType)
            {
                case AttachmentType.Personnel:
                    attachmentKey = "PersonnelImagesPath";
                    if (File.Exists(WebConfigurationManager.AppSettings[attachmentKey] + "\\" + ClientAttachment))
                        buffer = File.ReadAllBytes(WebConfigurationManager.AppSettings[attachmentKey] + "\\" + ClientAttachment);
                    else
                    {
                        if (File.Exists(Server.MapPath("ClientAttachments\\user.png")))
                            buffer = File.ReadAllBytes(Server.MapPath("ClientAttachments\\user.png"));
                    }
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    break;
            }
        }


        //byte[] buffer = GetImageByteArray(HttpContext.Current.Request.QueryString["PersonnelID"]);
        //HttpContext.Current.Response.ContentType = "image/jpg";
        //if(buffer != null)
        //   HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
    }

    //private byte[] GetImageByteArray(string ID)
    //{
    //    byte[] b = null;
    //    ID = ID.Replace("\"", "");
    //    string ImageHexString = this.PersonBusiness.GetPersonImage(decimal.Parse(ID));
    //    if(ImageHexString != null)
    //        b = Enumerable.Range(0, ImageHexString.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(ImageHexString.Substring(x, 2), 16)).ToArray();
    //    else
    //    {
    //        if (File.Exists(Server.MapPath("ClientAttachments\\user.png")))
    //        {
    //            string filePath = Server.MapPath("ClientAttachments\\user.png");
    //            b = File.ReadAllBytes(filePath);
    //        }
    //    }           
    //    return b;        
    //}
}