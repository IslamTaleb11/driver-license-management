namespace DVLD
{
    partial class addUpdatePerson
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
            this.lblAddUpdatePerson = new System.Windows.Forms.Label();
            this.addUpdatePersonControl1 = new DVLD.addUpdatePersonControl();
            this.SuspendLayout();
            // 
            // lblAddUpdatePerson
            // 
            this.lblAddUpdatePerson.AutoSize = true;
            this.lblAddUpdatePerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddUpdatePerson.ForeColor = System.Drawing.Color.Red;
            this.lblAddUpdatePerson.Location = new System.Drawing.Point(463, 21);
            this.lblAddUpdatePerson.Name = "lblAddUpdatePerson";
            this.lblAddUpdatePerson.Size = new System.Drawing.Size(236, 31);
            this.lblAddUpdatePerson.TabIndex = 0;
            this.lblAddUpdatePerson.Text = "AddUpdatePerson";
            // 
            // addUpdatePersonControl1
            // 
            this.addUpdatePersonControl1.Location = new System.Drawing.Point(25, 41);
            this.addUpdatePersonControl1.Name = "addUpdatePersonControl1";
            this.addUpdatePersonControl1.Size = new System.Drawing.Size(1065, 541);
            this.addUpdatePersonControl1.TabIndex = 1;
            // 
            // addUpdatePerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 584);
            this.Controls.Add(this.lblAddUpdatePerson);
            this.Controls.Add(this.addUpdatePersonControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addUpdatePerson";
            this.Text = "Add/Update Person";
            this.Load += new System.EventHandler(this.addUpdatePerson_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddUpdatePerson;
        private addUpdatePersonControl addUpdatePersonControl1;
    }
}