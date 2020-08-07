using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Business.Proxy
{
    public class CalendarCellInfo
    {
        public CalendarCellInfo() { }

        /// <summary>
        /// تاریخ را نیز تبدیل میکند
        /// </summary>
        /// <param name="calendar"></param>
        public CalendarCellInfo(Calendar calendar)
        {
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                PersianDateTime p = new PersianDateTime(calendar.Date);
                this.Month = p.Month;
                this.Day = p.Day;
                this.Color = "#FF0000";
            }
            else
            {
                Day = calendar.Date.Day;
                Month = calendar.Date.Month;
            }
        }

        public int Day { get; set; }

        public int Month { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public decimal ShiftID { get; set; }

        /// <summary>
        /// جهت مقایسه
        /// </summary>
        public string Key
        {
            get
            {
                string key = Month.ToString() + "-" + Day.ToString();
                return key;
            }
        }

        public Calendar Export(int year) 
        {
            Calendar cal = new Calendar();
            DateTime date = new DateTime();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                PersianDateTime p = new PersianDateTime(year, this.Month, this.Day);
                date = p.GregorianDate;
            }
            else             
            {
                date = new DateTime(year, Month, Day);
            }
            cal.Date = date;
            return cal;
        }

    }
}
