using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlInt32 ExecuteByPersonBarcode(SqlString barcode)
    {
        try
        {
            GTS.Clock.Model.SQLServerProject.localhost.TotalWebService service = new GTS.Clock.Model.SQLServerProject.localhost.TotalWebService();
            service.FillByPersonBarCode(barcode.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return 0;
    }
};

