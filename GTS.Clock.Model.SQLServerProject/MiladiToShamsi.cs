using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString GTS_ASM_MiladiToShamsi(string GregorianDate)
    {
        try
        {
            DateTime date = Convert.ToDateTime(GregorianDate);
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date).ToString() + '/' +
                    pc.GetMonth(date).ToString().PadLeft(2, '0') + '/' +
                    pc.GetDayOfMonth(date).ToString().PadLeft(2, '0');
        }
        catch (Exception ex) 
        {
            throw new Exception(String.Format("GTS Miladi To Shamsi Exception Parameter: {0} Exception:{1}", GregorianDate, ex.Message));
        }
    }
};

