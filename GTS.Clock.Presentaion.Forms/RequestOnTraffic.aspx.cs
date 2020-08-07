using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Globalization;
using System.Threading;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.UI;
using GTS.Clock.Business;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class RequestOnTraffic : GTSBasePage
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

        public ITrafficBRequest RequestBusiness
        {
            get
            {
                return (ITrafficBRequest)(BusinessHelper.GetBusinessInstance<BRequest>());
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
            RequestOnTraffic_onPageLoad,
            DialogRequestOnTraffic_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridTrafficPairs_RequestOnTraffic.IsCallback && !CallBack_GridRegisteredRequests_RequestOnTraffic.IsCallback && !CallBack_cmbTrafficType_RequestOnTraffic.IsCallback)
            {
                Page RequestOnTrafficPage = this;
                Ajax.Utility.GenerateMethodScripts(RequestOnTrafficPage);

                this.CheckRequestOnTrafficLoadState_RequestOnTraffic();
                this.SetRequestsStatesStr_RequestOnTraffic();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckRequestOnTrafficLoadState_RequestOnTraffic()
        {
            string[] retMessage = new string[4];
            try
            {
                if(HttpContext.Current.Request.QueryString.AllKeys.Contains("RC") && HttpContext.Current.Request.QueryString.AllKeys.Contains("RLS"))
                {
                    RequestLoadState requestLoadState = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RLS"]));
                    RequestCaller requestCaller = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RC"]));

                    switch (requestLoadState)
                    {
                        case RequestLoadState.Personnel:
                            switch (requestCaller)
	                        {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckTrafficRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckTrafficRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                    break;
	                        }
                            break;
                        case RequestLoadState.Manager:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckTrafficRequestLoadAccess_onManagerLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckTrafficRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
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

        private void SetRequestsStatesStr_RequestOnTraffic()
        {
            string strRequestsStates = string.Empty;
            foreach (RequestState requestsStateItem in Enum.GetValues(typeof(RequestState)))
            {
                strRequestsStates += "#" + GetLocalResourceObject(requestsStateItem.ToString()).ToString() + ":" + ((int)requestsStateItem).ToString();
            }
            this.hfRequestStates_RequestOnTraffic.Value = strRequestsStates;
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

        protected void CallBack_GridTrafficPairs_RequestOnTraffic_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.Fill_GridTrafficPairs_RequestOnTraffic((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_TrafficPairs.RenderControl(e.Output);
            this.GridTrafficPairs_RequestOnTraffic.RenderControl(e.Output);
        }

        private void Fill_GridTrafficPairs_RequestOnTraffic(RequestCaller RC, string DateKey, string RequestDate)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<MonthlyDetailReportProxy> TrafficPairsList = this.RequestBusiness.GetAllTrafic(RequestDate);
                this.GridTrafficPairs_RequestOnTraffic.DataSource = TrafficPairsList;
                this.GridTrafficPairs_RequestOnTraffic.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_TrafficPairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_TrafficPairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_TrafficPairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }

        protected void CallBack_GridRegisteredRequests_RequestOnTraffic_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.Fill_GridRegisteredRequests_RequestOnTraffic((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]));
            this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
            this.GridRegisteredRequests_RequestOnTraffic.RenderControl(e.Output);
        }

        private void Fill_GridRegisteredRequests_RequestOnTraffic(RequestCaller RC, string DateKey, string RequestDate)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Request> RequestsList = this.RequestBusiness.GetAllTrafficRequests(RequestDate);
                this.GridRegisteredRequests_RequestOnTraffic.DataSource = RequestsList;
                this.GridRegisteredRequests_RequestOnTraffic.DataBind();
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

        protected void CallBack_cmbTrafficType_RequestOnTraffic_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbTrafficType_RequestOnTraffic.Dispose();
            this.Fill_cmbTrafficType_RequestOnTraffic();
            this.ErrorHiddenField_TrafficTypes.RenderControl(e.Output);
            this.cmbTrafficType_RequestOnTraffic.RenderControl(e.Output);
        }

        private void Fill_cmbTrafficType_RequestOnTraffic()
        {
            string[] retMessage = new string[4];
            try
            {
                //this.InitializeCulture();

                //for (int i = 0; i < 3; i++)
                //{
                //    string cmbItemText = string.Empty;
                //    switch (i)
                //    {
                //        case 0:
                //            cmbItemText = GetLocalResourceObject("cmbItemEntrance_cmbTrafficType_RequestOnTraffic").ToString();
                //            break;
                //        case 1:
                //            cmbItemText = GetLocalResourceObject("cmbItemExit_cmbTrafficType_RequestOnTraffic").ToString();
                //            break;
                //        case 2:
                //            cmbItemText = GetLocalResourceObject("cmbItemEntranceAndExit_cmbTrafficType_RequestOnTraffic").ToString();
                //            break;
                //    }
                //    ComboBoxItem cmbItem = new ComboBoxItem(cmbItemText);
                //    cmbItem.Id = i.ToString();
                //    this.cmbTrafficType_RequestOnTraffic.Items.Add(cmbItem);
                //}

                IList<Precard> TrafficTypesList = this.RequestBusiness.GetAllTraffics();
                this.cmbTrafficType_RequestOnTraffic.DataSource = TrafficTypesList;
                this.cmbTrafficType_RequestOnTraffic.DataBind();
                this.cmbTrafficType_RequestOnTraffic.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_TrafficTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_TrafficTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_TrafficTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


        [Ajax.AjaxMethod("UpdateRequest_RequestOnTrafficPage", "UpdateRequest_RequestOnTrafficPage_onCallBack", null, null)]
        public string[] UpdateRequest_RequestOnTrafficPage(string requestCaller, string requestLoadState, string state, string SelectedRequestID, string PreCardID, string RequestDate, string RequestFromTime, string RequestToTime, string IsRequestToTimeInNextDay, string RequestDescription)
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
                        RequestFromTime = this.StringBuilder.CreateString(RequestFromTime);
                        RequestToTime = this.StringBuilder.CreateString(RequestToTime);
                        bool isRequestToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsRequestToTimeInNextDay));
                        RequestDescription = this.StringBuilder.CreateString(RequestDescription);

                        request.TheFromDate = request.TheToDate = this.StringBuilder.CreateString(RequestDate);
                        Precard precard = new Precard();
                        precard.ID = preCardID;
                        request.Precard = precard;
                        if(RequestFromTime != string.Empty)
                           request.TheFromTime = RequestFromTime;
                        if(RequestToTime != string.Empty)
                           request.TheToTime = RequestToTime;
                        request.ContinueOnTomorrow = isRequestToTimeInNextDay;
                        request.Description = RequestDescription;
                        
                        switch (RC)
	                    {
                            case RequestCaller.Grid:
                                switch (RLS)
	                            {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertTrafficRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertTrafficRequest_onManagerLoadStateInGridSchema(request);
                                        break;
	                            }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertTrafficRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertTrafficRequest_onManagerLoadStateInGanttChartSchema(request);
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
                                        this.RequestBusiness.DeleteTrafficRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteTrafficRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        this.RequestBusiness.DeleteTrafficRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteTrafficRequest_onManagerLoadStateInGanttChartSchema(request);
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