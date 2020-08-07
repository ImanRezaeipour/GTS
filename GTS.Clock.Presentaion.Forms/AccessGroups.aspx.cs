using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using System.IO;
using GTS.Clock.Business.Proxy;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class AccessGroups : GTSBasePage
    {

        public BPrecardAccessGroup AccessGroupBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BPrecardAccessGroup>();
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
            AccessGroups_onPageLoad,
            DialogAccessGroups_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridAccessGroups_AccessGroups.IsCallback && !CallBack_trvAccessLevel_AccessGroups.IsCallback)
            {
                Page AccessGroupsPage = this;
                Ajax.Utility.GenerateMethodScripts(AccessGroupsPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckAccessGroupsLoadAccess_AccessGroups();
            }
        }

        private void CheckAccessGroupsLoadAccess_AccessGroups()
        {
            string[] retMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("FlowState"))
                {
                    UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["FlowState"]).ToUpper());
                    switch (uam)
                    {
                        case UIActionType.ADD:
                            this.AccessGroupBusiness.CheckAccessGroupsLoadAccess_onOrganizationFlowInsert();
                            break;
                        case UIActionType.EDIT:
                            this.AccessGroupBusiness.CheckAccessGroupsLoadAccess_onOrganizationFlowUpdate();
                            break;
                    }
                }
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

        protected void CallBack_GridAccessGroups_AccessGroups_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridAccessGroups_AccessGroups();
            this.ErrorHiddenField_AccessGroups.RenderControl(e.Output);
            this.GridAccessGroups_AccessGroups.RenderControl(e.Output);
        }

        private void Fill_GridAccessGroups_AccessGroups()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<PrecardAccessGroup> accessGroupsList = this.AccessGroupBusiness.GetAll();
                this.GridAccessGroups_AccessGroups.DataSource = accessGroupsList;
                this.GridAccessGroups_AccessGroups.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AccessGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_trvAccessLevel_AccessGroups_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvAccessLevel_AccessGroups(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])));
            this.hfAccessLevelsList_AccessGroups.RenderControl(e.Output);
            this.ErrorHiddenField_AccessLevel.RenderControl(e.Output);
            this.trvAccessLevel_AccessGroups.RenderControl(e.Output);
        }

        private void Fill_trvAccessLevel_AccessGroups(decimal accessGroupID)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                string ImageUrl = "Images/TreeView/folder.gif";
                string ImagePath = "Images\\TreeView\\folder.gif";
                this.hfAccessLevelsList_AccessGroups.Value = string.Empty;
                IList<PrecardGroups> PrecardGroupsList = this.AccessGroupBusiness.GetPrecardTree(accessGroupID);
                foreach (PrecardGroups precardGroupsItem in PrecardGroupsList)
                {
                    TreeViewNode trvNodePrecardGroups = new TreeViewNode();
                    trvNodePrecardGroups.Text = precardGroupsItem.Name;
                    trvNodePrecardGroups.ID = precardGroupsItem.ID.ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
                        trvNodePrecardGroups.ImageUrl = ImageUrl;
                    trvNodePrecardGroups.ShowCheckBox = true;
                    trvNodePrecardGroups.Checked = precardGroupsItem.ContainInPrecardAccessGroup;
                    foreach (Precard precardItem in this.AccessGroupBusiness.GetPrecardGroupChilds(precardGroupsItem.ID))
                    {
                        TreeViewNode trvNodePreCard = new TreeViewNode();
                        trvNodePreCard.Text = precardItem.Name;
                        trvNodePreCard.ID = precardItem.ID.ToString();
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
                            trvNodePreCard.ImageUrl = ImageUrl;
                        trvNodePreCard.ShowCheckBox = true;
                        trvNodePreCard.Checked = precardItem.ContainInPrecardAccessGroup;
                        trvNodePrecardGroups.Nodes.Add(trvNodePreCard);
                        if (!precardGroupsItem.ContainInPrecardAccessGroup)
                             this.CreateAccessLevelsList_AccessGroups(precardItem.ID.ToString(), precardItem.ContainInPrecardAccessGroup, precardGroupsItem.ID.ToString());
                    }
                    this.trvAccessLevel_AccessGroups.Nodes.Add(trvNodePrecardGroups);
                    this.CreateAccessLevelsList_AccessGroups(precardGroupsItem.ID.ToString(), precardGroupsItem.ContainInPrecardAccessGroup, "undefined");
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }

        private void CreateAccessLevelsList_AccessGroups(string ID, bool Checked, string parentID)
        {
            if (Checked)
                this.hfAccessLevelsList_AccessGroups.Value += "ID=" + ID + "%Ch=" + Checked.ToString().ToLower() + "%P=" + parentID + "#";
        }

        [Ajax.AjaxMethod("UpdateAccessGroup_AccessGroupsPage", "UpdateAccessGroup_AccessGroupsPage_onCallBack", null, null)]
        public string[] UpdateAccessGroup_AccessGroupsPage(string state, string flowState, string SelectedAccessGroupID, string AccessGroupName, string AccessGroupDescription, string StrAccessLevelsList)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal AccessGroupID = 0;
                decimal selectedAccessGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedAccessGroupID));
                AccessGroupName = this.StringBuilder.CreateString(AccessGroupName);
                AccessGroupDescription = this.StringBuilder.CreateString(AccessGroupDescription);
                StrAccessLevelsList = this.StringBuilder.CreateString(StrAccessLevelsList);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
                UIActionType FlowState = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(flowState).ToUpper());
                PrecardAccessGroup accessGroup = new PrecardAccessGroup();
                accessGroup.ID = selectedAccessGroupID;
                accessGroup.Name = AccessGroupName;
                accessGroup.Description = AccessGroupDescription;

                IList<AccessGroupProxy> AccessLevelsList = new List<AccessGroupProxy>();
                if (StrAccessLevelsList != "null")
                    AccessLevelsList = this.CreateAccessLevelsList_AccessGroups(StrAccessLevelsList);
                switch (uam)
                {
                    case UIActionType.ADD:
                        switch (FlowState)
	                    {
                            case UIActionType.ADD:
                                AccessGroupID = this.AccessGroupBusiness.InsertAccessGroup_onOrganizationFlowInsert(AccessGroupName, AccessGroupDescription, AccessLevelsList);
                                break;
                            case UIActionType.EDIT:
                                AccessGroupID = this.AccessGroupBusiness.InsertAccessGroup_onOrganizationFlowUpdate(AccessGroupName, AccessGroupDescription, AccessLevelsList);
                                break;
	                    }
                        break;
                    case UIActionType.EDIT:
                        if (selectedAccessGroupID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoAccessGroupSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        bool IsUpdateAccessLevelsList = true;
                        if (StrAccessLevelsList == "null")
                            IsUpdateAccessLevelsList = false;
                        switch (FlowState)
	                    {
                            case UIActionType.ADD:
                                AccessGroupID = this.AccessGroupBusiness.UpdateAccessGroup_onOrganizationFlowInsert(selectedAccessGroupID, AccessGroupName, AccessGroupDescription, AccessLevelsList, IsUpdateAccessLevelsList);
                                break;
                            case UIActionType.EDIT:
                                AccessGroupID = this.AccessGroupBusiness.UpdateAccessGroup_onOrganizationFlowUpdate(selectedAccessGroupID, AccessGroupName, AccessGroupDescription, AccessLevelsList, IsUpdateAccessLevelsList);
                                break;
	                    }
                        break;
                    case UIActionType.DELETE:
                        if (selectedAccessGroupID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoAccessGroupSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        switch (FlowState)
	                    {
                            case UIActionType.ADD:
                                this.AccessGroupBusiness.DeleteAccessGroup_onOrganizationFlowInsert(accessGroup, uam);
                                break;
                            case UIActionType.EDIT:
                                this.AccessGroupBusiness.DeleteAccessGroup_onOrganizationFlowUpdate(accessGroup, uam);
                                break;
	                    }
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
                retMessage[3] = AccessGroupID.ToString();
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

        private List<AccessGroupProxy> CreateAccessLevelsList_AccessGroups(string StrAccessLevelsList)
        {
            List<AccessGroupProxy> AccessLevelsList = new List<AccessGroupProxy>();
            StrAccessLevelsList = StrAccessLevelsList.Replace("ID=", string.Empty).Replace("Ch=", string.Empty).Replace("P=", string.Empty);
            string[] AccessLevelsListParts = StrAccessLevelsList.Split(new char[] { '#' });
            for (int i = 0; i < AccessLevelsListParts.Length; i++)
            {
                if (AccessLevelsListParts[i] != string.Empty)
                {
                    string[] AccessLevelsListPartsDetails = AccessLevelsListParts[i].Split(new char[] { '%' });
                    if (AccessLevelsListPartsDetails[2] != "Delete")
                    {
                        AccessGroupProxy accessLevel = new AccessGroupProxy();
                        accessLevel.ID = decimal.Parse(AccessLevelsListPartsDetails[0]);
                        accessLevel.Checked = bool.Parse(AccessLevelsListPartsDetails[1]);
                        if (AccessLevelsListPartsDetails[2] == "undefined")
                            accessLevel.IsParent = true;
                        else
                            accessLevel.IsParent = false;
                        AccessLevelsList.Add(accessLevel);
                    }
                }
            }
            return AccessLevelsList;
        }


    }
}