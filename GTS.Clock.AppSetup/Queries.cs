using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.AppSetup
{
    public partial class Queries : GTS.Clock.AppSetup.BaseForm
    {
        QueryExecuter exe = new QueryExecuter(null);
        public Queries()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ترددهای پردازش شده حذف شوند؟", "Proceed Traffic Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                exe.RunQuery("Update  TA_BaseTraffic set BasicTraffic_Used= 0");
                exe.RunQuery("delete from TA_ProceedTraffic ");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShiftsQuery q = new ShiftsQuery();
            q.ShowDialog();
        }

        private void Queries_Load(object sender, EventArgs e)
        {
            base.menuStrip1.Visible = false;
            if (GTSAppSettings.FromDate != null && GTSAppSettings.FromDate.Length > 0)
            {
                shamsiDateTB.Text = GTSAppSettings.FromDate;              
                barcodeTB.Text = GTSAppSettings.Barcode;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                GTSAppSettings.FromDate = shamsiDateTB.Text;             
                GTSAppSettings.Barcode = barcodeTB.Text;       

                GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter PersonTA = new GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter();
                GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.DBQuries queriesTA = new GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.DBQuries();
                DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter cfpTA = new DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter();

                PersonTA.Connection = GTSAppSettings.SQLConnection;
                
                decimal PersonId = (decimal)PersonTA.GetPrsID(barcodeTB.Text.PadLeft(8,'0'));
                
                cfpTA.Connection = GTSAppSettings.SQLConnection;
               
                string miladiDate = Utility.ToMildiDateString(shamsiDateTB.Text);
                cfpTA.UpdateCFP(Utility.ToMildiDateTime(miladiDate), PersonId);
                
                if (cfpTrafficCB.Checked)
                {
                    cfpTA.DeleteProcceedTraffic(PersonId, Utility.ToMildiDateTime(miladiDate));
                    cfpTA.InvalidateBasicTraffic(PersonId, Utility.ToMildiDateTime(miladiDate));
                    cfpTA.InvalidateTrafficPermits(PersonId, Utility.ToMildiDateTime(miladiDate));
                }
            
               
                MessageBox.Show("انجام شد");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا مطمئن هستید؟", "Change Password", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_SecurityUserTableAdapter userTA = new DataSet.GTSDBTableAdapters.TA_SecurityUserTableAdapter();
                userTA.Connection = GTSAppSettings.SQLConnection;
                //GTS.Clock.Infrastructure.Repository.UserRepository userRe = new Infrastructure.Repository.UserRepository(false);
                //GTS.Clock.Business.Security.BUser bus = new Business.Security.BUser();
                //IList<GTS.Clock.Model.Security.User> list = bus.GetAll();
                DataSet.GTSDB.TA_SecurityUserDataTable table = userTA.GetData();
                foreach (DataSet.GTSDB.TA_SecurityUserRow row in table)
                {
                    row.user_Password = GTS.Clock.Infrastructure.Utility.Utility.GetHashCode(row.user_Password);
                    userTA.Update(row);
                    //userRe.Update(user);
                }
                MessageBox.Show("با موفقیت انجام شد");
                this.Close();
            }
        }
    }
}
