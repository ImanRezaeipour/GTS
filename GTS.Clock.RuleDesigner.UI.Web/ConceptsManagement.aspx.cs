using System;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.RuleDesigner.UI.Web.Classes;
using ExceptionHandler = GTS.Clock.RuleDesigner.UI.Web.Classes.ExceptionHandler;
using StringGenerator = GTS.Clock.RuleDesigner.UI.Web.Classes.StringGenerator;

namespace GTS.Clock.RuleDesigner.UI.Web
{
    public partial class ConceptsManagement : GTSBasePage
    {
        #region Initialize

        public StringGenerator StringBuilder;
        public ExceptionHandler ExceptionHandler;
        public BSecondaryConceptUserDefined BSecondaryConceptUserDefined = null;
        public BLanguage LangProv;

        public ConceptsManagement()
        {
            BSecondaryConceptUserDefined = new BSecondaryConceptUserDefined();
            StringBuilder = new StringGenerator();
            LangProv = new BLanguage();
            ExceptionHandler = new ExceptionHandler();
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
            //ConceptsManagement_onPageLoad,
            ConceptsManagement_onPageLoad,
            DialogConceptsManagement_Operations,
            Alert_Box,
            DropDownDive,
            ColorPicker
        }

        public enum LoadState
        {
            Normal,
            Search,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (
                !CallBack_cmbTypeField_Concepts.IsCallback &&
                !CallBack_GridConcepts_Concept.IsCallback &&
                !CallBack_cmbPersistSituationTypeField_Concepts.IsCallback &&
                !CallBack_cmbPeriodicTypeField_Concepts.IsCallback
                )
            {
                Ajax.Utility.GenerateMethodScripts(this);
                this.SetConceptsPageSize_Concepts();
                this.SetConceptsPageCount_Concepts(LoadState.Normal, string.Empty);
                SetJsonEnumIntoHiddenField();
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                SkinHelper.InitializeSkin(this.Page);
                CheckConceptManagementLoadAccess_ConceptManagement();
            }
        }

        private void SetConceptsPageSize_Concepts()
        {
            this.hfConceptsPageSize_Concepts.Value =
                this.GridConcepts_Concepts.PageSize.ToString(CultureInfo.InvariantCulture);
        }

        private void CheckConceptManagementLoadAccess_ConceptManagement()
        {
            string[] retMessage = new string[4];
            try
            {
                // this.conceptBusiness.CheckConceptManagementLoadAccess();
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
            if (string.IsNullOrEmpty(hfJsonEnum_PeriodicType.Value))
                hfJsonEnum_PeriodicType.Value = ConvertEnumToJavascript(typeof(ScndCnpPeriodicType));

            if (string.IsNullOrEmpty(hfJsonEnum_Type.Value))
                hfJsonEnum_Type.Value = ConvertEnumToJavascript(typeof(ScndCnpPairableType));

            if (string.IsNullOrEmpty(hfJsonEnum_CalSituationType.Value))
                hfJsonEnum_CalSituationType.Value = ConvertEnumToJavascript(typeof(ScndCnpCalcSituationType));

            if (string.IsNullOrEmpty(hfJsonEnum_PersistSituationType.Value))
                hfJsonEnum_PersistSituationType.Value = ConvertEnumToJavascript(typeof(ScndCnpPersistSituationType));

            if (string.IsNullOrEmpty(hfJsonEnum_CustomeCategoryCode.Value))
                hfJsonEnum_CustomeCategoryCode.Value = ConvertEnumToJavascript(typeof(ScndCnpCustomeCategoryCode));
        }

        private void SetConceptsPageCount_Concepts(LoadState Ls, string SearchTerm)
        {
            var ConceptsCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    ConceptsCount = this.BSecondaryConceptUserDefined.GetAllByPageBySearchCount(string.Empty);
                    break;
                case LoadState.Search:
                    ConceptsCount = this.BSecondaryConceptUserDefined.GetAllByPageBySearchCount(SearchTerm);
                    break;
            }
            this.hfConceptsCount_Concepts.Value = ConceptsCount.ToString();
            this.hfConceptsPageCount_Concepts.Value =
                Utility.GetPageCount(ConceptsCount, this.GridConcepts_Concepts.PageSize).ToString();
        }

        protected void CallBack_GridConcepts_Concept_OnCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.SetConceptsPageCount_Concepts(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.Fill_GridConcepts_Concepts(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[1])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[2])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.GridConcepts_Concepts.RenderControl(e.Output);
            this.hfConceptsCount_Concepts.RenderControl(e.Output);
            this.hfConceptsPageCount_Concepts.RenderControl(e.Output);
            this.ErrorHiddenField_Concepts.RenderControl(e.Output);
        }

