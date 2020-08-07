using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.AppSettings;
using System.Collections.Specialized;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
using ComponentArt.Web.UI;
using System.IO;
using GTS.Clock.Model;
using System.Threading;
using System.Globalization;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;

public partial class SendPrivateMessage : GTSBasePage
{
    enum ManagerCreator
    {
        Personnel,
        OrganizationPost,
        None
    }
    public User user
    {
        get
        {
            return new User();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }
    enum PageState
    {
        View,
        Add,
        Edit,
        Delete
    }

    public BPrivateMessage privateMessageBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BPrivateMessage>();
        }
    }

    public BManager ManagerBusiness
    {
        get
        {
            return new BManager();
        }
    }

    internal class PersonnelDetails
    {
        public string ID { get; set; }
        public string OrganizationPostID { get; set; }
        public string OrganizationPostName { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    enum Scripts
    {
        Alert_Box,
        DropDownDive,
        SendPrivateMessage_onPageLoad,
        DialogSendPrivateMessage_Operations,
        DialogWaiting_Operations
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
   
    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page UnderManagementPersonnelPage = this;
        Ajax.Utility.GenerateMethodScripts(UnderManagementPersonnelPage);

        this.CheckSendPrivateMessageLoadAccess_SendPrivateMessage();
        this.DepPersonnelLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }

    private void CheckSendPrivateMessageLoadAccess_SendPrivateMessage()
    {
        string[] retMessage = new string[4];
        try
        {
            this.privateMessageBusiness.CheckPrivateMessagesLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void DepPersonnelLoadonDemandExceptionsHandler(NameValueCollection QueryString)
    {
        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            if (HttpContext.Current.Request.QueryString["DepPersonnelErrorSender"] != null)
            {
                string senderPage = "XmlDeparmentsPersonnelLoadonDemand.aspx";
                if (HttpContext.Current.Request.QueryString["DepPersonnelErrorSender"].ToLower() == senderPage.ToLower())
                {
                    string[] RetMessage = new string[3];
                    RetMessage[0] = HttpContext.Current.Request.QueryString["ErrorType"];
                    RetMessage[1] = HttpContext.Current.Request.QueryString["ErrorBody"];
                    RetMessage[2] = HttpContext.Current.Request.QueryString["error"];
                    Session.Add("LoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnel", this.exceptionHandler.CreateErrorMessage(RetMessage));
                }
            }
        }
    }
    [Ajax.AjaxMethod("GetLoadonDemandError_DepartmetsPersonnel_SendPrivateMessagePage", "GetLoadonDemandError_DepartmetsPersonnel_SendPrivateMessagePage_onCallBack", null, null)]
    public string GetLoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnelPage()
    {
        this.InitializeCulture();
        string retError = string.Empty;
        if (Session["LoadonDemandError_DepartmetsPersonnel_SendPrivateMessage"] != null)
        {
            retError = Session["LoadonDemandError_DepartmetsPersonnel_SendPrivateMessage"].ToString();
            Session["LoadonDemandError_DepartmetsPersonnel_SendPrivateMessage"] = null;
        }
        else
        {
            string[] retMessage = new string[3];
            retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
            retMessage[1] = GetLocalResourceObject("ParentDepPersonnelNodeFillProblem").ToString();
            retMessage[2] = "error";
            retError = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        return retError;
    }
    protected void CallBack_trvOrganizationPersonnel_SendPrivateMessage_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.Fill_trvOrganizationPersonnel_SendPrivateMessage();
        this.ErrorHiddenField_OrganizationPersonnel_SendPrivateMessage.RenderControl(e.Output);
        this.trvOrganizationPersonnel_SendPrivateMessage.RenderControl(e.Output);
    }
    private void Fill_trvOrganizationPersonnel_SendPrivateMessage()
    {
        string imageUrl = "Images\\TreeView\\folder.gif";
        string imagePath = "Images/TreeView/folder.gif";
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            Department rootDepartment = this.ManagerBusiness.GetDepartmentRoot();
            TreeViewNode rootDepartmentNode = new TreeViewNode();
            rootDepartmentNode.ID = rootDepartment.ID.ToString();
            string rootOrgPostNodeText = string.Empty;
            if (GetLocalResourceObject("OrgNode_trvOrganizationPersonnel_SendPrivateMessage") != null)
                rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvOrganizationPersonnel_SendPrivateMessage").ToString();
            else
                rootOrgPostNodeText = rootDepartment.Name;
            rootDepartmentNode.Text = rootOrgPostNodeText;
            rootDepartmentNode.Value = rootDepartment.CustomCode;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                rootDepartmentNode.ImageUrl = imagePath;
            this.trvOrganizationPersonnel_SendPrivateMessage.Nodes.Add(rootDepartmentNode);
            IList<Department> DepartmentChildList = this.ManagerBusiness.GetDepartmentChilds(rootDepartment.ID);
            foreach (Department childDepartment in DepartmentChildList)
            {
                TreeViewNode childOrgPostNode = new TreeViewNode();
                childOrgPostNode.ID = childDepartment.ID.ToString();
                childOrgPostNode.Text = childDepartment.Name;
                childOrgPostNode.Value = ((int)UnderManagmentTypes.Department).ToString();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    childOrgPostNode.ImageUrl = imagePath;
                childOrgPostNode.ContentCallbackUrl = "XmlDeparmentsPersonnelLoadonDemand.aspx?ParentDepartmentID=" + childDepartment.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                if (this.ManagerBusiness.GetDepartmentChilds(childDepartment.ID).Count > 0 || this.ManagerBusiness.GetDepartmentPerson(childDepartment.ID).Count > 0)
                    childOrgPostNode.Nodes.Add(new TreeViewNode());
                rootDepartmentNode.Nodes.Add(childOrgPostNode);
            }
            if (DepartmentChildList.Count > 0 || this.ManagerBusiness.GetDepartmentPerson(rootDepartment.ID).Count > 0)
                rootDepartmentNode.Expanded = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_OrganizationPersonnel_SendPrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_OrganizationPersonnel_SendPrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_OrganizationPersonnel_SendPrivateMessage.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridUnderManagementPersonnel_SendPrivateMessage_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.hfUnderManagementPersonnelList_SendPrivateMessage.RenderControl(e.Output);
        this.ErrorHiddenField_SendPrivateMessage_SendPrivateMessage.RenderControl(e.Output);
        this.GridUnderManagementPersonnel_SendPrivateMessage.RenderControl(e.Output);
    }

    private void CreatePersonDepartmentList_SendPrivateMessage(IList<UnderManagment> UnderManagementsList)
    {
        string strUnderManagementPersonnel_SendPrivateMessage = string.Empty;
        foreach (UnderManagment underManagmentItem in UnderManagementsList)
        {
            strUnderManagementPersonnel_SendPrivateMessage += "Key=" + underManagmentItem.KeyID + "%Type=" + ((int)(underManagmentItem.Type)).ToString() + "%Access=" + underManagmentItem.Contains.ToString().ToLower() + "%SubDep=" + underManagmentItem.ContainInnerChilds.ToString().ToLower() + "#";
        }
        this.hfUnderManagementPersonnelList_SendPrivateMessage.Value = strUnderManagementPersonnel_SendPrivateMessage;
    }

    [Ajax.AjaxMethod("UpdateUnderManagement_SendPrivateMessagePage", "UpdateUnderManagement_SendPrivateMessagePage_onCallBack", null, null)]
    public string[] UpdateUnderManagement_SendPrivateMessagePage(string strPersonDepartmentList, string message, string subject)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            string message_SendPrivateMessage = this.StringBuilder.CreateString(message);
            string subject_SendPrivateMessage = this.StringBuilder.CreateString(subject);
            IList<PersonDepartmentProxy> PersonDepartmentList = this.CreateUnderManagementList_SendPrivateMessage(this.StringBuilder.CreateString(strPersonDepartmentList));

            privateMessageBusiness.CheckSendPrivateMessageAccess();
            privateMessageBusiness.NewMessage(subject_SendPrivateMessage, message_SendPrivateMessage, PersonDepartmentList);
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
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
    private IList<PersonDepartmentProxy> CreateUnderManagementList_SendPrivateMessage(string strUnderManagementList)
    {
        List<PersonDepartmentProxy> PersonDepartmentList = new List<PersonDepartmentProxy>();
        if (strUnderManagementList != string.Empty)
        {
            strUnderManagementList = strUnderManagementList.Replace("Key=", string.Empty).Replace("Type=", string.Empty).Replace("Access=", string.Empty).Replace("SubDep=", string.Empty);
            string[] UnderManagementListParts = strUnderManagementList.Split(new char[] { '#' });
            foreach (string UnderManagementListPartItem in UnderManagementListParts)
            {
                if (UnderManagementListPartItem != string.Empty)
                {
                    decimal departmentID = 0;
                    decimal personnelID = 0;
                    UnderManagmentTypes UMT = UnderManagmentTypes.Department;
                    bool hasAccess = false;
                    bool hasSubDep = false;

                    string[] UnderManagementListPartSections = UnderManagementListPartItem.Split(new char[] { '%' });
                    departmentID = decimal.Parse(UnderManagementListPartSections[0].Substring(3, UnderManagementListPartSections[0].IndexOf("prs") - 3));
                    personnelID = decimal.Parse(UnderManagementListPartSections[0].Substring(UnderManagementListPartSections[0].IndexOf("prs") + 3, UnderManagementListPartSections[0].Length - UnderManagementListPartSections[0].IndexOf("prs") - 3));
                    UMT = (UnderManagmentTypes)Enum.Parse(typeof(UnderManagmentTypes), UnderManagementListPartSections[1]);
                    hasAccess = bool.Parse(UnderManagementListPartSections[2]);
                    hasSubDep = bool.Parse(UnderManagementListPartSections[3]);
                    PersonDepartmentProxy personDepartment = new PersonDepartmentProxy();
                   
                    personDepartment.DepartmentId = departmentID;
                    switch (UMT)
                    {
                        case UnderManagmentTypes.Department:
                            break;
                        case UnderManagmentTypes.Person:
                            Person personnel = new Person();
                            personnel.ID = personnelID;
                            personDepartment.PersonId = personnelID;
                            break;
                        default:
                            break;
                    }
                   
                    personDepartment.ContainsInnerchilds = hasSubDep;

                    PersonDepartmentList.Add(personDepartment);
                }
            }
        }
        return PersonDepartmentList;
    }
    [Ajax.AjaxMethod("UpdateUnderManagementReply_SendPrivateMessagePage", "UpdateUnderManagementReply_SendPrivateMessagePage_onCallBack", null, null)]
    public string[] UpdateUnderManagementReply_SendPrivateMessagePage(string MessageID, string message, string subject)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal MessageID_SendPrivateMessage = Convert.ToDecimal(this.StringBuilder.CreateString(MessageID));
            string message_SendPrivateMessage = this.StringBuilder.CreateString(message);
            string subject_SendPrivateMessage = this.StringBuilder.CreateString(subject);

            privateMessageBusiness.CheckSendPrivateMessageAccess();
            privateMessageBusiness.ReplyMessage(MessageID_SendPrivateMessage, message_SendPrivateMessage);
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
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