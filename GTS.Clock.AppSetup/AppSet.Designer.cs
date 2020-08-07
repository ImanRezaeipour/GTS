namespace GTS.Clock.AppSetup
{
    partial class AppSet
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
            this.label1 = new System.Windows.Forms.Label();
            this.serverTB = new System.Windows.Forms.TextBox();
            this.DBTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clockCalculationCB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.logPasswordTB = new System.Windows.Forms.TextBox();
            this.logserverTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.logUserNameTB = new System.Windows.Forms.TextBox();
            this.logDBTB = new System.Windows.Forms.TextBox();
            this.serviceRefTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.settingNamesCombo = new System.Windows.Forms.ComboBox();
            this.configFileTB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCopyNames = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Server Name:";
            // 
            // serverTB
            // 
            this.serverTB.Location = new System.Drawing.Point(121, 29);
            this.serverTB.Name = "serverTB";
            this.serverTB.Size = new System.Drawing.Size(126, 21);
            this.serverTB.TabIndex = 2;
            // 
            // DBTB
            // 
            this.DBTB.Location = new System.Drawing.Point(121, 57);
            this.DBTB.Name = "DBTB";
            this.DBTB.Size = new System.Drawing.Size(126, 21);
            this.DBTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "DB Name:";
            // 
            // usernameTB
            // 
            this.usernameTB.Location = new System.Drawing.Point(121, 85);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(126, 21);
            this.usernameTB.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Username:";
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(121, 112);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(126, 21);
            this.passwordTB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Password:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 37);
            this.button1.TabIndex = 11;
            this.button1.Text = "ذخیره تنظیمات";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCopyNames);
            this.groupBox1.Controls.Add(this.clockCalculationCB);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.logPasswordTB);
            this.groupBox1.Controls.Add(this.logserverTB);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.logUserNameTB);
            this.groupBox1.Controls.Add(this.logDBTB);
            this.groupBox1.Controls.Add(this.serviceRefTb);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.passwordTB);
            this.groupBox1.Controls.Add(this.serverTB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.usernameTB);
            this.groupBox1.Controls.Add(this.DBTB);
            this.groupBox1.Location = new System.Drawing.Point(12, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 207);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تنظیمات سفارشی";
            // 
            // clockCalculationCB
            // 
            this.clockCalculationCB.AutoSize = true;
            this.clockCalculationCB.Location = new System.Drawing.Point(25, 173);
            this.clockCalculationCB.Name = "clockCalculationCB";
            this.clockCalculationCB.Size = new System.Drawing.Size(152, 17);
            this.clockCalculationCB.TabIndex = 18;
            this.clockCalculationCB.Text = "Use The Clock Webservice";
            this.clockCalculationCB.UseVisualStyleBackColor = true;
            this.clockCalculationCB.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Log Username:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(313, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Log Server Name:";
            // 
            // logPasswordTB
            // 
            this.logPasswordTB.Location = new System.Drawing.Point(412, 112);
            this.logPasswordTB.Name = "logPasswordTB";
            this.logPasswordTB.PasswordChar = '*';
            this.logPasswordTB.Size = new System.Drawing.Size(126, 21);
            this.logPasswordTB.TabIndex = 10;
            // 
            // logserverTB
            // 
            this.logserverTB.Location = new System.Drawing.Point(412, 29);
            this.logserverTB.Name = "logserverTB";
            this.logserverTB.Size = new System.Drawing.Size(126, 21);
            this.logserverTB.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(313, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Log Password:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(313, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Log DB Name:";
            // 
            // logUserNameTB
            // 
            this.logUserNameTB.Location = new System.Drawing.Point(412, 85);
            this.logUserNameTB.Name = "logUserNameTB";
            this.logUserNameTB.Size = new System.Drawing.Size(126, 21);
            this.logUserNameTB.TabIndex = 9;
            // 
            // logDBTB
            // 
            this.logDBTB.Location = new System.Drawing.Point(412, 57);
            this.logDBTB.Name = "logDBTB";
            this.logDBTB.Size = new System.Drawing.Size(126, 21);
            this.logDBTB.TabIndex = 8;
            this.logDBTB.Text = "LogDb";
            // 
            // serviceRefTb
            // 
            this.serviceRefTb.Location = new System.Drawing.Point(127, 146);
            this.serviceRefTb.Name = "serviceRefTb";
            this.serviceRefTb.Size = new System.Drawing.Size(404, 21);
            this.serviceRefTb.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Service Reference:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.settingNamesCombo);
            this.groupBox2.Controls.Add(this.configFileTB);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(12, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(562, 78);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "محل ذخیره تنظیمات";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(473, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "تنظیمان موجود :";
            // 
            // settingNamesCombo
            // 
            this.settingNamesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingNamesCombo.FormattingEnabled = true;
            this.settingNamesCombo.Location = new System.Drawing.Point(346, 29);
            this.settingNamesCombo.Name = "settingNamesCombo";
            this.settingNamesCombo.Size = new System.Drawing.Size(121, 21);
            this.settingNamesCombo.TabIndex = 21;
            this.settingNamesCombo.SelectedIndexChanged += new System.EventHandler(this.settingNamesCombo_SelectedIndexChanged);
            // 
            // configFileTB
            // 
            this.configFileTB.Location = new System.Drawing.Point(115, 29);
            this.configFileTB.Name = "configFileTB";
            this.configFileTB.Size = new System.Drawing.Size(126, 21);
            this.configFileTB.TabIndex = 0;
            this.configFileTB.Text = "appcon";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(247, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "نام تنظیمات جدید :";
            // 
            // btnCopyNames
            // 
            this.btnCopyNames.Location = new System.Drawing.Point(262, 29);
            this.btnCopyNames.Name = "btnCopyNames";
            this.btnCopyNames.Size = new System.Drawing.Size(32, 104);
            this.btnCopyNames.TabIndex = 21;
            this.btnCopyNames.Text = ">";
            this.btnCopyNames.UseVisualStyleBackColor = true;
            this.btnCopyNames.Click += new System.EventHandler(this.btnCopyNames_Click);
            // 
            // AppSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(578, 313);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppSet";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.Text = "تنظیمات";
            this.Load += new System.EventHandler(this.AppSettings_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverTB;
        private System.Windows.Forms.TextBox DBTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox serviceRefTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox logPasswordTB;
        private System.Windows.Forms.TextBox logserverTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox logUserNameTB;
        private System.Windows.Forms.TextBox logDBTB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox configFileTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox clockCalculationCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox settingNamesCombo;
        private System.Windows.Forms.Button btnCopyNames;
    }
}
