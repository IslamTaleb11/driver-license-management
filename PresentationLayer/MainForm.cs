using DVLD.Drivers;
using DVLD.Driving_Licenses;
using DVLD.Driving_Licenses.International;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class MainForm : Form
    {
        public event Action onSignout;
        public MainForm()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmPeople = new frmPeople();
            frmPeople.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form usersFrm = new frmUsers();
            usersFrm.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails frmUserDetails = new frmUserDetails(clsGlobalSettings.currentLoggedInUser);
            frmUserDetails.Show();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword(clsGlobalSettings.currentLoggedInUser);
            frmChangeUserPassword.Show();

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginScreen frmLoginScreen = new frmLoginScreen();
            frmLoginScreen.Show();
            this.Close();
        }

        private void drivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationTypes frmApplicationTypes = new frmApplicationTypes();
            frmApplicationTypes.Show();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();

            frmManageTestTypes.Show();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicense frmAddNewLocalDrivingLicense = new frmAddNewLocalDrivingLicense();
            frmAddNewLocalDrivingLicense.Show();
        }

        private void localDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frmLocalDrivingLicenseApplications = new frmLocalDrivingLicenseApplications();
            frmLocalDrivingLicenseApplications.Show();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDrivers frmManageDrivers = new frmManageDrivers();
            frmManageDrivers.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApplication frmInternationalLicenseApplication = new frmInternationalLicenseApplication();
            frmInternationalLicenseApplication.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDrivingLicenseApplications frmInternationalDrivingLicenseApplications
                = new frmInternationalDrivingLicenseApplications();

            frmInternationalDrivingLicenseApplications.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frmRenewLicenseApplication = new frmRenewLicenseApplication();
            frmRenewLicenseApplication.ShowDialog();
        }

        private void replacmentForLostOrDamagedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacmentForDamagedOrLostLicense frmReplacmentForDamagedOrLostLicense =
                new frmReplacmentForDamagedOrLostLicense();
            frmReplacmentForDamagedOrLostLicense.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frmListDetainedLicenses = new frmListDetainedLicenses();
            frmListDetainedLicenses.ShowDialog();
        }
    }
}
