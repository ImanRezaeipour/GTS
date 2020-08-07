using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using ComponentArt.Web.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;
using System.IO;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class PreCard : GTSBasePage
    {
        public BPrecard PrecardBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BPrecard>();
            }
        }

        public BRole RoleBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BRole>();
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

        enum Scripts
        {
            PreCard_onPageLoad,
            tbPreCard_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbPreCardType_PreCard.IsCallback && !CallBack_GridPreCards_PreCard.IsCallback && !CallBack_trvPrecardAccessLevels_PreCard.IsCallback)
            {
                Page PreCardPage = this;
                Ajax.Utility.GenerateMethodScripts(PreCardPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckPrecardsLoadAccess_PraCard();
            }
        }

        private void CheckPrecardsLoadAccess_PraCard()
        {
            string[] retMessage = new string[4];
            try
            {
                this.PrecardBusiness.CheckPrecardsLoadAccess();
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);                            
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

        protected void CallBack_cmbPreCardType_PreCard_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPreCardType_PreCard.Dispose();
            this.Fill_cmbPreCardType_PreCard();
            this.ErrorHiddenField_PreCardType.RenderControl(e.Output);
            this.cmbPreCardType_PreCard.Enabled = true;
            this.cmbPreCardType_PreCard.RenderControl(e.Output);
        }

        private void Fill_cmbPreCardType_PreCard()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<PrecardGroups> PreCardTypesList = this.PrecardBusiness.GetAllPrecardGroups();
                this.cmbPreCardType_PreCard.DataSource = PreCardTypesList;
                this.cmbPreCardType_PreCard.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_PreCardType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_PreCardType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_PreCardType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridPreCards_PreCard_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridPreCards_PreCard();
            this.ErrorHiddenField_PreCards.RenderControl(e.Output);
            this.GridPreCards_PreCard.RenderControl(e.Output);
        }

        private void Fill_GridPreCards_PreCard()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<Precard> PreCardList = this.PrecardBusiness.GetAll();
                this.GridPreCards_PreCard.DataSource = PreCardList;
                this.GridPreCards_PreCard.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_PreCards.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_PreCards.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_PreCards.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            } 
        }

        [Ajax.AjaxMethod("UpdatePreCard_PreCardPage", "UpdatePreCard_PreCardPage_onCallBack", null, null)]
        public string[] UpdatePreCard_PreCardPage(string state, string SelectedPreCardID, string IsActive, string PreCardCode, string PreCardName, string PreCardTypeID, string IsDaily, string IsHourly, string IsMonthly, string IsJustification)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal PreCardID = 0;
                decimal selectedPreCardID = decimal.Parse(this.StringBuilder.CreateString(SelectedPreCardID));
                bool isActive = bool.Parse(this.StringBuilder.CreateString(IsActive));
                PreCardCode = this.StringBuilder.CreateString(PreCardCode);
                PreCardName = this.StringBuilder.CreateString(PreCardName);
                decimal preCardTypeID = decimal.Parse(this.StringBuilder.CreateString(PreCardTypeID));
                bool isDaily = bool.Parse(this.StringBuilder.CreateString(IsDaily));
                bool isHourly = bool.Parse(this.StringBuilder.CreateString(IsHourly));
                bool isMonthly = bool.Parse(this.StringBuilder.CreateString(IsMonthly));
                bool isJustification = bool.Parse(this.StringBuilder.CreateString(IsJustification));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Precard precard = new Precard();
                precard.ID = selectedPreCardID;
                if (uam != UIActionType.DELETE)
                {
                    precard.Active = isActive;
                    precard.Code = PreCardCode;
                    precard.Name = PreCardName;
                    PrecardGroups precardType = new PrecardGroups();
                    precardType.ID = preCardTypeID;
                    precard.PrecardGroup = precardType;
                    precard.IsDaily = isDaily;
                    precard.IsHourly = isHourly;
                    precard.IsMonthly = isMonthly;
                    precard.IsPermit = isJustification;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        PreCardID = this.PrecardBusiness.InsertPrecard(precard, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedPreCardID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPreCardSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        PreCardID = this.PrecardBusiness.UpdatePrecard(precard,uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedPreCardID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPreCardSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        PreCardID = this.PrecardBusiness.DeletePrecard(precard, uam);
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
                retMessage[3] = PreCardID.ToString();
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

        protected void CallBack_trvPrecardAccessLevels_PreCard_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvPrecardAccessLevels_PreCard(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.ErrorHiddenField_PrecardAccessLevels.RenderControl(e.Output);
            this.trvPrecardAccessLevels_PreCard.RenderControl(e.Output);
        }

        private void Fill_trvPrecardAccessLevels_PreCard(decimal PrecardID)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                Precard precard = this.PrecardBusiness.GetByID(PrecardID);
                Role rootDep = this.RoleBusiness.GetRoleTree();
                TreeViewNode rootRoleNode = new TreeViewNode();
                rootRoleNode.ID = rootDep.ID.ToString();
                string rootRoleNodeText = string.Empty;
                if (GetLocalResourceObject("RolesNode_trvPrecardAccessLevels_PreCard") != null)
                    rootRoleNodeText = GetLocalResourceObject("RolesNode_trvPrecardAccessLevels_PreCard").ToString();
                else
                    rootRoleNodeText = rootDep.Name;
                rootRoleNode.Text = rootRoleNodeText;
                rootRoleNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    rootRoleNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvPrecardAccessLevels_PreCard.Nodes.Add(rootRoleNode);
                rootRoleNode.Expanded = true;

                this.GetChildPrecardAccessLevels_trvPrecardAccessLevels_PreCard(rootRoleNode, precard, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_PrecardAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_PrecardAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_PrecardAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildPrecardAccessLevels_trvPrecardAccessLevels_PreCard(TreeViewNode parentRoleNode, Precard precard, Role parentRole)
        {
            foreach (Role childRole in this.RoleBusiness.GetRoleChilds(parentRole.ID))
            {
                TreeViewNode childRoleNode = new TreeViewNode();
                childRoleNode.ID = childRole.ID.ToString();
                childRoleNode.Text = childRole.Name;
                childRoleNode.Value = childRole.CustomCode;
                childRoleNode.ShowCheckBox = true;
                if (precard.AccessRoleList.Where(role => role.ID == childRole.ID).Count() > 0)
                    childRoleNode.Checked = true;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                    childRoleNode.ImageUrl = "Images/TreeView/folder.gif";
                parentRoleNode.Nodes.Add(childRoleNode);
                try
                {
                    if (parentRoleNode.Parent.Parent == null)
                        parentRoleNode.Expanded = true;
                }
                catch
                { }
                if (this.RoleBusiness.GetRoleChilds(childRole.ID).Count > 0)
                    this.GetChildPrecardAccessLevels_trvPrecardAccessLevels_PreCard(childRoleNode, precard, childRole);
            }
        }

        [Ajax.AjaxMethod("UpdatePrecardAccessLevelsAsign_PreCardPage", "UpdatePrecardAccessLevelsAsign_PreCardPage_onCallBack", null, null)]
        public string[] UpdatePrecardAccessLevelsAsign_PreCardPage(string PrecardID, string StrPrecardAccessLevelsList) 
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                decimal precardID = decimal.Parse(this.StringBuilder.CreateString(PrecardID));
                IList<Role> RolesList = this.CreateRolesList_PreCard(this.StringBuilder.CreateString(StrPrecardAccessLevelsList));
                this.PrecardBusiness.UpdateRoleAccess(precardID, RolesList);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("EditComplete").ToString();
                retMessage[2] = "success";
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

        private IList<Role> CreateRolesList_PreCard(string StrAccessLevelsList)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            object[] ObjAccessLevelsList = (object[])JsSerializer.DeserializeObject(StrAccessLevelsList);
            IList<Role> RolesList = new List<Role>();
            foreach (object objAccessLevelsList in ObjAccessLevelsList)
            {
                RolesList.Add(this.RoleBusiness.GetByID(decimal.Parse(objAccessLevelsList.ToString())));
            }
            return RolesList;
        }


    }
}