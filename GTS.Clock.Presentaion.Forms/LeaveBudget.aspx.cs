using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using Ajax;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Leave;
using GTS.Clock.Model.Concepts;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class LeaveBudget : GTSBasePage
    {
        public BLeaveBudget LeaveBudgetBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BLeaveBudget>();
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

        internal class LeaveBudgetObj
        {
            public string LeaveBudgetType { get; set; }
            public string YD { get; set; }
            public string YH { get; set; }
            public string MD1 { get; set; }
            public string MH1 { get; set; }
            public string MD2 { get; set; }
            public string MH2 { get; set; }
            public string MD3 { get; set; }
            public string MH3 { get; set; }
            public string MD4 { get; set; }
            public string MH4 { get; set; }
            public string MD5 { get; set; }
            public string MH5 { get; set; }
            public string MD6 { get; set; }
            public string MH6 { get; set; }
            public string MD7 { get; set; }
            public string MH7 { get; set; }
            public string MD8 { get; set; }
            public string MH8 { get; set; }
            public string MD9 { get; set; }
            public string MH9 { get; set; } 
            public string MD10 { get; set; }
            public string MH10 { get; set; }
            public string MD11 { get; set; }
            public string MH11 { get; set; }
            public string MD12 { get; set; }
            public string MH12 { get; set; }
            public string Description { get; set; }
        }

        enum Scripts
        {
            LeaveBudget_onPageLoad,
            DialogLeaveBudget_Operations,
            Alert_Box,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page LeaveBudgetPage = this;
            Ajax.Utility.GenerateMethodScripts(LeaveBudgetPage);
            this.GetAxises_LeaveBudget();
            this.GetLeaveBudget_LeaveBudget(this.Fill_cmbYear_LeaveBudget());
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

        [Ajax.AjaxMethod("GetLeaveBudget_LeaveBudgetPage", "GetLeaveBudget_LeaveBudgetPage_onCallBack", null, null)]
        public string[] GetLeaveBudget_LeaveBudgetPage(string Year, string RuleGroupID)
        {
            string[] RetMessage = new string[4];
            try
            {
                string StrLeaveBudgetObj = this.GetLeaveBudget_LeaveBudget(int.Parse(this.StringBuilder.CreateString(Year)), decimal.Parse(this.StringBuilder.CreateString(RuleGroupID)));
                RetMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                RetMessage[1] = "Success";
                RetMessage[2] = "success";
                RetMessage[3] = StrLeaveBudgetObj;
                return RetMessage;
            }
            catch (UIValidationExceptions ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, RetMessage);
                return RetMessage;
            }
            catch (UIBaseException ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, RetMessage);
                return RetMessage;
            }
            catch (Exception ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, RetMessage);
                return RetMessage;
            }
        }

        private void GetLeaveBudget_LeaveBudget(int Year)
        {
            string[]RetMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RuleGroupID"))
                {
                    decimal RuleGroupID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RuleGroupID"]));
                    this.hfLeaveBudget_LeaveBudget.Value = this.GetLeaveBudget_LeaveBudget(Year, RuleGroupID);
                }
            }
            catch (UIValidationExceptions ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, RetMessage);
                this.ErrorHiddenField_LeaveBudget.Value = this.exceptionHandler.CreateErrorMessage(RetMessage);
            }
            catch (UIBaseException ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, RetMessage);
                this.ErrorHiddenField_LeaveBudget.Value = this.exceptionHandler.CreateErrorMessage(RetMessage);
            }
            catch (Exception ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, RetMessage);
                this.ErrorHiddenField_LeaveBudget.Value = this.exceptionHandler.CreateErrorMessage(RetMessage);
            }
        }

        private string GetLeaveBudget_LeaveBudget(int Year, decimal RuleGroupID)
        {
            LeaveBudgetObj ObjLeaveBudget = new LeaveBudgetObj();
            LeaveBudgetProxy leaveBudgetProxy = this.LeaveBudgetBusiness.GetRuleBudget(RuleGroupID, Year);
            ObjLeaveBudget.LeaveBudgetType = leaveBudgetProxy.BudgetType.ToString();
            ObjLeaveBudget.Description = leaveBudgetProxy.Description;
            switch (leaveBudgetProxy.BudgetType)
            {
                case GTS.Clock.Infrastructure.BudgetType.Usual:
                    ObjLeaveBudget.YD = leaveBudgetProxy.TotoalDay;
                    ObjLeaveBudget.YH = leaveBudgetProxy.TotoalTime;
                    break;
                case GTS.Clock.Infrastructure.BudgetType.PerMonth:
                    ObjLeaveBudget.MD1 = leaveBudgetProxy.Day1;
                    ObjLeaveBudget.MH1 = leaveBudgetProxy.Time1;
                    ObjLeaveBudget.MD2 = leaveBudgetProxy.Day2;
                    ObjLeaveBudget.MH2 = leaveBudgetProxy.Time2;
                    ObjLeaveBudget.MD3 = leaveBudgetProxy.Day3;
                    ObjLeaveBudget.MH3 = leaveBudgetProxy.Time3;
                    ObjLeaveBudget.MD4 = leaveBudgetProxy.Day4;
                    ObjLeaveBudget.MH4 = leaveBudgetProxy.Time4;
                    ObjLeaveBudget.MD5 = leaveBudgetProxy.Day5;
                    ObjLeaveBudget.MH5 = leaveBudgetProxy.Time5;
                    ObjLeaveBudget.MD6 = leaveBudgetProxy.Day6;
                    ObjLeaveBudget.MH6 = leaveBudgetProxy.Time6;
                    ObjLeaveBudget.MD7 = leaveBudgetProxy.Day7;
                    ObjLeaveBudget.MH7 = leaveBudgetProxy.Time7;
                    ObjLeaveBudget.MD8 = leaveBudgetProxy.Day8;
                    ObjLeaveBudget.MH8 = leaveBudgetProxy.Time8;
                    ObjLeaveBudget.MD9 = leaveBudgetProxy.Day9;
                    ObjLeaveBudget.MH9 = leaveBudgetProxy.Time9;
                    ObjLeaveBudget.MD10 = leaveBudgetProxy.Day10;
                    ObjLeaveBudget.MH10 = leaveBudgetProxy.Time10;
                    ObjLeaveBudget.MD11 = leaveBudgetProxy.Day11;
                    ObjLeaveBudget.MH11 = leaveBudgetProxy.Time11;
                    ObjLeaveBudget.MD12 = leaveBudgetProxy.Day12;
                    ObjLeaveBudget.MH12 = leaveBudgetProxy.Time12;
                    break;
            }
            string StrLeaveBudgetObj = this.JsSerializer.Serialize(ObjLeaveBudget);
            return StrLeaveBudgetObj;
        }

        private int Fill_cmbYear_LeaveBudget()
        {
            int CurrentYear = 0;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "en-US":
                    CurrentYear = DateTime.Now.Year;
                    break;
                case "fa-IR":
                    PersianCalendar pCal = new PersianCalendar();
                    CurrentYear = pCal.GetYear(DateTime.Now);
                    break;
            }
            for (int i = CurrentYear - 10; i <= (CurrentYear + 1); i++)
            {
                ComboBoxItem cmbItemYear = new ComboBoxItem(i.ToString());
                cmbItemYear.Value = i.ToString();
                this.cmbYear_LeaveBudget.Items.Add(cmbItemYear);
            }
            this.hfCurrentYear_LeaveBudget.Value = CurrentYear.ToString();
            this.cmbYear_LeaveBudget.SelectedIndex = this.cmbYear_LeaveBudget.Items.Count - 2;
            return CurrentYear;
        }

        public void GetAxises_LeaveBudget()
        {
            string[] retMessage = new string[3];
            try
            {
                string CurrentLangID = string.Empty;
                string SysLangID = string.Empty;
                string Identifier = string.Empty;
                string retAxises = string.Empty;
                this.InitializeCulture();
                CurrentLangID = this.LangProv.GetCurrentLanguage();
                SysLangID = this.LangProv.GetCurrentSysLanguage();
                switch (SysLangID)
                {
                    case "en-US":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "ee";
                                break;
                            case "fa-IR":
                                Identifier = "ef";
                                break;
                        }
                        break;
                    case "fa-IR":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "fe";
                                break;
                            case "fa-IR":
                                Identifier = "ff";
                                break;
                        }
                        break;
                }

                string Splitter = "@";
                for (int i = 1; i <= 12; i++)
                {
                    if (i == 12)
                        Splitter = string.Empty;
                    retAxises += GetLocalResourceObject("lblMonth" + i + "" + Identifier + "").ToString() + Splitter;
                }
                this.hfBudgetAxises_LeaveBudget.Value = retAxises;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_BudgetAxises_LeaveBudget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_BudgetAxises_LeaveBudget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_BudgetAxises_LeaveBudget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateLeaveBudget_LeaveBudgetPage", "UpdateLeaveBudget_LeaveBudgetPage_onCallBack", null, null)]
        public string[] UpdateLeaveBudget_LeaveBudgetPage(string RuleGroupID, string Year, string StrObjLeaveBudget)
        {
            this.InitializeCulture();
            string[] RetMessage = new string[4];
            try
            {
                decimal ruleGroupID = decimal.Parse(this.StringBuilder.CreateString(RuleGroupID));
                int year = int.Parse(this.StringBuilder.CreateString(Year));
                StrObjLeaveBudget = this.StringBuilder.CreateString(StrObjLeaveBudget);
                Dictionary<string, object> ParamDic = (Dictionary<string, object>)JsSerializer.DeserializeObject(StrObjLeaveBudget);
                BudgetType budgetType = (BudgetType)Enum.Parse(typeof(BudgetType), ParamDic["LeaveBudgetType"].ToString());
                LeaveBudgetProxy leaveBudgetProxy = new LeaveBudgetProxy();
                leaveBudgetProxy.BudgetType = budgetType;
                leaveBudgetProxy.Description = ParamDic["Description"].ToString();
                switch (budgetType)
                {
                    case BudgetType.Usual:
                        leaveBudgetProxy.TotoalDay = ParamDic["YD"].ToString();
                        leaveBudgetProxy.TotoalTime = ParamDic["YH"].ToString();
                        break;
                    case BudgetType.PerMonth:
                        leaveBudgetProxy.Day1 = ParamDic["MD1"].ToString();
                        leaveBudgetProxy.Time1 = ParamDic["MH1"].ToString();
                        leaveBudgetProxy.Day2 = ParamDic["MD2"].ToString();
                        leaveBudgetProxy.Time2 = ParamDic["MH2"].ToString();
                        leaveBudgetProxy.Day3 = ParamDic["MD3"].ToString();
                        leaveBudgetProxy.Time3 = ParamDic["MH3"].ToString();
                        leaveBudgetProxy.Day4 = ParamDic["MD4"].ToString();
                        leaveBudgetProxy.Time4 = ParamDic["MH4"].ToString();
                        leaveBudgetProxy.Day5 = ParamDic["MD5"].ToString();
                        leaveBudgetProxy.Time5 = ParamDic["MH5"].ToString();
                        leaveBudgetProxy.Day6 = ParamDic["MD6"].ToString();
                        leaveBudgetProxy.Time6 = ParamDic["MH6"].ToString();
                        leaveBudgetProxy.Day7 = ParamDic["MD7"].ToString();
                        leaveBudgetProxy.Time7 = ParamDic["MH7"].ToString();
                        leaveBudgetProxy.Day8 = ParamDic["MD8"].ToString();
                        leaveBudgetProxy.Time8 = ParamDic["MH8"].ToString();
                        leaveBudgetProxy.Day9 = ParamDic["MD9"].ToString();
                        leaveBudgetProxy.Time9 = ParamDic["MH9"].ToString();
                        leaveBudgetProxy.Day10 = ParamDic["MD10"].ToString();
                        leaveBudgetProxy.Time10 = ParamDic["MH10"].ToString();
                        leaveBudgetProxy.Day11 = ParamDic["MD11"].ToString();
                        leaveBudgetProxy.Time11 = ParamDic["MH11"].ToString();
                        leaveBudgetProxy.Day12 = ParamDic["MD12"].ToString();
                        leaveBudgetProxy.Time12 = ParamDic["MH12"].ToString();
                        break;
                }
                this.LeaveBudgetBusiness.SaveBudget(ruleGroupID, year, leaveBudgetProxy);
                RetMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                RetMessage[1] = GetLocalResourceObject("LeaveBudgetAssignmentOperationCompleted").ToString();
                RetMessage[2] = "success";
                RetMessage[3] = budgetType.ToString();
                return RetMessage;
            }
            catch (UIValidationExceptions ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, RetMessage);
                return RetMessage;
            }
            catch (UIBaseException ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, RetMessage);
                return RetMessage;
            }
            catch (Exception ex)
            {
                RetMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, RetMessage);
                return RetMessage;
            }
        }

    }
}