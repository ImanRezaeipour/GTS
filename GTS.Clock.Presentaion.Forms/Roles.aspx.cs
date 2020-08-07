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
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Security;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.IO;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Roles : GTSBasePage
    {
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
            Roles_onPageLoad,
            tbRoles_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }	    

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_trvRoles_Roles.IsCallback)
            {
                Page RolesPage = this;
                Ajax.Utility.GenerateMethodScripts(RolesPage);

                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckRolesLoadAccess_Roles();
            }            
        }

        private void CheckRolesLoadAccess_Roles()
        {
            string[] retMessage = new string[4];
            try
            {
                this.RoleBusiness.CheckRolesLoadAccess();
            }
            catch (BaseException ex)
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

        private void Fill_trvRoles_Roles()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                Role rootDep = this.RoleBusiness.GetRoleTree();
                TreeViewNode rootRoleNode = new TreeViewNode();
                rootRoleNode.ID = rootDep.ID.ToString();
                string rootRoleNodeText = string.Empty;
                if (GetLocalResourceObject("RolesNode_trvRoles_Roles") != null)
                    rootRoleNodeText = GetLocalResourceObject("RolesNode_trvRoles_Roles").ToString();
                else
                    rootRoleNodeText = rootDep.Name;
                rootRoleNode.Text = rootRoleNodeText;
                rootRoleNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    rootRoleNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvRoles_Roles.Nodes.Add(rootRoleNode);
                rootRoleNode.Expanded = true;

                this.GetChildRoles_trvRoles_Roles(rootRoleNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildRoles_trvRoles_Roles(TreeViewNode parentRoleNode, Role parentRole)
        {
            foreach (Role childRole in this.RoleBusiness.GetRoleChilds(parentRole.ID))
            {
                TreeViewNode childRoleNode = new TreeViewNode();
                childRoleNode.ID = childRole.ID.ToString();
                childRoleNode.Text = childRole.Name;
                childRoleNode.Value = childRole.CustomCode;
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
                    this.GetChildRoles_trvRoles_Roles(childRoleNode, childRole);
            }
        }

        protected void CallBack_trvRoles_Roles_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvRoles_Roles();
            this.ErrorHiddenField_Roles.RenderControl(e.Output);
            this.trvRoles_Roles.RenderControl(e.Output);
        }

        [Ajax.AjaxMethod("UpdateRoles_RolesPage", "UpdateRoles_RolesPage_onCallBack", null, null)]
        public string[] UpdateRoles_RolesPage(string state, string SelectedRoleID, string RoleCode, string RoleName)
        {

            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal RoleID = 0;
                decimal selectedRoleID = decimal.Parse(this.StringBuilder.CreateString(SelectedRoleID));
                RoleCode = this.StringBuilder.CreateString(RoleCode);
                RoleName = this.StringBuilder.CreateString(RoleName);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Role role = new Role();
                if (uam != UIActionType.DELETE)
                {
                    role.CustomCode = RoleCode;
                    role.Name = RoleName;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        role.ParentId = selectedRoleID;
                        role.ID = 0;
                        RoleID = this.RoleBusiness.InsertRole(role, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedRoleID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRoleSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            role.ID = selectedRoleID;
                        RoleID = this.RoleBusiness.UpdateRole(role, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedRoleID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRoleSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            role.ID = selectedRoleID;
                        RoleID = this.RoleBusiness.DeleteRole(role, uam);
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
                retMessage[3] = RoleID.ToString();
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