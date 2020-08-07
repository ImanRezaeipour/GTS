using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlDateTime TA_ASM_CalculateFromDateRange(DateTime dt, int RangeOrder, int RangeFromMonth, int RangeFromDay, int RangeToMonth, int RangeToDay, int culture)
    {
        try
        {
            Calendar calendar = null;
            switch (culture)
            {
                case 1: calendar = new PersianCalendar(); break;
                default: calendar = new GregorianCalendar(); break;
            }
            if (RangeOrder == 1 && RangeFromMonth > RangeToMonth)
            {
                //در اولین بازه محدوده، ماه شروع در سال قبل قرار گرفته
                dt = calendar.AddYears(dt, -1);
            }
            else if (RangeOrder == 0 && RangeFromMonth >= RangeToMonth)
            {
                //بازه سالانه است و شروع در سال قبل قرار گرفته
                dt = calendar.AddYears(dt, -1);
            }

            return calendar.ToDateTime(calendar.GetYear(dt), RangeFromMonth, RangeFromDay, 0, 0, 0, 0);
        }
        catch (Exception ex) 
        {
            throw new Exception(String.Format("GTS CalculateFromDateRange Exception:{0}", ex.Message));
        }
    }
};

