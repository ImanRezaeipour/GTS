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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.UI;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;

public partial class PrivateMessage : GTSBasePage
{
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }
     public User user
    {
        get
        {
            return new User();
        }
    }
     public BPrivateMessage privateMessageBusiness
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
        PrivateMessage_onPageLoad,
        tbPrivateMessage_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations,
        DialogSystemMessage_onPageLoad,
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CallBack_GridPrivateMessageReceive_PrivateMessage.IsCallback && !CallBack_GridPrivateMessageSend_PrivateMessage.IsCallback)
        {
            Page UsersPage = this;
            Ajax.Utility.GenerateMethodScripts(UsersPage);

            this.CheckPrivateMessagesLoadAccess_PrivateMessage();
            SetPrivateMessageReceivePageSize_PrivateMessage();
            SetPrivateMessageSendPageSize_PrivateMessage();
            SetPrivateMessageReceivePageCount_PrivateMessage();
            SetPrivateMessageSendPageCount_PrivateMessage();
            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckPrivateMessagesLoadAccess_PrivateMessage()
    {
        string[] retMessage = new string[4];
        try
        {
            this.privateMessageBusiness.CheckPrivateMessagesLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripPrivateMessageMenus);
    }

    private void SetPrivateMessageReceivePageCount_PrivateMessage()
    {
        int PrivateMessageCount = 0;
        PrivateMessageCount = privateMessageBusiness.GetAllRecievedMessagesCount();
        this.hfPrivateMessageReceiveCount_PrivateMessage.Value = PrivateMessageCount.ToString();
        this.hfPrivateMessageReceivePageCount_PrivateMessage.Value = Utility.GetPageCount(PrivateMessageCount, this.GridPrivateMessageReceive_PrivateMessage.PageSize).ToString();
    }
    private void SetPrivateMessageSendPageCount_PrivateMessage()
    {
        int PrivateMessageCount = 0;
        PrivateMessageCount = privateMessageBusiness.GetAllSentMessageCount();
        this.hfPrivateMessageSendCount_PrivateMessage.Value = PrivateMessageCount.ToString();
        this.hfPrivateMessageSendPageCount_PrivateMessage.Value = Utility.GetPageCount(PrivateMessageCount, this.GridPrivateMessageSend_PrivateMessage.PageSize).ToString();
    }
    private void SetPrivateMessageReceivePageSize_PrivateMessage()
    {
        this.hfPrivateMessageReceivePageSize_PrivateMessage.Value = this.GridPrivateMessageReceive_PrivateMessage.PageSize.ToString();
    }
    private void SetPrivateMessageSendPageSize_PrivateMessage()
    {
        this.hfPrivateMessageSendPageSize_PrivateMessage.Value = this.GridPrivateMessageSend_PrivateMessage.PageSize.ToString();
    }
    protected void CallBack_GridPrivateMessageReceive_PrivateMessage_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        GTS.Clock.Model.Security.User user=new GTS.Clock.Model.Security.User();
        user=BUser.CurrentUser;
        SetPrivateMessageReceivePageCount_PrivateMessage();
        this.Fill_GridPrivateMessageReceive_PrivateMessage(int.Parse(this.StringBuilder.CreateString(e.Parameters[0])),int.Parse(this.StringBuilder.CreateString(e.Parameters[1])),user.Person.ID);
        this.GridPrivateMessageReceive_PrivateMessage.RenderControl(e.Output);
        this.hfPrivateMessageReceivePageCount_PrivateMessage.RenderControl(e.Output);
         this.errorHiddenFieldReceive_PrivateMessage.RenderControl(e.Output);
        
    }
    
    protected void CallBack_GridPrivateMessageSend_PrivateMessage_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        GTS.Clock.Model.Security.User user = new GTS.Clock.Model.Security.User();
        user = BUser.CurrentUser;
        SetPrivateMessageSendPageCount_PrivateMessage();
        this.Fill_GridPrivateMessageSend_PrivateMessage(int.Parse(this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), user.Person.ID);
        this.GridPrivateMessageSend_PrivateMessage.RenderControl(e.Output);
        this.hfPrivateMessageSendPageCount_PrivateMessage.RenderControl(e.Output);
        this.errorHiddenFieldSend_PrivateMessage.RenderControl(e.Output);

        
    }
    private void Fill_GridPrivateMessageReceive_PrivateMessage(int pageSize, int pageIndex, decimal userID)
    {
        string[] retMessage = new string[4];
        try
        {
           
            IList<GTS.Clock.Model.BaseInformation.PrivateMessage> PrivateMessageReceiveList = this.privateMessageBusiness.GetAllRecievedMessages(pageIndex, pageSize);
            
            this.GridPrivateMessageReceive_PrivateMessage.DataSource = PrivateMessageReceiveList;
            this.GridPrivateMessageReceive_PrivateMessage.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.errorHiddenFieldReceive_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.errorHiddenFieldReceive_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.errorHiddenFieldReceive_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.errorHiddenFieldReceive_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    private void Fill_GridPrivateMessageSend_PrivateMessage(int pageSize, int pageIndex, decimal userID)
    {
        string[] retMessage = new string[4];
        
        try
        {                       
            IList<GTS.Clock.Model.BaseInformation.PrivateMessage> PrivateMessageSendList = this.privateMessageBusiness.GetAllSentMessage(pageIndex, pageSize);
            this.GridPrivateMessageSend_PrivateMessage.DataSource = PrivateMessageSendList;
            this.GridPrivateMessageSend_PrivateMessage.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.errorHiddenFieldSend_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.errorHiddenFieldSend_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.errorHiddenFieldSend_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.errorHiddenFieldSend_PrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    [Ajax.AjaxMethod("UpdatePrivateMessageReceive_PrivateMessagePage", "UpdatePrivateMessageReceive_PrivateMessagePage_onCallBack", null, null)]
    public string[] UpdatePrivateMessageReceive_PrivateMessagePage(string state, string  SelectedMessageID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            string strSelectedMessages = this.StringBuilder.CreateString(SelectedMessageID);
            string[] ArraySelectedMessages = strSelectedMessages.Split(',');
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            switch (uam)
            {                
                case UIActionType.DELETE:
                    if (strSelectedMessages.Length == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoMessageSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.privateMessageBusiness.CheckDeletePrivateMessageAccess();
                    decimal[] SelectedMessages = Array.ConvertAll<string, decimal>(ArraySelectedMessages, Convert.ToDecimal);
                    privateMessageBusiness.DeleteFromInbox(SelectedMessages.ToList());
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = "";
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
    [Ajax.AjaxMethod("UpdatePrivateMessageSend_PrivateMessagePage", "UpdatePrivateMessageSend_PrivateMessagePage_onCallBack", null, null)]
    public string[] UpdatePrivateMessageSend_PrivateMessagePage(string state, string SelectedMessageID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

         try
        {
            string strSelectedMessages = this.StringBuilder.CreateString(SelectedMessageID);
            string[] ArraySelectedMessages = strSelectedMessages.Split(',');
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            switch (uam)
            {
                 
                case UIActionType.DELETE:
                    if (strSelectedMessages.Length == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoMessageSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.privateMessageBusiness.CheckDeletePrivateMessageAccess();
                    decimal[] SelectedMessages = Array.ConvertAll<string, decimal>(ArraySelectedMessages, Convert.ToDecimal);
                    privateMessageBusiness.DeleteFromSentBox(SelectedMessages.ToList());
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = "";
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
    [Ajax.AjaxMethod("NavigateRead_PrivateMessageReceive_PrivateMessagePage", "NavigateRead_PrivateMessageReceive_PrivateMessagePage_onCallBack", null, null)]
    public string[] NavigateRead_PrivateMessageReceive_PrivateMessagePage(string SelectedMessageID)
    {
        this.InitializeCulture();
        decimal SelectedMessages =Convert.ToDecimal(this.StringBuilder.CreateString(SelectedMessageID));
        string[] retMessage = new string[4];
        try
        {
            privateMessageBusiness.SetMessageAsRead(SelectedMessages);
                       
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = "";
            retMessage[2] = "success";
            retMessage[3] = "";
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
    [Ajax.AjaxMethod("NavigateRead_PrivateMessageSend_PrivateMessagePage", "NavigateRead_PrivateMessageSend_PrivateMessagePage_onCallBack", null, null)]
    public string[] NavigateRead_PrivateMessageSend_PrivateMessagePage(string SelectedMessageID)
    {
        this.InitializeCulture();
        string SelectedMessages = this.StringBuilder.CreateString(SelectedMessageID);
        string[] retMessage = new string[4];
        try
        {               
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = "";
            retMessage[2] = "success";
            retMessage[3] = "";
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
 