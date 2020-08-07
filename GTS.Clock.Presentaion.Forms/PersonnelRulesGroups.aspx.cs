using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.Assignments;
using System.Collections;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

public partial class PersonnelRulesGroups : GTSBasePage
{
    public BAssignRule PersonnelRuleBusiness
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
            return new BAssignRule(Slr);
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
        PersonnelRulesGroups_onPageLoad,
        DialogPersonnelRulesGroups_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups.IsCallback && !CallBackcmbRulesGroups_PersonnelRulesGroups.IsCallback)
        {
            Page PersonnelRulesGroupsPage = this;
            Ajax.Utility.GenerateMethodScripts(PersonnelRulesGroupsPage);

            this.ViewCurrentLangCalendars_PersonnelRulesGroups();
            this.SetCurrentDate_PersonnelRulesGroups();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void ViewCurrentLangCalendars_PersonnelRulesGroups()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_PersonnelRulesGroups.Visible = true;
                this.Container_pdpToDate_PersonnelRulesGroups.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_PersonnelRulesGroups.Visible = true;
                this.Container_gdpToDate_PersonnelRulesGroups.Visible = true;
                break;
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


    private void SetCurrentDate_PersonnelRulesGroups()
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
        this.hfCurrentDate_PersonnelRulesGroups.Value = strCurrentDate;
    }

    protected void CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridPersonnelRulesGroups_PersonnelRulesGroups(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
        this.GridPersonnelRulesGroups_PersonnelRulesGroups.RenderControl(e.Output);
        this.ErrorHiddenField_PersonnelRulesGroups_PersonnelRulesGroups.RenderControl(e.Output);
    }

    private void Fill_GridPersonnelRulesGroups_PersonnelRulesGroups(decimal PersonID)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            IList<PersonRuleCatAssignment> PersonnelRulesGroupsList = this.PersonnelRuleBusiness.GetAll(PersonID);
            this.GridPersonnelRulesGroups_PersonnelRulesGroups.DataSource = PersonnelRulesGroupsList;
            this.GridPersonnelRulesGroups_PersonnelRulesGroups.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PersonnelRulesGroups_PersonnelRulesGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PersonnelRulesGroups_PersonnelRulesGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PersonnelRulesGroups_PersonnelRulesGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }         
    }

    protected void CallBackcmbRulesGroups_PersonnelRulesGroups_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbRulesGroups_PersonnelRulesGroups.Dispose();
        this.Fill_cmbRulesGroups_PersonnelRulesGroups();
        this.cmbRulesGroups_PersonnelRulesGroups.Enabled = true;
        this.ErrorHiddenField_RulesGroups_PersonnelRulesGroups.RenderControl(e.Output);
        this.cmbRulesGroups_PersonnelRulesGroups.RenderControl(e.Output);
    }

    private void Fill_cmbRulesGroups_PersonnelRulesGroups()
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            IList<RuleCategory> RulesGroupsList = this.PersonnelRuleBusiness.GetAllRuleCategories();
            this.cmbRulesGroups_PersonnelRulesGroups.DataSource = RulesGroupsList;
            this.cmbRulesGroups_PersonnelRulesGroups.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RulesGroups_PersonnelRulesGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RulesGroups_PersonnelRulesGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RulesGroups_PersonnelRulesGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdatePersonnelRuleGroup_PersonnelRulesGroupsPage", "UpdatePersonnelRuleGroup_PersonnelRulesGroupsPage_onCallBack", null, null)]
    public string[] UpdatePersonnelRuleGroup_PersonnelRulesGroupsPage(string state, string SelectedPersonnelRuleGroupID, string PersonnelID, string RuleGroupID, string RuleGroupName, string FromDate, string ToDate)
    {
        this.InitializeCulture();

        string[] retMessage = new string[5];

        decimal PersonnelRuleGroupID = 0;
        decimal selectedPersonnelRuleGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedPersonnelRuleGroupID));
        decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
        decimal ruleGroupID = decimal.Parse(this.StringBuilder.CreateString(RuleGroupID));
        RuleGroupName = this.StringBuilder.CreateString(RuleGroupName);
        FromDate = this.StringBuilder.CreateString(FromDate);
        ToDate = this.StringBuilder.CreateString(ToDate);

        PersonRuleCatAssignment personnelRuleGroup = new PersonRuleCatAssignment();
        UIActionType uam = UIActionType.ADD;
        try
        {
            switch (this.StringBuilder.CreateString(state))
            {
                case "Add":
                    uam = UIActionType.ADD;
                    break;
                case "Edit":
                    uam = UIActionType.EDIT;
                    if (selectedPersonnelRuleGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPersonnelRuleGroupSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    break;
                case "Delete":
                    uam = UIActionType.DELETE;
                    if (selectedPersonnelRuleGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPersonnelRuleGroupSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    break;
                default:
                    break;
            }

            personnelRuleGroup.ID = selectedPersonnelRuleGroupID;
            if (uam != UIActionType.DELETE)
            {
                Person person = new Person();
                person.ID = personnelID;
                personnelRuleGroup.Person = person;
                RuleCategory ruleGroup = new RuleCategory();
                ruleGroup.ID = ruleGroupID;
                ruleGroup.Name = RuleGroupName;
                personnelRuleGroup.RuleCategory = ruleGroup;
                personnelRuleGroup.UIFromDate = FromDate;
                personnelRuleGroup.UIToDate = ToDate;
            }
            PersonnelRuleGroupID = this.PersonnelRuleBusiness.SaveChanges(personnelRuleGroup, uam);


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
            retMessage[3] = PersonnelRuleGroupID.ToString();
            retMessage[4] = this.PersonBusiness.GetCurrentActiveRuleGroup(personnelID);
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