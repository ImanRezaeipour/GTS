using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using GTS.Clock.AppSetup.DataSet;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.AppSetup
{
    public partial class CFP : GTS.Clock.AppSetup.BaseForm
    {
        public CFP()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GTSAppSettings.FromDate = startTimeTB.Text;
                GTSAppSettings.Barcode = barcodeTB.Text.PadLeft(8, '0');
                GTSAppSettings.PTableName = ptableTB.Text;

                GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter PersonTA = new GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter();
                decimal PersonId = (decimal)PersonTA.GetPrsID(barcodeTB.Text);

                PTableHelper helper = new PTableHelper();
                this.Cursor = Cursors.WaitCursor;


                DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter cfpTA = new DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter();
                cfpTA.Connection = GTSAppSettings.SQLConnection;
                string date = PersianDateTime.ShamsiToMiladi(startTimeTB.Text);

                cfpTA.InsertCFP(PersonId, new DateTime(int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[2])));
            }
            finally
            {
                this.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
      
        private void Calculator_Load(object sender, EventArgs e)
        {
            if (GTSAppSettings.PTableName != null)
            {
                startTimeTB.Text = GTSAppSettings.FromDate;
                barcodeTB.Text = GTSAppSettings.Barcode;
                ptableTB.Text = GTSAppSettings.PTableName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter cfpTA = new DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter();
            cfpTA.Connection = GTSAppSettings.SQLConnection;
            cfpTA.DeleteAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                GTSAppSettings.FromDate = startTimeTB.Text;
                GTSAppSettings.PTableName = ptableTB.Text;

                string sql = String.Format(@"insert into TA_Calculation_Flag_Persons 
                                        select Prc_PCode ,'{0}'  from {1} group by Prc_PCode", startTimeTB.Text, ptableTB.Text);
                QueryExecuter exe = new QueryExecuter(null);
                exe.RunQuery(sql, GTSAppSettings.SQLConnection);

                DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter cfpTA = new DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter();
                DBDataSet.TA_Calculation_Flag_PersonsDataTable cfpTable = new DBDataSet.TA_Calculation_Flag_PersonsDataTable();
                cfpTA.Connection = GTSAppSettings.SQLConnection;
                cfpTA.Fill(cfpTable);
                int cfpCount = cfpTable.Rows.Count;

                MessageBox.Show(String.Format("تعداد {0} درج شد", cfpCount.ToString()));
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
      
       
    }
}
