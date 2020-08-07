using System;
using System.Globalization;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Infrastructure.Utility
{
    public class PersianDateTime
    {
        #region Variables

        private int year;
        private int month;
        private int day;
        private int hour;
        private int minute;
        private PersianDateTime _persianDateTime;

        private DateTime gregorianDate;

        private PersianCalendar pc = new PersianCalendar();

        private static string Shanbeh = "شنبه";
        private static string Yekshanbeh = "یکشنبه";
        private static string Doshanbeh = "دوشنبه";
        private static string Seshanbeh = "ﺳﻪشنبه";
        private static string Chaharshanbeh = "چهارشنبه";
        private static string Panjshanbeh = "پنجشنبه";
        private static string Jomeh = "جمعه";
        private static int[,] jdaytable = new int[,] { { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 }, { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 30 } };


        private static PersianDateTime now;

        private static PersianDateTime MinValue;
        private static PersianDateTime MaxValue;

        #endregion

        #region Constructors

        static PersianDateTime()
        {
            now = new PersianDateTime(DateTime.Now);
            MinValue = new PersianDateTime(1, 1, 1, 12, 0); // 12:00:00.000 AM, 22/03/0622
            MaxValue = new PersianDateTime(DateTime.MaxValue);
        }

        public PersianDateTime(DateTime GregorianDate)
        {
            this.year = pc.GetYear(GregorianDate);
            this.month = pc.GetMonth(GregorianDate);
            this.day = pc.GetDayOfMonth(GregorianDate);
            this.hour = pc.GetHour(GregorianDate);
            this.minute = pc.GetMinute(GregorianDate);
            this.gregorianDate = GregorianDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year">سال شمسی</param>
        /// <param name="Month">ماه شمسی</param>
        /// <param name="Day">روز شمسی</param>
        public PersianDateTime(int Year, int Month, int Day)
        {
            CheckYear(Year);
            CheckMonth(Month);
            CheckDay(Year, Month, Day);

            this.year = Year;
            this.month = Month;
            this.day = Day;
            this.hour = 0;
            this.minute = 0;
            this.gregorianDate = pc.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year">سال شمسی</param>
        /// <param name="Month">ماه شمسی</param>
        /// <param name="Day">روز شمسی</param>
        /// <param name="Hour"></param>
        /// <param name="Minute"></param>
        public PersianDateTime(int Year, int Month, int Day, int Hour, int Minute)
        {
            CheckYear(Year);
            CheckMonth(Month);
            CheckDay(Year, Month, Day);
            CheckHour(Hour);
            CheckMinute(Minute);

            this.year = Year;
            this.month = Month;
            this.day = Day;
            this.hour = Hour;
            this.minute = Minute;

            this.gregorianDate = pc.ToDateTime(Year, Month, Day, Hour, Minute, 0, 0);
        }

        #endregion

        #region Properties

        public int Year
        {
            get
            {
                return this.year;
            }
        }

        public int Month
        {
            get
            {
                return this.month;
            }
        }

        public int Day
        {
            get
            {
                return this.day;
            }
        }

        public DateTime GregorianDate
        {
            get
            {
                return this.gregorianDate;
            }
        }

        public DateTime NextMonthGregorianDate
        {
            get
            {
                return this.gregorianDate.AddMonths(1);
            }
        }

        public string PersianDate
        {
            get
            {
                return String.Format("{0}/{1}/{2}", this.year.ToString().PadLeft(4, '0'), this.month.ToString().PadLeft(2, '0'), this.day.ToString().PadLeft(2, '0'));
            }
        }

        public PersianDateTime Tomorrow
        {
            get
            {               
                _persianDateTime = this.AddDays(1);
                return _persianDateTime;
            }
        }

        public PersianDateTime Yesterday
        {
            get
            {               
                _persianDateTime = this.SubtractDays(1);
                return _persianDateTime;
            }
        }

        public PersianCalendar PersianCalendar
        {
            get
            {
                return this.pc;
            }
        }

        #endregion

        #region Private Check Methods

        private void CheckYear(int YearNo)
        {
            if (YearNo < 1 || YearNo > 9999)
            {
                throw new InvalidPersianDateException(String.Format("مقدار {0} برای سال نامعتبر است", YearNo), "PersianDateTime.CheckYear()");
            }
        }

        private void CheckMonth(int MonthNo)
        {
            if (MonthNo > 12 || MonthNo < 1)
            {
                throw new InvalidPersianDateException(String.Format("مقدار {0} برای ماه نامعتبر است", MonthNo), "PersianDateTime.CheckMonth()");
            }
        }

        private void CheckDay(int YearNo, int MonthNo, int DayNo)
        {
            if (MonthNo < 6 && DayNo > 31)
                throw new InvalidPersianDateException(String.Format("مقدار {0} برای روز در 6 ماه اول سال نامعتبر است", DayNo), "PersianDateTime.CheckDay()");

            if (MonthNo > 6 && DayNo > 30)
                throw new InvalidPersianDateException(String.Format("مقدار {0} برای روز در 6 ماه دوم سال نامعتبر است", DayNo), "PersianDateTime.CheckDay()");

            if (MonthNo == 12 && DayNo > 29)
            {
                if (!pc.IsLeapDay(YearNo, MonthNo, DayNo) || DayNo > 30)
                    throw new InvalidPersianDateException(String.Format("مقدار {0} برای روز در سال ها کبیسه نامعتبر است", DayNo), "PersianDateTime.CheckDay()");
            }
        }

        private void CheckHour(int HourNo)
        {
            if (HourNo > 24 || HourNo < 0)
            {
                throw new InvalidPersianDateException(String.Format("مقدار {0} برای ساعت نامعتبر است", HourNo), "PersianDateTime.CheckDay()");
            }
        }

        private void CheckMinute(int MinuteNo)
        {
            if (MinuteNo > 60 || MinuteNo < 0)
            {
                throw new InvalidPersianDateException(String.Format("مقدار {0} برای دقیقه نامعتبر است", MinuteNo), "PersianDateTime.CheckDay()");
            }
        }
        #endregion

        #region Methods

        public bool IsMatchDayOfWeek(DayOfWeek dayOfWeek)
        {
            return this.gregorianDate.DayOfWeek == dayOfWeek;
        }

        public PersianDateTime AddDays(int Value)
        {
            DateTime gregorianDate = this.gregorianDate.AddDays(Convert.ToDouble(Value));           
            _persianDateTime = new PersianDateTime(gregorianDate);
            return _persianDateTime;
        }

        public PersianDateTime AddMonths(int Value)
        {          
            DateTime gregorianDate = this.gregorianDate.AddMonths(Value);                          
            _persianDateTime = new PersianDateTime(gregorianDate);
            return _persianDateTime;
        }

        public PersianDateTime AddYears(int Value)
        {
            DateTime gregorianDate = this.gregorianDate.AddYears(Value);
            _persianDateTime = new PersianDateTime(gregorianDate);
            return _persianDateTime;
        }

        /// <summary>
        /// ابتدای ماه بعد
        /// </summary>
        /// <returns></returns>
        public PersianDateTime NextMonthStart()
        {    
                  
            int _day = 1;
            int _month = this.month + 1;
            if (_month > 12) 
            {
                _month = 1;
                this.year += 1;
            }
            _persianDateTime = new PersianDateTime(this.year, _month, _day);

            return _persianDateTime;
        }

        public PersianDateTime SubtractDays(int Value)
        {
            _persianDateTime= this.AddDays(-Value);          
            return _persianDateTime;
        }

        public override string ToString()
        {
            return String.Format("{0}/{1}/{2} {3}:{4}", this.year, this.month, this.day, this.hour, this.minute);
        }


        #endregion

        #region Static Methods

        /// <summary>
        /// یک رشته را برای تبدیل به یک نمونه از کلاس "تاریخ فارسی" پردازش می کند
        /// </summary>
        /// <exception cref="InvalidPersianDateException"></exception>
        /// <param name="value">رشته تاریخ مورد نظر</param>
        /// <param name="includesTime"></param>
        /// <returns></returns>
        public static PersianDateTime Parse(string Value, bool IncludesTime)
        {
            if (Value == string.Empty)
                return MinValue;

            if (IncludesTime)
            {
                if (Value.Length > 19)
                {
                    throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                            , String.Format("PersianCalendar.Pars({0}, {1})", Value, IncludesTime), Value);
                }

                string[] dt = Value.Split(" ".ToCharArray());

                if (dt.Length != 2)
                {
                    throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                            , String.Format("PersianCalendar.Pars({0}, {1})", Value, IncludesTime), Value);
                }

                string _date = dt[0];
                string _time = dt[1];

                string[] dateParts = _date.Split("/".ToCharArray());
                string[] timeParts = _time.Split(":".ToCharArray());

                if (dateParts.Length != 3)
                {
                    throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                            , String.Format("PersianCalendar.Pars({0}, {1})", Value, IncludesTime), Value);
                }

                if (timeParts.Length != 2)
                {
                    throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                            , String.Format("PersianCalendar.Pars({0}, {1})", Value, IncludesTime), Value);
                }

                int day = int.Parse(dateParts[2]);
                int month = int.Parse(dateParts[1]);
                int year = int.Parse(dateParts[0]);
                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);

                return new PersianDateTime(year, month, day, hour, minute);
            }
            else
            {
                return Parse(Value);
            }
        }

        /// <summary>
        /// یک رشته را برای تبدیل به یک نمونه از کلاس "تاریخ فارسی" پردازش می کند
        /// </summary>
        /// <exception cref="InvalidPersianDateException"></exception>
        /// <param name="value">رشته تاریخ مورد نظر</param>
        /// <param name="includesTime"></param>
        /// <returns></returns>
        public static PersianDateTime Parse(string Value, string Format)
        {
            switch (Format)
            {
                case "g": //yyyy/mm/dd hh:mm tt
                    return ParseDateShortTime(Value);

                case "d": //yyyy/mm/dd
                    return Parse(Value);

                default:
                    throw new BaseException("", String.Format("PesianDatTime.Pars({0}, {1})", Value, Format), new ArgumentException("Currently g,d formats are supported."));
            }
        }

        private static PersianDateTime ParseDateShortTime(string Value)
        {
            if (Value == string.Empty)
                return MinValue;

            if (Value.Length > 20)
                throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                        , String.Format("PersianCalendar.ParseDateShortTime({0})", Value));

            string[] dt = Value.Split(" ".ToCharArray());

            if (dt.Length != 3)
                throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                        , String.Format("PersianCalendar.ParseDateShortTime({0})", Value));


            string _date = dt[0];
            string _time = dt[1];

            string[] dateParts = _date.Split("/".ToCharArray());
            string[] timeParts = _time.Split(":".ToCharArray());

            if (dateParts.Length != 3)
                throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                        , String.Format("PersianCalendar.ParseDateShortTime({0})", Value));


            if (timeParts.Length != 2)
                throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                        , String.Format("PersianCalendar.ParseDateShortTime({0})", Value));


            int day = int.Parse(dateParts[2]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[0]);
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            return new PersianDateTime(year, month, day, hour, minute);
        }

        /// <summary>
        /// یک رشته را برای تبدیل به یک نمونه از کلاس "تاریخ فارسی" پردازش می کند
        /// </summary>
        /// <exception cref="InvalidPersianDateException"></exception>
        /// <param name="value">رشته تاریخ مورد نظر</param>
        /// <param name="includesTime"></param>
        /// <returns></returns>
        public static PersianDateTime Parse(string Value)
        {
            if (Value.Length == 10)
                return ParseShortDate(Value);
            else if (Value.Length == 20)
                return ParseDateShortTime(Value);

            throw new InvalidPersianDateException(String.Format("فرمت مقدار تاریخ {0} نامعتبر است", Value)
                                                    , String.Format("PersianCalendar.Parse({0})", Value), Value);
        }

        private static PersianDateTime ParseShortDate(string Value)
        {
            if (Value == string.Empty)
                return MinValue;

            if (Value.Length > 10)
            {
                throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                        , String.Format("PersianCalendar.ParseShortDate({0})", Value));
            }

            string[] dateParts = Value.Split("/".ToCharArray());

            if (dateParts.Length != 3)
            {
                throw new InvalidPersianDateException(String.Format("مقدار تاریخ {0} نامعتبر است", Value)
                                                        , String.Format("PersianCalendar.ParseShortDate({0})", Value));
            }

            int day = int.Parse(dateParts[2]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[0]);

            return new PersianDateTime(year, month, day);
        }

        public static PersianDateTime ToPersianDateTime(DateTime GregorianDate)
        {
            return new PersianDateTime(GregorianDate);
        }

        public static DateTime GetGregorianNow()
        {            
            return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        }

        public static DayOfWeek GetDayOfWeek(DateTime GregorianDate)
        {
            return (new PersianCalendar()).GetDayOfWeek(GregorianDate);
        }

        /// <summary>
        /// نام روز هفته به انگلیسی
        /// </summary>
        /// <param name="WeekDay"></param>
        /// <returns></returns>
        public static string GetEnglishDayName(DayOfWeek WeekDay)
        {
            return WeekDay.ToString("G");            
        }        
              
        public static string GetEnglishDayName(DateTime GregorianDate)
        {
            return GetEnglishDayName(PersianDateTime.GetDayOfWeek(GregorianDate));
        }

        /// <summary>
        /// نام روز هفته به فارسی
        /// </summary>
        /// <param name="WeekDay"></param>
        /// <returns></returns>
        public static string GetPershianDayName(DayOfWeek WeekDay)
        {
            switch (WeekDay)
            {
                case DayOfWeek.Saturday:
                    return Shanbeh;

                case DayOfWeek.Sunday:
                    return Yekshanbeh;

                case DayOfWeek.Monday:
                    return Doshanbeh;

                case DayOfWeek.Tuesday:
                    return Seshanbeh;

                case DayOfWeek.Wednesday:
                    return Chaharshanbeh;

                case DayOfWeek.Thursday:
                    return Panjshanbeh;

                case DayOfWeek.Friday:
                    return Jomeh;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetPershianDayName(DateTime GregorianDate)
        {
            return GetPershianDayName(PersianDateTime.GetDayOfWeek(GregorianDate));
        }

        public static int DateDifference(DateTime FromDate, DateTime ToDate)
        {
            if  (FromDate > ToDate)
                throw new BaseException("تاریخ شروع از پایان بزرگتر است", "DateDifference");
            if (FromDate == ToDate)
                return 1;
            TimeSpan span = ToDate.Subtract(FromDate);
            return span.Days + 1;
        }

        public static PersianDateTime GetEndOfShamsiMonth(int persianYear, int persianMonth)
        {
            PersianCalendar pc = new PersianCalendar();
            return new PersianDateTime(persianYear, persianMonth, pc.GetDaysInMonth(persianYear, persianMonth));
        }

        /// <summary>
        /// تاریخ انتهای سال شمسی را برمیگرداند
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>      
        /// <returns></returns>
        public static PersianDateTime GetEndOfShamsiYear(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            pc.GetYear(date);
            return new PersianDateTime(pc.GetYear(date), 12, pc.GetDaysInMonth(pc.GetYear(date), 12));
        }

        public static string MiladiToShamsi(string miladiDate) 
        {
            try
            {
                DateTime date = Convert.ToDateTime(miladiDate);

                PersianDateTime pDate = new PersianDateTime(date);

                return pDate.PersianDate;
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Sended date: {0} {1}", miladiDate, e.Message));
            }
        }

        public static string ShamsiToMiladi(string shamsiDate)
        {
            try
            {
                string[] strs = shamsiDate.Split('/');
                PersianCalendar pc = new PersianCalendar();
                if (strs[0].Length == 4)
                {
                    return pc.ToDateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]), 0, 0, 0, 0).ToString("yyyy/MM/dd");
                }
                else
                {
                    return pc.ToDateTime(Convert.ToInt32(strs[2]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[0]), 0, 0, 0, 0).ToString("yyyy/MM/dd");
                }
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Sended date: {0} {1}", shamsiDate, e.Message));
            }
        }

        #endregion

    }
}