        private void Fill_GridConcepts_Concepts(LoadState Ls, int pageSize, int pageIndex, string searchTerm)
        {
            var retMessage = new string[4];
            IList<SecondaryConcept> ConceptsList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        ConceptsList = BSecondaryConceptUserDefined.GetAllByPageBySearch(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        ConceptsList = BSecondaryConceptUserDefined.GetAllByPageBySearch(pageIndex, pageSize, searchTerm);
                        break;
                }

                this.GridConcepts_Concepts.DataSource = ConceptsList;
                this.GridConcepts_Concepts.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbPeriodicTypeField_Concepts_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbPeriodicTypeField_Concepts.Dispose();
            Fill_cmbPeriodicTypeField_Concepts();
            cmbPeriodicTypeField_Concepts.Enabled = true;
            ErrorHiddenField_TypeFields.RenderControl(e.Output);
            cmbPeriodicTypeField_Concepts.RenderControl(e.Output);
        }
        private void Fill_cmbPeriodicTypeField_Concepts()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var enumValue in Enum.GetValues(typeof(ScndCnpPeriodicType)))
                {
                    var newComboBoxConcept = new ComboBoxItem(GetLocalResourceObject(enumValue.ToString()).ToString());
                    newComboBoxConcept.Value = ((int)enumValue).ToString();
                    cmbPeriodicTypeField_Concepts.Items.Add(newComboBoxConcept);
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

        protected void CallBack_cmbTypeField_Concepts_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbTypeField_Concepts.Dispose();
            Fill_cmbTypeField_Concepts();
            cmbTypeField_Concepts.Enabled = true;
            ErrorHiddenField_MattFields.RenderControl(e.Output);
            cmbTypeField_Concepts.RenderControl(e.Output);
        }
        private void Fill_cmbTypeField_Concepts()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var enumValue in Enum.GetValues(typeof(ScndCnpPairableType)))
                {
                    var newComboBoxConcept = new ComboBoxItem(GetLocalResourceObject(enumValue.ToString()).ToString());
                    newComboBoxConcept.Value = ((int)enumValue).ToString();
                    cmbTypeField_Concepts.Items.Add(newComboBoxConcept);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_MattFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_MattFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MattFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_ExecuteTimeField_Concepts_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbCallSituationTypeField_Concepts.Dispose();
            Fill_cmbCallSituationTypeField_Concepts();
            cmbCallSituationTypeField_Concepts.Enabled = true;
            ErrorHiddenField_ExecuteFields.RenderControl(e.Output);
            cmbCallSituationTypeField_Concepts.RenderControl(e.Output);
        }
        private void Fill_cmbCallSituationTypeField_Concepts()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var enumValue in Enum.GetValues(typeof(ScndCnpCalcSituationType)))
                {
                    var newComboBoxConcept = new ComboBoxItem(GetLocalResourceObject(enumValue.ToString()).ToString());
                    newComboBoxConcept.Value = ((int)enumValue).ToString();
                    cmbCallSituationTypeField_Concepts.Items.Add(newComboBoxConcept);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_MattFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_MattFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MattFields.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbPersistSituationTypeField_Concepts_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbPersistSituationTypeField_Concepts.Dispose();
            Fill_cmbPersistSituationTypeFieldField_Concepts();
            cmbPersistSituationTypeField_Concepts.Enabled = true;
            ErrorHiddenField_StorageMethodField.RenderControl(e.Output);
            cmbPersistSituationTypeField_Concepts.RenderControl(e.Output);
        }
        private void Fill_cmbPersistSituationTypeFieldField_Concepts()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var enumValue in Enum.GetValues(typeof(ScndCnpPersistSituationType)))
                {
                    var newComboBoxConcept = new ComboBoxItem((GetLocalResourceObject(enumValue.ToString())).ToString());
                    newComboBoxConcept.Value = ((int)enumValue).ToString();
                    cmbPersistSituationTypeField_Concepts.Items.Add(newComboBoxConcept);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_StorageMethodField.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_StorageMethodField.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_StorageMethodField.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbConceptCustomeCategoryCodeField_Concepts_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbConceptCustomeCategoryCodeField_Concepts.Dispose();
            Fill_cmbConceptCustomCategoryCode_Concepts();
            cmbConceptCustomeCategoryCodeField_Concepts.Enabled = true;
            ErrorHiddenField_ConceptCustomeCategoryCodeFields.RenderControl(e.Output);
            cmbConceptCustomeCategoryCodeField_Concepts.RenderControl(e.Output);
        }
        private void Fill_cmbConceptCustomCategoryCode_Concepts()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var enumValue in Enum.GetValues(typeof(ScndCnpCustomeCategoryCode)))
                {
                    var newComboBoxConcept = new ComboBoxItem((GetLocalResourceObject(enumValue.ToString())).ToString());
                    newComboBoxConcept.Value = ((int)enumValue).ToString();
                    cmbConceptCustomeCategoryCodeField_Concepts.Items.Add(newComboBoxConcept);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_StorageMethodField.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_StorageMethodField.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_StorageMethodField.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
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

        [Ajax.AjaxMethod("UpdateConcept_ConceptsPage", "UpdateConcept_ConceptsPage_onCallBack", null, null)]
        public string[] UpdateConcept_ConceptsPage(
            string ID,
            string IdentifierCode,
            string Name,
            //string Description,
            string Color,
            string KeyColumnName,
            string CSharpCode,
            string Script,
            string UserDefined,
            string PeriodicType,
            string Type,
            string CalcSituationType,
            string PersistSituationType,
            string CustomeCategoryCode,
            string JsonObject,
            string PageState
            )
        {

            this.InitializeCulture();

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();
            string[] retMessage = new string[4];

            decimal iID = 0;
            SecondaryConcept ConceptRecived = new SecondaryConcept();
            ConceptRecived.ID = Convert.ToDecimal(StringBuilder.CreateString(ID));

            PageState = StringBuilder.CreateString(PageState);
            if (PageState != "Delete")
            {
                uiValidationExceptions = BSecondaryConceptUserDefined.SecondaryConceptEnumJsonObjectsValidation(
                    StringBuilder.CreateString(PeriodicType),
                    StringBuilder.CreateString(Type),
                    StringBuilder.CreateString(CalcSituationType),
                    StringBuilder.CreateString(PersistSituationType)
                    );

                if (uiValidationExceptions.ExceptionList.Count > 0)
                {
                    retMessage = this.ExceptionHandler.HandleException(
                        this.Page,
                        ExceptionTypes.UIValidationExceptions,
                        uiValidationExceptions, retMessage);

                    return retMessage;
                }

                ConceptRecived.IdentifierCode = Convert.ToDecimal(StringBuilder.CreateString(IdentifierCode));
                ConceptRecived.Name = StringBuilder.CreateString(Name);
                ConceptRecived.Color = StringBuilder.CreateString(Color);
                ConceptRecived.KeyColumnName = StringBuilder.CreateString(KeyColumnName);
                ConceptRecived.CSharpCode = StringBuilder.CreateString(CSharpCode);
                ConceptRecived.Script = StringBuilder.CreateString(Script);
                ConceptRecived.UserDefined = bool.Parse(StringBuilder.CreateString(UserDefined));
                ConceptRecived.PeriodicType = (ScndCnpPeriodicType)Enum.Parse(typeof(ScndCnpPeriodicType), StringBuilder.CreateString(PeriodicType));
                ConceptRecived.Type = (ScndCnpPairableType)Enum.Parse(typeof(ScndCnpPairableType), StringBuilder.CreateString(Type));
                ConceptRecived.CalcSituationType = (ScndCnpCalcSituationType)Enum.Parse(typeof(ScndCnpCalcSituationType), StringBuilder.CreateString(CalcSituationType));
                ConceptRecived.PersistSituationType = (ScndCnpPersistSituationType)Enum.Parse(typeof(ScndCnpPersistSituationType), StringBuilder.CreateString(PersistSituationType));
                ConceptRecived.CustomCategoryCode = ((ScndCnpCustomeCategoryCode)Enum.Parse(typeof(ScndCnpCustomeCategoryCode), StringBuilder.CreateString(CustomeCategoryCode))).ToString("D");
                ConceptRecived.JsonObject = StringBuilder.CreateString(JsonObject);
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
                        iID = BSecondaryConceptUserDefined.InsertConcept(ConceptRecived);
                        break;
                    #endregion
                    #region Edit
                    case "EDIT":
                        uiActionType = UIActionType.EDIT;
                        if (ConceptRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoConceptSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }

                        var concept = BSecondaryConceptUserDefined.GetByID(ConceptRecived.ID);
                        BSecondaryConceptUserDefined.Copy(ConceptRecived, ref concept);

                        iID = BSecondaryConceptUserDefined.UpdateConcept(concept);
                        break;
                    #endregion
                    #region Delete
                    case "DELETE":
                        uiActionType = UIActionType.DELETE;
                        if (ConceptRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoConceptSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        iID = BSecondaryConceptUserDefined.DeleteConcept(ConceptRecived);
                        break;
                    #endregion
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
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = iID.ToString(CultureInfo.InvariantCulture);
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

        protected void CallBack_cmbConcept_Concepts_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbConcept_Concepts.Dispose();
            this.SetConceptsPageCount_Concepts((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbConcept_Concepts((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbConcept_Concepts.RenderControl(e.Output);
            this.hfConceptCount_Concepts.RenderControl(e.Output);
            this.hfConceptPageCount_Concepts.RenderControl(e.Output);
            this.hfErrorHiddenField_cmbConcept_Concepts.RenderControl(e.Output);
        }

        private void Fill_cmbConcept_Concepts(LoadState Ls, int pageSize, int pageIndex, string searchTerm)
        {
            var retMessage = new string[4];
            IList<SecondaryConcept> ConceptsList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        ConceptsList = BSecondaryConceptUserDefined.GetAllNonPeriodicByPageBySearch(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        ConceptsList = BSecondaryConceptUserDefined.GetAllNonPeriodicByPageBySearch(pageIndex, pageSize, searchTerm);
                        break;
                }

                foreach (SecondaryConcept secondaryConcept in ConceptsList)
                {
                    ComboBoxItem personCmbItem = new ComboBoxItem(secondaryConcept.Name + " " + secondaryConcept.IdentifierCode);
                    personCmbItem["Name"] = secondaryConcept.Name;
                    personCmbItem["Code"] = secondaryConcept.IdentifierCode;
                    personCmbItem.Id = secondaryConcept.ID.ToString();
                    this.cmbConcept_Concepts.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Concepts.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }



    }
}