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
    public partial class frmInternationalDrivingLicenseApplications : Form
    {
        private clsPerson _selectedPerson;
        private clsInternationalLicense _selectedInternationalLicense;
        public frmInternationalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _loadAllDrivingLicenseApplications()
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicense.loadAllInternationalLicenses();
            _countDgvRecords();
            _hidePersonIDInDgv();

        }

        private void _countDgvRecords()
        {
            lblNumOfRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void _selectedCbFilterByItem()
        {
            cbFilterBy.SelectedItem = "None";
        }

        private void _selectedCbFilterByActiveItem()
        {
            cbFilterByActive.SelectedItem = "None";
        }

        private void _setContextMenuStripToDgv()
        {
            dgvInternationalLicenses.ContextMenuStrip = contextMenuStrip1;
        }

        private void frmInternationalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _loadAllDrivingLicenseApplications();
            _selectedCbFilterByItem();
            _setContextMenuStripToDgv();
        }

        private void btnAddInternationalLicense_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApplication frmInternationalLicenseApplication =
                new frmInternationalLicenseApplication();
            frmInternationalLicenseApplication.onIssueingInternationalLicense += _loadAllDrivingLicenseApplications;
            frmInternationalLicenseApplication.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                tbFilterBy.Visible = false;
                cbFilterByActive.Visible = false;
            }
            else if(cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                tbFilterBy.Visible = false;
                cbFilterByActive.Visible = true;
                _selectedCbFilterByActiveItem();
            }
            else
            {
                tbFilterBy.Visible = true;
                cbFilterByActive.Visible = false;
            }
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedItem == "None")
            {
                return;
            }

            // allow control keys like Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // block the character
            }
        }

        private void _getInternationalLicensesFilterByIntlLicenseID()
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicense.
                loadIntlLicensesFilterByIntlLicenseID(Convert.ToInt32(tbFilterBy.Text));

            _countDgvRecords();
            _hidePersonIDInDgv();

        }

        private void _getInternationalLicensesFilterByApplicationID()
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicense.
                loadIntlLicensesFilterByApplicationID(Convert.ToInt32(tbFilterBy.Text));

            _countDgvRecords();
            _hidePersonIDInDgv();

        }

        private void _getInternationalLicensesFilterByDriverID()
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicense.
                loadIntlLicensesFilterByDriverID(Convert.ToInt32(tbFilterBy.Text));

            _countDgvRecords();
            _hidePersonIDInDgv();

        }

        private void _getInternationalLicensesFilterByLocalLicenseID()
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicense.
                loadIntlLicensesFilterByLocalLicenseID(Convert.ToInt32(tbFilterBy.Text));

            _countDgvRecords();
            _hidePersonIDInDgv();

        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFilterBy.Text))
            {
                _loadAllDrivingLicenseApplications();
                return;
            }
            switch (cbFilterBy.SelectedItem.ToString())
            {
                case "International License ID":
                    _getInternationalLicensesFilterByIntlLicenseID();
                    break;
                case "Application ID":
                    _getInternationalLicensesFilterByApplicationID();
                    break;
                case "Driver ID":
                    _getInternationalLicensesFilterByDriverID();
                    break;
                case "Issued Using Local License ID":
                    _getInternationalLicensesFilterByLocalLicenseID();
                    break;
            }
        }

        private void _getInternationalLicensesFilterByActive(bool Active)
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicense.
                loadIntlLicensesFilterByActive(Active);

            _countDgvRecords();
            _hidePersonIDInDgv();
        }

        private void cbFilterByActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbFilterByActive.SelectedItem.ToString())
            {
                case "Active":
                    _getInternationalLicensesFilterByActive(true);
                    break;
                case "InActive":
                    _getInternationalLicensesFilterByActive(false);
                    break;
                case "None":
                    _loadAllDrivingLicenseApplications();
                    break;
            }
        }

        private void _hidePersonIDInDgv()
        {
            dgvInternationalLicenses.Columns["PersonID"].Visible = false;
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails frmPersonDetails = new PersonDetails(_selectedPerson);
            frmPersonDetails.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
             int selectedPersonID = Convert.ToInt32(dgvInternationalLicenses.
                 CurrentRow.Cells["PersonID"].Value);
            _selectedPerson = clsPerson.find(selectedPersonID);

            int selectedInternationalLicenseID = Convert.ToInt32(dgvInternationalLicenses.
                 CurrentRow.Cells["InternationalLicenseID"].Value);
            _selectedInternationalLicense = clsInternationalLicense.find(selectedInternationalLicenseID);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverInternationalLicenseInfo frmDriverInternationalLicenseInfo = 
              new frmDriverInternationalLicenseInfo(_selectedPerson, _selectedInternationalLicense);
            frmDriverInternationalLicenseInfo.fillControlsWithData();
            frmDriverInternationalLicenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(_selectedPerson);
            frmLicenseHistory.ShowDialog();
        }
    }
}
