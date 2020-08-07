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
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.BaseInformation;
using System.IO;
using GTS.Clock.Model.Concepts;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;
using System.Web.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using System.Reflection;
using Subgurim.Controles;
using System.Web.Configuration;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.RequestFlow;

public partial class RequestRegister : GTSBasePage
{
    const string ZeroTime = "00:00";

    public IRegisteredRequests RequestRegisterBusiness
    {
        get
        {
            return (IRegisteredRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
        }
    }

    public BImperativeRequest ImperativeRequestBusiness
    {
        get
        {
            return new BImperativeRequest();
        }
    }

    public BRequest MasterRequestBusiness
    {
        get
        {
            return new BRequest();
        }
    }

    enum RequestCaller
    {
        NormalUser,
        Operator
    }

    enum RequestPersonnelCountState
    {
        Single,
        Collective
    }

    internal class RequestTargetFeatures
    {
        public bool IsSickLeave { get; set; }
        public bool IsMission { get; set; }
        public bool IsTraffic { get; set; }
    }

    private enum RequestTarget
    {
        Hourly,
        Daily,
        OverTime,
        Imperative
    }

    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get
        {
            return new OperationYearMonthProvider();
        }
    }

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public enum LoadState
    {
        Normal,
        Search,
        AdvancedSearch
    }

    internal class PersonnelDetails
    {
        public string ID { get; set; }
        public string OrganizationPostID { get; set; }
        public string OrganizationPostName { get; set; }
    }

    internal class ImperativeProxy
    {
        public string PersonID { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string PersonImage { get; set; }
        public decimal ImperativeValue { get; set; }
        public bool IsLockedImperative { get; set; }
        public string CalcInfo { get; set; }
    }

    public JavaScriptSerializer JsSeializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public ISearchPerson PersonnelBusiness
    {
        get
        {
            return (ISearchPerson)(new BPerson());
        }
    }

    enum Scripts
    {
        RequestRegister_onPageLoad,
        DialogRequestRegister_Operations,
        Alert_Box,
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
        if (!CallBack_cmbDoctors_tbDaily_RequestRegister.IsCallback && !CallBack_cmbDoctors_tbHourly_RequestRegister.IsCallback && !CallBack_cmbIllnesses_tbDaily_RequestRegister.IsCallback && !CallBack_cmbIllnesses_tbHourly_RequestRegister.IsCallback && !CallBack_cmbMissionLocation_tbDaily_RequestRegister.IsCallback && !CallBack_cmbMissionLocation_tbHourly_RequestRegister.IsCallback && !CallBack_cmbRequestType_tbHourly_RequestRegister.IsCallback && !CallBack_cmbRequestType_tbDaily_RequestRegister.IsCallback && !CallBack_cmbRequestType_tbOverTime_RequestRegister.IsCallback && !CallBack_cmbPersonnel_RequestRegister.IsCallback && !CallBack_GridPersonnel_CollectiveTraffic.IsCallback && !CallBack_GridPersonnel_tbImperative_RequestRegister.IsCallback && !CallBack_GridPersonnel_tbImperative_RequestRegister.IsCallback && !Callback_AttachmentUploader_tbHourly_RequestRegister.IsCallback)
        {
            Page RequestRegisterPage = this;
            Ajax.Utility.GenerateMethodScripts(RequestRegisterPage);

            this.CheckRequestRegisterLoadAccess_ReqiestRegister();
            this.CustomizeRequestRegister_RequestRegister();
            this.ViewCurrentLangCalendars_RequestRegister();
            this.SetCurrentDate_RequestRegiser();
            this.SetPersonnelPageSize_cmbPersonnel_RequestRegister();
            this.SetPersonnelPageCount_cmbPersonnel_RequestRegister(LoadState.Normal, this.cmbPersonnel_RequestRegister.DropDownPageSize, string.Empty);
            this.SetPersonnelPageSize_GridPersonnel_CollectiveTraffic();
            this.SetPersonnelPageCount_GridPersonnel_CollectiveTraffic(LoadState.Normal, this.GridPersonnel_CollectiveTraffic.PageSize, string.Empty);
            this.SetPersonnelPageSize_GridPersonnel_tbImperative_RequestRegister();
            this.Fill_cmbYear_tbImperative_RequestRegister();
            this.Fill_cmbMonth_tbImperative_RequestRegister();
            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        if (this.AttachmentUploader_tbHourly_RequestRegister.IsPosting)
            this.ManagePostedData_AttachmentUploader_RequestRegister(this.AttachmentUploader_tbHourly_RequestRegister);
        if (this.AttachmentUploader_tbDaily_RequestRegister.IsPosting)
            this.ManagePostedData_AttachmentUploader_RequestRegister(this.AttachmentUploader_tbDaily_RequestRegister);

        if (!Page.IsPostBack)
        {
            this.AttachmentUploader_tbHourly_RequestRegister.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_tbHourly_RequestRegister_OnAfterFileUpload('" + StrRequestAttachment + "');");
            this.AttachmentUploader_tbDaily_RequestRegister.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_tbDaily_RequestRegister_OnAfterFileUpload('" + StrRequestAttachment + "');");
        }
    }

