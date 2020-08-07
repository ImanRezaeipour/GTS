using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GTS.Clock.Presentaion.Forms.App_Code;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class SecurityImageViewer : System.Web.UI.Page
    {
        public SecurityImageProvider SIP
        {
            get
            {
                return new SecurityImageProvider();
            }
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["SecurityCode"] == null)
                Session["SecurityCode"] = this.SIP.GenerateRandomCode();

            SecurityImageProvider SIP = new SecurityImageProvider(Session["SecurityCode"].ToString(), 110, 23, string.Empty);

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            SIP.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            SIP.Dispose();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}
