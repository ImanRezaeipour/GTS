using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GTS.Clock.AppSetup
{
    public partial class PriorityDependencyEntry : GTS.Clock.AppSetup.BaseForm
    {
        public PriorityDependencyEntry()
        {
            InitializeComponent();
        }

        private void tA_PriorityDependencyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tA_PriorityDependencyBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.ghadircopyDataSet);

        }

        private void PriorityDependencyEntry_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ghadircopyDataSet.TA_PriorityDependency' table. You can move, or remove it, as needed.
            this.tA_PriorityDependencyTableAdapter.Fill(this.ghadircopyDataSet.TA_PriorityDependency);

        }
    }
}
