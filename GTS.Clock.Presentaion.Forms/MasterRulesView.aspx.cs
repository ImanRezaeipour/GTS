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
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Rules;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;

public partial class MasterRulesView : GTSBasePage
{
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
        MasterRulesView_onPageLoad,
        DialogMasterRulesView_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridRuleDateRanges_MasterRulesView.IsCallback && !CallBack_GridRuleParameters_MasterRulesView.IsCallback)
        {
            Page MasterManagersPage = this;
            Ajax.Utility.GenerateMethodScripts(MasterManagersPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    protected void CallBack_GridRuleDateRanges_MasterRulesView_onCallback(object sender, CallBackEventArgs e)
    {
        this.Fill_GridRuleDateRanges_MasterRulesView(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_RuleDateRanges.RenderControl(e.Output);
        this.GridRuleDateRanges_MasterRulesView.RenderControl(e.Output);
    }

    private void Fill_GridRuleDateRanges_MasterRulesView(decimal RuleGroupID, decimal RuleID)
    {
        string[] retMessage = new string[4];
        IList<AssignRuleParameter> RuleDateRangesList = null; 
        try
        {
            BRuleViewer RulesViewerBusiness = new BRuleViewer(RuleGroupID);
            RuleDateRangesList = RulesViewerBusiness.GetAllRuleParametersRange(RuleID);
            this.GridRuleDateRanges_MasterRulesView.DataSource = RuleDateRangesList;
            this.GridRuleDateRanges_MasterRulesView.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RuleDateRanges.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridRuleParameters_MasterRulesView_onCallback(object sender, CallBackEventArgs e)
    {
        this.Fill_GridRuleParameters_MasterRulesView(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_RuleParameters.RenderControl(e.Output);
        this.GridRuleParameters_MasterRulesView.RenderControl(e.Output);
    }

    private void Fill_GridRuleParameters_MasterRulesView(decimal RuleGroupID, decimal RuleDataRangeID)
    {
        string[] retMessage = new string[4];
        IList<RuleParameter> RuleParametersList = null;
        try
        {
            BRuleViewer RulesViewerBusiness = new BRuleViewer(RuleGroupID);
            RuleParametersList = RulesViewerBusiness.GetAllRuleParameters(RuleDataRangeID);
            this.GridRuleParameters_MasterRulesView.DataSource = RuleParametersList;
            this.GridRuleParameters_MasterRulesView.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RuleParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
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