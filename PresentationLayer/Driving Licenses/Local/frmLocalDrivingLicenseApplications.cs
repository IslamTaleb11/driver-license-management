using BussinessLayer;
using DVLD.Driving_Licenses.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Driving_Licenses
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        private int _applicationId;
        private string _licenseClassName;
        private clsLicense _license;
        private clsPerson _person;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _loadAllApplications()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.getAllApplications();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;

            // hide ApplicationID column, the reason that this column in the datatable is we will
            // need the applicationID in the test appointment reservation form
            _hideApplicationIDColumn();
            _countRowsOnLbl();
        }

        

        private int _getPassedTestsOnApplication(int applicationID)
        {
            return clsLocalDrivingLicenseApplication.getPassedTestsOnApplicationByAppID(applicationID);
        }

        private void _countRowsOnLbl()
        {
            lblNumOfRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void _setCbSelectedFilterByDefaultItem()
        {
            cbFilterBy.SelectedItem = "None";
        }

        private void _setCbSelectedFilterByStatusDefaultItem()
        {
            cbFilterByStatus.SelectedItem = "All";
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _loadAllApplications();
            _setCbSelectedFilterByDefaultItem();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem != "None" && cbFilterBy.SelectedItem != "Status")
            {
                tbFilterBy.Visible = true;
                cbFilterByStatus.Visible = false;
            }
            else if(cbFilterBy.SelectedItem == "Status")
            {
                tbFilterBy.Visible = false;
                cbFilterByStatus.Visible = true;
                _setCbSelectedFilterByStatusDefaultItem();
            }
            else
            {
                tbFilterBy.Visible = false;
                cbFilterByStatus.Visible = false;

            }  
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedItem == "L.D.L.AppID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block the input
                }
            }
        }

        private void _loadAllApplicationsFilterByNationalNo()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.getAllApplicationsFilterByNationalNo(tbFilterBy.Text);
            _countRowsOnLbl();
        }
        private void _loadAllApplicationsFilterByLocalDrivingLicenseAppID()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.getAllApplicationsFilterByLocalDrivingLicenseAppID(Convert.ToInt32(tbFilterBy.Text));
            _countRowsOnLbl();
        }

        private void _loadAllApplicationsFilterByFullName()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.getAllApplicationsFilterByFullName(tbFilterBy.Text);
            _countRowsOnLbl();
        }

        private void _hideApplicationIDColumn()
        {
            if (dataGridView1.DataSource != null && dataGridView1.Columns.Contains("ApplicationID"))
            {
                dataGridView1.Columns["ApplicationID"].Visible = false;
            }
        }

        private void _loadAllApplicationsFilterByStatus()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.getAllApplicationsFilterByStatus(cbFilterByStatus.SelectedItem.ToString());
            _hideApplicationIDColumn();
            _countRowsOnLbl();
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFilterBy.Text))
            {
                _loadAllApplications();
            }
            else
            {
                switch (cbFilterBy.SelectedItem)
                {
                    case "NationalNo":
                        _loadAllApplicationsFilterByNationalNo();
                        break;
                    case "L.D.L.AppID":
                        _loadAllApplicationsFilterByLocalDrivingLicenseAppID();
                        break;
                    case "FullName":
                        _loadAllApplicationsFilterByFullName();
                        break;
                }
            }
            
        }

        private void cbFilterByStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterByStatus.SelectedItem.ToString() == "All")
            {
                _loadAllApplications();
            }
            else
            {
                _loadAllApplicationsFilterByStatus();
            }
        }

        private void _checkIfPersonHaveAlreadyADrivingLicense()
        {
            int applicationID =
                Convert.ToInt32(dataGridView1.CurrentRow.Cells["ApplicationID"].Value);
            string personNationNo = dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString();
            if (clsLicense.hasExistingDrivingLicense(applicationID, personNationNo))
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = true;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            }
            else
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
            }
        }

        private void _applyContextMenuRules(int passedTestsOnApp)
        {

            if (clsApplication.isCanceled(_applicationId))
            {
                showApplicationDetailsToolStripMenuItem.Enabled = true;
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                scheduleTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                return;
            }
            if (passedTestsOnApp == 0)
            {
                scheduleVesionTestToolStripMenuItem.Enabled = true;
                scheduleWritingTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                scheduleTestsToolStripMenuItem.Enabled = true;

                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
            }
            else if (passedTestsOnApp == 1)
            {
                scheduleVesionTestToolStripMenuItem.Enabled = false;
                scheduleWritingTestToolStripMenuItem.Enabled = true;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                scheduleTestsToolStripMenuItem.Enabled = true;

                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
            }
            else if (passedTestsOnApp == 2)
            {
                scheduleVesionTestToolStripMenuItem.Enabled = false;
                scheduleWritingTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = true;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                scheduleTestsToolStripMenuItem.Enabled = true;

                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
            }
            else
            {
                scheduleTestsToolStripMenuItem.Enabled = false;

                _checkIfPersonHaveAlreadyADrivingLicense();

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _applicationId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ApplicationID"].Value);
            int passedTestsOnApp = _getPassedTestsOnApplication(_applicationId);
            _applyContextMenuRules(passedTestsOnApp);
            _licenseClassName = dataGridView1.CurrentRow.Cells["ClassName"].Value.ToString();
            _license = clsLicense.find(_applicationId);

            string personNationNo = dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString();
            _person = clsPerson.find(personNationNo);
        }

        private void scheduleVesionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments frmVisionTestAppointments = new frmTestAppointments(dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString());
            frmVisionTestAppointments.currentTestTypeMode = frmTestAppointments.enTestTypeMode.visionTest;
            frmVisionTestAppointments.onFormClosed += _loadAllApplications;
            frmVisionTestAppointments.fillControls(_applicationId);
            frmVisionTestAppointments.ShowDialog();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicense frmAddNewLocalDrivingLicense = new frmAddNewLocalDrivingLicense();
            frmAddNewLocalDrivingLicense.onAddingNewLocalDrivingLicense += _loadAllApplications;
            frmAddNewLocalDrivingLicense.Show();
        }

        private void scheduleWritingTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments frmWrittenTestAppointments = new frmTestAppointments(dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString());
            frmWrittenTestAppointments.currentTestTypeMode = frmTestAppointments.enTestTypeMode.writtenTest;
            frmWrittenTestAppointments.onFormClosed += _loadAllApplications;
            frmWrittenTestAppointments.fillControls(_applicationId);
            frmWrittenTestAppointments.ShowDialog();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments frmStreetTestAppointments = new frmTestAppointments(dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString());
            frmStreetTestAppointments.currentTestTypeMode = frmTestAppointments.enTestTypeMode.streetTest;
            frmStreetTestAppointments.onFormClosed += _loadAllApplications;
            frmStreetTestAppointments.fillControls(_applicationId);
            frmStreetTestAppointments.ShowDialog();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.find(dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString());

            frmIssueDrivingLicense frmIssueDrivingLicense = new frmIssueDrivingLicense(_applicationId, person, 
                _licenseClassName);
            frmIssueDrivingLicense.onIssueingDrivingLicense += _loadAllApplications;
            frmIssueDrivingLicense.sendDataToControl();
            frmIssueDrivingLicense.ShowDialog();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.find(dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString());

            frmApplicationDetails frmApplicationDetails = new frmApplicationDetails();
            frmApplicationDetails.fillControlsWithData(_applicationId, person);
            frmApplicationDetails.ShowDialog();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirmation first
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this application?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (clsApplication.delete(_applicationId)) 
                    {
                        MessageBox.Show(
                            "Application deleted successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        _loadAllApplications();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to delete the application.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "An error occurred while deleting the application.\n\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirmation first
            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this application?",
                "Confirm Cancelation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (clsApplication.cancel(_applicationId))
                    {
                        MessageBox.Show(
                            "Application canceled successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        _loadAllApplications();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to cancel the application.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "An error occurred while canceling the application.\n\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string personNationNo = dataGridView1.CurrentRow.Cells["NationalNo"].Value.ToString();
            
            frmDriverLicenseInfo frmDriverLicenseInfo = new frmDriverLicenseInfo(_license, _person);
            frmDriverLicenseInfo.fillControlsWithData();
            frmDriverLicenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(_person);
            frmLicenseHistory.ShowDialog();
        }
    }
}
