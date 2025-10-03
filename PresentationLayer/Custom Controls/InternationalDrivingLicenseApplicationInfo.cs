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
    public partial class InternationalDrivingLicenseApplicationInfo : UserControl
    {
        public clsLicense license;
        public clsInternationalLicense internationalLicense;
        public InternationalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _fillControlsWithDefaultData()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblCreatedBy.Text = clsGlobalSettings.currentLoggedInUser.UserName;
        }

        public void fillControlsAfterIntitializingLocalDrivingLicense()
        {
            lblLocalLicenseID.Text = license.LicenseID.ToString();
            lblFees.Text = Math.Truncate(
                clsApplicationType.GetApplicationFees("New International License")).ToString();
        }

        public void fillControlsAfterIssueingInternationalDrivingLicense()
        {
            lblInternationalLicenseAppID.Text = internationalLicense.ApplicationID.ToString();
            lblILicenseID.Text = internationalLicense.InternationalLicenseID.ToString();
        }

        private void InternationalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            if (!DesignMode) // only run at runtime, not in designer
            {
                _fillControlsWithDefaultData();
            }
        }
    }
}
