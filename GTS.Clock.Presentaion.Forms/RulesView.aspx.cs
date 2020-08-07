using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using System.Data;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model;
using ComponentArt.Web.UI;

public partial class RulesView : GTSBasePage
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
        RulesView_onPageLoad,
        iFrameRulesView_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridRules_RulesView.IsCallback)
            ClearSessions_RulesView();
        else
            if (this.GridRules_RulesView.CausedCallback)
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RuleGroupID"))
                    this.Fill_GridRules_RulesView(decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RuleGroupID"])));
            }
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));

        this.GridRules_RulesView.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridRules_RulesView_NeedRebind);
        this.GridRules_RulesView.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(GridRules_RulesView_NeedDataSource);
        this.GridRules_RulesView.NeedChildDataSource += new ComponentArt.Web.UI.Grid.NeedChildDataSourceEventHandler(GridRules_RulesView_NeedChildDataSource);
    }

    public BRuleViewer GetRuleViewerBusiness(decimal RuleGroupID)
    {
        return BusinessHelper.GetBusinessInstance<BRuleViewer>(new KeyValuePair<string, object>("ruleCategoryId", RuleGroupID));
    }


    private void ClearSessions_RulesView()
    {
        Session["RulesSource_RulesView"] = null;
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

    protected void CallBack_GridRules_RulesView_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.Fill_GridRules_RulesView(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
        this.ErrorHiddenField_RulesView.RenderControl(e.Output);
        this.GridRules_RulesView.RenderControl(e.Output);
    }

    private void Fill_GridRules_RulesView(decimal RuleGroupID)
    {
        string[] retMessage = new string[4];
        IList<RuleType> RulesGroupsList = null;
        try
        {
            if (Session["RulesSource_RulesView"] == null)
            {
                BRuleViewer RulesViewerBusiness = this.GetRuleViewerBusiness(RuleGroupID);
                RulesGroupsList = RulesViewerBusiness.GetAllRuleTypes();
                foreach (RuleType ruleGroupItem in RulesGroupsList)
                {
                    ruleGroupItem.Name = GetLocalResourceObject(ruleGroupItem.Name).ToString();
                }
                Session.Add("RulesSource_RulesView", RulesGroupsList);
            }
            this.GridRules_RulesView.DataSource = (IList<RuleType>)Session["RulesSource_RulesView"];
            this.GridRules_RulesView.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RulesView.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RulesView.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RulesView.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    void GridRules_RulesView_NeedChildDataSource(object sender, ComponentArt.Web.UI.GridNeedChildDataSourceEventArgs e)
    {
        this.FillChilds_GridRules_RulesView(e);
    }

    private void FillChilds_GridRules_RulesView(GridNeedChildDataSourceEventArgs e)
    {
        if (e.Item.Level == 0)
        {
            decimal RuleGroupID = 0;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RuleGroupID"))
                RuleGroupID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RuleGroupID"]));

            BRuleViewer RulesViewerBusiness = this.GetRuleViewerBusiness(RuleGroupID);
            IList<GTS.Clock.Model.Rule> RulesList = RulesViewerBusiness.GetAllRules((decimal)e.Item["ID"]);
            e.DataSource = RulesList;
        }
    }

    void GridRules_RulesView_NeedDataSource(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RuleGroupID"))
        { 
            decimal RuleGroupID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RuleGroupID"]));
            this.Fill_GridRules_RulesView(RuleGroupID);
        }
    }

    void GridRules_RulesView_NeedRebind(object sender, EventArgs e)
    {
        this.GridRules_RulesView.DataBind();
    }

}