    private void ManagePostedData_AttachmentUploader_RequestRegister(FileUploaderAJAX AttachmentUploader)
    {
        try
        {
            string separator = "_";
            string physicalPath = WebConfigurationManager.AppSettings["RequestAttachmentsPath"];
            string path = "ClientAttachments";
            HttpPostedFileAJAX HPFA = AttachmentUploader.PostedFile;
            string operatorSeparator = string.Empty;
            if ((new BOperator().GetOperator(BUser.CurrentUser.Person.ID)).Count() > 0)
                operatorSeparator = "Operator";
            string RequestAttachmentSavedFileName = Guid.NewGuid().ToString() + separator + operatorSeparator + separator + BUser.CurrentUser.Person.BarCode + separator + HPFA.FileName;
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

    [Ajax.AjaxMethod("DeleteRequestAttachment_RequestRegisterPage", "DeleteRequestAttachment_RequestRegisterPage_onCallBack", null, null)]
    public string[] DeleteRequestAttachment_RequestRegisterPage(string requestTarget, string RequestAttachmentSavedName)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            RequestTarget RT = (RequestTarget)Enum.Parse(typeof(RequestTarget), this.StringBuilder.CreateString(requestTarget));
            RequestAttachmentSavedName = this.StringBuilder.CreateString(RequestAttachmentSavedName);
            string filePath = WebConfigurationManager.AppSettings["RequestAttachmentsPath"] + "\\" + RequestAttachmentSavedName;
            this.MasterRequestBusiness.DeleteRequestAttachment(filePath);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("DeleteComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = RT.ToString();
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

    private void CheckRequestRegisterLoadAccess_ReqiestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
            {
                RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
                switch (RC)
                {
                    case RequestCaller.NormalUser:
                        this.RequestRegisterBusiness.CheckRequestRgisterLoadAccess_onNormalUser();
                        break;
                    case RequestCaller.Operator:
                        this.RequestRegisterBusiness.CheckRequestRgisterLoadAccess_onOperator();
                        break;
                }
            }
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void CustomizeRequestRegister_RequestRegister()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            switch (RC)
            {
                case RequestCaller.NormalUser:
                    this.Container_PersonnelSelect_RequestRegister.Visible = false;
                    this.TabStripRequestRegister.Tabs[3].Visible = false;
                    break;
                case RequestCaller.Operator:
                    break;
            }
        }
    }

    private void ViewCurrentLangCalendars_RequestRegister()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_tbDaily_RequestRegister.Visible = true;
                this.Container_pdpFromDate_tbOverTime_RequestRegister.Visible = true;
                this.Container_pdpRequestDate_tbHourly_RequestRegister.Visible = true;
                this.Container_pdpToDate_tbDaily_RequestRegister.Visible = true;
                this.Container_pdpToDate_tbOverTime_RequestRegister.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_tbDaily_RequestRegister.Visible = true;
                this.Container_gdpFromDate_tbOverTime_RequestRegister.Visible = true;
                this.Container_gdpRequestDate_tbHourly_RequestRegister.Visible = true;
                this.Container_gdpToDate_tbDaily_RequestRegister.Visible = false;
                this.Container_gdpToDate_tbOverTime_RequestRegister.Visible = false;
                break;
        }
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripRequestRegister);
    }

    private void SetCurrentDate_RequestRegiser()
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
        this.hfCurrentDate_RequestRegister.Value = strCurrentDate;
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

    private void SetPersonnelPageSize_cmbPersonnel_RequestRegister()
    {
        this.hfPersonnelPageSize_RequestRegister.Value = this.cmbPersonnel_RequestRegister.DropDownPageSize.ToString();
    }

