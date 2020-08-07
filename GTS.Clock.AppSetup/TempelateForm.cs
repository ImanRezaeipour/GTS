using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace GTS.Clock.AppSetup
{
    public partial class TempelateForm :BaseForm
    {
        private SqlConnection sqlConnection;

        public TempelateForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clockDataSet.TA_RuleType' table. You can move, or remove it, as needed.
            try
            {
                
                sqlConnection = GTSAppSettings.SQLConnection;

                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                this.menuStrip1.Visible = false;

                this.tA_RuleTemplateTableAdapter.Connection = sqlConnection;
                this.tA_RuleTemplateMappingTableAdapter.Connection = sqlConnection;
                this.tA_RuleTemplateParameterTableAdapter.Connection = sqlConnection;
                this.tA_ConceptTemplateParameterTableAdapter.Connection = sqlConnection;
                this.tA_ConceptTemplateTableAdapter.Connection = sqlConnection;
                this.tA_RuleTypeTableAdapter.Connection = sqlConnection;

                this.tA_RuleTemplateTableAdapter.Fill(this.clockDataSet.TA_RuleTemplate);
                this.tA_RuleTemplateMappingTableAdapter.Fill(this.clockDataSet.TA_RuleTemplateMapping);
                this.tA_RuleTemplateParameterTableAdapter.Fill(this.clockDataSet.TA_RuleTemplateParameter);
                this.tA_ConceptTemplateParameterTableAdapter.Fill(this.clockDataSet.TA_ConceptTemplateParameter);
                this.tA_ConceptTemplateTableAdapter.Fill(this.clockDataSet.TA_ConceptTemplate);
                this.tA_RuleTypeTableAdapter.Fill(this.clockDataSet.TA_RuleType);

                this.comboBox4.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

   
        private void tA_RuleTemplateBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tA_RuleTemplateBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.clockDataSet);

        }

        private void tA_RuleTemplateBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.tA_RuleTemplateBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.clockDataSet);

        }

        private void tA_RuleTemplateBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.tA_RuleTemplateBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.clockDataSet);

        }

        private void toolStripButton49_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableAdapterManager.UpdateAll(this.clockDataSet);
        }

        private void tA_RuleTemplateBindingNavigatorSaveItem_Click_4(object sender, EventArgs e)
        {
            try
            {
                //#region Rule Template --> Custom Category Code
                //ruleTmp_CustomCategoryCodeTextBox.Text = (customCategoryCB.SelectedIndex + 1).ToString();
                //#endregion

                this.Validate();
                this.tA_RuleTemplateBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.clockDataSet);
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.tableAdapterManager.UpdateAll(this.clockDataSet);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
             

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.tA_RuleTemplateMappingBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.clockDataSet);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.tableAdapterManager.UpdateAll(this.clockDataSet);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.tableAdapterManager.UpdateAll(this.clockDataSet);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }    

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tA_ConceptTemplateBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
        }

        private void toolStripButton56_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.tA_ConceptTemplateBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.clockDataSet);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.tA_RuleTemplateParameterBindingSource.Filter = String.Format("{0} Like '%{1}%'", this.comboBox4.Text, this.textBox4.Text); 
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.tA_ConceptTemplateTableAdapter.FillBy(this.clockDataSet.TA_ConceptTemplate);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.tA_ConceptTemplateTableAdapter.FillBy1(this.clockDataSet.TA_ConceptTemplate);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.tA_ConceptTemplateTableAdapter.FillBy2(this.clockDataSet.TA_ConceptTemplate);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void conceptTmp_IsRangelyCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void priodicDetaileBtn_Click(object sender, EventArgs e)
        {
            int cnpId=0;
            int.TryParse(conceptTmp_IDTextBox.Text, out cnpId);
            PriodicDtlTemplate tmp = new PriodicDtlTemplate(cnpId);
            tmp.ShowDialog();
        }

        private void textBox5_MouseHover(object sender, EventArgs e)
        {

            switch (((TextBox)sender).Text)
            {
                case "0": statusStrip1.Items["lbstatus"].Text = "زوج مرتب";
                    break;
                case "1": statusStrip1.Items["lbstatus"].Text = "غیر زوج مرتبی";
                    break;
            }
            
        }

        private void textBox5_MouseLeave(object sender, EventArgs e)
        {
            statusStrip1.Items["lbstatus"].Text = "";
        }

        private void textBox6_MouseHover(object sender, EventArgs e)
        {
            switch (((TextBox)sender).Text)
            {
                case "0": statusStrip1.Items["lbstatus"].Text = "اجرا در هر روز محاسبه";
                    break;
                case "1": statusStrip1.Items["lbstatus"].Text = "اجرا در اولین روز دوره مفهوم/بازه محاسبه";
                    break;
                case "2": statusStrip1.Items["lbstatus"].Text = "اجرا در آخرین روز دوره مفهوم/بازه محاسبه";
                    break;
            }
        }

        private void textBox7_MouseHover(object sender, EventArgs e)
        {
            switch (((TextBox)sender).Text)
            {
                case "1": statusStrip1.Items["lbstatus"].Text = "در صوریتکه مقدار مفهوم صفر نباشد ذخیره می شود";
                    break;
                case "2": statusStrip1.Items["lbstatus"].Text = "مقدار مفهوم ذخیره نمی شود";
                    break;
                case "3": statusStrip1.Items["lbstatus"].Text = "مقدار مفهوم حتما ذخیره می شود";
                    break;
            }
        }

        private void textBox8_MouseHover(object sender, EventArgs e)
        {
            switch (((TextBox)sender).Text)
            {
                case "0": statusStrip1.Items["lbstatus"].Text = "مفهوم غیر دوره ای";
                    break;
                case "1": statusStrip1.Items["lbstatus"].Text = "مفهوم دوره ای که از جمع مفاهیم روزانه بدست می آید";
                    break;
                case "2": statusStrip1.Items["lbstatus"].Text = "مفهوم دوره ای که به هیچ مفهوم روزانه وابسته نیست و در قوانین مقداردهی می شود";
                    break;
            }
        }

        private void textBox8_MouseLeave(object sender, EventArgs e)
        {
            statusStrip1.Items["lbstatus"].Text = "";
        }

        private void textBox7_MouseLeave(object sender, EventArgs e)
        {
            statusStrip1.Items["lbstatus"].Text = "";
        }

        private void textBox6_MouseLeave(object sender, EventArgs e)
        {
            statusStrip1.Items["lbstatus"].Text = "";
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            int id = 0;
            priodicDetaileBtn.Enabled = false;
            if (int.TryParse(conceptTmp_IDTextBox.Text, out id))
            {
                if (id > 0 && textBox8.Text == "1")
                {
                    priodicDetaileBtn.Enabled = true;
                    PriodicDtlTemplate.tmpID = id;
                }
            }
        }

        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
            ColorConverter ColorCn = new ColorConverter();
            
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color =  (Color)ColorCn.ConvertFromString(textBox9.Text);

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                textBox9.Text = string.Format("#{0:X2}{1:X2}{2:X2}", MyDialog.Color.R, MyDialog.Color.G, MyDialog.Color.B);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            tbcolor.BackColor = (Color)(new ColorConverter()).ConvertFromString(((TextBox)sender).Text);
        }

        private void ruleTmp_CustomCategoryCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            int value=0;
            if (ruleTmp_CustomCategoryCodeTextBox.TextLength > 0
                && Int32.TryParse(ruleTmp_CustomCategoryCodeTextBox.Text, out value)) 
            {
                customCategoryCB.SelectedIndex = value - 1;
                customCategoryCB.EndUpdate();
            }
        }

        private void customCategoryCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruleTmp_CustomCategoryCodeTextBox.Text = (customCategoryCB.SelectedIndex + 1).ToString();
        }

       
    }
}
