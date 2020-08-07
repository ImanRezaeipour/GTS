using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
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
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.RuleDesigner.UI.Web.Classes;
using GTS.Clock.RuleDesigner.UI.Web.Classes.ConceptEditor;
using ExceptionHandler = GTS.Clock.RuleDesigner.UI.Web.Classes.ExceptionHandler;
using StringGenerator = GTS.Clock.RuleDesigner.UI.Web.Classes.StringGenerator;

namespace GTS.Clock.RuleDesigner.UI.Web
{
    public partial class ExpressionsManagement : GTSBasePage
    {
        #region Initialize

        public StringGenerator StringBuilder;
        public ExceptionHandler ExceptionHandler;
        public BConceptExpression BExpression = null;
        public BLanguage LangProv;

        public ExpressionsManagement()
        {
            BExpression = new BConceptExpression();
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
            ExpressionsManagement_onPageLoad,
            DialogExpressionsManagement_Operations,
            Alert_Box,
        }

        public enum LoadState
        {
            Normal,
            Search,
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CallBack_GridExpressions_Expression.IsCallback &&
                !CallBack_trvExpressions_Expressions.IsCallback)
            {
                Ajax.Utility.GenerateMethodScripts(this);
                this.SetExpressionsPageSize_Expressions();
                this.SetExpressionsPageCount_Expressions(LoadState.Normal, string.Empty);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                SkinHelper.InitializeSkin(this.Page);
                CheckRuleManagementLoadAccess_RuleManagement();
            }
        }

        private void SetExpressionsPageSize_Expressions()
        {
            this.hfExpressionsPageSize_Expressions.Value =
                this.GridExpressions_Expressions.PageSize.ToString(CultureInfo.InvariantCulture);
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

        private void SetExpressionsPageCount_Expressions(LoadState Ls, string SearchTerm)
        {
            var ExpressionsCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    ExpressionsCount = this.BExpression.GetAllByPageBySearchCount(string.Empty);
                    break;
                case LoadState.Search:
                    ExpressionsCount = this.BExpression.GetAllByPageBySearchCount(SearchTerm);
                    break;
            }
            this.hfExpressionsCount_Expressions.Value = ExpressionsCount.ToString();
            this.hfExpressionsPageCount_Expressions.Value =
                Utility.GetPageCount(ExpressionsCount, this.GridExpressions_Expressions.PageSize).ToString();
        }

        protected void CallBack_GridExpressions_Expression_OnCallBack(object sender, CallBackEventArgs e)
        {
            this.SetExpressionsPageCount_Expressions(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.Fill_GridExpressions_Expressions(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[1])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[2])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.GridExpressions_Expressions.RenderControl(e.Output);
            this.hfExpressionsCount_Expressions.RenderControl(e.Output);
            this.hfExpressionsPageCount_Expressions.RenderControl(e.Output);
            this.ErrorHiddenField_Expressions.RenderControl(e.Output);
        }

        private void Fill_GridExpressions_Expressions(LoadState Ls, int pageSize, int pageIndex, string searchTerm)
        {
            var retMessage = new string[4];
            IList<ConceptExpression> ExpressionsList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        ExpressionsList = BExpression.GetAllByPageBySearch(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        ExpressionsList = BExpression.GetAllByPageBySearch(pageIndex, pageSize, searchTerm);
                        break;
                }

                this.GridExpressions_Expressions.DataSource = ExpressionsList;
                this.GridExpressions_Expressions.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Expressions.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex,
                                                                   retMessage);
                this.ErrorHiddenField_Expressions.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Expressions.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Expressions.Value = this.ExceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        #region trvConceptExpression
        protected void CallBack_trvExpressions_Expressions_onCallBack(object sender, CallBackEventArgs e)
        {
            Fill_trvConceptExpression_Concept();
            trvExpressions_Expressions.RenderControl(e.Output);
            ErrorHiddenField_trvExpressions_Expressions.RenderControl(e.Output);
        }

        private void Fill_trvConceptExpression_Concept()
        {
            #region image and initialization
            string imageUrl_Yellow = "Images\\TreeView\\folder.gif";
            string imagePath_Yellow = "Images/TreeView/folder.gif";

            var imageUrl_Blue = "Images\\TreeView\\folder_blue.gif";
            var imagePath_Blue = "Images/TreeView/folder_blue.gif";

            var imageUrl_silver = "Images\\TreeView\\folder_silver.gif";
            var imagePath_silver = "Images/TreeView/folder_silver.gif";

            string[] retMessage = new string[4];
            this.InitializeCulture();
            #endregion

            try
            {
                var rootExprsnCncpt = BExpression.GetByParentId(null).FirstOrDefault();
                if (rootExprsnCncpt == null) return;

                #region Create root node and details
                //var rootCncptExprsnNode = new TreeViewNode();

                var NodeValueExpressions_Object = new NodeValueExpressions();

                //rootCncptExprsnNode.ID = rootExprsnCncpt.ID.ToString();
                //rootCncptExprsnNode.Text = rootExprsnCncpt.ScriptBeginFa;
                //rootCncptExprsnNode.Value = NodeValueExpressions_Object.MakeJsonObjectListString(rootExprsnCncpt);

                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl_silver))
                //    rootCncptExprsnNode.ImageUrl = imageUrl_silver;
                #endregion

