using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GTS.Clock.AppSetup
{
    public partial class MainPage : Form
    {
        public string ErrorMessages = "";
        List<string> msgList = new List<string>();
        public MainPage()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "آماده به کار";
            this.Update();
        }

     
        public void UpdateStatusBar(string message)
        {
            toolStripStatusLabel1.Text = message;
            this.Update();
        }

        public void ShowLog(string itemName)
        {
            msgList.Add(itemName);
        }       

        private void MainPage_Load(object sender, EventArgs e)
        {
            GTSAppSettings.LoadFromFile();

          
        }
     
        private void securityBtn_Click(object sender, EventArgs e)
        {
            SecurityEntry se = new SecurityEntry();
            se.ShowDialog();
        }       

        private void calcCompairerBtn_Click(object sender, EventArgs e)
        {
            CalculationCompairer compairer = new CalculationCompairer();
            compairer.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            AppSet appset = new AppSet();
            appset.ShowDialog();

            button4.Enabled = button2.Enabled = button3.Enabled = button5.Enabled = button8.Enabled = securityBtn.Enabled = initDbBttn.Enabled = calcCompairerBtn.Enabled = button7.Enabled = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DebugForm debug = new DebugForm();
            debug.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TempelateForm form = new TempelateForm();
            form.ShowDialog();
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ImportReport.FrmMain frm = new ImportReport.FrmMain();
            frm.ShowDialog();
            //CalculatorAll all = new CalculatorAll();
            //all.ShowDialog();
        }

        private void initDbBttn_Click(object sender, EventArgs e)
        {
            DatabaseInit init = new DatabaseInit();
            init.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Queries q = new Queries();
            q.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculator calc = new Calculator();
            OpenForm(calc);
        }

        private void OpenForm(Form form) 
        {            
            bool found = false;
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Name.Equals(form.Name))
                {
                    found = true;
                    frm.Focus();
                }
            }
            if (!found)
            {
                form.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PriorityDependencyEntry en = new PriorityDependencyEntry();
            en.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RequestCheck checker = new RequestCheck();
            checker.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AsmInfo asm = new AsmInfo();
            asm.ShowDialog();
        }
      
    }
}
