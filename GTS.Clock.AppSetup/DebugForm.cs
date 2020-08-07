using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using GTS.Clock.AppSetup.DataSet;

namespace GTS.Clock.AppSetup
{
    public partial class DebugForm : GTS.Clock.AppSetup.BaseForm
    {
        private bool loaded = false;
        public SqlConnection Connection 
        {
            get 
            {
                if (GTSAppSettings.LogSQLConnection.State == ConnectionState.Closed)
                {
                    GTSAppSettings.LogSQLConnection.Open();
                }
                return GTSAppSettings.LogSQLConnection;
            }            
        }
        GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.TA_ApplicationSettingsTableAdapter appSettingsTA = new GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.TA_ApplicationSettingsTableAdapter();
     
        public DebugForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [TA_EngineLog]", Connection);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM [TA_WinSvcLog]";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM [TA_Businesslog]";
                command.ExecuteNonQuery();

                MessageBox.Show("انجام شد");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GTSAppSettings.SQLConnection.State == ConnectionState.Closed)
            {
                GTSAppSettings.SQLConnection.Open();
            }      
            try
            {
                //Model.AppSetting.ApplicationSettings appSet = Business.AppSettings.BApplicationSettings.CurrentApplicationSettings;
                //appSet.RuleDebug = saveLogCheckBox.Checked;
                //appSet.RuleDurationDebug = ruleDurationCB.Checked;
                //appSet.BusinessLogEnable = busnessLogCB.Checked;
                //appSet.BusinessErrorLogEnable = busnessErrorLogCB.Checked;
                //Business.AppSettings.BApplicationSettings.Update(appSet);

                appSettingsTA.Connection = GTSAppSettings.SQLConnection;
                appSettingsTA.UpdateAppSettings(saveLogCheckBox.Checked, ruleDurationCB.Checked, busnessLogCB.Checked, busnessErrorLogCB.Checked);
                loaded = false;

                MessageBox.Show("ذخیره شد");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserActivityGroupBox.Visible = false;
            RuleLogFilterGB.Enabled = false;
            EngineLogGroupBox.Visible = false;
            winServiceGB.Visible = false;
            BusinessErrorLogGB.Visible = false;
            BusinessLogGB.Visible = false;
            if (logTypeCombo.SelectedIndex == 0) 
            {
                BusinessErrorLogGB.Visible = true; 
            }
            else if (logTypeCombo.SelectedIndex == 1)
            {
                RuleLogFilterGB.Enabled = true;                
                EngineLogGroupBox.Visible = true;
            }
            else if (logTypeCombo.SelectedIndex == 2)
            {
                UserActivityGroupBox.Visible = true;                
            }
            else if (logTypeCombo.SelectedIndex == 3)
            {
                winServiceGB.Visible = true;
            }
            else if (logTypeCombo.SelectedIndex == 4)
            {
                BusinessLogGB.Visible = true;
            }
            BindData();
        }

