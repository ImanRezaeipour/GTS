using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString fn_CompileAssemblies()
    {
        GTS.Clock.Model.SQLServerProject.localhost.ExeceuteCompilerService executeCompiler =
            new GTS.Clock.Model.SQLServerProject.localhost.ExeceuteCompilerService();
        executeCompiler.CompileAssemblies();
        return new SqlString("Complie Service Was Called!");
    }
};

