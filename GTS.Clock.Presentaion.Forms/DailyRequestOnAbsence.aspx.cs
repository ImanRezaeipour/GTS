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
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using System.IO;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;
using Subgurim.Controles;
using System.Web.Configuration;
using GTS.Clock.Business.Security;
using System.Web.Script.Serialization;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class DailyRequestOnAbsence : GTSBasePage
    {
        enum RequestCaller
        {
            Grid,
            GanttChart
        }

        enum RequestLoadState
        {
            Personnel,
            Manager
        }

        enum RequestTypes
        {
            Leave,
            Mission
        }

        public IDailyAbsenceBRequest RequestBusiness
        {
            get
            {
                return (IDailyAbsenceBRequest)(BusinessHelper.GetBusinessInstance<BRequest>());
            }
        }

        public BRequest MasterRequestBusiness
        {
            get
            {
                return new BRequest();
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

        internal class ObjRequestAttachment
        {
            public string RequestAttachmentPath { get; set; }
            public string RequestAttachmentRealName { get; set; }
            public string RequestAttachmentSavedPath { get; set; }
            public string RequestAttachmentSavedName { get; set; }
            public bool IsErrorOccured { get; set; }
            public string Message { get; set; }
        }

        enum Scripts
        {
            DailyRequestOnAbsence_onPageLoad,
            DialogDailyRequestOnAbsence_Operations,
            Alert_Box,
            DropDownDive,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        private string StrRequestAttachment = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridRegisteredRequests_DailyRequestOnAbsence.IsCallback && !CallBack_cmbDoctorName_DailyRequestOnAbsence.IsCallback && !CallBack_cmbIllnessName_DailyRequestOnAbsence.IsCallback && !CallBack_cmbLeaveType_DailyRequestOnAbsence.IsCallback && !CallBack_cmbMissionLocation_DailyRequestOnAbsence.IsCallback && !CallBack_cmbMissionType_DailyRequestOnAbsence.IsCallback && !Callback_AttachmentUploader_Leave_DailyRequestOnAbsence.IsCallback && !Callback_AttachmentUploader_Mission_DailyRequestOnAbsence.IsCallback)
            {
                Page DailyRequestOnAbsencePage = this.Page;
                Ajax.Utility.GenerateMethodScripts(DailyRequestOnAbsencePage);

                this.CheckDailyRequestOnAbsenceLoadState_DailyRequestOnAbsence();
                this.ViewCurrentLangCalendars_DailyRequestOnAbsence();
                this.SetCurrentDate_DailyRequestOnAbsence();
                this.SetRequestsStatesStr_DailyRequestOnAbsence();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }

            if (this.AttachmentUploader_Leave_DailyRequestOnAbsence.IsPosting)
                this.ManagePostedData_AttachmentUploader_DailyRequestOnAbsence(this.AttachmentUploader_Leave_DailyRequestOnAbsence);
            if (this.AttachmentUploader_Mission_DailyRequestOnAbsence.IsPosting)
                this.ManagePostedData_AttachmentUploader_DailyRequestOnAbsence(this.AttachmentUploader_Mission_DailyRequestOnAbsence);
            if (!Page.IsPostBack)
            {
                this.AttachmentUploader_Leave_DailyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_Leave_DailyRequestOnAbsence_OnAfterFileUpload('" + StrRequestAttachment + "');");
                this.AttachmentUploader_Mission_DailyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_Mission_DailyRequestOnAbsence_OnAfterFileUpload('" + StrRequestAttachment + "');");
            }
        }

        private void ManagePostedData_AttachmentUploader_DailyRequestOnAbsence(FileUploaderAJAX AttachmentUploader)
        {
            try
            {
                string separator = "_";
                string physicalPath = WebConfigurationManager.AppSettings["RequestAttachmentsPath"];
                string path = "ClientAttachments";
                HttpPostedFileAJAX HPFA = AttachmentUploader.PostedFile;
                string RequestAttachmentSavedFileName = Guid.NewGuid().ToString() + separator + BUser.CurrentUser.Person.BarCode + separator + HPFA.FileName;
                ObjRequestAttachment RequestAttachment = new ObjRequestAttachment()
                {
                    RequestAttachmentPath = path,
                    RequestAttachmentRealName = HPFA.FileName,
                    RequestAttachmentSavedPath = path + "/" + RequestAttachmentSavedFileName,
                    RequestAttachmentSavedName = RequestAttachmentSavedFileName
                };
                this.StrRequestAttachment = this.JsSerializer.Serialize(RequestAttachment);
                AttachmentUploader.PostedFile.responseMessage_Uploaded_Saved = " ";
                AttachmentUploader.PostedFile.responseMessage_Uploaded_NotSaved = " ";
                AttachmentUploader.SaveAs(path, RequestAttachmentSavedFileName);
                File.Move(Server.MapPath(path + "\\" + RequestAttachmentSavedFileName), physicalPath + "\\" + RequestAttachmentSavedFileName);
            }
            catch (Exception ex)
            {
                ObjRequestAttachment RequestAttachment = new ObjRequestAttachment()
                {
                    IsErrorOccured = true,
                    Message = GetLocalResourceObject("UploadingError").ToString()
                };
                this.StrRequestAttachment = this.JsSerializer.Serialize(RequestAttachment);
            }
        }

        [Ajax.AjaxMethod("DeleteRequestAttachment_DailyRequestOnAbsencePage", "DeleteRequestAttachment_DailyRequestOnAbsencePage_onCallBack", null, null)]
        public string[] DeleteRequestAttachment_DailyRequestOnAbsencePage(string RequestAttachmentSavedName)
        {
            this.InitializeCulture();

            string[] retMessage = new string[3];

            try
            {
                RequestAttachmentSavedName = this.StringBuilder.CreateString(RequestAttachmentSavedName);
                string filePath = WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + RequestAttachmentSavedName;
                this.MasterRequestBusiness.DeleteRequestAttachment(filePath);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("DeleteComplete").ToString();
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


        private void CheckDailyRequestOnAbsenceLoadState_DailyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RC") && HttpContext.Current.Request.QueryString.AllKeys.Contains("RLS"))
                {
                    RequestLoadState requestLoadState = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RLS"]));
                    RequestCaller requestCaller = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RC"]));

                    switch (requestLoadState)
                    {
                        case RequestLoadState.Personnel:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckDailyRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckDailyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Manager:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckDailyRequestLoadAccess_onManagerLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckDailyRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        private void ViewCurrentLangCalendars_DailyRequestOnAbsence()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpFromDate_Leave_DailyRequestOnAbsence.Visible = true;
                    this.Container_pdpToDate_Leave_DailyRequestOnAbsence.Visible = true;
                    this.Container_pdpFromDate_Mission_DailyRequestOnAbsence.Visible = true;
                    this.Container_pdpToDate_Mission_DailyRequestOnAbsence.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpFromDate_Leave_DailyRequestOnAbsence.Visible = true;
                    this.Container_gdpToDate_Leave_DailyRequestOnAbsence.Visible = true;
                    this.Container_gdpFromDate_Mission_DailyRequestOnAbsence.Visible = true;
                    this.Container_gdpToDate_Mission_DailyRequestOnAbsence.Visible = true;
                    break;
            }
        }

        private void SetCurrentDate_DailyRequestOnAbsence()
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
            this.hfCurrentDate_DailyRequestOnAbsence.Value = strCurrentDate;
        }

        private void SetRequestsStatesStr_DailyRequestOnAbsence()
        {
            string strRequestsStates = string.Empty;
            foreach (RequestState requestsStateItem in Enum.GetValues(typeof(RequestState)))
            {
                strRequestsStates += "#" + GetLocalResourceObject(requestsStateItem.ToString()).ToString() + ":" + ((int)requestsStateItem).ToString();
            }
            this.hfRequestStates_DailyRequestOnAbsence.Value = strRequestsStates;
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

        protected void CallBack_GridRegisteredRequests_DailyRequestOnAbsence_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.Fill_GridRegisteredRequests_DailyRequestOnAbsence((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
            this.GridRegisteredRequests_DailyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_GridRegisteredRequests_DailyRequestOnAbsence(RequestCaller RC, string DateKey, string RequestDate)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Request> RequestsList = this.RequestBusiness.GetAllDailyLeaveDutyRequests(RequestDate);
                this.GridRegisteredRequests_DailyRequestOnAbsence.DataSource = RequestsList;
                this.GridRegisteredRequests_DailyRequestOnAbsence.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbLeaveType_DailyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbLeaveType_DailyRequestOnAbsence.Dispose();
            this.Fill_cmbLeaveType_DailyRequestOnAbsence();
            this.ErrorHiddenField_LeaveTypes.RenderControl(e.Output);
            this.cmbLeaveType_DailyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbLeaveType_DailyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> LeaveTypesList = this.RequestBusiness.GetAllDailyLeaves();
                foreach (Precard LeaveTypesListItem in LeaveTypesList)
                {
                    ComboBoxItem cmbItemLeaveType = new ComboBoxItem(LeaveTypesListItem.Name);
                    cmbItemLeaveType.Value = LeaveTypesListItem.IsEstelajy.ToString().ToLower();
                    cmbItemLeaveType.Id = LeaveTypesListItem.ID.ToString();
                    this.cmbLeaveType_DailyRequestOnAbsence.Items.Add(cmbItemLeaveType);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbDoctorName_DailyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbDoctorName_DailyRequestOnAbsence.Dispose();
            this.Fill_cmbDoctorName_DailyRequestOnAbsence();
            this.ErrorHiddenField_Doctors.RenderControl(e.Output);
            this.cmbDoctorName_DailyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbDoctorName_DailyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
                cmbItemNotDetermined.Value = "-1";
                this.cmbDoctorName_DailyRequestOnAbsence.Items.Add(cmbItemNotDetermined);

                IList<Doctor> DoctorsList = this.RequestBusiness.GetAllDoctors();
                foreach (Doctor DoctorItem in DoctorsList)
                {
                    ComboBoxItem cmbItemDoctor = new ComboBoxItem(DoctorItem.Name);
                    cmbItemDoctor.Value = DoctorItem.ID.ToString();
                    this.cmbDoctorName_DailyRequestOnAbsence.Items.Add(cmbItemDoctor);
                }
                this.cmbDoctorName_DailyRequestOnAbsence.SelectedItem = cmbItemNotDetermined;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Doctors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Doctors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Doctors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbIllnessName_DailyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbIllnessName_DailyRequestOnAbsence.Dispose();
            this.Fill_cmbIllnessName_DailyRequestOnAbsence();
            this.ErrorHiddenField_Illnesses.RenderControl(e.Output);
            this.cmbIllnessName_DailyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbIllnessName_DailyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
                cmbItemNotDetermined.Value = "-1";
                this.cmbIllnessName_DailyRequestOnAbsence.Items.Add(cmbItemNotDetermined);

                IList<Illness> IllnessesList = this.RequestBusiness.GetAllIllness();
                foreach (Illness IllnessItem in IllnessesList)
                {
                    ComboBoxItem cmbItemIllness = new ComboBoxItem(IllnessItem.Name);
                    cmbItemIllness.Value = IllnessItem.ID.ToString();
                    this.cmbIllnessName_DailyRequestOnAbsence.Items.Add(cmbItemIllness);
                }
                this.cmbIllnessName_DailyRequestOnAbsence.SelectedItem = cmbItemNotDetermined;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Illnesses.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Illnesses.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Illnesses.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMissionType_DailyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMissionType_DailyRequestOnAbsence.Dispose();
            this.Fill_cmbMissionType_DailyRequestOnAbsence();
            this.ErrorHiddenField_MissionTypes.RenderControl(e.Output);
            this.cmbMissionType_DailyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbMissionType_DailyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> MissionTypesList = this.RequestBusiness.GetAllDailyDuties();
                this.cmbMissionType_DailyRequestOnAbsence.DataSource = MissionTypesList;
                this.cmbMissionType_DailyRequestOnAbsence.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMissionLocation_DailyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMissionLocation_DailyRequestOnAbsence.Dispose();
            this.Fill_cmbMissionLocation_DailyRequestOnAbsence();
            this.ErrorHiddenField_MissionLocations.RenderControl(e.Output);
            this.cmbMissionLocation_DailyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbMissionLocation_DailyRequestOnAbsence()
        {
            this.Fill_trvMissionLocation_DailyRequestOnAbsence();
        }

        private void Fill_trvMissionLocation_DailyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                TreeViewNode trvNodeNotDetermined = new TreeViewNode();
                trvNodeNotDetermined.Text = GetLocalResourceObject("NotDetermined").ToString();
                trvNodeNotDetermined.ID = "-1";
                trvNodeNotDetermined.Value = "NotDetermined";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder_blue.gif"))
                    trvNodeNotDetermined.ImageUrl = "Images/TreeView/folder_blue.gif";
                this.trvMissionLocation_DailyRequestOnAbsence.Nodes.Add(trvNodeNotDetermined);

                IList<DutyPlace> rootDutyPlacesList = this.RequestBusiness.GetAllDutyPlaceRoot();
                foreach (DutyPlace rootDutyPlaceItem in rootDutyPlacesList)
                {
                    TreeViewNode trvNodeRootDutyPlace = new TreeViewNode();
                    if (rootDutyPlaceItem.ParentID == 0 && this.GetLocalResourceObject("MissLocNode_trvMissionLocation_DailyRequestOnAbsence") != null)
                        trvNodeRootDutyPlace.Text = this.GetLocalResourceObject("MissLocNode_trvMissionLocation_DailyRequestOnAbsence").ToString();                       
                    else
                        trvNodeRootDutyPlace.Text = rootDutyPlaceItem.Name;
                    trvNodeRootDutyPlace.Value = trvNodeRootDutyPlace.ID = rootDutyPlaceItem.ID.ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                        trvNodeRootDutyPlace.ImageUrl = "Images/TreeView/folder.gif";
                    this.trvMissionLocation_DailyRequestOnAbsence.Nodes.Add(trvNodeRootDutyPlace);
                    this.GetChildMissionLocation_trvMissionLocation_DailyRequestOnAbsence(trvNodeRootDutyPlace, rootDutyPlaceItem);
                }

                this.trvMissionLocation_DailyRequestOnAbsence.SelectedNode = trvNodeNotDetermined;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildMissionLocation_trvMissionLocation_DailyRequestOnAbsence(TreeViewNode parentDutyPlaceNode, DutyPlace parentDutyPlace)
        {
            foreach (DutyPlace childDutyPlace in this.RequestBusiness.GetAllDutyPlaceChild(parentDutyPlace.ID))
            {
                TreeViewNode trvNodeChildDutyPlace = new TreeViewNode();
                trvNodeChildDutyPlace.Text = childDutyPlace.Name;
                trvNodeChildDutyPlace.ID = childDutyPlace.ID.ToString();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                    trvNodeChildDutyPlace.ImageUrl = "Images/TreeView/folder.gif";
                parentDutyPlaceNode.Nodes.Add(trvNodeChildDutyPlace);
                if (this.RequestBusiness.GetAllDutyPlaceChild(childDutyPlace.ID).Count > 0)
                    this.GetChildMissionLocation_trvMissionLocation_DailyRequestOnAbsence(trvNodeChildDutyPlace, childDutyPlace);
            }
        }

        [Ajax.AjaxMethod("UpdateRequest_DailyRequestOnAbsencePage", "UpdateRequest_DailyRequestOnAbsencePage_onCallBack", null, null)]
        public string[] UpdateRequest_DailyRequestOnAbsencePage(string requestCaller, string requestLoadState, string state, string SelectedRequestID, string RequestType, string PreCardID, string RequestDate, string RequestFromDate, string RequestToDate, string RequestDescription, string IsSeakLeave, string PhysicianID, string IllnessID, string MissionLocationID, string RequestAttachmentFile)
        {
            this.InitializeCulture();

            string[] retMessage = new string[7];

            try
            {
                decimal RequestID = 0;
                decimal selectedRequestID = decimal.Parse(this.StringBuilder.CreateString(SelectedRequestID));
                Request request = new Request();
                request.ID = selectedRequestID;
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
                RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(requestCaller));
                RequestLoadState RLS = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(requestLoadState));

                switch (uam)
                {
                    case UIActionType.ADD:
                        RequestTypes requestType = (RequestTypes)Enum.Parse(typeof(RequestTypes), this.StringBuilder.CreateString(RequestType));
                        decimal preCardID = decimal.Parse(this.StringBuilder.CreateString(PreCardID));
                        RequestFromDate = this.StringBuilder.CreateString(RequestFromDate);
                        RequestToDate = this.StringBuilder.CreateString(RequestToDate);
                        RequestDescription = this.StringBuilder.CreateString(RequestDescription);
                        bool isSeakLeave = bool.Parse(this.StringBuilder.CreateString(IsSeakLeave));
                        decimal physicianID = decimal.Parse(this.StringBuilder.CreateString(PhysicianID));
                        decimal illnessID = decimal.Parse(this.StringBuilder.CreateString(IllnessID));
                        decimal missionLocationID = decimal.Parse(this.StringBuilder.CreateString(MissionLocationID));
                        RequestAttachmentFile = this.StringBuilder.CreateString(RequestAttachmentFile);
                        
                        request.RegisterDate = Utility.ToMildiDateTime(this.StringBuilder.CreateString(RequestDate));
                        Precard precard = new Precard();
                        precard.ID = preCardID;
                        request.Precard = precard;
                        request.TheFromDate = RequestFromDate;
                        request.TheToDate = RequestToDate;
                        request.IsDateSetByUser = true;
                        request.Description = RequestDescription;
                        request.AttachmentFile = RequestAttachmentFile;
                        switch (requestType)
                        {
                            case RequestTypes.Leave:
                                if (isSeakLeave)
                                {
                                    if (physicianID != -1)
                                        request.DoctorID = physicianID;
                                    if (illnessID != -1)
                                        request.IllnessID = illnessID;
                                }
                                break;
                            case RequestTypes.Mission:
                                if (missionLocationID != -1)
                                    request.DutyPositionID = missionLocationID;
                                break;
                            default:
                                break;
                        }

                        switch (RC)
	                    {
                            case RequestCaller.Grid:
                                switch (RLS)
	                            {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertDailyRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertDailyRequest_onManagerLoadStateInGridSchema(request);
                                        break;
	                            }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertDailyRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertDailyRequest_onManagerLoadStateInGanttChartSchema(request);
                                        break;
                                }
                                break;
	                    }
                        RequestID = request.ID;
                        break;
                    case UIActionType.DELETE:
                        if (selectedRequestID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRequestSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        switch (RC)
                        {
                            case RequestCaller.Grid:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.MasterRequestBusiness.GetRequestByID(request.ID);
                                        this.RequestBusiness.DeleteDailyRequest_onPersonnelLoadStateInGridSchema(request);
                                        this.MasterRequestBusiness.DeleteRequestAttachment(WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + request.AttachmentFile);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteDailyRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.MasterRequestBusiness.GetRequestByID(request.ID);
                                        this.RequestBusiness.DeleteDailyRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        this.MasterRequestBusiness.DeleteRequestAttachment(WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + request.AttachmentFile);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteDailyRequest_onManagerLoadStateInGanttChartSchema(request);
                                        break;
                                }
                                break;
                        }
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case Business.UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case Business.UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = RequestID.ToString();
                retMessage[4] = ((int)request.Status).ToString();
                retMessage[5] = request.RegistrationDate;
                retMessage[6] = request.AddClientSide.ToString().ToLower();
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

        protected void Callback_AttachmentUploader_Leave_DailyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.AttachmentUploader_Leave_DailyRequestOnAbsence.RenderControl(e.Output);
        }

        protected void Callback_AttachmentUploader_Mission_DailyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.AttachmentUploader_Mission_DailyRequestOnAbsence.RenderControl(e.Output);
        }


    }
}