        private void BindData() 
        {
            try
            {
                string engineLogSelect = String.Format("select [PersonBarcode],[dbo].GTS_ASM_MiladiToShamsi(Convert(varchar(10),[Date],101)) [date],[Level],[Message],[Exception] from [TA_EngineLog] order by [Date] desc");
                string winLogSelect = String.Format("select [dbo].GTS_ASM_MiladiToShamsi(Convert(varchar(10),[WinSvcLog_Date],101)) [date],[WinSvcLog_Message],[WinSvcLog_Exception]  from [TA_WinSvcLog] order by [WinSvcLog_Date] desc");
                string bussinessErrorLog = String.Format("select [dbo].GTS_ASM_MiladiToShamsi(Convert(varchar(10),[Date],101)) [date],Username,ClientIPAddress,ClassName,MethodName,[Message],Exception,ExceptionSource from TA_Businesslog where Level='Error' order by [date],Username");
                string bussinessLog = String.Format("select [dbo].GTS_ASM_MiladiToShamsi(Convert(varchar(10),[Date],101)) [date],Username,ClientIPAddress,ClassName,MethodName,[Message],Exception,ExceptionSource from TA_Businesslog order by [date],Username");
                string userActionLog = String.Format("select Username,ClientIPAddress,[dbo].GTS_ASM_MiladiToShamsi(Convert(varchar(10),[Date],101)) [date],PageId,ClassName,MethodName,Action from [TA_UserActionLog] order by pageid,[date]");
                QueryExecuter exec = new QueryExecuter(null);
                userActivityGridView.DataSource = exec.RunQueryResult(userActionLog, Connection);
                engineLogGridView.DataSource = exec.RunQueryResult(engineLogSelect, Connection);
                winServiceGrid.DataSource = exec.RunQueryResult(winLogSelect, Connection);
                bussinessErrorGrid.DataSource = exec.RunQueryResult(bussinessErrorLog, Connection);
                bussinessLogGrid.DataSource = exec.RunQueryResult(bussinessLog, Connection);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                GTSAppSettings.FilterDate = filterDateTB.Text;
                GTSAppSettings.SaveToFile();
                string filter = "";
                if (filterDateTB.TextLength > 0 && conceptFilterTB.TextLength > 0)
                {
                    filter = String.Format(" where Message like ('%{0} %') and Message like ('% {1} %')", filterDateTB.Text, conceptFilterTB.Text);
                }
                else if (filterDateTB.TextLength > 0)
                {
                    filter = String.Format(" where Message like ('%{0} %')", filterDateTB.Text);
                }
                else if (conceptFilterTB.TextLength > 0)
                {
                    filter = String.Format(" where Message like ('% {0} %')", conceptFilterTB.Text);
                }
                QueryExecuter exec = new QueryExecuter(null);
                string engineLogSelect = String.Format("select [PersonBarcode],[dbo].GTS_ASM_MiladiToShamsi(Convert(varchar(10),[Date],101)) [date],[Level],[Message],[Exception] from [TA_EngineLog] order {1} order by [Date] desc", filter);
                engineLogGridView.DataSource = exec.RunQueryResult(engineLogSelect, Connection);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            if (!loaded)
            {
                appSettingsTA.Connection = GTSAppSettings.SQLConnection;
                DBDataSet.TA_ApplicationSettingsDataTable appSetTable = appSettingsTA.GetFirstRow();
                if (appSetTable.Rows.Count > 0)
                {
                    DBDataSet.TA_ApplicationSettingsRow setting = appSetTable.Rows[0] as DBDataSet.TA_ApplicationSettingsRow;

                    //Model.AppSetting.ApplicationSettings appSet = Business.AppSettings.BApplicationSettings.CurrentApplicationSettings;
                    busnessErrorLogCB.Checked = setting.appSet_BusinessErrorLog;
                    busnessLogCB.Checked = setting.appSet_BusinessLog;
                    ruleDurationCB.Checked = setting.appSet_RuleDurationDebug;
                    saveLogCheckBox.Checked = setting.appSet_RuleDebug;
                    loaded = true;
                }

            }
            if (GTSAppSettings.FilterDate != null) 
            {
                filterDateTB.Text = GTSAppSettings.FilterDate;
            }
            //DataSet.DBDataSetTableAdapters.TA_ApplicationSettingsTableAdapter appSetTA = new GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.TA_ApplicationSettingsTableAdapter();
            //appSetTA.Connection = GTSAppSettings.SQLConnection;
            //DataSet.DBDataSet.TA_ApplicationSettingsDataTable table = appSetTA.GetData();
            //if (table.Rows.Count > 0) 
            //{
            //    saveLogCheckBox.Checked = Convert.ToBoolean(table.Rows[0]["appSet_RuleDebug"]);
            //    ruleDurationCB.Checked = Convert.ToBoolean(table.Rows[0]["appSet_RuleDurationDebug"]);
            //}
        }

        

    }
}
