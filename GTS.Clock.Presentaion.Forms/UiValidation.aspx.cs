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
using GTS.Clock.Business.UIValidation;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Infrastructure.Exceptions;

public partial class UiValidation : GTSBasePage
{
    public BUIValidationGroup UiValidationBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BUIValidationGroup>();
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
        UiValidation_onPageLoad,
        tbUiValidation_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridUiValidation_UiValidation.CausedCallback)
        {
            Page UiValidation = this;
            Ajax.Utility.GenerateMethodScripts(UiValidation);

            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckUIValidationLoadAccess_UiValidation();
        }
    }

    private void CheckUIValidationLoadAccess_UiValidation()
    {
        string[] retMessage = new string[4];
        try
        {
            this.UiValidationBusiness.CheckUIValidationLoadAccess();
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

    protected void CallBack_GridUiValidation_UiValidation_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.Fill_GridUiValidation_UiValidation();
        this.GridUiValidation_UiValidation.RenderControl(e.Output);
        this.ErrorHiddenField_UiValidation.RenderControl(e.Output);
    }
    private void Fill_GridUiValidation_UiValidation()
    {
        string[] retMessage = new string[4];
        try
        {

            this.InitializeCulture();
            IList<UIValidationGroup> uiValidationList = this.UiValidationBusiness.GetAll();
            this.GridUiValidation_UiValidation.DataSource = uiValidationList;
            this.GridUiValidation_UiValidation.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_UiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_UiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_UiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    [Ajax.AjaxMethod("UpdateUiValidation_UiValidationPage", "UpdateUiValidation_UiValidationPage_onCallBack", null, null)]
    public string[] UpdateUiValidation_UiValidationPage(string state, string SelectedUiValidationID, string UiValidationCode, string UiValidationName)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal uiValidationID = 0;
            decimal selectedUiValidationID = decimal.Parse(this.StringBuilder.CreateString(SelectedUiValidationID));
            UiValidationCode = this.StringBuilder.CreateString(UiValidationCode);
            UiValidationName = this.StringBuilder.CreateString(UiValidationName);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            UIValidationGroup uiValidation = new UIValidationGroup();
            uiValidation.ID = selectedUiValidationID;
            if (uam != UIActionType.DELETE)
            {

                uiValidation.CustomCode = UiValidationCode;
                uiValidation.Name = UiValidationName;

            }

            switch (uam)
            {
                case UIActionType.ADD:
                    uiValidationID = this.UiValidationBusiness.InsertUIValidationGroup(uiValidation, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedUiValidationID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoUiValidationSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    uiValidationID = this.UiValidationBusiness.UpdateUIValidationGroup(uiValidation, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedUiValidationID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoUiValidationSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    uiValidationID = this.UiValidationBusiness.DeleteUIValidationGroup(uiValidation, uam);
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
            retMessage[3] = uiValidationID.ToString();
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