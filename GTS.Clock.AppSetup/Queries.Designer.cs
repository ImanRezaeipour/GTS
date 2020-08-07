namespace GTS.Clock.AppSetup
{
    partial class Queries
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
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.shamsiDateTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.barcodeTB = new System.Windows.Forms.TextBox();
            this.cfpTrafficCB = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 15);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(257, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = " Invalidate Traffics Now !";
            this.toolTip1.SetToolTip(this.button1, "Invalidate All Traffic");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(257, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Some Usable Queries";
            this.toolTip1.SetToolTip(this.button2, "نمایش شیفت های یک شخص");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(152, 155);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 21);
            this.button3.TabIndex = 5;
            this.button3.Text = "UpDate CFP";
            this.toolTip1.SetToolTip(this.button3, "نمایش شیفت های یک شخص");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // shamsiDateTB
            // 
            this.shamsiDateTB.Location = new System.Drawing.Point(51, 155);
            this.shamsiDateTB.Name = "shamsiDateTB";
            this.shamsiDateTB.Size = new System.Drawing.Size(85, 21);
            this.shamsiDateTB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "بارکد:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "تاریخ:";
            // 
            // barcodeTB
            // 
            this.barcodeTB.Location = new System.Drawing.Point(51, 122);
            this.barcodeTB.Name = "barcodeTB";
            this.barcodeTB.Size = new System.Drawing.Size(85, 21);
            this.barcodeTB.TabIndex = 2;
            // 
            // cfpTrafficCB
            // 
            this.cfpTrafficCB.AutoSize = true;
            this.cfpTrafficCB.Location = new System.Drawing.Point(152, 124);
            this.cfpTrafficCB.Name = "cfpTrafficCB";
            this.cfpTrafficCB.Size = new System.Drawing.Size(131, 17);
            this.cfpTrafficCB.TabIndex = 4;
            this.cfpTrafficCB.Text = "شامل ترددها هم باشد";
            this.cfpTrafficCB.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 73);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(257, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "Hash All Passwords";
            this.toolTip1.SetToolTip(this.button4, "نمایش شیفت های یک شخص");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Queries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cfpTrafficCB);
            this.Controls.Add(this.barcodeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shamsiDateTB);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Queries";
            this.Text = "Queries";
            this.Load += new System.EventHandler(this.Queries_Load);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.Controls.SetChildIndex(this.shamsiDateTB, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.barcodeTB, 0);
            this.Controls.SetChildIndex(this.cfpTrafficCB, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox shamsiDateTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox barcodeTB;
        private System.Windows.Forms.CheckBox cfpTrafficCB;
        private System.Windows.Forms.Button button4;
    }
}
