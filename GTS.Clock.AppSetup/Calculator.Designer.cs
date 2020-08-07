namespace GTS.Clock.AppSetup
{
    partial class Calculator
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
            this.label1 = new System.Windows.Forms.Label();
            this.sunDateTB1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.miladiTB1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.sunDateTB2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.miladiTB2 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.timeTB1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minutesTB1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.minutesTB2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.timeTB2 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dayResultLbl = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.minTBDay = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "تاریخ خورشیدی:";
            // 
            // sunDateTB1
            // 
            this.sunDateTB1.Location = new System.Drawing.Point(332, 27);
            this.sunDateTB1.Name = "sunDateTB1";
            this.sunDateTB1.Size = new System.Drawing.Size(100, 21);
            this.sunDateTB1.TabIndex = 0;
            this.sunDateTB1.Text = "1389/05/01";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "محاسبه کن";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.miladiTB1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sunDateTB1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 72);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "خورشیدی به میلادی";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "تاریخ میلادی:";
            // 
            // miladiTB1
            // 
            this.miladiTB1.Location = new System.Drawing.Point(94, 27);
            this.miladiTB1.Name = "miladiTB1";
            this.miladiTB1.ReadOnly = true;
            this.miladiTB1.Size = new System.Drawing.Size(100, 21);
            this.miladiTB1.TabIndex = 10;
            this.miladiTB1.Text = "2010/07/23";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.sunDateTB2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.miladiTB2);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 72);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "میلادی به خورشیدی";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "محاسبه کن";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "تاریخ خورشیدی:";
            // 
            // sunDateTB2
            // 
            this.sunDateTB2.Location = new System.Drawing.Point(94, 27);
            this.sunDateTB2.Name = "sunDateTB2";
            this.sunDateTB2.ReadOnly = true;
            this.sunDateTB2.Size = new System.Drawing.Size(100, 21);
            this.sunDateTB2.TabIndex = 11;
            this.sunDateTB2.Text = "1388/10/11";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(453, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "تاریخ میلادی:";
            // 
            // miladiTB2
            // 
            this.miladiTB2.Location = new System.Drawing.Point(332, 27);
            this.miladiTB2.Name = "miladiTB2";
            this.miladiTB2.Size = new System.Drawing.Size(100, 21);
            this.miladiTB2.TabIndex = 2;
            this.miladiTB2.Text = "2010/01/01";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.timeTB1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.minutesTB1);
            this.groupBox3.Location = new System.Drawing.Point(12, 162);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 72);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ذقیقه به ساعت";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "محاسبه کن";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ساعت:";
            // 
            // timeTB1
            // 
            this.timeTB1.Location = new System.Drawing.Point(94, 27);
            this.timeTB1.Name = "timeTB1";
            this.timeTB1.ReadOnly = true;
            this.timeTB1.Size = new System.Drawing.Size(100, 21);
            this.timeTB1.TabIndex = 12;
            this.timeTB1.Text = "00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "ساعت به دقیقه:";
            // 
            // minutesTB1
            // 
            this.minutesTB1.Location = new System.Drawing.Point(332, 27);
            this.minutesTB1.Name = "minutesTB1";
            this.minutesTB1.Size = new System.Drawing.Size(100, 21);
            this.minutesTB1.TabIndex = 4;
            this.minutesTB1.Text = "0";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.minutesTB2);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.timeTB2);
            this.groupBox4.Location = new System.Drawing.Point(12, 237);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(528, 72);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ساعت به دقیقه";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(73, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "محاسبه کن";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(216, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "ساعت به دقیقه:";
            // 
            // minutesTB2
            // 
            this.minutesTB2.Location = new System.Drawing.Point(94, 27);
            this.minutesTB2.Name = "minutesTB2";
            this.minutesTB2.ReadOnly = true;
            this.minutesTB2.Size = new System.Drawing.Size(100, 21);
            this.minutesTB2.TabIndex = 13;
            this.minutesTB2.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(476, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "ساعت :";
            // 
            // timeTB2
            // 
            this.timeTB2.Location = new System.Drawing.Point(332, 27);
            this.timeTB2.Name = "timeTB2";
            this.timeTB2.Size = new System.Drawing.Size(100, 21);
            this.timeTB2.TabIndex = 6;
            this.timeTB2.Text = "00:00";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dayResultLbl);
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.minTBDay);
            this.groupBox5.Location = new System.Drawing.Point(12, 315);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(528, 72);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "دقیقه به روز";
            // 
            // dayResultLbl
            // 
            this.dayResultLbl.AutoSize = true;
            this.dayResultLbl.Location = new System.Drawing.Point(92, 30);
            this.dayResultLbl.Name = "dayResultLbl";
            this.dayResultLbl.Size = new System.Drawing.Size(0, 13);
            this.dayResultLbl.TabIndex = 8;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 25);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(73, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "محاسبه کن";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "دقیقه به روز:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(476, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "دقیقه :";
            // 
            // minTBDay
            // 
            this.minTBDay.Location = new System.Drawing.Point(332, 27);
            this.minTBDay.Name = "minTBDay";
            this.minTBDay.Size = new System.Drawing.Size(100, 21);
            this.minTBDay.TabIndex = 6;
            this.minTBDay.Text = "0";
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(549, 399);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Calculator";
            this.ShowIcon = false;
            this.Text = "Convertor";
            this.Load += new System.EventHandler(this.Calculator_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sunDateTB1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sunDateTB2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox miladiTB2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox miladiTB1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox minutesTB2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox timeTB2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox timeTB1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox minutesTB1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label dayResultLbl;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox minTBDay;
    }
}
