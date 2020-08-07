using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using System.IO;
using GTS.Clock.Model;
using System.Collections.Specialized;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions;

public partial class SubstituteSettings : GTSBasePage
{
    public BSubstitute SubstituteBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BSubstitute>();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
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

    enum Scripts
    {
        SubstituteSettings_onPageLoad,
        DialogSubstituteSettings_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridManagerWorkFlows_SubstituteSettings.IsCallback && !CallBack_trvUnderManagementPersonnel_SubstituteSettings.IsCallback && !CallBack_trvWorkFlowAccessLevels_SubstituteSettings.IsCallback)
        {
            Page SubstituteSttingsPage = this;
            Ajax.Utility.GenerateMethodScripts(SubstituteSttingsPage);

            this.CheckSubstituteSettingsLoadAccess_SubstituteSettings();
            this.UnderManagementPersonnelLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckSubstituteSettingsLoadAccess_SubstituteSettings()
    {
        string[] retMessage = new string[4];
        try
        {
            this.SubstituteBusiness.CheckSubstituteSettingsLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        } 
    }

    private void UnderManagementPersonnelLoadonDemandExceptionsHandler(NameValueCollection QueryString)
    {
        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            if (HttpContext.Current.Request.QueryString["UnderManagementPersonnelErrorSender"] != null)
            {
                string senderPage = "XmlUnderManagementPersonnelLoadonDemand.aspx";
                if (HttpContext.Current.Request.QueryString["UnderManagementPersonnelErrorSender"].ToLower() == senderPage.ToLower())
                {
                    string[] RetMessage = new string[3];
                    RetMessage[0] = HttpContext.Current.Request.QueryString["ErrorType"];
                    RetMessage[1] = HttpContext.Current.Request.QueryString["ErrorBody"];
                    RetMessage[2] = HttpContext.Current.Request.QueryString["error"];
                    Session.Add("LoadonDemandError_UnderManagementPersonnel_SubstituteSttings", this.exceptionHandler.CreateErrorMessage(RetMessage));
                }
            }
        }
    }

