using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.RuleDesigner.UI.Web.Classes;
using GTS.Clock.RuleDesigner.UI.Web.Classes.ConceptEditor;

namespace GTS.Clock.RuleDesigner.UI.Web
{
    public partial class ConceptRuleEditor : GTSBasePage
    {
        #region Business Objects, Initialize, cstr
        public enum LoadState
        {
            New,
            Edit
        }
        public BConceptExpression BConceptExpression;
        public StringGenerator StringBuilder;
        public ExceptionHandler ExceptionHandler;
        public BLanguage LangProv;
        public ConceptRuleEditor()
        {
            if (BConceptExpression == null) BConceptExpression = new BConceptExpression();
            if (StringBuilder == null) StringBuilder = new StringGenerator();
            if (ExceptionHandler == null) ExceptionHandler = new ExceptionHandler();
            if (LangProv == null) LangProv = new BLanguage();
        }
        #endregion

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

        private enum Scripts
        {
            ConceptRuleEditor_onPageLoad,
            DialogConceptRuleEditor_Operations,
            Alert_Box,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.GenerateMethodScripts(this);
            if (
                !CallBack_trvConceptExpression_Concept.IsCallback
                &&
                !CallBack_trvDetails_Concept.IsCallback
                )
            {
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                SkinHelper.InitializeSkin(this.Page);

                //if (string.IsNullOrEmpty(hfPageIsLoadedBefore.Value))
                //    Fill_trvDetails_Concept();
            }
        }

        #region trvDetails

        protected void CallBack_trvDetails_Concept_onCallBack(object sender, CallBackEventArgs e)
        {
            var csSource = string.Empty;

            //var trvDetailsCallBack = (ComponentArt.Web.UI.TreeView)sender;

            if (trvDetails_Concept.Nodes != null &&
                trvDetails_Concept.Nodes.Count > 0)
            {
                csSource = NodeToSource(trvDetails_Concept.Nodes);
                ObjectJsonHiddenField_trDetails_Concept.Value = csSource;
            }
            //else
            //{
            //    Fill_trvDetails_Concept();
            //}

        }
        private string NodeToSource(TreeViewNodeCollection treeViewNodeCollection)
        {
            var startSourceCode = string.Empty;
            var ChildsSourceCode = string.Empty;
            var finishSourceCode = string.Empty;

            foreach (TreeViewNode treeViewNode in treeViewNodeCollection)
            {
                var curCncptExprsn =
                BConceptExpression.GetByID(Convert.ToDecimal(treeViewNode.Value));

                startSourceCode = curCncptExprsn.ScriptBeginEn;
                if (treeViewNode.Nodes != null &&
                    treeViewNode.Nodes.Count > 1)
                    ChildsSourceCode = NodeToSource(treeViewNode.Nodes);
                finishSourceCode = curCncptExprsn.ScriptEndEn;

            }

            return string.Format("{0}{2}{3}", startSourceCode, ChildsSourceCode, finishSourceCode);
        }
        protected void Fill_trvDetails_Concept()
        {
            trvDetails_Concept.NodeEditingEnabled = true;
            //trvDetails_Concept.AutoPostBackOnNodeRename = chkPostBackOnRename.Checked;

            trvDetails_Concept.DragAndDropEnabled = true;// chkDragDropEnabled.Checked;
            //trvDetails_Concept.AutoPostBackOnNodeMove = chkPostBackOnMove.Checked;
            trvDetails_Concept.DropChildEnabled = true;//chkAllowChild.Checked;
            trvDetails_Concept.DropSiblingEnabled = true;//chkAllowSibling.Checked;
            //trvDetails_Concept.DropRootEnabled = chkAllowRoot.Checked;

            var childCncptExprsnNode =
                new
                TreeViewNode
                    {
                        ID = "C1",
                        Value =
                         NodeValueExpressions.Serialize(
                         new NodeValueExpressions()),
                        Text = "قانون تستی",
                        ImageUrl = "Images/TreeView/folder.gif",
                        EditingEnabled = false
                    };

            hfConceptOrRuleIdentification.Value = childCncptExprsnNode.Value;
            hfPageIsLoadedBefore.Value = "true";

            this.trvDetails_Concept.Nodes.Add(childCncptExprsnNode);
        }

        #endregion


        #region trvConceptExpression
        protected void CallBack_trvConceptExpression_Concept_onCallBack(object sender, CallBackEventArgs e)
        {
            Fill_trvConceptExpression_Concept();
            trvConceptExpression_Concept.RenderControl(e.Output);
            ErrorHiddenField_trvConceptExpression_Concep.RenderControl(e.Output);
            SetVariableItemCodeInExpression();
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
                var rootExprsnCncpt = BConceptExpression.GetRoot();
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

                List<ConceptExpression> organizationUnitChlidList = this.BConceptExpression.GetByParentId(rootExprsnCncpt.ID);

                NodeValueExpressions_Object.MakeJsonObjectListString(organizationUnitChlidList, this.trvConceptExpression_Concept, this.LangProv.GetCurrentLanguage(),false);

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

        private void SetVariableItemCodeInExpression()
        {
            if (BConceptExpression.GetByParentId(null).FirstOrDefault() == null) return;
            VariableItemCodeInExpression.Value =
                BConceptExpression.GetByParentId(null).FirstOrDefault().ID.ToString();
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_ConceptsPage", "GetLoadonDemandError_ConceptsPage_onCallBack", null, null)]
        public string GetLoadonDemandError_PostsPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_ConceptsPage"] != null)
            {
                retError = Session["LoadonDemandError_ConceptsPage"].ToString();
                Session["LoadonDemandError_ConceptsPage"] = null;
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


        [Ajax.AjaxMethod("GetChildrenOnCreation_ConceptEditorPage", "GetChildrenOnCreation_ConceptEditorPage_onCallBack", null, null)]
        public string[] GetChildrenOnCreation_ConceptEditorPage(string parentDbId, string parentId)
        {
            StringBuilder.CreateString(parentDbId);

            string[] retMessage = new string[5];
            try
            {
                UIActionType uam = UIActionType.ADD;

                var NodeValueExpressions = new NodeValueExpressions();

                var organizationUnitChlidList = this.BConceptExpression.GetChildrenOnCreation(Convert.ToDecimal(StringBuilder.CreateString(parentDbId)));
                
                var strJSON = NodeValueExpressions.MakeJsonObjectListString(organizationUnitChlidList);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();

                retMessage[1] = "";
                retMessage[2] = "success";
                retMessage[3] = StringBuilder.CreateString(parentId); ;
                retMessage[4] = strJSON;

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


        [Ajax.AjaxMethod("UpdateConcept_ConceptEditorPage", "UpdateConcept_ConceptEditorPage_onCallBack", null, null)]
        public string[] UpdateConcept_ConceptEditorPage(string state, string jsonObj, string scriptEn, string ScriptFa)
        {
            var retMessage = new string[3];

            try
            {

                // operations
                // todo: saving into database
                // operations
                var uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

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
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                //retMessage[3] = parentId.ToString();

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

    }
}