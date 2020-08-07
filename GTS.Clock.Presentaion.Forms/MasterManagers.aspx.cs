using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using System.Data;
using GTS.Clock.Business.AppSettings;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class MasterManagers : GTSBasePage
    {
        public BManager ManagerBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BManager>();
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
            MasterManagers_onPageLoad,
            tbMasterManagers_TabStripMenus_Operations,
            DropDownDive,
            Alert_Box,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page MasterManagersPage = this;
            Ajax.Utility.GenerateMethodScripts(MasterManagersPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckManagesLoadAccess_MasterManagers();
        }

        public void CheckManagesLoadAccess_MasterManagers()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ManagerBusiness.CheckManagesLoadAccess();
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

        protected void CallBackWorkFlow_MasterManagers_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            if (this.StringBuilder.CreateString(e.Parameter) != string.Empty)
                this.DrawFlow_MasterManagers(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.ErrorHiddenField_WorkFlow.RenderControl(e.Output);
            this.smpWorkFlow_MasterManagers.RenderControl(e.Output);
        }

        private void DrawFlow_MasterManagers(decimal flowID)
        {
            string[] retMessage = new string[4];
            try
            {
                ComponentArt.Web.UI.SiteMapNode childManagerNode = null;
                ComponentArt.Web.UI.SiteMapNode parentManagerNode = null;
                string fistManagerNodeID = string.Empty;

                if (flowID == 0)
                {
                    this.smpWorkFlow_MasterManagers.Nodes.Clear();
                    return;
                }
                IList<Manager> ManagersList = this.ManagerBusiness.GetManagerFlow(flowID);
                foreach (Manager managerItem in ManagersList)
                {
                    if (childManagerNode == null)
                    {
                        parentManagerNode = childManagerNode = new ComponentArt.Web.UI.SiteMapNode();
                        fistManagerNodeID = childManagerNode.ID = Guid.NewGuid().ToString();
                        childManagerNode.Text = managerItem.ThePerson.Name;
                        childManagerNode.NavigateUrl = "#";
                    }
                    else
                    {
                        parentManagerNode = new ComponentArt.Web.UI.SiteMapNode();
                        if(managerItem.ThePerson.Name != " ")
                            parentManagerNode.Text = managerItem.ThePerson.Name;
                        else
                            parentManagerNode.Text = managerItem.OrganizationUnit.Name;
                        parentManagerNode.ID = Guid.NewGuid().ToString();
                        parentManagerNode.Nodes.Add(childManagerNode);
                        childManagerNode = parentManagerNode;
                        childManagerNode.NavigateUrl = "#";
                    }
                }
                this.smpWorkFlow_MasterManagers.Nodes.Add(parentManagerNode);
                this.smpWorkFlow_MasterManagers.SelectedNode = this.smpWorkFlow_MasterManagers.FindNodeById(fistManagerNodeID);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlow.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbAccessGroup_MasterManagers_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbAccessGroup_MasterManagers.Dispose();
            this.Fill_cmbAccessGroup_MasterManagers();
            this.ErrorHiddenField_Filter.RenderControl(e.Output);
            this.cmbAccessGroup_MasterManagers.RenderControl(e.Output);
        }

        private void Fill_cmbAccessGroup_MasterManagers()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<PrecardAccessGroup> AccessGroupList = this.ManagerBusiness.GetAllAccessGroups();
                this.cmbAccessGroup_MasterManagers.DataSource = AccessGroupList;
                this.cmbAccessGroup_MasterManagers.DataBind();
                this.cmbAccessGroup_MasterManagers.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Filter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Filter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Filter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbSearchField_MasterManagers_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbSearchField_MasterManagers.Dispose();
            this.Fill_cmbSearchField_MasterManagers();
            this.ErrorHiddenField_Search.RenderControl(e.Output);
            this.cmbSearchField_MasterManagers.RenderControl(e.Output);
        }

        private void Fill_cmbSearchField_MasterManagers()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (string searchItem in Enum.GetNames(typeof(ManagerSearchFields)))
                {
                    ComboBoxItem cmbItemSearch = new ComboBoxItem(GetLocalResourceObject(searchItem.ToString()).ToString());
                    cmbItemSearch.Value = searchItem.ToString();
                    this.cmbSearchField_MasterManagers.Items.Add(cmbItemSearch);
                }
                this.cmbSearchField_MasterManagers.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Filter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Filter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Filter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


    }
}