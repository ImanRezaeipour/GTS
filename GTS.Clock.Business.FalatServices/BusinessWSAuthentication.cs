using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.BusinessService
{
    public class BusinessWSAuthentication : UserNamePasswordValidator
    {
        GTSWinSvcLogger logger = new GTSWinSvcLogger();
        const string UsernameSpiliter = "|*|";
        private const string WebserivceResourceKey = "WSAccessKey";
        public override void Validate(string userName, string password)
        {
            if (Utility.IsEmpty(userName) ||
                Utility.IsEmpty(password))
            {
                throw new IllegalServiceAccess("نام کاربری و یا کلمه عبور نا معتبر است", "Business WS");
            }
            BLogin securitySerivce = new BLogin();
            bool isAuthenticate = securitySerivce.IsAuthenticate(userName, password);
            if (!isAuthenticate)
            {
                throw new IllegalServiceAccess("کاربر قابل شناسایی نمیباشد", "Business WS Authentication");
            }
            BRole busRole = new BRole();
            if (!busRole.HasAccessToResource(userName, WebserivceResourceKey))
            {
                throw new IllegalServiceAccess("اجازه دسترسی به سرویس را ندارید", "Business WS Authorization");
            }
            else
            {
                return;
            }
        }
    }
}
