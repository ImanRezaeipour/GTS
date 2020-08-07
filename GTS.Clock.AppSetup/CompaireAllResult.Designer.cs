namespace GTS.Clock.AppSetup
{
    partial class CompaireAllResult
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dBDataSet = new GTS.Clock.AppSetup.DataSet.DBDataSet();
            this.tACompaireDiffrenceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tA_CompaireDiffrenceTableAdapter = new GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.TA_CompaireDiffrenceTableAdapter();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cDIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDBarcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDDiffCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDMonthlyDiffCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDFromDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDToDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tACompaireDiffrenceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dBDataSet
            // 
            this.dBDataSet.DataSetName = "DBDataSet";
            this.dBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tACompaireDiffrenceBindingSource
            // 
            this.tACompaireDiffrenceBindingSource.DataMember = "TA_CompaireDiffrence";
            this.tACompaireDiffrenceBindingSource.DataSource = this.dBDataSet;
            // 
            // tA_CompaireDiffrenceTableAdapter
            // 
            this.tA_CompaireDiffrenceTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDIDDataGridViewTextBoxColumn,
            this.cDBarcodeDataGridViewTextBoxColumn,
            this.cDDiffCountDataGridViewTextBoxColumn,
            this.cDMonthlyDiffCountDataGridViewTextBoxColumn,
            this.cDFromDateDataGridViewTextBoxColumn,
            this.cDToDateDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tACompaireDiffrenceBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(560, 294);
            this.dataGridView1.TabIndex = 11;
            // 
            // cDIDDataGridViewTextBoxColumn
            // 
            this.cDIDDataGridViewTextBoxColumn.DataPropertyName = "CD_ID";
            this.cDIDDataGridViewTextBoxColumn.HeaderText = "CD_ID";
            this.cDIDDataGridViewTextBoxColumn.Name = "cDIDDataGridViewTextBoxColumn";
            this.cDIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.cDIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // cDBarcodeDataGridViewTextBoxColumn
            // 
            this.cDBarcodeDataGridViewTextBoxColumn.DataPropertyName = "CD_Barcode";
            this.cDBarcodeDataGridViewTextBoxColumn.HeaderText = "بارکد";
            this.cDBarcodeDataGridViewTextBoxColumn.Name = "cDBarcodeDataGridViewTextBoxColumn";
            this.cDBarcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cDDiffCountDataGridViewTextBoxColumn
            // 
            this.cDDiffCountDataGridViewTextBoxColumn.DataPropertyName = "CD_DiffCount";
            this.cDDiffCountDataGridViewTextBoxColumn.HeaderText = "تعداد کل اختلاف";
            this.cDDiffCountDataGridViewTextBoxColumn.Name = "cDDiffCountDataGridViewTextBoxColumn";
            this.cDDiffCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cDMonthlyDiffCountDataGridViewTextBoxColumn
            // 
            this.cDMonthlyDiffCountDataGridViewTextBoxColumn.DataPropertyName = "CD_MonthlyDiffCount";
            this.cDMonthlyDiffCountDataGridViewTextBoxColumn.HeaderText = "تعداد اختلاف ماهانه";
            this.cDMonthlyDiffCountDataGridViewTextBoxColumn.Name = "cDMonthlyDiffCountDataGridViewTextBoxColumn";
            this.cDMonthlyDiffCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.cDMonthlyDiffCountDataGridViewTextBoxColumn.Visible = false;
            // 
            // cDFromDateDataGridViewTextBoxColumn
            // 
            this.cDFromDateDataGridViewTextBoxColumn.DataPropertyName = "CD_FromDate";
            this.cDFromDateDataGridViewTextBoxColumn.HeaderText = "تاریخ شروع محاسبه";
            this.cDFromDateDataGridViewTextBoxColumn.Name = "cDFromDateDataGridViewTextBoxColumn";
            this.cDFromDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cDToDateDataGridViewTextBoxColumn
            // 
            this.cDToDateDataGridViewTextBoxColumn.DataPropertyName = "CD_ToDate";
            this.cDToDateDataGridViewTextBoxColumn.HeaderText = "تاریخ پایان محاسبه";
            this.cDToDateDataGridViewTextBoxColumn.Name = "cDToDateDataGridViewTextBoxColumn";
            this.cDToDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(-1, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(566, 314);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(451, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Delete All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CompaireAllResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(453, 372);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompaireAllResult";
            this.Text = "نتیجه مقایسات";
            this.Load += new System.EventHandler(this.CompaireAllResult_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tACompaireDiffrenceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataSet.DBDataSet dBDataSet;
        private System.Windows.Forms.BindingSource tACompaireDiffrenceBindingSource;
        private GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters.TA_CompaireDiffrenceTableAdapter tA_CompaireDiffrenceTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDBarcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDDiffCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDMonthlyDiffCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDFromDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDToDateDataGridViewTextBoxColumn;
    }
}
