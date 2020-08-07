using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.AppSetup.DataSet;
using GTS.Clock.Infrastructure.Utility;


namespace GTS.Clock.AppSetup
{
    public partial class CalculatorAll : GTS.Clock.AppSetup.BaseForm
    {
        struct CalcObject
        {
            public string barcode;
            public string fromDate;
            public string toDate;
        }

        #region variables
        DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter cfpTA = new DataSet.DBDataSetTableAdapters.TA_Calculation_Flag_PersonsTableAdapter();
        DBDataSet.TA_Calculation_Flag_PersonsDataTable cfpTable = new DBDataSet.TA_Calculation_Flag_PersonsDataTable();

        int cfpCount = 0;
        PTableHelper helper = new PTableHelper();

        static List<CalcObject> calObjectList = new List<CalcObject>();
        #endregion

        public CalculatorAll()
        {
            InitializeComponent();
        }

        #region Form Events
        private void Calculator_Load(object sender, EventArgs e)
        {
            endtimeTB.Text = GTSAppSettings.ToDate;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cfpTA.Connection = GTSAppSettings.SQLConnection;
                cfpTA.Fill(cfpTable);
                cfpCount = cfpTable.Rows.Count;

                GTSAppSettings.ToDate = endtimeTB.Text;
                GTSAppSettings.SaveToFile();

                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;


                if (cfpCount == 0)
                {
                    MessageBox.Show("CFP Table is Empty!");
                }
                else
                {
                    toolStripStatusLabel2.Text = "محاسبه";
                    WaitNow(Calculate, "Initilizing....");
                    toolStripStatusLabel2.Text = "مقایسه";
                    Compaire();
                    toolStripStatusLabel2.Text = "اتمام";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CFP cfp = new CFP();
            cfp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CompaireAllResult r = new CompaireAllResult();
            r.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cfpTA.Connection = GTSAppSettings.SQLConnection;                
                cfpTA.Fill(cfpTable);
                cfpCount = cfpTable.Rows.Count;

                GTSAppSettings.ToDate = endtimeTB.Text;
                GTSAppSettings.SaveToFile();

                this.Cursor = Cursors.WaitCursor;
                EnableDiasable(false);


                if (cfpCount == 0)
                {
                    MessageBox.Show("CFP Table is Empty!");
                }
                else
                {
                    toolStripStatusLabel2.Text = "محاسبه";
                    CalculateAll();                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        #endregion

        private void Calculate(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            string fromdate = "";
            string todate = "";
            if (endtimeTB.InvokeRequired)
            {
                calculatedTB.Invoke(new MethodInvoker(delegate { todate = endtimeTB.Text; }));
            }
            else
            {
                todate = endtimeTB.Text;
            }

            calObjectList.Clear();
            for (int i = 0; i < cfpCount; i++)
            {
                string barcode = cfpTable.Rows[i]["CFP_Barcode"].ToString();
                string date = cfpTable.Rows[i]["CFP_Date"].ToString();

                //انتقال به جدول موقت برای مقایسه و فقط یکبار
                if (fromdate.Length == 0)
                {
                    fromdate = date;
                    helper.TransferPTableToTempTable(helper.GetPtableFromDate(fromdate));

                    helper.TransferPTableToTempTable(helper.GetPtableFromDate(todate));
                }

                GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter PersonTA = new GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters.TA_PersonTableAdapter();
                decimal PersonId = (decimal)PersonTA.GetPrsID(barcode);

                helper.GTSCalculate(barcode, PersonId, date, todate);
              
                if (calculatedTB.InvokeRequired)
                {
                    calculatedTB.Invoke(new MethodInvoker(delegate { calculatedTB.Text += "- " + barcode; }));
                }
                else 
                {
                    calculatedTB.Text += "- " + barcode;
                }
                //toolStripStatusLabel1.Text = String.Format("{0} of {1}", i + 1, cfpCount);
                e.Window.Message = String.Format("Calculating {0} of {1}", i + 1, cfpCount);
                CalcObject calcObj = new CalcObject();
                calcObj.barcode = barcode;
                calcObj.fromDate = date;
                calcObj.toDate = endtimeTB.Text;
                calObjectList.Add(calcObj);

                //toolStripStatusLabel1.Invalidate();                
                //this.Invalidate();
                //this.Update();
            }
        }

        private void Compaire()
        {

            helper.InitTA_PTable();

            DataSet.DBDataSetTableAdapters.TA_CompaireDiffrenceTableAdapter cdTA = new DataSet.DBDataSetTableAdapters.TA_CompaireDiffrenceTableAdapter();
            cdTA.Connection = GTSAppSettings.SQLConnection;
            for (int k = 0; k < calObjectList.Count; k++)
            {
                CalcObject objcet = calObjectList[k];

                DBDataSet.TA_PTableUsableColumnsDataTable ta_ptable = new DBDataSet.TA_PTableUsableColumnsDataTable();
                DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter ptableTA = new DataSet.DBDataSetTableAdapters.TA_PTableUsableColumnsTableAdapter();
                ptableTA.Connection = GTSAppSettings.SQLConnection;
                ptableTA.Fill(ta_ptable);

                DBDataSet.TA_PTableUsableColumnsDataTable ptable1 = helper.GetPTableUsableColumns(objcet.barcode, objcet.fromDate, objcet.toDate, "Clock6", true);

                DBDataSet.TA_PTableUsableColumnsDataTable ptable2 = helper.GetPTableUsableColumns(objcet.barcode, objcet.fromDate, objcet.toDate, "GTS", false);

                DBDataSet.TA_PTableUsableColumnsDataTable ptable = helper.AppendPTable(ptable1, ptable2);

                string monthDate = objcet.toDate;
                monthDate = monthDate.Remove(monthDate.Length - 2, 2);
                monthDate += "00";

                ptable.DefaultView.Sort = "prc_date";
                ta_ptable = helper.AppendPTable(ta_ptable, ptable.DefaultView);

                bool[,] deffMat = helper.GetDifferenceIndex(ta_ptable, objcet.barcode);
                int diffCount = 0;
                int difMonthCount = 0;
                for (int i = 1; i < deffMat.GetLength(0); i++)
                {
                    for (int j = 0; j < deffMat.GetLength(1) - 1; j++)
                    {
                        if ((ta_ptable.Rows[i][1].ToString() == monthDate) && (j == 2 || j == 3 || j == 4 || j == 21))
                        {
                            continue;
                        }
                        if (ta_ptable.Rows[i][1].ToString() == monthDate && deffMat[i, j])
                        {
                            difMonthCount++;
                        }
                        if (deffMat[i, j])
                        {
                            diffCount++;
                        }
                    }
                }
                diffCount = diffCount / 2;
                cdTA.Insert(objcet.barcode, diffCount, difMonthCount, objcet.fromDate, objcet.toDate);
            }
        }

        private void CalculateAll()
        {
            string fromdate = "";
            string todate = endtimeTB.Text;

            calObjectList.Clear();
            for (int i = 0; i < cfpCount; i++)
            {
                string barcode = cfpTable.Rows[i]["CFP_Barcode"].ToString();
                string date = PersianDateTime.MiladiToShamsi(cfpTable.Rows[i]["CFP_Date"].ToString());

                //انتقال به جدول موقت برای مقایسه و فقط یکبار
                if (fromdate.Length == 0)
                {
                    fromdate = date;
                    helper.TransferPTableToTempTable(helper.GetPtableFromDate(fromdate));

                    helper.TransferPTableToTempTable(helper.GetPtableFromDate(todate));

                }

                CalcObject calcObj = new CalcObject();
                calcObj.barcode = barcode;
                calcObj.fromDate = date;
                calcObj.toDate = endtimeTB.Text;
                calObjectList.Add(calcObj);
            }
            if (fromdate.Length > 0)
            {
                helper.DeletePTable(helper.GetPtableFromDate(fromdate));
                helper.DeletePTable(helper.GetPtableFromDate(todate));

                helper.GTSCalculateAll(todate);
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = (int)cfpTA.GetCount();

            calculatedTB.Text += " - " + count.ToString();
            toolStripStatusLabel1.Text = String.Format("{0} باقیمانده ... ", count);

            if (count == 0)
            {
                timer1.Stop();
                toolStripStatusLabel1.Text = "مقایسه ...";
                Compaire();

                EnableDiasable(true);
                this.Cursor = Cursors.Default;
                toolStripStatusLabel1.Text = "انمام";
            }

            this.Update();
        }

        private void EnableDiasable(bool state) 
        {
            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = state;
        }
    }
}
