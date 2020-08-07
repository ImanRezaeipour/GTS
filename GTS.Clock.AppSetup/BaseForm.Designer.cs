namespace GTS.Clock.AppSetup
{
    partial class BaseForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.queriesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.calCulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hashingToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.queriesToolStripMenuItem1,
            this.calCulatorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip1.Size = new System.Drawing.Size(292, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "Queries";
            // 
            // queriesToolStripMenuItem1
            // 
            this.queriesToolStripMenuItem1.Name = "queriesToolStripMenuItem1";
            this.queriesToolStripMenuItem1.Size = new System.Drawing.Size(103, 20);
            this.queriesToolStripMenuItem1.Text = "Queries(Ctrl + Q)";
            this.queriesToolStripMenuItem1.Click += new System.EventHandler(this.queriesToolStripMenuItem1_Click);
            // 
            // calCulatorToolStripMenuItem
            // 
            this.calCulatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hashingToolToolStripMenuItem});
            this.calCulatorToolStripMenuItem.Name = "calCulatorToolStripMenuItem";
            this.calCulatorToolStripMenuItem.ShowShortcutKeys = false;
            this.calCulatorToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.calCulatorToolStripMenuItem.Text = "Calculator(Ctrl + U)";
            this.calCulatorToolStripMenuItem.Click += new System.EventHandler(this.calCulatorToolStripMenuItem_Click);
            // 
            // hashingToolToolStripMenuItem
            // 
            this.hashingToolToolStripMenuItem.Name = "hashingToolToolStripMenuItem";
            this.hashingToolToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hashingToolToolStripMenuItem.Text = "Hashing Tool";
            this.hashingToolToolStripMenuItem.Click += new System.EventHandler(this.hashingToolToolStripMenuItem_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BaseForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem queriesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem calCulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hashingToolToolStripMenuItem;
    }
}