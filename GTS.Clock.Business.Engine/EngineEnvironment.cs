using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using System.Collections;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.ELE;
using GTS.Clock.Model;
using GTS.Clock.Business.TrafficMapping;
using GTS.Clock.Infrastructure.Exceptions.UI;

namespace GTS.Clock.Business.Engine
{
    public class EngineEnvironment : IEngineEnvironment
    {
        #region Constructor

        public EngineEnvironment()
        {
            this.ConceptList = new Dictionary<decimal, SecondaryConcept>();
            foreach (SecondaryConcept ScndCnp in SecondaryConcept.GetRepository(false).GetAll())
            {
                this.ConceptList.Add(ScndCnp.IdentifierCode, ScndCnp);
            }
            this.PrecardDic = new Dictionary<string, Precard>();
            IList<Precard> list = Precard.GetPrecardRepository(false).GetAll();            
            foreach (int precardCode in Enum.GetValues(typeof(Precards)))
            {
                Precard p = list.Where(x => x.Code == precardCode.ToString()).FirstOrDefault();
                if (p != null)
                {
                    this.PrecardDic.Add(Enum.Parse(typeof(Precards), precardCode.ToString()).ToString(), p);
                }
                //else
                //{
                //    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ExpectedPrecardDoesNotExists, String.Format("پیشکارت با کد {0} در پایگاه داد یافت نشد", precardCode.ToString()), "GTS.Clock.Business.Engine");
                //}
            }
            this.CalendarList = Calendar.GetRepository(false).GetAll();
        }

        #endregion

        #region Properties

        public virtual IList<Calendar> CalendarList
        {
            get;
            set;
        }

        public virtual IDictionary<decimal, SecondaryConcept> ConceptList
        {
            get;
            set;
        }

        public IDictionary<string, Precard> PrecardDic 
        {
        get;
            set;
        }

        public virtual AssignedRule AssignedRule
        {
            get;
            set;
        }

        public virtual Person Person
        {
            get;
            set;

        }

        public virtual DateTime ConceptCalculateDate
        {
            get;
            set;
        }

        public virtual DateTime RuleCalculateDate
        {
            get;
            set;
        }

        public virtual DateRange CalcDateZone
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// لیست تمام تقویم های تاریخ مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که تقویم های آن مدنظر است</param>
        /// <returns>لیستی از کلاس تقویم که شامل تمام تقویم های پرسنل در تاریخ مشخص شده می باشد</returns>
        public virtual IList<Calendar> GetCalendarByDate(DateTime Date)
        {
            return this.CalendarList
                        .Where(x => x.Date == Date)
                        .ToList();
        }

        /// <summary>
        /// نتیجه ی بررسی وجود تقویم مشخص شده در روز مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که در آن نام تقویم جستجو می شود</param>
        /// <param name="CalendarCode">کد تعریف شده تقویم مورد نظر</param>
        /// <returns>درست یا غلط</returns>
        public virtual bool HasCalendar(DateTime Date, string CalendarCode)
        {
            return (this.CalendarList
                        .Where(x => x.CalendarType.CustomCode == CalendarCode &&
                                    x.Date == Date)
                                    .FirstOrDefault() == null) ? false : true;
        }


        /// <summary>
        /// نتیجه ی بررسی وجود تقویم های مشخص شده در روز مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که در آن نام تقویم جستجو می شود. این تاریخ به صورت شمسی دریافت می شود ولی از مقدار میلادی آن استفاده می گردد</param>
        /// <param name="CalendarId">کد تعریف شده تقویم مورد نظر</param>
        /// <returns>درست یا غلط</returns>
        public virtual bool HasCalendar(DateTime Date, params string[] CalendarsId)
        {
            return (this.CalendarList
                        .Where(x => CalendarsId.Contains(x.CalendarType.CustomCode) &&
                                    x.Date == Date)
                                    .FirstOrDefault() == null) ? false : true;
        }


        /// <summary>
        /// تقویمی که نام و تاریخ آن مشخص شده است را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که تقویم آن مدنظر است</param>
        /// <param name="CalendarName">نام تقویم مورد نظر</param>
        /// <returns>یک نمونه از کلاس تقویم که شامل تقویم درخواست شده می باشد. در صورت نبود تقویم مورد نظر یک نمونه ی خالی از کلاس تقویم برگشت داده می شود</returns>
        public virtual Calendar GetCalendarByhDate(DateTime Date, string CalendarName)
        {
            return this.CalendarList
                        .Where(x => x.CalendarType.Name == CalendarName && x.Date == Date)
                        .FirstOrDefault() ?? new Calendar();
        }


        /// <summary>
        /// مفهوم ثانویه با شناسه مورد نظر را برمی گرداند
        /// </summary>
        /// <param name="IdentifierCode">شناسه مفهوم مورد نظر</param>
        public virtual SecondaryConcept GetConcept(decimal IdentifierCode)
        {
            if (this.ConceptList.ContainsKey(IdentifierCode))
            {
                return (SecondaryConcept)this.ConceptList[IdentifierCode];
            }
            else 
            {
                return null;
            }
        }

        /// <summary>
        ///  پیشکارت را از دیکشنری استخراج میکند
        /// </summary>
        /// <param name="precard"></param>
        /// <returns></returns>
        public Precard GetPrecard(Precards precards)
        {
            Precard Result;
            this.PrecardDic.TryGetValue(Utility.ToString(precards), out Result);
            return Result;
        }

        /// <summary>
        ///  پیشکارت را از دیکشنری استخراج میکند
        /// </summary>
        /// <param name="precard"></param>
        /// <returns></returns>
        public Precard GetPrecard(int precardCode)
        {
            IList<Precard> list = Precard.GetPrecardRepository(false).GetAll();
            Precard Result = list.Where(x => x.Code == precardCode.ToString()).FirstOrDefault();
            if (Result == null) 
            {
                Result = new Precard();
            }
            return Result;
        }
        #endregion

    }
}


