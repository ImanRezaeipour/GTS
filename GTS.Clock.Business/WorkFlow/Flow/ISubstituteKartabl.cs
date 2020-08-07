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
    public interface ISubstituteKartabl
    {
        /// <summary>
        /// تعداد درخواستهای کارتابل مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requestType"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        //int GetRequestCount(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate);

        //int GetRequestCount(decimal managerId, decimal personId, IList<Person> quickSearchUnderMnagment, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// جهت نمایش در خلاصه کارتابل
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="personId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int GetRequestCount(decimal managerId, decimal personId, DateTime fromDate, DateTime toDate);

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
        //IList<InfoRequest> GetAllRequests(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby);

        //IList<InfoRequest> GetAllRequests(decimal managerId, decimal personId, IList<Person> quickSearchUnderMnagment, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby);

        //IList<InfoRequest> GetAllRequests(decimal managerId, decimal personId);



        //int GetRequestCountByFilter(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate, IList<RequestFliterProxy> fliters);

        //IList<InfoRequest> GetAllRequestsByFilter(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, IList<RequestFliterProxy> fliters, KartablOrderBy orderby);
    }
}