                //this.trvConceptExpression_Concept.Nodes.Add(rootCncptExprsnNode);

                List<ConceptExpression> organizationUnitChlidList = this.BExpression.GetByParentId(rootExprsnCncpt.ID);

                NodeValueExpressions_Object.MakeJsonObjectListString(organizationUnitChlidList, this.trvExpressions_Expressions, this.LangProv.GetCurrentLanguage(),false);

                //if (organizationUnitChlidList.Count > 0) rootCncptExprsnNode.Expanded = true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //catch (UIValidationExceptions ex)
            //{
            //    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            //    this.ErrorHiddenField_Posts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            //}
            //catch (UIBaseException ex)
            //{
            //    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            //    this.ErrorHiddenField_Posts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            //}
            //catch (Exception ex)
            //{
            //    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            //    this.ErrorHiddenField_Posts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            //}
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_ExpressionsPage", "GetLoadonDemandError_ExpressionsPage_onCallBack", null, null)]
        public string GetLoadonDemandError_ExpressionPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_ExpressionsPage"] != null)
            {
                retError = Session["LoadonDemandError_ExpressionsPage"].ToString();
                Session["LoadonDemandError_ExpressionsPage"] = null;
            }
            else
            {
                if (GetLocalResourceObject("RetErrorType") != null &&
                    GetLocalResourceObject("ParentNodeFillProblem") != null)
                {
                    var retMessage = new[]
                {
                    GetLocalResourceObject("RetErrorType").ToString(),
                    GetLocalResourceObject("ParentNodeFillProblem").ToString(),
                    "error"
                };
                    retError = this.ExceptionHandler.CreateErrorMessage(retMessage);
                }
            }
            return retError;
        }

        #endregion


        [Ajax.AjaxMethod("UpdateExpression_ExpressionsPage", "UpdateExpression_ExpressionsPage_onCallBack", null, null)]
        public string[] UpdateExpression_ExpressionsPage(
            string ID,
            string Parent_ID,
            string ScriptBeginFa,
            string ScriptEndFa,
            string ScriptBeginEn,
            string ScriptEndEn,
            string AddOnParentCreation,
            string CanAddToFinal,
            string CanEditInFinal,
            string Visible,
            string SortOrder,
            string PageState
            )
        {

            this.InitializeCulture();

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();
            string[] retMessage = new string[4];

            decimal iID = 0;
            ConceptExpression ConceptExpressionRecived = new ConceptExpression();
            ConceptExpressionRecived.ID = Convert.ToDecimal(StringBuilder.CreateString(ID));

            PageState = StringBuilder.CreateString(PageState);
            if (PageState != "Delete")
            {

                uiValidationExceptions = BExpression.Validation(ScriptBeginFa);

                if (uiValidationExceptions.ExceptionList.Count > 0)
                {
                    retMessage = this.ExceptionHandler.HandleException(
                        this.Page,
                        ExceptionTypes.UIValidationExceptions,
                        uiValidationExceptions, retMessage);

                    return retMessage;
                }

                if (!string.IsNullOrEmpty(StringBuilder.CreateString(Parent_ID)))
                    ConceptExpressionRecived.Parent_ID = Convert.ToDecimal(StringBuilder.CreateString(Parent_ID));
                /*
                   ID, Parent_ID, ScriptBeginFa, ScriptEndFa, ScriptBeginEn,
                   ScriptEndEn, AddOnParentCreation, CanAddToFinal, CanEditInFinal,
                   Visible, SortOrder, PageState
                 */

                ConceptExpressionRecived.ScriptBeginFa = StringBuilder.CreateString(ScriptBeginFa);
                ConceptExpressionRecived.ScriptEndFa = StringBuilder.CreateString(ScriptEndFa);
                ConceptExpressionRecived.ScriptBeginEn = StringBuilder.CreateString(ScriptBeginEn);
                ConceptExpressionRecived.ScriptEndEn = StringBuilder.CreateString(ScriptEndEn);
                ConceptExpressionRecived.SortOrder = int.Parse(StringBuilder.CreateString(SortOrder));

                ConceptExpressionRecived.AddOnParentCreation = bool.Parse(StringBuilder.CreateString(AddOnParentCreation));
                ConceptExpressionRecived.CanAddToFinal = bool.Parse(StringBuilder.CreateString(CanAddToFinal));
                ConceptExpressionRecived.CanEditInFinal = bool.Parse(StringBuilder.CreateString(CanEditInFinal));
                ConceptExpressionRecived.Visible = bool.Parse(StringBuilder.CreateString(Visible));


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
                        iID = BExpression.InsertConceptExpression(ConceptExpressionRecived);
                        break;
                    #endregion
                    #region Edit
                    case "EDIT":
                        uiActionType = UIActionType.EDIT;
                        if (ConceptExpressionRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoConceptExpressionSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }

                        var conceptFromDb = BExpression.GetByID(ConceptExpressionRecived.ID);
                        BExpression.Copy(ConceptExpressionRecived, ref conceptFromDb);

                        iID = BExpression.UpdateConceptExpression(conceptFromDb);
                        break;
                    #endregion
                    #region Delete
                    case "DELETE":
                        uiActionType = UIActionType.DELETE;
                        if (ConceptExpressionRecived.ID == 0)
                        {
                            retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoConceptExpressionSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        iID = BExpression.DeleteConceptExpression(ConceptExpressionRecived);
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


    }
}