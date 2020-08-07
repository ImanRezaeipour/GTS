using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.Infrastructure.Utility;


namespace GTS.Clock.AppSetup
{
    public partial class CalculationCompairer : BaseForm
    {
        private object compaireSource = null;
        private object calculate = null;
        private object proccedTraffic = null;
        private object basicTraffic = null;
        private object wthoutCalc = null;
        private object requestOK = null;
        private object permits = null;
        static string diffCount = "0";
        bool[,] deffMat;
       
        public string Barcode
        {
            get
            {
                return Utility.ToInteger(barcodeTB.Text).ToString();
            }
        }

        public CalculationCompairer()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.TA_PTable' table. You can move, or remove it, as needed.
            if (GTSAppSettings.FromDate != null && GTSAppSettings.FromDate.Length > 0)
            {
                fromDataTB.Text = GTSAppSettings.FromDate;
                toDataTB.Text = GTSAppSettings.ToDate;
                barcodeTB.Text = GTSAppSettings.Barcode;
             
            }
            actionTypeCombo.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {                       
            try
            {
                //PTableHelper helper = new PTableHelper();
                //helper.GetPtableFromDate("1388/08/05");
                //return;
                GTSAppSettings.FromDate = fromDataTB.Text;
                GTSAppSettings.ToDate = toDataTB.Text;
                GTSAppSettings.Barcode = Barcode;            
                GTSAppSettings.SaveToFile();
                switch (actionTypeCombo.SelectedIndex)
                {                   
                    //case 0:
                    //    difCountLbl.Text = "0";
                    //    difCountLbl.Enabled = false;
                    //    WaitNow(this.CalculateAndDisplay);
                    //    break;
                    //case 1:
                    //    difCountLbl.Text = "0";
                    //    difCountLbl.Enabled = false;
                    //    WaitNow(this.Display);
                    //    break;                    
                    case 0:
                        difCountLbl.Text = "0";
                        difCountLbl.Enabled = false;
                        DisplayProceedTraffic();
                        break;
                    case 1:
                        difCountLbl.Text = "0";
                        difCountLbl.Enabled = false;
                        DisplayBasicTraffic();
                        break;
                    case 2:
                        difCountLbl.Text = "0";
                        difCountLbl.Enabled = false;
                        DisplayRequests();
                        break;
                    case 3:
                        difCountLbl.Text = "0";
                        difCountLbl.Enabled = false;
                        DisplayPermits();
                        break;
                    //case 6:
                    //    difCountLbl.Enabled = true;
                    //    WaitNow(this.Compaire);
                    //    break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            int mergin = 15;
            int windowHight = this.Height - topMetginHeight;
            int windowWdth = this.Width;
            gridContainerGrpBox.Height = windowHight - groupBox1.Height - 50;
            gridContainerGrpBox.Width = windowWdth - mergin;
            groupBox1.Width = windowWdth - mergin;
            groupBox1.Location = new Point(groupBox1.Location.X, topMetginHeight);
        }

        private void Compaire(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            try
            {
                GTSAppSettings.FromDate = fromDataTB.Text;
                GTSAppSettings.ToDate = toDataTB.Text;
                GTSAppSettings.Barcode = Barcode;              
                GTSAppSettings.SaveToFile();
              
                PTableHelper helper = new PTableHelper();


                helper.InitTA_PTable();

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ta_ptable = new DataSet.DBDataSet.TA_PTableUsableColumnsDataTable();
                DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter ptableTA = new DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter();
                ptableTA.Connection = GTSAppSettings.SQLConnection;
                ptableTA.Fill(ta_ptable);

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ptable1 = helper.GetPTableUsableColumns(Barcode, fromDataTB.Text, toDataTB.Text, "Clock6", false);

                GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter PersonTA = new GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter();
                decimal PersonId = (decimal)PersonTA.GetPrsID(this.Barcode);

                helper.GTSCalculate(Barcode, PersonId, fromDataTB.Text, toDataTB.Text);

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ptable2 = helper.GetPTableUsableColumns(Barcode, fromDataTB.Text, toDataTB.Text, "GTS", false);

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ptable = helper.AppendPTable(ptable1, ptable2);

                ptable.DefaultView.Sort = "prc_date";
                ta_ptable = helper.AppendPTable(ta_ptable, ptable.DefaultView);

                ta_ptable = helper.MinutesToTime(ta_ptable);

                compaireSource = ta_ptable;
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new MethodInvoker(delegate { dataGridView1.DataSource = ta_ptable; }));
                }
                else
                {
                    dataGridView1.DataSource = ta_ptable;

                }      

                deffMat = helper.GetDifferenceIndex(ta_ptable, Barcode);

                int count = HighlightDiff(deffMat, ta_ptable);
                           
                if (difCountLbl.InvokeRequired)
                {
                    difCountLbl.Invoke(new MethodInvoker(delegate { difCountLbl.Text = Convert.ToString(count / 2); }));
                    difCountLbl.Invoke(new MethodInvoker(delegate { diffCount = difCountLbl.Text; }));
                }
                else
                {
                    dataGridView1.DataSource = ta_ptable;
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Display(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            try
            {
                string ptableName = "";
                //ptableName = ptableTB1.Text;
                PTableHelper helper = new PTableHelper();

                helper.InitTA_PTable();

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ta_ptable = new DataSet.DBDataSet.TA_PTableUsableColumnsDataTable();
                DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter ptableTA = new DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter();
                ptableTA.Connection = GTSAppSettings.SQLConnection;
                ptableTA.Fill(ta_ptable);

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ptable = helper.GetPTableUsableColumns(Barcode, fromDataTB.Text, toDataTB.Text, "", false);


               // ptable.DefaultView.Sort = "prc_date";
                ta_ptable = helper.AppendPTable(ta_ptable, ptable.DefaultView);

                ta_ptable = helper.MinutesToTime(ta_ptable);
                wthoutCalc = ta_ptable;
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new MethodInvoker(delegate { dataGridView1.DataSource = ta_ptable; }));
                }
                else
                {
                    dataGridView1.DataSource = ta_ptable;

                }     
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalculateAndDisplay(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {

            //string ptableName = "";
            try
            {
                GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter PersonTA = new GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter();
                PersonTA.Connection = GTSAppSettings.SQLConnection;
                decimal PersonId = (decimal)PersonTA.GetPrsID(this.Barcode);
 
                PTableHelper helper = new PTableHelper();

                helper.InitTA_PTable();

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ta_ptable = new DataSet.DBDataSet.TA_PTableUsableColumnsDataTable();
                DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter ptableTA = new DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter();
                ptableTA.Connection = GTSAppSettings.SQLConnection;
                ptableTA.Fill(ta_ptable);

                helper.GTSCalculate(this.Barcode, PersonId, fromDataTB.Text, toDataTB.Text);

                DataSet.DBDataSet.TA_PTableUsableColumnsDataTable ptable = helper.GetPTableUsableColumns(Barcode, fromDataTB.Text, toDataTB.Text, "", false);

                //ptable.DefaultView.Sort = "prc_date";
                ta_ptable = helper.AppendPTable(ta_ptable, ptable.DefaultView);

                ta_ptable = helper.MinutesToTime(ta_ptable);

                calculate = ta_ptable;
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new MethodInvoker(delegate { dataGridView1.DataSource = ta_ptable; }));
                }
                else 
                {
                    dataGridView1.DataSource = ta_ptable;

                }            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayProceedTraffic()
        {
            try
            {
                QueryExecuter exec = new QueryExecuter(null);
                string trafficSelect = String.Format(@"
                declare @prsId numeric
                set @prsId=dbo.GetPerson('{0}') 
                select  ProceedTraffic_ID [ID],dbo.GTS_ASM_MiladiToShamsi(Convert(varchar(10),ProceedTraffic_FromDate,101)) as [Date],[dbo].[MinutesToTime] (ProceedTrafficPair_From) [From]  ,[dbo].[MinutesToTime] (ProceedTrafficPair_To) [To], ProceedTrafficPair_PishCardID [Pishcart] ,ProceedTraffic_IsPairly [Is Pairly] ,ProceedTrafficPair_IsFilled [IsFilled]  ,ProceedTrafficPair_BasicTrafficIdFrom BasicFrom ,ProceedTrafficPair_BasicTrafficIdTo BasicTo
                ,ProceedTraffic_HasDailyItem [Has Daily Item] ,ProceedTraffic_HasHourlyItem [Has Hourly Item]
                 from TA_ProceedTraffic join TA_ProceedTrafficPair   on
                 ProceedTrafficPair_ProceedTrafficId=ProceedTraffic_ID 
                 where ProceedTraffic_PersonId=@prsId
                    and ProceedTraffic_FromDate>=dbo.GTS_ASM_Shamsitomiladi('{1}') and ProceedTraffic_FromDate <=dbo.GTS_ASM_Shamsitomiladi('{2}')
                 order by ProceedTraffic_FromDate,ProceedTraffic_ID", GTSAppSettings.Barcode, fromDataTB.Text, toDataTB.Text);

                proccedTraffic=exec.RunQueryResult(trafficSelect, GTSAppSettings.SQLConnection);
                dataGridView1.DataSource = proccedTraffic;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayBasicTraffic()
        {
            try
            {

                QueryExecuter exec = new QueryExecuter(null);
                string trafficSelect = String.Format(@"
                declare @d1 varchar(10),@d2 varchar(10),@prsId numeric
                set @d1=dbo.GTS_ASM_Shamsitomiladi('{0}')
                set @d2=dbo.GTS_ASM_Shamsitomiladi('{1}')
                set @prsId=dbo.GetPerson('{2}') 
                select   [BasicTraffic_ID] ID
              ,[BasicTraffic_PersonID] [Person ID]
              ,  dbo.GTS_ASM_MiladiToShamsi(CONVERT(char(10), BasicTraffic_Date, 101)) Date,[dbo].[MinutesToTime] (BasicTraffic_Time)as [Time],[BasicTraffic_PrecardId]  Pishcart,[BasicTraffic_Time] RealTime ,BasicTraffic_Manual as Manual ,BasicTraffic_Date as [Milady Date],[BasicTraffic_Used] as [Used] ,[BasicTraffic_Active] as [Active]
              from TA_BaseTraffic 
              where BasicTraffic_PersonID=@prsId
                and BasicTraffic_Date between  @d1 and  @d2
              order by BasicTraffic_Date  ,BasicTraffic_Time ", fromDataTB.Text, toDataTB.Text, GTSAppSettings.Barcode);

                basicTraffic = exec.RunQueryResult(trafficSelect, GTSAppSettings.SQLConnection);
                dataGridView1.DataSource = basicTraffic;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayRequests() 
        {
            try 
            {
                QueryExecuter exec = new QueryExecuter(null);
                string requestSelect = String.Format(@"SELECT * FROM 
(
SELECT  request_ID ID,
dbo.GTS_ASM_MiladiToShamsi(Convert(varchar(10),request_FromDate,101)) FromDate,dbo.GTS_ASM_MiladiToShamsi(Convert(varchar(10),request_ToDate,101)) ToDate,
[dbo].[MinutesToTime] (request_FromTime) FromTime,[dbo].[MinutesToTime] (request_ToTime) ToTime,
[dbo].[MinutesToTime] (request_TimeDuration) TimeDuration,request_Description Description,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, prs.Prs_FirstName+' ' + prs.Prs_LastName  Applicant,
precardGroup.PishcardGrp_LookupKey LookupKey
							 FROM TA_Request Req
                             inner join TA_Person as prs on prs.prs_ID=request_PersonId
							 Inner join TA_Precard on Precrd_ID=request_PrecardID
							 Inner join TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID							 
                             Inner join TA_SecurityUser usr on user_ID=request_UserID
							 Inner join TA_Person  usrprs on usrprs.Prs_ID=usr.user_PersonID
							 WHERE  (( Request_FromDate >= '{0}' AND '{1}' >= Request_FromDate ) OR 
                                     ( Request_ToDate >= '{0}' AND '{1}' >= Request_ToDate ))
								    AND
								   request_PersonID=dbo.GetPerson('{2}')
								   
								   
								   AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_Confirm=1
                                    AND reqStat_EndFlow=1
                                    )
                                    
) as window
Order by FromDate ", Utility.ToMildiDateString(fromDataTB.Text), Utility.ToMildiDateString(toDataTB.Text), GTSAppSettings.Barcode);

                requestOK = exec.RunQueryResult(requestSelect, GTSAppSettings.SQLConnection);
                dataGridView1.DataSource = requestOK;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayPermits()
        {
            try
            {
                QueryExecuter exec = new QueryExecuter(null);
                string permitSelect = String.Format(@"select  Permit_ID  ,PermitPair_PishCardID Precard,PERMITPAIR_isapplyedontraffic Applyed ,Permit_IsPairly IsPairly  ,permitpair_from  ,PermitPair_To  ,dbo.GTS_ASM_MiladiToShamsi(convert(varchar(10),Permit_FromDate,101)) as FromDate,dbo.GTS_ASM_MiladiToShamsi(convert(varchar(10),Permit_ToDate,101)) as ToDate
,[dbo].[MinutesToTime](convert(varchar(10),PermitPair_From,101))as [from]
,[dbo].[MinutesToTime] (convert(varchar(10),PermitPair_To,101)) as [to],PermitPair_Value as value
 
 from TA_Permit join TA_PermitPair   on
 PermitPair_PermitId=Permit_ID 
 where Permit_PersonId=dbo.GetPerson('{0}') and 
 (( Permit_FromDate >= '{1}' AND '{2}' >= Permit_FromDate ) OR 
 ( Permit_ToDate >= '{1}' AND '{2}' >= Permit_ToDate ))
 order by Permit_FromDate ", GTSAppSettings.Barcode, Utility.ToMildiDateString(fromDataTB.Text), Utility.ToMildiDateString(toDataTB.Text));

                permits = exec.RunQueryResult(permitSelect, GTSAppSettings.SQLConnection);
                dataGridView1.DataSource = permits;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void actionTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            object obj = null;
            switch (actionTypeCombo.SelectedIndex)
            {
               /* case 0:
                    if (calculate != null)
                    {
                        obj = calculate;
                        dataGridView1.DataSource = obj;
                    }
                    break;
                case 1:
                    if (wthoutCalc != null)
                    {
                        obj = wthoutCalc;
                        dataGridView1.DataSource = obj;
                    }
                    break;*/
                case 0:
                    if (proccedTraffic != null)
                    {
                        obj = proccedTraffic;
                        dataGridView1.DataSource = obj;
                    }
                    break;
                case 1:
                    if (basicTraffic != null)
                    {
                        obj = basicTraffic;
                        dataGridView1.DataSource = obj;
                    }
                    break;
                case 2:
                    obj = requestOK;
                    dataGridView1.DataSource = obj;
                    break;
                case 3:
                    obj = permits;
                    dataGridView1.DataSource = obj;
                    break;
                //case 6:

                //    if (compaireSource != null)
                //    {
                //        obj = compaireSource;
                //        dataGridView1.DataSource = obj;
                //        difCountLbl.Text = diffCount;
                //        HighlightDiff(deffMat, (DataTable)obj);
                //    }
                //    break;

                  
            }
            
        }
        
        private int HighlightDiff(bool[,] deffMatrix, DataTable ta_ptable)
        {
            int count = 0;
            string monthDate = toDataTB.Text;
            monthDate = monthDate.Remove(monthDate.Length - 2, 2);
            monthDate += "00";

            for (int i = 1; i < deffMatrix.GetLength(0); i++)
            {

                for (int j = 0; j < deffMatrix.GetLength(1) - 1; j++)
                {
                    if ((ta_ptable.Rows[i][1].ToString() == monthDate) && (j == 2 || j == 3 || j == 4 || j == 21))
                    {
                        continue;
                    }
                    if (deffMatrix[i, j])
                    {
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Pink;                        count++;
                    }
                }
            }
            return count;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DebugForm form = new DebugForm();
            form.ShowDialog();
        }

       

      
    }
    
}