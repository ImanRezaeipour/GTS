using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;

public partial class RuleParameters : GTSBasePage
{
    enum RuleParametersTimeState
    {
        Get,
        Set
    }

    internal class ParameterSource
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public bool IsInNextDay { get; set; }
    }

    internal class RuleDateRangeSource
    {
        public string ID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
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
        RuleParameters_onPageLoad,
        DialogRuleParameters_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridRuleDateRanges_RuleParameters.CausedCallback && !this.CallBack_GridRuleParameters_RuleParameters.CausedCallback)
        {
            Page RuleParametersPage = this;
            Ajax.Utility.GenerateMethodScripts(RuleParametersPage);

            this.ViewCurrentLangCalendars_RuleParameters();
            this.SetCurrentDate_RuleParameters();
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private BRuleParameter GetRuleParameterBusiness(decimal ruleID, decimal ruleGroupID)
    {
        return BusinessHelper.GetBusinessInstance<BRuleParameter>(new KeyValuePair<string, object>("ruleTmpId", ruleID), new KeyValuePair<string, object>("ruleCategoryCode", ruleGroupID));         
    }

    private void ViewCurrentLangCalendars_RuleParameters()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_RuleParameters.Visible = true;
                this.Container_pdpFromDate_RuleParameters.Visible = true;
                this.Container_pdpToDate_RuleParameters.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_RuleParameters.Visible = true;
                this.Container_gdpFromDate_RuleParameters.Visible = true;
                this.Container_gdpToDate_RuleParameters.Visible = true;
                break;
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

    protected void CallBack_GridRuleDateRanges_RuleParameters_onCallBack(object sender, CallBackEventArgs e)
    {
        UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), e.Parameters[0].ToUpper());
        decimal ruleGroupID = decimal.Parse(e.Parameters[1]);
        decimal ruleID = decimal.Parse(e.Parameters[2]);
        this.Fill_GridRuleDateRanges_RuleParameters(uam, ruleGroupID, ruleID);
        this.GridRuleDateRanges_RuleParameters.RenderControl(e.Output);
        this.ErrorHiddenField_RuleDateRanges_RuleParameters.RenderControl(e.Output);
    }

    private void Fill_GridRuleDateRanges_RuleParameters(UIActionType uam, decimal ruleGroupID, decimal ruleID)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            BRuleParameter RuleParameterBusiness = this.GetRuleParameterBusiness(ruleID, ruleGroupID);
            IList<AssignRuleParameter> RuleDateRangesList = null;
            switch (uam)
            {
                case UIActionType.ADD:
                    RuleDateRangesList = RuleParameterBusiness.GetAllAssignedRuleParameter_onRuleGroupInsert();
                    break;
                case UIActionType.EDIT:
                    RuleDateRangesList = RuleParameterBusiness.GetAllAssignedRuleParameter_onRuleGroupUpdate();
                    break;
            }
            List<RuleDateRangeSource> RuleDateRanges = new List<RuleDateRangeSource>();
            foreach (AssignRuleParameter assignRuleParameter in RuleDateRangesList)
            {
                RuleDateRangeSource ruleDateRangesDS = new RuleDateRangeSource();
                ruleDateRangesDS.ID = assignRuleParameter.ID.ToString();
                ruleDateRangesDS.FromDate = this.LangProv.GetSysDateString(assignRuleParameter.FromDate);
                ruleDateRangesDS.ToDate = this.LangProv.GetSysDateString(assignRuleParameter.ToDate);
                RuleDateRanges.Add(ruleDateRangesDS);
            }
            this.GridRuleDateRanges_RuleParameters.DataSource = RuleDateRanges;
            this.GridRuleDateRanges_RuleParameters.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }


    protected void CallBack_GridRuleParameters_RuleParameters_onCallBack(object sender, CallBackEventArgs e)
    {
        decimal ruleGroupID = decimal.Parse(e.Parameters[0]);
        decimal ruleID = decimal.Parse(e.Parameters[1]);
        decimal ruleDateRangeID = -1;
        if (e.Parameters.Length > 2)
            ruleDateRangeID = decimal.Parse(e.Parameters[2]);
        Fill_GridRuleParameters_RuleParameters(ruleGroupID, ruleID, ruleDateRangeID);
        this.GridRuleParameters_RuleParameters.RenderControl(e.Output);
        this.ErrorHiddenField_RuleParameters_RuleParameters.RenderControl(e.Output);
    }

    private void Fill_GridRuleParameters_RuleParameters(decimal ruleGroupID, decimal ruleID, decimal ruleDateRangeID)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            BRuleParameter RuleParameterBusiness = new BRuleParameter(ruleID, ruleGroupID);
            ParameterSource parameterSource = new ParameterSource();
            List<ParameterSource> ParametersSourceList = new List<ParameterSource>();
            if (ruleDateRangeID == -1)
            {
                IList<RuleTemplateParameter> ruleParametersTemplateList = RuleParameterBusiness.GetTemplateParameters();
                foreach (RuleTemplateParameter ruleTemplateParameter in ruleParametersTemplateList)
                {                    
                    parameterSource = this.InsertParameters_RuleParameters(ruleTemplateParameter.ID, ruleTemplateParameter.Name, ruleTemplateParameter.Title, ruleTemplateParameter.Value, (RuleParamType)Enum.ToObject(typeof(RuleParamType), ruleTemplateParameter.Type), ruleTemplateParameter.ContinueOnTomorrow);
                    ParametersSourceList.Add(parameterSource);
                }
            }
            else
            {
                IList<RuleParameter> ruleParametersList = RuleParameterBusiness.GetRuleParameters(ruleDateRangeID);
                foreach (RuleParameter ruleParameter in ruleParametersList)
                {
                   parameterSource = this.InsertParameters_RuleParameters(ruleParameter.ID, ruleParameter.Name, ruleParameter.Title, ruleParameter.Value, (RuleParamType)Enum.ToObject(typeof(RuleParamType), ruleParameter.Type), ruleParameter.ContinueOnTomorrow);
                   ParametersSourceList.Add(parameterSource);
                }
            }
            this.GridRuleParameters_RuleParameters.DataSource = ParametersSourceList;
            this.GridRuleParameters_RuleParameters.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private ParameterSource InsertParameters_RuleParameters(decimal ParamID, string ParamName, string ParamTitle, string ParamValue, RuleParamType ParamType, bool IsParamValueInNextDay)
    {
        ParameterSource parameterSource = new ParameterSource();
        parameterSource.ID = ParamID.ToString();
        parameterSource.Name = ParamName;
        parameterSource.Title = ParamTitle != string.Empty ? ParamTitle : ParamName;
        parameterSource.Type = (int)ParamType;
        parameterSource.IsInNextDay = IsParamValueInNextDay;
        switch (ParamType)
        {
            case RuleParamType.Date:
                ParamValue = this.LangProv.GetSysDateString(DateTime.Parse(ParamValue));
                break;
            case RuleParamType.Time:
                if (IsParamValueInNextDay)
                    ParamValue = (int.Parse(ParamValue) - 1440).ToString();
                ParamValue = this.CreateTimeString_RuleParameters(ParamValue, RuleParametersTimeState.Get);
                if (IsParamValueInNextDay)
                    ParamValue = "+" + ParamValue;
                break;
        }
        parameterSource.Value = ParamValue;
        return parameterSource;
    }

    private string CreateTimeString_RuleParameters(string strTime, RuleParametersTimeState rpts)
    {
        string retStrTime = string.Empty;
        int hour = 0;
        int min = 0;
        string strHour = string.Empty;
        string strMin = string.Empty;
        switch (rpts)
        {
            case RuleParametersTimeState.Get:
                hour = Math.DivRem(int.Parse(strTime), 60, out min);
                strHour = hour.ToString();
                if (strHour.Length < 2)
                    strHour = "0" + strHour;
                strMin = min.ToString();
                if (strMin.Length < 2)
                    strMin = "0" + strMin;
                retStrTime = strHour + ":" + strMin;
                break;
            case RuleParametersTimeState.Set:
                string[] strTiemParts = strTime.Split(new char[] { ':' });
                hour = int.Parse(strTiemParts[0]) * 60;
                min = int.Parse(strTiemParts[1]);
                retStrTime = (hour + min).ToString();
                break;
            default:
                break;
        }
        return retStrTime;
    }

    [Ajax.AjaxMethod("UpdateRuleParameter_RuleParametersPage", "UpdateRuleParameter_RuleParametersPage_onCallBack", null, null)]
    public string[] UpdateRuleParameter_RuleParametersPage(string state, string ruleGroupState, string RuleGroupID, string RuleID, string SelectedRuleDateRangeID, string FromDate, string ToDate, string ParametersList)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal RuleDateRangeID = 0;
            decimal ruleGroupID = decimal.Parse(this.StringBuilder.CreateString(RuleGroupID));
            decimal ruleID = decimal.Parse(this.StringBuilder.CreateString(RuleID));
            decimal selectedRuleDateRangeID = decimal.Parse(this.StringBuilder.CreateString(SelectedRuleDateRangeID));
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            ParametersList = this.StringBuilder.CreateString(ParametersList);
            object parameterList = null;
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            UIActionType RGS = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(ruleGroupState).ToUpper());
            AssignRuleParameter ruleDateRange = new AssignRuleParameter();   


            decimal ID = selectedRuleDateRangeID;
            DateTime fromDate = this.LangProv.GetSysDateTime(FromDate);
            DateTime toDate = this.LangProv.GetSysDateTime(ToDate);
            BRuleParameter RuleParameterBusiness = this.GetRuleParameterBusiness(ruleID, ruleGroupID);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;

            switch (uam)
            {
                case UIActionType.ADD:
                    parameterList = this.GetParametersList_RuleParameters(uam, ParametersList);
                    switch (RGS)
	                {
                        case UIActionType.ADD:
                            RuleDateRangeID = RuleParameterBusiness.InsertRuleParameter_onRuleGroupInsert((IList<RuleTemplateParameter>)parameterList, fromDate, toDate);
                            break;
                        case UIActionType.EDIT:
                            RuleDateRangeID = RuleParameterBusiness.InsertRuleParameter_onRuleGroupUpdate((IList<RuleTemplateParameter>)parameterList, fromDate, toDate);
                            break;
	                }
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.EDIT:
                    if (selectedRuleDateRangeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleDateRangeSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    parameterList = this.GetParametersList_RuleParameters(uam, ParametersList);
                    switch (RGS)
	                {
                        case UIActionType.ADD:
                            RuleDateRangeID = RuleParameterBusiness.UpdateRuleParameter_onRuleGroupInsert((IList<RuleParameter>)parameterList, selectedRuleDateRangeID, fromDate, toDate);
                            break;
                        case UIActionType.EDIT:
                            RuleDateRangeID = RuleParameterBusiness.UpdateRuleParameter_onRuleGroupUpdate((IList<RuleParameter>)parameterList, selectedRuleDateRangeID, fromDate, toDate);
                            break;
	                }
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    if (selectedRuleDateRangeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleDateRangeSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    switch (RGS)
	                {
                        case UIActionType.ADD:
                            RuleParameterBusiness.DeleteRuleParameter_onRuleGroupInsert(selectedRuleDateRangeID);
                            break;
                        case UIActionType.EDIT:
                            RuleParameterBusiness.DeleteRuleParameter_onRuleGroupUpdate(selectedRuleDateRangeID);
                            break;
	                }
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = RuleDateRangeID.ToString();
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

    private object GetParametersList_RuleParameters(UIActionType uam, string strParametersList)
    {
        object ParametersList = null;
        switch (uam)
        {
            case UIActionType.ADD:
                ParametersList = new List<RuleTemplateParameter>();
                break;
            case UIActionType.EDIT:
                ParametersList = new List<RuleParameter>();
                break;
        }
        if (strParametersList != string.Empty)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            object[] ParamsBatchs = (object[])jsSerializer.DeserializeObject(strParametersList);
            foreach (object paramBatch in ParamsBatchs)
            {
                Dictionary<string, object> paramDic = (Dictionary<string, object>)paramBatch;
                decimal ParameterID = decimal.Parse(paramDic["ID"].ToString());
                string ParameterName = paramDic["Name"].ToString();
                string ParameterTitle = paramDic["Title"].ToString();
                RuleParamType ParameterType = (RuleParamType)Enum.ToObject(typeof(RuleParamType), int.Parse(paramDic["Type"].ToString()));
                bool IsParameterValueInNextDay = false;
                string ParameterValue = string.Empty;

                switch (ParameterType)
                {
                    case RuleParamType.Date:
                        ParameterValue = this.LangProv.GetSysDateTime(paramDic["Value"].ToString()).ToShortDateString();
                        break;
                    case RuleParamType.Time:
                        string TimeValue = string.Empty;
                        if (paramDic["Value"].ToString().Contains("+"))
                        {
                            TimeValue = paramDic["Value"].ToString().Replace("+", string.Empty);
                            IsParameterValueInNextDay = true;
                        }
                        else
                            TimeValue = paramDic["Value"].ToString();
                        ParameterValue = this.CreateTimeString_RuleParameters(TimeValue, RuleParametersTimeState.Set);
                        break;
                    default:
                        ParameterValue = paramDic["Value"].ToString();
                        break;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        RuleTemplateParameter ruleTemplateParameter = new RuleTemplateParameter();
                        ruleTemplateParameter.ID = ParameterID;
                        ruleTemplateParameter.Name = ParameterName;
                        ruleTemplateParameter.Title = ParameterTitle;
                        ruleTemplateParameter.Type = ParameterType;
                        ruleTemplateParameter.Value = ParameterValue;
                        ruleTemplateParameter.ContinueOnTomorrow = IsParameterValueInNextDay;
                        ((List<RuleTemplateParameter>)ParametersList).Add(ruleTemplateParameter);
                        break;
                    case UIActionType.EDIT:
                        RuleParameter ruleParameter = new RuleParameter();
                        ruleParameter.ID = ParameterID;
                        ruleParameter.Name = ParameterName;
                        ruleParameter.Title = ParameterTitle;
                        ruleParameter.Type = ParameterType;
                        ruleParameter.Value = ParameterValue;
                        ruleParameter.ContinueOnTomorrow = IsParameterValueInNextDay;
                        ((List<RuleParameter>)ParametersList).Add(ruleParameter);
                        break;
                }

            }
        }
        return ParametersList;
    }
}