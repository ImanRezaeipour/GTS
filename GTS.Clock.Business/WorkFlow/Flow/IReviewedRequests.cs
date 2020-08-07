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
    /// درخواستهای بررسی شده
    /// مربوط به مدیر میباشد
    /// </summary>
    public interface IReviewedRequests
    {
        /// <summary>
        /// تعداد درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int GetRequestCount(RequestState requestState, int year, int month);

        /// <summary>
        /// تعداد درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int GetRequestCount(string searchKey, int year, int month);

        /// <summary>
        /// درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby);


        /// <summary>
        /// درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> GetAllRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby);

        /// <summary>
        /// حذف یک درخواست توسط مدیر
        /// </summary>
        /// <param name="requestId"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void DeleteRequst(decimal requestId, string managerDescription);
   
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckSurveyedRequestsLoadAccess();
    }
}
