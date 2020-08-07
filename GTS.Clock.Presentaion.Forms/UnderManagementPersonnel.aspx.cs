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
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using ComponentArt.Web.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Business.UI;
using GTS.Clock.Model.Charts;
using System.IO;
using System.Collections.Specialized;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class UnderManagementPersonnel : GTSBasePage
    {
        public ISearchPerson PersonSearchBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }

        enum PageState
        {
            View,
            Add,
            Edit,
            Delete
        }

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
                return BusinessHelper.GetBusinessInstance<BManager>();
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

        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }

        enum Scripts
        {
            Alert_Box,
            DropDownDive,
            UnderManagementPersonnel_onPageLoad,
            DialogManagersWorkFlow_onPageLoad,
            DialogUnderManagementPersonnel_Operations,
            DialogUnderManagementPersonnelExeptionAccessView_onPageLoad,
            DialogUnderManagementPersonnelExeptionAccessCreation_onPageLoad,
            DialogAccessGroups_onPageLoad,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbPersonnel_UnderManagementPersonnel.IsCallback && !CallBack_trvOrganizationPosts_UnderManagementPersonnel.IsCallback && !CallBack_cmbPostSearchResult_UnderManagementPersonnel.IsCallback && !CallBack_cmbAccessGroup_UnderManagementPersonnel.IsCallback && !CallBack_trvOrganizationPersonnel_UnderManagementPersonnel.IsCallback && !CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel.IsCallback)
            {
                Page UnderManagementPersonnelPage = this;
                Ajax.Utility.GenerateMethodScripts(UnderManagementPersonnelPage);

                this.CreateUnderManagementTypesList_UnderManagementPersonnel();

                this.OrgPostsLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);
                this.DepPersonnelLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);

                SetPersonnelPageSize_cmbPersonnel_UnderManagementPersonnel();
                SetPersonnelPageCount_cmbPersonnel_UnderManagementPersonnel(LoadState.Normal, this.cmbPersonnel_UnderManagementPersonnel.DropDownPageSize, string.Empty);

                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CreateUnderManagementTypesList_UnderManagementPersonnel()
        {
            string strUnderManagementList = string.Empty;
            foreach (UnderManagmentTypes underManagementTypeItem in Enum.GetValues(typeof(UnderManagmentTypes)))
            {
                strUnderManagementList += ((int)underManagementTypeItem).ToString() + ":" + GetLocalResourceObject(underManagementTypeItem.ToString()).ToString() + "#";
            }
            this.hfUndermanagementTypesList_UnderManagementPersonnel.Value = strUnderManagementList;
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
                        Session.Add("LoadonDemandError_OrganizationPosts_UnderManagementPersonnel", this.exceptionHandler.CreateErrorMessage(RetMessage));
                    }
                }
            }
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_OrganizationPosts_UnderManagementPersonnelPage", "GetLoadonDemandError_UnderManagementPersonnelPage_onCallBack", null, null)]
        public string GetLoadonDemandError_OrganizationPosts_UnderManagementPersonnelPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_OrganizationPosts_UnderManagementPersonnel"] != null)
            {
                retError = Session["LoadonDemandError_OrganizationPosts_UnderManagementPersonnel"].ToString();
                Session["LoadonDemandError_OrganizationPosts_UnderManagementPersonnel"] = null;
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


        [Ajax.AjaxMethod("GetLoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnelPage", "GetLoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnelPage_onCallBack", null, null)]
        public string GetLoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnelPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnel"] != null)
            {
                retError = Session["LoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnel"].ToString();
                Session["LoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnel"] = null;
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


        private void SetPersonnelPageSize_cmbPersonnel_UnderManagementPersonnel()
        {
            this.hfPersonnelPageSize_UnderManagementPersonnel.Value = this.cmbPersonnel_UnderManagementPersonnel.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnel_UnderManagementPersonnel(LoadState Ls, int pageSize, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int PersonnelCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                        break;
                    case LoadState.Search:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                        break;
                }
                this.hfPersonnelPageCount_UnderManagementPersonnel.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected override void InitializeCulture()
        {
            BLanguage LangProv = new BLanguage();
            this.SetCurrentCultureResObjs(LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        protected void CallBack_cmbPersonnel_UnderManagementPersonnel_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.cmbPersonnel_UnderManagementPersonnel.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_UnderManagementPersonnel((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnel_UnderManagementPersonnel((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_UnderManagementPersonnel.RenderControl(e.Output);
            this.hfPersonnelPageCount_UnderManagementPersonnel.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_UnderManagementPersonnel.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnel_UnderManagementPersonnel(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Person> PersonList = null;
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        PersonList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonList = this.PersonSearchBusiness.GetPersonInAdvanceSearchApplyCulture(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
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
                    this.cmbPersonnel_UnderManagementPersonnel.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_trvOrganizationPosts_UnderManagementPersonnel_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvOrganizationPosts_UnderManagementPersonnel();
            this.ErrorHiddenField_OrganizationPosts_UnderManagementPersonnel.RenderControl(e.Output);
            this.trvOrganizationPosts_UnderManagementPersonnel.RenderControl(e.Output);
        }

        private void Fill_trvOrganizationPosts_UnderManagementPersonnel()
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
                this.trvOrganizationPosts_UnderManagementPersonnel.Nodes.Add(rootOrgPostNode);
                foreach (OrganizationUnit childOrgPost in rootOrgPost.ChildList)
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
                    if (childOrgPost.ChildList.Count > 0)
                        childOrgPostNode.Nodes.Add(new TreeViewNode());
                    rootOrgPostNode.Nodes.Add(childOrgPostNode);
                }
                if (rootOrgPost.ChildList.Count > 0)
                    rootOrgPostNode.Expanded = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OrganizationPosts_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPosts_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPosts_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbPostSearchResult_UnderManagementPersonnel_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPostSearchResult_UnderManagementPersonnel.Dispose();
            this.Fill_cmbPostSearchResult_UnderManagementPersonnel(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_PostSearchResult_UnderManagementPersonnel.RenderControl(e.Output);
            this.cmbPostSearchResult_UnderManagementPersonnel.RenderControl(e.Output);
        }

        private void Fill_cmbPostSearchResult_UnderManagementPersonnel(string SearchTerm)
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
                    this.cmbPostSearchResult_UnderManagementPersonnel.Items.Add(orgPostCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_PostSearchResult_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_PostSearchResult_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_PostSearchResult_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbAccessGroup_UnderManagementPersonnel_OnCallback(object sender, CallBackEventArgs e)
        {
            this.cmbAccessGroup_UnderManagementPersonnel.Dispose();
            this.Fill_cmbAccessGroup_UnderManagementPersonnel();
            this.ErrorHiddenField_AccessGroup_UnderManagementPersonnel.RenderControl(e.Output);
            this.cmbAccessGroup_UnderManagementPersonnel.RenderControl(e.Output);
        }

        private void Fill_cmbAccessGroup_UnderManagementPersonnel()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<PrecardAccessGroup> AccessGroupsList = this.ManagerBusiness.GetAllAccessGroups();
                this.cmbAccessGroup_UnderManagementPersonnel.DataSource = AccessGroupsList;
                this.cmbAccessGroup_UnderManagementPersonnel.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AccessGroup_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AccessGroup_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AccessGroup_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_trvOrganizationPersonnel_UnderManagementPersonnel_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvOrganizationPersonnel_UnderManagementPersonnel();
            this.ErrorHiddenField_OrganizationPersonnel_UnderManagementPersonnel.RenderControl(e.Output);
            this.trvOrganizationPersonnel_UnderManagementPersonnel.RenderControl(e.Output);
        }

        private void Fill_trvOrganizationPersonnel_UnderManagementPersonnel()
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
                if (GetLocalResourceObject("OrgNode_trvPosts_Post") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvPosts_Post").ToString();
                else
                    rootOrgPostNodeText = rootDepartment.Name;
                rootDepartmentNode.Text = rootOrgPostNodeText;
                rootDepartmentNode.Value = rootDepartment.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    rootDepartmentNode.ImageUrl = imagePath;
                this.trvOrganizationPersonnel_UnderManagementPersonnel.Nodes.Add(rootDepartmentNode);
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
                this.ErrorHiddenField_OrganizationPersonnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPersonnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPersonnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel_onCallBack(object sender, CallBackEventArgs e)
        {
            decimal flowID = 0;
            switch ((PageState)Enum.Parse(typeof(PageState), this.StringBuilder.CreateString(e.Parameters[0])))
            {
                case PageState.Add:
                    flowID = 0;
                    break;
                case PageState.Edit:
                    flowID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]));
                    break;
            }
            this.Fill_GridUnderManagementPersonnel_UnderManagementPersonnel(flowID);
            this.hfUnderManagementPersonnelList_UnderManagementPersonnel.RenderControl(e.Output);
            this.ErrorHiddenField_UnderManagementPersonnel_UnderManagementPersonnel.RenderControl(e.Output);
            this.GridUnderManagementPersonnel_UnderManagementPersonnel.RenderControl(e.Output);
        }

        private void Fill_GridUnderManagementPersonnel_UnderManagementPersonnel(decimal flowID)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<UnderManagment> UnderManagementsList = this.ManagerBusiness.GetAllUnderManagments(flowID);
                this.CreateUnderManagementList_UnderManagementPersonnel(UnderManagementsList);
                this.GridUnderManagementPersonnel_UnderManagementPersonnel.DataSource = UnderManagementsList;
                this.GridUnderManagementPersonnel_UnderManagementPersonnel.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_UnderManagementPersonnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_UnderManagementPersonnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_UnderManagementPersonnel_UnderManagementPersonnel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void CreateUnderManagementList_UnderManagementPersonnel(IList<UnderManagment> UnderManagementsList)
        {
            string strUnderManagementPersonnel_UnderManagementPersonnel = string.Empty;
            foreach (UnderManagment underManagmentItem in UnderManagementsList)
            {
                strUnderManagementPersonnel_UnderManagementPersonnel += "Key=" + underManagmentItem.KeyID + "%Type=" + ((int)(underManagmentItem.Type)).ToString() + "%Access=" + underManagmentItem.Contains.ToString().ToLower() + "%SubDep=" + underManagmentItem.ContainInnerChilds.ToString().ToLower() + "#";
            }
            this.hfUnderManagementPersonnelList_UnderManagementPersonnel.Value = strUnderManagementPersonnel_UnderManagementPersonnel;
        }

        [Ajax.AjaxMethod("UpdateUnderManagement_UnderManagementPersonnelPage", "UpdateUnderManagement_UnderManagementPersonnelPage_onCallBack", null, null)]
        public string[] UpdateUnderManagement_UnderManagementPersonnelPage(string state, string SelectedManagerID, string managerCreator, string PersonnelID, string OrganizationPostID, string AccessGroupID, string FlowID, string FlowName, string strUnderManagementList)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal retFlowID = 0;
                decimal targetManagerCreatorID = 0;
                decimal selectedManagerID = decimal.Parse(this.StringBuilder.CreateString(SelectedManagerID));
                ManagerCreator Mc = (ManagerCreator)Enum.Parse(typeof(ManagerCreator), this.StringBuilder.CreateString(managerCreator));
                decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
                decimal organizationPostID = decimal.Parse(this.StringBuilder.CreateString(OrganizationPostID));
                decimal accessGroupID = decimal.Parse(this.StringBuilder.CreateString(AccessGroupID));
                decimal flowID = decimal.Parse(this.StringBuilder.CreateString(FlowID));
                FlowName = this.StringBuilder.CreateString(FlowName);
                IList<UnderManagment> UnderManagmentList = this.CreateUnderManagementList_UnderManagementPersonnel(this.StringBuilder.CreateString(strUnderManagementList));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                switch (uam)
                {
                    case UIActionType.ADD:
                        switch (Mc)
                        {
                            case ManagerCreator.Personnel:
                                targetManagerCreatorID = personnelID;
                                break;
                            case ManagerCreator.OrganizationPost:
                                targetManagerCreatorID = organizationPostID;
                                break;
                            case ManagerCreator.None:
                                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("FlowPersonOrOrganizationMustSpecified").ToString()), retMessage);
                                return retMessage;
                        }
                        retFlowID = this.ManagerBusiness.InsertFlowByManagerCreator(Mc, targetManagerCreatorID, accessGroupID, FlowName, UnderManagmentList);
                        break;
                    case UIActionType.EDIT:
                        if (flowID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoFlowforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        retFlowID = this.ManagerBusiness.UpdateFlow(flowID, accessGroupID, FlowName, UnderManagmentList);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case Business.UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case Business.UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = retFlowID.ToString();
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

        private IList<UnderManagment> CreateUnderManagementList_UnderManagementPersonnel(string strUnderManagementList)
        {
            List<UnderManagment> UnderManagementList = new List<UnderManagment>();
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

                        UnderManagment underManagment = new UnderManagment();
                        Department department = new Department();
                        department.ID = departmentID;
                        underManagment.Department = department;
                        switch (UMT)
                        {
                            case UnderManagmentTypes.Department:
                                break;
                            case UnderManagmentTypes.Person:
                                Person personnel = new Person();
                                personnel.ID = personnelID;
                                underManagment.Person = personnel;
                                break;
                            default:
                                break;
                        }
                        underManagment.Contains = hasAccess;
                        underManagment.ContainInnerChilds = hasSubDep;

                        UnderManagementList.Add(underManagment);
                    }
                }
            }
            return UnderManagementList;
        }

    }
}