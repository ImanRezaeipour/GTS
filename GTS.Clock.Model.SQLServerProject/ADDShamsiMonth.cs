using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString GTS_ASM_AddShamsiMonth(int Year, int Month, int Day, int Value)
    {
        string ShamsiDate = String.Format("{0}/{1}/{2}", Year, Month, Day);
        PersianCalendar pc = new PersianCalendar();
        DateTime dt = Convert.ToDateTime(GTS_ASM_ShamsiToMiladi(ShamsiDate).ToString());
        return GTS_ASM_MiladiToShamsi(pc.AddMonths(dt, Value).ToShortDateString()).ToString();

    }
};

