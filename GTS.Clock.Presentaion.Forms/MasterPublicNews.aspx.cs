using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Business.Shifts;
using System.IO;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.UI;
using GTS.Clock.Infrastructure.Exceptions;

public partial class MasterPublicNews : GTSBasePage
{

    public BPublicMessage PublicMessageBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BPublicMessage>();
        }
    }
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
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
        MasterPublicNews_onPageLoad,
        tbMasterPublicNews_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridMasterPublicNews_MasterPublicNews.CausedCallback)
        {
            Page MasterPublicNews = this;
            Ajax.Utility.GenerateMethodScripts(MasterPublicNews);

            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckMasterPublicNewsLoadAccess_MasterPublicNews();
        }
    }

    private void CheckMasterPublicNewsLoadAccess_MasterPublicNews()
    {
        string[] retMessage = new string[4];
        try
        {
            this.PublicMessageBusiness.CheckPublicNewsLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    /// <summary>
    /// تنظیم زبان انتخابی کاربر 
    /// </summary>
    /// <param name="LangID"></param>
    private void SetCurrentCultureResObjs(string LangID)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }


    protected void CallBack_GridMasterPublicNews_MasterPublicNews_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.Fill_MasterPublicNews_MasterPublicNews();
        this.GridMasterPublicNews_MasterPublicNews.RenderControl(e.Output);
        this.ErrorHiddenField_MasterPublicNews.RenderControl(e.Output);
    }
    private void Fill_MasterPublicNews_MasterPublicNews()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<PublicMessage> PublicMessageList = this.PublicMessageBusiness.GetAll();
            this.GridMasterPublicNews_MasterPublicNews.DataSource = PublicMessageList;
            this.GridMasterPublicNews_MasterPublicNews.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MasterPublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MasterPublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MasterPublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    [Ajax.AjaxMethod("UpdateMasterPublicNews_MasterPublicNewsPage", "UpdateMasterPublicNews_MasterPublicNewsPage_onCallBack", null, null)]
    public string[] UpdateMasterPublicNews_MasterPublicNewsPage(string state, string SelectedMasterPublicNewsID, string MasterPublicNewsActive, string MasterPublicSubject, string MasterPublicNewsMessage)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal MasterPublicNewsID = 0;
            decimal selectedMasterPublicNewsID = decimal.Parse(this.StringBuilder.CreateString(SelectedMasterPublicNewsID));
            MasterPublicNewsActive = this.StringBuilder.CreateString(MasterPublicNewsActive);
            MasterPublicSubject = this.StringBuilder.CreateString(MasterPublicSubject);
            MasterPublicNewsMessage = this.StringBuilder.CreateString(MasterPublicNewsMessage);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            PublicMessage publicMessage = new PublicMessage();
            publicMessage.ID = selectedMasterPublicNewsID;
            if (uam != UIActionType.DELETE)
            {
                publicMessage.Active = Convert.ToBoolean(MasterPublicNewsActive);
                publicMessage.Subject = MasterPublicSubject;
                publicMessage.Message = MasterPublicNewsMessage;
                publicMessage.Date = DateTime.Now;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    MasterPublicNewsID = this.PublicMessageBusiness.InsertPublicNews(publicMessage, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedMasterPublicNewsID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoMasterPublicNewsSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    MasterPublicNewsID = this.PublicMessageBusiness.UpdatePublicNews(publicMessage, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedMasterPublicNewsID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoMasterPublicNewsSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    MasterPublicNewsID = this.PublicMessageBusiness.DeletePublicNews(publicMessage, uam);
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.ADD:
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = MasterPublicNewsID.ToString();
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

}