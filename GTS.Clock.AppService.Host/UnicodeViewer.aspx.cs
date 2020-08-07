using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTS.Clock.AppService.Host
{
    public partial class UnicodeViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn1Click(object sender, EventArgs e)
        {
            if (tb1.Text.Length > 0)
            {
                string hex = ((int)tb1.Text[0]).ToString("X4");
                lbl1.Text = hex;
            }

        }
    }
}
