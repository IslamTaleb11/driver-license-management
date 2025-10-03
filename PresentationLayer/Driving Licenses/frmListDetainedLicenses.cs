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
    public partial class frmListDetainedLicenses : Form
    {
        clsPerson _person;
        clsLicense _license;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _loadAllDetainedLicenses()
        {
            dataGridView1.DefaultCellStyle.NullValue = "NULL";
            dataGridView1.DataSource = clsDetainedLicense.getAllDetainedLicenses();
            _setContextMenuStrip();
            _setLblNumOfRecords();
        }

        private void _setLblNumOfRecords()
        {
            lblNumberOfRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void _setContextMenuStrip()
        {
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _loadAllDetainedLicenses();
            _setCbFilterByDefaultItem();
        }

        private void _setCbFilterByDefaultItem()
        {
            cbFilterBy.SelectedItem = "None";
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails frmPersonDetails = new PersonDetails(_person.ID);
            frmPersonDetails.ShowDialog();
        }

        private void showLicenseDetaillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverLicenseInfo frmDriverLicenseInfo = new frmDriverLicenseInfo(_license, _person);
            frmDriverLicenseInfo.fillControlsWithData();
            frmDriverLicenseInfo.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _person = clsPerson.find(Convert.ToInt32(dataGridView1.CurrentRow.Cells["PersonID"].Value));
            _license = clsLicense.findByLicenseID(Convert.ToInt32
                (dataGridView1.CurrentRow.Cells["LicenseID"].Value));

            bool isReleased = (bool)dataGridView1.CurrentRow.Cells["IsReleased"].Value;

            releaseDetainedLicenseToolStripMenuItem.Enabled = isReleased ? false : true;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(_person);
            frmLicenseHistory.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.prepareObjects(_license.LicenseID);
            frmReleaseDetainedLicense.setAndDisableTbSearchForm(_license.LicenseID);
            frmReleaseDetainedLicense.setDriverLicenseInfo(_license, _person);
            frmReleaseDetainedLicense.setDetainInfo(_license.LicenseID);
            frmReleaseDetainedLicense.ShowDialog();
            
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                tbFilterBy.Visible = false;
                cbIsReleased.Visible = false;
                _loadAllDetainedLicenses();
            }
            else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
            {
                tbFilterBy.Visible = false;
                cbIsReleased.Visible = true;
            }
            else
            {
                tbFilterBy.Visible = true;
                cbIsReleased.Visible = false;
            }
        }

        private void _loadDetainedLicensesFilterByDetainID()
        {
            dataGridView1.DataSource = clsDetainedLicense.getDetainedLicensesFilterByDetainID
                (Convert.ToInt32(tbFilterBy.Text));
            _setContextMenuStrip();
            _setLblNumOfRecords();
        }

        private void _loadDetainedLicensesFilterByNationalNo()
        {
            dataGridView1.DataSource = clsDetainedLicense.getDetainedLicensesFilterByNationalNo
                (tbFilterBy.Text);
            _setContextMenuStrip();
            _setLblNumOfRecords();
        }

        private void _loadDetainedLicensesFilterByFullName()
        {
            dataGridView1.DataSource = clsDetainedLicense.getDetainedLicensesFilterByFullName
                (tbFilterBy.Text);
            _setContextMenuStrip();
            _setLblNumOfRecords();
        }

        private void _loadDetainedLicensesFilterByReleaseAppID()
        {
            dataGridView1.DataSource = clsDetainedLicense.getDetainedLicensesFilterByReleaseAppID
                (Convert.ToInt32(tbFilterBy.Text));
            _setContextMenuStrip();
            _setLblNumOfRecords();
        }
        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFilterBy.Text))
            {
                _loadAllDetainedLicenses();
                return;
            }

            switch (cbFilterBy.SelectedItem.ToString())
            {
                case "None":
                    _loadAllDetainedLicenses();
                    break;
                case "Detain ID":
                    _loadDetainedLicensesFilterByDetainID();
                    break;
                case "National No":
                    _loadDetainedLicensesFilterByNationalNo();
                    break;
                case "Full Name":
                    _loadDetainedLicensesFilterByFullName();
                    break;
                case "Release Application ID":
                    _loadDetainedLicensesFilterByReleaseAppID();
                    break;
            }
        }

        private void _loadDetainedLicensesFilterByAllReleased()
        {
            dataGridView1.DataSource = clsDetainedLicense.getDetainedLicensesFilterByAllReleased();
            _setContextMenuStrip();
            _setLblNumOfRecords();
        }

        private void _loadDetainedLicensesFilterByNotReleased()
        {
            dataGridView1.DataSource = clsDetainedLicense.getDetainedLicensesFilterByNotReleased();
            _setContextMenuStrip();
            _setLblNumOfRecords();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsReleased.SelectedItem.ToString())
            {
                case "Yes":
                    _loadDetainedLicensesFilterByAllReleased();
                    break;
                case "No":
                    _loadDetainedLicensesFilterByNotReleased();
                    break;
            }
        }
    }
}
