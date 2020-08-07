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
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using System.Web.Script.Serialization;
using GTS.Clock.Model.Charts;
using System.IO;
using System.Collections.Specialized;
using GTS.Clock.Business;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ManagersWorkFlow : GTSBasePage
    {
        internal class PersonnelDetails
        {
            public string ID { get; set; }
            public string OrganizationPostID { get; set; }
            public string OrganizationPostName { get; set; }
            public string RoleID { get; set; }
            public string RoleName { get; set; }
        }

        internal class OrganizationPostDetails
        {
            public string ID { get; set; }
            public string PersonnelID { get; set; }
            public string PersonnelBarCode { get; set; }
            public string PersonnelName { get; set; }
            public string RoleID { get; set; }
            public string RoleName { get; set; }
        }


        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
        }

        public BManager ManagerBusiness
        {
            get
            {
                return new BManager();
            }
        }

        public BFlow FlowBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BFlow>();
            }
        }

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        public ISearchPerson PersonnelBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }

        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }


        internal class OrganizationPostNodeValue
        {
            public string CustomCode { get; set; }
            public string ParentPath { get; set; }
            public string PersonnelName { get; set; }
            public string PersonnelCode { get; set; }
            public string PersonnelID { get; set; }
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
            Alert_Box,
            ManagersWorkFlow_onPageLoad,
            DialogManagersWorkFlow_Operations,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbPersonnel_ManagersWorkFlow.IsCallback && !CallBack_cmbPostSearchResult_ManagersWorkFlow.IsCallback && !CallBack_GridManagersWorkFlow_ManagersWorkFlow.IsCallback && !CallBack_trvOrganizationPosts_ManagersWorkFlow.IsCallback)
            {
                Page UnderManagementPersonnelExeptionAccessCreationPage = this;
                Ajax.Utility.GenerateMethodScripts(UnderManagementPersonnelExeptionAccessCreationPage);

                SetPersonnelPageSize_cmbPersonnel_ManagersWorkFlow();
                SetPersonnelPageCount_cmbPersonnel_ManagersWorkFlow(LoadState.Normal, this.cmbPersonnel_ManagersWorkFlow.DropDownPageSize, string.Empty);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }



        private void OrgPostsLoadonDemandExceptionsHandler(NameValueCollection QueryString)
        {
            if (HttpContext.Current.Request.QueryString.Count > 0)
            {
                if (HttpContext.Current.Request.QueryString["OrgPostsErrorSender"] != null)
                {
                    string senderPage = "XmlOrganizationPostsLoadonDemand.aspx";
                    if (HttpContext.Current.Request.QueryString["OrgPostsErrorSender"].ToLower() == senderPage.ToLower())
                    {
                        string[] RetMessage = new string[3];
                        RetMessage[0] = HttpContext.Current.Request.QueryString["ErrorType"];
                        RetMessage[1] = HttpContext.Current.Request.QueryString["ErrorBody"];
                        RetMessage[2] = HttpContext.Current.Request.QueryString["error"];
                        Session.Add("LoadonDemandError_OrganizationPosts_ManagersWorkFlow", this.exceptionHandler.CreateErrorMessage(RetMessage));
                    }
                }
            }
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_OrganizationPosts_ManagersWorkFlowPage", "GetLoadonDemandError_OrganizationPosts_ManagersWorkFlowPage_onCallBack", null, null)]
        public string GetLoadonDemandError_OrganizationPosts_ManagersWorkFlowPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_OrganizationPosts_ManagersWorkFlow"] != null)
            {
                retError = Session["LoadonDemandError_OrganizationPosts_ManagersWorkFlow"].ToString();
                Session["LoadonDemandError_OrganizationPosts_ManagersWorkFlow"] = null;
            }
            else
            {
                string[] retMessage = new string[3];
                retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
                retMessage[1] = GetLocalResourceObject("ParentOrgPostNodeFillProblem").ToString();
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

        protected void CallBack_cmbPersonnel_ManagersWorkFlow_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel_ManagersWorkFlow.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_ManagersWorkFlow((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnel_ManagersWorkFlow((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_ManagersWorkFlow.RenderControl(e.Output);
            this.hfPersonnelPageCount_ManagersWorkFlow.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_ManagersWorkFlow.RenderControl(e.Output);
        }

        private void SetPersonnelPageSize_cmbPersonnel_ManagersWorkFlow()
        {
            this.hfPersonnelPageSize_ManagersWorkFlow.Value = this.cmbPersonnel_ManagersWorkFlow.DropDownPageSize.ToString();
        }


        private void SetPersonnelPageCount_cmbPersonnel_ManagersWorkFlow(LoadState Ls, int pageSize, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int PersonnelCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelCount = this.PersonnelBusiness.GetPersonCount();
                        break;
                    case LoadState.Search:
                        PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                        break;
                }
                this.hfPersonnelPageCount_ManagersWorkFlow.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void Fill_cmbPersonnel_ManagersWorkFlow(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Person> PersonList = null;
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        PersonList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                        break;
                }
                foreach (Person personItem in PersonList)
                {
                    ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                    personCmbItem["BarCode"] = personItem.BarCode;
                    personCmbItem["CardNum"] = personItem.CardNum;
                    PersonnelDetails personnelDetails = new PersonnelDetails();
                    personnelDetails.ID = personItem.ID.ToString();
                    personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                    personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                    personnelDetails.RoleID = personItem.User.Role.ID.ToString();
                    personnelDetails.RoleName = personItem.User.Role.Name;
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    personCmbItem.Value = jsSerializer.Serialize(personnelDetails);
                    this.cmbPersonnel_ManagersWorkFlow.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_trvOrganizationPosts_ManagersWorkFlow_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvOrganizationPosts_ManagersWorkFlow();
            this.ErrorHiddenField_OrganizationPosts_ManagersWorkFlow.RenderControl(e.Output);
            this.trvOrganizationPosts_ManagersWorkFlow.RenderControl(e.Output);
        }

        private void Fill_trvOrganizationPosts_ManagersWorkFlow()
        {
            string imageUrl = "Images\\TreeView\\folder.gif";
            string imagePath = "Images/TreeView/folder.gif";
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                OrganizationUnit rootOrgPost = this.ManagerBusiness.GetOrganizationUnitTree();
                TreeViewNode rootOrgPostNode = new TreeViewNode();
                rootOrgPostNode.ID = rootOrgPost.ID.ToString();
                string rootOrgPostNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvPosts_Post") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvPosts_Post").ToString();
                else
                    rootOrgPostNodeText = rootOrgPost.Name;
                rootOrgPostNode.Text = rootOrgPostNodeText;
                OrganizationPostNodeValue rootOrgPostNodeValue = new OrganizationPostNodeValue();
                rootOrgPostNodeValue.CustomCode = rootOrgPost.CustomCode;
                rootOrgPostNodeValue.ParentPath = string.Empty;
                rootOrgPostNodeValue.PersonnelName = string.Empty;
                rootOrgPostNodeValue.PersonnelCode = string.Empty;
                rootOrgPostNodeValue.PersonnelID = "0";
                rootOrgPostNode.Value = this.JsSerializer.Serialize(rootOrgPostNodeValue);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    rootOrgPostNode.ImageUrl = imagePath;
                this.trvOrganizationPosts_ManagersWorkFlow.Nodes.Add(rootOrgPostNode);
                IList<OrganizationUnit> OrganizationUnitChildList = this.ManagerBusiness.GetOrganizationUnitChilds(rootOrgPost.ID);
                foreach (OrganizationUnit childOrgPost in OrganizationUnitChildList)
                {
                    TreeViewNode childOrgPostNode = new TreeViewNode();
                    childOrgPostNode.ID = childOrgPost.ID.ToString();
                    childOrgPostNode.Text = childOrgPost.Name;
                    OrganizationPostNodeValue childOrgPostNodeValue = new OrganizationPostNodeValue();
                    childOrgPostNodeValue.CustomCode = childOrgPost.CustomCode;
                    childOrgPostNodeValue.ParentPath = childOrgPost.ParentPath;
                    childOrgPostNodeValue.PersonnelName = childOrgPost.Person != null ? childOrgPost.Person.Name : string.Empty;
                    childOrgPostNodeValue.PersonnelCode = childOrgPost.Person != null ? childOrgPost.Person.PersonCode : string.Empty;
                    childOrgPostNodeValue.PersonnelID = childOrgPost.Person != null ? childOrgPost.Person.ID.ToString() : "0";
                    childOrgPostNode.Value = this.JsSerializer.Serialize(childOrgPostNodeValue);
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                        childOrgPostNode.ImageUrl = imagePath;
                    childOrgPostNode.ContentCallbackUrl = "XmlOrganizationPostsLoadonDemand.aspx?ParentOrgPostID=" + childOrgPost.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                    if (this.ManagerBusiness.GetOrganizationUnitChilds(childOrgPost.ID).Count > 0)
                        childOrgPostNode.Nodes.Add(new TreeViewNode());
                    rootOrgPostNode.Nodes.Add(childOrgPostNode);
                }
                if (OrganizationUnitChildList.Count > 0)
                    rootOrgPostNode.Expanded = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OrganizationPosts_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPosts_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPosts_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


        protected void CallBack_cmbPostSearchResult_ManagersWorkFlow_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPostSearchResult_ManagersWorkFlow.Dispose();
            this.Fill_cmbPostSearchResult_ManagersWorkFlow(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_PostSearchResult_ManagersWorkFlow.RenderControl(e.Output);
            this.cmbPostSearchResult_ManagersWorkFlow.RenderControl(e.Output);
        }

        private void Fill_cmbPostSearchResult_ManagersWorkFlow(string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<OrganizationUnit> OrganizationPostsList = this.ManagerBusiness.QuickSearchByOrganizationUnitName(SearchTerm);
                foreach (OrganizationUnit orgPostItem in OrganizationPostsList)
                {
                    ComboBoxItem orgPostCmbItem = new ComboBoxItem(orgPostItem.Name);
                    OrganizationPostDetails orgPostDetails = new OrganizationPostDetails();
                    orgPostDetails.ID = orgPostItem.ID.ToString();
                    orgPostDetails.PersonnelID = orgPostItem.Person.ID.ToString();
                    orgPostDetails.PersonnelBarCode = orgPostItem.Person.BarCode;
                    orgPostDetails.PersonnelName = orgPostItem.Person.Name;
                    orgPostDetails.RoleID = orgPostItem.Person.User.Role.ID.ToString();
                    orgPostDetails.RoleName = orgPostItem.Person.User.Role.Name;
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    orgPostCmbItem.Value = jsSerializer.Serialize(orgPostDetails);
                    this.cmbPostSearchResult_ManagersWorkFlow.Items.Add(orgPostCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_PostSearchResult_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_PostSearchResult_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_PostSearchResult_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridManagersWorkFlow_ManagersWorkFlow_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridManagersWorkFlow_ManagersWorkFlow(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.ErrorHiddenField_ManagersWorkFlow.RenderControl(e.Output);
            this.GridManagersWorkFlow_ManagersWorkFlow.RenderControl(e.Output);
        }

        private void Fill_GridManagersWorkFlow_ManagersWorkFlow(decimal flowID)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<ManagerProxy> ManagersProxyList = this.FlowBusiness.GetAllManagers(flowID);
                this.GridManagersWorkFlow_ManagersWorkFlow.DataSource = ManagersProxyList;
                this.GridManagersWorkFlow_ManagersWorkFlow.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ManagersWorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateManagersWorkFlow_ManagersWorkFlowPage", "UpdateManagersWorkFlow_ManagersWorkFlowPage_onCallBack", null, null)]
        public string[] UpdateManagersWorkFlow_ManagersWorkFlowPage(string FlowState, string FlowID, string IsActiveFlow, string IsMainFlow, string strFlowPartsList)
        {
            string[] retMessage = new string[4];
            this.InitializeCulture();

            try
            {
                UIActionType FS = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(FlowState).ToUpper());
                decimal flowID = decimal.Parse(this.StringBuilder.CreateString(FlowID));
                bool isActiveFlow = bool.Parse(this.StringBuilder.CreateString(IsActiveFlow).ToLower());
                bool isMainFlow = bool.Parse(this.StringBuilder.CreateString(IsMainFlow).ToLower());
                strFlowPartsList = this.StringBuilder.CreateString(strFlowPartsList);
                IList<ManagerProxy> ManagerProxyList = new List<ManagerProxy>();

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                object[] ParamsBatchs = (object[])jsSerializer.DeserializeObject(strFlowPartsList);
                foreach (object paramBatch in ParamsBatchs)
                {
                    ManagerProxy managerProxy = new ManagerProxy();
                    managerProxy.Level = int.Parse(((Dictionary<string, object>)paramBatch)["Level"].ToString());
                    ManagerType managerType = (ManagerType)Enum.ToObject(typeof(ManagerType), int.Parse(((Dictionary<string, object>)paramBatch)["Type"].ToString()));
                    managerProxy.ManagerType = managerType;
                    managerProxy.OwnerID = int.Parse(((Dictionary<string, object>)paramBatch)["TypeID"].ToString());
                    ManagerProxyList.Add(managerProxy);
                }

                switch (FS)
                {
                    case UIActionType.ADD:
                        this.FlowBusiness.UpdateManagerFlows_onOrganizationFlowInsert(flowID, isActiveFlow, isMainFlow, ManagerProxyList);
                        break;
                    case UIActionType.EDIT:
                        this.FlowBusiness.UpdateManagerFlows_onOrganizationFlowUpdate(flowID, isActiveFlow, isMainFlow, ManagerProxyList);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
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
}