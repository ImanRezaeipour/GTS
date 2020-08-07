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
    public partial class PriodicDtlTemplate : BaseForm
    {
        public static int tmpID = 0;
        public PriodicDtlTemplate(int templateID)
        {
            InitializeComponent();
            tmpID = templateID;
        }

        private void PriodicDtlTemplate_Load(object sender, EventArgs e)
        {
            this.tA_ConceptTemplate1TableAdapter.Connection = GTSAppSettings.SQLConnection;
            this.tA_PeriodicScndCnpDetailTemplateTableAdapter.Connection = GTSAppSettings.SQLConnection;
            this.tA_ConceptTemplateTableAdapter.Connection = GTSAppSettings.SQLConnection;

            this.menuStrip1.Visible = false;
            // TODO: This line of code loads data into the 'ghadircopyDataSet.TA_ConceptTemplate1' table. You can move, or remove it, as needed.
            this.tA_ConceptTemplate1TableAdapter.Fill(this.ghadircopyDataSet.TA_ConceptTemplate1);
            // TODO: This line of code loads data into the 'ghadircopyDataSet.TA_PeriodicScndCnpDetailTemplate' table. You can move, or remove it, as needed.
            this.tA_PeriodicScndCnpDetailTemplateTableAdapter.Fill(this.ghadircopyDataSet.TA_PeriodicScndCnpDetailTemplate);
            // TODO: This line of code loads data into the 'ghadircopyDataSet.TA_ConceptTemplate' table. You can move, or remove it, as needed.
            this.tA_ConceptTemplateTableAdapter.FillByID(this.ghadircopyDataSet.TA_ConceptTemplate, tmpID); 

        }

        private void tA_ConceptTemplateBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                //this.tA_ConceptTemplateBindingSource.EndEdit();
                // this.tA_PeriodicScndCnpDetailTemplateBindingSource.EndEdit();
                //this.tableAdapterManager.UpdateAll(this.ghadircopyDataSet);
                tA_PeriodicScndCnpDetailTemplateTableAdapter.Update(this.ghadircopyDataSet);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            tA_PeriodicScndCnpDetailTemplateBindingSource.RemoveCurrent();
        }
    }
}
