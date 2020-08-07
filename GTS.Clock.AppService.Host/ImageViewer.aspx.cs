using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTS.Clock.AppService.Host
{
    public partial class ImageViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                if (Request.QueryString["personId"] != null && GTS.Clock.Infrastructure.Utility.Utility.IsNumber(Request.QueryString["personId"])) 
                {
                    string personId = Request.QueryString["personId"];
                    imageControl.ImageUrl = String.Format("~/ImageLoader.aspx?personid={0}", personId);                  
                }
            }
        }
    }
}
