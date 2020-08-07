using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.RuleDesigner.UI.Web.Classes;
using ExceptionHandler = GTS.Clock.RuleDesigner.UI.Web.Classes.ExceptionHandler;

namespace GTS.Clock.RuleDesigner.UI.Web
{

    public partial class WhitePage : GTSBasePage
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetError_WhitePage();
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

        private void GetError_WhitePage()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Error"))
            {
                JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
                object[] ErrorMessage = (object[])JsSerializer.DeserializeObject(HttpContext.Current.Request.QueryString["Error"]);
                LiteralControl liError = new LiteralControl("<table><tr><td>" + ErrorMessage[0].ToString() + ":" + "</td></tr><tr><td>" + ErrorMessage[1].ToString() + "</td></tr></table>");
                this.Form.Controls.Add(liError);
            }
        }
    }
}