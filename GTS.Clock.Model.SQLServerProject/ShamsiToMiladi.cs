using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString GTS_ASM_ShamsiToMiladi(string ShamsiDate)
    {
        try
        {
            string[] strs = ShamsiDate.Split('/');
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
            throw new Exception(String.Format("GTS Shamsi To Miladi Sended date: {0} {1}", ShamsiDate, e.Message));
        }
    }
};

