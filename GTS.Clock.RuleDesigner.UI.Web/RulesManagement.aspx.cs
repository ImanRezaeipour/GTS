using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Rules;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.Model;
using GTS.Clock.RuleDesigner.UI.Web.Classes;
using ExceptionHandler = GTS.Clock.RuleDesigner.UI.Web.Classes.ExceptionHandler;
using StringGenerator = GTS.Clock.RuleDesigner.UI.Web.Classes.StringGenerator;

namespace GTS.Clock.RuleDesigner.UI.Web
{
    public partial class RulesManagement : GTSBasePage
    {
        #region Initialize

        public StringGenerator StringBuilder;
        public ExceptionHandler ExceptionHandler;
        public BRuleTemp RuleBusiness = null;
        public BRuleParameterTemp RuleParameterTemp = null;
        public BLanguage LangProv;
        public BRuleType BRuleType;

        public RulesManagement()
        {
            RuleBusiness = new BRuleTemp();
            RuleParameterTemp = new BRuleParameterTemp();
            StringBuilder = new StringGenerator();
            LangProv = new BLanguage();
            ExceptionHandler = new ExceptionHandler();
            BRuleType = new BRuleType();
        }

        #endregion

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        private enum Scripts
        {
            //RulesManagement_onPageLoad,
            RulesManagement_onPageLoad,
            DialogRulesManagement_Operations,
            Alert_Box,
        }

        public enum LoadState
        {
            Normal,
            Search,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CallBack_GridRules_Rule.IsCallback)
            {
                Ajax.Utility.GenerateMethodScripts(this);
                this.SetRulesPageSize_Rules();
                this.SetRulesPageCount_Rules(LoadState.Normal, string.Empty);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                SkinHelper.InitializeSkin(this.Page);
                SetJsonEnumIntoHiddenField();
                CheckRuleManagementLoadAccess_RuleManagement();
            }
        }

        private void SetRulesPageSize_Rules()
        {
            this.hfRulesPageSize_Rules.Value =
                this.GridRules_Rules.PageSize.ToString(CultureInfo.InvariantCulture);
        }

        private void CheckRuleManagementLoadAccess_RuleManagement()
        {
            string[] retMessage = new string[4];
            try
            {
                // this.RuleBusiness.CheckRuleManagementLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        private void SetJsonEnumIntoHiddenField()
        {
            if (string.IsNullOrEmpty(hfJsonRuleParameterTypeEnum.Value))
                hfJsonRuleParameterTypeEnum.Value = ConvertEnumToJavascript(typeof(RuleParamType));
        }

        private void SetRulesPageCount_Rules(LoadState Ls, string SearchTerm)
        {
            var RulesCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    RulesCount = this.RuleBusiness.GetAllByPageBySearchCount(string.Empty);
                    break;
                case LoadState.Search:
                    RulesCount = this.RuleBusiness.GetAllByPageBySearchCount(SearchTerm);
                    break;
            }
            this.hfRulesCount_Rules.Value = RulesCount.ToString();
            this.hfRulesPageCount_Rules.Value =
                Utility.GetPageCount(RulesCount, this.GridRules_Rules.PageSize).ToString();
        }

        protected void CallBack_GridRules_Rule_OnCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.SetRulesPageCount_Rules(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.Fill_GridRules_Rules(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[1])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[2])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.GridRules_Rules.RenderControl(e.Output);
            this.hfRulesCount_Rules.RenderControl(e.Output);
            this.hfRulesPageCount_Rules.RenderControl(e.Output);
            this.ErrorHiddenField_Rules.RenderControl(e.Output);
        }

