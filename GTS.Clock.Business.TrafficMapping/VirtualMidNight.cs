using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Model;

namespace GTS.Clock.Business.TrafficMapping
{
    /// <summary>
    /// معیار قسمت اول شبانه روز است یا قسمت دوم
    /// </summary>
    public enum FirstOrSecond {NotSpec=0, First = 1, Second = 2 };
    /// <summary>
    /// ساختمان داده نیمه شب
    /// </summary>
    public class VirtualMidNight
    {
        #region variables
               
        public struct VirtualDayRegion 
        {
            private const int DayMinutes = 1440;
            private DateTime fromDate, toDate;

            public VirtualDayRegion(DateTime date, int fromTime, int toTime)
                : this()
            {
                try
                {
                    if (toTime < fromTime)
                    {
                        toTime = fromTime;
                    }
                    int hours = ((int)(fromTime / 60));
                    int minutes = fromTime % 60;

                    DateTime _date = date;
                    if (hours >= 24)
                    {
                        _date = _date.AddDays(1);
                        hours -= 24;
                    }
                    fromDate = new DateTime(_date.Year, _date.Month, _date.Day, hours, minutes, 0);

                    hours = ((int)(toTime / 60));
                    minutes = toTime % 60;
                    _date = date;
                    if (hours >= 24)
                    {
                        _date = _date.AddDays(1);
                        hours -= 24;
                    }
                    toDate = new DateTime(_date.Year, _date.Month, _date.Day, hours, minutes, 0);
                    IsNotEmpty = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("TrafficMaper-VirtualDayRegion->" + Infrastructure.Utility.Utility.GetExecptionMessage(ex));
                }
            }
            
            public DateTime StartDateTime
            {
                get 
                {
                    return fromDate;
                }                
            }
            public DateTime EndDateTime
            {
                get 
                {
                    return toDate;
                }
                
            }
            public DateTime Date
            {
                get
                {
                    return fromDate.Date;
                }

            }
            public double Length 
            {
                get { return (EndDateTime - StartDateTime).TotalMinutes; }
            }

            /// <summary>
            /// گاهی قسمت دوم یک نیمه شب مجازی خالی است
            /// مثلا وقتی که نیمه شب مجازی بصورت معمول است و نیمه شب بربر 12 شب است
            /// </summary>
            public bool IsNotEmpty { get; set; }

            public override string ToString()
            {
                return String.Format("{0}-{1}"
                    , new GTS.Clock.Infrastructure.Utility.PersianDateTime(fromDate).ToString()
                    , toDate.TimeOfDay);
            }

        }
       
        private DateTime date = new DateTime();
        private int time = 0;
        private const int DayNightMinutes = 1440;
        private int beforeTelorance = 0, afterTelorance = 0;
        public FirstOrSecond DayPart = FirstOrSecond.NotSpec;
        
        #endregion

        public VirtualMidNight() 
        {

        }

        public VirtualMidNight(DateTime _date, int _time)
        {
            date = new DateTime(_date.Year, _date.Month, _date.Day);
            time = _time;
            beforeTelorance = 0;
            afterTelorance = 0;
            this.DayPart = FirstOrSecond.First;
        }

        public VirtualMidNight(DateTime _date, int _time, int beforeTelorance, int afterTelorance)
        {
            date = new DateTime(_date.Year, _date.Month, _date.Day);
            time = _time;
            this.afterTelorance = afterTelorance;
            this.beforeTelorance = beforeTelorance;
            DayPart = FirstOrSecond.First;
        }

        /// <summary>
        /// در صورتی که نیمه دوم با شیفت فردا تداخل داشته باشد میتوان با مقدار دهی این خصیصه نیمه دوم را محدود نمود
        /// به عبارتی نیمه شب مجازی بازه کمتر از 24 ساعت را پوشش دهد.
        /// </summary>
        public int SecondMidLimit { get; set; }

        public int Time
        {
            get
            {
                return time;
            }
           
        }

        public DateTime Date
        {
            get
            {
                return date.Date;
            }
            //set
            //{
            //    date = new DateTime(value.Year, value.Month, value.Day);
            //}
        }

        public DateTime DateTime
        {
            get
            {
                DateTime tmpDate = date;
                int tmpTime = time;
                if (time >= DayNightMinutes)
                {
                    int days = Convert.ToInt32((time) / DayNightMinutes);
                    tmpTime = time - days * DayNightMinutes;
                    tmpDate = date.AddDays(days);
                }
                int hours = ((int)(tmpTime / 60));
                int minutes = tmpTime % 60;
                DateTime datetime = new DateTime(tmpDate.Year, tmpDate.Month, tmpDate.Day, hours, minutes, 0);
                return datetime;
            }
        }

