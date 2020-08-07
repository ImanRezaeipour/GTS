using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GTS.Clock.Infrastructure.Utility;


namespace GTS.Clock.AppSetup
{
    public partial class AppSet : GTS.Clock.AppSetup.BaseForm
    {

        public AppSet()
        {
            InitializeComponent();
        }

        private void AppSettings_Load(object sender, EventArgs e)
        {
            settingNamesCombo.DataSource = GTSAppSettings.GetExistSettings();
            GTSAppSettings.GetExistSettings();
            if (GTSAppSettings.SettingName != null && GTSAppSettings.SettingName.Length > 0)
            {
                configFileTB.Text = GTSAppSettings.SettingName;
            }
            base.menuStrip1.Visible = false;
            serverTB.Text = GTSAppSettings.SQLConnection.DataSource;
            DBTB.Text = GTSAppSettings.SQLConnection.Database;
            serviceRefTb.Text = GTSAppSettings.WebServiceAddress;
            logDBTB.Text = GTSAppSettings.LogSQLConnection.Database;
            logserverTB.Text = GTSAppSettings.LogSQLConnection.DataSource;
            logUserNameTB.Text = GTSAppSettings.LogUserName;
            logPasswordTB.Text = GTSAppSettings.LogPassword;
            usernameTB.Text = GTSAppSettings.GTSUserName;
            passwordTB.Text = GTSAppSettings.GTSPassword;
            if (GTSAppSettings.ClockCalculation)
                clockCalculationCB.Checked = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {

            string conStr = "";
            conStr = String.Format("Data Source={0};Initial Catalog={1};User ID={2};", serverTB.Text, DBTB.Text, usernameTB.Text);
            if (passwordTB.TextLength > 0)
            {
                conStr += "Password=" + passwordTB.Text + ";";
            }
            SqlConnection connection = new SqlConnection(conStr);
            GTSAppSettings.SQLConnection = connection;

            conStr = String.Format("Data Source={0};Initial Catalog={1};User ID={2};", logserverTB.Text, logDBTB.Text, logUserNameTB.Text);
            if (logPasswordTB.TextLength > 0)
            {
                conStr += "Password=" + logPasswordTB.Text + ";";
            }


            SqlConnection logconnection = new SqlConnection(conStr);
            GTSAppSettings.LogSQLConnection = logconnection;

            GTSAppSettings.WebServiceAddress = serviceRefTb.Text;

            GTSAppSettings.LogUserName = logUserNameTB.Text;
            GTSAppSettings.LogPassword = logPasswordTB.Text;
            GTSAppSettings.GTSUserName = usernameTB.Text;
            GTSAppSettings.GTSPassword = passwordTB.Text;
            GTSAppSettings.GTSDBName = DBTB.Text;
            GTSAppSettings.ClockCalculation = clockCalculationCB.Checked;

            GTSAppSettings.SaveToFile(configFileTB.Text);
            MessageBox.Show("ذخیره گردید");
            this.Close();
        }

        private void loadSaveBtn_Click(object sender, EventArgs e)
        {
            LoadData(configFileTB.Text);
            GTSAppSettings.SaveToFile(configFileTB.Text);
            MessageBox.Show("بارگذاری صورت گرفت");
            this.Close();
        }

        private void LoadData(string config)
        {
            GTSAppSettings.LoadFromFile(config);

            serverTB.Text = GTSAppSettings.SQLConnection.DataSource;
            DBTB.Text = GTSAppSettings.SQLConnection.Database;
            serviceRefTb.Text = GTSAppSettings.WebServiceAddress;
            logDBTB.Text = GTSAppSettings.LogSQLConnection.Database;
            logserverTB.Text = GTSAppSettings.LogSQLConnection.DataSource;
            logUserNameTB.Text = GTSAppSettings.LogUserName;
            logPasswordTB.Text = GTSAppSettings.LogPassword;
            usernameTB.Text = GTSAppSettings.GTSUserName;
            passwordTB.Text = GTSAppSettings.GTSPassword;
            GTSAppSettings.SettingName = config;

        }

        private void settingNamesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSetting = Utility.ToString(settingNamesCombo.SelectedItem);
            if (!Utility.IsEmpty(selectedSetting))
            {
                configFileTB.Text = selectedSetting;
                LoadData(selectedSetting);
            }
        }

        private void btnCopyNames_Click(object sender, EventArgs e)
        {
            logserverTB.Text = serverTB.Text;
            // DBTB.Text = logDBTB.Text;
            usernameTB.Text = logUserNameTB.Text;
            passwordTB.Text = logPasswordTB.Text;
        }

    }
}
