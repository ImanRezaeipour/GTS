using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using GTS.Clock.AppSetup.DataSet;
using GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters;
using GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters;
using GTS.Clock.AppSetup.DataSet.ClockDataSetTableAdapters;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.AppSetup
{
    public class QueryExecuter
    {
        DatabaseInit _page;
        SqlConnection connectionString;
        //const string FN_PATH = @"\Queries\Functions\";
        //const string TR_PATH = @"\Queries\Triggers\";
        //const string P_PATH = @"\Queries\StoredProcedures\";
        const string P_QUERY = @"\Query\Templates\";
        //const string ASM_PATH = @"\Queries\CLRAssembly\";

        public QueryExecuter(DatabaseInit page)
        {
            connectionString = GTSAppSettings.SQLConnection;
            _page = page;
        }

        public bool RunQuery(string query)
        {
            try
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    connectionString.Open();
                }
                SqlCommand command = new SqlCommand(query, connectionString);
                command.CommandTimeout = 20000;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_page != null)
                {
                    _page.ErrorMessages += String.Format("Querry : {0} \r\n Message:{1} \r\n ------------- \r\n", query, ex.Message);
                }
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                connectionString.Close();
            }
        }

        public bool RunQuery(string query, SqlConnection sqlConnection)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.CommandTimeout = 20000;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_page != null)
                {
                    _page.ErrorMessages += String.Format("Querry : {0} \r\n Message:{1} \r\n ------------- \r\n", query, ex.Message);
                }
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool CheckHasIdentity(string tableName) 
        {
            try
            {
                string query = String.Format(@"select Count(o.name) as [RowCount]
                                                from syscolumns c, sysobjects o
                                                where c.status & 128 = 128
                                                and o.id = c.id and o.name='{0}';", tableName);
                if (connectionString.State == ConnectionState.Closed)
                {
                    connectionString.Open();
                }
                SqlCommand command = new SqlCommand(query, connectionString);
                command.CommandTimeout = 20000;
                int count = Utility.ToInteger(command.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                if (_page != null)
                {
                    _page.ErrorMessages += String.Format("Check Identity : {0} \r\n Message:{1} \r\n ------------- \r\n", tableName, ex.Message);
                }
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                connectionString.Close();
            }
        }

        public DataTable RunQueryResult(string query, SqlConnection sqlConnection)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                DataTable result = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);

                adapter.Fill(result);

                return result;
            }
            catch (Exception ex)
            {
                if (_page != null)
                {
                    _page.ErrorMessages += String.Format("Querry : {0} \r\n Message:{1} \r\n ------------- \r\n", query, ex.Message);
                }
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool ExsistTrigger(string triggerName)
        {
            string type = "TR";
            ObjectChecker checker = new ObjectChecker();
            int count = (int)checker.ExsistsObject(type, triggerName);
            if (count.Equals(1))
            {
                return true;
            }
            return false;
        }
        public bool ExsistFunction(string functionName)
        {
            string type = "FN";
            ObjectChecker checker = new ObjectChecker();
            int count = (int)checker.ExsistsObject(type, functionName);
            if (count.Equals(1))
            {
                return true;
            }
            return false;
        }
        public bool ExsistStoredProcedure(string SPName)
        {
            string type = "P";
            ObjectChecker checker = new ObjectChecker();
            int count = (int)checker.ExsistsObject(type, SPName);
            if (count.Equals(1))
            {
                return true;
            }
            return false;
        }

        public void DeleteObjects()
        {
            string query = @"
declare @name varchar(200),@query varchar(500),@type varchar(200) 
	DECLARE xxx CURSOR 
	FOR SELECT objectname,objectType 
	FROM ta_objectordersetup
	
	OPEN xxx
	FETCH NEXT FROM xxx
	INTO @name,@type

		WHILE @@FETCH_STATUS = 0
	BEGIN		
	
IF  EXISTS (SELECT * FROM sysobjects WHERE name = @name AND type = @type)
	BEGIN
	IF @type in( 'FN','TF','IF')
	 exec ('DROP FUNCTION [' + @name + ']')
	 ELSE IF @type = 'P'
	 exec ('DROP PROCEDURE [' + @name + ']')
	 ELSE IF @type = 'TR'
	 exec ('DROP TRIGGER [' + @name + ']')	
	END
	ELSE IF @type = 'Q' and @name like('%template%')
	 	 exec('DELETE FROM [' + @name + ']')
	 
	FETCH NEXT FROM xxx
		
		INTO @name,@type
	END
	CLOSE xxx
	DEALLOCATE xxx";

            RunQuery(query);

            connectionString.Close();
        }

        public void RebuildObjects(bool asmBuild)
        {
            int fnCounter = 0, spCounter = 0, trCounter = 0;
            _page.UpdateStatusBar("حذف موجویتهای موجود");
            DeleteObjects();
            _page.UpdateStatusBar("حذف انجام شد");
            BuildObjectOrder();
            TA_ObjectOrderSetupTableAdapter ta = new TA_ObjectOrderSetupTableAdapter();
            ta.Connection = GTSAppSettings.SQLConnection;
            GTSDB.TA_ObjectOrderSetupDataTable table = ta.GetAllOrderByOrder();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i]["ObjectName"].ToString();
                string fileName = table.Rows[i]["FileName"].ToString();
                string type = table.Rows[i]["ObjectType"].ToString().ToUpper();
                string logMsg = name;

                _page.UpdateStatusBar(String.Format("ساخت موجودیت : {0} {1}", name, type));
               /* if (type.Equals("FN") || type.Equals("TF") || type.Equals("IF"))
                {
                    string path = System.Environment.CurrentDirectory + FN_PATH + fileName;
                    if (File.Exists(path))
                    {
                        using (StreamReader reader = new StreamReader(path))
                        {
                            string query = reader.ReadToEnd().ToLower();
                            query = RemoveExtraChars(query);
                            query = query.Replace("alter function", "create function");
                            if (RunQuery(query))
                                fnCounter++;
                            else
                                logMsg += " ناموفق";
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("File Not Fount:" + path);
                    }
                }
                else if (type.Equals("P"))
                {
                    string path = System.Environment.CurrentDirectory + P_PATH + fileName;
                    if (File.Exists(path))
                    {
                        using (StreamReader reader = new StreamReader(path))
                        {
                            string query = reader.ReadToEnd().ToLower();
                            query = RemoveExtraChars(query);
                            query = query.Replace("alter proc", "create proc");
                            query = query.Replace("alter procedure", "create procedure");
                            if (RunQuery(query))
                                spCounter++;
                            else
                                logMsg += " ناموفق";
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("File Not Fount:" + path);
                    }
                }

                else if (type.Equals("TR"))
                {
                    string path = System.Environment.CurrentDirectory + TR_PATH + fileName;
                    if (File.Exists(path))
                    {
                        using (StreamReader reader = new StreamReader(path))
                        {
                            string query = reader.ReadToEnd().ToLower();
                            query = RemoveExtraChars(query);
                            query = query.Replace("alter trigger", "create trigger");
                            if (RunQuery(query))
                                trCounter++;
                            else
                                logMsg += " ناموفق";
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("File Not Fount:" + path);
                    }
                }
                else */if (type.Equals("Q"))
                {
                    string path = System.Environment.CurrentDirectory + P_QUERY + fileName;
                    if (File.Exists(path))
                    {
                        using (StreamReader reader = new StreamReader(path))
                        {
                            string query = reader.ReadToEnd();
                            query = RemoveExtraChars(query);
                            query = query.Insert(0, String.Format("DELETE FROM {0};", name));
                            if (CheckHasIdentity(name))
                            {
                                query = query.Insert(0, String.Format(@"SET IDENTITY_INSERT  dbo.{0} ON;", name));
                                query = query.Insert(query.Length, String.Format(";SET IDENTITY_INSERT  dbo.{0} OFF;", name));

                            }

                            if (RunQuery(query))
                                trCounter++;
                            else
                                logMsg += " ناموفق";
                        }

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("File Not Fount:" + path);
                    }
                }

                else if (type.Equals("ASM") && asmBuild)
                {
                    CreateCLRAssembly();
                }
                _page.ShowLog(logMsg);

            }
            _page.UpdateStatusBar("اتمام با موفقیت - " + String.Format("Finished:Functions {0} ,Procedures {1} ,Triggers {2}", fnCounter, spCounter, trCounter));


        }

        private string RemoveExtraChars(string query)
        {
            query = query.Replace("\r\ngo ", " ");
            query = query.Replace("\r\nGO ", " ");
            query = query.Replace(" go ", " ");
            query = query.Replace(" GO ", " ");
            query = query.Replace("\r\ngo\r\n", " ");
            query = query.Replace("\r\nGO\r\n", " ");
            query = query.Replace("\r\ngo", " ");
            query = query.Replace("\r\nGO", " ");
            query = query.Replace("n'", "N'");
            return query;
        }

        public bool CheckFolders()
        {
          /*  string path1 = System.Environment.CurrentDirectory + P_PATH;
            string path2 = System.Environment.CurrentDirectory + FN_PATH;
            string path3 = System.Environment.CurrentDirectory + TR_PATH;*/
            string path4 = System.Environment.CurrentDirectory + P_QUERY;
            if (/*!Directory.Exists(path1) || !Directory.Exists(path2) || !Directory.Exists(path3) || */!Directory.Exists(path4))
            {
                System.Windows.Forms.MessageBox.Show("Some Folders are not exsits.You Must Sync Files To Rebuild Them");
                return false;
            }

            return true;

        }

        public void SyncFiles()
        {
            int count = 0;
            if (System.Configuration.ConfigurationSettings.AppSettings["SourceFilesPath"] != null)
            {
                string path = System.Configuration.ConfigurationSettings.AppSettings["SourceFilesPath"];
                if (Directory.Exists(path))
                {
                    //Directory.CreateDirectory(System.Environment.CurrentDirectory + TR_PATH);
                    //Directory.CreateDirectory(System.Environment.CurrentDirectory + FN_PATH);
                    //Directory.CreateDirectory(System.Environment.CurrentDirectory + P_PATH);
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + P_QUERY);
                    //Directory.CreateDirectory(System.Environment.CurrentDirectory + ASM_PATH);

                  /*  string p = path + "\\Triggers";
                    string[] files = Directory.GetFiles(p, "*.sql", SearchOption.AllDirectories);
                    count += files.Count();
                    foreach (string file in files)
                    {
                        File.Copy(file, System.Environment.CurrentDirectory + TR_PATH + new FileInfo(file).Name, true);
                    }
                    */
                    string p = path + P_QUERY;

                    string[] files = Directory.GetFiles(p, "*.sql", SearchOption.AllDirectories);
                    count += files.Count();
                    foreach (string file in files)
                    {
                        File.Copy(file, System.Environment.CurrentDirectory + P_QUERY + new FileInfo(file).Name, true);

                    }
/*
                    p = path + "\\Stored Procedure";
                    files = Directory.GetFiles(p, "*.sql", SearchOption.AllDirectories);
                    count += files.Count();
                    foreach (string file in files)
                    {
                        File.Copy(file, System.Environment.CurrentDirectory + P_PATH + new FileInfo(file).Name, true);
                    }

                    p = path + "\\Functions";
                    files = Directory.GetFiles(p, "*.sql", SearchOption.AllDirectories);
                    count += files.Count();
                    foreach (string file in files)
                    {
                        File.Copy(file, System.Environment.CurrentDirectory + FN_PATH + new FileInfo(file).Name, true);
                    }
                    
                    if (System.Configuration.ConfigurationSettings.AppSettings["AsmSQLSourceFilesPath"] != null)
                    {
                        path = System.Configuration.ConfigurationSettings.AppSettings["AsmSQLSourceFilesPath"];
                        p = path;
                        files = Directory.GetFiles(p, "*.sql", SearchOption.AllDirectories);
                        count += files.Count();
                        foreach (string file in files)
                        {
                            File.Copy(file, System.Environment.CurrentDirectory + ASM_PATH + new FileInfo(file).Name, true);
                        }
                    }

                    if (System.Configuration.ConfigurationSettings.AppSettings["DLLSourceFilesPath"] != null)
                    {
                        path = System.Configuration.ConfigurationSettings.AppSettings["DLLSourceFilesPath"];
                        p = path;
                        files = Directory.GetFiles(p, "*.dll", SearchOption.AllDirectories);
                        count += files.Count();
                        foreach (string file in files)
                        {
                            File.Copy(file, System.Environment.CurrentDirectory + ASM_PATH + new FileInfo(file).Name, true);
                        }
                    }
 * */
                    _page.UpdateStatusBar(" همزمانی انجام شد " + count.ToString() + " Files");
                }
            }
            else
            {
                _page.UpdateStatusBar("Source Folder Not Found!");
            }
        }

        public void BuildObjectOrder()
        {
            try
            {
                string path = System.Environment.CurrentDirectory + P_QUERY + "ObjectOrderSetup.sql";
                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);
                    string query = reader.ReadToEnd().ToLower();
                    query = RemoveExtraChars(query);
                    query = query.Insert(0, "DELETE FROM ta_objectordersetup;");
                    if (!RunQuery(query))
                    {
                        MessageBox.Show("Object Order Settings Executing Problem");
                    }
                    else
                    {
                        _page.UpdateStatusBar("Object Order Settings Success");

                    }
                }
                else
                {
                    MessageBox.Show("Object Order Settings File Is Not Exsits");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Object Order Settings Executing Problem :" + ex.Message);
            }
        }

        public void CreateCLRAssembly()
        {
           
          string DLLSourceFilesPath= System.Configuration.ConfigurationSettings.AppSettings["DLLSourceFilesPath"];

            #region 1

            string sql1 = @"EXEC dbo.sp_changedbowner @loginame = N'sa', @map = false

if  exists(select * from sysobjects where name='CompileAssemblies')
	drop procedure CompileAssemblies
	
if  exists(select * from sysobjects where name='FillPFile')
	drop procedure FillPFile
	
if  exists(select * from sysobjects where name='ExecuteSQL')
	drop function ExecuteSQL		
	
if  exists(select * from sysobjects where name='MiladiToShamsi')
	drop function MiladiToShamsi	
	
if  exists(select * from sysobjects where name='ShamsiToMiladi')
	drop function ShamsiToMiladi	
	
if  exists(select * from sysobjects where name='AddShamsiDay')
	drop function AddShamsiDay	
		
if  exists(select * from sysobjects where name='AddShamsiMonth')
	drop function AddShamsiMonth	

if exists(SELECT name from sys.assemblies where name='GTSAssemblyXmlSerializers')
	DROP ASSEMBLY GTSAssemblyXmlSerializers		

if exists(SELECT name from sys.assemblies where name='GTSAssembly')
	DROP ASSEMBLY GTSAssembly		";

            sql1 = RemoveExtraChars(sql1.ToLower());

            RunQuery(sql1); 
            #endregion

            #region 2
            sql1 = @"EXEC sp_configure 'show advanced options' , '1';
go
reconfigure;
go
EXEC sp_configure 'clr enabled' , '1'
go
reconfigure;
EXEC sp_configure 'show advanced options' , '0';
go
declare @DBName varchar(50)
set @DBName='"+GTSAppSettings.SQLConnection.Database+@"'
Execute ('ALTER DATABASE ' +  @DBName + ' SET TRUSTWORTHY ON;')	";

            sql1 = RemoveExtraChars(sql1.ToLower());
            RunQuery(sql1); 
            #endregion

            #region 3
            sql1 = @"CREATE ASSEMBLY GTSAssembly
   FROM '" + DLLSourceFilesPath + @"\GTS.Clock.Model.SQLServerProject.dll'
   WITH PERMISSION_SET = UNSAFE;	";

            sql1 = RemoveExtraChars(sql1.ToLower());
            RunQuery(sql1); 
            #endregion

            #region 4
            sql1 = @"CREATE ASSEMBLY GTSAssemblyXmlSerializers
    FROM '" + DLLSourceFilesPath + @"\GTS.Clock.Model.SQLServerProject.XmlSerializers.dll'
    WITH PERMISSION_SET = UNSAFE;	";

            sql1 = RemoveExtraChars(sql1.ToLower());
            RunQuery(sql1); 
            #endregion

            #region 5
            sql1 = @"CREATE FUNCTION ExecuteSQL(@SQL NVARCHAR(MAX))
RETURNS INT
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.ExecuteSQL;	";

            sql1 = RemoveExtraChars(sql1);
            RunQuery(sql1);
            #endregion

            #region 6
            sql1 = @"CREATE FUNCTION MiladiToShamsi(@GregoriandDate NVARCHAR(10))
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.MiladiToShamsi;	";

            sql1 = RemoveExtraChars(sql1);
            RunQuery(sql1);
            #endregion

            #region 7
            sql1 = @"CREATE FUNCTION ShamsiToMiladi(@ShamsiDate NVARCHAR(10))
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.ShamsiToMiladi;	";

            sql1 = RemoveExtraChars(sql1);
            RunQuery(sql1);
            #endregion

            #region 8
            sql1 = @"CREATE FUNCTION AddShamsiDay(@Year int, @Month int, @Day int, @Value int)
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.AddShamsiDay;	";

            sql1 = RemoveExtraChars(sql1);
            RunQuery(sql1);
            #endregion

            #region 9
            sql1 = @"CREATE FUNCTION AddShamsiMonth(@Year int, @Month int, @Day int, @Value int)
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.AddShamsiMonth;";

            sql1 = RemoveExtraChars(sql1);
            RunQuery(sql1);
            #endregion
        }
    }
}
