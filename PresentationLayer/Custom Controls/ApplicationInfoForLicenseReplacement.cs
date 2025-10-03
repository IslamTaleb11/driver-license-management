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
    public partial class ApplicationInfoForLicenseReplacement : UserControl
    {
        public ApplicationInfoForLicenseReplacement()
        {
            InitializeComponent();
        }

        private void _fillControlsWithDefaultData()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobalSettings.currentLoggedInUser.UserID.ToString();
            lblApplicationFees.Text = Math.Truncate(
                clsApplicationType.GetApplicationFees("Replacement for a Damaged Driving License")).ToString();
        }

        private void ApplicationInfoForLicenseReplacement_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
              _fillControlsWithDefaultData();  
            }
        }

        public void fillControlsAfterSelectingALicense(int oldLicenseID)
        {
            lblOldLicenseID.Text = oldLicenseID.ToString();
        }

        public void fillApplicationFeesControl(string applicationTypeTitle)
        {
            lblApplicationFees.Text = Math.Truncate(
                clsApplicationType.GetApplicationFees(applicationTypeTitle)).ToString();
        }

        public void fillControlsDataAfterSavingNewLicense(int licenseReplacementAppID,
            int renewedLicenseID)
        {
            lblRLApplicationID.Text = licenseReplacementAppID.ToString();
            lblRenewedLicenseID.Text = renewedLicenseID.ToString();
        }
    }
}
