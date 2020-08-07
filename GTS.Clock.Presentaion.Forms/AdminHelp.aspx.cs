using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.UI;

namespace HelpGTS
{
    public partial class AdminHelp : GTSBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
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
            TreeView1.Nodes.Add(nodeRoot);
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

        protected void CallBack1_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            LoadTreeView();
            TreeView1.RenderControl(e.Output);
        }




    }
}