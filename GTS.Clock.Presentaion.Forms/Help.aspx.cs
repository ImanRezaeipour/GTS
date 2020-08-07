using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;


namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Help : GTSBasePage
    {
        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        enum Scripts
        {
            HelpForm_onPageLoad,
            HelpForm_Operations
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
        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckHelpReferer();
            if (!IsPostBack)
            {
                if (Request.QueryString["formKeyHelp"] != null)
                {
                    hf_TreeViewFormKey_HelpForm.Value = Request.QueryString["formKeyHelp"].ToString();
                }
            }
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        private void LoadTreeView()
        {
            BHelp bhelp = new BHelp();
            HelpProxy proot = bhelp.GetHelpRoot();
            ComponentArt.Web.UI.TreeViewNode nodeRoot = new ComponentArt.Web.UI.TreeViewNode();
            nodeRoot.Text = proot.Name;
            nodeRoot.ID = proot.FormKey.ToString();
            nodeRoot.Value = proot.ID.ToString();
            LoadChild(nodeRoot);
            TreeViewHelpForm_HelpForm.Nodes.Add(nodeRoot);

        }
        private void LoadChild(ComponentArt.Web.UI.TreeViewNode nodRoot)
        {

            BHelp bhelp = new BHelp();
            IList<HelpProxy> pchild = bhelp.GetHelpChilds(Convert.ToDecimal(nodRoot.Value));

            foreach (var item in pchild)
            {

                ComponentArt.Web.UI.TreeViewNode itemchild = new ComponentArt.Web.UI.TreeViewNode();
                itemchild.Text = item.Name;
                itemchild.ID = item.FormKey.ToString();
                itemchild.Value = item.ID.ToString();
                nodRoot.Nodes.Add(itemchild);
                LoadChild(itemchild);
            }

        }

        protected void CallBack_TreeViewHelpForm_HelpForm_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            LoadTreeView();
            TreeViewHelpForm_HelpForm.RenderControl(e.Output);
        }






    }
}