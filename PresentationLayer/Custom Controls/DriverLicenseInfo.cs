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
    public partial class DriverLicenseInfo : UserControl
    {
        public clsLicense license;
        public clsPerson person;
        public DriverLicenseInfo()
        {
            InitializeComponent();
        }

        private string _getFirstName()
        {
            return person.FirstName + " " + person.SecondName + " " + person.ThirdName + " "
                + person.LastName;
        }

        public void callFindControlsWithDataMethod()
        {
            _fillControlsWithData();
        }

        private void _fillControlsWithData()
        {
            if (this.DesignMode) return; // skip when in designer

            lblClass.Text = clsLicenseClass.find(license.LicenseClassID).ClassName;
            lblName.Text = _getFirstName();
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = person.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text = license.IssueDate.ToShortDateString();
            lblIssueReason.Text = license.IssueReason == 1 ? "First Time" :
                license.IssueReason == 2 ? "Renew" : license.IssueReason == 3 ? "Replacement for Damaged" :
                "Replacement for Lost";
            lblNotes.Text = string.IsNullOrWhiteSpace(license.Notes) ? "No Notes" : license.Notes;
            lblIsActive.Text = license.IsActive == true ? "Active" : "InActive";
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = license.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = clsDetainedLicense.isLicenseDetained(license.LicenseID) ? "Yes" : "No";
        }
        private void DriverLicenseInfo_Load(object sender, EventArgs e)
        {
            //_fillControlsWithData();
        }
    }
}
