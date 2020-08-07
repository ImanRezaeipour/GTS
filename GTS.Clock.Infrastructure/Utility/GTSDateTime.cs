using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Utility
{
    public static class GTSDateTime
    {
        public static int HourPerDay; 
        public static int MinutePerHour;
        public static int MinutPerDay;

        static GTSDateTime()
        {
            GTSDateTime.MinutePerHour = 60;
            GTSDateTime.HourPerDay = 24;
            GTSDateTime.MinutPerDay = (HourPerDay * MinutePerHour);
        }

        /// <summary>
        /// .تبدیل دقیقه به زمان
        /// </summary>
        /// <param name="Minute">.میزان دقیقه ای که به زمان تبدیل می شود</param>
        /// <returns>زمان محاسبه شده</returns>
        /// <example>
        /// MinuteToTime(130); --> 2:10
        /// MinuteToTime(1450); --> +0:10
        /// </example>
        public static string ExMinuteToTime(int Minute)
        {
            int day = 0;
            string temp = "";
            day = Minute / GTSDateTime.MinutPerDay;
            for (int i = 1; i <= day; i++)
                temp += "+";
            Minute -= GTSDateTime.MinutPerDay * day;
            temp += (Minute / MinutePerHour).ToString().PadLeft(2, '0') + ":" + (Minute % MinutePerHour).ToString().PadLeft(2, '0');
            return temp;
        }

        /// <summary>
        /// .زمان وارد شده را به دقیقه تبدیل می نماید
        /// </summary>
        /// <param name="Time">.زمان مورد نظر برای تبدیل</param>
        /// <returns>.میزان دقیقه ی محاسبه شده</returns>
        /// <example>
        /// TimeToMinute("2:10") --> 130
        /// TimeToMinute("+0:10") --> 1450
        /// </example>
        public static int EXTimeToMinute(string Time)
        {
            int temp = 0;
            switch (Time[Time.Length - 1])
            {
                case '+': temp += GTSDateTime.MinutPerDay; Time = Time.Remove(Time.Length - 1, 1); break;
                case '-': temp -= GTSDateTime.MinutPerDay; Time = Time.Remove(Time.Length - 1, 1); break;
            }
            temp += Convert.ToInt32((Time.Split(':')[0])) * GTSDateTime.MinutePerHour + Convert.ToInt32((Time.Split(':')[1]));
            return temp;
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
        public static string MinuteToTime(int Minute)
        {
            return (Minute / MinutePerHour).ToString().PadLeft(2, '0') + ":" + (Minute % MinutePerHour).ToString().PadLeft(2, '0');
        }

    }
}
