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
    public interface IOperatorRegisteredRequests
    {
        /// <summary>
        /// تعداد درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="requestStatus"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int GetRequestCount(decimal personId, decimal userId, RequestState requestStatus, DateTime fromDate, DateTime toDate);

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
        IList<InfoRequest> GetAllRequests(decimal personId, decimal userId, RequestState requestState, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize);

        int GetRequestCountByFilter(decimal personId, decimal userId, decimal undermanagmentPersonId, RequestType? requestType, RequestSubmiter? submiter, DateTime? fromDate, DateTime? toDate);

        IList<InfoRequest> GetAllRequestsByFilter(decimal personId, decimal userId, decimal undermanagmentPersonId, RequestType? requestType, RequestSubmiter? submiter, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
    }
}
