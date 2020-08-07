using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using System.Collections.Specialized;
using GTS.Clock.Model.Charts;
using System.IO;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure;

public partial class MultiLevelsDataAccessLevels : GTSBasePage
{
    public BDataAccess MultiLevelsDataAccessLevelesBusiness
    {
        get
        {
            return new BDataAccess();
        }
    }

    public BUser UserBusiness
    {
        get
        {
            return new BUser();
        }
    }

    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    internal class DataAccessLevelSourceObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    enum DataAccessLevelOperationState
    {
        Before,
        After
    }

    enum Scripts
    {
        MultiLevelsDataAccessLevels_onPageLoad,
        MultiLevelsDataAccessLevels_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels.IsCallback && !CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels.IsCallback)
        {
            Page MultiLevelsDataAccessLevelsPage = this;
            Ajax.Utility.GenerateMethodScripts(MultiLevelsDataAccessLevelsPage);
            this.DataAccessLevelsLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
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


    private void DataAccessLevelsLoadonDemandExceptionsHandler(NameValueCollection QueryString)
    {
        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            if (HttpContext.Current.Request.QueryString["DalErrorSender"] != null)
            {
                string senderPage = "XmlMultiLevelsDataAccessLevels.aspx";
                if (HttpContext.Current.Request.QueryString["DalErrorSender"].ToLower() == senderPage.ToLower())
                {
                    string[] RetMessage = new string[3];
                    RetMessage[0] = HttpContext.Current.Request.QueryString["ErrorType"];
                    RetMessage[1] = HttpContext.Current.Request.QueryString["ErrorBody"];
                    RetMessage[2] = HttpContext.Current.Request.QueryString["error"];
                    Session.Add("LoadonDemandError_MultiLevelsDataAccessLevelsPage", this.exceptionHandler.CreateErrorMessage(RetMessage));
                }
            }
        }
    }

    [Ajax.AjaxMethod("GetLoadonDemandError_MultiLevelsDataAccessLevelsPage", "GetLoadonDemandError_MultiLevelsDataAccessLevelsPage_onCallBack", null, null)]
    public string GetLoadonDemandError_MultiLevelsDataAccessLevelsPage()
    {
        this.InitializeCulture();
        string retError = string.Empty;
        if (Session["LoadonDemandError_MultiLevelsDataAccessLevelsPage"] != null)
        {
            retError = Session["LoadonDemandError_MultiLevelsDataAccessLevelsPage"].ToString();
            Session["LoadonDemandError_MultiLevelsDataAccessLevelsPage"] = null;
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


    protected void CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels((DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameter)));
        this.ErrorHiddenField_DataAccessLevelsSource.RenderControl(e.Output);
        this.trvDataAccessLevelsSource_MultiLevelsDataAccessLevels.RenderControl(e.Output);
    }

