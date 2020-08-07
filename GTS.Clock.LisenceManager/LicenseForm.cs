using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.Infrastructure.Utility;


namespace GTS.Clock.LisenceManager
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (personCountNdd.Value <= 0 || (!currentSystemCB.Checked && systemIdTB.TextLength == 0)) 
            {
                MessageBox.Show("اطلاعات ناقص است");
                return;
                
            }
            string machineId = "";
            if (currentSystemCB.Checked)
            {
                machineId=GTS.Clock.Infrastructure.Utility.Utility.ServerFingerPrint;
            }
            else 
            {
                machineId = systemIdTB.Text;
            }
            int personCount = (int)personCountNdd.Value;
            string mixed = machineId + "-" + personCount.ToString();

            hashedTB.Text = Utility.GetHashCode(mixed) + "*-*" + personCount.ToString();

        }

        private void currentSystemCB_CheckedChanged(object sender, EventArgs e)
        {
            if (currentSystemCB.Checked)
            {
                systemIdTB.ReadOnly = true;
                currentSysTB.Text = GTS.Clock.Infrastructure.Utility.Utility.ServerFingerPrint;
            }
            else
                systemIdTB.ReadOnly = false;
        }
    }
}