    [Ajax.AjaxMethod("GetLoadonDemandError_DepartmetsPersonnel_SubstituteSettingsPage", "GetLoadonDemandError_DepartmetsPersonnel_SubstituteSettingsPage_onCallBack", null, null)]
    public string GetLoadonDemandError_DepartmetsPersonnel_SubstituteSettingsPage()
    {
        this.InitializeCulture();
        string retError = string.Empty;
        if (Session["LoadonDemandError_UnderManagementPersonnel_SubstituteSettings"] != null)
        {
            retError = Session["LoadonDemandError_UnderManagementPersonnel_SubstituteSettings"].ToString();
            Session["LoadonDemandError_UnderManagementPersonnel_SubstituteSettings"] = null;
        }
        else
        {
            string[] retMessage = new string[3];
            retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
            retMessage[1] = GetLocalResourceObject("ParentNodeFillProblem").ToString();
            retMessage[2] = "error";
            retError = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        return retError;
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

    protected void CallBack_GridManagerWorkFlows_SubstituteSettings_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridManagerWorkFlows_SubstituteSettings(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_ManagerWorkFlows.RenderControl(e.Output);
        this.GridManagerWorkFlows_SubstituteSettings.RenderControl(e.Output);
    }

    private void Fill_GridManagerWorkFlows_SubstituteSettings(decimal SubstituteID, decimal ManagerPersonnelID)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Flow> ManagerWorkFlowsList = this.SubstituteBusiness.GetAllFlowByManager(SubstituteID, ManagerPersonnelID);
            this.GridManagerWorkFlows_SubstituteSettings.DataSource = ManagerWorkFlowsList;
            this.GridManagerWorkFlows_SubstituteSettings.DataBind();            
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ManagerWorkFlows.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ManagerWorkFlows.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ManagerWorkFlows.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_trvUnderManagementPersonnel_SubstituteSettings_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_trvUnderManagementPersonnel_SubstituteSettings(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
        this.ErrorHiddenField_UnderManagementPersonnel.RenderControl(e.Output);
        this.trvUnderManagementPersonnel_SubstituteSettings.RenderControl(e.Output);
    }

    private void Fill_trvUnderManagementPersonnel_SubstituteSettings(decimal flowID)
    {
        string imageUrl = "Images\\TreeView\\folder.gif";
        string imagePath = "Images/TreeView/folder.gif";
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            Department rootDepartment = this.SubstituteBusiness.GetDepartmentRoot();
            TreeViewNode rootDepartmentNode = new TreeViewNode();
            rootDepartmentNode.ID = rootDepartment.ID.ToString();
            string rootOrgPostNodeText = string.Empty;
            if (GetLocalResourceObject("OrgNode_trvUnderManagementPersonnel_SubstituteSettings") != null)
                rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvUnderManagementPersonnel_SubstituteSettings").ToString();
            else
                rootOrgPostNodeText = rootDepartment.Name;
            rootDepartmentNode.Text = rootOrgPostNodeText;
            rootDepartmentNode.Value = rootDepartment.CustomCode;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                rootDepartmentNode.ImageUrl = imagePath;
            this.trvUnderManagementPersonnel_SubstituteSettings.Nodes.Add(rootDepartmentNode);
            IList<Department> DepartmentChildList = this.SubstituteBusiness.GetDepartmentChilds(rootDepartment.ID, flowID);
            foreach (Department childDepartment in DepartmentChildList)
            {
                TreeViewNode childOrgPostNode = new TreeViewNode();
                childOrgPostNode.ID = childDepartment.ID.ToString();
                childOrgPostNode.Text = childDepartment.Name;
                childOrgPostNode.Value = ((int)UnderManagmentTypes.Department).ToString();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    childOrgPostNode.ImageUrl = imagePath;
                childOrgPostNode.ContentCallbackUrl = "XmlUnderManagementPersonnelLoadonDemand.aspx?FlowID=" + flowID + "&ParentDepartmentID=" + childDepartment.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                if (this.SubstituteBusiness.GetDepartmentChilds(childDepartment.ID, flowID).Count > 0 || this.SubstituteBusiness.GetDepartmentPerson(childDepartment.ID, flowID).Count > 0)
                    childOrgPostNode.Nodes.Add(new TreeViewNode());
                rootDepartmentNode.Nodes.Add(childOrgPostNode);
            }
            if (DepartmentChildList.Count > 0 || this.SubstituteBusiness.GetDepartmentPerson(rootDepartment.ID, flowID).Count > 0)
                rootDepartmentNode.Expanded = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_trvWorkFlowAccessLevels_SubstituteSettings(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.hfAccessLevelsList_SubstituteSettings.RenderControl(e.Output);
        this.ErrorHiddenField_WorkFlowAccessLevels.RenderControl(e.Output);
        this.trvWorkFlowAccessLevels_SubstituteSettings.RenderControl(e.Output);
    }

    private void Fill_trvWorkFlowAccessLevels_SubstituteSettings(decimal SubstituteID, decimal FlowID)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            string ImageUrl = "Images/TreeView/folder.gif";
            string ImagePath = "Images\\TreeView\\folder.gif";
            this.hfAccessLevelsList_SubstituteSettings.Value = string.Empty;
            IList<PrecardGroups> PrecardGroupsList = this.SubstituteBusiness.GetPrecardTree(SubstituteID, FlowID);
            foreach (PrecardGroups precardGroupsItem in PrecardGroupsList)
            {
                TreeViewNode trvNodePrecardGroups = new TreeViewNode();
                trvNodePrecardGroups.Text = precardGroupsItem.Name;
                trvNodePrecardGroups.ID = precardGroupsItem.ID.ToString();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
                    trvNodePrecardGroups.ImageUrl = ImageUrl;
                trvNodePrecardGroups.ShowCheckBox = true;
                trvNodePrecardGroups.Checked = precardGroupsItem.ContainInPrecardAccessGroup;
                foreach (Precard precardItem in precardGroupsItem.PrecardList)
                {
                    TreeViewNode trvNodePreCard = new TreeViewNode();
                    trvNodePreCard.Text = precardItem.Name;
                    trvNodePreCard.ID = precardItem.ID.ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
                        trvNodePreCard.ImageUrl = ImageUrl;
                    trvNodePreCard.ShowCheckBox = true;
                    trvNodePreCard.Checked = precardItem.ContainInPrecardAccessGroup;
                    trvNodePrecardGroups.Nodes.Add(trvNodePreCard);
                    if (!precardGroupsItem.ContainInPrecardAccessGroup)
                        this.CreateAccessLevelsList_SubstituteSettings(precardItem.ID.ToString(), precardItem.ContainInPrecardAccessGroup, precardGroupsItem.ID.ToString());
                }
                this.trvWorkFlowAccessLevels_SubstituteSettings.Nodes.Add(trvNodePrecardGroups);
                this.CreateAccessLevelsList_SubstituteSettings(precardGroupsItem.ID.ToString(), precardGroupsItem.ContainInPrecardAccessGroup, "undefined");
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_WorkFlowAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_WorkFlowAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_WorkFlowAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateSubstituteSetting_SubstituteSettingsPage", "UpdateSubstituteSetting_SubstituteSettingsPage_onCallBack", null, null)]
    public string[] UpdateSubstituteSetting_SubstituteSettingsPage(string state, string SubstituteID, string FlowID, string StrAccessLevelsList)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        state = this.StringBuilder.CreateString(state);
        bool IsFlowAssigned = false;
        decimal substituteID = decimal.Parse(this.StringBuilder.CreateString(SubstituteID));
        decimal flowID = decimal.Parse(this.StringBuilder.CreateString(FlowID));
        StrAccessLevelsList = this.StringBuilder.CreateString(StrAccessLevelsList);
        try
        {
            if (flowID == 0)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoWorkFlowSelected").ToString()), retMessage);
                return retMessage;
            }

            IList<AccessGroupProxy> AccessLevelsList = new List<AccessGroupProxy>();
            if (StrAccessLevelsList != "null")
                AccessLevelsList = this.CreateAccessLevelsList_SubstituteSettings(StrAccessLevelsList);
            else
                AccessLevelsList = null;

            IsFlowAssigned = this.SubstituteBusiness.UpdateByProxy(substituteID, flowID, AccessLevelsList);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            if(state == "Delete")
                SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();   
            else
                SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = IsFlowAssigned.ToString().ToLower();

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

    private void CreateAccessLevelsList_SubstituteSettings(string ID, bool Checked, string parentID)
    {
        if (Checked)
            this.hfAccessLevelsList_SubstituteSettings.Value += "ID=" + ID + "%Ch=" + Checked.ToString().ToLower() + "%P=" + parentID + "#";
    }

    private List<AccessGroupProxy> CreateAccessLevelsList_SubstituteSettings(string StrAccessLevelsList)
    {
        List<AccessGroupProxy> AccessLevelsList = new List<AccessGroupProxy>();
        StrAccessLevelsList = StrAccessLevelsList.Replace("ID=", string.Empty).Replace("Ch=", string.Empty).Replace("P=", string.Empty);
        string[] AccessLevelsListParts = StrAccessLevelsList.Split(new char[] { '#' });
        for (int i = 0; i < AccessLevelsListParts.Length; i++)
        {
            if (AccessLevelsListParts[i] != string.Empty)
            {
                string[] AccessLevelsListPartsDetails = AccessLevelsListParts[i].Split(new char[] { '%' });
                if (AccessLevelsListPartsDetails[2] != "Delete")
                {
                    AccessGroupProxy accessLevel = new AccessGroupProxy();
                    accessLevel.ID = decimal.Parse(AccessLevelsListPartsDetails[0]);
                    accessLevel.Checked = bool.Parse(AccessLevelsListPartsDetails[1]);
                    if (AccessLevelsListPartsDetails[2] == "undefined")
                        accessLevel.IsParent = true;
                    else
                        accessLevel.IsParent = false;
                    AccessLevelsList.Add(accessLevel);
                }
            }
        }
        return AccessLevelsList;
    }
}