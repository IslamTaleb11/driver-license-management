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
    public partial class frmIssueDrivingLicense : Form
    {
        private int _applicationID { get; set; }
        private clsPerson _person { get; set; }

        private clsDriver _driver;
        private clsLicense _license;
        private clsApplication _application;
        private string _licenseClassName;

        public event Action onIssueingDrivingLicense;
        public frmIssueDrivingLicense(int applicationID, clsPerson person, string licenseClassName)
        {
            InitializeComponent();
            this._applicationID = applicationID;
            this._person = person;
            this._application = clsApplication.find(applicationID);
            this._licenseClassName = licenseClassName;
        }

        public void sendDataToControl()
        {
            drivingLicenseApplicationInfoAndAppInfo1.loadControlsWithData(this._applicationID);
            drivingLicenseApplicationInfoAndAppInfo1.person = this._person;
        }

        private void _prepareDriverObject()
        {
            _driver = new clsDriver();
            _driver.PersonID = this._person.ID;
            _driver.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
            _driver.CreatedDate = DateTime.Now;
        }

        private void _prepareLicenseObject()
        {
            clsLicenseClass licenseClass = clsLicenseClass.find(_licenseClassName);
            _license = new clsLicense();
            _license.ApplicationID = _application.ApplicationID;
            _license.DriverID = _driver.DriverID;
            _license.LicenseClassID = licenseClass.LicenseClassID;
            _license.IssueDate = DateTime.Now;
            _license.ExpirationDate = DateTime.Now.AddYears(licenseClass.DefaultValidityLength);
            _license.Notes = rtbNotes.Text;
            _license.PaidFees = licenseClass.ClassFees;
            _license.IsActive = true;
            _license.IssueReason = 1;
            _license.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;

        }

        private bool _isPersonAlreadyADriver()
        {
            return clsDriver.isPersonAlreadyADriver(_person.ID);
        }

        private void _saveLicense()
        {
            _prepareLicenseObject();
            if (_license.save())
            {
                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.onIssueingDrivingLicense?.Invoke();
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isPersonAlreadyADriver())
            {
                _driver = clsDriver.find(_person.ID);
                _saveLicense();
            }
            else
            {
                _prepareDriverObject();
                if (_driver.save())
                {
                    _saveLicense();
                }
                else
                {
                    MessageBox.Show("An error occurred while saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
