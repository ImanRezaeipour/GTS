using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using ComponentArt.Web.UI;
using System.Threading;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Station_Clock_Date_ReportParameter : GTSBasePage
    {
        public BClock MachineBusiness
        {
            get
            {
                return new BClock();
            }
        }
        public BControlStation ControlStationBusiness
        {
            get
            {
                return new BControlStation();
            }
        }
        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
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
            Station_Clock_Date_ReportParameter_onPageLoad,
            Station_Clock_Date_ReportParameter_Operations,
            Alert_Box,
            DialogWaiting_Operations
        }

        private void SetCurrentDate_RuleParameters()
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
            this.hfCurrentfromDate_RuleParameters.Value = strCurrentDate;
            this.hfCurrenttoDate_RuleParameters.Value = strCurrentDate;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page Station_Clock_Date_ReportParameterForm = this;
            Ajax.Utility.GenerateMethodScripts(Station_Clock_Date_ReportParameterForm);

            this.ViewCurrentLangCalendars_Station_Clock_Date_ReportParameter();
            this.SetCurrentDate_RuleParameters();
            this.SetReportParameterID_Station_Clock_Date_ReportParameter();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        private void ViewCurrentLangCalendars_Station_Clock_Date_ReportParameter()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpfromDate_RuleParameters.Visible = true;
                    this.Container_pdptoDate_RuleParameters.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpfromDate_RuleParameters.Visible = true;
                    this.Container_gdptoDate_RuleParameters.Visible = true;
                    break;
            }
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
        private void SetReportParameterID_Station_Clock_Date_ReportParameter()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportParametersID"))
                this.ReportParameterID.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportParametersID"]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="uiParamerId"></param>
        /// <param name="actionId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>

        public string GetParameterValue(decimal fileId, decimal uiParamerId, string actionId, string from, string to, decimal station, decimal clock)
        {
            try
            {
                DateTime fromDate = new DateTime();
                DateTime toDate = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {

                    fromDate = Utility.ToMildiDate(from);
                    toDate = Utility.ToMildiDate(to);
                }
                else
                {
                    fromDate = DateTime.Parse(from);
                    toDate = DateTime.Parse(to);
                }
                string result = String.Format("@fromDate={0};@toDate={1};@stationID={2};@clockID={3};", Utility.ToString(fromDate), Utility.ToString(toDate), station.ToString(), clock.ToString());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Ajax.AjaxMethod("Register_Station_Clock_Date_ReportParameterPage", "Register_Station_Clock_Date_ReportParameterPage_onCallBack", null, null)]
        public string[] Register_Station_Clock_Date_ReportParameterPage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string from, string to, string station, string clock)
        {
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {

                string RetValue = string.Empty;
                decimal reportParameterID = decimal.Parse(this.StringBuilder.CreateString(ReportParameterID));
                ReportParameterActionID = this.StringBuilder.CreateString(ReportParameterActionID);
                decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID));
                string fromDate = this.StringBuilder.CreateString(from);
                string toDate = this.StringBuilder.CreateString(to);
                decimal Station = decimal.Parse(this.StringBuilder.CreateString(station));
                decimal Clock = decimal.Parse(this.StringBuilder.CreateString(clock));


                RetValue = this.GetParameterValue(reportFileID, reportParameterID, ReportParameterActionID, fromDate, toDate, Station, Clock);

                retMessage[0] = HttpContext.GetLocalResourceObject("~/Station_Clock_Date_ReportParameter.aspx", "RetSuccessType").ToString();
                retMessage[1] = HttpContext.GetLocalResourceObject("~/Station_Clock_Date_ReportParameter.aspx", "EditComplete").ToString();
                retMessage[2] = "success";
                retMessage[3] = RetValue;

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
        //************************
        protected void CallBack_cmbStation_Station_Clock_Date_ReportParameter_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_cmbStation_Station_Clock_Date_ReportParameter();
            this.ErrorHiddenField_Station_Station_Clock_Date_ReportParameter.RenderControl(e.Output);
            this.cmbStation_Station_Clock_Date_ReportParameter.RenderControl(e.Output);
        }

        private void Fill_cmbStation_Station_Clock_Date_ReportParameter()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {


                IList<ControlStation> ControlStationsList = this.ControlStationBusiness.GetAll();
                cmbStation_Station_Clock_Date_ReportParameter.DataSource = ControlStationsList;
                cmbStation_Station_Clock_Date_ReportParameter.DataBind();

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Station_Station_Clock_Date_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Station_Station_Clock_Date_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Station_Station_Clock_Date_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        //*********************************
        protected void CallBack_cmbClock_Station_Clock_Date_ReportParameter_onCallBack(object sender, CallBackEventArgs e)
        {

            this.Fill_cmbClock_Station_Clock_Date_ReportParameter(e.Parameters[0].ToString());
            this.ErrorHiddenField_Clock_Station_Clock_Date_ReportParameter.RenderControl(e.Output);
            this.cmbClock_Station_Clock_Date_ReportParameter.RenderControl(e.Output);
        }

        private void Fill_cmbClock_Station_Clock_Date_ReportParameter(string StationID)
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {

                IList<ControlStation> StationList = this.ControlStationBusiness.GetAll();
                foreach (ControlStation station in StationList)
                {
                    if (Convert.ToDecimal(StationID) == station.ID)
                    {
                        IList<GTS.Clock.Model.BaseInformation.Clock> ClockList = station.ClockList;
                        cmbClock_Station_Clock_Date_ReportParameter.DataSource = ClockList;
                        cmbClock_Station_Clock_Date_ReportParameter.DataBind();
                    }
                }


            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Clock_Station_Clock_Date_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Clock_Station_Clock_Date_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Clock_Station_Clock_Date_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
    }
}