using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTS.Clock.AppSetup
{
    public partial class BaseForm : Form
    {
       
        protected int topMetginHeight = 0;
        public BaseForm()
        {
            InitializeComponent();
            topMetginHeight = menuStrip1.Height;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape && this.Enabled)
            {
                this.Close();
            }
            else if (keyData == (Keys.Control | Keys.Q)  && !this.Name.ToLower().Equals("queries")) 
            {
                Queries q = new Queries();
                q.ShowDialog();
            }
            else if (keyData == (Keys.Control | Keys.D) && !this.Name.ToLower().Equals("debugform"))
            {
                DebugForm d = new DebugForm();
                d.ShowDialog();
            }
            else if (keyData == (Keys.Control | Keys.U) && !this.Name.ToLower().Equals("Convertor") && !this.Name.ToLower().Equals("debugform"))
            {
                Calculator c = new Calculator();
                c.Show();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void queriesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Queries q = new Queries();
            q.ShowDialog();
        }

        private void hashingToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UtiliyForm util = new UtiliyForm();
            util.Show();
        }

        private void calCulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculator calc = new Calculator();
            OpenForm(calc);
        }

        protected void WaitNow(EventHandler<Jacksonsoft.WaitWindowEventArgs> workerMethod) 
        {
            object result = Jacksonsoft.WaitWindow.Show(workerMethod);
        }

        protected void WaitNow(EventHandler<Jacksonsoft.WaitWindowEventArgs> workerMethod,string message)
        {
            object result = Jacksonsoft.WaitWindow.Show(workerMethod, message);
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

        
    }
}
