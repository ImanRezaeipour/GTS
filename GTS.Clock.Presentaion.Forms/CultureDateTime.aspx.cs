using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using System.Threading;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Reporting;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class CultureDateTime : GTSBasePage
    {
        public BControlParameter_DateTime CultureDateTimeBusiness
        {
            get
            {
                return new BControlParameter_DateTime();
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

        public OperationYearMonthProvider operationYearMonthProvider
        {
            get
            {
                return new OperationYearMonthProvider();
            }
        }

        enum Scripts
        {
            CultureDateTime_onPageLoad,
            CultureDateTime_Operations,
            Alert_Box,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page CultureDateTimeForm = this;
            Ajax.Utility.GenerateMethodScripts(CultureDateTimeForm);
            this.Fill_cmbYear_CultureDateTime();
            this.Fill_cmbMonth_CultureDateTime();
            this.SetReportParameterID_CultureDateTime();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
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

        private void SetReportParameterID_CultureDateTime()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportParametersID"))
                this.ReportParameterID.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportParametersID"]);
        }

        private int Fill_cmbYear_CultureDateTime()
        {
            return this.operationYearMonthProvider.GetOperationYear(this.cmbYear_CultureDateTime, this.hfCurrentYear_CultureDateTime);
        }

        private int Fill_cmbMonth_CultureDateTime()
        {
            return this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_CultureDateTime, this.hfCurrentMonth_CultureDateTime);
        }

        [Ajax.AjaxMethod("Register_CultureDateTimePage", "Register_CultureDateTimePage_onCallBack", null, null)]
        public string[] Register_CultureDateTimePage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string Year, string Month, string Hour, string Minute)
        {
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                string RetValue = string.Empty;
                decimal reportParameterID = decimal.Parse(this.StringBuilder.CreateString(ReportParameterID));
                ReportParameterActionID = this.StringBuilder.CreateString(ReportParameterActionID);
                decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID));
                int year = int.Parse(this.StringBuilder.CreateString(Year));
                int month = int.Parse(this.StringBuilder.CreateString(Month));
                int hour = int.Parse(this.StringBuilder.CreateString(Hour));
                int minute = int.Parse(this.StringBuilder.CreateString(Minute));


                RetValue = this.CultureDateTimeBusiness.GetParameterValue(reportFileID, reportParameterID, ReportParameterActionID, year, month, hour, minute);

                this.operationYearMonthProvider.SetOperationYearMonth(year, month);

                retMessage[0] = HttpContext.GetLocalResourceObject("~/CultureDateTime.aspx", "RetSuccessType").ToString();
                retMessage[1] = HttpContext.GetLocalResourceObject("~/CultureDateTime.aspx", "EditComplete").ToString();
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