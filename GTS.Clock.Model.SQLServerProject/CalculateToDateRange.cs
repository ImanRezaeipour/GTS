using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlDateTime TA_ASM_CalculateToDateRange(DateTime dt, int RangeOrder, int RangeFromMonth, int RangeFromDay, int RangeToMonth, int RangeToDay, int culture)
    {
        try
        {
            Calendar calendar = null;
            switch (culture)
            {
                case 1: calendar = new PersianCalendar(); break;
                default: calendar = new GregorianCalendar(); break;
            }

            if (RangeOrder == 12 && RangeFromMonth > RangeToMonth)
            {
                //در آخرین بازه محدوده، ماه پایان در سال بعد قرار گرفته
                dt = calendar.AddYears(dt, 1);
            }
            else if (RangeOrder == 0 && RangeFromMonth >= RangeToMonth)
            {
                //بازه سالانه است و پایان در سال بعد قرار گرفته
                dt = calendar.AddYears(dt, 1);
            }

            if (calendar is PersianCalendar)
            {
                if (calendar.IsLeapYear(calendar.GetYear(dt)) && RangeToMonth == 12 && RangeToDay == 29)
                {
                    return calendar.ToDateTime(calendar.GetYear(dt), RangeToMonth, 30, 0, 0, 0, 0);
                }
                else
                {
                    return calendar.ToDateTime(calendar.GetYear(dt), RangeToMonth, RangeToDay, 0, 0, 0, 0);
                }
            }
            else
            {
                if (calendar.IsLeapYear(dt.Year) && RangeToMonth == 2 && RangeToDay == 28)
                {
                    //اگر سال کبسه بود و برای ماه فوریه روز 28 انتخاب شده بود
                    //به صورت خودکار با روز 29 جایگزین می شود
                    return calendar.ToDateTime(dt.Year, RangeToMonth, 29, 0, 0, 0, 0);
                }
                else
                {
                    return calendar.ToDateTime(dt.Year, RangeToMonth, RangeToDay, 0, 0, 0, 0);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("GTS CalculateFromDateRange Exception:{0}", ex.Message));
        }
    }
};

