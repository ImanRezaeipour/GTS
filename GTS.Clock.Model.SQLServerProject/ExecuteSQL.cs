using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read, 
            IsDeterministic = true, IsPrecise = true, 
            SystemDataAccess = SystemDataAccessKind.Read)]
    public static SqlInt32 GTS_ASM_ExecuteSQL(string SQL)
    {
        if (SQL == null)
            return -1000;

        if (SQL == "")
            return -1000;

        SqlInt32 ret = -1000;

        SqlConnection conn = new SqlConnection("Context Connection = True;");
        SqlCommand cmd = conn.CreateCommand();
        SqlDataReader r = null;

        try
        {
            conn.Open();
            cmd.CommandTimeout = 0;
            cmd.CommandText = SQL;
            r = cmd.ExecuteReader();

            if (r != null)
                if (r.Read())
                    if (r.FieldCount > 0 && !r.IsDBNull(0))
                        ret = Convert.ToInt32(r[0]);
        }
        finally
        {
            if (r != null)
                if (!r.IsClosed)
                    r.Close();

            if (conn.State != ConnectionState.Closed)
                conn.Close();
            cmd = null;
        }
        return ret;
    }


}

