using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// درخواستهای ثبت شده
    /// </summary>
    public interface IUserRegisteredRequests
    {
        /// <summary>
        /// تعداد درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="requestStatus"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int GetRequestCount(decimal personId, RequestState requestStatus, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="requestState"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<InfoRequest> GetAllRequests(decimal personId, RequestState requestState, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize);

        IList<InfoRequest> GetAllRequests(decimal personId, RequestState requestState, DateTime fromDate, DateTime toDate);

        int GetRequestCountByFilter(decimal personId, RequestType? requestType,RequestSubmiter? submiter ,DateTime? fromDate, DateTime? toDate);

        IList<InfoRequest> GetAllRequestsByFilter(decimal personId, RequestType? requestType,RequestSubmiter? submiter ,DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
    }
}
