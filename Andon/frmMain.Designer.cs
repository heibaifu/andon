namespace Andon
{
    partial class frmMain
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
            this.btnLine = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnShift = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(3, 3);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(148, 103);
            this.btnLine.TabIndex = 0;
            this.btnLine.Text = "Line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(3, 112);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(148, 103);
            this.btnUser.TabIndex = 1;
            this.btnUser.Text = "User";
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnShift
            // 
            this.btnShift.Location = new System.Drawing.Point(157, 3);
            this.btnShift.Name = "btnShift";
            this.btnShift.Size = new System.Drawing.Size(148, 103);
            this.btnShift.TabIndex = 2;
            this.btnShift.Text = "Shift";
            this.btnShift.Click += new System.EventHandler(this.btnShift_Click);
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(157, 112);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(148, 103);
            this.btnSchedule.TabIndex = 3;
            this.btnSchedule.Text = "Schedules";
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.btnShift);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.btnLine);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnShift;
        private System.Windows.Forms.Button btnSchedule;





    }
}

