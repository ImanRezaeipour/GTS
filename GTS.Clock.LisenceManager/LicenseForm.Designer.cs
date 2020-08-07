namespace GTS.Clock.LisenceManager
{
    partial class LicenseForm 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.currentSystemCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.systemIdTB = new System.Windows.Forms.TextBox();
            this.personCountNdd = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.hashedTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.currentSysTB = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personCountNdd)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.currentSysTB);
            this.groupBox1.Controls.Add(this.currentSystemCB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.systemIdTB);
            this.groupBox1.Controls.Add(this.personCountNdd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.hashedTB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 167);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تولید لایسنس";
            // 
            // currentSystemCB
            // 
            this.currentSystemCB.AutoSize = true;
            this.currentSystemCB.Location = new System.Drawing.Point(108, 29);
            this.currentSystemCB.Name = "currentSystemCB";
            this.currentSystemCB.Size = new System.Drawing.Size(92, 17);
            this.currentSystemCB.TabIndex = 14;
            this.currentSystemCB.Text = "سیستم جاری";
            this.currentSystemCB.UseVisualStyleBackColor = true;
            this.currentSystemCB.CheckedChanged += new System.EventHandler(this.currentSystemCB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "شناسه سیستم :";
            // 
            // systemIdTB
            // 
            this.systemIdTB.Location = new System.Drawing.Point(6, 98);
            this.systemIdTB.Name = "systemIdTB";
            this.systemIdTB.Size = new System.Drawing.Size(326, 21);
            this.systemIdTB.TabIndex = 13;
            // 
            // personCountNdd
            // 
            this.personCountNdd.Location = new System.Drawing.Point(222, 28);
            this.personCountNdd.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.personCountNdd.Name = "personCountNdd";
            this.personCountNdd.Size = new System.Drawing.Size(110, 21);
            this.personCountNdd.TabIndex = 11;
            this.personCountNdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.personCountNdd.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "لایسنس :";
            // 
            // hashedTB
            // 
            this.hashedTB.Location = new System.Drawing.Point(6, 135);
            this.hashedTB.Name = "hashedTB";
            this.hashedTB.ReadOnly = true;
            this.hashedTB.Size = new System.Drawing.Size(326, 21);
            this.hashedTB.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "تعداد کاربر :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "تولید";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "شناسه جاری :";
            // 
            // currentSysTB
            // 
            this.currentSysTB.Location = new System.Drawing.Point(6, 64);
            this.currentSysTB.Name = "currentSysTB";
            this.currentSysTB.ReadOnly = true;
            this.currentSysTB.Size = new System.Drawing.Size(326, 21);
            this.currentSysTB.TabIndex = 16;
            // 
            // LicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 203);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "مدیریت لایسنس";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personCountNdd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox hashedTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown personCountNdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox systemIdTB;
        private System.Windows.Forms.CheckBox currentSystemCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox currentSysTB;
    }
}