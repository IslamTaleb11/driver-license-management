using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Driving_Licenses
{
    public partial class frmDetainLicense : Form
    {
        private clsLicense _license;
        private clsDetainedLicense _detainedLicense;
        private clsPerson _person;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _errorMessageBox(string title, string body)
        {
            MessageBox.Show(
               body,                     
               title,                    
               MessageBoxButtons.OK,     
               MessageBoxIcon.Error      
           );
        }

        private void btnSearchLicense_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                _errorMessageBox("Searching Error", "The search box is empty!");
                return;
            }

            _license = clsLicense.findByLicenseID(Convert.ToInt32(tbSearch.Text));

            if (_license == null)
            {
                _errorMessageBox("Searching Error", $"There is no License with ID = {tbSearch.Text}");
                return;
            }

            if (!_license.IsActive)
            {
                _errorMessageBox("Searching Error", "The selected License is not active!");
                return;
            }

            if (clsDetainedLicense.findByLicenseID(Convert.ToInt32(tbSearch.Text)) != null)
            {
                _errorMessageBox("Searching Error", "The selected License is already detained!");
                return;
            }

            driverLicenseInfo1.license = _license;
            driverLicenseInfo1.person = clsPerson.findByLicenseID(_license.LicenseID);
            driverLicenseInfo1.callFindControlsWithDataMethod();
            detainInfo1.setLblLicenseID(_license.LicenseID);
            btnDetainLicense.Enabled = true;
            llbShowLicenseInfo.Enabled = true;
            llbShowLicensesHistory.Enabled = true;
            _person = clsPerson.findByLicenseID(_license.LicenseID);
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }

        private void _prepareDetainedLicenseObject()
        {
            _detainedLicense = new clsDetainedLicense();
            _detainedLicense.LicenseID = _license.LicenseID;
            _detainedLicense.DetainDate = DateTime.Now;
            _detainedLicense.FineFees = detainInfo1.fees;
            _detainedLicense.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
            _detainedLicense.IsReleased = false;
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            if (_license == null)
            {
                _errorMessageBox("Saving Error", "Please select a license first!");
                return;
            }
            if (detainInfo1.fees > 0)
            {
                _prepareDetainedLicenseObject();
                if (_detainedLicense.save())
                {
                    MessageBox.Show("License detained successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDetainLicense.Enabled = false;
                    detainInfo1.setLblDetainID(_detainedLicense.DetainID);
                }
                else
                {
                    _errorMessageBox("Error", "Failed to detain the license. Please try again.");
                }
            }
            else
            {
                _errorMessageBox("Saving Error", "The fees value must be more then 0!");
            }
        }

        private void llbShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(_person);
            frmLicenseHistory.ShowDialog();
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo frmDriverLicenseInfo = new frmDriverLicenseInfo(_license, _person);
            frmDriverLicenseInfo.fillControlsWithData();
            frmDriverLicenseInfo.ShowDialog();
        }
    }
}
