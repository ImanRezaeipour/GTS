using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Diagnostics;
using System.Threading;
using GTS.Clock.Business.Engine;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using System.Web.Services;
using GTS.Clock.Business.BaseInformation;

namespace GTS.Clock.Business.BusinessService
{

    [AspNetCompatibilityRequirements(RequirementsMode =
        AspNetCompatibilityRequirementsMode.Required)]
    public class BusinessWS : IBusinessWS
    {
        private const string WebserivceResourceKey = "WSAccessKey";
        GTSWinSvcLogger logger = new GTSWinSvcLogger();

        #region IBusinessWS Members
     

        /// <summary>
        /// ترددهای یک کاربر را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ProceedTrafficProxy> GetClientTraffic(string clientUsername, DateTime fromDate, DateTime toDate)
        {
            BUser busUser = new BUser();
            decimal prsId =  busUser.GetPersonIdByUsername(clientUsername);
            IList<ProceedTrafficProxy> proxyList = new List<ProceedTrafficProxy>();
            if (prsId > 0 && fromDate > Utility.GTSMinStandardDateTime && toDate > Utility.GTSMinStandardDateTime)            
            {
                proxyList = new BTraffic().GetAllTrafic(prsId, fromDate, toDate);
            }
            else if (prsId <= 0)
                throw new Exception("نام کاربری یافت نشد");
            else
                throw new Exception("فرمت تاریخ نادرست است");          
            return proxyList;      
        }

        /// <summary>
        /// درخواستها را برمیگرداند
        /// </summary>
        /// <param name="clientUsername"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<ContractKartablProxy> GetClientRequests(string clientUsername, DateTime fromDate, DateTime toDate)
        {
            BUser busUser = new BUser();
            decimal prsId = busUser.GetPersonIdByUsername(clientUsername);

            IRegisteredRequests kartabl = new BKartabl();
            IList<ContractKartablProxy> list = new List<ContractKartablProxy>();
            if (prsId > 0 && fromDate > Utility.GTSMinStandardDateTime && toDate > Utility.GTSMinStandardDateTime)
            {
                list = kartabl.GetAllUserRequests(RequestState.UnKnown, fromDate, toDate, prsId);
            }
            else if (prsId <= 0)
                throw new Exception("نام کاربری یافت نشد");
            else
                throw new Exception("فرمت تاریخ نادرست است");
            return list;

        }

        /// <summary>
        /// کارتابل مدیر را برمیگرداند
        /// </summary>
        /// <param name="clientUsername"></param>
        /// <returns></returns>
        public IList<ContractKartablProxy> GetManagerKartabl(string clientUsername)
        {
            BUser busUser = new BUser();
            decimal prsId = busUser.GetPersonIdByUsername(clientUsername);

            IKartablRequests kartabl = new BKartabl();
            IList<ContractKartablProxy> list = new List<ContractKartablProxy>();
            if (prsId > 0)
            {
                list = kartabl.GetAllRequests(prsId);
            }
            else if (prsId <= 0)
                throw new Exception("نام کاربری یافت نشد");
            
            return list;
        }

        #endregion


        #region private
        /*
        /// <summary>
        /// Check Client Contact Key + Authentication by user pass + Authorization
        /// </summary>
        /// <returns></returns>
        public bool IsAuthorizeToAccess()
        {
            if (loginInfo != null)
            {
                bool verifyKey = false;
                for (int min = 0; min < 3; min++)
                {
                    DateTime dt = DateTime.Now.AddMinutes(min);
                    string wskey = dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
                    string clientKey = loginInfo.Username.ToLower() + "*" + wskey;
                    if (Utility.VerifyHashCode(clientKey, loginInfo.ClientContactKey))
                    {
                        verifyKey = true;
                        break;
                    }
                }
                if (verifyKey)
                {
                    BLogin securitySerivce = new BLogin();
                    bool isAuthenticate = securitySerivce.IsAuthenticate(loginInfo.Username, loginInfo.Password);
                    BRole busRole = new BRole();
                    if (busRole.HasAccessToResource(loginInfo.Username, WebserivceResourceKey))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        */
        #endregion


    }
}
