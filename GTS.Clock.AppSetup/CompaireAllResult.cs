using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GTS.Clock.AppSetup
{
    public partial class CompaireAllResult : GTS.Clock.AppSetup.BaseForm
    {
        public CompaireAllResult()
        {
            InitializeComponent();
        }

        private void CompaireAllResult_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.TA_CompaireDiffrence' table. You can move, or remove it, as needed.
            this.tA_CompaireDiffrenceTableAdapter.Connection = GTSAppSettings.SQLConnection;
            this.tA_CompaireDiffrenceTableAdapter.Fill(this.dBDataSet.TA_CompaireDiffrence);
            
           // dataGridView1.DataSource = this.dBDataSet.TA_CompaireDiffrence;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tA_CompaireDiffrenceTableAdapter.DeleteAll();
            this.tA_CompaireDiffrenceTableAdapter.Fill(this.dBDataSet.TA_CompaireDiffrence);
        }
    }
}
