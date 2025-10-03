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
    public partial class DetainInfoForRelease : UserControl
    {
        public DetainInfoForRelease()
        {
            InitializeComponent();
        }

        public void setControlsDefaultData()
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobalSettings.currentLoggedInUser.UserName;
        }

        public void setLblApplicationID(int ApplicationID)
        {
            lblApplicationID.Text = ApplicationID.ToString();
        }

        private void DetainInfo_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
        }

        public void setLblLicenseID(int LicenseID)
        {

            lblLicenseID.Text = LicenseID.ToString();
        }

        public void setControlsAfterSelections(clsDetainedLicense DetainedLicense)
        {
            decimal applicationFees = clsApplicationType.GetApplicationFees("Release Detained Driving Licsense");
            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblApplicationFees.Text = Math.Truncate(applicationFees).ToString();
            lblTotalFees.Text = Math.Truncate(applicationFees + DetainedLicense.FineFees).ToString();
            lblFineFees.Text = Math.Truncate(DetainedLicense.FineFees).ToString();
        }
    }
}
