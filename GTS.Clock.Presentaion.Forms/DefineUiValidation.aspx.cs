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
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Model;

public partial class DefineUiValidation : GTSBasePage
{
    enum RuleParametersTimeState
    {
        Get,
        Set
    }
    public BUIValidationGroup UiValidationBusiness
    {
        get
        {
            return new BUIValidationGroup();
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

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridDefineUiValidation_DefineUiValidation.CausedCallback)
        {
            Page UiValidation = this;
            Ajax.Utility.GenerateMethodScripts(UiValidation);
            this.SetCurrentDate_RuleParameters();
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.InitializeSkin();
        }
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripRuleParametersTerms);
    }

    private void SetCurrentDate_RuleParameters()
    {
        string strCurrentDate = string.Empty;
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "en-US":
                strCurrentDate = DateTime.Now.ToShortDateString();
                break;
            case "fa-IR":
                strCurrentDate = this.LangProv.GetSysDateString(DateTime.Now);
                break;
        }
        this.hfCurrentDate_RuleParameters.Value = strCurrentDate;
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
    protected void CallBack_GridDefineUiValidation_DefineUiValidation_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        decimal selectedGroupID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]));
        this.Fill_GridDefineUiValidation_DefineUiValidation(selectedGroupID);
        this.GridDefineUiValidation_DefineUiValidation.RenderControl(e.Output);
        this.ErrorHiddenField_DefineUiValidation.RenderControl(e.Output);
    }
    private void Fill_GridDefineUiValidation_DefineUiValidation(decimal GroupID)
    {
        string[] retMessage = new string[4];
        try
        {

            this.InitializeCulture();
            IList<GTS.Clock.Business.Proxy.UIValidationGroupingProxy> uiValidationRuleList = this.UiValidationBusiness.GetRuleList(GroupID);
            this.GridDefineUiValidation_DefineUiValidation.DataSource = uiValidationRuleList;
            this.GridDefineUiValidation_DefineUiValidation.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DefineUiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DefineUiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DefineUiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    protected void CallBack_GridParameterDefineUiValidation_DefineUiValidation_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        decimal selectedGroupID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]));
        decimal selectedRuleID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]));
        this.Fill_GridParameterDefineUiValidation_DefineUiValidation(selectedGroupID, selectedRuleID);
        this.GridParameterDefineUiValidation_DefineUiValidation.RenderControl(e.Output);
        this.ErrorHiddenField_ParameterDefineUiValidation.RenderControl(e.Output);
    }
    private void Fill_GridParameterDefineUiValidation_DefineUiValidation(decimal GroupID, decimal RuleID)
    {
        string[] retMessage = new string[4];
        try
        {
            
            this.InitializeCulture();
            IList<GTS.Clock.Model.UIValidation.UIValidationRuleParameter> uiValidationRuleParameterList = this.UiValidationBusiness.GetRuleParameter(GroupID, RuleID);
            this.GridParameterDefineUiValidation_DefineUiValidation.DataSource = uiValidationRuleParameterList;
            this.GridParameterDefineUiValidation_DefineUiValidation.DataBind();

        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ParameterDefineUiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ParameterDefineUiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ParameterDefineUiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    [Ajax.AjaxMethod("UpdateDefineUiValidation_DefineUiValidationPage", "UpdateDefineUiValidation_DefineUiValidationPage_onCallBack", null, null)]
    public string[] UpdateDefineUiValidation_DefineUiValidationPage(string strListRow, string state)
    {
        this.InitializeCulture();
        string[] retMessage = new string[4];
        IList<UIValidationGroupingProxy> UIValidationGroupingProxyList = this.CreateUIValidationGroupingProxyList(this.StringBuilder.CreateString(strListRow));
        UIActionType uam = UIActionType.EDIT;

        try
        {
            switch (this.StringBuilder.CreateString(state))
            {
              
                case "Edit":
                    uam = UIActionType.EDIT;
                    if (strListRow == "")
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("nouivalidationselectedforedit").ToString()), retMessage);
                        return retMessage;
                    }
                    break;
                
                default:
                    break;
            }
            this.UiValidationBusiness.UpdateRuleList(UIValidationGroupingProxyList);


            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                
                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
               
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            // retMessage[3] = uiValidationID.ToString();
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

    private IList<UIValidationGroupingProxy> CreateUIValidationGroupingProxyList(string StrObjRuleFeatures)
    {
        IList<UIValidationGroupingProxy> UIValidationGroupingProxyList = new List<UIValidationGroupingProxy>();
        object[] ParamsBatchList = (object[])this.JsSerializer.DeserializeObject(StrObjRuleFeatures);
        foreach (object paramsBatch in ParamsBatchList)
        {
            Dictionary<string, object> paramsBatchDic = (Dictionary<string, object>)paramsBatch;
            UIValidationGroupingProxy UIVGP = new UIValidationGroupingProxy();
            UIVGP.ID = decimal.Parse(paramsBatchDic["uvID"].ToString());
            UIVGP.Active = bool.Parse(paramsBatchDic["IsA"].ToString());
            UIVGP.OpratorRestriction = bool.Parse(paramsBatchDic["IsO"].ToString());
            UIVGP.GroupID = decimal.Parse(paramsBatchDic["GID"].ToString());
            UIVGP.RuleID = decimal.Parse(paramsBatchDic["RID"].ToString());
            UIValidationGroupingProxyList.Add(UIVGP);
        }
        return UIValidationGroupingProxyList;
    }
    [Ajax.AjaxMethod("UpdateParameterDefineUiValidation_DefineUiValidationPage", "UpdateParameterDefineUiValidation_DefineUiValidationPage_onCallBack", null, null)]
    public string[] UpdateParameterDefineUiValidation_DefineUiValidationPage(string strListRow, string state)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        
        IList<UIValidationRuleParameter> UIValidationRuleParameterList = this.CreateUIValidationRuleParameterList(this.StringBuilder.CreateString(strListRow));

        UIActionType uam = UIActionType.EDIT;

        try
        {
            switch (this.StringBuilder.CreateString(state))
            {

                case "Edit":
                    uam = UIActionType.EDIT;
                    if (strListRow == "")
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("nouivalidationselectedforedit").ToString()), retMessage);
                        return retMessage;
                    }
                    break;

                default:
                    break;
            }
            this.UiValidationBusiness.UpdateRuleParameter(UIValidationRuleParameterList);


            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {

                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;

                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            // retMessage[3] = uiValidationID.ToString();
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

    private IList<UIValidationRuleParameter> CreateUIValidationRuleParameterList(string StrObjRuleParameter)
    {
        IList<UIValidationRuleParameter> UIValidationRuleParameterList = new List<UIValidationRuleParameter>();
        object[] ParamsBatchList = (object[])this.JsSerializer.DeserializeObject(StrObjRuleParameter);
        foreach (object paramsBatch in ParamsBatchList)
        {
            Dictionary<string, object> paramsBatchDic = (Dictionary<string, object>)paramsBatch;
            UIValidationRuleParameter UIVGP = new UIValidationRuleParameter();
            UIVGP.ID = decimal.Parse(paramsBatchDic["PID"].ToString());
            UIVGP.Name = paramsBatchDic["Name"].ToString();
            RuleParamType ParameterType = (RuleParamType)Enum.ToObject(typeof(RuleParamType), int.Parse(paramsBatchDic["Type"].ToString()));
            UIVGP.TheValue = paramsBatchDic["Val"].ToString();
            UIVGP.KeyName = paramsBatchDic["KN"].ToString();
       
          UIValidationRuleParameterList.Add(UIVGP);
        }
        return UIValidationRuleParameterList;
    }
    
}