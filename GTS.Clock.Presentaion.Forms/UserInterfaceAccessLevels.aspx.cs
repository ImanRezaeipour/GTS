using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Proxy;
using System.Threading;
using System.Globalization;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Security;
using System.IO;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class UserInterfaceAccessLevels : GTSBasePage
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
            DialogUserInterfaceAccessLevels_Operations,
            UserInterfaceAccessLevels_onPageLoad,
            Alert_Box,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_trvAccessLevelsAsign_AccessLevelsAsign.IsCallback)
            {
                Page AccessLevelsPage = this;
                Ajax.Utility.GenerateMethodScripts(AccessLevelsPage);

                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckAccessLevelsAsignLoadAccess_AccessLevelsAsign();
            }
        }

        private void CheckAccessLevelsAsignLoadAccess_AccessLevelsAsign()
        {
            string[] retMessage = new string[4];
            try
            {
                this.RoleBusiness.CheckAccessLevelsAsignLoadAccess();
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

        protected void CallBack_trvAccessLevelsAsign_AccessLevelsAsign_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.Fill_trvAccessLevelsAsign_AccessLevelsAsign(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.ErrorHiddenField_AccessLevelsAsign.RenderControl(e.Output);
            this.trvAccessLevelsAsign_AccessLevelsAsign.RenderControl(e.Output);
        }

        private void Fill_trvAccessLevelsAsign_AccessLevelsAsign(decimal RoleID)
        {
            string[] retMessage = new string[4];
            try
            {
                ResourceProxy rootAccessLevel = this.RoleBusiness.GetResources(RoleID);
                TreeViewNode rootAccessLevelNode = new TreeViewNode();
                rootAccessLevelNode.Text = rootAccessLevel.ResourceName;
                rootAccessLevelNode.ID = rootAccessLevel.ID.ToString();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    rootAccessLevelNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvAccessLevelsAsign_AccessLevelsAsign.Nodes.Add(rootAccessLevelNode);
                rootAccessLevelNode.Expanded = true;

                this.GetChildAccessLevelsAsign_trvAccessLevelsAsign_AccessLevelsAsign(rootAccessLevelNode, rootAccessLevel);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AccessLevelsAsign.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AccessLevelsAsign.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AccessLevelsAsign.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildAccessLevelsAsign_trvAccessLevelsAsign_AccessLevelsAsign(TreeViewNode parentAccessLevelNode, ResourceProxy parentAccessLevel)
        {
            foreach (ResourceProxy childAccessLevel in parentAccessLevel.ChildList)
            {
                TreeViewNode childAccessLevelNode = new TreeViewNode();
                childAccessLevelNode.Text = childAccessLevel.ResourceName;
                childAccessLevelNode.ID = childAccessLevel.ID.ToString();
                childAccessLevelNode.ShowCheckBox = true;
                childAccessLevelNode.Checked = childAccessLevel.IsAllowed;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                    childAccessLevelNode.ImageUrl = "Images/TreeView/folder.gif";
                parentAccessLevelNode.Nodes.Add(childAccessLevelNode);
                try
                {
                    if (parentAccessLevelNode.Parent.Parent == null)
                        parentAccessLevelNode.Expanded = true;
                }
                catch
                { }
                if (childAccessLevel.ChildList.Count > 0)
                    this.GetChildAccessLevelsAsign_trvAccessLevelsAsign_AccessLevelsAsign(childAccessLevelNode, childAccessLevel);
            }
        }

        [Ajax.AjaxMethod("UpdateAccessLevelsAsign_AccessLevelsAsignPage", "UpdateAccessLevelsAsign_AccessLevelsAsignPage_onCallBack", null, null)]
        public string[] UpdateAccessLevelsAsign_AccessLevelsAsignPage(string RoleID, string StrAccessLevelsList)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                decimal roleID = decimal.Parse(this.StringBuilder.CreateString(RoleID));
                IList<decimal> AccessLevelsList = this.CreateAccessLevelsList_AccessLevelsAsignPage(this.StringBuilder.CreateString(StrAccessLevelsList));
                this.RoleBusiness.UpdateAthorize(roleID, AccessLevelsList);

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

        private IList<decimal> CreateAccessLevelsList_AccessLevelsAsignPage(string StrAccessLevelsList)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            object[] ObjAccessLevelsList = (object[])JsSerializer.DeserializeObject(StrAccessLevelsList);
            IList<decimal> AccessLevelsList = new List<decimal>();
            foreach (object objAccessLevelsList in ObjAccessLevelsList)
            {
                AccessLevelsList.Add(decimal.Parse(objAccessLevelsList.ToString()));
            }
            return AccessLevelsList;
        }

    }

    
}