        private void Fill_GridRules_Rules(LoadState Ls, int pageSize, int pageIndex, string searchTerm)
        {
            var retMessage = new string[4];
            IList<RuleTemplateProxy> RulesProxyList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        RulesProxyList = RuleBusiness.GetAllByPageBySearch(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        RulesProxyList = RuleBusiness.GetAllByPageBySearch(pageIndex, pageSize, searchTerm);
                        break;
                }

                foreach (var ruleTemplateProxy in RulesProxyList)
                    ruleTemplateProxy.Type = GetLocalResourceObject(ruleTemplateProxy.Type).ToString();

                this.GridRules_Rules.DataSource = RulesProxyList;
                this.GridRules_Rules.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Rules.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Rules.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Rules.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Rules.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        /// <summary>
        /// This method is used to generate javascript equivalents of C# enumerations.
        /// This makes it possible to use the C# enum without having to code and maintain
        /// an equivalent javascript class
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string ConvertEnumToJavascript(Type t)
        {
            if (!t.IsEnum) throw new Exception("Type must be an enumeration");

            var values = System.Enum.GetValues(t);
            var dict = new Dictionary<int, string>();

            foreach (object obj in values)
            {
                try
                {
                    var resorce = GetLocalResourceObject(obj.ToString()).ToString();
                    dict.Add(Convert.ToInt32(System.Enum.Format(t, obj, "D")), resorce);
                }
                catch (Exception)
                {
                    string name = System.Enum.GetName(t, obj);
                    dict.Add(Convert.ToInt32(System.Enum.Format(t, obj, "D")), name);
                }
            }

            //var serializer = new JavaScriptSerializer();
            //return serializer.Serialize(dict);

            var sss = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

            return sss;
        }

        [Ajax.AjaxMethod("UpdateRule_RulesPage", "UpdateRule_RulesPage_onCallBack", null, null)]
        public string[] UpdateRule_RulesPage(
                  string ID,
                  string IdentifierCode,
                  string Name,
                  string CustomCategoryCode,
                  string TypeId,
                  string UserDefined,
                  string Script,
                  string CSharpCode,
                  string JsonObject,
                  string PageState)
        {

            this.InitializeCulture();

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();
            string[] retMessage = new string[4];

            decimal iID = 0;
            RuleTemplate RuleRecived = new RuleTemplate();
            RuleRecived.ID = Convert.ToDecimal(StringBuilder.CreateString(ID));

            PageState = StringBuilder.CreateString(PageState);
            if (PageState != "Delete")
            {
                RuleRecived.IdentifierCode = Convert.ToDecimal(StringBuilder.CreateString(IdentifierCode));
                RuleRecived.Name = StringBuilder.CreateString(Name);
                RuleRecived.CustomCategoryCode = StringBuilder.CreateString(CustomCategoryCode);
                RuleRecived.TypeId = Convert.ToDecimal(StringBuilder.CreateString(TypeId));
                RuleRecived.UserDefined = bool.Parse(StringBuilder.CreateString(UserDefined));
                RuleRecived.Script = StringBuilder.CreateString(Script);
                RuleRecived.CSharpCode = StringBuilder.CreateString(CSharpCode);
                if(!string.IsNullOrEmpty(JsonObject))
                   RuleRecived.JsonObject = JsonObject.Substring(1, JsonObject.Length - 2);

            }

            #region Effect on DB

            try
            {
                #region Set UIActionType Enum
                UIActionType uiActionType = UIActionType.ADD;
                switch (PageState.ToUpper())
                {
                    #region Add
                    case "ADD":
                        uiActionType = UIActionType.ADD;
                        iID = RuleBusiness.InsertRule(RuleRecived);
                        break;
                    #endregion
                    #region Edit
                    case "EDIT":
                        uiActionType = UIActionType.EDIT;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }

                        var concept = RuleBusiness.GetByID(RuleRecived.ID);
                        RuleBusiness.Copy(RuleRecived, ref concept);

                        iID = RuleBusiness.UpdateRule(concept);
                        break;
                    #endregion
                    #region Delete
                    case "DELETE":
                        uiActionType = UIActionType.DELETE;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        iID = RuleBusiness.DeleteRule(RuleRecived);
                        break;
                    #endregion
                    default:
                        break;
                }
                #endregion

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uiActionType)
                {
                    case UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = iID.ToString();
                return retMessage;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                return retMessage;
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                return retMessage;
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                return retMessage;
            }
            #endregion

        }

