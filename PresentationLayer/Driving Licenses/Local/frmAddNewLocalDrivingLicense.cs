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
    public partial class frmAddNewLocalDrivingLicense : Form
    {
        private clsPerson _person;
        private clsLicenseClass _licenseClass;
        private string _applicationTitle = "New Local Driving License Service";
        private clsApplication _application;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        public event Action onAddingNewLocalDrivingLicense;
        public frmAddNewLocalDrivingLicense()
        {
            InitializeComponent();
            findPersonControl1.onPersonSelected += _initialisePersonObject;
        }

        private void _initialisePersonObject(clsPerson person)
        {
            _person = person;
        }

        private void _populateLicenseClassComboAndSetID()
        {
             DataTable dt = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow dr in dt.Rows)
            {
                cbLicenseClasses.Items.Add(dr["ClassName"]);
            }
            cbLicenseClasses.SelectedItem = "Class 3 - Ordinary driving license";

            _licenseClass = clsLicenseClass.find("Class 3 - Ordinary driving license");
        }

        private void _fillLblDate()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
        }

        private void _fillLblApplicationFees()
        {
            decimal ApplicationFees = clsApplicationType.GetApplicationFees(_applicationTitle);
            ApplicationFees = Math.Truncate(ApplicationFees);
            if (ApplicationFees != -1)
            {
                lblApplicationFees.Text = ApplicationFees.ToString();
            }
            else
            {
                MessageBox.Show(
                    "Something went wrong while retrieving the application fees.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private void _fillLblCreatedBy()
        {
            lblCreatedBy.Text = clsGlobalSettings.currentLoggedInUser.UserName;
        }

        private void _fillControls()
        {
            _populateLicenseClassComboAndSetID();
            _fillLblDate();
            _fillLblApplicationFees();
            _fillLblCreatedBy();
        }

        private void _changeFindPersonControlMode()
        {
            findPersonControl1.addingMode = FindPersonControl.enAddingMode.AddNewDrivingLicense;
        }

        private void frmAddNewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _fillControls();
            _changeFindPersonControlMode();
        }

        private void btnNextTab_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _fillApplicationObjectAndReturn()
        {
            clsApplicationType applicationType = clsApplicationType.find(_applicationTitle);
            _application = new clsApplication();
            _application.ApplicantPersonID = _person.ID;
            _application.ApplicationTypeID = applicationType.ID;
            _application.ApplicationStatus = 1;
            _application.PaidFees = applicationType.Fees;
            _application.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;

        }

        private bool _hasLicenseInSameClass()
        {
            return clsLocalDrivingLicenseApplication.PersonHasLicenseInClass(_licenseClass.LicenseClassID, _person.ID);
        }

        private void _fillLocalDrivingLicenseApplicationObject()
        {
            _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            _localDrivingLicenseApplication.ApplicationID = _application.ApplicationID;
            _localDrivingLicenseApplication.LicenseClassID = _licenseClass.LicenseClassID;
        }

        private void _setLblDLAID()
        {
            lblDrivingLicenseApplicationID.Text = _application.ApplicationID.ToString();
        }

        private bool isMinimumAllowedAge()
        {
            DateTime today = DateTime.Today;

            int age = today.Year - _person.DateOfBirth.Year;
            if (age >= _licenseClass.MinimumAllowedAge)
            {
                return true;
            }
            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_person != null)
            {
                // first we check if the person doesn't have a driving license with the same class and its status not new
                if (_hasLicenseInSameClass())
                {
                    MessageBox.Show(
                        "The Selected Person Already Has a license in the same class.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                else
                {
                    if (isMinimumAllowedAge())
                    {
                        _fillApplicationObjectAndReturn();

                        if (_application.save())
                        {
                            _fillLocalDrivingLicenseApplicationObject();
                            if (_localDrivingLicenseApplication.save())
                            {
                                _setLblDLAID();
                                MessageBox.Show(
                                    "The local driving license application has been successfully created.",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );
                                this.onAddingNewLocalDrivingLicense?.Invoke();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Failed to save the local driving license application.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to save the main application.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            $"The person is not old enough to apply for this license class. " +
                            $"Minimum allowed age is {_licenseClass.MinimumAllowedAge} years.",
                            "Age Restriction",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Please select a person before attempting to do this action.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

        }

        private void cbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            _licenseClass = clsLicenseClass.find(cbLicenseClasses.SelectedItem.ToString());
        }
    }
}
