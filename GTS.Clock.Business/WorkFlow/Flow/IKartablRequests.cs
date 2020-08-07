using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// کارتابل مدیر
    /// </summary>
    public interface IKartablRequests
    {
        int GetRequestCount(RequestType requestType, int year, int month);

        int GetRequestCount(string searchKey, int year, int month);

        IList<KartablProxy> GetAllRequests(RequestType requestType, int year, int month, int pageIndex, int pageSize,KartablOrderBy orderby);

        IList<ContractKartablProxy> GetAllRequests(decimal personId);

        IList<KartablProxy> GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby);

        bool SetStatusOfRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description);

        KartablRequestHistoryProxy GetRequestHistory(decimal requestId);

        IList<KartablFlowLevelProxy> GetRequestLevelsByManagerFlowID(decimal requestId, decimal managerFlowId);

        IList<KartablFlowLevelProxy> GetRequestLevelsByPersonnelID(decimal requestId, decimal personnelID);

        int GetRequestsByFilterCount(IList<RequestFliterProxy> fliters);

        IList<KartablProxy> GetAllRequestsByFilter(IList<RequestFliterProxy> fliters, int pageIndex, int pageSize, KartablOrderBy orderby);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckKartableLoadAccess();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool ConfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool UnconfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description);

    }
}
