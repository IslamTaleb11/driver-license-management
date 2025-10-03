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
    public partial class frmRenewLicenseApplication : Form
    {
        private clsLicense _license;
        private clsPerson _person;
        private clsApplication _application;
        private clsApplication _oldApplication;
        private clsLicense _newLicense;
        public frmRenewLicenseApplication()
        {
            InitializeComponent();
        }

        private bool _isSearchByEmpty()
        {
            return string.IsNullOrWhiteSpace(tbSearch.Text);
        }

        private void _initializeObjects(clsLicense license)
        {
            _license = license;
            _person = clsPerson.findByLicenseID(Convert.ToInt32(tbSearch.Text));
        }

        private void _fillDriverLicenseInfoControls()
        {
                driverLicenseInfo1.person = _person;
                driverLicenseInfo1.license = _license;
                driverLicenseInfo1.callFindControlsWithDataMethod();
        }


        private void _fillApplicationRenewLicenseInfoDefaultData()
        {
            applicationRenewLicenseInfo1.oldLicense = _license;
            applicationRenewLicenseInfo1.fillControlsWithDefaultData();
        }

        private void _initializeOldApplication()
        {
            _oldApplication = clsApplication.find(_license.ApplicationID);
        }

        private void _setSearchedLicenseToInActive(clsLicense searchedLicense)
        {
            searchedLicense.IsActive = false;
            if (!searchedLicense.save())
            {
                MessageBox.Show(
                    "Failed to save the license. Please try again.",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_isSearchByEmpty())
            {
                clsLicense searchedLicense = clsLicense.findByLicenseID(Convert.ToInt32(tbSearch.Text));
                if ((searchedLicense != null) && searchedLicense.IsActive != false)
                {
                    _initializeObjects(searchedLicense);
                    _fillDriverLicenseInfoControls();

                    if (DateTime.Now >= _license.ExpirationDate)
                    {
                        _fillApplicationRenewLicenseInfoDefaultData();
                        _initializeOldApplication();
                        _setSearchedLicenseToInActive(searchedLicense);
                        btnRenew.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(
                            $@"Selected License is not yet expired! it will expire on: 
                            {_license.ExpirationDate}",
                            "Not Allowed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                else
                {
                    MessageBox.Show(
                        "No license found with the provided ID.",
                        "Search Result",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Please enter search criteria before continuing.",
                    "Empty Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow control keys like Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // block the character
            }
        }

        private void _prepareApplicationObject()
        {
            _application = new clsApplication();
            _application.ApplicantPersonID = _oldApplication.ApplicantPersonID;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationTypeID = clsApplicationType.find("Renew Driving License Service").ID;
            _application.ApplicationStatus = 1;
            _application.LastStatusDate = DateTime.Now;
            _application.PaidFees = clsApplicationType.find("Renew Driving License Service").Fees;
            _application.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
        }

        private void _prepareLicenseObject()
        {
            _newLicense = new clsLicense();
            _newLicense.ApplicationID = _application.ApplicationID;
            _newLicense.DriverID = _license.DriverID;
            _newLicense.LicenseClassID = _license.LicenseClassID;
            _newLicense.IssueDate = DateTime.Now;
            _newLicense.ExpirationDate = DateTime.Now.AddYears
                (clsLicenseClass.find(_license.LicenseClassID).DefaultValidityLength);
            _newLicense.Notes = applicationRenewLicenseInfo1.rtbNotesValues();
            _newLicense.PaidFees = _license.PaidFees;
            _newLicense.IsActive = true;
            _newLicense.IssueReason = 2;
            _newLicense.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            // Show a confirmation message before proceeding
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to continue?",
                "Confirm Action",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _prepareApplicationObject();

                    if (_application.save())
                    {
                        _prepareLicenseObject();

                        if (_newLicense.save())
                        {
                            MessageBox.Show(
                                "License saved successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            applicationRenewLicenseInfo1.
                                fillControlsAfterRenewingLicense(_application.ApplicationID, _newLicense.LicenseID);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to save the new license.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to save new license.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "An unexpected error occurred: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            btnRenew.Enabled = false;

        }
    }
}
