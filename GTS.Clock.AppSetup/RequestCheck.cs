using System;
using System.Drawing;
using System.Windows.Forms;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.AppSetup.DataSet;

namespace GTS.Clock.AppSetup
{
    public partial class RequestCheck : BaseForm
    {
        public RequestCheck()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.IsEmpty(barcodeTB.Text))
                {
                    GTSAppSettings.FromDate = fromDataTB.Text;
                    GTSAppSettings.ToDate = toDataTB.Text;
                    GTSAppSettings.Barcode = barcodeTB.Text;
                    GTSAppSettings.SaveToFile();

                    GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.TA_PersonTableAdapter prsTA = new DataSet.DBDataSetTableAdapters.TA_PersonTableAdapter();
                    DBDataSet.TA_PersonDataTable table = prsTA.GetDataByBarcode(GTSAppSettings.Barcode);
                    if (table != null && table.Rows.Count > 0) 
                    {
                        prsNameLbl.Text = (table.Rows[0] as DBDataSet.TA_PersonRow).Prs_FirstName + " " + (table.Rows[0] as DBDataSet.TA_PersonRow).Prs_LastName;
                    }                    


                    #region Query

                    QueryExecuter exec = new QueryExecuter(null);
                    string requestSelect = String.Format(@"
select request_PrecardID Pishcard,[dbo].[MinutesToTime](convert(varchar(10), request_FromTime,101)) FromTime,[dbo].[MinutesToTime](convert(varchar(10), request_ToTime,101)) ToTime,dbo.GTS_ASM_MiladiToShamsi(convert(varchar(10), request_FromDate,101))FromDate,dbo.GTS_ASM_MiladiToShamsi(convert(varchar(10),request_ToDate,101))ToDate,
[dbo].[MinutesToTime](convert(varchar(10) , request_TimeDuration,101))TimeDuration,mng.Prs_FirstName + ' ' + mng.Prs_LastName as Manager,Flow_FlowName Flow,reqStat_Confirm Confirm,reqStat_EndFlow [End],reqStat_IsDeleted [Delete]
from TA_Request
left outer join TA_RequestStatus on reqStat_RequestID=request_ID
join TA_ManagerFlow on mngrFlow_ID=reqStat_MnagerFlowID
join TA_Flow on Flow_ID=mngrFlow_FlowID
join TA_Manager on MasterMng_ID=mngrFlow_ManagerID
join TA_Person as mng on mng.Prs_ID=MasterMng_PersonID
WHERE request_FromDate >= '{0}'
									AND 
								   request_ToDate <= '{1}'
								    AND
								   request_PersonID=dbo.GetPerson('{2}') ", 
 Utility.ToMildiDateString(fromDataTB.Text), Utility.ToMildiDateString(toDataTB.Text), GTSAppSettings.Barcode);

                    object result = exec.RunQueryResult(requestSelect, GTSAppSettings.SQLConnection);
                    dataGridView1.DataSource = result;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RequestCheck_Load(object sender, EventArgs e)
        {
            if (GTSAppSettings.FromDate != null && GTSAppSettings.FromDate.Length > 0)
            {
                fromDataTB.Text = GTSAppSettings.FromDate;
                toDataTB.Text = GTSAppSettings.ToDate;
                barcodeTB.Text = GTSAppSettings.Barcode;

            }
        }

        private void RequestCheck_Resize(object sender, EventArgs e)
        {
            int mergin = 15;
            int windowHight = this.Height - topMetginHeight;
            int windowWdth = this.Width;
            gridContainerGrpBox.Height = windowHight - groupBox1.Height - 50;
            gridContainerGrpBox.Width = windowWdth - mergin;
            groupBox1.Width = windowWdth - mergin;
            groupBox1.Location = new Point(groupBox1.Location.X, topMetginHeight);
        }
    }
}
