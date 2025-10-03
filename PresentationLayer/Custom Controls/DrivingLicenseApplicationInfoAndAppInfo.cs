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

namespace DVLD.Custom_Controls
{
    public partial class DrivingLicenseApplicationInfoAndAppInfo : UserControl
    {
        private clsUser _user;
        public clsLocalDrivingLicenseApplication localDrivingLicenseApplication;
        public clsLicenseClass licenseClass;
        public clsApplication application;
        public clsPerson person;


        public int trailsPerTest;
        public DrivingLicenseApplicationInfoAndAppInfo()
        {
            InitializeComponent();
        }

        public void loadControlsWithData(int applicationID)
        {
            LoadLocalDrivingLicenseData(applicationID);
            LoadLicenseClassData();
            LoadApplicationData(applicationID);
            LoadUserData();
        }

        private void LoadLocalDrivingLicenseData(int applicationID)
        {
            localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.findByApplicationID(applicationID);

            int localAppID = localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            lblDLAID.Text = localAppID.ToString();
            lblDLAppID.Text = localAppID.ToString();

            lblPassTests.Text = clsLocalDrivingLicenseApplication
                .getPassedTestsOnApplicationByAppID(applicationID)
                .ToString() + "/3";
        }

        private void LoadLicenseClassData()
        {
            licenseClass = clsLicenseClass.find(localDrivingLicenseApplication.LicenseClassID);
            lblAppliedForLicense.Text = licenseClass.ClassName;
        }

        private void LoadApplicationData(int applicationID)
        {
            application = clsApplication.find(applicationID);

            lblAppID.Text = application.ApplicationID.ToString();
            lblAppStatus.Text = GetApplicationStatusText(application.ApplicationStatus);
            lblAppFees.Text = Math.Truncate(application.PaidFees).ToString();
            lblAppType.Text = application.ApplicationTypeName;
            lblAppDate.Text = application.ApplicationDate.ToShortDateString();
            lblAppStatusDate.Text = application.LastStatusDate.ToShortDateString();
            lblAppApplicant.Text = application.ApplicantPersonFullName;
        }

        private void LoadUserData()
        {
            _user = clsUser.find(application.CreatedByUserID);
            lblAppCreatedBy.Text = _user.UserName;
        }

        private string GetApplicationStatusText(int status)
        {
            return status == 1 ? "New" : (status == 2 ? "Canceled" : "Completed");
        }



        private void llbViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails frmPersonDetails = new PersonDetails(person);
            frmPersonDetails.ShowDialog();
        }
    }
}
