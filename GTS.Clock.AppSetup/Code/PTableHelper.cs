using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using GTS.Clock.AppSetup.DataSet;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.AppSetup
{
    public class PTableHelper
    {

        const string UsableColumns = @"Prc_PCode,Prc_Date,[dbo].[MinutesToTime](Prc_FirstIn) as Prc_FirstIn ,[dbo].[MinutesToTime] (Prc_FirstOut) as[Prc_FirstOut],[dbo].[MinutesToTime] (Prc_SecondIn) as[Prc_SecondIn],[dbo].[MinutesToTime] (Prc_LastOut) as [Prc_LastOut]
 ,Prc_NormWork ,Prc_PresenceWork,Prc_PureWork,Prc_PureDayWork,Prc_TotalWork,
 Prc_ValidAddWork,Prc_InvalidAddWork ,Prc_ValidLessWork,Prc_HourAbsence,Prc_DayAbsence
 ,Prc_HourMission ,Prc_DayMission,Prc_HourSleaveSalary,Prc_DaySleaveSalary
 ,Prc_HourEleaveSalary,Prc_DayEleaveSalary";
       public PTableHelper() 
       {
           
       }
       public SqlConnection Connection
       {
           get
           {
               SqlConnection sql=GTSAppSettings.SQLConnection;
               if (sql.State == ConnectionState.Closed) 
               {
                   sql.Open();
               }
               return sql;
           }
       }
            
        public void DeletePTable(string barcode, string tableName)
        {
            return;
            try
            {
                string sql = String.Format("delete from {0} where prc_pcode={1}", tableName, barcode.PadLeft(8, '0'));
                SqlCommand command = new SqlCommand(sql, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
        }

        public void DeletePTable( string tableName)
        {
            return;
            try
            {
                string sql = String.Format("delete from {0} ", tableName);
                SqlCommand command = new SqlCommand(sql, Connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
        }

        public DBDataSet.TA_PTableDataTable GetPTable1(string barcode, string tableName)
        {
            try
            {
                string sql = String.Format("SELECT * from {0} where prc_pcode={1}", tableName, barcode.PadLeft(8, '0'));
                SqlCommand command = new SqlCommand(sql, Connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DBDataSet.TA_PTableDataTable ptable = new DBDataSet.TA_PTableDataTable();
                adapter.Fill(ptable);                
                return ptable;
            }
            finally
            {
                Connection.Close();
            }
        }

        public DBDataSet.TA_PTableUsableColumnsDataTable GetPTableUsableColumns(string barcode, string tableName,string description)
        {
            try
            {
                string desc = "";
                if (description.Length > 0)
                {
                    desc = "'" + description + "' as [Operator],";
                }
                string sql = String.Format(@"SELECT {0}{1} from {2} where prc_pcode={3}", desc, UsableColumns, tableName, barcode.PadLeft(8, '0'));
                SqlCommand command = new SqlCommand(sql, Connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DBDataSet.TA_PTableUsableColumnsDataTable ptable = new DBDataSet.TA_PTableUsableColumnsDataTable();
                adapter.Fill(ptable);
                return ptable;
                
            }
            finally
            {
                Connection.Close();
            }
        }

        public DBDataSet.TA_PTableUsableColumnsDataTable GetPTableUsableColumns(string barcode, string fromDate,string toDate, string description,bool fromSavedTable)
        {
            try
            {
                string tableName1 = GetPtableFromDate(toDate);
                string tableName2 = GetPtableFromDate(fromDate);

                if (tableName1 == "" || tableName2 == "")
                    return null;

                if (fromSavedTable) 
                {
                    tableName1 += "_saved";
                    tableName2 += "_saved";
                }
                DBDataSet.TA_PTableUsableColumnsDataTable ptable = new DBDataSet.TA_PTableUsableColumnsDataTable();

                string desc = "";
                if (description.Length > 0)
                {
                    desc = "'" + description + "' as [Operator],";
                }
                string sql = String.Format(@"SELECT {0}{1} from {2} where prc_pcode={3} and  Prc_Date <='{4}' and  Prc_Date <>'{5}' order by Prc_Date  ", desc, UsableColumns, tableName1, barcode.PadLeft(8, '0'), toDate, GetMonthDate(toDate));
                SqlCommand command = new SqlCommand(sql, Connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DBDataSet.TA_PTableUsableColumnsDataTable ptable1 = new DBDataSet.TA_PTableUsableColumnsDataTable();
                adapter.Fill(ptable1);

                sql = String.Format(@"SELECT {0}{1} from {2} where prc_pcode={3} and  Prc_Date ='{4}' order by Prc_Date  ", desc, UsableColumns, tableName1, barcode.PadLeft(8, '0'), GetMonthDate(toDate));

                command.CommandText = sql;
                adapter = new SqlDataAdapter(command);
                DBDataSet.TA_PTableUsableColumnsDataTable monthPtable = new DBDataSet.TA_PTableUsableColumnsDataTable();
                adapter.Fill(monthPtable);

                if (tableName1 == tableName2)
                {
                    ptable = AppendPTable(monthPtable, ptable1); 
                }
                else
                {
                    sql = String.Format(@"SELECT {0}{1} from {2} where prc_pcode={3} and  Prc_Date >='{4}' order by Prc_Date  ", desc, UsableColumns, tableName2, barcode.PadLeft(8, '0'), fromDate);

                    command.CommandText = sql;
                    adapter = new SqlDataAdapter(command);
                    DBDataSet.TA_PTableUsableColumnsDataTable ptable2 = new DBDataSet.TA_PTableUsableColumnsDataTable();
                    adapter.Fill(ptable2);
                    ptable = AppendPTable(monthPtable, ptable2);
                    ptable = AppendPTable(ptable, ptable1);
                }
                return ptable;

            }
            finally
            {
                Connection.Close();
            }
        }
       

        public DBDataSet.TA_PTableUsableColumnsDataTable AppendPTable(DBDataSet.TA_PTableUsableColumnsDataTable table1, DBDataSet.TA_PTableUsableColumnsDataTable table2)
        {
            try
            {
                DBDataSet.TA_PTableUsableColumnsDataTable table = new DBDataSet.TA_PTableUsableColumnsDataTable();
                table.Merge(table1);
                table.Merge(table2);
                return table;
            }
            finally
            {
                Connection.Close();
            }
        }

   
        public DBDataSet.TA_PTableUsableColumnsDataTable AppendPTable(DBDataSet.TA_PTableUsableColumnsDataTable table1, DataView view)
        {
            try
            {
                DBDataSet.TA_PTableUsableColumnsDataTable table = new DBDataSet.TA_PTableUsableColumnsDataTable();
                table.Merge(table1);
                table.Merge(view.ToTable());
              

                return table;
            }
            finally
            {
                Connection.Close();
            }
        }


        /// <summary>
        /// delete CFP
        /// Insert CFP
        /// Calculate
        /// </summary>

        public void GTSCalculate(string barcode, decimal PrsId, string fromdate, string todate)
        {
            try
            {
                string pTableName1 = GetPtableFromDate(fromdate);
                string pTableName2 = GetPtableFromDate(todate);
                barcode = barcode.PadLeft(8, '0');
                DeletePTable(barcode, pTableName1);
                DeletePTable(barcode, pTableName2);
                DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter cfpTA = new DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter();
                cfpTA.Connection = GTSAppSettings.SQLConnection;
                //cfpTA.DeleteCFP(PrsId);
                cfpTA.UpdateCFP(Utility.ToMildiDate(fromdate), PrsId);
                string MiladifromDate = Utility.ToString(Utility.ToMildiDate(fromdate));
                //cfpTA.InsertCFP(PrsId, new DateTime(int.Parse(MiladifromDate.Split('/')[0]), int.Parse(MiladifromDate.Split('/')[1]), int.Parse(MiladifromDate.Split('/')[2])));
                ServiceReference1.TotalWebServiceClient total = new GTS.Clock.AppSetup.ServiceReference1.TotalWebServiceClient("BasicHttpBinding_ITotalWebService", GTSAppSettings.WebServiceAddress);
                if (GTSAppSettings.ClockCalculation)
                {
                    //total.Clock_FillByPersonBarCodeAndToDate(barcode, todate);
                }
                else
                {
                    string date = Utility.ToString(Utility.ToMildiDate(todate));
                    total.GTS_ExecuteByPersonIdAndToDate("InternalUtility", PrsId, new DateTime(int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[2])));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// execute all
        /// </summary>
        /// <param name="todate"></param>
        public void GTSCalculateAll(string todate)
        {
            try
            {
                ServiceReference1.TotalWebServiceClient total = new GTS.Clock.AppSetup.ServiceReference1.TotalWebServiceClient("BasicHttpBinding_ITotalWebService", GTSAppSettings.WebServiceAddress);
                if (GTSAppSettings.ClockCalculation)
                {
                    total.GTS_ExecuteAllByToDate("InternalUtility", PersianDateTime.Parse(todate).GregorianDate);
                }
                else 
                {
                    total.GTS_ExecuteAllByToDate("InternalUtility", PersianDateTime.Parse(todate).GregorianDate);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }      

        /// <summary>
        /// سطرهای جدول را دو به دو باهم مقایسه میکند.سطر اول را در نظر نمیگیریم
        /// </summary>
        /// <param name="table">جداول ادغام شده</param>
        /// <returns></returns>
        public bool[,] GetDifferenceIndex(DataTable  table,string barcode) 
        {
            int rowCount=table.Rows.Count;
            int celCount=table.Columns.Count;
            bool[,] matris = new bool[rowCount, celCount];
            for (int i = 1; i < celCount; i ++) 
            {
                matris[0, i] = false;
            }

            if (rowCount % 2 == 1)
            {
                for (int i = 1; i < rowCount; i += 2)
                {
                    for (int j = 0; j < celCount; j++)
                    {
                        if (table.Rows[i][j].ToString() != table.Rows[i + 1][j].ToString()
                            && !((table.Rows[i][j].ToString() == "0" && table.Rows[i+1][j].ToString() == "") ||
                                 (table.Rows[i][j].ToString() == "" && table.Rows[i+1][j].ToString() == "0")))
                        {
                            matris[i, j] = true;
                            matris[i + 1, j] = true;
                        }
                        else
                        {
                            matris[i, j] = false;
                            matris[i + 1, j] = false;
                        }
                    }
                }
            }
            else 
            {
                System.Windows.Forms.MessageBox.Show(barcode + " تعداد سطرها برای پیدا کردن اختلافات نادرست است ");
            }

            return matris;
        }

        public void InitTA_PTable()
        {
            string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='ta_ptable'";
            SqlCommand command = new SqlCommand(sql, Connection);
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0)
            {
                sql = @"CREATE TABLE [dbo].[TA_PTable](
	[Prc_PCode] [nvarchar](8) NOT NULL,
	[Prc_Date] [nvarchar](10) NOT NULL,
	[Prc_FirstIn] [nvarchar](100) NULL,
	[Prc_FirstOut] [nvarchar](100) NULL,
	[Prc_SecondIn] [nvarchar](100) NULL,
	[Prc_SecondOut] [nvarchar](100) NULL,
	[Prc_ThirdIn] [nvarchar](100) NULL,
	[Prc_ThirdOut] [nvarchar](100) NULL,
	[Prc_FourthIn] [nvarchar](100) NULL,
	[Prc_FourthOut] [nvarchar](100) NULL,
	[Prc_FifthIn] [nvarchar](100) NULL,
	[Prc_FifthOut] [nvarchar](100) NULL,
	[Prc_LastIn] [nvarchar](100) NULL,
	[Prc_LastOut] [nvarchar](100) NULL,
	[Prc_LastInOut] [nvarchar](100) NULL,
	[Prc_NormWork] [nvarchar](100) NULL,
	[Prc_NormWorkDay] [nvarchar](100) NULL,
	[Prc_PresenceWork] [nvarchar](100) NULL,
	[Prc_PresenceWorkDay] [nvarchar](100) NULL,
	[Prc_PresenceHolliday] [nvarchar](100) NULL,
	[Prc_PresenceHollidays] [nvarchar](100) NULL,
	[Prc_PresenceFriday] [nvarchar](100) NULL,
	[Prc_PresenceFridays] [nvarchar](100) NULL,
	[Prc_PresenceSpecial] [nvarchar](100) NULL,
	[Prc_WorkFridays] [nvarchar](100) NULL,
	[Prc_addnoworkday] [nvarchar](100) NULL,
	[Prc_TotalWork] [nvarchar](100) NULL,
	[Prc_PureWork] [nvarchar](100) NULL,
	[Prc_PureWorkKham] [nvarchar](100) NULL,
	[Prc_PureDayWork] [nvarchar](100) NULL,
	[Prc_PureWorkNight] [nvarchar](100) NULL,
	[Prc_PureWorkNights] [nvarchar](100) NULL,
	[Prc_standardwork] [nvarchar](100) NULL,
	[Prc_ValidTakhir] [nvarchar](100) NULL,
	[Prc_ValidTajil] [nvarchar](100) NULL,
	[Prc_ValidAddWork] [nvarchar](100) NULL,
	[Prc_ValidAddWorkHolliday] [nvarchar](100) NULL,
	[Prc_ValidAddWorkNoworkday] [nvarchar](100) NULL,
	[Prc_ValidaddWorkFriday] [nvarchar](100) NULL,
	[Prc_InValidaddWorkFriday] [nvarchar](100) NULL,
	[Prc_ValidaddWorkHolNoFr] [nvarchar](100) NULL,
	[Prc_InvalidaddWorkHolNoFr] [nvarchar](100) NULL,
	[Prc_ValidAddWorkbefore] [nvarchar](100) NULL,
	[Prc_ValidAddWorkafter] [nvarchar](100) NULL,
	[Prc_ValidAddWorkbein] [nvarchar](100) NULL,
	[Prc_keshik] [nvarchar](100) NULL,
	[Prc_janbaz] [nvarchar](100) NULL,
	[Prc_ValidAddWorkNight] [nvarchar](100) NULL,
	[Prc_ValidAddWorkFree] [nvarchar](100) NULL,
	[Prc_InvalidAddWork] [nvarchar](100) NULL,
	[Prc_TotalLessWork] [nvarchar](100) NULL,
	[Prc_ValidLessWork] [nvarchar](100) NULL,
	[Prc_BeinLessWork] [nvarchar](100) NULL,
	[Prc_TakhirLessWork] [nvarchar](100) NULL,
	[Prc_TajilLessWork] [nvarchar](100) NULL,
	[Prc_HourAbsence] [nvarchar](100) NULL,
	[Prc_DayAbsence] [nvarchar](100) NULL,
	[Prc_HourDayAbsence] [nvarchar](100) NULL,
	[Prc_DayAbsencePure] [nvarchar](100) NULL,
	[Prc_HourAbsencePure] [nvarchar](100) NULL,
	[Prc_AbsenceNaghes] [nvarchar](100) NULL,
	[Prc_HourleaveNoSalary] [nvarchar](100) NULL,
	[Prc_DayleaveNoSalary] [nvarchar](100) NULL,
	[Prc_HourDayleaveNoSalary] [nvarchar](100) NULL,
	[Prc_HourSleaveNoSalary] [nvarchar](100) NULL,
	[Prc_DaySleaveNoSalary] [nvarchar](100) NULL,
	[Prc_HourDaySleaveNoSalary] [nvarchar](100) NULL,
	[Prc_HourleaveSalary] [nvarchar](100) NULL,
	[Prc_HourleaveSalary23] [nvarchar](100) NULL,
	[Prc_HourleaveSalary24] [nvarchar](100) NULL,
	[Prc_HourleaveSalary25] [nvarchar](100) NULL,
	[Prc_HourleaveSalary26] [nvarchar](100) NULL,
	[Prc_HourleaveSalary27] [nvarchar](100) NULL,
	[Prc_DayleaveSalary] [nvarchar](100) NULL,
	[Prc_DayleaveSalary43] [nvarchar](100) NULL,
	[Prc_DayleaveSalary44] [nvarchar](100) NULL,
	[Prc_DayleaveSalary45] [nvarchar](100) NULL,
	[Prc_DayleaveSalary46] [nvarchar](100) NULL,
	[Prc_DayleaveSalary47] [nvarchar](100) NULL,
	[Prc_HourDayleaveSalary] [nvarchar](100) NULL,
	[Prc_HourEleaveSalary] [nvarchar](100) NULL,
	[Prc_HourEleaveSalarysum] [nvarchar](100) NULL,
	[Prc_DayEleaveSalary] [nvarchar](100) NULL,
	[Prc_HourDayEleaveSalary] [nvarchar](100) NULL,
	[Prc_HourSleaveSalary] [nvarchar](100) NULL,
	[Prc_DaySleaveSalary] [nvarchar](100) NULL,
	[Prc_HourDaySleaveSalary] [nvarchar](100) NULL,
	[Prc_HourMission] [nvarchar](100) NULL,
	[Prc_DayMission] [nvarchar](100) NULL,
	[Prc_HourDayMission] [nvarchar](100) NULL,
	[Prc_FullMission] [nvarchar](100) NULL,
	[Prc_FullHourMission] [nvarchar](100) NULL,
	[Prc_HourMission51] [nvarchar](100) NULL,
	[Prc_DayMission61] [nvarchar](100) NULL,
	[Prc_FullMission71] [nvarchar](100) NULL,
	[Prc_HourMission52] [nvarchar](100) NULL,
	[Prc_DayMission62] [nvarchar](100) NULL,
	[Prc_FullMission72] [nvarchar](100) NULL,
	[Prc_HourMission53] [nvarchar](100) NULL,
	[Prc_DayMission63] [nvarchar](100) NULL,
	[Prc_FullMission73] [nvarchar](100) NULL,
	[Prc_HourMission54] [nvarchar](100) NULL,
	[Prc_DayMission64] [nvarchar](100) NULL,
	[Prc_FullMission74] [nvarchar](100) NULL,
	[Prc_HourMission55] [nvarchar](100) NULL,
	[Prc_DayMission65] [nvarchar](100) NULL,
	[Prc_FullMission75] [nvarchar](100) NULL,
	[Prc_Shift1Count] [nvarchar](100) NULL,
	[Prc_Shift1time] [nvarchar](100) NULL,
	[Prc_Shift2Count] [nvarchar](100) NULL,
	[Prc_Shift2time] [nvarchar](100) NULL,
	[Prc_Shift3Count] [nvarchar](100) NULL,
	[Prc_Shift3time] [nvarchar](100) NULL,
	[Prc_Shift4Count] [nvarchar](100) NULL,
	[Prc_Shift4time] [nvarchar](100) NULL,
	[Prc_Shift5Count] [nvarchar](100) NULL,
	[Prc_Shift5time] [nvarchar](100) NULL,
	[Prc_Zahab] [nvarchar](100) NULL,
	[Prc_Ghaza] [nvarchar](100) NULL,
	[Prc_Type] [nvarchar](100) NULL,
	[Prc_Kind] [nvarchar](100) NULL,
	[Prc_28] [nvarchar](100) NULL,
	[Prc_shiftcode] [nvarchar](100) NULL,
	[Prc_Station] [nvarchar](100) NULL,
	[Prc_Reserve99] [nvarchar](100) NULL,
	[Prc_Changed] [nvarchar](100) NULL,
	[Prc_TakhirCnt] [nvarchar](100) NULL,
	[Prc_TakhirTotal] [nvarchar](100) NULL,
	[Prc_TakhirTotalJarimeh] [nvarchar](100) NULL,
	[Prc_addfreeRemark] [nvarchar](30) NULL,
	[Prc_scrtimes1] [nvarchar](100) NULL,
	[Prc_scrtimes2] [nvarchar](100) NULL,
	[Prc_scrtimes3] [nvarchar](100) NULL,
	[Prc_scrtimes4] [nvarchar](100) NULL,
	[Prc_scrtimes5] [nvarchar](100) NULL,
	[Prc_scrtimes6] [nvarchar](100) NULL,
	[Prc_scrtimes7] [nvarchar](100) NULL,
	[Prc_scrtimes8] [nvarchar](100) NULL,
	[Prc_scrtimes9] [nvarchar](100) NULL,
	[Prc_scrtimes10] [nvarchar](100) NULL,
	[Prc_scrdays1] [nvarchar](100) NULL,
	[Prc_scrdays2] [nvarchar](100) NULL,
	[Prc_scrdays3] [nvarchar](100) NULL,
	[Prc_scrdays4] [nvarchar](100) NULL,
	[Prc_scrdays5] [nvarchar](100) NULL,
	[Prc_scrdays6] [nvarchar](100) NULL,
	[Prc_scrdays7] [nvarchar](100) NULL,
	[Prc_scrdays8] [nvarchar](100) NULL,
	[Prc_scrdays9] [nvarchar](100) NULL,
	[Prc_scrdays10] [nvarchar](100) NULL
) ON [PRIMARY]
";
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }

            sql = "SELECT COUNT(*) FROM ta_ptable";
            command.CommandText = sql;
            count = Convert.ToInt32(command.ExecuteScalar());
            if (count % 2 == 0)
            {
                sql = "DELETE FROM ta_ptable";
                command.CommandText = sql;
                command.ExecuteNonQuery();
                sql = @"INSERT ta_ptable(Prc_PCode,Prc_Date,Prc_FirstIn,Prc_FirstOut,Prc_SecondIn,Prc_SecondOut,Prc_ThirdIn,Prc_ThirdOut,Prc_FourthIn,Prc_FourthOut,Prc_FifthIn,Prc_FifthOut,Prc_LastIn,Prc_LastOut,Prc_LastInOut,Prc_NormWork,Prc_NormWorkDay,Prc_PresenceWork,Prc_PresenceWorkDay,Prc_PresenceHolliday,Prc_PresenceHollidays,Prc_PresenceFriday,Prc_PresenceFridays,Prc_PresenceSpecial,Prc_WorkFridays,Prc_addnoworkday,Prc_TotalWork,Prc_PureWork,Prc_PureWorkKham,Prc_PureDayWork,Prc_PureWorkNight,Prc_PureWorkNights,Prc_standardwork,Prc_ValidTakhir,Prc_ValidTajil,Prc_ValidAddWork,Prc_ValidAddWorkHolliday,Prc_ValidAddWorkNoworkday,Prc_ValidaddWorkFriday,Prc_InValidaddWorkFriday,Prc_ValidaddWorkHolNoFr,Prc_InvalidaddWorkHolNoFr,Prc_ValidAddWorkbefore,Prc_ValidAddWorkafter,Prc_ValidAddWorkbein,Prc_keshik,Prc_janbaz,Prc_ValidAddWorkNight,Prc_ValidAddWorkFree,Prc_InvalidAddWork,Prc_TotalLessWork,Prc_ValidLessWork,Prc_BeinLessWork,Prc_TakhirLessWork,Prc_TajilLessWork,Prc_HourAbsence,Prc_DayAbsence,Prc_HourDayAbsence,Prc_DayAbsencePure,Prc_HourAbsencePure,Prc_AbsenceNaghes,Prc_HourleaveNoSalary,Prc_DayleaveNoSalary,Prc_HourDayleaveNoSalary,Prc_HourSleaveNoSalary,Prc_DaySleaveNoSalary,Prc_HourDaySleaveNoSalary,Prc_HourleaveSalary,Prc_HourleaveSalary23,Prc_HourleaveSalary24,Prc_HourleaveSalary25,Prc_HourleaveSalary26,Prc_HourleaveSalary27,Prc_DayleaveSalary,Prc_DayleaveSalary43,Prc_DayleaveSalary44,Prc_DayleaveSalary45,Prc_DayleaveSalary46,Prc_DayleaveSalary47,Prc_HourDayleaveSalary,Prc_HourEleaveSalary,Prc_HourEleaveSalarysum,Prc_DayEleaveSalary,Prc_HourDayEleaveSalary,Prc_HourSleaveSalary,Prc_DaySleaveSalary,Prc_HourDaySleaveSalary,Prc_HourMission,Prc_DayMission,Prc_HourDayMission,Prc_FullMission,Prc_FullHourMission,Prc_HourMission51,Prc_DayMission61,Prc_FullMission71,Prc_HourMission52,Prc_DayMission62,Prc_FullMission72,Prc_HourMission53,Prc_DayMission63,Prc_FullMission73,Prc_HourMission54,Prc_DayMission64,Prc_FullMission74,Prc_HourMission55,Prc_DayMission65,Prc_FullMission75,Prc_Shift1Count,Prc_Shift1time,Prc_Shift2Count,Prc_Shift2time,Prc_Shift3Count,Prc_Shift3time,Prc_Shift4Count,Prc_Shift4time,Prc_Shift5Count,Prc_Shift5time,Prc_Zahab,Prc_Ghaza,Prc_Type,Prc_Kind,Prc_28,Prc_shiftcode,Prc_Station,Prc_Reserve99,Prc_Changed,Prc_TakhirCnt,Prc_TakhirTotal,Prc_TakhirTotalJarimeh,Prc_addfreeRemark,Prc_scrtimes1,Prc_scrtimes2,Prc_scrtimes3,Prc_scrtimes4,Prc_scrtimes5,Prc_scrtimes6,Prc_scrtimes7,Prc_scrtimes8,Prc_scrtimes9,Prc_scrtimes10,Prc_scrdays1,Prc_scrdays2,Prc_scrdays3,Prc_scrdays4,Prc_scrdays5,Prc_scrdays6,Prc_scrdays7,Prc_scrdays8,Prc_scrdays9,Prc_scrdays10) 
                        VALUES('بارکد','تاریخ','اولین ورود','اولین خروج','دومین ورود','دومین خروج','سومین ورود','سومین خروج','چهارمین ورود','جهارمین خروج','پنجمین ورود','پنجمین خروج','آخرین ورود','آخرین خروج','آخرین ورود و خروج','کارکرد لازم','کارکرد لازم روزانه','مدت حضور','حضور روزانه','حضور روز تعطیل','مدت حضور روز تعطیل','تعداد روز حضور جمعه','مدت زمان حضور روز جمعه','حضور خاص','مدت زمان کار در روز جمعه','AddNoWorkDay','کارکرد ناخالص ساعتی','کارکرد خالص ساعتی','کارکرد خالص ساعتی خام','کارکرد خالص روزانه','کارکرد خاص شبانه','PureWorkNights','کارکرد استاندارد','تاخیر مجاز','تعجیل مجاز','اضافه کار مجاز','اضافه کار مجاز روز تعطیل','اضافه کار مجاز روز غیر کاری','اضافه کار مجاز جمعه','اضافه کار غیر مجاز جمعه','اضافه کار مجاز تعطیل رسمی','اضافه کار غیر مجاز تعطیل رسمی','اضافه کار قبل از وقت','اضافه کار بعد از وقت','اضافه کار بین وقت','کشیک','جانباز','اضافه کار مجاز شب','ValidAddWorkFree','اضافه کار غیر مجاز','مجموع کم کاری','غیبت مجاز ساعتی','غیبت بین وقت','غیبت اول وقت','غیبت آخر وقت','غیبت غیر مجاز ساعتی','غیبت روزانه','HourDayAbsence','غیبت خالص روزانه','غیبت خالص روزانه','غیبت ناقص','مرخصی ساعتی بدون حقوق','غیبت روزانه بدون حقوق','HourDayLeaveNoSalary','HoursLeaveNoSalary','DaySLeaveNoSalary','HourDaySleaveNoSalary','مرخصی ساعتی با حقوق','مرخصی ساعتی با حقوق 23','مرخصی ساعتی با حقوق 24','مرخصی ساعتی با حقوق 25','مرخصی ساعتی با حقوق 26','مرخصی ساعتی با حقوق 27','مرخصی روزانه با حقوق','مرخصی روزانه با حقوق 43','مرخصی روزانه با حقوق 44','مرخصی روزانه با حقوق 45','مرخصی روزانه با حقوق 46','مرخصی روزانه با حقوق 47','HourDayLeaveSalary','مرخصی استحقاقی ساعتی','مجموع مرخصی استحقاقی ساعتی','مرخصی استحقاقی روزانه','HourDayEleaveSalary','مرخصی استعلاجی ساعتی','مرخصی استعلاجی روزانه','HourDaySleaveSalary','ماموریت ساعتی','ماموریت روزانه','HourDayMission','FullMission','FullourMission','ماموریت ساعتی 51','ماموریت روزانه 61','FullMission71','ماموریت ساعتی 52','ماموریت روزانه 62','FullMission 72','ماموریت ساعتی 53','ماموریت روزانه 63','FullMission 73','ماموریت ساعتی 54','ماموریت روزانه 64','FullMission 74','ماموریت ساعتی 55','ماموریت روزانه 65','fullMission 75 ','Shift1Count','shift1time','Shift2Count','shift2time','Shift3Count','shift3time','Shift4Count','shift4time','Shift5Count','shift5time','ایاب ذهاب','غذا','دسته','نوع','پیشکارت 28','کد شیفت','Station','Ewserve99','changed','تعداد تاخیر','مجموع تاخیر','مجموع جریمه تاخیر','addfreeRemark','scrtimes1','scrtimes2','scrtimes3','scrtimes4','scrtimes5','scrtimes6','scrtimes7','scrtimes8','scrtimes9','scrtimes10','scrdays1','scrdays2','scrdays3','scrdays4','scrdays5','scrdays6','scrdays7','scrdays8','scrdays9','scrdays10')";
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public DBDataSet.TA_PTableUsableColumnsDataTable MinutesToTime(DBDataSet.TA_PTableUsableColumnsDataTable table) 
        {
            for (int i = 1; i < table.Rows.Count; i++) 
            {
                for (int j = 0; j < table.Columns.Count; j++) 
                {
                    if (IsHourlyColumn(j))
                    {
                        string col = table.Rows[i][j].ToString();
                        int intCol = 0;
                        if (col.Length > 0 && col != "-1000" && Int32.TryParse(col, out intCol) && intCol > 0)
                        {
                            if (table.Rows[i][1].ToString().Split('/')[2] == "00")
                            {
                                col = GTS.Clock.Infrastructure.Utility.Utility.IntTimeToTime(intCol);
                            }
                            else
                            {
                                col = GTS.Clock.Infrastructure.Utility.Utility.IntTimeToRealTime(intCol);
                            }
                            table.Rows[i][j] = col;
                        }
                    }
                }
            }
            return table;
        }

        private bool IsHourlyColumn(int index) 
        {
            switch (index)
            {
                case 0:
                case 1:
                case 8:
                case 14:
                case 16:
                case 18:
                case 20:
                    return false;
            }
            return true;
        }

        public void TransferPTableToTempTable(string ptable) 
        {
            string sql = String.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='{0}_saved'",ptable );
            SqlCommand command = new SqlCommand(sql, Connection);
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0)
            {
                sql = String.Format(@"select * into {0}_saved 
                                      from {0}", ptable);
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            else 
            {
                sql = String.Format(@"delete from {0}_saved", ptable);
                command.CommandText = sql;
                command.ExecuteNonQuery();

                sql = String.Format(@"insert into {0}_saved 
                                      select * from  {0}", ptable);
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public string GetPtableFromDate(string date)
        {
            //date=1388/08/05
            if (date.Length == 10)
            {
                string[] parts = date.Split('/');
                if (parts.Length == 3)
                {
                    return "p" + parts[0] + parts[1];
                }
            }

            return "";
        }

        public string GetMonthDate(string date)
        {
            //date=1388/08/05
            if (date.Length == 10)
            {
                string[] parts = date.Split('/');
                if (parts.Length == 3)
                {
                    return parts[0] + "/" + parts[1] + "/00";
                }
            }

            return "";
        }
    }
}
