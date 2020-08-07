using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model;
using System.IO;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using System.Collections;
using GTS.Clock.Business.UI;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Infrastructure;

public partial class RulesGroupUpdate : GTSBasePage
{

    public BRuleCategory RulesGroupBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BRuleCategory>();
        }
    }

    public BRuleType RuleTypeBusiness
    {
        get
        {
            return new BRuleType();
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

    internal class RuleNodeValue
    {
        public string Script { get; set; }
        public bool HasParameter { get; set; }
        public bool IsForcible { get; set; }
    }

    enum Scripts
    {
        RulesGroupUpdate_onPageLoad,
        DialogRulesGroupUpdate_Operations,
        DialogRuleParameters_onPageLoad,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_trvRulesTemplates_RulesGroupUpdate.CausedCallback && !this.CallBack_trvRules_RulesGroupUpdate.CausedCallback)
        {
            Page RulesGroupUpdatePage = this;
            Ajax.Utility.GenerateMethodScripts(RulesGroupUpdatePage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
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

    /// <summary>
    /// CallBack درخت الگوهای قوانین
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CallBack_trvRulesTemplates_RulesGroupUpdate_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_trvRulesTemplates_RulesGroupUpdate();
        this.trvRulesTemplates_RulesGroupUpdate.RenderControl(e.Output);
        this.ErrorHiddenField_RulesTemplates_RulesGroupUpdate.RenderControl(e.Output);
    }

    /// <summary>
    ///پر کردن درخت الگوهای قوانین
    /// </summary>
    private void Fill_trvRulesTemplates_RulesGroupUpdate()
    {
        string imageUrl = "Images\\TreeView\\folder.gif";
        string imagePath = "Images/TreeView/folder.gif";
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            IList<RuleType> RuleTypesList = this.RuleTypeBusiness.GetAll();
            foreach (RuleType ruleType in RuleTypesList)
            {
                TreeViewNode ruleTypeNode = new TreeViewNode();
                ruleTypeNode.ID = ruleType.ID.ToString();
                string ruleTypeNodeText = string.Empty;
                if (GetLocalResourceObject(ruleType.Name) != null)
                    ruleTypeNodeText = GetLocalResourceObject(ruleType.Name).ToString();
                else
                    ruleTypeNodeText = ruleType.Name;
                ruleTypeNode.Text = ruleTypeNodeText;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    ruleTypeNode.ImageUrl = imagePath;
                ruleTypeNode.Selectable = false;
                this.trvRulesTemplates_RulesGroupUpdate.Nodes.Add(ruleTypeNode);
                if (ruleType.RuleTemplateList.Count > 0)
                {
                    foreach (RuleTemplate ruleTemplate in ruleType.RuleTemplateList)
                    {
                        TreeViewNode ruleTemplateNode = new TreeViewNode();
                        ruleTemplateNode.ID = ruleTemplate.ID.ToString();
                        ruleTemplateNode.Text = ruleTemplate.Name;
                        ruleTemplateNode.Value = this.SetTreeViewNodeValue_RuleParameters(ruleTemplateNode, ruleTemplate.HasParameter, ruleTemplate.IsForcible, ruleTemplate.Script);
                        ruleTypeNode.Nodes.Add(ruleTemplateNode);
                    }
                }
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RulesTemplates_RulesGroupUpdate.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RulesTemplates_RulesGroupUpdate.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RulesTemplates_RulesGroupUpdate.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    /// <summary>
    /// تنظیم مقدار گره درخت
    /// </summary>
    /// <param name="trvNode">درخت مرجع</param>
    /// <param name="HasParameter">شاخص وجود پارامتر</param>
    /// <param name="Script">متن</param>
    /// <returns>رشته جیسون حاوی شاخص وجود پارامتر و متن برای هر گره درخت مرجع</returns>
    private string SetTreeViewNodeValue_RuleParameters(TreeViewNode trvNode, bool HasParameter, bool IsForcible, string Script)
    {
        string withoutParameterimageUrl = "Images\\TreeView\\folder.gif";
        string withParameterimageUrl = "Images\\TreeView\\folder_blue.gif";
        string withoutParameterImagePath = "Images/TreeView/folder.gif";
        string withParameterImagePath = "Images/TreeView/folder_blue.gif";
        if (HasParameter)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + withParameterimageUrl))
                trvNode.ImageUrl = withParameterImagePath;
            trvNode.ToolTip = GetLocalResourceObject("WithParameter").ToString();
        }
        else
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + withoutParameterimageUrl))
                trvNode.ImageUrl = withoutParameterImagePath;
            trvNode.ToolTip = GetLocalResourceObject("WithoutParameter").ToString();
        }
        RuleNodeValue rnv = new RuleNodeValue();
        rnv.HasParameter = HasParameter;
        rnv.IsForcible = IsForcible;
        rnv.Script = Script;
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        string retNodeValue = jsSerializer.Serialize(rnv);
        return retNodeValue;
    }

    /// <summary>
    /// CallBack درخت قوانین
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CallBack_trvRules_RulesGroupUpdate_onCallBack(object sender, CallBackEventArgs e)
    {
        string ActionMode = this.StringBuilder.CreateString(e.Parameters[0]);
        decimal RuleGroupID = 0;
        if (ActionMode != "Add")
            RuleGroupID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]));
        this.Fill_trvRules_RulesGroupUpdate(ActionMode, RuleGroupID);
        this.trvRules_RulesGroupUpdate.RenderControl(e.Output);
        this.ErrorHiddenField_Rules_RulesGroupUpdate.RenderControl(e.Output);
    }

    /// <summary>
    /// پرکردن درخت قوانین
    /// </summary>
    /// <param name="ActionMode">عملیات جاری</param>
    /// <param name="RuleGroupID">شناسه گروه قانون</param>
    private void Fill_trvRules_RulesGroupUpdate(string ActionMode, decimal RuleGroupID)
    {
        string imageUrl = "Images\\TreeView\\folder.gif";
        string imagePath = "Images/TreeView/folder.gif";
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            IList<RuleType> RuleTypesList = this.RuleTypeBusiness.GetAll();
            foreach (RuleType ruleType in RuleTypesList)
            {
                TreeViewNode ruleTypeNode = new TreeViewNode();
                ruleTypeNode.ID = ruleType.ID.ToString();
                string ruleTypeNodeText = string.Empty;
                if (GetLocalResourceObject(ruleType.Name) != null)
                    ruleTypeNodeText = GetLocalResourceObject(ruleType.Name).ToString();
                else
                    ruleTypeNodeText = ruleType.Name;
                ruleTypeNode.Text = ruleTypeNodeText;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    ruleTypeNode.ImageUrl = imagePath;
                ruleTypeNode.Selectable = false;
                this.trvRules_RulesGroupUpdate.Nodes.Add(ruleTypeNode);
            }

            TreeViewNode ruleNode = null;
            IList<Rule> AssignedRulesList = null;
            if (ActionMode != "Add")
            {
                AssignedRulesList = this.RulesGroupBusiness.GetRulesByRuleCatID(RuleGroupID);
                foreach (Rule assignedRule in AssignedRulesList)
                {
                    ruleNode = new TreeViewNode();
                    ruleNode.ID = assignedRule.TemplateId.ToString();
                    ruleNode.Text = assignedRule.Name;
                    ruleNode.Value = this.SetTreeViewNodeValue_RuleParameters(ruleNode, assignedRule.HasParameter, assignedRule.IsForcible, assignedRule.Script);
                    this.trvRules_RulesGroupUpdate.FindNodeById(assignedRule.TypeId.ToString()).Nodes.Add(ruleNode);
                }
            }

            IList<RuleTemplate> ForcibleRuleTemplatesList = this.RulesGroupBusiness.GetForcibleRuleTemplates();
            foreach (RuleTemplate forcibleRuleTemplateItem in ForcibleRuleTemplatesList)
            {
                if (AssignedRulesList == null || AssignedRulesList.Where(assignedRule => assignedRule.TemplateId == forcibleRuleTemplateItem.ID).Count() == 0)
                {
                    ruleNode = new TreeViewNode();
                    ruleNode.ID = forcibleRuleTemplateItem.ID.ToString();
                    ruleNode.Text = forcibleRuleTemplateItem.Name;
                    ruleNode.Value = this.SetTreeViewNodeValue_RuleParameters(ruleNode, forcibleRuleTemplateItem.HasParameter, forcibleRuleTemplateItem.IsForcible, forcibleRuleTemplateItem.Script);
                    this.trvRules_RulesGroupUpdate.FindNodeById(forcibleRuleTemplateItem.TypeId.ToString()).Nodes.Add(ruleNode);
                }
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Rules_RulesGroupUpdate.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Rules_RulesGroupUpdate.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Rules_RulesGroupUpdate.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


    /// <summary>
    /// درج و ویرایش گروه قانون
    /// </summary>
    /// <param name="state">عملیات جاری</param>
    /// <param name="SelectedRuleGroupID">شناسه گروه قانون</param>
    /// <param name="RuleGroupName">نام گروه قانون</param>
    /// <param name="DepartmentName">توضیحات گروه قانون</param>
    /// <param name="RulesList">آرایه ای از شناسه قوانین</param>
    /// <returns>آرایه ای از پیغام و شناسه</returns>
    [Ajax.AjaxMethod("UpdateRuleGroup_RulesGroupUpdatePage", "UpdateRuleGroup_RulesGroupUpdatePage_onCallBack", null, null)]
    public string[] UpdateRuleGroup_RulesGroupUpdatePage(string state, string SelectedRuleGroupID, string RuleGroupName, string RuleGroupDescriptions, string RulesList)
    {

        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal RuleGroupID = 0;
            decimal selectedRuleGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedRuleGroupID));
            RuleGroupName = this.StringBuilder.CreateString(RuleGroupName);
            RuleGroupDescriptions = this.StringBuilder.CreateString(RuleGroupDescriptions);
            Dictionary<string, decimal[]> rulesListDic = this.CreateRulesListArray_RulesGroupUpdate(this.StringBuilder.CreateString(RulesList));
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            RuleCategory ruleGroup = new RuleCategory();
            ruleGroup.Name = RuleGroupName;
            ruleGroup.Discription = RuleGroupDescriptions;

            switch (uam)
            {
                case UIActionType.ADD:
                    ruleGroup.ID = 0;
                    ruleGroup.InsertedTemplateIDs = rulesListDic["AddedRules"];
                    RuleGroupID = this.RulesGroupBusiness.InsertRuleGroup(ruleGroup, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedRuleGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleGroupSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    else
                    {
                        ruleGroup.ID = selectedRuleGroupID;
                        ruleGroup.InsertedTemplateIDs = rulesListDic["AddedRules"];
                        ruleGroup.DeletedTemplateIDs = rulesListDic["DeletedRules"];
                    }
                    RuleGroupID = this.RulesGroupBusiness.UpdateRuleGroup(ruleGroup, uam);
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
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = RuleGroupID.ToString();
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

    /// <summary>
    /// ایجاد آرایه قوانین اضافه شده و حذف شده
    /// </summary>
    /// <param name="RulesList">رشته لیست قوانین</param>
    /// <returns>آرایه هایی از شناسه قوانین اضافه شده و حذف شده</returns>
    private Dictionary<string, decimal[]> CreateRulesListArray_RulesGroupUpdate(string RulesList)
    {
        Dictionary<string, decimal[]> RulesListDic = new Dictionary<string, decimal[]>();
        ArrayList AddedRulesList = new ArrayList();
        ArrayList DeletedRulesList = new ArrayList();
        string[] RulesListParts = RulesList.Split(new char[] { '#' });
        foreach (string ruleListPart in RulesListParts)
        {
            if (ruleListPart != string.Empty)
            {
                string[] ruleListSections = ruleListPart.Split(new char[] { '%' });
                switch (ruleListSections[0])
                {
                    case "Add":
                        AddedRulesList.Add(decimal.Parse(ruleListSections[1]));
                        break;
                    case "Delete":
                        DeletedRulesList.Add(decimal.Parse(ruleListSections[1]));
                        break;
                }
            }
        }
        RulesListDic.Add("AddedRules", (decimal[])AddedRulesList.ToArray(typeof(decimal)));
        RulesListDic.Add("DeletedRules", (decimal[])DeletedRulesList.ToArray(typeof(decimal)));
        return RulesListDic;
    }

    [Ajax.AjaxMethod("ValidateRuleGroupRuleParameters_RulesGroupUpdatePage", "ValidateRuleGroupRuleParameters_RulesGroupUpdatePage_onCallBack", null, null)]
    public string[] ValidateRuleGroupRuleParameters_RulesGroupUpdatePage(string ruleGroupState, string SelectedRuleGroupID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        string WarningMessage = string.Empty;
        string splitter = " --- ";
        string space = " ";
        UIActionType RGS = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(ruleGroupState).ToUpper());
        decimal RuleGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedRuleGroupID));
        IList<RuleParametersValidationFeaturesProxy> RuleParametersValidationFeaturesProxyList = null;

        try
        {
            switch (RGS)
            {
                case UIActionType.ADD:
                    RuleParametersValidationFeaturesProxyList = this.RulesGroupBusiness.ValidateRuleGroupRulesParameters_onRuleGroupInsert(RuleGroupID);
                    break;
                case UIActionType.EDIT:
                    RuleParametersValidationFeaturesProxyList = this.RulesGroupBusiness.ValidateRuleGroupRulesParameters_onRuleGroupUpdate(RuleGroupID);
                    break;
            }
            foreach (RuleParametersValidationFeaturesProxy ruleParametersValidationFeaturesProxyItem in RuleParametersValidationFeaturesProxyList)
            {
                switch (ruleParametersValidationFeaturesProxyItem.ValidationType)
                {
                    case RuleParametersValidationType.RuleParametersNoRegulation:
                        switch (this.LangProv.GetCurrentLanguage())
                        {
                            case "fa-IR":
                                WarningMessage += GetLocalResourceObject("RuleParameters").ToString() + space + ruleParametersValidationFeaturesProxyItem.RelativeRule.Name + space + GetLocalResourceObject("IsNotValued").ToString() + splitter;
                                break;
                            case "en-US":
                                WarningMessage += ruleParametersValidationFeaturesProxyItem.RelativeRule.Name + space + GetLocalResourceObject("RuleParameters").ToString() + space + GetLocalResourceObject("IsNotValued").ToString() + splitter;
                                break;
                        }
                        break;
                    case RuleParametersValidationType.RuleParametersDateRangesNoCover:
                        Person RelativePersonnel = ruleParametersValidationFeaturesProxyItem.RelativePersonRuleCatAssignment.Person;
                        WarningMessage += GetLocalResourceObject("RuleParametersDateRanges").ToString() + space + ruleParametersValidationFeaturesProxyItem.RelativeRule.Name + space + GetLocalResourceObject("For" + RelativePersonnel.Sex.ToString()).ToString() + space + RelativePersonnel.Name + space + GetLocalResourceObject("IsNotValid").ToString() + splitter;
                        break;
                }
            }

            if (WarningMessage != string.Empty)
            {
                retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                retMessage[1] = WarningMessage;
                retMessage[2] = "warning";
            }
            else
            {
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("RuleGroupRulesParametersAreValid").ToString();
                retMessage[2] = "success";
            }
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
