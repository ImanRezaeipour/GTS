using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using Subgurim.Controles;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
using System.IO;
using System.Data;
using System.Collections.Specialized;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.UIValidation;
using System.Web.Script.Serialization;
using System.Web.Configuration;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class PersonnelMainInformation : GTSBasePage
    {
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
                return BusinessHelper.GetBusinessInstance<BPerson>(new KeyValuePair<string, object>("sysLanguage", Slr), new KeyValuePair<string, object>("localLanguage", Llr));
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

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        internal class ReserveFieldObj
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public string Value { get; set; }
        }

        internal class OrganizationPostNodeValue
        {
            public string CustomCode { get; set; }
            public string ParentPath { get; set; }
            public string PersonnelName { get; set; }
            public string PersonnelCode { get; set; }
            public string PersonnelID { get; set; }
        }

        internal class ObjPersonnelImage
        {
            public string PersonnelImagePath { get; set; }
            public string PersonnelImageRealName { get; set; }
            public string PersonnelImageSavedPath { get; set; }
            public string PersonnelImageSavedName { get; set; }
            public bool IsErrorOccured { get; set; }
            public string Message { get; set; }
        }

        enum Scripts
        {
            PersonnelMainInformation_onPageLoad,
            DialogPersonnelMainInformation_Operations,
            DialogPersonnelExtraInformation_onPageLoad,
            DialogPersonnelSingleDateFeatures_onPageLoad,
            DialogPersonnelRulesGroups_onPageLoad,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        private string StrPersonnelImage = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if(!CallBack_cmbControlStation_DialogPersonnelMainInformation.IsCallback && !CallBack_cmbDepartment_DialogPersonnelMainInformation.IsCallback && !CallBack_cmbEmployType_DialogPersonnelMainInformation.IsCallback && !CallBack_cmbMarriageState_DialogPersonnelMainInformation.IsCallback && !CallBack_cmbMilitaryState_DialogPersonnelMainInformation.IsCallback && !CallBack_cmbOrganizationPost_DialogPersonnelMainInformation.IsCallback && !CallBack_cmbSex_DialogPersonnelMainInformation.IsCallback && !CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.IsCallback && !Callback_ImageUploader_DialogPersonnelMainInformation.IsCallback && !CallBack_trvParentDepartments_PersonnelMainInformation.IsCallback && !CallBack_cmbGrade_DialogPersonnelMainInformation.IsCallback)
            {
                Page PersonnelMainInformationPage = this;
                Ajax.Utility.GenerateMethodScripts(PersonnelMainInformationPage);

                this.ViewCurrentCalendars_PersonnelMainInformation();
                this.SetCurrentDate_PersonnelMainInformation();
                this.OrgPostsLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);
                this.SetSexStr_PersonnelMainInformation();
                this.SetMilitaryStateStr_PersonnelMainInformation();
                this.SetMarriageStateStr_PersonnelMainInformation();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }

            if (ImageUploader_DialogPersonnelMainInformation.IsPosting)
                this.ManagePostedData_ImageUploader_DialogPersonnelMainInformation();
            if (!Page.IsPostBack)
                ImageUploader_DialogPersonnelMainInformation.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.ImageUploader_DialogPersonnelMainInformation_OnAfterFileUpload('" + StrPersonnelImage + "');");
        }

        private void ViewCurrentCalendars_PersonnelMainInformation()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpBirthDate_DialogPersonnelMainInformation.Visible = true;
                    this.Container_pdpEmployDate_WorkStart_DialogPersonnelMainInformation.Visible = true;
                    this.Container_pdpEmployEndDate_DialogPersonnelMainInformation.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpBirthDate_DialogPersonnelMainInformation.Visible = true;
                    this.Container_gdpEmployDate_WorkStart_DialogPersonnelMainInformation.Visible = true;
                    this.gdpEmployEndDate_DialogPersonnelMainInformation.Visible = true;
                    break;
            }
        }

        private void SetCurrentDate_PersonnelMainInformation()
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
            this.hfCurrentDate_PersonnelMainInformation.Value = strCurrentDate;
        }


        private void SetSexStr_PersonnelMainInformation()
        {
            string strSex = string.Empty;
            foreach (PersonSex sexItem in Enum.GetValues(typeof(PersonSex)))
            {
                strSex += "#" + GetLocalResourceObject(sexItem.ToString()).ToString() + ":" + ((int)sexItem).ToString();
            }
            this.hfSexList_DialogPersonnelMainInformation.Value = strSex;
        }

        private void SetMilitaryStateStr_PersonnelMainInformation()
        {
            string strMilitaryState = string.Empty;
            foreach (MilitaryStatus militaryStateItem in Enum.GetValues(typeof(MilitaryStatus)))
            {
                strMilitaryState += "#" + GetLocalResourceObject(militaryStateItem.ToString()).ToString() + ":" + ((int)militaryStateItem).ToString();
            }
            this.hfMilitaryStateList_DialogPersonnelMainInformation.Value = strMilitaryState;
        }

        private void SetMarriageStateStr_PersonnelMainInformation()
        {
            string strMarriageState = string.Empty;
            foreach (MaritalStatus marriageStateItem in Enum.GetValues(typeof(MaritalStatus)))
            {
                strMarriageState += "#" + GetLocalResourceObject(marriageStateItem.ToString()).ToString() + ":" + ((int)marriageStateItem).ToString();
            }
            this.hfMarriageStateList_DialogPersonnelMainInformation.Value = strMarriageState;
        }



        [Ajax.AjaxMethod("GetWorkingPersonnelID_PersonnelMainInformationPage", "GetWorkingPersonnelID_PersonnelMainInformationPage_onCallBack", null, null)]
        public string GetWorkingPersonnelID_PersonnelMainInformationPage(string temp)
        {
            return this.PersonBusiness.CreateWorkingPerson().ToString();
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
                        Session.Add("LoadonDemandError_PersonnelMainInformationPage", this.exceptionHandler.CreateErrorMessage(RetMessage));
                    }
                }
            }
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_PersonnelMainInformationPage", "GetLoadonDemandError_PersonnelMainInformationPage_onCallBack", null, null)]
        public string GetLoadonDemandError_PersonnelMainInformationPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_PersonnelMainInformationPage"] != null)
            {
                retError = Session["LoadonDemandError_PersonnelMainInformationPage"].ToString();
                Session["LoadonDemandError_PersonnelMainInformationPage"] = null;
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

        private void ManagePostedData_ImageUploader_DialogPersonnelMainInformation()
        {
            try
            {
                string separator = "_";
                string physicalPath = WebConfigurationManager.AppSettings["PersonnelImagesPath"];
                string path = "ClientAttachments";
                HttpPostedFileAJAX HPFA = ImageUploader_DialogPersonnelMainInformation.PostedFile;
                string PersonnelImageSavedFileName = Guid.NewGuid().ToString() + separator + BUser.CurrentUser.Person.ID + separator + HPFA.FileName;
                ObjPersonnelImage PersonnelImage = new ObjPersonnelImage()
                {
                    PersonnelImagePath = path,
                    PersonnelImageRealName = HPFA.FileName,
                    PersonnelImageSavedPath = path + "/" + PersonnelImageSavedFileName,
                    PersonnelImageSavedName = PersonnelImageSavedFileName
                };
                this.StrPersonnelImage = this.JsSerializer.Serialize(PersonnelImage);
                ImageUploader_DialogPersonnelMainInformation.PostedFile.responseMessage_Uploaded_Saved = " ";
                ImageUploader_DialogPersonnelMainInformation.PostedFile.responseMessage_Uploaded_NotSaved = " ";
                ImageUploader_DialogPersonnelMainInformation.SaveAs(path, PersonnelImageSavedFileName);
                File.Move(Server.MapPath(path + "\\" + PersonnelImageSavedFileName), physicalPath + "\\" + PersonnelImageSavedFileName);
            }
            catch (Exception ex)
            {
                ObjPersonnelImage PersonnelImage = new ObjPersonnelImage()
                {
                    IsErrorOccured = true,
                    Message = GetLocalResourceObject("UploadingError").ToString()
                };
                this.StrPersonnelImage = this.JsSerializer.Serialize(PersonnelImage);
            }

        }

        protected void Callback_ImageUploader_DialogPersonnelMainInformation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            ImageUploader_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        //private void SavePersonnelImage_DialogPersonnelMainInformation(decimal PersonnelID, string FileName)
        //{
        //    string[] retMessage = new string[3];
        //    try
        //    {
        //        string filePath = Server.MapPath("ClientAttachments\\") + FileName;
        //        byte[] b = File.ReadAllBytes(filePath);
        //        this.PersonBusiness.UpdatePersonImage(PersonnelID, BitConverter.ToString(b).Replace("-", string.Empty));
        //        File.Delete(filePath);
        //    }
        //    catch (UIValidationExceptions ex)
        //    {
        //        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
        //        this.ErrorHiddenField_ImageUploader_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        //    }
        //    catch (UIBaseException ex)
        //    {
        //        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
        //        this.ErrorHiddenField_ImageUploader_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
        //        this.ErrorHiddenField_ImageUploader_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        //    }
        //}

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

        protected void CallBack_cmbSex_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbSex_DialogPersonnelMainInformation.Dispose();
            this.cmbSex_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbSex_DialogPersonnelMainInformation();
            this.ErrorHiddenField_Sex_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbSex_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbSex_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (PersonSex personSexItem in Enum.GetValues(typeof(PersonSex)))
                {
                    ComboBoxItem cmbItemPersonSex = new ComboBoxItem(GetLocalResourceObject(personSexItem.ToString()).ToString());
                    cmbItemPersonSex.Value = ((int)personSexItem).ToString();
                    cmbItemPersonSex.Id = ((int)personSexItem).ToString();
                    this.cmbSex_DialogPersonnelMainInformation.Items.Add(cmbItemPersonSex);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Sex_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Sex_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Sex_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }

        protected void CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMilitaryState_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbMilitaryState_DialogPersonnelMainInformation();
            this.ErrorHiddenField_MilitaryState_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbMilitaryState_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbMilitaryState_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (MilitaryStatus militaryStatusItem in Enum.GetValues(typeof(MilitaryStatus)))
                {
                    ComboBoxItem cmbItemMilitaryStatus = new ComboBoxItem(GetLocalResourceObject(militaryStatusItem.ToString()).ToString());
                    cmbItemMilitaryStatus.Value = ((int)militaryStatusItem).ToString();
                    cmbItemMilitaryStatus.Id = ((int)militaryStatusItem).ToString();
                    this.cmbMilitaryState_DialogPersonnelMainInformation.Items.Add(cmbItemMilitaryStatus);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMarriageState_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMarriageState_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbMarriageState_DialogPersonnelMainInformation();
            this.ErrorHiddenField_MarriageState_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbMarriageState_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbMarriageState_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (MaritalStatus maritalStatusItem in Enum.GetValues(typeof(MaritalStatus)))
                {
                    ComboBoxItem cmbItemMaritalStatus = new ComboBoxItem(GetLocalResourceObject(maritalStatusItem.ToString()).ToString());
                    cmbItemMaritalStatus.Value = ((int)maritalStatusItem).ToString();
                    cmbItemMaritalStatus.Id = ((int)maritalStatusItem).ToString();
                    this.cmbMarriageState_DialogPersonnelMainInformation.Items.Add(cmbItemMaritalStatus);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MarriageState_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MarriageState_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MarriageState_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbDepartment_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbDepartment_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbDepartment_DialogPersonnelMainInformation();
            this.ErrorHiddenField_Department_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbDepartment_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbDepartment_DialogPersonnelMainInformation()
        {
            this.Fill_trvDepartment_DialogPersonnelMainInformation();
        }

        private void Fill_trvDepartment_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                Department rootDep = this.PersonBusiness.GetAllDepartmentTree();
                TreeViewNode rootDepNode = new TreeViewNode();
                rootDepNode.ID = rootDep.ID.ToString();
                string rootDepNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvDepartment_DialogPersonnelMainInformation") != null)
                    rootDepNodeText = GetLocalResourceObject("OrgNode_trvDepartment_DialogPersonnelMainInformation").ToString();
                else
                    rootDepNodeText = rootDep.Name;
                rootDepNode.Text = rootDepNodeText;
                rootDepNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    rootDepNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvDepartment_DialogPersonnelMainInformation.Nodes.Add(rootDepNode);
                rootDepNode.Expanded = true;

                this.GetChildDepartment_trvDepartment_DialogPersonnelMainInformation(rootDepNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Department_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Department_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Department_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildDepartment_trvDepartment_DialogPersonnelMainInformation(TreeViewNode parentDepartmentNode, Department parentDepartment)
        {
            foreach (Department childDep in this.PersonBusiness.GetAllDepartmentChildTree(parentDepartment.ID))
            {
                TreeViewNode childDepNode = new TreeViewNode();
                childDepNode.ID = childDep.ID.ToString();
                childDepNode.Text = childDep.Name;
                childDepNode.Value = childDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                    childDepNode.ImageUrl = "Images/TreeView/folder.gif";
                parentDepartmentNode.Nodes.Add(childDepNode);
                try
                {
                    if (parentDepartmentNode.Parent.Parent == null)
                        parentDepartmentNode.Expanded = true;
                }
                catch
                { }
                if (this.PersonBusiness.GetAllDepartmentChildTree(childDep.ID).Count > 0)
                    this.GetChildDepartment_trvDepartment_DialogPersonnelMainInformation(childDepNode, childDep);
            }
        }

        protected void CallBack_cmbEmployType_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbEmployType_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbEmployType_DialogPersonnelMainInformation();
            this.ErrorHiddenField_EmployType_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbEmployType_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbEmployType_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<EmploymentType> EmploymentTypesList = this.PersonBusiness.GetAllEmployType();
                this.cmbEmployType_DialogPersonnelMainInformation.DataSource = EmploymentTypesList;
                this.cmbEmployType_DialogPersonnelMainInformation.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_EmployType_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_EmployType_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_EmployType_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbOrganizationPost_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbOrganizationPost_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbOrganizationPost_DialogPersonnelMainInformation();
            this.ErrorHiddenField_OrganizationPost_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbOrganizationPost_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbOrganizationPost_DialogPersonnelMainInformation()
        {
            this.Fill_trvOrganizationPost_DialogPersonnelMainInformation();
        }

        private void Fill_trvOrganizationPost_DialogPersonnelMainInformation()
        {
            string imageUrl = "Images\\TreeView\\folder.gif";
            string imagePath = "Images/TreeView/folder.gif";
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                OrganizationUnit rootOrgPost = this.PersonBusiness.GetAllOrganizationUnitTree();
                TreeViewNode rootOrgPostNode = new TreeViewNode();
                rootOrgPostNode.ID = rootOrgPost.ID.ToString();
                string rootOrgPostNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvOrganizationPost_DialogPersonnelMainInformation") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvOrganizationPost_DialogPersonnelMainInformation").ToString();
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
                this.trvOrganizationPost_DialogPersonnelMainInformation.Nodes.Add(rootOrgPostNode);
                IList<OrganizationUnit> OrganizationUnitChildList = this.PersonBusiness.GetAllOrganizationUnitChildsTree(rootOrgPost.ID);
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
                    if (this.PersonBusiness.GetAllOrganizationUnitChildsTree(childOrgPost.ID).Count > 0)
                        childOrgPostNode.Nodes.Add(new TreeViewNode());
                    rootOrgPostNode.Nodes.Add(childOrgPostNode);
                }
                if (OrganizationUnitChildList.Count > 0)
                    rootOrgPostNode.Expanded = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }

        protected void CallBack_cmbControlStation_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbControlStation_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbControlStation_DialogPersonnelMainInformation();
            this.ErrorHiddenField_ControlStation_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbControlStation_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbControlStation_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<ControlStation> ControlStationsList = this.PersonBusiness.GetAllControlStations();
                this.cmbControlStation_DialogPersonnelMainInformation.DataSource = ControlStationsList;
                this.cmbControlStation_DialogPersonnelMainInformation.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ControlStation_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ControlStation_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ControlStation_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.Dispose();
            this.Fill_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation();
            this.ErrorHiddenField_UserInterfaceRuleGroup_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<UIValidationGroup> UserInterfaceRuleGroupsList = this.PersonBusiness.GetAllUIValidationGroup(); ;
                this.cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.DataSource = UserInterfaceRuleGroupsList;
                this.cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_UserInterfaceRuleGroup_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_UserInterfaceRuleGroup_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_UserInterfaceRuleGroup_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdatePersonnel_PersonnelMainInformationPage", "UpdatePersonnel_PersonnelMainInformationPage_onCallBack", null, null)]
        public string[] UpdatePersonnel_PersonnelMainInformationPage(string PageState, string PageSize, string SelectedPersonnelID, string IsActive, string FirstName, string LastName, string SexID, string FatherName, string NationalCode, string MilitaryStateID, string IdentityCertificate, string IssuanceLocation, string Education, string MarriageStateID, string Tel, string MobileNumber, string Address, string EmailAddress, string BirthLocation, string BirthDate, string PersonnelNumber, string CardNumber, string DepartmentID, string OrganizationPostID, string OrganizationPostName, string OrganizationPostCustomCode, string OrganizationPostParentPath, string ParentOrganizationPostID, string CurrentActiveWorkGroup, string CurrentActiveRuleGroup, string CurrentActiveCalculationRangeGroup, string ControlStationID, string EmployNumber, string EmployTypeID, string EmployDate, string EmployEndDate, string UserInterfaceRuleGroupID, string StrPersonnelExtraInformation, string Grade, string ImageFile)
        {
            this.InitializeCulture();

            string[] retMessage = new string[5];

            try
            {
                decimal PersonnelID = 0;
                decimal selectedPersonnelID = decimal.Parse(this.StringBuilder.CreateString(SelectedPersonnelID));
                bool isActive = bool.Parse(this.StringBuilder.CreateString(IsActive));
                FirstName = this.StringBuilder.CreateString(FirstName);
                LastName = this.StringBuilder.CreateString(LastName);
                int sexID = int.Parse(this.StringBuilder.CreateString(SexID));
                FatherName = this.StringBuilder.CreateString(FatherName);
                NationalCode = this.StringBuilder.CreateString(NationalCode);
                int militaryStateID = int.Parse(this.StringBuilder.CreateString(MilitaryStateID));
                IdentityCertificate = this.StringBuilder.CreateString(IdentityCertificate);
                IssuanceLocation = this.StringBuilder.CreateString(IssuanceLocation);
                Education = this.StringBuilder.CreateString(Education);
                int marriageStateID = int.Parse(this.StringBuilder.CreateString(MarriageStateID));
                Tel = this.StringBuilder.CreateString(Tel);
                MobileNumber = this.StringBuilder.CreateString(MobileNumber);
                Address = this.StringBuilder.CreateString(Address);
                EmailAddress = this.StringBuilder.CreateString(EmailAddress);
                BirthLocation = this.StringBuilder.CreateString(BirthLocation);
                BirthDate = this.StringBuilder.CreateString(BirthDate);
                PersonnelNumber = this.StringBuilder.CreateString(PersonnelNumber);
                CardNumber = this.StringBuilder.CreateString(CardNumber);
                decimal departmentID = decimal.Parse(this.StringBuilder.CreateString(DepartmentID));
                decimal organizationPostID = decimal.Parse(this.StringBuilder.CreateString(OrganizationPostID));
                OrganizationPostName = this.StringBuilder.CreateString(OrganizationPostName);
                OrganizationPostCustomCode = this.StringBuilder.CreateString(OrganizationPostCustomCode);
                OrganizationPostParentPath = this.StringBuilder.CreateString(OrganizationPostParentPath);
                decimal parentOrganizationPostID = decimal.Parse(this.StringBuilder.CreateString(ParentOrganizationPostID));
                CurrentActiveWorkGroup = this.StringBuilder.CreateString(CurrentActiveWorkGroup);
                CurrentActiveRuleGroup = this.StringBuilder.CreateString(CurrentActiveRuleGroup);
                CurrentActiveCalculationRangeGroup = this.StringBuilder.CreateString(CurrentActiveCalculationRangeGroup);
                decimal controlStationID = decimal.Parse(this.StringBuilder.CreateString(ControlStationID));
                EmployNumber = this.StringBuilder.CreateString(EmployNumber);
                decimal employTypeID = decimal.Parse(this.StringBuilder.CreateString(EmployTypeID));
                EmployDate = this.StringBuilder.CreateString(EmployDate);
                EmployEndDate = this.StringBuilder.CreateString(EmployEndDate);
                decimal userInterfaceRuleGroupID = decimal.Parse(this.StringBuilder.CreateString(UserInterfaceRuleGroupID));
                Grade = this.StringBuilder.CreateString(Grade);

                ImageFile = this.StringBuilder.CreateString(ImageFile);
                 
                StrPersonnelExtraInformation = this.StringBuilder.CreateString(StrPersonnelExtraInformation);


                Person person = new Person();
                PersonDetail personDetail = new PersonDetail();
                PersonTASpec personSpec = new PersonTASpec();

                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(PageState).ToUpper());

                person.ID = selectedPersonnelID;
                person.Active = isActive;
                person.FirstName = FirstName;
                person.LastName = LastName;
                person.Sex = (PersonSex)Enum.ToObject(typeof(PersonSex), sexID);
                personDetail.FatherName = FatherName;
                personDetail.MeliCode = NationalCode;
                personDetail.MilitaryStatus = (MilitaryStatus)Enum.ToObject(typeof(MilitaryStatus), militaryStateID);
                personDetail.BirthCertificate = IdentityCertificate;
                personDetail.PlaceIssued = IssuanceLocation;
                person.Education = Education;
                person.MaritalStatus = (MaritalStatus)Enum.ToObject(typeof(MaritalStatus), marriageStateID);
                personDetail.Tel = Tel;
                personDetail.MobileNumber = MobileNumber;
                personDetail.Address = Address;
                personDetail.EmailAddress = EmailAddress;
                personDetail.BirthPlace = BirthLocation;
                personDetail.UIBirthDate = BirthDate;
                person.PersonCode = PersonnelNumber;
                person.CardNum = CardNumber;
                Department department = new Department();
                department.ID = departmentID;
                person.Department = department;
                person.OrganizationUnit = new OrganizationUnit() {ID = organizationPostID, Name = OrganizationPostName, ParentID = parentOrganizationPostID, CustomCode = OrganizationPostCustomCode, ParentPath = OrganizationPostParentPath, Person = person};
                person.CurrentActiveWorkGroup = CurrentActiveWorkGroup;
                person.CurrentActiveRuleGroup = CurrentActiveRuleGroup;
                person.CurrentActiveDateRangeGroup = CurrentActiveCalculationRangeGroup;
                person.EmploymentNum = EmployNumber;
                EmploymentType employmentType = new EmploymentType();
                employmentType.ID = employTypeID;
                person.EmploymentType = employmentType;
                person.UIEmploymentDate = EmployDate;
                person.UIEndEmploymentDate = EmployEndDate;
                personDetail.Grade = Grade;
                personDetail.Image = ImageFile;
                person.PersonDetail = personDetail;
                personSpec = this.UpdatePersonnelExtraInformation_PersonnelMainInformation(personSpec, StrPersonnelExtraInformation);
                personSpec.ControlStation = new ControlStation() { ID = controlStationID };
                personSpec.UIValidationGroup = new UIValidationGroup() { ID = userInterfaceRuleGroupID };
                person.PersonTASpec = personSpec;

                switch (uam)
                {
                    case UIActionType.ADD:
                        PersonnelID = this.PersonBusiness.InsertPerson(person, UIActionType.EDIT);
                        break;
                    case UIActionType.EDIT:
                        if (selectedPersonnelID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPersonnelSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        PersonnelID = this.PersonBusiness.UpdatePerson(person, uam);
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
                retMessage[3] = PersonnelID.ToString();
                retMessage[4] = this.SetPersonnelPageCount_PersonnelMainInformation(int.Parse(this.StringBuilder.CreateString(PageSize)));

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

        private PersonTASpec UpdatePersonnelExtraInformation_PersonnelMainInformation(PersonTASpec personSpec, string StrPersonnelExtraInformation)
        {
            if (StrPersonnelExtraInformation != string.Empty)
            {
                ReserveFieldObj[] ReserveFieldObjList = this.JsSerializer.Deserialize<ReserveFieldObj[]>(StrPersonnelExtraInformation);
                for (int i = 0; i < 15; i++)
                {
                    typeof(PersonTASpec).GetProperty(ReserveFieldObjList[i].Name).SetValue(personSpec, ReserveFieldObjList[i].Value, null);
                }
                for (int j = 15; j < 20; j++)
                {
                    typeof(PersonTASpec).GetProperty(ReserveFieldObjList[j].Name).SetValue(personSpec, decimal.Parse(ReserveFieldObjList[j].Value), null);
                }
            }
            return personSpec;
        }

        private string SetPersonnelPageCount_PersonnelMainInformation(int PageSize)
        {
            int PersonnelCount = this.PersonBusiness.GetPersonCount();
            return GTS.Clock.Infrastructure.Utility.Utility.GetPageCount(PersonnelCount, PageSize).ToString();
        }

        protected void CallBack_trvParentDepartments_PersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvParentDepartments_PersonnelMainInformation(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.ErrorHiddenField_ParentDepartments_PersonnelMainInformation.RenderControl(e.Output);
            this.trvParentDepartments_PersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_trvParentDepartments_PersonnelMainInformation(decimal DepartmentID)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<Department> DepartmentsList = this.PersonBusiness.GetAllPersonnelDepartmentParents(DepartmentID);

                TreeViewNode trvNodeParentDepartment = null;

                foreach (Department department in DepartmentsList)
                {
                    TreeViewNode trvNodeDepartment = new TreeViewNode();
                    string trvNodeDepartmentText = string.Empty;
                    if (department.Parent == null && GetLocalResourceObject("OrgNode_trvParentDepartments_PersonnelMainInformation") != null)
                        trvNodeDepartmentText = GetLocalResourceObject("OrgNode_trvParentDepartments_PersonnelMainInformation").ToString();
                    else
                        trvNodeDepartmentText = department.Name;
                    trvNodeDepartment.Text = trvNodeDepartmentText;
                    trvNodeDepartment.ID = department.ID.ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                        trvNodeDepartment.ImageUrl = "Images/TreeView/folder.gif";
                    if (trvNodeParentDepartment == null)
                        this.trvParentDepartments_PersonnelMainInformation.Nodes.Add(trvNodeDepartment);
                    else
                        trvNodeParentDepartment.Nodes.Add(trvNodeDepartment);
                    trvNodeDepartment.Expanded = true;
                    trvNodeParentDepartment = trvNodeDepartment;
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ParentDepartments_PersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ParentDepartments_PersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ParentDepartments_PersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbGrade_DialogPersonnelMainInformation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_cmbGrade_DialogPersonnelMainInformation();
            this.ErrorHiddenField_Grade_DialogPersonnelMainInformation.RenderControl(e.Output);
            this.cmbGrade_DialogPersonnelMainInformation.RenderControl(e.Output);
        }

        private void Fill_cmbGrade_DialogPersonnelMainInformation()
        {
            string[] retMessage = new string[4];
            try
            {
                for (int i = 65; i < 75; i++)
                {
                    ComboBoxItem cmbItemGrade = new ComboBoxItem();
                    cmbItemGrade.Text = char.ConvertFromUtf32(i);
                    cmbItemGrade.Value = i.ToString();
                    this.cmbGrade_DialogPersonnelMainInformation.Items.Add(cmbItemGrade);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Grade_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Grade_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Grade_DialogPersonnelMainInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }



    }
}