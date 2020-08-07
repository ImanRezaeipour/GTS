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
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class OvertimeJustificationRequest : GTSBasePage
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

        public IOverTimeBRequest RequestBusiness
        {
            get
            {
                return (IOverTimeBRequest)(BusinessHelper.GetBusinessInstance<BRequest>());
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
            OvertimeJustificationRequest_onPageLoad,
            DialogOvertimeJustificationRequest_Operations,
            DialogShiftsView_onPageLoad,
            Alert_Box,
            DropDownDive,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridRegisteredRequests_OvertimeJustificationRequest.IsCallback && !CallBack_cmbOverTimeType_OvertimeJustificationRequest.IsCallback)
            {
                Page OvertimeJustificationRequestPage = this.Page;
                Ajax.Utility.GenerateMethodScripts(OvertimeJustificationRequestPage);

                this.CheckOvertimeJustificationRequestLoadState_OvertimeJustificationRequest();
                this.ViewCurrentLangCalendars_OvertimeJustificationRequest();
                this.SetCurrentDate_OvertimeJustificationRequest();
                this.SetRequestsStatesStr_OvertimeJustificationRequest();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckOvertimeJustificationRequestLoadState_OvertimeJustificationRequest()
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
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Manager:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onManagerLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
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

        private void ViewCurrentLangCalendars_OvertimeJustificationRequest()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpFromDate_OvertimeJustificationRequest.Visible = true;
                    this.Container_pdpToDate_OvertimeJustificationRequest.Visible = true;
                    break;
                case "en-US":
                    this.gdpFromDate_OvertimeJustificationRequest.Visible = true;
                    this.gdpToDate_OvertimeJustificationRequest.Visible = true;
                    break;
            }
        }

        private void SetCurrentDate_OvertimeJustificationRequest()
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
            this.hfCurrentDate_OvertimeJustificationRequest.Value = strCurrentDate;
        }

        private void SetRequestsStatesStr_OvertimeJustificationRequest()
        {
            string strRequestsStates = string.Empty;
            foreach (RequestState requestsStateItem in Enum.GetValues(typeof(RequestState)))
            {
                strRequestsStates += "#" + GetLocalResourceObject(requestsStateItem.ToString()).ToString() + ":" + ((int)requestsStateItem).ToString();
            }
            this.hfRequestStates_OvertimeJustificationRequest.Value = strRequestsStates;
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

        protected void CallBack_GridRegisteredRequests_OvertimeJustificationRequest_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridRegisteredRequests_OvertimeJustificationRequest((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
            this.GridRegisteredRequests_OvertimeJustificationRequest.RenderControl(e.Output);
        }

        private void Fill_GridRegisteredRequests_OvertimeJustificationRequest(RequestCaller RC, string DateKey, string RequestDate)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Request> RequestsList = this.RequestBusiness.GetAllOverTimeRequests(RequestDate);
                this.GridRegisteredRequests_OvertimeJustificationRequest.DataSource = RequestsList;
                this.GridRegisteredRequests_OvertimeJustificationRequest.DataBind();
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

        protected void CallBack_cmbOverTimeType_OvertimeJustificationRequest_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbOverTimeType_OvertimeJustificationRequest.Dispose();
            this.Fill_cmbOverTimeType_OvertimeJustificationRequest();
            this.ErrorHiddenField_OverTimeTypes.RenderControl(e.Output);
            this.cmbOverTimeType_OvertimeJustificationRequest.RenderControl(e.Output);
        }

        private void Fill_cmbOverTimeType_OvertimeJustificationRequest()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> OverTimeTypesList = this.RequestBusiness.GetAllOverWorks();
                this.cmbOverTimeType_OvertimeJustificationRequest.DataSource = OverTimeTypesList;
                this.cmbOverTimeType_OvertimeJustificationRequest.DataBind();
                this.cmbOverTimeType_OvertimeJustificationRequest.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OverTimeTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OverTimeTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OverTimeTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateRequest_OvertimeJustificationRequestPage", "UpdateRequest_OvertimeJustificationRequestPage_onCallBack", null, null)]
        public string[] UpdateRequest_OvertimeJustificationRequestPage(string requestCaller, string requestLoadState, string state, string SelectedRequestID, string PreCardID, string RequestDate, string RequestFromDate, string RequestToDate, string RequestFromTime, string RequestToTime, string RequestDuration, string RequestDescription)
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
                        decimal preCardID = decimal.Parse(this.StringBuilder.CreateString(PreCardID));
                        RequestFromDate = this.StringBuilder.CreateString(RequestFromDate);
                        RequestToDate = this.StringBuilder.CreateString(RequestToDate);
                        RequestFromTime = this.StringBuilder.CreateString(RequestFromTime);
                        RequestToTime = this.StringBuilder.CreateString(RequestToTime);
                        RequestDuration = this.StringBuilder.CreateString(RequestDuration);
                        RequestDescription = this.StringBuilder.CreateString(RequestDescription);

                        Precard precard = new Precard();
                        precard.ID = preCardID;
                        request.Precard = precard;
                        request.TheFromDate = RequestFromDate;
                        request.TheToDate = RequestToDate;
                        request.IsDateSetByUser = true;
                        request.TheFromTime = RequestFromTime;
                        request.TheToTime = RequestToTime;
                        request.TheTimeDuration = RequestDuration;
                        request.Description = RequestDescription;

                        switch (RC)
                        {
                            case RequestCaller.Grid:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onManagerLoadStateInGanttChartSchema(request);
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
                                        this.RequestBusiness.DeleteOverTimeRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteOverTimeRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        this.RequestBusiness.DeleteOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteOverTimeRequest_onManagerLoadStateInGanttChartSchema(request);
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



    }
}