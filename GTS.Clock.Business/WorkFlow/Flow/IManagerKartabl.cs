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
    /// کارتابل مدیر
    /// </summary>
    public interface IManagerKartabl
    {
        /// <summary>
        /// تعداد درخواستهای کارتابل مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requestType"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int GetRequestCount(decimal managerId, RequestType requestType, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// تعداد درخواستهای کارتابل مدیر در جستجوی سریع
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="quickSearchUnderMnagmentIds"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int GetRequestCount(decimal managerId, string searchKey, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// درخواستهای کارتابل مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requestType"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<InfoRequest> GetAllRequests(decimal managerId, RequestType requestType, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby);

        /// <summary>
        /// درخواستهای کارتابل مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requestType"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<InfoRequest> GetAllRequests(decimal managerId);

        /// <summary>
        /// درخواستهای کارتابل مدیر در جستجوی سریع را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="quickSearchUnderMnagmentIds"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<InfoRequest> GetAllRequests(decimal managerId, string searchKey, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby);

     }
}
