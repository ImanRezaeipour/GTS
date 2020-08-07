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
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.IO;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model;
using System.Collections.Specialized;
using GTS.Clock.Business;
using GTS.Clock.Presentaion.Forms;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class OrganizationWorkFlow : GTSBasePage
    {
        enum PageState
        {
            View,
            Add,
            Edit,
            Delete
        }

        public enum LoadState
        {
            Normal,
            Search
        }

        public BFlow FlowBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BFlow>();
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
            OrganizationWorkFlow_onPageLoad,
            tbOrganizationWorkFlow_TabStripMenus_Operations,
            Alert_Box,
            DropDownDive,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbSearchField_OrganizationWorkFlow.IsCallback && !CallBack_GridWorkFlows_OrganizationWorkFlow.IsCallback && !CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow.IsCallback && !CallBack_WorkFlow_OrganizationWorkFlow.IsCallback)
            {
                Page OrganizationWorkFlowPage = this;
                Ajax.Utility.GenerateMethodScripts(OrganizationWorkFlowPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckOrganizationWorkFlowLoadAccess_OrganizationWorkFlow();
                this.UnderManagementPersonnelLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);
            }
        }

        private void CheckOrganizationWorkFlowLoadAccess_OrganizationWorkFlow()
        {
            string[] retMessage = new string[4];
            try
            {
                this.FlowBusiness.CheckOrganizationWorkFlowLoadAccess();
            }
            catch (UIBaseException ex)
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
                        Session.Add("LoadonDemandError_UnderManagementPersonnel_OrganizationWorkFlow", this.exceptionHandler.CreateErrorMessage(RetMessage));
                    }
                }
            }
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_DepartmetsPersonnel_OrganizationWorkFlowPage", "GetLoadonDemandError_DepartmetsPersonnel_OrganizationWorkFlowPage_onCallBack", null, null)]
        public string GetLoadonDemandError_DepartmetsPersonnel_OrganizationWorkFlowPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_UnderManagementPersonnel_OrganizationWorkFlow"] != null)
            {
                retError = Session["LoadonDemandError_UnderManagementPersonnel_OrganizationWorkFlow"].ToString();
                Session["LoadonDemandError_UnderManagementPersonnel_OrganizationWorkFlow"] = null;
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

        protected void CallBack_WorkFlow_OrganizationWorkFlow_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
           if (this.StringBuilder.CreateString(e.Parameter) != string.Empty)
               this.DrawFlow_OrganizationWorkFlow(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.ErrorHiddenField_WorkFlow_OrganizationWorkFlow.RenderControl(e.Output);
            this.smpWorkFlow_OrganizationWorkFlow.RenderControl(e.Output);
        }

        private void DrawFlow_OrganizationWorkFlow(decimal flowID)
        {
            this.smpWorkFlow_OrganizationWorkFlow.Nodes.Clear();
            string[] retMessage = new string[4];
            try
            {
                ComponentArt.Web.UI.SiteMapNode childManagerNode = null;
                ComponentArt.Web.UI.SiteMapNode parentManagerNode = null;
                string fistManagerNodeID = string.Empty;

                if (flowID == 0)
                {
                    this.smpWorkFlow_OrganizationWorkFlow.Nodes.Clear();
                    return;
                }
                IList<Manager> ManagersList = this.FlowBusiness.GetManagerFlow(flowID);
                foreach (Manager managerItem in ManagersList)
                {
                    if (childManagerNode == null)
                    {
                        parentManagerNode = childManagerNode = new ComponentArt.Web.UI.SiteMapNode();
                        fistManagerNodeID = childManagerNode.ID = Guid.NewGuid().ToString();
                        childManagerNode.Text = managerItem.ThePerson.Name;
                        childManagerNode.NavigateUrl = "#";
                    }
                    else
                    {
                        parentManagerNode = new ComponentArt.Web.UI.SiteMapNode();
                        if (managerItem.ThePerson.Name != " ")
                            parentManagerNode.Text = managerItem.ThePerson.Name;
                        else
                            parentManagerNode.Text = managerItem.OrganizationUnit.Name;
                        parentManagerNode.ID = Guid.NewGuid().ToString();
                        parentManagerNode.Nodes.Add(childManagerNode);
                        childManagerNode = parentManagerNode;
                        childManagerNode.NavigateUrl = "#";
                    }
                }
                if (parentManagerNode != null)
                {
                    this.smpWorkFlow_OrganizationWorkFlow.Nodes.Add(parentManagerNode);
                    this.smpWorkFlow_OrganizationWorkFlow.SelectedNode = this.smpWorkFlow_OrganizationWorkFlow.FindNodeById(fistManagerNodeID);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlow_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlow_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlow_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbSearchField_OrganizationWorkFlow_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbSearchField_OrganizationWorkFlow.Dispose();
            this.Fill_cmbSearchField_OrganizationWorkFlow();
            this.ErrorHiddenField_WorkFlowSearch.RenderControl(e.Output);
            this.cmbSearchField_OrganizationWorkFlow.RenderControl(e.Output);
        }

        private void Fill_cmbSearchField_OrganizationWorkFlow()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (string searchItem in Enum.GetNames(typeof(FlowSearchFields)))
                {
                    ComboBoxItem cmbItemSearch = new ComboBoxItem(GetLocalResourceObject(searchItem.ToString()).ToString());
                    cmbItemSearch.Value = searchItem.ToString();
                    this.cmbSearchField_OrganizationWorkFlow.Items.Add(cmbItemSearch);
                }
                this.cmbSearchField_OrganizationWorkFlow.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlowSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlowSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlowSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


        protected void CallBack_GridWorkFlows_OrganizationWorkFlow_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridWorkFlows_OrganizationWorkFlow((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_WorkFlows_OrganizationWorkFlow.RenderControl(e.Output);
            this.GridWorkFlows_OrganizationWorkFlow.RenderControl(e.Output);
        }

        private void Fill_GridWorkFlows_OrganizationWorkFlow(LoadState Ls, string SearchField, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<Flow> FlowList = null;
                switch (Ls)
                {
                    case LoadState.Normal:
                        FlowList = this.FlowBusiness.GetAll();
                        break;
                    case LoadState.Search:
                        FlowList = this.FlowBusiness.SearchFlow((FlowSearchFields)Enum.Parse(typeof(FlowSearchFields), SearchField), SearchTerm);
                        break;
                    default:
                        break;
                }
                this.GridWorkFlows_OrganizationWorkFlow.DataSource = FlowList;
                this.GridWorkFlows_OrganizationWorkFlow.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow_onCallBack(object sender, CallBackEventArgs e)
        {
            Fill_trvUnderManagementPersonnel_OrganizationWorkFlow(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.ErrorHiddenField_UnderManagementPersonnel_OrganizationWorkFlow.RenderControl(e.Output);
            this.trvUnderManagementPersonnel_OrganizationWorkFlow.RenderControl(e.Output);
        }

        private void Fill_trvUnderManagementPersonnel_OrganizationWorkFlow(decimal flowID)
        {
            string imageUrl = "Images\\TreeView\\folder.gif";
            string imagePath = "Images/TreeView/folder.gif";
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                Department rootDepartment = this.FlowBusiness.GetDepartmentRoot();
                TreeViewNode rootDepartmentNode = new TreeViewNode();
                rootDepartmentNode.ID = rootDepartment.ID.ToString();
                string rootOrgPostNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvPosts_Post") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvPosts_Post").ToString();
                else
                    rootOrgPostNodeText = rootDepartment.Name;
                rootDepartmentNode.Text = rootOrgPostNodeText;
                rootDepartmentNode.Value = rootDepartment.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    rootDepartmentNode.ImageUrl = imagePath;
                this.trvUnderManagementPersonnel_OrganizationWorkFlow.Nodes.Add(rootDepartmentNode);
                IList<Department> DepartmentChildList = this.FlowBusiness.GetDepartmentChilds(rootDepartment.ID, flowID);
                foreach (Department childDepartment in DepartmentChildList)
                {
                    TreeViewNode childOrgPostNode = new TreeViewNode();
                    childOrgPostNode.ID = childDepartment.ID.ToString();
                    childOrgPostNode.Text = childDepartment.Name;
                    childOrgPostNode.Value = ((int)UnderManagmentTypes.Department).ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                        childOrgPostNode.ImageUrl = imagePath;
                    childOrgPostNode.ContentCallbackUrl = "XmlUnderManagementPersonnelLoadonDemand.aspx?FlowID=" + flowID + "&ParentDepartmentID=" + childDepartment.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                    if (this.FlowBusiness.GetDepartmentChilds(childDepartment.ID, flowID).Count > 0 || this.FlowBusiness.GetDepartmentPerson(childDepartment.ID).Count > 0)
                        childOrgPostNode.Nodes.Add(new TreeViewNode());
                    rootDepartmentNode.Nodes.Add(childOrgPostNode);
                }
                if (DepartmentChildList.Count > 0 || this.FlowBusiness.GetDepartmentPerson(rootDepartment.ID).Count > 0)
                    rootDepartmentNode.Expanded = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_UnderManagementPersonnel_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_UnderManagementPersonnel_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_UnderManagementPersonnel_OrganizationWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateWorkFow_OrganizationWorkFlowPage", "UpdateWorkFow_OrganizationWorkFlowPage_onCallBack", null, null)]
        public string[] UpdateWorkFow_OrganizationWorkFlowPage(string state, string selectedFlowID)
        {
            this.InitializeCulture();
            string[] retMessage = new string[4];

            try
            {
                decimal flowID = decimal.Parse(this.StringBuilder.CreateString(selectedFlowID));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Flow organizationFlow = new Flow();
                organizationFlow.ID = flowID;

                switch (uam)
                {
                    case UIActionType.DELETE:
                        if (flowID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoFlowSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        this.FlowBusiness.DeleteOrganizationFlow(organizationFlow, uam);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case Business.UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = flowID.ToString();
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
}