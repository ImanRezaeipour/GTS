using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using System.Collections;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.ELE
{
    public interface IEngineEnvironment
    {
        IList<Calendar> CalendarList
        {
            get;
            set;
        }

        IDictionary<decimal, SecondaryConcept> ConceptList
        {
            get;
            set;
        }

        AssignedRule AssignedRule
        {
            get;
            set;
        }

        Person Person
        {
            get;
            set;

        }

        DateTime ConceptCalculateDate
        {
            get;
            set;
        }

        DateTime RuleCalculateDate
        {
            get;
            set;
        }

        DateRange CalcDateZone
        {
            get;
            set;
        }

        /// <summary>
        /// لیست تمام تقویم های تاریخ مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که تقویم های آن مدنظر است</param>
        /// <returns>لیستی از کلاس تقویم که شامل تمام تقویم های پرسنل در تاریخ مشخص شده می باشد</returns>
        IList<Calendar> GetCalendarByDate(DateTime Date);

        /// <summary>
        /// نتیجه ی بررسی وجود تقویم مشخص شده در روز مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که در آن نام تقویم جستجو می شود</param>
        /// <param name="CalendarCode">کد تعریف شده تقویم مورد نظر</param>
        /// <returns>درست یا غلط</returns>
        bool HasCalendar(DateTime Date, string CalendarCode);

        /// <summary>
        /// نتیجه ی بررسی وجود تقویم های مشخص شده در روز مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که در آن نام تقویم جستجو می شود.
        /// این تاریخ به صورت شمسی دریافت می شود ولی از مقدار میلادی آن استفاده می گردد</param>
        /// <param name="CalendarId">کد تعریف شده تقویم مورد نظر</param>
        /// <returns>درست یا غلط</returns>
        bool HasCalendar(DateTime Date, params string[] CalendarsId);

        /// <summary>
        /// تقویمی که نام و تاریخ آن مشخص شده است را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که تقویم آن مدنظر است</param>
        /// <param name="CalendarName">نام تقویم مورد نظر</param>
        /// <returns>یک نمونه از کلاس تقویم که شامل تقویم درخواست شده می باشد. در صورت نبود تقویم مورد نظر یک نمونه ی خالی از کلاس تقویم برگشت داده می شود</returns>
        Calendar GetCalendarByhDate(DateTime Date, string CalendarName);

        /// <summary>
        /// مفهوم ثانویه با شناسه مورد نظر را برمی گرداند
        /// </summary>
        /// <param name="IdentifierCode">شناسه مفهوم مورد نظر</param>
        SecondaryConcept GetConcept(decimal IdentifierCode);

        /// <summary>
        /// پیشکارت را از دیکشنری استخراج میکند
        /// </summary>
        /// <param name="precard"></param>
        /// <returns></returns>
        Precard GetPrecard(Precards precards);

        /// <summary>
        /// پیشکارت را از دیکشنری استخراج میکند
        /// </summary>
        /// <param name="precard"></param>
        /// <returns></returns>
        Precard GetPrecard(int precardCode);
    }
}


