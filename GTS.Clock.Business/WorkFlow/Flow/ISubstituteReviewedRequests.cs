using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// درخواستهای ثبت شده
    /// </summary>
    public interface ISubstituteReviewedRequests
    {
        /// <summary>
        /// تعداد درخواستهای بررسی شده یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requestStatus"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int GetRequestCount(decimal managerId, decimal substitutePersonId, RequestState requestStatus, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// تعداد درخواستهای بررسی شده یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requestStatus"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int GetRequestCount(decimal managerId, decimal substitutePersonId, IList<Person> quickSearchUnderMnagment, DateTime fromDate, DateTime toDate);

        IList<InfoRequest> GetAllRequests(decimal managerId, decimal substitutePersonId, IList<Person> quickSearchUnderMnagment, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby);

        /// <summary>
        /// درخواستهای بررسی شده یک مدیر را یرمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requestState"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<InfoRequest> GetAllRequests(decimal managerId, decimal substitutePersonId, RequestState requestState, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby);
    }
}