    private void Fill_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels(DataAccessParts DataAccessLevelKey)
    {
        string[] retMessage = new string[4];
        try
        {
            this.GetDataAccessLevelsRoot_MultiLevelsDataAccessLevels(trvDataAccessLevelsSource_MultiLevelsDataAccessLevels, DataAccessLevelsType.Source, DataAccessLevelKey, null, null, -1, null, string.Empty);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsSource.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsSource.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsSource.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void GetDataAccessLevelsRoot_MultiLevelsDataAccessLevels(ComponentArt.Web.UI.TreeView trvDataAccessLevels, DataAccessLevelsType Dalt, DataAccessParts DataAccessLevelKey, DataAccessLevelOperationType? Dalot, DataAccessLevelOperationState? Dalos, decimal UserID, UserSearchKeys? SearchKey, string SearchTerm)
    {
        DataAccessProxy rootDals = null;
        string rootDalsNodeText = string.Empty;
        if (Dalot == DataAccessLevelOperationType.Group && Dalos == DataAccessLevelOperationState.After)
            UserID = BUser.CurrentUser.ID;
        switch (DataAccessLevelKey)
        {
            case DataAccessParts.Department:
                rootDals = this.MultiLevelsDataAccessLevelesBusiness.GetDepartmentRoot(Dalt, UserID);
                if (GetLocalResourceObject("OrgNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels") != null)
                    rootDalsNodeText = GetLocalResourceObject("OrgNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels").ToString();
                else
                    rootDalsNodeText = rootDals.Name;
                break;
            case DataAccessParts.OrganizationUnit:
                rootDals = this.MultiLevelsDataAccessLevelesBusiness.GetOrganizationRoot(Dalt, UserID);
                if (GetLocalResourceObject("OrgNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels") != null)
                    rootDalsNodeText = GetLocalResourceObject("OrgNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels").ToString();
                else
                    rootDalsNodeText = rootDals.Name;
                break;
            case DataAccessParts.Report:
                rootDals = this.MultiLevelsDataAccessLevelesBusiness.GetReportRoot(Dalt, UserID);
                if (GetLocalResourceObject("ReportsNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels") != null)
                    rootDalsNodeText = GetLocalResourceObject("ReportsNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels").ToString();
                else
                    rootDalsNodeText = rootDals.Name;
                break;
            case DataAccessParts.RuleGroup:
                rootDals = this.MultiLevelsDataAccessLevelesBusiness.GetRuleRoot(Dalt, UserID);
                if (GetLocalResourceObject("RulesGroupsNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels") != null)
                    rootDalsNodeText = GetLocalResourceObject("RulesGroupsNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels").ToString();
                else
                    rootDalsNodeText = rootDals.Name;
                break;
        }
        TreeViewNode rootDalsNode = new TreeViewNode();
        rootDalsNode.ID = rootDals.ID.ToString();
        rootDalsNode.Text = rootDalsNodeText;
        rootDalsNode.Value = rootDals.DeleteEnable.ToString().ToLower();
        string ImagePath = string.Empty;
        string ImageUrl = string.Empty;
        if (rootDals.DeleteEnable)
        {
            if (DataAccessLevelKey != DataAccessParts.Report)
            {
                ImagePath = "\\Images\\TreeView\\folder_blue.gif";
                ImageUrl = "Images/TreeView/folder_blue.gif";
            }
            else
            {
                ImagePath = "\\Images\\TreeView\\group.png";
                ImageUrl = "Images/TreeView/group.png";
            }
        }
        else
        {
            if (DataAccessLevelKey != DataAccessParts.Report)
            {
                ImagePath = "\\Images\\TreeView\\folder.gif";
                ImageUrl = "Images/TreeView/folder.gif";
            }
            else
            {
                ImagePath = "\\Images\\TreeView\\group_silver.png";
                ImageUrl = "Images/TreeView/group_silver.png";
            }
        }
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
            rootDalsNode.ImageUrl = ImageUrl;
        trvDataAccessLevels.Nodes.Add(rootDalsNode);
        rootDalsNode.Expanded = true;

        switch (Dalot)
        {
            case DataAccessLevelOperationType.Single:
                this.GetChildDataAccessLevels_MultiLevelsDataAccessLevels(Dalt, DataAccessLevelKey, null, UserID, SearchKey, string.Empty, rootDalsNode, rootDals);
                break;
            case DataAccessLevelOperationType.Group:
                switch (Dalos)
                {
                    case DataAccessLevelOperationState.Before:
                        break;
                    case DataAccessLevelOperationState.After:
                        this.GetChildDataAccessLevels_MultiLevelsDataAccessLevels(Dalt, DataAccessLevelKey, Dalot, UserID, SearchKey, SearchTerm, rootDalsNode, rootDals);
                        break;
                }
                break;
            default:
                if (Dalt == DataAccessLevelsType.Source)
                    this.GetChildDataAccessLevels_MultiLevelsDataAccessLevels(Dalt, DataAccessLevelKey, Dalot, UserID, SearchKey, SearchTerm, rootDalsNode, rootDals);
                break;
        }
    }

    private void GetChildDataAccessLevels_MultiLevelsDataAccessLevels(DataAccessLevelsType Dalt, DataAccessParts DataAccessLevelKey, DataAccessLevelOperationType? Dalot, decimal UserID, UserSearchKeys? SearchKey, string SearchTerm, TreeViewNode parentDalNode, DataAccessProxy parentDal)
    {
        bool HasChildDals = false;
        IList<DataAccessProxy> DalList = null;
        UserID = this.GetUserID_MultiLevelsDataAccessLevels(Dalot, UserID, SearchKey, SearchTerm);

        switch (DataAccessLevelKey)
        {
            case DataAccessParts.Department:
                switch (Dalt)
                {
                    case DataAccessLevelsType.Source:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetDepartmentChilds(parentDal.ID);
                        break;
                    case DataAccessLevelsType.Target:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetDepartmentsOfUser(UserID, parentDal.ID);
                        break;
                }
                break;
            case DataAccessParts.OrganizationUnit:
                switch (Dalt)
                {
                    case DataAccessLevelsType.Source:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetOrganizationChilds(parentDal.ID);
                        break;
                    case DataAccessLevelsType.Target:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetOrganizationOfUser(UserID, parentDal.ID);
                        break;
                }
                break;
            case DataAccessParts.Report:
                switch (Dalt)
                {
                    case DataAccessLevelsType.Source:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetReportChilds(parentDal.ID);
                        break;
                    case DataAccessLevelsType.Target:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetReportOfUser(UserID, parentDal.ID);
                        break;
                }
                break;
            case DataAccessParts.RuleGroup:
                switch (Dalt)
                {
                    case DataAccessLevelsType.Source:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetRuleChilds(parentDal.ID);
                        break;
                    case DataAccessLevelsType.Target:
                        DalList = this.MultiLevelsDataAccessLevelesBusiness.GetRuleOfUser(UserID, parentDal.ID);
                        break;
                }
                break;
        }

        foreach (DataAccessProxy childDal in DalList)
        {
            TreeViewNode childDalNode = new TreeViewNode();
            childDalNode.ID = childDal.ID.ToString();
            childDalNode.Text = childDal.Name;
            childDalNode.Value = childDal.DeleteEnable.ToString().ToLower();
            if (DataAccessLevelKey == DataAccessParts.OrganizationUnit)
            {
                childDalNode.ContentCallbackUrl = "XmlMultiLevelsDataAccessLevels.aspx?Dalt=" + Dalt.ToString() + "&Dalk=" + DataAccessLevelKey.ToString() + "&UserID=" + UserID.ToString() + "&ParentDalID=" + childDalNode.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                if (this.MultiLevelsDataAccessLevelesBusiness.GetOrganizationChilds(childDal.ID).Count > 0)
                    childDalNode.Nodes.Add(new TreeViewNode());
            }
            string ImagePath = string.Empty;
            string ImageUrl = string.Empty;
            if (childDal.DeleteEnable)
            {
                if (DataAccessLevelKey != DataAccessParts.Report)
                {
                    ImagePath = "\\Images\\TreeView\\folder_blue.gif";
                    ImageUrl = "Images/TreeView/folder_blue.gif";
                }
                else
                {
                    if (childDal.IsReport)
                    {
                        ImagePath = "\\Images\\TreeView\\report.png";
                        ImageUrl = "Images/TreeView/report.png";
                    }
                    else
                    {
                        ImagePath = "\\Images\\TreeView\\group.png";
                        ImageUrl = "Images/TreeView/group.png";
                    }
                }
            }
            else
            {
                if (DataAccessLevelKey != DataAccessParts.Report)
                {
                    ImagePath = "\\Images\\TreeView\\folder.gif";
                    ImageUrl = "Images/TreeView/folder.gif";
                }
                else
                {
                    if (childDal.IsReport)
                    {
                        ImagePath = "\\Images\\TreeView\\report_silver.png";
                        ImageUrl = "Images/TreeView/report_silver.png";
                    }
                    else
                    {
                        ImagePath = "\\Images\\TreeView\\group_silver.png";
                        ImageUrl = "Images/TreeView/group_silver.png";
                    }
                }
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
                childDalNode.ImageUrl = ImageUrl;
            parentDalNode.Nodes.Add(childDalNode);
            try
            {
                if (parentDalNode.Parent.Parent == null)
                    parentDalNode.Expanded = true;
            }
            catch
            { }
            switch (DataAccessLevelKey)
            {
                case DataAccessParts.Department:
                    if (this.MultiLevelsDataAccessLevelesBusiness.GetDepartmentChilds(childDal.ID).Count > 0)
                        HasChildDals = true;
                    break;
                case DataAccessParts.Report:
                    if (this.MultiLevelsDataAccessLevelesBusiness.GetReportChilds(childDal.ID).Count > 0)
                        HasChildDals = true;
                    break;
                case DataAccessParts.RuleGroup:
                    break;
            }
            if (HasChildDals)
                this.GetChildDataAccessLevels_MultiLevelsDataAccessLevels(Dalt, DataAccessLevelKey, Dalot, UserID, SearchKey, SearchTerm, childDalNode, childDal);
        }
    }

    private decimal GetUserID_MultiLevelsDataAccessLevels(DataAccessLevelOperationType? Dalot, decimal UserID, UserSearchKeys? SearchKey, string SearchTerm)
    {
        switch (Dalot)
        {
            case DataAccessLevelOperationType.Single:
                break;
            case DataAccessLevelOperationType.Group:
                IList<decimal> UserIDList = this.UserBusiness.GetAllUserIDList(BUser.CurrentUser.ID, SearchKey, SearchTerm, true);
                if (UserIDList.Count() > 0)
                    UserID = UserIDList.First();
                break;
        }
        return UserID;
    }


    protected void CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallBack(object sender, CallBackEventArgs e)
    {
        string SearchKey = this.StringBuilder.CreateString(e.Parameters[4]);
        this.Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), (DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameters[1])), (DataAccessLevelOperationType)Enum.Parse(typeof(DataAccessLevelOperationType), this.StringBuilder.CreateString(e.Parameters[2])), (DataAccessLevelOperationState)Enum.Parse(typeof(DataAccessLevelOperationState), this.StringBuilder.CreateString(e.Parameters[3])), SearchKey != "Null" ? (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), SearchKey) : (UserSearchKeys?)null, this.StringBuilder.CreateString(e.Parameters[5]));
        this.ErrorHiddenField_DataAccessLevelsTarget.RenderControl(e.Output);
        this.trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels.RenderControl(e.Output);
    }

    private void Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels(decimal UserID, DataAccessParts DataAccessLevelKey, DataAccessLevelOperationType Dalot, DataAccessLevelOperationState Dalos, UserSearchKeys? SearchKey, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            this.GetDataAccessLevelsRoot_MultiLevelsDataAccessLevels(trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels, DataAccessLevelsType.Target, DataAccessLevelKey, Dalot, Dalos, UserID, SearchKey, SearchTerm);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsTarget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsTarget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsTarget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateDataAccessLevels_MultiLevelsDataAccessLevelsPage", "UpdateDataAccessLevels_MultiLevelsDataAccessLevelsPage_onCallBack", null, null)]
    public string[] UpdateDataAccessLevels_MultiLevelsDataAccessLevelsPage(string state, string DataAccessLevelKey, string AccessLevelOperationType, string UserID, string SearchKey, string SearchTerm, string DataAccessLevelSourceID, string DataAccessLevelTargetID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        state = this.StringBuilder.CreateString(state);
        DataAccessParts dataAccessLevelKey = (DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(DataAccessLevelKey));
        DataAccessLevelOperationType dataAccessLevelOperationType = (DataAccessLevelOperationType)Enum.Parse(typeof(DataAccessLevelOperationType), this.StringBuilder.CreateString(AccessLevelOperationType));
        decimal userID = decimal.Parse(this.StringBuilder.CreateString(UserID));
        SearchKey = this.StringBuilder.CreateString(SearchKey);
        UserSearchKeys? searchKey = SearchKey != "Null" ? (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), SearchKey) : (UserSearchKeys?)null;
        SearchTerm = this.StringBuilder.CreateString(SearchTerm);
        decimal dataAccessLevelSourceID = decimal.Parse(this.StringBuilder.CreateString(DataAccessLevelSourceID));
        decimal dataAccessLevelTargetID = decimal.Parse(this.StringBuilder.CreateString(DataAccessLevelTargetID));
        UIActionType uam = UIActionType.ADD;
        try
        {
            switch (state)
            {
                case "Add":
                    uam = UIActionType.ADD;
                    if (dataAccessLevelSourceID == -1)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoDataAccessLevelSourceSelectedforInsert").ToString()), retMessage);
                        return retMessage;
                    }
                    this.MultiLevelsDataAccessLevelesBusiness.InsertDataAccess(dataAccessLevelOperationType, dataAccessLevelKey, dataAccessLevelSourceID, userID, searchKey, SearchTerm);
                    break;
                case "Delete":
                    uam = UIActionType.DELETE;
                    if (dataAccessLevelTargetID == -1)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoDataAccessLevelTargetSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.MultiLevelsDataAccessLevelesBusiness.DeleteAccess(dataAccessLevelKey, dataAccessLevelTargetID, userID);
                    break;
                default:
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.ADD:
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
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