using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using ComponentArt.Web.UI;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;

public partial class Illnesses : GTSBasePage
{
    public BIllness IllnessBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BIllness>();
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
        Illnesses_onPageLoad,
        tbIllnesses_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridIllness_Illness.IsCallback)
        {
            Page IllnessesPage = this;
            Ajax.Utility.GenerateMethodScripts(IllnessesPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckIllnessesLoadAccess_Illness();
        }
    }

    private void CheckIllnessesLoadAccess_Illness()
    {
        string[] retMessage = new string[4];
        try
        {
            this.IllnessBusiness.CheckIllnessesLoadAccess();
        }
        catch (UIBaseException ex)
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

    protected void CallBack_GridIllness_Illness_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridIllness_Illness();
        this.GridIllness_Illness.RenderControl(e.Output);
        this.ErrorHiddenField_Illness.RenderControl(e.Output);
    }

    private void Fill_GridIllness_Illness()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<Illness> illnessesList = this.IllnessBusiness.GetAll();
            this.GridIllness_Illness.DataSource = illnessesList;
            this.GridIllness_Illness.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Illness.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Illness.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Illness.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateIllness_IllnessPage", "UpdateIllness_IllnessPage_onCallBack", null, null)]
    public string[] UpdateIllness_IllnessPage(string state, string SelectedIllnessID, string IllnessName, string IllnessDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal IllnessID = 0;
            decimal selectedIllnessID = decimal.Parse(this.StringBuilder.CreateString(SelectedIllnessID));
            IllnessName = this.StringBuilder.CreateString(IllnessName);
            IllnessDescription = this.StringBuilder.CreateString(IllnessDescription);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            GTS.Clock.Model.BaseInformation.Illness illness = new GTS.Clock.Model.BaseInformation.Illness();
            illness.ID = selectedIllnessID;
            if (uam != UIActionType.DELETE)
            {
                illness.Name = IllnessName;
                illness.Description = IllnessDescription;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    IllnessID = this.IllnessBusiness.InsertIllness(illness, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedIllnessID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoIllnessSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    IllnessID = this.IllnessBusiness.UpdateIllness(illness, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedIllnessID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoIllnessSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    IllnessID = this.IllnessBusiness.DeleteIllness(illness, uam);
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
            retMessage[3] = IllnessID.ToString();
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