        /// <summary>
        /// از شیفت میآید
        /// </summary>
        public int BeforeTelorance
        {
            get { return beforeTelorance; }
            set { beforeTelorance = value; }
        }

        /// <summary>
        /// از شیفت میآید
        /// </summary>
        public int AfterTelorance
        {
            get { return afterTelorance; }
            set { afterTelorance = value; }
        }

        /// <summary>
        /// نیمه اول روز در روز اول
        /// گاهی ممکن است طلو این بازه صفر یا منفی شود مانند وقتی که نیمه شب در انتهای شب قرار
        /// گیرد.در این صورت نیمه دوم روز شامل کل محدودخ میشود
        /// </summary>
        public VirtualDayRegion FirstMid
        {
            get
            {
                try
                {
                    int virtualMidNight = time;
                    System.DateTime virtualMidnightDate = date;
                    virtualMidNight = GetStandardMidNight(virtualMidNight);

                    //3-6-1393 تلورانس در تابع GetMidNight چک میشود
                    VirtualDayRegion vdr = new VirtualDayRegion(date, virtualMidNight, DayNightMinutes - 1);
                    return vdr;
                    /*if (virtualMidNight - BeforeTelorance > 0)
                    {
                        VirtualDayRegion vdr = new VirtualDayRegion(date, virtualMidNight - BeforeTelorance, DayNightMinutes - 1);
                        return vdr;
                    }
                    else if (virtualMidNight - BeforeTelorance < 0)
                    {
                        VirtualDayRegion vdr = new VirtualDayRegion(date.AddDays(1), DayNightMinutes + (virtualMidNight - BeforeTelorance), DayNightMinutes - 1);
                        return vdr;
                    }  
                    else//این حالت باید بررسی شود و اعمال گردد 
                    {
                        VirtualDayRegion vdr = new VirtualDayRegion(date, virtualMidNight, DayNightMinutes - 1);
                        return vdr;
                    }   */                 
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        ///  نیمه دوم روز در روز بعد
        /// </summary>
        public VirtualDayRegion SecondMid
        {
            get
            {
                try
                {
                    int virtualMidNight = GetStandardMidNight(time - 1); //نیمه شب 7 صبح باشد که نیمه دوم تا قبل 7 صبح فردا میشود

                    //در این حالت تلورانس شیفت اعمال نشده است , هر زمانی که مورد آن پیش آمد باید اعمال گردد 
                    if (virtualMidNight == 0) //یعنی نیمه شب صفر بامداد بوده است
                    {
                        VirtualDayRegion vdr = new VirtualDayRegion(date, 0, 0);
                        vdr.IsNotEmpty = false;
                        return vdr;
                    }
                    else
                    {
                        VirtualDayRegion vdr = new VirtualDayRegion(date.Date.AddDays(1), 0, virtualMidNight + AfterTelorance);
                        if (SecondMidLimit > 0) 
                        {
                            vdr = new VirtualDayRegion(date.Date.AddDays(1), 0, SecondMidLimit);
                        }
                        return vdr;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool operator ==(VirtualMidNight vmn1, VirtualMidNight vmn2)
        {
            if (vmn1.Date == vmn2.Date && vmn1.Time == vmn2.Time) 
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(VirtualMidNight vmn1, VirtualMidNight vmn2)
        {
            if (vmn1.Date == vmn2.Date && vmn1.Time == vmn2.Time)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return String.Format("DateTime:{0} |---| First Mid:{1} , Second Mid:{2}"
                , new GTS.Clock.Infrastructure.Utility.PersianDateTime(DateTime).ToString()
                , FirstMid.ToString(), SecondMid.ToString());
        }

        /// <summary>
        /// اگر نیمه شب ساعات نزدیک به نیمه شب تعریف شده باشد برای استاندارد سازی همه را 
        /// صفر بامداد در نظر میگیریم
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
        private int GetStandardMidNight(int _time)
        {
            if (_time > DayNightMinutes)
            {
                _time -= DayNightMinutes;
            }

            if (_time == 1 || _time < 0) //یعنی نیمه شب صفر بامداد بوده است
            {
                _time = 0;
            }
            if (_time == DayNightMinutes - 2 || _time == DayNightMinutes - 1 || _time == DayNightMinutes)
            {
                _time = 0;
            }
            return _time;
        }
    }
}
