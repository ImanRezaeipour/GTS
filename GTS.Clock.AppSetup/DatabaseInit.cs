using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GTS.Clock.AppSetup
{
    public partial class DatabaseInit : GTS.Clock.AppSetup.BaseForm
    {
        public string ErrorMessages = "";

        public DatabaseInit()
        {
            InitializeComponent();
        }

        private void syncBtn_Click(object sender, EventArgs e)
        {
            GTS.Clock.AppSetup.QueryExecuter q = new GTS.Clock.AppSetup.QueryExecuter(this);
            q.SyncFiles();
            button1.Enabled = true;

        }

        public void UpdateStatusBar(string message)
        {
            toolStripStatusLabel1.Text = message;
            this.Update();
        }

        public void ShowLog(string itemName)
        {
            listBox1.Items.Add(itemName);
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            string strTip = "";

            //Get the item
            int nIdx = listBox1.IndexFromPoint(e.Location);
            if ((nIdx >= 0) && (nIdx < listBox1.Items.Count))
                strTip = listBox1.Items[nIdx].ToString();

            toolTip1.SetToolTip(listBox1, strTip);
        }

        private void DatabaseInit_Load(object sender, EventArgs e)
        {
            GTS.Clock.AppSetup.QueryExecuter q = new GTS.Clock.AppSetup.QueryExecuter(this);
            if (!q.CheckFolders())
            {
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ErrorMessages);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                GTS.Clock.AppSetup.QueryExecuter q = new GTS.Clock.AppSetup.QueryExecuter(this);
                q.RebuildObjects(asmCB.Checked);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void holidaysBtn_Click(object sender, EventArgs e)
        {
            Helper helper = new Helper();
            DataSet.DBDataSetTableAdapters.TA_HolidaysTemplateTableAdapter holidayTmpTA = new GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.TA_HolidaysTemplateTableAdapter();
            holidayTmpTA.Connection = GTSAppSettings.SQLConnection;
            DataSet.DBDataSet.TA_HolidaysTemplateDataTable holidayTable = new GTS.Clock.AppSetup.DataSet.DBDataSet.TA_HolidaysTemplateDataTable(); //holidayTmpTA.GetData();
            IList<DateTime> list = helper.GetHolidaysFromClockTable();
           
            foreach (DateTime date in list)
            {
                DataSet.DBDataSet.TA_HolidaysTemplateRow holiday = holidayTable.NewTA_HolidaysTemplateRow();
                holiday.TmpHlidy_Date = date;
                holidayTable.AddTA_HolidaysTemplateRow(holiday);
            }
            holidayTmpTA.ClearTable();
            holidayTmpTA.Update(holidayTable);
            MessageBox.Show("انجام شد");
        }
    }
}
