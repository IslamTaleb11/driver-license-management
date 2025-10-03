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
    public partial class frmReplacmentForDamagedOrLostLicense : Form
    {
        private clsLicense _searchedLicense;
        private clsApplication _newApplication;
        private clsLicense _newLicense;
        public frmReplacmentForDamagedOrLostLicense()
        {
            InitializeComponent();
        }

        private void _fillDriverLicenseInfoControlWithData()
        {
            driverLicenseInfo1.license = _searchedLicense;
            driverLicenseInfo1.person = clsPerson.findByLicenseID(_searchedLicense.LicenseID);
            driverLicenseInfo1.callFindControlsWithDataMethod();
        }

        private void _fillApplicationInfoForLicenseReplacementControlWithData()
        {
            applicationInfoForLicenseReplacement1.fillControlsAfterSelectingALicense(_searchedLicense.LicenseID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                _searchedLicense = clsLicense.findByLicenseID(Convert.ToInt32(tbSearch.Text));
                if (_searchedLicense != null)
                {
                    llbShowLicenseInfo.Enabled = false;

                    _fillDriverLicenseInfoControlWithData();
                    _fillApplicationInfoForLicenseReplacementControlWithData();
                    if (_searchedLicense.IsActive != false)
                    {
                        btnIssueReplacement.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(
                            "The selected License is InActive!",
                            "License Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                else
                {
                    MessageBox.Show(
                        $"There is no license with the ID of: {tbSearch.Text}.",
                        "Not Found!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Please enter a value before searching.",
                    "Input Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblFormTitle.Text = "Replacement For Lost License";
            applicationInfoForLicenseReplacement1.fillApplicationFeesControl("Replacement for a Lost Driving License");
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblFormTitle.Text = "Replacement For Damaged License";
            applicationInfoForLicenseReplacement1.fillApplicationFeesControl("Replacement for a Damaged Driving License");
        }

        private void _setRbDefaultValue()
        {
            rbDamagedLicense.Checked = true;
        }

        private void frmReplacmentForDamagedOrLostLicense_Load(object sender, EventArgs e)
        {
            _setRbDefaultValue();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _prepareSearchedLicenseObject()
        {
            _searchedLicense.IsActive = false;
        }

        private void _prepareNewApplicationObject()
        {
            string applicationTypeTitle = rbDamagedLicense.Checked ? "Replacement for a Damaged Driving License" :
                "Replacement for a Lost Driving License";
            clsApplication oldApplication = clsApplication.find(_searchedLicense.ApplicationID);
            _newApplication = new clsApplication();
            _newApplication.ApplicantPersonID = oldApplication.ApplicantPersonID;
            _newApplication.ApplicationDate = DateTime.Now;
            _newApplication.ApplicationTypeID = clsApplicationType.find(applicationTypeTitle).ID;
            _newApplication.ApplicationStatus = (byte)(rbDamagedLicense.Checked ? 3 : 4);
            _newApplication.LastStatusDate = DateTime.Now;
            _newApplication.PaidFees = clsApplicationType.GetApplicationFees(applicationTypeTitle);
            _newApplication.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
        }

        private void _prepareNewLicenseObject()
        {
            clsLicenseClass licenseClass = clsLicenseClass.find(_searchedLicense.LicenseClassID);
            _newLicense = new clsLicense();
            _newLicense.ApplicationID = _newApplication.ApplicationID;
            _newLicense.DriverID = _searchedLicense.DriverID;
            _newLicense.LicenseClassID = _searchedLicense.LicenseClassID;
            _newLicense.IssueDate = DateTime.Now;
            _newLicense.ExpirationDate = DateTime.Now.AddYears(
                licenseClass.DefaultValidityLength);
            _newLicense.Notes = "";
            _newLicense.PaidFees = licenseClass.ClassFees;
            _newLicense.IsActive = true;
            _newLicense.IssueReason = (byte)(rbLostLicense.Checked ? 2 : 3);
            _newLicense.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save this license?",
                                "Confirm Save",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                _showError("License save cancelled by user.");
                return;
            }
            _prepareSearchedLicenseObject();
            if (!_searchedLicense.save())
            {
                _showError("Failed to save searched license.");
                return;
            }

            _prepareNewApplicationObject();
            if (!_newApplication.save())
            {
                _showError("Failed to save application.");
                return;
            }

            _prepareNewLicenseObject();


            if (_newLicense.save())
            {
                _showSuccess("License saved successfully!");
                applicationInfoForLicenseReplacement1.
                    fillControlsDataAfterSavingNewLicense(_newApplication.ApplicationID, _newLicense.LicenseID);
                btnIssueReplacement.Enabled = false;
                llbShowLicenseInfo.Enabled = true;
            }
            else
            {
                _showError("Failed to save new license.");
            }
        }

        private void _showError(string message)
        {
            MessageBox.Show(message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        private void _showSuccess(string message)
        {
            MessageBox.Show(message,
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.findByLicenseID(_newLicense.LicenseID);
            frmDriverLicenseInfo frmDriverLicenseInfo = new frmDriverLicenseInfo(_newLicense, person);
            frmDriverLicenseInfo.fillControlsWithData();
            frmDriverLicenseInfo.ShowDialog();
        }

        private void llbShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.findByLicenseID(_newLicense.LicenseID);
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(person);
            frmLicenseHistory.ShowDialog();
        }
    }
}

