using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Custom_Controls
{
    public partial class DetainInfo : UserControl
    {
        public decimal fees;
        public DetainInfo()
        {
            InitializeComponent();
        }

        private void _setControlsDefaultData()
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobalSettings.currentLoggedInUser.UserName;
        }

        private void DetainInfo_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            _setControlsDefaultData();
        }

        public void setLblLicenseID(int LicenseID)
        {

            lblLicenseID.Text = LicenseID.ToString();
        }

        public void setLblDetainID(int DetainID)
        {
            lblDetainID.Text = DetainID.ToString();
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }

        private void tbFineFees_TextChanged(object sender, EventArgs e)
        {
            this.fees = Convert.ToDecimal(tbFineFees.Text);
        }
    }
}
