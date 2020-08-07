using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.AppSetup.DataSet;
using GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters;

namespace GTS.Clock.AppSetup
{
    public partial class UtiliyForm : GTS.Clock.AppSetup.BaseForm
    {
        public UtiliyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hashedTB.Text = Utility.GetHashCode(valueTB.Text);
/*
            TA_SecurityResourceTableAdapter resourceTA = new TA_SecurityResourceTableAdapter();
            resourceTA.Connection = GTSAppSettings.SQLConnection;
            GTSDB.TA_SecurityResourceDataTable table =
            table= resourceTA.GetData();
            for(int i=0;i<table.Rows.Count;i++)
            {
                GTSDB.TA_SecurityResourceRow row=(GTSDB.TA_SecurityResourceRow )table.Rows[i];
                row.resource_CheckKey=Utility.GetHashCode(row.resource_ResourceID);
                resourceTA.Update(row);
            }
 */
        }
    }
}
