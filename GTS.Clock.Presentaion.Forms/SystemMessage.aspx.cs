using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Infrastructure.Exceptions;

public partial class SystemMessage : GTSBasePage
{
    public BPrivateMessage SystemMessageBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BPrivateMessage>();
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
        SystemMessage_onPageLoad,
        DialogSystemMessage_Operations,
        Alert_Box,
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CheckSystemMessagesLoadAccess_SystemMessage();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }

    private void CheckSystemMessagesLoadAccess_SystemMessage()
    {
        string[] retMessage = new string[4];
        try
        {
            this.SystemMessageBusiness.CheckSystemMessageLoadAccess();
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

    private void SetCurrentCultureResObjs(string LangID)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    protected void TlbSystemMessage_ItemCheckChanged(object sender, ToolBarItemEventArgs e)
    {
        SendMessage_SystemMessage();
    }

    private void SendMessage_SystemMessage()
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            string Subject = this.txtSubject_SystemMessage.Value;
            string Message = this.txtMessage_SystemMessage.Value;

            this.SystemMessageBusiness.NewMessage(Subject, Message);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("SendComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = "";
            this.hfMessage_SystemMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.hfMessage_SystemMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.hfMessage_SystemMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.hfMessage_SystemMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
}