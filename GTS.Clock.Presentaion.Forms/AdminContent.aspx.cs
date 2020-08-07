using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.UI;
namespace HelpGTS
{
    public partial class AdminContent : GTSBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["formKey"] != null)
                {
                    LoadContent(Request.QueryString["formKey"].ToString());
                    HFFormID.Value = Request.QueryString["formID"].ToString();

                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BHelp bhelp = new BHelp();
            HelpProxy help = new HelpProxy();
            help.ID = Convert.ToDecimal(HFFormID.Value);
            help.HtmlCotent = FreeTextBox1.Text;
            bhelp.UpdateHelp(help);
            HFMassegeOk.Value = "ok";
        }
        private void LoadContent(string itmSelected)
        {
            BHelp bhelp = new BHelp();
            HelpProxy help = new HelpProxy();
            help = bhelp.GetHelpByFormKey(itmSelected);
            FreeTextBox1.Text = help.HtmlCotent;
        }
    }
}