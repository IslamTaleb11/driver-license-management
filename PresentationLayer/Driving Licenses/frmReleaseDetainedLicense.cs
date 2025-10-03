using BussinessLayer;
using DVLD.Custom_Controls;
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
    public partial class frmReleaseDetainedLicense : Form
    {
        clsLicense _license;
        clsPerson _person;
        clsDetainedLicense _detainedLicense;
        clsApplication _releaseApplication;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
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

        private void _enableOrDisableLicenseHistory_ShowLicenseInfo()
        {
            llbShowLicenseInfo.Enabled = !llbShowLicenseInfo.Enabled;
            llbShowLicensesHistory.Enabled = !llbShowLicensesHistory.Enabled;
        }

        private void _disableLicenseHistory_ShowLicenseInfo()
        {
            llbShowLicenseInfo.Enabled = false;
            llbShowLicensesHistory.Enabled = false;
        }

        private void btnSearchLicense_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                _disableLicenseHistory_ShowLicenseInfo();
                _errorMessageBox("Searching Error", "The search box is empty!");
                return;
            }

            _license = clsLicense.findByLicenseID(Convert.ToInt32(tbSearch.Text));

            if (_license == null)
            {
                _disableLicenseHistory_ShowLicenseInfo();
                _errorMessageBox("Searching Error", $"There is no License with ID = {tbSearch.Text}");
                return;
            }

            if (!_license.IsActive)
            {
                _disableLicenseHistory_ShowLicenseInfo();
                _errorMessageBox("Searching Error", "The selected License is not active!");
                return;
            }

            if (!clsDetainedLicense.isLicenseDetained(_license.LicenseID))
            {
                _disableLicenseHistory_ShowLicenseInfo();
                _errorMessageBox("Searching Error", "The selected License is already released!");
                return;
            }

            _detainedLicense = clsDetainedLicense.findByLicenseID(_license.LicenseID);

            driverLicenseInfo1.license = _license;
            driverLicenseInfo1.person = clsPerson.findByLicenseID(_license.LicenseID);
            driverLicenseInfo1.callFindControlsWithDataMethod();
            btnReleaseDetainedLicense.Enabled = true;
            
            _person = clsPerson.findByLicenseID(_license.LicenseID);

            detainInfoForRelease1.setControlsDefaultData();
            detainInfoForRelease1.setLblLicenseID(_license.LicenseID);
            detainInfoForRelease1.setControlsAfterSelections(_detainedLicense);

            _enableOrDisableLicenseHistory_ShowLicenseInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void _prepareApplicationObject()
        {
            _releaseApplication = new clsApplication();
            _releaseApplication.ApplicantPersonFullName = _person.FirstName + " " + _person.SecondName +
               " " + _person.ThirdName + " " + _person.LastName;
            _releaseApplication.ApplicantPersonID = _person.ID;
            _releaseApplication.ApplicationDate = DateTime.Now;
            _releaseApplication.ApplicationStatus = 1;
            _releaseApplication.ApplicationTypeID = clsApplicationType.find("Release Detained Driving Licsense").ID;
            _releaseApplication.ApplicationTypeName = "Release Detained Driving Licsense";
            _releaseApplication.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
            _releaseApplication.LastStatusDate = DateTime.Now;
            _releaseApplication.PaidFees = clsApplicationType.GetApplicationFees("Release Detained Driving Licsense");

        }


        

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            _prepareApplicationObject();

            if (_releaseApplication.save())
            {
                if (_detainedLicense.releaseDetainedLicense(_releaseApplication.ApplicationID, _detainedLicense.CreatedByUserID))
                {
                    MessageBox.Show("Application and License saved successfully ✅",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);


                    detainInfoForRelease1.setLblApplicationID(_releaseApplication.ApplicationID);
                    btnReleaseDetainedLicense.Enabled = false;
                }
                else
                {
                    _errorMessageBox("Error", "Something went wrong please try again!");
                }
            }
            else
            {
                _errorMessageBox("Error", "Something went wrong please try again!");
            }

        }

        public void setAndDisableTbSearchForm(int value)
        {
            tbSearch.Text = value.ToString();
            tbSearch.Enabled = false;
        }

        public void setDriverLicenseInfo(clsLicense license, clsPerson person)
        {
            driverLicenseInfo1.license = license;
            driverLicenseInfo1.person = person;
            driverLicenseInfo1.callFindControlsWithDataMethod();
        }

        public void prepareObjects(int licenseID)
        {
            _person = clsPerson.findByLicenseID(licenseID);
            _license = clsLicense.findByLicenseID(licenseID);
            _detainedLicense = clsDetainedLicense.findByLicenseID(licenseID);
        }

        public void setDetainInfo(int licenseID)
        {
            detainInfoForRelease1.setControlsDefaultData();
            detainInfoForRelease1.setLblLicenseID(licenseID);
            detainInfoForRelease1.setControlsAfterSelections(_detainedLicense);
            _enableOrDisableLicenseHistory_ShowLicenseInfo();
            btnReleaseDetainedLicense.Enabled = true;
        }
    }
}
