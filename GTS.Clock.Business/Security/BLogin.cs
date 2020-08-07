using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Security
{
    public class BLogin
    {
        const string ExceptionSrc = "GTS.Clock.Business.Security.BLogin";
        #region Properties
    
        /// <summary>
        /// اطلاعات مربوط به دامین تعریف شده را برمیگرداند
        /// </summary>
        public string GetDomainInfo
        {
            get
            {
                return GTS.Clock.Infrastructure.Utility.Utility.ReadAppSetting("DomainInfo");
            }
        }

        /// <summary>
        /// کاربری که درحال حاضر وارد شده است را برمیگرداند
        /// </summary>
        public string CurentUserName 
        {
            get
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    return System.Web.HttpContext.Current.User.Identity.Name;
                return "";
            }
        }

        /// <summary>
        /// آیا کاربری وارد شده است
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// آیا برای کاربر در سیستم اجازه استفاده از اکتیودایرکتوری تعریف شده است
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ActiveDirectoryISAllowed(string username)
        {
            username =Utility.GetSimpleUsername(username);
            BUser buser = new BUser();
            User user = buser.GetByUsername(username);
            if (user != null && user.ID > 0 && user.ActiveDirectoryAuthenticate)
            {
                return true;
            }
            return false;
        }

        public bool IsAuthenticate(string username, string pass)
        {
            try
            {
                GTSMembershipProvider provider = new GTSMembershipProvider();
                return provider.ValidateUser(username, pass);
            }
            catch (Exception ex) 
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }
        #endregion
    }
}
