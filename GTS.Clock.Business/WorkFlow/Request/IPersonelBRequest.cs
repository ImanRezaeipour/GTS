using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// جهت نمایش خلاصه در صفحه اول
    /// </summary>
    public interface IDashboardBRequest
    {
        int GetAllRequestCount(int year, int month);

        int GetAllRequestCount(int year, int month, RequestState state);

        int GetAllRequestCount(int year, RequestState state);

        /// <summary>
        /// کاربرانی که تاحالا برای این شخص درخواست ثبت کرده اند را برمیگرداند
        /// </summary>
        /// <returns></returns>
        int GetAllUserInRequestCount();

        ///// <summary>
        ///// درخواستها را یلتر میکند
        ///// </summary>
        ///// <param name="requestType"></param>
        ///// <param name="userId">اگر -1 باشد در فیلتر اثر تمیگزارد</param>
        ///// <param name="fromDate">تاریخ شروع میلادی</param>
        ///// <param name="toDate">تاریخ پایان میلادی</param>
        ///// <returns></returns>
        //IList<Request> FilterRequest(RequestType requestType, decimal userId, DateTime fromDate, DateTime toDate);

    }
}
