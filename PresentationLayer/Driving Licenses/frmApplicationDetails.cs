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
    public partial class frmApplicationDetails : Form
    {
        public frmApplicationDetails()
        {
            InitializeComponent();
        }

        public void fillControlsWithData(int applicationsID, clsPerson person)
        {
            drivingLicenseApplicationInfoAndAppInfo1.loadControlsWithData(applicationsID);
            drivingLicenseApplicationInfoAndAppInfo1.person = person;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
