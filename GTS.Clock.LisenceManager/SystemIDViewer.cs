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
    public partial class SystemIDViewer : Form
    {
        public SystemIDViewer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentSysTB.Text = GTS.Clock.Infrastructure.Utility.Utility.ServerFingerPrint;
        }       
    }
}
