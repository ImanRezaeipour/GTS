using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.UI;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class HelpContent : GTSBasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (Request.QueryString["formKeyHelp"] != null)
            {
                string queryS = Request.QueryString["formKeyHelp"].ToString();
                LoadHelp(queryS);
            }
            SkinHelper.InitializeSkin(this.Page);
        }
        private void LoadHelp(string formkey)
        {
            BHelp bhelp = new BHelp();
            HelpProxy pLoad = bhelp.GetHelpByFormKey(formkey);
            LiteralContent.Text = pLoad.HtmlCotent;
        }
    }
}