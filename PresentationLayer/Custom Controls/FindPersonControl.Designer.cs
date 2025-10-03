namespace DVLD.Custom_Controls
{
    partial class FindPersonControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindPersonControl));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.searchOrAddPanel = new System.Windows.Forms.Panel();
            this.btnAddNewPerson2 = new System.Windows.Forms.Button();
            this.tbSelectUserFilter = new System.Windows.Forms.TextBox();
            this.cbAddNewUserFilterBy = new System.Windows.Forms.ComboBox();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.personDetailsControl1 = new DVLD.PersonDetailsControl();
            this.searchOrAddPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(59, -20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 24);
            this.label4.TabIndex = 25;
            this.label4.Text = "Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, -20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 24);
            this.label3.TabIndex = 23;
            // 
            // searchOrAddPanel
            // 
            this.searchOrAddPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchOrAddPanel.Controls.Add(this.btnAddNewPerson2);
            this.searchOrAddPanel.Controls.Add(this.tbSelectUserFilter);
            this.searchOrAddPanel.Controls.Add(this.cbAddNewUserFilterBy);
            this.searchOrAddPanel.Controls.Add(this.btnFindPerson);
            this.searchOrAddPanel.Controls.Add(this.label5);
            this.searchOrAddPanel.Location = new System.Drawing.Point(41, 56);
            this.searchOrAddPanel.Name = "searchOrAddPanel";
            this.searchOrAddPanel.Size = new System.Drawing.Size(748, 100);
            this.searchOrAddPanel.TabIndex = 24;
            // 
            // btnAddNewPerson2
            // 
            this.btnAddNewPerson2.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewPerson2.Image")));
            this.btnAddNewPerson2.Location = new System.Drawing.Point(556, 22);
            this.btnAddNewPerson2.Name = "btnAddNewPerson2";
            this.btnAddNewPerson2.Size = new System.Drawing.Size(56, 42);
            this.btnAddNewPerson2.TabIndex = 26;
            this.btnAddNewPerson2.UseVisualStyleBackColor = true;
            this.btnAddNewPerson2.Click += new System.EventHandler(this.btnAddNewPerson2_Click);
            // 
            // tbSelectUserFilter
            // 
            this.tbSelectUserFilter.Location = new System.Drawing.Point(276, 34);
            this.tbSelectUserFilter.Multiline = true;
            this.tbSelectUserFilter.Name = "tbSelectUserFilter";
            this.tbSelectUserFilter.Size = new System.Drawing.Size(182, 27);
            this.tbSelectUserFilter.TabIndex = 25;
            // 
            // cbAddNewUserFilterBy
            // 
            this.cbAddNewUserFilterBy.FormattingEnabled = true;
            this.cbAddNewUserFilterBy.Items.AddRange(new object[] {
            "National No"});
            this.cbAddNewUserFilterBy.Location = new System.Drawing.Point(100, 37);
            this.cbAddNewUserFilterBy.Name = "cbAddNewUserFilterBy";
            this.cbAddNewUserFilterBy.Size = new System.Drawing.Size(170, 21);
            this.cbAddNewUserFilterBy.TabIndex = 24;
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnFindPerson.Image")));
            this.btnFindPerson.Location = new System.Drawing.Point(485, 22);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(56, 42);
            this.btnFindPerson.TabIndex = 22;
            this.btnFindPerson.UseVisualStyleBackColor = true;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindPerson_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 24);
            this.label5.TabIndex = 23;
            this.label5.Text = "Filter By:";
            // 
            // personDetailsControl1
            // 
            this.personDetailsControl1.Location = new System.Drawing.Point(16, 151);
            this.personDetailsControl1.Name = "personDetailsControl1";
            this.personDetailsControl1.Size = new System.Drawing.Size(962, 423);
            this.personDetailsControl1.TabIndex = 22;
            // 
            // FindPersonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchOrAddPanel);
            this.Controls.Add(this.personDetailsControl1);
            this.Name = "FindPersonControl";
            this.Size = new System.Drawing.Size(992, 628);
            this.Load += new System.EventHandler(this.FindPersonControl_Load);
            this.searchOrAddPanel.ResumeLayout(false);
            this.searchOrAddPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel searchOrAddPanel;
        private System.Windows.Forms.Button btnAddNewPerson2;
        private System.Windows.Forms.TextBox tbSelectUserFilter;
        private System.Windows.Forms.ComboBox cbAddNewUserFilterBy;
        private System.Windows.Forms.Button btnFindPerson;
        private System.Windows.Forms.Label label5;
        private PersonDetailsControl personDetailsControl1;
    }
}
