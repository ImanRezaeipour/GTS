using System;
using System.Web;

namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
    /// <summary>
    /// Summary description for CacheSettingsProvider
    /// </summary>
    public class CacheSettingsProvider
    {
        public CacheSettingsProvider()
        {

        }

        public void ClearCache()
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}