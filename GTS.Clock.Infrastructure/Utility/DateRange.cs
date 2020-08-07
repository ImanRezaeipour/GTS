using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GTS.Clock.Infrastructure.Utility
{
    /// <summary>
    /// از این کلاس برای نگهداری یک محدوده ی تاریخ استفاده می شود
    /// با استفاده از این کلاس می توان بین دو تاریخ "گردش" نمود
    /// </summary>
    public class DateRange : IEnumerable
    {
        #region

        private DateTime fromDate;
        private DateTime toDate;
        private DateTime beginOfYear;
        private DateTime endOfYear;

        #endregion

        #region Constructor

        public DateRange()
            :this(DateTime.MinValue, DateTime.MaxValue,1)
        { 
        
        }

        public DateRange(DateTime FromDate, DateTime ToDate,int daterangeOrder)
        {
            this.DateRangeOrder = daterangeOrder;
            if (FromDate <= ToDate)
            {
                this.fromDate = FromDate;
                this.toDate = ToDate;
            }
            else
            {
                throw new ArgumentOutOfRangeException(String.Format("مقدار تاریخ شروع از پایان بزرگتر است :FromDate {0} - ToDate {1}", FromDate, ToDate));
            }
        }

        public DateRange(DateTime FromDate, DateTime ToDate, DateTime BeginOfYear, DateTime EndOfYear)
            :this(FromDate, ToDate,0)
        {
            if (BeginOfYear <= EndOfYear)
            {
                this.beginOfYear = BeginOfYear;
                this.endOfYear = EndOfYear;
            }
            else
            {
                throw new ArgumentOutOfRangeException(String.Format("مقدار تاریخ شروع سال از پایان بزرگتر است :FromDate {0} - ToDate {1}", BeginOfYear, EndOfYear));
            }
        }

        #endregion

        #region Properties
       
        public decimal ID { get; set; }
       
        public DateTime FromDate
        {
            get
            {
                return this.fromDate;
            }
            set
            {
                if (this.toDate <= value)
                    throw new ArgumentOutOfRangeException(String.Format("مقدار تاریخ شروع از پایان بزرگتر است :FromDate {0} - ToDate {1}", fromDate, toDate));
                this.fromDate = value;
            }
        }

        public DateTime ToDate
        {
            get
            {
                return this.toDate;
            }
            set
            {
                if (this.fromDate >= value)
                    throw new ArgumentOutOfRangeException(String.Format("مقدار تاریخ پایان از شروع کوچکتر است :FromDate {0} - ToDate {1}", fromDate, toDate));
                this.toDate = value;
            }
        }

        public int DateRangeOrder { get; set; }

        public DateTime BeginOfYear
        {
            get
            {
                return this.beginOfYear;
            }
            set
            {
                if (this.endOfYear <= value)
                    throw new ArgumentOutOfRangeException(String.Format("مقدار تاریخ شروع از پایان بزرگتر است :BeginOfYear {0} - EndOfYear {1}", beginOfYear, endOfYear));
                this.beginOfYear = value;
            }
        }

        public DateTime EndOfYear
        {
            get
            {
                return this.endOfYear;
            }
            set
            {
                if (this.beginOfYear >= value)
                    throw new ArgumentOutOfRangeException(String.Format("مقدار تاریخ پایان سال از شروع کوچکتر است :BeginOfYear {0} - EndOfYear {1}", beginOfYear, endOfYear));
                this.endOfYear = value;
            }
        }

        /// <summary>
        /// وظیفه بررسی وجود تاریخ ارسالی در بازه این کلاس بعهده این تابع می باشد
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns>اگر تاریخ ارسالی در بازه نگهداری شده توسط کلاس باشد "درست" وگرنه "نا درست" برمی گرداند</returns>
        public bool IsContain(PersianDateTime DateTime)
        {
            if (this.fromDate.Date <= DateTime.GregorianDate.Date && this.toDate.Date >= DateTime.GregorianDate.Date)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// وظیفه بررسی وجود تاریخ ارسالی در بازه این کلاس بعهده این تابع می باشد
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns>اگر تاریخ ارسالی در بازه نگهداری شده توسط کلاس باشد "درست" وگرنه "نا درست" برمی گرداند</returns>
        public bool IsContain(DateTime DateTime)
        {
            if (this.fromDate.Date <= DateTime.Date && this.toDate.Date >= DateTime.Date)
            {
                return true;
            }
            return false;
        }

        #endregion
         
        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            DateTime temp = FromDate;
            while (temp.Date <= ToDate.Date)
            {
                yield return temp.Date;
                temp = temp.AddDays(1);
            }
            

        }

        #endregion

    }
}
