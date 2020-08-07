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
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.RequestFlow;
using ComponentArt.Web.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using System.IO;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;
using Subgurim.Controles;
using System.Web.Configuration;
using GTS.Clock.Business.Security;
using System.Web.Script.Serialization;
using System.Text;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class HourlyRequestOnAbsence : GTSBasePage
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

        public IHourlyAbsenceBRequest RequestBusiness
        {
            get
            {
                return (IHourlyAbsenceBRequest)(BusinessHelper.GetBusinessInstance<BRequest>());
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

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        enum Scripts
        {
            HourlyRequestOnAbsence_onPageLoad,
            DialogHourlyRequestOnAbsence_Operations,
            Alert_Box,
            DropDownDive,
            HelpForm_Operations,
            DialogWaiting_Operations
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

        private string StrRequestAttachment = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridAbsencePairs_RequestOnAbsence.IsCallback && !CallBack_GridRegisteredRequests_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbLeaveType_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbMissionType_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbDoctorName_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbIllnessName_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbMissionLocation_HourlyRequestOnAbsence.IsCallback && !Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence.IsCallback && !Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence.IsCallback)
            {
                Page HourlyRequestOnAbsencePage = this;
                Ajax.Utility.GenerateMethodScripts(HourlyRequestOnAbsencePage);

                this.CheckHourlyRequestOnAbsenceLoadState_HourlyRequestOnAbsence();
                this.SetRequestsStatesStr_HourlyRequestOnAbsence();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }

            if (this.AttachmentUploader_Leave_HourlyRequestOnAbsence.IsPosting)
                this.ManagePostedData_AttachmentUploader_HourlyRequestOnAbsence(this.AttachmentUploader_Leave_HourlyRequestOnAbsence);
            if (this.AttachmentUploader_Mission_HourlyRequestOnAbsence.IsPosting)
                this.ManagePostedData_AttachmentUploader_HourlyRequestOnAbsence(this.AttachmentUploader_Mission_HourlyRequestOnAbsence);
            if (!Page.IsPostBack)
            {
                this.AttachmentUploader_Leave_HourlyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_Leave_HourlyRequestOnAbsence_OnAfterFileUpload('" + StrRequestAttachment + "');");
                this.AttachmentUploader_Mission_HourlyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_Mission_HourlyRequestOnAbsence_OnAfterFileUpload('" + StrRequestAttachment + "');");
            }
        }

        private void ManagePostedData_AttachmentUploader_HourlyRequestOnAbsence(FileUploaderAJAX AttachmentUploader)
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

        [Ajax.AjaxMethod("DeleteRequestAttachment_HourlyRequestOnAbsencePage", "DeleteRequestAttachment_HourlyRequestOnAbsencePage_onCallBack", null, null)]
        public string[] DeleteRequestAttachment_HourlyRequestOnAbsencePage(string RequestAttachmentSavedName)
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

        private void CheckHourlyRequestOnAbsenceLoadState_HourlyRequestOnAbsence()
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
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Manager:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onManagerLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
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


        private void SetRequestsStatesStr_HourlyRequestOnAbsence()
        {
            string strRequestsStates = string.Empty;
            foreach (RequestState requestsStateItem in Enum.GetValues(typeof(RequestState)))
            {
                strRequestsStates += "#" + GetLocalResourceObject(requestsStateItem.ToString()).ToString() + ":" + ((int)requestsStateItem).ToString();
            }
            this.hfRequestStates_HourlyRequestOnAbsence.Value = strRequestsStates;
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

        protected void CallBack_GridAbsencePairs_RequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridAbsencePairs_RequestOnAbsence((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_AbsencePairs.RenderControl(e.Output);
            this.GridAbsencePairs_RequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_GridAbsencePairs_RequestOnAbsence(RequestCaller RC, string DateKey, string RequestDate)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<MonthlyDetailReportProxy> HourlyAbsencePairsList = this.RequestBusiness.GetAllHourlyAbsence(RequestDate);
                this.GridAbsencePairs_RequestOnAbsence.DataSource = HourlyAbsencePairsList;
                this.GridAbsencePairs_RequestOnAbsence.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AbsencePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AbsencePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AbsencePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridRegisteredRequests_HourlyRequestOnAbsence((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
            this.GridRegisteredRequests_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_GridRegisteredRequests_HourlyRequestOnAbsence(RequestCaller RC, string DateKey, string RequestDate)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Request> RequestsList = this.RequestBusiness.GetAllHourlyLeaveDutyRequests(RequestDate);
                this.GridRegisteredRequests_HourlyRequestOnAbsence.DataSource = RequestsList;
                this.GridRegisteredRequests_HourlyRequestOnAbsence.DataBind();
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

        protected void CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbLeaveType_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbLeaveType_HourlyRequestOnAbsence();
            this.ErrorHiddenField_LeaveTypes.RenderControl(e.Output);
            this.cmbLeaveType_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbLeaveType_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> LeaveTypesList = this.RequestBusiness.GetAllHourlyLeaves();
                foreach (Precard LeaveTypesListItem in LeaveTypesList)
                {
                    ComboBoxItem cmbItemLeaveType = new ComboBoxItem(LeaveTypesListItem.Name);
                    cmbItemLeaveType.Value = LeaveTypesListItem.IsEstelajy.ToString().ToLower();
                    cmbItemLeaveType.Id = LeaveTypesListItem.ID.ToString();
                    this.cmbLeaveType_HourlyRequestOnAbsence.Items.Add(cmbItemLeaveType);
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

        protected void CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMissionType_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbMissionType_HourlyRequestOnAbsence();
            this.ErrorHiddenField_MissionTypes.RenderControl(e.Output);
            this.cmbMissionType_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbMissionType_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> MissionTypesList = this.RequestBusiness.GetAllHourlyDutis();
                this.cmbMissionType_HourlyRequestOnAbsence.DataSource = MissionTypesList;
                this.cmbMissionType_HourlyRequestOnAbsence.DataBind();
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

        protected void CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMissionLocation_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbMissionLocation_HourlyRequestOnAbsence();
            this.ErrorHiddenField_MissionLocations.RenderControl(e.Output);
            this.cmbMissionLocation_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbMissionLocation_HourlyRequestOnAbsence()
        {
            this.Fill_trvMissionLocation_HourlyRequestOnAbsence();
        }

        private void Fill_trvMissionLocation_HourlyRequestOnAbsence()
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
                this.trvMissionLocation_HourlyRequestOnAbsence.Nodes.Add(trvNodeNotDetermined);

                IList<DutyPlace> rootDutyPlacesList = this.RequestBusiness.GetAllDutyPlaceRoot();
                foreach (DutyPlace rootDutyPlaceItem in rootDutyPlacesList)
                {
                    TreeViewNode trvNodeRootDutyPlace = new TreeViewNode();
                    if (rootDutyPlaceItem.ParentID == 0 && this.GetLocalResourceObject("MissLocNode_trvMissionLocation_HourlyRequestOnAbsence") != null)
                        trvNodeRootDutyPlace.Text = this.GetLocalResourceObject("MissLocNode_trvMissionLocation_HourlyRequestOnAbsence").ToString();
                    else
                        trvNodeRootDutyPlace.Text = rootDutyPlaceItem.Name;
                    trvNodeRootDutyPlace.Value = trvNodeRootDutyPlace.ID = rootDutyPlaceItem.ID.ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                        trvNodeRootDutyPlace.ImageUrl = "Images/TreeView/folder.gif";
                    this.trvMissionLocation_HourlyRequestOnAbsence.Nodes.Add(trvNodeRootDutyPlace);
                    this.GetChildMissionLocation_trvMissionLocation_HourlyRequestOnAbsence(trvNodeRootDutyPlace, rootDutyPlaceItem);
                }

                this.trvMissionLocation_HourlyRequestOnAbsence.SelectedNode = trvNodeNotDetermined;
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

        private void GetChildMissionLocation_trvMissionLocation_HourlyRequestOnAbsence(TreeViewNode parentDutyPlaceNode, DutyPlace parentDutyPlace)
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
                    this.GetChildMissionLocation_trvMissionLocation_HourlyRequestOnAbsence(trvNodeChildDutyPlace, childDutyPlace);
            }
        }

        protected void CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbDoctorName_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbDoctorName_HourlyRequestOnAbsence();
            this.ErrorHiddenField_Doctors.RenderControl(e.Output);
            this.cmbDoctorName_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbDoctorName_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
                cmbItemNotDetermined.Value = "-1";
                this.cmbDoctorName_HourlyRequestOnAbsence.Items.Add(cmbItemNotDetermined);

                IList<Doctor> DoctorsList = this.RequestBusiness.GetAllDoctors();
                foreach (Doctor DoctorItem in DoctorsList)
                {
                    ComboBoxItem cmbItemDoctor = new ComboBoxItem(DoctorItem.Name);
                    cmbItemDoctor.Value = DoctorItem.ID.ToString();
                    this.cmbDoctorName_HourlyRequestOnAbsence.Items.Add(cmbItemDoctor);
                }
                this.cmbDoctorName_HourlyRequestOnAbsence.SelectedItem = cmbItemNotDetermined;
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

        protected void CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbIllnessName_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbIllnessName_HourlyRequestOnAbsence();
            this.ErrorHiddenField_Illnesses.RenderControl(e.Output);
            this.cmbIllnessName_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbIllnessName_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
                cmbItemNotDetermined.Value = "-1";
                this.cmbIllnessName_HourlyRequestOnAbsence.Items.Add(cmbItemNotDetermined);

                IList<Illness> IllnessesList = this.RequestBusiness.GetAllIllness();
                foreach (Illness IllnessItem in IllnessesList)
                {
                    ComboBoxItem cmbItemIllness = new ComboBoxItem(IllnessItem.Name);
                    cmbItemIllness.Value = IllnessItem.ID.ToString();
                    this.cmbIllnessName_HourlyRequestOnAbsence.Items.Add(cmbItemIllness);
                }
                this.cmbIllnessName_HourlyRequestOnAbsence.SelectedItem = cmbItemNotDetermined;
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

        [Ajax.AjaxMethod("UpdateRequest_HourlyRequestOnAbsencePage", "UpdateRequest_HourlyRequestOnAbsencePage_onCallBack", null, null)]
        public string[] UpdateRequest_HourlyRequestOnAbsencePage(string requestCaller, string requestLoadState, string state, string SelectedRequestID, string RequestType, string PreCardID, string RequestDate, string RequestFromTime, string RequestToTime, string IsRequestToTimeInNextDay, string RequestDescription, string IsSeakLeave, string PhysicianID, string IllnessID, string MissionLocationID, string RequestAttachmentFile)
        {
            this.InitializeCulture();

            string[] retMessage = new string[6];

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
                        RequestFromTime = this.StringBuilder.CreateString(RequestFromTime);
                        RequestToTime = this.StringBuilder.CreateString(RequestToTime);
                        bool isRequestToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsRequestToTimeInNextDay));
                        RequestDescription = this.StringBuilder.CreateString(RequestDescription);
                        bool isSeakLeave = bool.Parse(this.StringBuilder.CreateString(IsSeakLeave));
                        decimal physicianID = decimal.Parse(this.StringBuilder.CreateString(PhysicianID));
                        decimal illnessID = decimal.Parse(this.StringBuilder.CreateString(IllnessID));
                        decimal missionLocationID = decimal.Parse(this.StringBuilder.CreateString(MissionLocationID));
                        RequestAttachmentFile = this.StringBuilder.CreateString(RequestAttachmentFile);

                        request.TheFromDate = request.TheToDate = this.StringBuilder.CreateString(RequestDate);
                        Precard precard = new Precard();
                        precard.ID = preCardID;
                        request.Precard = precard;
                        request.TheFromTime = RequestFromTime;
                        request.TheToTime = RequestToTime;
                        request.ContinueOnTomorrow = isRequestToTimeInNextDay;
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
                                        request = this.RequestBusiness.InsertHourlyRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertHourlyRequest_onManagerLoadStateInGridSchema(request);
                                        break;
	                            }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertHourlyRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertHourlyRequest_onManagerLoadStateInGanttChartSchema(request);
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
                                        this.RequestBusiness.DeleteHourlyRequest_onPersonnelLoadStateInGridSchema(request);
                                        this.MasterRequestBusiness.DeleteRequestAttachment(WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + request.AttachmentFile);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteHourlyRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.MasterRequestBusiness.GetRequestByID(request.ID);
                                        this.RequestBusiness.DeleteHourlyRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        this.MasterRequestBusiness.DeleteRequestAttachment(WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + request.AttachmentFile);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteHourlyRequest_onManagerLoadStateInGanttChartSchema(request);
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

        protected void Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.AttachmentUploader_Leave_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        protected void Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            this.AttachmentUploader_Mission_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

    }
}