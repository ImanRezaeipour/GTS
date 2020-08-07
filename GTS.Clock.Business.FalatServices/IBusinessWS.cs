using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using GTS.Clock.Infrastructure;
using System.Web.Services.Protocols;
using GTS.Clock.Business.Proxy;
using System.Net.Security;
using System.Security.Permissions;

namespace GTS.Clock.Business.BusinessService
{
    [ServiceContract()]
    public interface IBusinessWS
    {       
       
        [OperationContract]
        IList<ProceedTrafficProxy> GetClientTraffic(string clientUsername, DateTime fromDate, DateTime toDate);
       
        [OperationContract]
        IList<ContractKartablProxy> GetClientRequests(string clientUsername, DateTime fromDate, DateTime toDate);

        [OperationContract]
        IList<ContractKartablProxy> GetManagerKartabl(string clientUsername);



    }
}
