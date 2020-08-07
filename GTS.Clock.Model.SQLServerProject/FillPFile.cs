using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void FillPFile(string str)
    {
        try
        {
            GTSWebServiceProxy.TotalWebService totalServices =
                new GTSWebServiceProxy.TotalWebService();
            totalServices.FillByPersonBarCode(str);
                     
        }
        catch (Exception ex)
        {
            throw new Exception("FillPFile StoredProcedure Exception " + ex.Message);
        }


    }
};
