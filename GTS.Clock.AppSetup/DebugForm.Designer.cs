namespace GTS.Clock.AppSetup
{
    partial class DebugForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.busnessErrorLogCB = new System.Windows.Forms.CheckBox();
            this.busnessLogCB = new System.Windows.Forms.CheckBox();
            this.ruleDurationCB = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.saveLogCheckBox = new System.Windows.Forms.CheckBox();
            this.UserActivityGroupBox = new System.Windows.Forms.GroupBox();
            this.userActivityGridView = new System.Windows.Forms.DataGridView();
            this.EngineLogGroupBox = new System.Windows.Forms.GroupBox();
            this.engineLogGridView = new System.Windows.Forms.DataGridView();
            this.logTypeCombo = new System.Windows.Forms.ComboBox();
            this.RuleLogFilterGB = new System.Windows.Forms.GroupBox();
            this.filterBtn = new System.Windows.Forms.Button();
            this.conceptFilterTB = new System.Windows.Forms.TextBox();
            this.filterDateTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.winServiceGB = new System.Windows.Forms.GroupBox();
            this.winServiceGrid = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.BusinessErrorLogGB = new System.Windows.Forms.GroupBox();
            this.bussinessErrorGrid = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.BusinessLogGB = new System.Windows.Forms.GroupBox();
            this.bussinessLogGrid = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.UserActivityGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userActivityGridView)).BeginInit();
            this.EngineLogGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.engineLogGridView)).BeginInit();
            this.RuleLogFilterGB.SuspendLayout();
            this.winServiceGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.winServiceGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.BusinessErrorLogGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bussinessErrorGrid)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.BusinessLogGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bussinessLogGrid)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.busnessErrorLogCB);
            this.groupBox1.Controls.Add(this.busnessLogCB);
            this.groupBox1.Controls.Add(this.ruleDurationCB);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.saveLogCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(7, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // busnessErrorLogCB
            // 
            this.busnessErrorLogCB.AutoSize = true;
            this.busnessErrorLogCB.Location = new System.Drawing.Point(135, 35);
            this.busnessErrorLogCB.Name = "busnessErrorLogCB";
            this.busnessErrorLogCB.Size = new System.Drawing.Size(114, 17);
            this.busnessErrorLogCB.TabIndex = 4;
            this.busnessErrorLogCB.Text = "ثبت خطاهای برنامه";
            this.busnessErrorLogCB.UseVisualStyleBackColor = true;
            // 
            // busnessLogCB
            // 
            this.busnessLogCB.AutoSize = true;
            this.busnessLogCB.Location = new System.Drawing.Point(107, 13);
            this.busnessLogCB.Name = "busnessLogCB";
            this.busnessLogCB.Size = new System.Drawing.Size(141, 17);
            this.busnessLogCB.TabIndex = 3;
            this.busnessLogCB.Text = "ثبت خطاهای کاربر کاربری";
            this.busnessLogCB.UseVisualStyleBackColor = true;
            // 
            // ruleDurationCB
            // 
            this.ruleDurationCB.AutoSize = true;
            this.ruleDurationCB.Location = new System.Drawing.Point(259, 32);
            this.ruleDurationCB.Name = "ruleDurationCB";
            this.ruleDurationCB.Size = new System.Drawing.Size(140, 17);
            this.ruleDurationCB.TabIndex = 2;
            this.ruleDurationCB.Text = "ثبت مدت اجرای هر قانون";
            this.ruleDurationCB.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(2, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Delete Log Tables";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "تایید";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveLogCheckBox
            // 
            this.saveLogCheckBox.AutoSize = true;
            this.saveLogCheckBox.Location = new System.Drawing.Point(265, 9);
            this.saveLogCheckBox.Name = "saveLogCheckBox";
            this.saveLogCheckBox.Size = new System.Drawing.Size(133, 17);
            this.saveLogCheckBox.TabIndex = 0;
            this.saveLogCheckBox.Text = "ثبت ترتیب اجرای قوانین";
            this.saveLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // UserActivityGroupBox
            // 
            this.UserActivityGroupBox.Controls.Add(this.userActivityGridView);
            this.UserActivityGroupBox.Location = new System.Drawing.Point(7, 90);
            this.UserActivityGroupBox.Name = "UserActivityGroupBox";
            this.UserActivityGroupBox.Size = new System.Drawing.Size(986, 617);
            this.UserActivityGroupBox.TabIndex = 1;
            this.UserActivityGroupBox.TabStop = false;
            this.UserActivityGroupBox.Text = "User Activity Log";
            this.UserActivityGroupBox.Visible = false;
            // 
            // userActivityGridView
            // 
            this.userActivityGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.userActivityGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userActivityGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userActivityGridView.Location = new System.Drawing.Point(3, 17);
            this.userActivityGridView.Name = "userActivityGridView";
            this.userActivityGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userActivityGridView.Size = new System.Drawing.Size(980, 597);
            this.userActivityGridView.TabIndex = 0;
            // 
            // EngineLogGroupBox
            // 
            this.EngineLogGroupBox.Controls.Add(this.engineLogGridView);
            this.EngineLogGroupBox.Location = new System.Drawing.Point(7, 90);
            this.EngineLogGroupBox.Name = "EngineLogGroupBox";
            this.EngineLogGroupBox.Size = new System.Drawing.Size(986, 614);
            this.EngineLogGroupBox.TabIndex = 2;
            this.EngineLogGroupBox.TabStop = false;
            this.EngineLogGroupBox.Text = "Engine Log";
            this.EngineLogGroupBox.Visible = false;
            // 
            // engineLogGridView
            // 
            this.engineLogGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.engineLogGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.engineLogGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.engineLogGridView.Location = new System.Drawing.Point(3, 17);
            this.engineLogGridView.Name = "engineLogGridView";
            this.engineLogGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.engineLogGridView.Size = new System.Drawing.Size(980, 594);
            this.engineLogGridView.TabIndex = 0;
            // 
            // logTypeCombo
            // 
            this.logTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logTypeCombo.FormattingEnabled = true;
            this.logTypeCombo.ItemHeight = 13;
            this.logTypeCombo.Items.AddRange(new object[] {
            "Business Error Log",
            "Engine Log",
            "User ActivityLog",
            "Windows Service Log",
            "Business Log"});
            this.logTypeCombo.Location = new System.Drawing.Point(421, 42);
            this.logTypeCombo.Name = "logTypeCombo";
            this.logTypeCombo.Size = new System.Drawing.Size(121, 21);
            this.logTypeCombo.TabIndex = 2;
            this.logTypeCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // RuleLogFilterGB
            // 
            this.RuleLogFilterGB.Controls.Add(this.filterBtn);
            this.RuleLogFilterGB.Controls.Add(this.conceptFilterTB);
            this.RuleLogFilterGB.Controls.Add(this.filterDateTB);
            this.RuleLogFilterGB.Controls.Add(this.label2);
            this.RuleLogFilterGB.Controls.Add(this.label1);
            this.RuleLogFilterGB.Enabled = false;
            this.RuleLogFilterGB.Location = new System.Drawing.Point(560, 24);
            this.RuleLogFilterGB.Name = "RuleLogFilterGB";
            this.RuleLogFilterGB.Size = new System.Drawing.Size(422, 60);
            this.RuleLogFilterGB.TabIndex = 3;
            this.RuleLogFilterGB.TabStop = false;
            this.RuleLogFilterGB.Text = "RuleLog Filter";
            // 
            // filterBtn
            // 
            this.filterBtn.Location = new System.Drawing.Point(22, 18);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(75, 23);
            this.filterBtn.TabIndex = 6;
            this.filterBtn.Text = "Filter Now";
            this.filterBtn.UseVisualStyleBackColor = true;
            this.filterBtn.Click += new System.EventHandler(this.filterBtn_Click);
            // 
            // conceptFilterTB
            // 
            this.conceptFilterTB.Location = new System.Drawing.Point(123, 20);
            this.conceptFilterTB.Name = "conceptFilterTB";
            this.conceptFilterTB.Size = new System.Drawing.Size(74, 21);
            this.conceptFilterTB.TabIndex = 5;
            this.conceptFilterTB.Text = "C13";
            // 
            // filterDateTB
            // 
            this.filterDateTB.Location = new System.Drawing.Point(297, 20);
            this.filterDateTB.Name = "filterDateTB";
            this.filterDateTB.Size = new System.Drawing.Size(74, 21);
            this.filterDateTB.TabIndex = 3;
            this.filterDateTB.Text = "1389/5/1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "نام مفهوم:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "تاریخ:";
            // 
            // winServiceGB
            // 
            this.winServiceGB.Controls.Add(this.winServiceGrid);
            this.winServiceGB.Controls.Add(this.groupBox3);
            this.winServiceGB.Location = new System.Drawing.Point(9, 90);
            this.winServiceGB.Name = "winServiceGB";
            this.winServiceGB.Size = new System.Drawing.Size(986, 618);
            this.winServiceGB.TabIndex = 4;
            this.winServiceGB.TabStop = false;
            this.winServiceGB.Text = "Windows Service Log";
            this.winServiceGB.Visible = false;
            // 
            // winServiceGrid
            // 
            this.winServiceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.winServiceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.winServiceGrid.Location = new System.Drawing.Point(3, 17);
            this.winServiceGrid.Margin = new System.Windows.Forms.Padding(10);
            this.winServiceGrid.Name = "winServiceGrid";
            this.winServiceGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.winServiceGrid.Size = new System.Drawing.Size(980, 598);
            this.winServiceGrid.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(24, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(961, 583);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rule Log";
            this.groupBox3.Visible = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(3, 17);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView2.Size = new System.Drawing.Size(980, 600);
            this.dataGridView2.TabIndex = 0;
            // 
            // BusinessErrorLogGB
            // 
            this.BusinessErrorLogGB.Controls.Add(this.bussinessErrorGrid);
            this.BusinessErrorLogGB.Controls.Add(this.groupBox4);
            this.BusinessErrorLogGB.Location = new System.Drawing.Point(9, 90);
            this.BusinessErrorLogGB.Name = "BusinessErrorLogGB";
            this.BusinessErrorLogGB.Size = new System.Drawing.Size(986, 618);
            this.BusinessErrorLogGB.TabIndex = 5;
            this.BusinessErrorLogGB.TabStop = false;
            this.BusinessErrorLogGB.Text = "Business Error Log";
            this.BusinessErrorLogGB.Visible = false;
            // 
            // bussinessErrorGrid
            // 
            this.bussinessErrorGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.bussinessErrorGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bussinessErrorGrid.Location = new System.Drawing.Point(3, 17);
            this.bussinessErrorGrid.Margin = new System.Windows.Forms.Padding(10);
            this.bussinessErrorGrid.Name = "bussinessErrorGrid";
            this.bussinessErrorGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bussinessErrorGrid.Size = new System.Drawing.Size(980, 598);
            this.bussinessErrorGrid.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView3);
            this.groupBox4.Location = new System.Drawing.Point(24, 36);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(961, 583);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rule Log";
            this.groupBox4.Visible = false;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(3, 17);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView3.Size = new System.Drawing.Size(980, 600);
            this.dataGridView3.TabIndex = 0;
            // 
            // BusinessLogGB
            // 
            this.BusinessLogGB.Controls.Add(this.bussinessLogGrid);
            this.BusinessLogGB.Controls.Add(this.groupBox5);
            this.BusinessLogGB.Location = new System.Drawing.Point(9, 90);
            this.BusinessLogGB.Name = "BusinessLogGB";
            this.BusinessLogGB.Size = new System.Drawing.Size(986, 618);
            this.BusinessLogGB.TabIndex = 5;
            this.BusinessLogGB.TabStop = false;
            this.BusinessLogGB.Text = "Business Log";
            this.BusinessLogGB.Visible = false;
            // 
            // bussinessLogGrid
            // 
            this.bussinessLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.bussinessLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bussinessLogGrid.Location = new System.Drawing.Point(3, 17);
            this.bussinessLogGrid.Margin = new System.Windows.Forms.Padding(10);
            this.bussinessLogGrid.Name = "bussinessLogGrid";
            this.bussinessLogGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bussinessLogGrid.Size = new System.Drawing.Size(980, 598);
            this.bussinessLogGrid.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridView5);
            this.groupBox5.Location = new System.Drawing.Point(24, 36);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(961, 583);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Rule Log";
            this.groupBox5.Visible = false;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(3, 17);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView5.Size = new System.Drawing.Size(980, 600);
            this.dataGridView5.TabIndex = 0;
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(994, 720);
            this.Controls.Add(this.RuleLogFilterGB);
            this.Controls.Add(this.logTypeCombo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BusinessLogGB);
            this.Controls.Add(this.winServiceGB);
            this.Controls.Add(this.EngineLogGroupBox);
            this.Controls.Add(this.UserActivityGroupBox);
            this.Controls.Add(this.BusinessErrorLogGB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DebugForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debuger";
            this.Load += new System.EventHandler(this.DebugForm_Load);
            this.Controls.SetChildIndex(this.BusinessErrorLogGB, 0);
            this.Controls.SetChildIndex(this.UserActivityGroupBox, 0);
            this.Controls.SetChildIndex(this.EngineLogGroupBox, 0);
            this.Controls.SetChildIndex(this.winServiceGB, 0);
            this.Controls.SetChildIndex(this.BusinessLogGB, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.logTypeCombo, 0);
            this.Controls.SetChildIndex(this.RuleLogFilterGB, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.UserActivityGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userActivityGridView)).EndInit();
            this.EngineLogGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.engineLogGridView)).EndInit();
            this.RuleLogFilterGB.ResumeLayout(false);
            this.RuleLogFilterGB.PerformLayout();
            this.winServiceGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.winServiceGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.BusinessErrorLogGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bussinessErrorGrid)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.BusinessLogGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bussinessLogGrid)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox saveLogCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox UserActivityGroupBox;
        private System.Windows.Forms.DataGridView userActivityGridView;
        private System.Windows.Forms.GroupBox EngineLogGroupBox;
        private System.Windows.Forms.DataGridView engineLogGridView;
        private System.Windows.Forms.ComboBox logTypeCombo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox RuleLogFilterGB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox conceptFilterTB;
        private System.Windows.Forms.TextBox filterDateTB;
        private System.Windows.Forms.Button filterBtn;
        private System.Windows.Forms.GroupBox winServiceGB;
        private System.Windows.Forms.DataGridView winServiceGrid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.CheckBox ruleDurationCB;
        private System.Windows.Forms.GroupBox BusinessErrorLogGB;
        private System.Windows.Forms.DataGridView bussinessErrorGrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.GroupBox BusinessLogGB;
        private System.Windows.Forms.DataGridView bussinessLogGrid;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.CheckBox busnessErrorLogCB;
        private System.Windows.Forms.CheckBox busnessLogCB;
    }
}
