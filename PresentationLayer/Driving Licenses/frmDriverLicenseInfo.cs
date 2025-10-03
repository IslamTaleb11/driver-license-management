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
    public partial class frmDriverLicenseInfo : Form
    {
        private clsLicense _license;
        private clsPerson _person;
        public frmDriverLicenseInfo(clsLicense license, clsPerson person)
        {
            InitializeComponent();
            this._license = license;
            this._person = person;
        }

        public void fillControlsWithData()
        {

            driverLicenseInfo1.license = this._license;
            driverLicenseInfo1.person = this._person;
            driverLicenseInfo1.callFindControlsWithDataMethod();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
