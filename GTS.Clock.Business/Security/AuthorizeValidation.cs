using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using PostSharp.Aspects;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;


namespace GTS.Clock.Business.Security
{
    /// <summary>
    /// از این کلاس برای اعتبارسنجی در فراخوانی متدهای بیزینس استفاده میشود
    /// </summary>
    [Serializable]
    class AuthorizeValidation //: OnMethodBoundaryAspect
    {
        BLogin businessLogin = new BLogin();
        UserRepository userRepository = new UserRepository(false);
/*
        public override void OnEntry(MethodExecutionArgs args)
        {
            string methodName = args.Method.Name;
            if (businessLogin.IsAuthenticated)
            {
                string username = businessLogin.CurentUserName;

                if (username != null && username.Length > 0)
                {
                    User user = userRepository.GetByUserName(username);

                    IList<Authorize> authorizeList = user.Role.AuthorizeList.Where(x => x.Allow = false).ToList();
                    foreach (Authorize authorize in authorizeList)
                    {
                        Resource resource = authorize.Resource;
                        if (resource.MethodInfo.Length > 0
                            && resource.MethodInfo.Equals(methodName))
                        {
                            throw new IllegalServiceAccess("دسترسی غیر مجاز به سرویسها", "GTS.Clock.Business.Security.AuthorizeValidation :: " + methodName);
                        }
                    }
                }

                else
                {
                    throw new IllegalServiceAccess("دسترسی غیر مجاز به سرویسها - نام کاربری غیرمجاز", "GTS.Clock.Business.Security.AuthorizeValidation :: " + methodName);
                }
            }
            else
            {
                throw new IllegalServiceAccess("دسترسی غیر مجاز به سرویسها - کاربر غیرمجاز", "GTS.Clock.Business.Security.AuthorizeValidation :: " + methodName);
            }

        }*/

    }
}
