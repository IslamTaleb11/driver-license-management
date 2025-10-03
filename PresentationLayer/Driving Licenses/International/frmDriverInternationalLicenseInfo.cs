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

namespace DVLD.Driving_Licenses.International
{
    public partial class frmDriverInternationalLicenseInfo : Form
    {
        private clsPerson _person;
        private clsInternationalLicense _internationalLicense;
        public frmDriverInternationalLicenseInfo(clsPerson person, clsInternationalLicense internationalLicense)
        {
            InitializeComponent();
            this._person = person;
            this._internationalLicense = internationalLicense;
        }

        public void fillControlsWithData()
        {
            driverInternationalLicenseInfo1.person = _person;
            driverInternationalLicenseInfo1.internationalLicense = _internationalLicense;
            driverInternationalLicenseInfo1.fillControlsWithData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
