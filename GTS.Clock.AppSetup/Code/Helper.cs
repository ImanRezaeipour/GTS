using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.AppSetup
{
    public class Helper
    {
        DataSet.ClockDataSetTableAdapters.holidayTableAdapter holidayTA = new GTS.Clock.AppSetup.DataSet.ClockDataSetTableAdapters.holidayTableAdapter();
        DataSet.ClockDataSet.holidayDataTable holidayTable = new GTS.Clock.AppSetup.DataSet.ClockDataSet.holidayDataTable();
        public IList<DateTime> GetHolidaysFromClockTable() 
        {
            List<DateTime> list = new List<DateTime>();
            PersianDateTime p=new PersianDateTime(DateTime.Now);
            int fromYear = (int)(p.Year - 5);
            int toYear = (int)(p.Year + 10);
            holidayTable = holidayTA.GetDataByYear(fromYear, toYear);
            foreach (DataSet.ClockDataSet.holidayRow holiday in holidayTable.Rows) 
            {
                list.AddRange(this.TranslateStringMonthDays(holiday.holiday_year, holiday.holiday_Month, holiday.holiday_Rasmi));
            }
            return list;
        }

        private IList<DateTime> TranslateStringMonthDays(int year, int month, string days) 
        {
            IList<DateTime> list = new List<DateTime>();
            string persianDate = String.Format("{0}/{1}/", year, month);
            for (int index = 0; index < days.Length && index < Utility.GetEndOfPersianMonth(year, month); index++)
            {
                try
                {
                    bool isHoliday = Utility.ToBoolean(days[index]);
                    if (isHoliday)
                    {
                        DateTime date = Utility.ToMildiDate(persianDate + (index + 1).ToString());
                        list.Add(date);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is InvalidPersianDateException)
                    {
                        //do nothing only do not add this date
                        //جدول هالیدی همه ماهها را 31 روزه درنشظ میگیرد و ما نباید روزهای غیرمجاز را 
                        //به مجموعه جواب اضافه کنیم
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            return list;
        }
    }
}
