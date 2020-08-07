using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using GTS.Clock.Infrastructure.Exceptions;
using System.Globalization;

namespace GTS.Clock.Infrastructure.Utility
{
    /// <summary>
    ///  برای اتصال شروط در کوئری استفاده میشود 
    /// </summary>
    public enum ConditionOperations
    {
        AND, OR
    }

    /// <summary>
    ///  برای اتصال عملوندها در شروط استفاده میشود 
    /// </summary>
    public enum CriteriaOperation
    {
        Equal, NotEqual, GreaterThan, LessThan, GreaterEqThan, LessEqThan, Like, IsNull, IsNotNull, IS, IN
    }

    public struct CriteriaStruct
    {
        public string PropertyName;
        public object Value;
        public CriteriaOperation Operation;

        public CriteriaStruct(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
            Operation = CriteriaOperation.Equal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="operation">برای اتصال به عملوند قبلی استفاده میشود </param>
        public CriteriaStruct(string propertyName, object value, CriteriaOperation operation)
        {
            PropertyName = propertyName;
            Value = value;
            Operation = operation;
        }
    }

    public static class Utility
    {
        static PerformanceCounter counter = null;
        static int MinutPerDay = 1440;
        static int MinutePerHour = 60;

        #region Reflection
        /// <summary>
        /// نام متد جاری را برمیگرداند
        /// </summary>
        public static string CurrntMethodName
        {
            get
            {
                StackTrace st = new StackTrace();
                if (st.FrameCount > 0)
                {
                    StackFrame sf = st.GetFrame(1);

                    return sf.GetMethod().Name;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// نام متد جاری را برمیگرداند
        /// در ثبت رخداد استفاده میگردد
        /// </summary>
        public static string CallerMethodName
        {
            get
            {
                StackTrace st = new StackTrace();
                if (st.FrameCount > 1)
                {
                    StackFrame sf = st.GetFrame(2);

                    return sf.GetMethod().Name;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// نام کلاس جاری را برمیگرداند
        /// در ثبت رخداد استفاده میگردد
        /// </summary>
        public static string CallerCalassName
        {
            get
            {
                StackTrace st = new StackTrace();
                if (st.FrameCount > 1)
                {
                    StackFrame sf = st.GetFrame(2);

                    return sf.GetMethod().DeclaringType.Name;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// نام کامل کلاس جاری را برمیگرداند
        /// در ثبت رخداد استفاده میگردد
        /// </summary>
        public static string CallerCalassFullName
        {
            get
            {
                StackTrace st = new StackTrace();
                if (st.FrameCount > 1)
                {
                    StackFrame sf = st.GetFrame(2);

                    return sf.GetMethod().DeclaringType.FullName;
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        #endregion

        /// <summary>
        /// حداقل مقدار تاریخ جهت اعتبارسنجی تاریخ های ورودی در واسط کاربر
        /// </summary>
        public static DateTime GTSMinStandardDateTime
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }

        /// <summary>
        /// حداقل مقدار تاریخ شمسی جهت اعتبارسنجی تاریخ های ورودی در واسط کاربر
        /// </summary>
        public static string GTSMinStandardShamsiDateTime
        {
            get
            {
                return ToPersianDate(GTSMinStandardDateTime);
            }
        }

        /// <summary>
        /// Get Page Count Used in GridView
        /// </summary>
        public static int GetPageCount(int itemCount, int pagesize)
        {
            int pageCount = 0;


            int remainder = 0;
            int quotient = (int)Math.DivRem(itemCount, pagesize, out remainder);
            if (remainder == 0)
            {
                if (itemCount == 0)
                    return 0;
                else if (itemCount < pagesize)
                    pageCount = 1;
                else
                    pageCount = quotient;
            }
            else
            {
                quotient++;
                pageCount = quotient;
            }

            return pageCount;
        }

        /// <summary>
        /// نام خصوصیت را برمی گرداند
        /// </summary>
        /// <remarks>var propertyName = GetPropertyName(() => myObject.AProperty); // returns "AProperty"</remarks>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        /// <summary>
        /// System CPU Usage
        /// used in ELE Helper Class
        /// </summary>
        public static float CpuUsage
        {
            get
            {
                if (counter == null)
                {
                    counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                }
                return (counter.NextValue());
            }
        }

        /// <summary>
        /// شناسه یکتا برای سرور
        /// </summary>
        public static string ServerFingerPrint
        {
            get
            {
                return FingerPrint.GetServerFingerPrint();
            }
        }

        /// <summary>
        /// یک استثنا میگیرد و پیام آن بهمراه تمام استثنا های داخلی را برمیگرداند
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExecptionMessage(Exception ex)
        {
            string message = ex.Message;

            while (ex.InnerException != null)
            {
                message += String.Format(" ---> {0}", ex.InnerException.Message);
                ex = ex.InnerException;
            }
            return message;
        }

        /// <summary>
        /// Appsetting خواندن از
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadAppSetting(string key)
        {
            string value = ConfigurationSettings.AppSettings[key];
            if (value != null)
            {
                return value;
            }
            return "";
        }

        public static string[] Spilit(string source, char ch)
        {
            if (IsEmpty(source))
            {
                return new string[1];
            }
            return source.Split(new char[] { ch });
        }

        public static string[] Spilit(string source, string str)
        {
            if (IsEmpty(source) || IsEmpty(str))
            {
                return new string[1];
            }
            return source.Split(new string[] { str }, StringSplitOptions.None);
        }

        /// <summary>
        /// حذف علامتهای اضافی در نام کاربری تا در دیتابیس قابل استفاده شود
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetSimpleUsername(string username)
        {
            if (username.Contains("@"))
            {
                username = username.Remove(username.IndexOf("@"));
            }
            return username;
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// Write to ASP.NET cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="durationMinutes">دقیقه</param>
        public static void CatchWrite(string key, object value, int durationMinutes)
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Cache[key] == null)
            {
                System.Web.HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(durationMinutes), TimeSpan.Zero);
            }
        }

        /// <summary>
        /// Read From ASP.NET cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object CatchRead(string key)
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Cache[key] != null)
            {
                return System.Web.HttpContext.Current.Cache[key];
            }
            return null;
        }

        /// <summary>
        /// یک رشته را هش میکند
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHashCode(string value)
        {
            if (value == null)
                value = "";
            string hashed = Infrastructure.SimpleHash.ComputeHash(value, HashStandards.MD5, null);
            return hashed;
        }

        public static string EncryptShortHashCode(Decimal value)
        {
            if (value <= 0) throw new ArgumentException("مقدار داده شده صحیح نیست");

            var hashed = new ShortHash()
                .Encrypt(
                    value.ToString("####").Split().Select(s => Convert.ToInt32(s)).ToArray()
                    );

            return hashed;
        }

        public static decimal DecryptShortHashCode(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("مقدار داده شده صحیح نیست");

            var hashed = new ShortHash()
                .Decrypt(
                    value)[0]
                    ;
            return hashed;
        }

        /// <summary>
        /// یک رشته هش نشده را با یک رشته هش شده مقایسه میکند
        /// </summary>
        /// <param name="value">رشته هش نشده</param>
        /// <param name="hashedText">رشته هش شده</param>
        /// <returns></returns>
        public static bool VerifyHashCode(string value, string hashedText)
        {
            if (value == null)
                value = "";
            bool result = Infrastructure.SimpleHash.VerifyHash(value, HashStandards.MD5, hashedText);
            return result;
        }

        /// <summary>
        /// نام روز را برمیگرداند
        /// </summary>
        /// <param name="date"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string GetDayName(DateTime date, LanguagesName language)
        {
            if (language == LanguagesName.Parsi)
            {
                return PersianDateTime.GetPershianDayName(date);
            }
            else
            {
                return PersianDateTime.GetEnglishDayName(date);
            }
        }

        #region Converts
        public static string GetValue(string obj)
        {
            if (IsEmpty(obj))
                return String.Empty;
            return obj;
        }


        /// <summary>
        /// دقیقه را به زمان واقعی تبدیل میکند
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <example>
        /// IntTimeToRealTime(130); --> 2:10
        /// IntTimeToRealTime(1450); --> +0:10
        /// IntTimeToRealTime(-1020); --> -07:00
        /// </example>/// 
        public static string IntTimeToRealTime(int Minute)
        {
            try
            {
                if (Minute == -1000) return "";
                if (Minute == 0) return "";
                int day = 0;
                string temp = "";
                if (Minute >= 0)
                {
                    day = Minute / MinutPerDay;
                    for (int i = 1; i <= day; i++)
                        temp += "+";
                    Minute -= MinutPerDay * day;
                }
                else if (Minute < 0)
                {
                    while (Minute < 0)
                    {
                        Minute += MinutPerDay;
                        temp += "-";
                    }
                }
                temp += (Minute / MinutePerHour).ToString().PadLeft(2, '0') + ":" + (Minute % MinutePerHour).ToString().PadLeft(2, '0');
                return temp;
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.IntTimeToRealTime({0})", Minute), ex);
            }
        }

        /// <summary>
        /// دقیقه را به زمان واقعی تبدیل میکند
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <example>
        /// IntTimeToRealTimeWithZero(130); --> 2:10
        /// IntTimeToRealTimeWithZero(1450); --> +0:10
        /// /// IntTimeToRealTimeWithZero(0); --> 00:00
        /// </example>/// 
        public static string IntTimeToRealTimeWithZero(int Minute)
        {
            try
            {
                if (Minute == -1000) return "";
                int day = 0;
                string temp = "";
                if (Minute >= 0)
                {
                    day = Minute / MinutPerDay;
                    for (int i = 1; i <= day; i++)
                        temp += "+";
                    Minute -= MinutPerDay * day;
                }
                else if (Minute < 0)
                {
                    while (Minute < 0)
                    {
                        Minute += MinutPerDay;
                        temp += "-";
                    }
                }
                temp += (Minute / MinutePerHour).ToString().PadLeft(2, '0') + ":" + (Minute % MinutePerHour).ToString().PadLeft(2, '0');
                return temp;
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.IntTimeToRealTime({0})", Minute), ex);
            }
        }

        /// <summary>
        /// دقیقه را به زمان واقعی تبدیل میکند
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <example>
        /// IntTimeToRealTime(130); --> 2:10
        /// IntTimeToRealTime(1450); --> +0:10
        /// </example>/// 
        public static string IntTimeToRealTime(decimal Minute)
        {
            try
            {
                return IntTimeToRealTime(Convert.ToInt32(Minute));
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.IntTimeToRealTime({0})", Minute), ex);
            }
        }

        /// <summary>
        /// .تبدیل دقیقه به زمان
        /// </summary>
        /// <param name="Minute">.میزان دقیقه ای که به زمان تبدیل می شود</param>
        /// <returns>زمان محاسبه شده</returns>
        /// <example>
        /// MinuteToTime(130); --> 2:10
        /// MinuteToTime(1450); --> 24:10
        /// </example>
        public static string IntTimeToTime(int Minute)
        {
            try
            {
                if (Minute == -1000) return "     ";
                if (Minute == 0) return "     ";
                bool negative = false;
                if (Minute < 0)
                {
                    Minute *= -1;
                    negative = true;
                }
                string time = (Minute / MinutePerHour).ToString().PadLeft(2, '0') + ":" + (Minute % MinutePerHour).ToString().PadLeft(2, '0');
                if (negative)
                {
                    time = "- " + time;
                }
                return time;
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.IntTimeToTime({0})", Minute), ex);
            }
        }

        /// <summary>
        /// .تبدیل دقیقه به زمان
        /// </summary>
        /// <param name="Minute">.میزان دقیقه ای که به زمان تبدیل می شود</param>
        /// <param name="withoutSpace">اگر زمان صفر باشد فاصله قرار نمیدهد</param>
        /// <returns>زمان محاسبه شده</returns>
        /// <example>
        /// MinuteToTime(130); --> 2:10
        /// MinuteToTime(1450); --> 24:10
        /// </example>
        public static string IntTimeToTime(int Minute, bool withoutSpace)
        {
            try
            {
                string time = Utility.IntTimeToTime(Minute);
                if (withoutSpace)
                {
                    time = time.Replace(" ", "");
                }
                return time;
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.IntTimeToTime({0})", Minute), ex);
            }
        }

        /// <summary>
        /// .تبدیل دقیقه به زمان
        /// </summary>
        /// <param name="Minute">.میزان دقیقه ای که به زمان تبدیل می شود</param>
        /// <returns>زمان محاسبه شده</returns>
        /// <example>
        /// MinuteToTime(130); --> 2:10
        /// MinuteToTime(1450); --> 24:10
        /// </example>
        public static string IntTimeToTime(decimal Minute)
        {
            try
            {
                return IntTimeToTime(Convert.ToInt32(Minute));
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.IntTimeToTime({0})", Minute), ex);
            }
        }

        /// <summary>
        /// زمان را به دقیقه تبدیل میکند
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <example>
        /// TimeToMinute("2:10") --> 130
        /// TimeToMinute("+0:10") --> 1450
        /// </example>
        public static int RealTimeToIntTime(string time)
        {
            try
            {
                if (IsEmpty(time)) return -1000;
                int temp = 0;
                if (time.Contains(':'))
                {
                    switch (time[time.Length - 1])
                    {
                        case '+': temp += MinutPerDay; time = time.Remove(time.Length - 1, 1); break;
                        case '-': temp -= MinutPerDay; time = time.Remove(time.Length - 1, 1); break;
                    }
                    temp += Convert.ToInt32((time.Split(':')[0])) * MinutePerHour + Convert.ToInt32((time.Split(':')[1]));
                    return temp;
                }
                else
                {
                    throw new Exception("فرمت زمان نادرست است -- RealTimeToIntTime");
                }
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.RealTimeToIntTime({0})", time), ex);
            }
        }

        /// <summary>
        /// تبدیل شمسی به میلادی
        /// </summary>
        /// <param name="persianDate">تاریخ شمسی</param>
        /// <returns></returns>
        public static DateTime ToMildiDate(string persianDate)
        {
            try
            {
                if (!Utility.IsEmpty(persianDate))
                {
                    string[] strs = persianDate.Split('/');
                    if (strs[0].Length == 4)
                    {
                        persianDate = strs[0] + "/" + strs[1].PadLeft(2, '0') + "/" + strs[2].PadLeft(2, '0');
                    }
                    else
                    {
                        persianDate = strs[2] + "/" + strs[1].PadLeft(2, '0') + "/" + strs[0].PadLeft(2, '0');
                    }

                    PersianDateTime p = PersianDateTime.Parse(persianDate);
                    return p.GregorianDate;
                }
                return GTSMinStandardDateTime;
            }
            catch (Exception ex)
            {
                if (ex is InvalidPersianDateException)
                    throw ex;
                else
                    throw new BaseException(ex.Message, String.Format("Utility.ToMiladiDate({0})", persianDate), ex);
            }
        }

        /// <summary>
        /// تبدیل میلادی متنی به میلادی
        /// </summary>
        /// <param name="miladiDate">تاریخ میلادی</param>
        /// <returns></returns>
        public static DateTime ToMildiDateTime(string miladiDate)
        {
            try
            {
                if (!Utility.IsEmpty(miladiDate))
                {
                    miladiDate = miladiDate.Replace("-", "/");
                    string[] strs = miladiDate.Split('/');
                    if (strs.Length == 3)
                    {
                        if (strs[0].Length == 4)
                        {
                            return new DateTime(Utility.ToInteger(strs[0]), Utility.ToInteger(strs[1]), Utility.ToInteger(strs[2]));
                        }
                        else if (strs[2].Length == 4)
                        {
                            return new DateTime(Utility.ToInteger(strs[2]), Utility.ToInteger(strs[1]), Utility.ToInteger(strs[0]));
                        }
                    }
                }
                else
                    return Utility.GTSMinStandardDateTime;
                throw new BaseException("فرمت تاریخ جهت تبدیل شمسی به میلادی صحیح نمیباشد", "GTS.Clock.Infrastructure.Utility.Utility.ToMildiDate(string miladiDate)");
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.ToMiladiDateTime({0})", miladiDate), ex);
            }
        }

        /// <summary>
        /// تبدیل شمسی متنی به میلادی متنی
        /// </summary>
        /// <param name="miladiDate">تاریخ میلادی</param>
        /// <returns></returns>
        public static string ToMildiDateString(string persianDate)
        {
            try
            {
                if (!Utility.IsEmpty(persianDate))
                {
                    DateTime date = Utility.ToMildiDate(persianDate);
                    return String.Format("{0}/{1}/{2}", date.Year, date.Month, date.Day);
                }
                else
                {
                    DateTime date = GTSMinStandardDateTime;
                    return String.Format("{0}/{1}/{2}", date.Year, date.Month, date.Day);
                }
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.ToMildiDateString({0})", persianDate), ex);
            }
        }

        /// <summary>
        /// تبدیل میلادی به شمسی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns></returns>
        public static string ToPersianDate(DateTime miladiDate)
        {
            try
            {
                return PersianDateTime.MiladiToShamsi(Utility.ToString(miladiDate));
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.ToPersianDate({0})", miladiDate), ex);
            }
        }

        /// <summary>
        /// تبدیل میلادی به شمسی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns></returns>
        public static string ToPersianDate(string miladiDate)
        {
            try
            {
                if (!Utility.IsEmpty(miladiDate))
                {
                    return PersianDateTime.MiladiToShamsi(miladiDate);
                }
                return PersianDateTime.MiladiToShamsi(Utility.ToString(GTSMinStandardDateTime));
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.ToPersianDate({0})", miladiDate), ex);
            }
        }

        /// <summary>
        /// تبدیل میلادی به شمسی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns></returns>
        public static PersianDateTime ToPersianDateTime(DateTime miladiDate)
        {
            try
            {
                PersianDateTime pd = new PersianDateTime(miladiDate);
                return pd;
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, String.Format("Utility.ToPersianDateTime({0})", miladiDate), ex);
            }
        }

        /// <summary>
        /// تاریخ را با فرمت سال/ماه/روز برمیگرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToString(DateTime date)
        {
            string str = date.ToString("u");
            str = str.Replace("-", "/");
            str = str.Remove(10);
            return str;
        }

        public static string ToString(TimeSpan span)
        {
            return String.Format("{0}:{1}:{2}", span.Hours, span.Minutes, span.Seconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumration"></param>
        /// <returns></returns>
        public static string ToString(Enum enumration)
        {
            return enumration.ToString("G");
        }

        /// <summary>
        /// اگر تهی باشد خالی برمیگرداند
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            if (obj != null)
                return obj.ToString();
            return String.Empty;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInteger(string obj)
        {
            if (obj == null) return 0;
            int val = 0;
            if (Int32.TryParse(obj, out val))
            {
                return val;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInteger(object obj)
        {
            if (obj == null) return 0;
            int val = 0;
            if (Int32.TryParse(obj.ToString(), out val))
            {
                return val;
            }
            return 0;
        }

        public static decimal ToDecimal(object obj)
        {
            if (obj == null) return 0;
            decimal val = 0;
            if (Decimal.TryParse(obj.ToString(), out val))
            {
                return val;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool ToBoolean(char bit)
        {
            bool flag = false;
            if (bit.Equals('0'))
                flag = false;
            else if (bit.Equals('1'))
                flag = true;
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool ToBoolean(string bit)
        {
            bool flag = false;
            if (bit.Equals('0') || bit.Equals("0"))
                flag = false;
            else if (bit.Equals('1') || bit.Equals("1"))
                flag = true;
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool ToBoolean(int bit)
        {
            bool flag = false;
            if (bit == 0)
                flag = false;
            else
                flag = true;
            return flag;
        }

        public static bool ToBoolean(object obj)
        {
            bool flag = false;
            if (obj == null)
                flag = false;
            else
                flag = (bool)obj;
            return flag;
        }

        /// <summary>
        /// دوره محاسبات از روز و ماه تشکیل شده است که برای راحتی کار در مقایسه ها 
        /// کلیدی ایجاد شده است که از ماه و روز یک عدد میسازد
        /// این عدد 5 رقمی است و با فرمت زیر ساخته میشود
        /// '2'+(MM).ToString()+(DD).ToString()
        /// اگر ماه مربوط به سال قبل بود بجای 2 عدد یک و اگر مربوط به سال بعد عدد سه را میگزاریم
        /// </summary> 
        /// <param name="month">ماه </param>
        /// <param name="day">روز </param>
        /// <param name="yearStatus"> یک یعنی سال قبل - 2 یعنی سال جاری - 3 یعنی سال بعد </param>
        /// <returns></returns>
        public static int ToDateRangeIndex(int month, int day, int yearStatus)
        {
            if (yearStatus < 1 || yearStatus > 3)
                throw new Exception("وضعیت سال باید بین یک و سه باشد");
            string index = String.Format("{0}{1}{2}", yearStatus, month.ToString().PadLeft(2, '0'), day.ToString().PadLeft(2, '0'));
            return ToInteger(index);
        }

        /// <summary>
        /// دوره محاسبات از روز و ماه تشکیل شده است که برای راحتی کار در مقایسه ها 
        /// کلیدی ایجاد شده است که از ماه و روز یک عدد میسازد
        /// این عدد 5 رقمی است و با فرمت زیر ساخته میشود
        /// '2'+(MM).ToString()+(DD).ToString()
        /// اگر ماه مربوط به سال قبل بود بجای 2 عدد یک و اگر مربوط به سال بعد عدد سه را میگزاریم
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int ToDateRangeIndex(DateTime date, LanguagesName culture)
        {
            if (culture == LanguagesName.Parsi)
            {
                PersianDateTime p = ToPersianDateTime(date);
                string index = String.Format("2{0}{1}", p.Month.ToString().PadLeft(2, '0'), p.Day.ToString().PadLeft(2, '0'));
                return ToInteger(index);
            }
            else if (culture == LanguagesName.English)
            {
                string index = String.Format("2{0}{1}", date.Month.ToString().PadLeft(2, '0'), date.Day.ToString().PadLeft(2, '0'));
                return ToInteger(index);
            }
            return 0;

        }

        /// <summary>
        /// دوره محاسبات از روز و ماه تشکیل شده است که برای راحتی کار در مقایسه ها 
        /// کلیدی ایجاد شده است که از ماه و روز یک عدد میسازد
        /// این عدد 5 رقمی است و با فرمت زیر ساخته میشود
        /// '2'+(MM).ToString()+(DD).ToString()
        /// اگر ماه مربوط به سال قبل بود بجای 2 عدد یک و اگر مربوط به سال بعد عدد سه را میگزاریم
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int ToDateRangeIndex(DateTime date)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// اعداد را به پارسی تبدیل میکند
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String ToParsiCharacter(string text)
        {
            return text;
            string str =
                text.Replace('0', '٠')
                .Replace('1', '١')
                .Replace('2', '٢')
                .Replace('3', '٣')
                .Replace('4', '٤')
                .Replace('5', '٥')
                .Replace('6', '٦')
                .Replace('7', '٧')
                .Replace('8', '٨')
                .Replace('9', '٩');
            return str;
        }

        /// <summary>
        /// This method is used to generate javascript equivalents of C# enumerations.
        /// This makes it possible to use the C# enum without having to code and maintain
        /// an equivalent javascript class
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ConvertEnumToJavascript(Type t)
        {
            if (!t.IsEnum) throw new Exception("Type must be an enumeration");

            var values = System.Enum.GetValues(t);
            var dict = new Dictionary<string, string>();

            foreach (object obj in values)
            {
                string name = System.Enum.GetName(t, obj);
                dict.Add(name, System.Enum.Format(t, obj, "D"));
            }

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(dict);
        }

        #region Perisan Date

        /// <summary>
        /// تعداد روز ماههای شمسی را برمیگرداند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetEndOfPersianMonth(int year, int month)
        {
            int endOfMonth = new PersianCalendar().GetDaysInMonth(year, month);
            return endOfMonth;
        }

        /// <summary>
        /// ابتدای ماه شمسی را باتوجه به تاریخ میلادی برمیگرداند
        /// </summary>
        /// <param name="miladiDate"></param>
        /// <returns></returns>
        public static DateTime GetStartOfPersianMonth(DateTime miladiDate)
        {
            DateTime monthStart = new DateTime();
            PersianDateTime date = Utility.ToPersianDateTime(miladiDate);
            monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", date.Year, date.Month, 1));
            return monthStart;
        }

        /// <summary>
        /// انتهای ماه شمسی را باتوجه به تاریخ میلادی برمیگرداند
        /// </summary>
        /// <param name="miladiDate"></param>
        /// <returns></returns>
        public static DateTime GetEndOfPersianMonth(DateTime miladiDate)
        {
            DateTime monthEnd = new DateTime();
            PersianDateTime date = Utility.ToPersianDateTime(miladiDate);
            int endOfMonth = new PersianCalendar().GetDaysInMonth(date.Year, date.Month);
            monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", date.Year, date.Month, endOfMonth));
            return monthEnd;
        }

        #endregion

        #region Miladi Date

        /// <summary>
        /// تعداد روزهای ماههای میلادی را برمیگرداند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetEndOfMiladiMonth(int year, int month)
        {
            int endOfMonth = DateTime.DaysInMonth(year, month);
            return endOfMonth;
        }

        /// <summary>
        /// ابتدای ماه میلادی را باتوجه به تاریخ میلادی برمیگرداند
        /// </summary>
        /// <param name="miladiDate"></param>
        /// <returns></returns>
        public static DateTime GetStartOfMiladiMonth(DateTime miladiDate)
        {
            return new DateTime(miladiDate.Year, miladiDate.Month, miladiDate.Day);
        }

        /// <summary>
        /// انتهای ماه میلادی را باتوجه به تاریخ میلادی برمیگرداند
        /// </summary>
        /// <param name="miladiDate"></param>
        /// <returns></returns>
        public static DateTime GetEndOfMiladiMonth(DateTime miladiDate)
        {
            return new DateTime(miladiDate.Year, miladiDate.Month, Utility.GetEndOfMiladiMonth(miladiDate.Year, miladiDate.Month));
        }

        #endregion

        /// <summary>
        /// تاریخ اولین روز سال مشخص شده را برمیگرداند
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static DateTime GetDateOfBeginYear(DateTime Date, LanguagesName SysLanguage)
        {
            switch (SysLanguage)
            {
                case LanguagesName.Parsi:
                    return new PersianDateTime(PersianDateTime.ToPersianDateTime(Date).Year, 1, 1).GregorianDate;
                case LanguagesName.English:
                case LanguagesName.Unknown:
                default:
                    return new DateTime(Date.Year, 1, Utility.GetEndOfMiladiMonth(Date.Year, 1));
            }
        }

        /// <summary>
        /// تاریخ آخرین روز سال مشخص شده را برمیگرداند
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static DateTime GetDateOfEndYear(DateTime Date, LanguagesName SysLanguage)
        {
            switch (SysLanguage)
            {
                case LanguagesName.Parsi:
                    int year = PersianDateTime.ToPersianDateTime(Date).Year;
                    return Convert.ToDateTime(PersianDateTime.ShamsiToMiladi(String.Format("{0}/{1}/{2}", year, 12, Utility.GetEndOfPersianMonth(year, 12))));
                case LanguagesName.English:
                case LanguagesName.Unknown:
                default:
                    return new DateTime(Date.Year, 12, Utility.GetEndOfMiladiMonth(Date.Year, 12));
            }
        }

        public static int GetCurrentYear()
        {
            return new PersianDateTime(DateTime.Now).Year;
        }
        #endregion

        #region Checkers

        public static int CharOccuranceCount(string source, string sign)
        {
            if (Utility.IsEmpty(source) || Utility.IsEmpty(sign))
                return 0;
            int len1 = source.Length;
            source = source.Replace(sign, "");
            int len2 = source.Length;
            return len1 - len2;
        }

        /// <summary>
        /// obj==null || obj.Length==0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmpty(string obj)
        {
            if (obj == null || obj.Length == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// obj==null 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmpty(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// obj==null 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmpty(bool obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// obj==null || obj == 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmpty(decimal? obj)
        {
            if (obj == null || obj == 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// obj==null || obj == '0001/01/01'
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmpty(DateTime obj)
        {
            if (obj == null || (obj.Year == 1 && obj.Month == 1 && obj.Day == 1) || obj.Date == GTSMinStandardDateTime)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// list==null || list.count == 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// آیا رشته ورودی یک شماره است
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            if (Utility.IsEmpty(value))
                return false;
            Regex regex = new Regex(@"\d{" + ((int)(value.Length - 1)).ToString() + "}");
            return regex.IsMatch(value);
        }

        public static bool IsIntiger(string value)
        {
            if (Utility.IsEmpty(value))
                return false;
            int result = 0;
            if (Int32.TryParse(value, out result))
            {
                return true;
            }
            return false;
        }

        public static bool IsTime(string value)
        {
            if (!Utility.IsEmpty(value))
            {
                if (value.Contains(':'))
                {
                    int temp = 0;
                    value = value.Replace("+", "").Replace("-", "");
                    if (value.Split(':').Length == 2)
                    {
                        if (Utility.IsIntiger(value.Split(':')[0])
                            && Utility.IsIntiger(value.Split(':')[1]) && value.Split(':')[1].Length <= 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        #endregion

        public static DateTime Max(DateTime date1, DateTime date2)
        {
            return date1 > date2 ? date1 : date2;
        }

        public static DateTime Min(DateTime date1, DateTime date2)
        {
            return date1 > date2 ? date2 : date1;
        }

        public static IList<Range> Intersect(Range Pair1, Range Pair2)
        {
            if (Pair1 != null && Pair2 != null)
            {
                IList<Range> tmp1 = new List<Range>();
                tmp1.Add(Pair1);
                IList<Range> tmp2 = new List<Range>();
                tmp2.Add(Pair2);

                var pairsResult = (from BB in
                                       (from A in tmp1
                                        from B in tmp2
                                        select new Range(Utility.Max(A.From, B.From), Utility.Min(A.To, B.To), A.AditionalField))
                                   where BB.From < BB.To
                                   select BB);
                return pairsResult.OfType<Range>().ToList<Range>();
            }
            return new List<Range>();
        }

        public static IList<Range> Intersect(IList<Range> PairList1, IList<Range> PairList2)
        {
            if (PairList1 != null && PairList2 != null)
            {
                var pairsResult = (from BB in
                                       (from A in PairList1
                                        from B in PairList2
                                        select new Range(Utility.Max(A.From, B.From), Utility.Min(A.To, B.To), A.AditionalField))
                                   where BB.From < BB.To
                                   select BB);
                return pairsResult.OfType<Range>().ToList<Range>();
            }
            return new List<Range>();
        }

        public static IList<Range> Differance(IList<Range> l1, IList<Range> l2)
        {

            bool neededIntersect = false;
            var q1 = (from A in l1
                      select A).OrderBy(t => t.From).ThenBy(t => t.To);
            var q2 = (from B in l2
                      select B).OrderBy(t => t.From).ThenBy(t => t.To);

            IList<Range> myList = new List<Range>(); //Main List
            IList<Range> List1 = new List<Range>(); //Stors continious items of q2 (items of an specific area)
            IList<Range> TempList = new List<Range>(); //Stors Current items of q2 (if it is in one of the areas)

            foreach (var A in q1)
            {
                neededIntersect = false;
                foreach (var B in q2)
                {
                    IList<Range> tmp = new List<Range>();
                    tmp.Add(A);
                    IList<Range> anySimilarity = Utility.Intersect(l2, tmp);
                    if (anySimilarity.Count != 0)
                    {
                        if (A.From < B.From && A.To > B.To) // First State
                        {
                            TempList.Add(new Range(A.From, B.From, A.AditionalField));
                            TempList.Add(new Range(B.To, A.To, A.AditionalField));
                        }

                        if (A.From < B.From && A.To <= B.To && B.From < A.To) // Second State
                        {
                            TempList.Clear();
                            TempList.Add(new Range(A.From, B.From, A.AditionalField));
                        }


                        if (A.From >= B.From && A.To > B.To && A.From < B.To) // Third State
                        {
                            TempList.Clear();
                            TempList.Add(new Range(B.To, A.To, A.AditionalField));
                        }

                        if (A.From >= B.From && A.To <= B.To)
                        {
                            break;
                        }
                    }
                    else
                    {
                        TempList.Clear();
                        TempList.Add(A);
                    }

                    if (TempList.Count != 0)
                    {
                        if (neededIntersect || List1.Count > 0)
                        {
                            List1 = Utility.Intersect(List1, TempList);
                            TempList.Clear();
                        }
                        else
                        {
                            foreach (Range item in TempList)
                                List1.Add(item);
                            TempList.Clear();
                        }
                        neededIntersect = true;
                    }
                }

                foreach (Range item in List1)
                    myList.Add(item);
                List1.Clear();
            }
            return myList;

        }

        public static IList<Range> Differance(Range l1, IList<Range> l2)
        {
            IList<Range> list1 = new List<Range>();
            list1.Add(l1);
            return Utility.Differance(list1, l2);
        }

        public static IList<Range> Differance(IList<Range> l1, Range l2)
        {
            IList<Range> list2 = new List<Range>();
            list2.Add(l2);
            return Utility.Differance(l1, list2);
        }

    }
}
