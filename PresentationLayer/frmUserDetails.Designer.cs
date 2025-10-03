namespace DVLD
{
    partial class frmUserDetails
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
            this.personDetailsControl1 = new DVLD.PersonDetailsControl();
            this.userDetails1 = new DVLD.UserDetails();
            this.SuspendLayout();
            // 
            // personDetailsControl1
            // 
            this.personDetailsControl1.Location = new System.Drawing.Point(29, 12);
            this.personDetailsControl1.Name = "personDetailsControl1";
            this.personDetailsControl1.Size = new System.Drawing.Size(971, 423);
            this.personDetailsControl1.TabIndex = 0;
            // 
            // userDetails1
            // 
            this.userDetails1.Location = new System.Drawing.Point(-75, 441);
            this.userDetails1.Name = "userDetails1";
            this.userDetails1.Size = new System.Drawing.Size(1075, 165);
            this.userDetails1.TabIndex = 1;
            // 
            // frmUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 702);
            this.Controls.Add(this.userDetails1);
            this.Controls.Add(this.personDetailsControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserDetails";
            this.Text = "User Details";
            this.Load += new System.EventHandler(this.frmUserDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PersonDetailsControl personDetailsControl1;
        private UserDetails userDetails1;
    }
}