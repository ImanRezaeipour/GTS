using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GTS.Clock.Infrastructure
{
    /// <summary>
    /// برروی دادهای کش شده مدیریت میکند
    /// </summary>
    public class SessionHelper
    {
        public const string GTSNotificationServiceHistory = "GTSNotificationServiceHistory";
        public const string BussinessSecurityResourceList = "BussinessSecurityResourceList";
        public const string BussinessSecurityAllResourceList = "BussinessSecurityAllResourceList";
        public const string BussinessSystemLanguageSessionName1 = "BussinessSystemLanguageSessionName1";
        public const string BussinessLocalLanguageSessionName1 = "BussinessLocalLanguageSession1";
        public const string BussinessLocalLanguageSessionName2 = "BussinessLocalLanguageSession2";
        public const string BussinessLocalLanguageIDSessionName = "BussinessLocalLanguageIDSessionName";
        public const string BussinessSystemLanguageIDSessionName = "BussinessSystemLanguageIDSessionName";
        public const string BussinessSystemLanguageSessionName = "BussinessSystemLanguageSession1";
        public const string BussinessLocalSkinSessionName = "BussinessLocalSkinSessionName";
        public const string BussinessAllSkinSessionName = "BussinessAllSkinSessionName";
        public const string BussinessCurentUser = "BussinessCurentUser1";
        public const string GTSApplicationSettings = "GTSApplicationSettings";
        public const string BusinessTotalCalculateCount = "BusinessTotalCalculateCount";
        public const string LicensePersonCount = "LicensePersonCount";
        public const string GTSCurrentUserManagmentState = "GTSCurrentUserManagmentState";
        public const string TrafficsTransferCompletedCountSessionName = "TrafficsTransferCompletedCountSessionName";
        public const string TrafficsCountSessionName = "TrafficsCountSessionName";
        public const string ReportHelperSessionName = "ReportHelperSessionName";
        public const string IKartablRequestsKey = "IKartablRequests", IRegisteredRequestsKey = "IRegisteredRequests", IReviewedRequestsKey = "IReviewedRequests", IRequestKey = "BRequest", ISpecialKartablRequestsKey = "ISpecialKartablRequests";

        public static SessionWorkSpace SessionWorkSpace
        {
            get;
            set;
        }
        private static Dictionary<string, object> WinServiceSession=new Dictionary<string, object>();
        
        /// <summary>
        /// has session initilized
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public static bool HasSessionValue(string sessionKey) 
        {
            if (SessionWorkSpace == SessionWorkSpace.WinService)
            {
                if (!WinServiceSession.ContainsKey(sessionKey))
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session[sessionKey] == null)
                {
                    return false;
                }
                return true;
            }
        }

        public static void SaveSessionValue(string sessionKey,object value)
        {
            if (SessionWorkSpace == SessionWorkSpace.WinService)
            {
                if (SessionHelper.HasSessionValue(sessionKey)) 
                {
                    WinServiceSession[sessionKey] = value;
                }
                else
                {
                    WinServiceSession.Add(sessionKey, value);
                }
            }
            else
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session.Add(sessionKey, value);
                }
            }
        }

        public static object GetSessionValue(string sessionKey)
        {
            object value = null;
            if (SessionWorkSpace == SessionWorkSpace.WinService)
            {
                if (WinServiceSession.ContainsKey(sessionKey)) 
                {
                    value = WinServiceSession[sessionKey];
                }
            }
            else
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    value = HttpContext.Current.Session[sessionKey];
                }
            }
            return value;
        }

        public static void ClearSessionValue(string sessionKey)
        {
            if (SessionWorkSpace == SessionWorkSpace.WinService)
            {
                if (WinServiceSession.ContainsKey(sessionKey))
                {
                    WinServiceSession.Remove(sessionKey);
                }
            }
            else
            {
                if (HttpContext.Current != null && HttpContext.Current.Session!=null)
                {
                    HttpContext.Current.Session.Add(sessionKey, null);
                }
            }
        }

        /// <summary>
        /// همه چیزهایی که در نشست ذخیره شده است حذف میشود
        /// </summary>
        public static void ClearAllCachedData()
        {
            if (SessionWorkSpace == SessionWorkSpace.WinService)
            {
                WinServiceSession.Clear();
            }
            else
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null )
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Add(BussinessSecurityResourceList, null);
                    HttpContext.Current.Session.Add(BussinessSecurityAllResourceList, null);
                    HttpContext.Current.Session.Add(BussinessSystemLanguageSessionName1, null);
                    HttpContext.Current.Session.Add(BussinessSystemLanguageIDSessionName, null);
                    HttpContext.Current.Session.Add(BussinessLocalLanguageSessionName1, null);
                    HttpContext.Current.Session.Add(BussinessLocalLanguageSessionName2, null);
                    HttpContext.Current.Session.Add(BussinessLocalLanguageIDSessionName, null);
                    HttpContext.Current.Session.Add(BussinessSystemLanguageSessionName, null);
                    HttpContext.Current.Session.Add(BussinessLocalSkinSessionName, null);
                    HttpContext.Current.Session.Add(BussinessAllSkinSessionName, null);
                    HttpContext.Current.Session.Add(BussinessCurentUser, null);
                    HttpContext.Current.Session.Add(BusinessTotalCalculateCount, null);
                    HttpContext.Current.Session.Add(GTSApplicationSettings, null);
                    HttpContext.Current.Session.Add(LicensePersonCount, null);
                    HttpContext.Current.Session.Add(GTSCurrentUserManagmentState, null);

                    //clear Security Resource for all roles
                    IList<string> keys = new List<string>();
                    foreach (string key in HttpContext.Current.Session.Keys)
                    {
                        if (key.Contains(BussinessSecurityResourceList))
                        {
                            keys.Add(key);
                        }
                    }

                    foreach (string key in keys)
                    {
                        HttpContext.Current.Session.Add(key, null);
                    }
                    HttpContext.Current.Session.Abandon();
                }
            }
        }

    }
}
