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

namespace DVLD.Driving_Licenses.International
{
    public partial class frmInternationalLicenseApplication : Form
    {
        private clsLicense _license;
        private clsApplication _application;
        private clsInternationalLicense _internationalLicense;
        public event Action onIssueingInternationalLicense;
        public frmInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void _initializeControls(int LicenseID)
        {
            clsLicense license = clsLicense.findByLicenseID(LicenseID);
            driverLicenseInfo1.license = license;
            driverLicenseInfo1.person = clsPerson.findByLicenseID(LicenseID);
            driverLicenseInfo1.callFindControlsWithDataMethod();

            internationalDrivingLicenseApplicationInfo1.license = license;
            internationalDrivingLicenseApplicationInfo1.fillControlsAfterIntitializingLocalDrivingLicense();
        }

        private void _prepareApplicationObject()
        {
            clsApplication localDrivingLicenseApplication = clsApplication.find(_license.ApplicationID);
            clsApplicationType applicationType = clsApplicationType.find("New International License");
            _application = new clsApplication();
            _application.ApplicantPersonID = localDrivingLicenseApplication.ApplicantPersonID;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationTypeID = applicationType.ID;
            _application.ApplicationStatus = 1;
            _application.LastStatusDate = DateTime.Now;
            _application.PaidFees = applicationType.Fees;
            _application.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
            _application.ApplicantPersonFullName = localDrivingLicenseApplication.ApplicantPersonFullName;
            _application.ApplicationTypeName = applicationType.Title;
        }

        private void _prepareInternationalDrivingLicenseObject()
        {
            _internationalLicense = new clsInternationalLicense();
            _internationalLicense.ApplicationID = _application.ApplicationID;
            _internationalLicense.DriverID = _license.DriverID;
            _internationalLicense.IssuedUsingLocalLicenseID = _license.LicenseID;
            _internationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _internationalLicense.IsActive = true;
            _internationalLicense.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                int LicenseID = Convert.ToInt32(tbSearch.Text);
                _license = clsLicense.findByLicenseID(LicenseID);

                if (clsLicense.doesLicenseExistAndActive(LicenseID) && 
                    !clsLicense.isLicenseHasIntlLink(LicenseID))
                {
                    _initializeControls(LicenseID);
                    _enableControls();
                }
                else
                {
                    MessageBox.Show(
                        @"The selected record might not exist, may be inactive, 
                        or is already linked to an international license.",
                        "Invalid Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    _disableControls();
                }
            }
            else
            {
                MessageBox.Show("Please enter a search term.",
                    "Input Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _disableControls();
            }
        }

        private void _enableControls()
        {
            llbShowLicensesHistory.Enabled = true;
            btnIssue.Enabled = true;
        }

        private void _disableControls()
        {
            llbShowLicensesHistory.Enabled = false;
            btnIssue.Enabled = false;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.findByLicenseID(_license.LicenseID);
            frmDriverInternationalLicenseInfo frmDriverInternationalLicenseInfo = new 
                frmDriverInternationalLicenseInfo(person, _internationalLicense);
            frmDriverInternationalLicenseInfo.fillControlsWithData();
            frmDriverInternationalLicenseInfo.ShowDialog();
        }

        private void llbShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.findByLicenseID(_license.LicenseID);
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(person);
            frmLicenseHistory.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _prepareApplicationObject();

            if (_application.save())
            {
                _prepareInternationalDrivingLicenseObject();
                if (_internationalLicense.save())
                {
                    MessageBox.Show(
                        "Internation Driving License saved successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    internationalDrivingLicenseApplicationInfo1.internationalLicense = _internationalLicense;
                    internationalDrivingLicenseApplicationInfo1.
                        fillControlsAfterIssueingInternationalDrivingLicense();

                    llbShowLicenseInfo.Enabled = true;
                    btnIssue.Enabled = false;

                    this.onIssueingInternationalLicense?.Invoke();
                }
                else
                {
                    MessageBox.Show(
                        "Error issueing International driving License. Please try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    llbShowLicenseInfo.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show(
                    "Error issueing International driving License. Please try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
