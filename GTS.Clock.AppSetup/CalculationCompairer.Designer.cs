namespace GTS.Clock.AppSetup
{
    partial class CalculationCompairer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.actionTypeCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.difCountLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toDataTB = new System.Windows.Forms.TextBox();
            this.fromDataTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.barcodeTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gridContainerGrpBox = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gridContainerGrpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.actionTypeCombo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.difCountLbl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.toDataTB);
            this.groupBox1.Controls.Add(this.fromDataTB);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.barcodeTB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Debug";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // actionTypeCombo
            // 
            this.actionTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionTypeCombo.FormattingEnabled = true;
            this.actionTypeCombo.Items.AddRange(new object[] {
            "ترددهای پردازش شده",
            "نمایش ترددهای خام",
            "درخواستهای تایید شده",
            "مجوزهای ثبت شده"});
            this.actionTypeCombo.Location = new System.Drawing.Point(434, 14);
            this.actionTypeCombo.Name = "actionTypeCombo";
            this.actionTypeCombo.Size = new System.Drawing.Size(121, 21);
            this.actionTypeCombo.TabIndex = 4;
            this.actionTypeCombo.SelectedIndexChanged += new System.EventHandler(this.actionTypeCombo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(690, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "تعداد تفاوت:";
            // 
            // difCountLbl
            // 
            this.difCountLbl.AutoSize = true;
            this.difCountLbl.Location = new System.Drawing.Point(649, 45);
            this.difCountLbl.Name = "difCountLbl";
            this.difCountLbl.Size = new System.Drawing.Size(0, 13);
            this.difCountLbl.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "تاریخ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "تا";
            // 
            // toDataTB
            // 
            this.toDataTB.Location = new System.Drawing.Point(250, 34);
            this.toDataTB.Name = "toDataTB";
            this.toDataTB.Size = new System.Drawing.Size(100, 21);
            this.toDataTB.TabIndex = 3;
            this.toDataTB.Text = "1389/05/31";
            // 
            // fromDataTB
            // 
            this.fromDataTB.Location = new System.Drawing.Point(250, 9);
            this.fromDataTB.Name = "fromDataTB";
            this.fromDataTB.Size = new System.Drawing.Size(100, 21);
            this.fromDataTB.TabIndex = 2;
            this.fromDataTB.Text = "1389/05/01";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "شروع";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // barcodeTB
            // 
            this.barcodeTB.Location = new System.Drawing.Point(584, 14);
            this.barcodeTB.Name = "barcodeTB";
            this.barcodeTB.Size = new System.Drawing.Size(100, 21);
            this.barcodeTB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(690, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "کد پرسنلی:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CadetBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Size = new System.Drawing.Size(751, 382);
            this.dataGridView1.TabIndex = 10;
            // 
            // gridContainerGrpBox
            // 
            this.gridContainerGrpBox.Controls.Add(this.dataGridView1);
            this.gridContainerGrpBox.Location = new System.Drawing.Point(4, 85);
            this.gridContainerGrpBox.Name = "gridContainerGrpBox";
            this.gridContainerGrpBox.Size = new System.Drawing.Size(757, 402);
            this.gridContainerGrpBox.TabIndex = 11;
            this.gridContainerGrpBox.TabStop = false;
            // 
            // CalculationCompairer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(764, 499);
            this.Controls.Add(this.gridContainerGrpBox);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "CalculationCompairer";
            this.ShowIcon = false;
            this.Text = "مقایسه نتیجه محاسبات";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_ResizeEnd);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.gridContainerGrpBox, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gridContainerGrpBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox barcodeTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox toDataTB;
        private System.Windows.Forms.TextBox fromDataTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gridContainerGrpBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label difCountLbl;
        private System.Windows.Forms.ComboBox actionTypeCombo;
        private System.Windows.Forms.Button button2;
    }
}

