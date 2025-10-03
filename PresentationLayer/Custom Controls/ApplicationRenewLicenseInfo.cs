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
    public partial class ApplicationRenewLicenseInfo : UserControl
    {
        public clsLicense oldLicense;
        public ApplicationRenewLicenseInfo()
        {
            InitializeComponent();
        }

        public void fillControlsWithDefaultData()
        {
            decimal licenseFees = 
                clsLicenseClass.find(oldLicense.LicenseClassID).ClassFees;
            decimal applicationFees = clsApplicationType.
                GetApplicationFees("Renew Driving License Service");


            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = Math.Truncate(applicationFees).ToString();
            lblLicenseFees.Text = Math.Truncate(licenseFees).ToString();
            lblOldLicenseID.Text = oldLicense.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears
                ((int)clsLicenseClass.find(oldLicense.LicenseClassID).DefaultValidityLength).ToString();
            lblCreatedBy.Text = clsGlobalSettings.currentLoggedInUser.UserName;
            lblTotalFees.Text =
            (Convert.ToUInt32(applicationFees) + Convert.ToUInt32(licenseFees)).ToString();

        }

        public string rtbNotesValues()
        {
            return rtbNotes.Text;
        }

        public void fillControlsAfterRenewingLicense(int renewedLicenseApplicationID, 
            int renewedLicenseID)
        {
            lblRLApplicationID.Text = renewedLicenseApplicationID.ToString();
            lblRenewedLicenseID.Text = renewedLicenseID.ToString();
        }
    }
}
