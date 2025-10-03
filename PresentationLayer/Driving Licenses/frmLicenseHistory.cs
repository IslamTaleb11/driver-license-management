using BussinessLayer;
using DVLD.Custom_Controls;
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

namespace DVLD.Driving_Licenses
{
    public partial class frmLicenseHistory : Form
    {
        private clsPerson _person;

        public frmLicenseHistory(clsPerson person)
        {
            InitializeComponent();
            this._person = person;
            findPersonControl1.person = this._person;
        }

        private void _loadAllLocalDrivingLicenses()
        {
            dgvLocalLicenses.DataSource = clsLicense.loadAllLocalDrivingLicenses(_person.ID);
            lblLocalNumOfRecords.Text = dgvLocalLicenses.Rows.Count.ToString();
        }

        private void _loadAllInternationalDrivingLicenses()
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicense.loadPersonInternationalLicenses(_person.ID);
        }

        private void _initializeContextMenuStripInDgv()
        {
            dgvInternationalLicenses.ContextMenuStrip = contextMenuStrip1;
            dgvLocalLicenses.ContextMenuStrip = contextMenuStrip1;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            findPersonControl1.disableSearchOrAddUser();
            findPersonControl1.addDataToSearchUserControl();
            findPersonControl1.fillControlsWithData();
            _loadAllLocalDrivingLicenses();
            _loadAllInternationalDrivingLicenses();
            _initializeContextMenuStripInDgv();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                lblLocalNumOfRecords.Text = dgvLocalLicenses.Rows.Count.ToString();
                return;
            }
            lblLocalNumOfRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                int licenseID = Convert.ToInt32(dgvLocalLicenses.CurrentRow.Cells["Lic.ID"].Value);
                clsLicense license = clsLicense.findByLicenseID(licenseID);
                frmDriverLicenseInfo frmDriverLicenseInfo = new frmDriverLicenseInfo(license, _person);
                frmDriverLicenseInfo.fillControlsWithData();
                frmDriverLicenseInfo.ShowDialog();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                int internationalLicenseID = Convert.ToInt32
                    (dgvInternationalLicenses.CurrentRow.Cells["InternationalLicenseID"].Value);

                clsInternationalLicense internationalLicense =
                    clsInternationalLicense.find(internationalLicenseID);

                frmDriverInternationalLicenseInfo frmDriverInternationalLicenseInfo =
                    new frmDriverInternationalLicenseInfo(_person, internationalLicense);

                frmDriverInternationalLicenseInfo.fillControlsWithData();
                frmDriverInternationalLicenseInfo.ShowDialog();
            }
        }
    }
}
