namespace DVLD.Driving_Licenses
{
    partial class frmApplicationDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApplicationDetails));
            this.drivingLicenseApplicationInfoAndAppInfo1 = new DVLD.Custom_Controls.DrivingLicenseApplicationInfoAndAppInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // drivingLicenseApplicationInfoAndAppInfo1
            // 
            this.drivingLicenseApplicationInfoAndAppInfo1.Location = new System.Drawing.Point(12, 12);
            this.drivingLicenseApplicationInfoAndAppInfo1.Name = "drivingLicenseApplicationInfoAndAppInfo1";
            this.drivingLicenseApplicationInfoAndAppInfo1.Size = new System.Drawing.Size(901, 575);
            this.drivingLicenseApplicationInfoAndAppInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(771, 610);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 46);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 668);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.drivingLicenseApplicationInfoAndAppInfo1);
            this.Name = "frmApplicationDetails";
            this.Text = "Application Details";
            this.ResumeLayout(false);

        }

        #endregion

        private Custom_Controls.DrivingLicenseApplicationInfoAndAppInfo drivingLicenseApplicationInfoAndAppInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}