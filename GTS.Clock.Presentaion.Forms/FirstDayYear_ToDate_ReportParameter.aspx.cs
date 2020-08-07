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

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class FirstDayYear_ToDate_ReportParameter : GTSBasePage
    {
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
            FirstDayYear_ToDate_ReportParameter_onPageLoad,
            FirstDayYear_ToDate_ReportParameter_Oparetions,
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

            this.hfCurrenttoDate_RuleParameters.Value = strCurrentDate;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page FirstDayYear_ToDate_ReportParameterFrom = this;
            Ajax.Utility.GenerateMethodScripts(FirstDayYear_ToDate_ReportParameterFrom);

            this.ViewCurrentLangCalendars_FirstDayYear_ToDate_ReportParameter();
            this.SetCurrentDate_RuleParameters();
            this.SetReportParameterID_FirstDayYear_ToDate_ReportParameter();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        private void ViewCurrentLangCalendars_FirstDayYear_ToDate_ReportParameter()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdptoDate_RuleParameters.Visible = true;
                    break;
                case "en-US":
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
        private void SetReportParameterID_FirstDayYear_ToDate_ReportParameter()
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

        public string GetParameterValue(decimal fileId, decimal uiParamerId, string actionId, string to)
        {
            try
            {
                DateTime fromDate = new DateTime();
                DateTime toDate = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    toDate = Utility.ToMildiDate(to);
                    PersianCalendar shamsi = new PersianCalendar();
                    int year = shamsi.GetYear(toDate);
                    string date = string.Format("{0}/01/01", year);
                    fromDate = Utility.ToMildiDate(date);

                }
                else
                {
                    toDate = DateTime.Parse(to);
                    DateTime date = toDate;
                    int year = date.Year;
                    fromDate = Convert.ToDateTime(string.Format("{0}/01/01", year));

                }
                string result = String.Format("@fromDate={0};@toDate={1};", Utility.ToString(fromDate), Utility.ToString(toDate));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Ajax.AjaxMethod("Register_FirstDayYear_ToDate_ReportParameterPage", "Register_FirstDayYear_ToDate_ReportParameterPage_onCallBack", null, null)]
        public string[] Register_FirstDayYear_ToDate_ReportParameterPage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string to)
        {
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {

                string RetValue = string.Empty;
                decimal reportParameterID = decimal.Parse(this.StringBuilder.CreateString(ReportParameterID));
                ReportParameterActionID = this.StringBuilder.CreateString(ReportParameterActionID);
                decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID));
                string toDate = this.StringBuilder.CreateString(to);


                RetValue = this.GetParameterValue(reportFileID, reportParameterID, ReportParameterActionID, toDate);

                retMessage[0] = HttpContext.GetLocalResourceObject("~/FirstDayYear_ToDate_ReportParameter.aspx", "RetSuccessType").ToString();
                retMessage[1] = HttpContext.GetLocalResourceObject("~/FirstDayYear_ToDate_ReportParameter.aspx", "EditComplete").ToString();
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
    }
}