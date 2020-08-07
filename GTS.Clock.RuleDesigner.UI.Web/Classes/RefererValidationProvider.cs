using System.Web;
using GTS.Clock.Business.Security;
using System.Web.Security;

namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
    /// <summary>
    /// Summary description for RefererCheckProvider
    /// </summary>
    public class RefererValidationProvider
    {
        public RefererValidationProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void CheckReferer()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
                HttpContext.Current.Response.Redirect("MainPage.aspx");
        }

        public static void CheckMainPageReferer()
        {
            if (HttpContext.Current.Request.Url.Segments[1] != "MainPage.aspx" && HttpContext.Current.Request.UrlReferrer == null)
            {
                BUser.ClearUserCach();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        public static void CheckHelpReferer()
        {
            if (HttpContext.Current.Request.Url != null && !HttpContext.Current.Request.Url.ToString().Contains("Help.aspx"))
                HttpContext.Current.Response.Redirect("MainPage.aspx");
        }
    }
}