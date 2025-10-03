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
    public partial class DriverInternationalLicenseInfo : UserControl
    {
        public clsPerson person;
        public clsInternationalLicense internationalLicense;
        public DriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private string _getPersonFullName()
        {
            return person.FirstName + " " + person.SecondName + " " + person.ThirdName
                + " " + person.LastName;
        }
        public void fillControlsWithData()
        {
            lblName.Text = _getPersonFullName();
            lblIntLicenseID.Text = internationalLicense.InternationalLicenseID.ToString();
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = person.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text = internationalLicense.IssueDate.ToShortDateString();
            lblApplicationID.Text = internationalLicense.ApplicationID.ToString();
            lblIsActive.Text = internationalLicense.IsActive == true ? "Active" : "InActive";
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = internationalLicense.DriverID.ToString();
            lblExpirationDate.Text = internationalLicense.ExpirationDate.ToShortDateString();
        }
    }
}