    private void SetPersonnelPageCount_cmbPersonnel_RequestRegister(LoadState Ls, int pageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int PersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), PersonCategory.Operator_UnderManagment);
                    break;
                default:
                    break;
            }
            this.hfPersonnelCount_RequestRegister.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_RequestRegister.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void SetPersonnelPageSize_GridPersonnel_CollectiveTraffic()
    {
        this.hfPersonnelPageSize_Personnel_CollectiveTraffic.Value = this.GridPersonnel_CollectiveTraffic.PageSize.ToString();
    }

    private void SetPersonnelPageCount_GridPersonnel_CollectiveTraffic(LoadState Ls, int PageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int PersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(string.Empty, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), PersonCategory.Operator_UnderManagment);
                    break;
                default:
                    break;
            }
            this.hfPersonnelCount_Personnel_CollectiveTraffic.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_Personnel_CollectiveTraffic.Value = Utility.GetPageCount(PersonnelCount, this.GridPersonnel_CollectiveTraffic.PageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void SetPersonnelPageSize_GridPersonnel_tbImperative_RequestRegister()
    {
        this.hfImperativePageSize_tbImperative_RequestRegister.Value = this.GridPersonnel_tbImperative_RequestRegister.PageSize.ToString();
    }

    private void SetImperativePageCount_GridPersonnel_tbImperative_RequestRegister(LoadState Ls, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, PersonCategory searchInCategory, int PageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int ImperativePersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    ImperativePersonnelCount = this.ImperativeRequestBusiness.GetQuickSearchPersonCountByImperativeRequest(string.Empty, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    ImperativePersonnelCount = this.ImperativeRequestBusiness.GetQuickSearchPersonCountByImperativeRequest(SearchTerm, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    ImperativePersonnelCount = this.ImperativeRequestBusiness.GetAdvancedSearchPersonCountByImperativeRequest(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment);
                    break;
            }
            this.hfImperativeCount_tbImperative_RequestRegister.Value = ImperativePersonnelCount.ToString();
            this.hfImperativePageCount_tbImperative_RequestRegister.Value = Utility.GetPageCount(ImperativePersonnelCount, this.GridPersonnel_tbImperative_RequestRegister.PageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbIllnesses_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbIllnesses_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbIllnesses_tbHourly_RequestRegister();
        this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbIllnesses_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbIllnesses_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Illnesses_RequestRegister(cmbIllnesses_tbHourly_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_Illnesses_RequestRegister(ComponentArt.Web.UI.ComboBox cmbIllnesses)
    {
        this.InitializeCulture();

        ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
        cmbItemNotDetermined.Value = "0";
        cmbIllnesses.Items.Add(cmbItemNotDetermined);

        IList<Illness> IllnessesList = this.RequestRegisterBusiness.GetAllIllness();
        foreach (Illness IllnessItem in IllnessesList)
        {
            ComboBoxItem cmbItemIllness = new ComboBoxItem(IllnessItem.Name);
            cmbItemIllness.Value = IllnessItem.ID.ToString();
            cmbIllnesses.Items.Add(cmbItemIllness);
        }
        cmbIllnesses.SelectedItem = cmbItemNotDetermined;
    }

    protected void CallBack_cmbDoctors_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbDoctors_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbDoctors_tbHourly_RequestRegister();
        this.ErrorHiddenField_Doctors_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbDoctors_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbDoctors_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Doctors_RequestRegister(cmbDoctors_tbHourly_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_Doctors_RequestRegister(ComponentArt.Web.UI.ComboBox cmbDoctors)
    {
        this.InitializeCulture();

        ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
        cmbItemNotDetermined.Value = "0";
        cmbDoctors.Items.Add(cmbItemNotDetermined);

        IList<Doctor> DoctorsList = this.RequestRegisterBusiness.GetAllDoctors();
        foreach (Doctor DoctorItem in DoctorsList)
        {
            ComboBoxItem cmbItemDoctor = new ComboBoxItem(DoctorItem.Name);
            cmbItemDoctor.Value = DoctorItem.ID.ToString();
            cmbDoctors.Items.Add(cmbItemDoctor);
        }
        cmbDoctors.SelectedItem = cmbItemNotDetermined;
    }

    protected void CallBack_cmbMissionLocation_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbMissionLocation_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbMissionLocation_tbHourly_RequestRegister();
        this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbMissionLocation_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbMissionLocation_tbHourly_RequestRegister()
    {
        this.Fill_trvMissionLocation_tbHourly_RequestRegister();
    }

    private void Fill_trvMissionLocation_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_MissionLocations_RequestRegister(trvMissionLocation_tbHourly_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_MissionLocations_RequestRegister(ComponentArt.Web.UI.TreeView trvMissionLocations)
    {
        this.InitializeCulture();

        TreeViewNode trvNodeNotDetermined = new TreeViewNode();
        trvNodeNotDetermined.Text = GetLocalResourceObject("NotDetermined").ToString();
        trvNodeNotDetermined.ID = "-1";
        trvNodeNotDetermined.Value = "NotDetermined";
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder_blue.gif"))
            trvNodeNotDetermined.ImageUrl = "Images/TreeView/folder_blue.gif";
        trvMissionLocations.Nodes.Add(trvNodeNotDetermined);

        IList<DutyPlace> rootDutyPlacesList = this.RequestRegisterBusiness.GetAllDutyPlaceRoot();
        foreach (DutyPlace rootDutyPlaceItem in rootDutyPlacesList)
        {
            TreeViewNode trvNodeRootDutyPlace = new TreeViewNode();
            if (rootDutyPlaceItem.ParentID == 0 && this.GetLocalResourceObject("MissLocNode_trvMissionLocations_RequestRegister") != null)
                trvNodeRootDutyPlace.Text = this.GetLocalResourceObject("MissLocNode_trvMissionLocations_RequestRegister").ToString();
            else
                trvNodeRootDutyPlace.Text = rootDutyPlaceItem.Name;
            trvNodeRootDutyPlace.Value = trvNodeRootDutyPlace.ID = rootDutyPlaceItem.ID.ToString();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                trvNodeRootDutyPlace.ImageUrl = "Images/TreeView/folder.gif";
            trvMissionLocations.Nodes.Add(trvNodeRootDutyPlace);
            this.GetChildMissionLocations_MissionLocations_RequestRegister(trvNodeRootDutyPlace, rootDutyPlaceItem);
        }

        trvMissionLocations.SelectedNode = trvNodeNotDetermined;
    }

    private void GetChildMissionLocations_MissionLocations_RequestRegister(TreeViewNode parentDutyPlaceNode, DutyPlace parentDutyPlace)
    {
        foreach (DutyPlace childDutyPlace in this.RequestRegisterBusiness.GetAllDutyPlaceChild(parentDutyPlace.ID))
        {
            TreeViewNode trvNodeChildDutyPlace = new TreeViewNode();
            trvNodeChildDutyPlace.Text = childDutyPlace.Name;
            trvNodeChildDutyPlace.ID = childDutyPlace.ID.ToString();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                trvNodeChildDutyPlace.ImageUrl = "Images/TreeView/folder.gif";
            parentDutyPlaceNode.Nodes.Add(trvNodeChildDutyPlace);
            if (this.RequestRegisterBusiness.GetAllDutyPlaceChild(childDutyPlace.ID).Count > 0)
                this.GetChildMissionLocations_MissionLocations_RequestRegister(trvNodeChildDutyPlace, childDutyPlace);
        }
    }


    protected void CallBack_cmbRequestType_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbRequestType_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbRequestType_tbHourly_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllHourlyRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                RequestTargetFeatures RtFeatures = new RequestTargetFeatures();
                RtFeatures.IsSickLeave = preCardItem.IsEstelajy;
                RtFeatures.IsMission = preCardItem.IsDuty;
                RtFeatures.IsTraffic = preCardItem.IsTraffic;
                cmbItemRequestType.Value = this.JsSerializer.Serialize(RtFeatures);
                this.cmbRequestType_tbHourly_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbIllnesses_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbIllnesses_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbIllnesses_tbDaily_RequestRegister();
        this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbIllnesses_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbIllnesses_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Illnesses_RequestRegister(cmbIllnesses_tbDaily_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbDoctors_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbDoctors_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbDoctors_tbDaily_RequestRegister();
        this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbDoctors_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbDoctors_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Doctors_RequestRegister(cmbDoctors_tbDaily_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbMissionLocation_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbMissionLocation_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbMissionLocation_tbDaily_RequestRegister();
        this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbMissionLocation_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbMissionLocation_tbDaily_RequestRegister()
    {
        this.Fill_trvMissionLocation_tbDaily_RequestRegister();
    }

    private void Fill_trvMissionLocation_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_MissionLocations_RequestRegister(trvMissionLocation_tbDaily_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbRequestType_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbRequestType_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbRequestType_tbDaily_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllDailyRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                RequestTargetFeatures RtFeatures = new RequestTargetFeatures();
                RtFeatures.IsSickLeave = preCardItem.IsEstelajy;
                RtFeatures.IsMission = preCardItem.IsDuty;
                cmbItemRequestType.Value = this.JsSerializer.Serialize(RtFeatures);
                this.cmbRequestType_tbDaily_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbRequestType_tbOverTime_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbRequestType_tbOverTime_RequestRegister.Dispose();
        this.Fill_cmbRequestType_tbOverTime_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbOverTime_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbOverTime_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllOverTimeRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                this.cmbRequestType_tbOverTime_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateRequest_RequestRegisterPage", "UpdateRequest_RequestRegisterPage_onCallBack", null, null)]
    public string[] UpdateRequest_RequestRegisterPage(string requestCaller, string requestPersonnelCountState, string SinglePersonnelID, string CollectiveConditionsLoadState, string CollectiveConditions, string StrPersonnelList, string requestTarget, string Year, string Month, string PageSize, string RequestID, string PreCardID, string RequestDate, string FromDate, string ToDate, string FromTime, string ToTime, string IsToTimeInNextDay, string Duration, string Description, string IsSeakLeave, string IllnessID, string DoctorID, string IsMission, string MissionLocationID, string Value, string RequestAttachmentFile)
    {
        string[] retMessage = new string[5];
        this.InitializeCulture();

        try
        {
            int RequestCount = 0;
            int RequestPageCount = 0;
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(requestCaller));
            RequestPersonnelCountState RPCS = (RequestPersonnelCountState)Enum.Parse(typeof(RequestPersonnelCountState), this.StringBuilder.CreateString(requestPersonnelCountState));
            decimal singlePersonnelID = decimal.Parse(this.StringBuilder.CreateString(SinglePersonnelID));
            LoadState CCLS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(CollectiveConditionsLoadState));
            CollectiveConditions = this.StringBuilder.CreateString(CollectiveConditions);
            IList<decimal> PersonnelIDsList = this.CreatePersonnelList(this.StringBuilder.CreateString(StrPersonnelList));
            RequestTarget RT = (RequestTarget)Enum.Parse(typeof(RequestTarget), this.StringBuilder.CreateString(requestTarget));
            int year = int.Parse(this.StringBuilder.CreateString(Year));
            int month = int.Parse(this.StringBuilder.CreateString(Month));
            int pageSize = int.Parse(this.StringBuilder.CreateString(PageSize));
            decimal requestID = decimal.Parse(this.StringBuilder.CreateString(RequestID));
            decimal preCardID = decimal.Parse(this.StringBuilder.CreateString(PreCardID));
            RequestDate = this.StringBuilder.CreateString(RequestDate);
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            FromTime = this.StringBuilder.CreateString(FromTime);
            ToTime = this.StringBuilder.CreateString(ToTime);
            bool isToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsToTimeInNextDay));
            Duration = this.StringBuilder.CreateString(Duration);
            Description = this.StringBuilder.CreateString(Description);
            bool isSickLeave = bool.Parse(this.StringBuilder.CreateString(IsSeakLeave));
            decimal illnessID = decimal.Parse(this.StringBuilder.CreateString(IllnessID));
            decimal doctorID = decimal.Parse(this.StringBuilder.CreateString(DoctorID));
            bool isMission = bool.Parse(this.StringBuilder.CreateString(IsMission));
            decimal missionLocationID = decimal.Parse(this.StringBuilder.CreateString(MissionLocationID));
            Value = this.StringBuilder.CreateString(Value);
            RequestAttachmentFile = this.StringBuilder.CreateString(RequestAttachmentFile);

            GTS.Clock.Model.RequestFlow.Request request = new GTS.Clock.Model.RequestFlow.Request();
            Precard preCard = new Precard();
            switch (RT)
            {
                case RequestTarget.Hourly:
                    preCard.IsHourly = true;
                    request.TheFromDate = request.TheToDate = RequestDate;
                    request.TheFromTime = FromTime;
                    request.TheToTime = ToTime;
                    request.ContinueOnTomorrow = isToTimeInNextDay;
                    request.AttachmentFile = RequestAttachmentFile;
                    if (isSickLeave)
                    {
                        request.IllnessID = illnessID;
                        request.DoctorID = doctorID;
                    }
                    if (isMission)
                        request.DutyPositionID = missionLocationID;
                    break;
                case RequestTarget.Daily:
                    preCard.IsDaily = true;
                    request.TheFromDate = FromDate;
                    request.TheToDate = ToDate;
                    request.AttachmentFile = RequestAttachmentFile;
                    if (isSickLeave)
                    {
                        request.IllnessID = illnessID;
                        request.DoctorID = doctorID;
                    }
                    if (isMission)
                        request.DutyPositionID = missionLocationID;
                    break;
                case RequestTarget.OverTime:
                    request.TheFromDate = FromDate;
                    request.TheToDate = ToDate;
                    request.TheFromTime = FromTime;
                    request.TheToTime = ToTime;
                    request.TheTimeDuration = Duration;
                    break;
                case RequestTarget.Imperative:
                    preCard.IsMonthly = true;
                    request.TheTimeDuration = Value;
                    FromDate = year.ToString() + "/" + month.ToString() + "/1";
                    switch (BLanguage.CurrentSystemLanguage)
                    {
                        case LanguagesName.Parsi:
                            ToDate = year.ToString() + "/" + month.ToString() + "/" + new PersianCalendar().GetDaysInMonth(year, month).ToString();
                            break;
                        case LanguagesName.English:
                            ToDate = year.ToString() + "/" + month.ToString() + "/" + new GregorianCalendar().GetDaysInMonth(year, month).ToString();
                            break;
                    }
                    request.TheFromDate = FromDate;
                    request.TheToDate = ToDate;
                    break;
            }
            preCard.ID = preCardID;
            request.Description = Description;
            request.Precard = preCard;

            switch (RC)
            {
                case RequestCaller.NormalUser:
                    switch (RT)
                    {
                        case RequestTarget.Hourly:
                            RequestCount = this.RequestRegisterBusiness.InsertSingleHourlyRequestByNormalUser(request, year, month);
                            break;
                        case RequestTarget.Daily:
                            RequestCount = this.RequestRegisterBusiness.InsertSingleDailyRequestByNormalUser(request, year, month);
                            break;
                        case RequestTarget.OverTime:
                            RequestCount = this.RequestRegisterBusiness.InsertSingleOverTimeRequestByNormalUser(request, year, month);
                            break;
                    }
                    break;
                case RequestCaller.Operator:
                    if (RT != RequestTarget.Imperative)
                    {
                        switch (RPCS)
                        {
                            case RequestPersonnelCountState.Single:
                                switch (RT)
                                {
                                    case RequestTarget.Hourly:
                                        RequestCount = this.RequestRegisterBusiness.InsertSingleHourlyRequestByOperator(request, year, month, singlePersonnelID);
                                        break;
                                    case RequestTarget.Daily:
                                        RequestCount = this.RequestRegisterBusiness.InsertSingleDailyRequestByOperator(request, year, month, singlePersonnelID);
                                        break;
                                    case RequestTarget.OverTime:
                                        RequestCount = this.RequestRegisterBusiness.InsertSingleOverTimeRequestByOperator(request, year, month, singlePersonnelID);
                                        break;
                                }
                                break;
                            case RequestPersonnelCountState.Collective:
                                switch (RT)
                                {
                                    case RequestTarget.Hourly:
                                        this.RequestRegisterBusiness.InsertCollectiveHourlyRequestByOperator();
                                        break;
                                    case RequestTarget.Daily:
                                        this.RequestRegisterBusiness.InsertCollectiveDailyRequestByOperator();
                                        break;
                                    case RequestTarget.OverTime:
                                        this.RequestRegisterBusiness.InsertCollectiveOverTimeRequestByOperator();
                                        break;
                                }
                                switch (CCLS)
                                {
                                    case LoadState.Normal:
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, PersonnelIDsList, year, month);
                                        break;
                                    case LoadState.Search:
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, CollectiveConditions, PersonnelIDsList, year, month);
                                        break;
                                    case LoadState.AdvancedSearch:
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, this.APSProv.CreateAdvancedPersonnelSearchProxy(CollectiveConditions), PersonnelIDsList, year, month);
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        ImperativeRequest imperativeRequest = new ImperativeRequest()
                        {
                            Precard = new Precard() { ID = preCardID },
                            Year = year,
                            Month = month
                        };
                        this.RequestRegisterBusiness.InsertImperativeRequestByOperator(request, imperativeRequest, PersonnelIDsList);
                        //switch (CCLS)
                        //{
                        //    case LoadState.Normal:
                        //        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, PersonnelIDsList, year, month);
                        //        break;
                        //    case LoadState.Search:
                        //        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, CollectiveConditions, PersonnelIDsList, year, month);
                        //        break;
                        //    case LoadState.AdvancedSearch:
                        //        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, this.APSProv.CreateAdvancedPersonnelSearchProxy(CollectiveConditions), PersonnelIDsList, year, month);
                        //        break;
                        //}
                    }
                    break;
            }
            RequestPageCount = Utility.GetPageCount(RequestCount, pageSize);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("AddComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = RequestPageCount.ToString();
            retMessage[4] = RT.ToString();
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

    private IList<decimal> CreatePersonnelList(string StrUnCollectivePersonnelList)
    {
        IList<decimal> UnCollectivePersonnelList = new List<decimal>();
        string[] StrUnCollectivePersonnelListParts = StrUnCollectivePersonnelList.Split(new char[] { '#' });
        for (int i = 0; i < StrUnCollectivePersonnelListParts.Length; i++)
        {
            if (StrUnCollectivePersonnelListParts[i] != string.Empty)
                UnCollectivePersonnelList.Add(decimal.Parse(StrUnCollectivePersonnelListParts[i]));
        }
        return UnCollectivePersonnelList;
    }

    protected void CallBack_cmbPersonnel_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbPersonnel_RequestRegister.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_RequestRegister((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_RequestRegister((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.cmbPersonnel_RequestRegister.RenderControl(e.Output);
        this.hfPersonnelCount_RequestRegister.RenderControl(e.Output);
        this.hfPersonnelPageCount_RequestRegister.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_RequestRegister(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, string.Empty, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                    break;
            }
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                PersonnelDetails personnelDetails = new PersonnelDetails();
                personnelDetails.ID = personItem.ID.ToString();
                personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                personCmbItem.Value = this.JsSeializer.Serialize(personnelDetails);
                this.cmbPersonnel_RequestRegister.Items.Add(personCmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridPersonnel_CollectiveTraffic_onCallBack(object sender, CallBackEventArgs e)
    {
        this.SetPersonnelPageCount_GridPersonnel_CollectiveTraffic((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_GridPersonnel_CollectiveTraffic((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.ErrorHiddenField_Personnel_CollectiveTraffic.RenderControl(e.Output);
        this.hfPersonnelCount_Personnel_CollectiveTraffic.RenderControl(e.Output);
        this.hfPersonnelPageCount_Personnel_CollectiveTraffic.RenderControl(e.Output);
        this.GridPersonnel_CollectiveTraffic.RenderControl(e.Output);
    }

    private void Fill_GridPersonnel_CollectiveTraffic(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, string.Empty, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                    break;
            }
            this.GridPersonnel_CollectiveTraffic.DataSource = PersonnelList;
            this.GridPersonnel_CollectiveTraffic.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbRequestType_tbImperative_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        this.Fill_cmbRequestType_tbImperative_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbImperative_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbImperative_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllImperativeRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                this.cmbRequestType_tbImperative_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridPersonnel_tbImperative_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        LoadState Ls = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0]));
        ImperativeRequestLoadState IRLS = (ImperativeRequestLoadState)Enum.Parse(typeof(ImperativeRequestLoadState), this.StringBuilder.CreateString(e.Parameters[1]));
        decimal precardID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
        int year = int.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
        int month = int.Parse(this.StringBuilder.CreateString(e.Parameters[4]));
        bool isLockedImperative = bool.Parse(this.StringBuilder.CreateString(e.Parameters[5]));
        int pageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[6]));
        int pageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[7]));
        string SearchTermConditions = this.StringBuilder.CreateString(e.Parameters[8]);

        ImperativeRequest imperativeRequest = new ImperativeRequest()
        {
            Precard = new Precard() { ID = precardID },
            Year = year,
            Month = month,
            IsLocked = isLockedImperative
        };

        this.SetImperativePageCount_GridPersonnel_tbImperative_RequestRegister(Ls, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, pageSize, SearchTermConditions);
        this.Fill_GridPersonnel_tbImperative_RequestRegister(Ls, IRLS, imperativeRequest, pageSize, pageIndex, SearchTermConditions);
        this.ErrorHiddenField_tbImperative_RequestRegister.RenderControl(e.Output);
        this.hfImperativeCount_tbImperative_RequestRegister.RenderControl(e.Output);
        this.hfImperativePageCount_tbImperative_RequestRegister.RenderControl(e.Output);
        this.GridPersonnel_tbImperative_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_GridPersonnel_tbImperative_RequestRegister(LoadState Ls, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, int PageSize, int PageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<ImperativeUndermanagementInfoProxy> ImperativeUndermanagementInfoProxyList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    ImperativeUndermanagementInfoProxyList = this.ImperativeRequestBusiness.GetQuickSearchPersonByImperativeRequest(string.Empty, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, PageIndex, PageSize);
                    break;
                case LoadState.Search:
                    ImperativeUndermanagementInfoProxyList = this.ImperativeRequestBusiness.GetQuickSearchPersonByImperativeRequest(SearchTerm, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, PageIndex, PageSize);
                    break;
                case LoadState.AdvancedSearch:
                    ImperativeUndermanagementInfoProxyList = this.ImperativeRequestBusiness.GetAdvancedSearchPersonByImperativeRequest(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, PageIndex, PageSize);
                    break;
            }
            this.GridPersonnel_tbImperative_RequestRegister.DataSource = this.CreateImperativeProxy_RequestRegister(ImperativeUndermanagementInfoProxyList);
            this.GridPersonnel_tbImperative_RequestRegister.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private IList<ImperativeProxy> CreateImperativeProxy_RequestRegister(IList<ImperativeUndermanagementInfoProxy> ImperativeUndermanagementInfoProxyList)
    {
        IList<ImperativeProxy> ImperativeProxyList = new List<ImperativeProxy>();
        foreach (ImperativeUndermanagementInfoProxy imperativeUndermanagementInfoProxyitem in ImperativeUndermanagementInfoProxyList)
        {
            ImperativeProxy imperativeProxy = new ImperativeProxy();
            imperativeProxy.PersonID = imperativeUndermanagementInfoProxyitem.PersonID.ToString();
            imperativeProxy.PersonCode = imperativeUndermanagementInfoProxyitem.PersonCode;
            imperativeProxy.PersonName = imperativeUndermanagementInfoProxyitem.PersonName;
            imperativeProxy.PersonImage = imperativeUndermanagementInfoProxyitem.PersonImage;
            imperativeProxy.ImperativeValue = imperativeUndermanagementInfoProxyitem.ImperativeValue;
            imperativeProxy.IsLockedImperative = imperativeUndermanagementInfoProxyitem.IsLockedImperative;
            string CalcInfo = string.Empty;
            foreach (PropertyInfo propertyInfo in typeof(CalcInfoProxy).GetProperties())
            {
                string propertyInfoValue = propertyInfo.GetValue(imperativeUndermanagementInfoProxyitem.CalcInfo, null).ToString().Trim();
                CalcInfo += GetLocalResourceObject(propertyInfo.Name).ToString() + ":" + (propertyInfoValue != string.Empty ? propertyInfoValue : "0") + ",";
            }
            imperativeProxy.CalcInfo = CalcInfo;
            ImperativeProxyList.Add(imperativeProxy);
        }
        return ImperativeProxyList;
    }

    private void Fill_cmbYear_tbImperative_RequestRegister()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_tbImperative_RequestRegister, this.hfCurrentYear_RequestRegister);
    }

    private void Fill_cmbMonth_tbImperative_RequestRegister()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_tbImperative_RequestRegister, this.hfCurrentMonth_RequestRegister);
    }

    protected void Callback_AttachmentUploader_tbHourly_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        this.AttachmentUploader_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    protected void Callback_AttachmentUploader_tbDaily_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        this.AttachmentUploader_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    [Ajax.AjaxMethod("ApplyImperativeRequest_RequestRegisterPage", "ApplyImperativeRequest_RequestRegisterPage_onCallBack", null, null)]
    public string[] ApplyImperativeRequest_RequestRegisterPage(string StrCollectivePersonnelList, string PrecardID, string Year, string Month, string ImperativeValue, string Description)
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();

        try
        {
            IList<decimal> PersonnelIDsList = this.CreatePersonnelList(this.StringBuilder.CreateString(StrCollectivePersonnelList));
            decimal precardID = decimal.Parse(this.StringBuilder.CreateString(PrecardID));
            int year = int.Parse(this.StringBuilder.CreateString(Year));
            int month = int.Parse(this.StringBuilder.CreateString(Month));
            int imperativeValue = int.Parse(this.StringBuilder.CreateString(ImperativeValue));
            Description = this.StringBuilder.CreateString(Description);

            ImperativeRequest imperativeRequest = new ImperativeRequest()
            {
                Precard = new Precard() { ID = precardID },
                Year = year,
                Month = month,
                Value = imperativeValue,
                Description = Description
            };

            this.ImperativeRequestBusiness.UpdateImperativeCollectiveRequest(imperativeRequest, PersonnelIDsList);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("ApplyComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = imperativeValue.ToString();

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