        protected void CallBack_cmbRuleType_Rules_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbRuleType_Rules.Dispose();
            Fill_cmbRuleType_Rules();
            cmbRuleType_Rules.Enabled = true;
            ErrorHiddenField_TypeFields.RenderControl(e.Output);
            cmbRuleType_Rules.RenderControl(e.Output);
        }


        private void Fill_cmbRuleType_Rules()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var bRuleType in BRuleType.GetAll())
                {
                    var newComboBoxConcept = new ComboBoxItem((GetLocalResourceObject(bRuleType.Name)).ToString())
                        {
                            Value = (bRuleType.ID).ToString()
                        };
                    cmbRuleType_Rules.Items.Add(newComboBoxConcept);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridRuleParameters_Rule_OnCallBack(object sender, CallBackEventArgs e)
        {
            decimal selectedRuleId = Convert.ToDecimal(StringBuilder.CreateString(e.Parameters[0]));

            Fill_GridRuleParameters_Rules(selectedRuleId);

            this.GridRuleParameters_Rules.RenderControl(e.Output);
            this.ErrorHiddenFieldRuleParameter_Rules.RenderControl(e.Output);
        }

        private void Fill_GridRuleParameters_Rules(decimal selectedRuleId)
        {
            var rulTempParams = RuleParameterTemp.GeRuleTempParams(selectedRuleId);

            this.GridRuleParameters_Rules.DataSource = rulTempParams;
            this.GridRuleParameters_Rules.DataBind();


        }

        protected void CallBack_cmbRuleParameterType_Rules_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbRuleParameterType_Rules.Dispose();
            Fill_cmbRuleParameterType_Rules();
            cmbRuleParameterType_Rules.Enabled = true;
            ErrorHiddenField_RuleParameterTypeFields.RenderControl(e.Output);
            cmbRuleParameterType_Rules.RenderControl(e.Output);

        }

        private void Fill_cmbRuleParameterType_Rules()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var enumValue in Enum.GetValues(typeof(RuleParamType)))
                {
                    var comboBoxItem = new ComboBoxItem(GetLocalResourceObject(enumValue.ToString()).ToString());
                    comboBoxItem.Value = ((int)enumValue).ToString();
                    cmbRuleParameterType_Rules.Items.Add(comboBoxItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateRuleParameter_RulesPage", "UpdateRuleParameter_RulesPage_onCallBack", null, null)]
        public string[] UpdateRuleParameter_RulesPage(
                  string ID,
                  string RuleID,
                  string Title,
                  string Name,
                  string TypeId,
                  string PageState)
        {

            this.InitializeCulture();
            string[] retMessage = new string[4];

            try
            {

                decimal RuleParameterId = 0;

                decimal SelectedRuleParameterId = decimal.Parse(this.StringBuilder.CreateString(ID), CultureInfo.InvariantCulture);
                decimal SelectedRuleId = decimal.Parse(this.StringBuilder.CreateString(RuleID), CultureInfo.InvariantCulture);
                decimal SelectedRuleParameterTypeId = decimal.Parse(this.StringBuilder.CreateString(TypeId), CultureInfo.InvariantCulture);

                Title = this.StringBuilder.CreateString(Title);
                Name = this.StringBuilder.CreateString(Name);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(PageState).ToUpper());

                //RuleTemplate rt = new BRuleTemp().GetByID(SelectedRuleId);                
                RuleTemplateParameter RuleParameter = new RuleTemplateParameter();
                RuleParameter.ID = SelectedRuleParameterId;
                RuleParameter.RuleTemplateId = SelectedRuleId;
                RuleParameter.Type = (RuleParamType)SelectedRuleParameterTypeId;
                //rt.RuleParameterList.Add(RuleParameter);
                //new BRuleTemp().SaveChanges(rt, UIActionType.EDIT);

                if (PageState != "Delete")
                {
                    RuleParameter.Title = Title;
                    RuleParameter.Name = Name;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        RuleParameterId = this.RuleParameterTemp.InsertRuleTempParam(RuleParameter);
                        break;
                    case UIActionType.EDIT:
                        if (SelectedRuleParameterId == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoShiftPairSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        RuleParameterId = this.RuleParameterTemp.UpdateRuleTempParam(RuleParameter);
                        break;
                    case UIActionType.DELETE:
                        if (SelectedRuleParameterId == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoShiftPairSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        RuleParameterId = this.RuleParameterTemp.DeleteRuleTempParam(RuleParameter);
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
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = RuleParameterId.ToString(CultureInfo.InvariantCulture);
                return retMessage;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                return retMessage;
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                return retMessage;
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                return retMessage;
            }


        }

        protected void CallBackSaveRules_Rules_OnCallBack(object sender, CallBackEventArgs e)
        {
            //UpdateRule_RulesPage(
            //    e.Parameters[0],
            //    e.Parameters[1],
            //    e.Parameters[2],
            //    e.Parameters[3],
            //    e.Parameters[4],
            //    e.Parameters[5],
            //    e.Parameters[6],
            //    e.Parameters[7],
            //    e.Parameters[8],
            //    e.Parameters[9]
            //    );

            string ID = e.Parameters[0];
            string IdentifierCode = e.Parameters[1];
            string Name = e.Parameters[2];
            string CustomCategoryCode = e.Parameters[3];
            string TypeId = e.Parameters[4];
            string UserDefined = e.Parameters[5];
            string Script = e.Parameters[6];
            string CSharpCode = e.Parameters[7];
            string JsonObject = e.Parameters[8];
            string PageState = e.Parameters[9];

            this.InitializeCulture();

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();
            string[] retMessage = new string[4];

            decimal iID = 0;
            RuleTemplate RuleRecived = new RuleTemplate();
            RuleRecived.ID = Convert.ToDecimal(StringBuilder.CreateString(ID));

            PageState = StringBuilder.CreateString(PageState);
            if (PageState != "Delete")
            {
                RuleRecived.IdentifierCode = Convert.ToDecimal(StringBuilder.CreateString(IdentifierCode));
                RuleRecived.Name = StringBuilder.CreateString(Name);
                RuleRecived.CustomCategoryCode = StringBuilder.CreateString(CustomCategoryCode);
                RuleRecived.TypeId = Convert.ToDecimal(StringBuilder.CreateString(TypeId));
                RuleRecived.UserDefined = bool.Parse(StringBuilder.CreateString(UserDefined));
                RuleRecived.Script = StringBuilder.CreateString(Script);
                RuleRecived.CSharpCode = StringBuilder.CreateString(CSharpCode);
                RuleRecived.JsonObject = JsonObject;
            }

            #region Effect on DB

            try
            {
                #region Set UIActionType Enum
                UIActionType uiActionType = UIActionType.ADD;
                switch (PageState.ToUpper())
                {
                    #region Add
                    case "ADD":
                        uiActionType = UIActionType.ADD;
                        iID = RuleBusiness.InsertRule(RuleRecived);
                        break;
                    #endregion
                    #region Edit
                    case "EDIT":
                        uiActionType = UIActionType.EDIT;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforEdit").ToString()), retMessage);
                            hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                            this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
                        }

                        var concept = RuleBusiness.GetByID(RuleRecived.ID);
                        RuleBusiness.Copy(RuleRecived, ref concept);

                        iID = RuleBusiness.UpdateRule(concept);
                        break;
                    #endregion
                    #region Delete
                    case "DELETE":
                        uiActionType = UIActionType.DELETE;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforDelete").ToString()), retMessage);
                            hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                            this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
                        }
                        iID = RuleBusiness.DeleteRule(RuleRecived);
                        break;
                    #endregion
                    default:
                        break;
                }
                #endregion

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uiActionType)
                {
                    case UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = iID.ToString(CultureInfo.InvariantCulture);

                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            #endregion
        }
    }
}