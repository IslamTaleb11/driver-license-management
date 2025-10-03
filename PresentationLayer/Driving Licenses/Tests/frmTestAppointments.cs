using BussinessLayer;
using DVLD.Custom_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Driving_Licenses.Tests
{
    public partial class frmTestAppointments : Form
    {
        private string _personNationalNo;
        clsLocalDrivingLicenseApplication _localApp;
        clsLicenseClass _licenseClass;
        clsApplication _application;
        public event Action onFormClosed;

        public enum enTestTypeMode
        {
            visionTest = 1,
            writtenTest = 2,
            streetTest = 3
        }

        public enTestTypeMode currentTestTypeMode;
        private clsTestType _testType;

        public frmTestAppointments(string personNationalNo)
        {
            InitializeComponent();

            //initialise person object to show the view person details form
            this._personNationalNo = personNationalNo;
            drivingLicenseApplicationInfoAndAppInfo1.person = clsPerson.find(personNationalNo);
        }

        private void _initializeTestType()
        {
            switch(currentTestTypeMode)
            {
                case enTestTypeMode.visionTest:
                    _testType = clsTestType.find("Vision Test");
                    break;
                case enTestTypeMode.writtenTest:
                    _testType = clsTestType.find("Written (Theory) Test");
                    break;
                case enTestTypeMode.streetTest:
                    _testType = clsTestType.find("Practical (Street) Test");
                    break;
            }
        }

        private void _loadDataGridViewWithData()
        {
            int localDrivingLicenseApplication = drivingLicenseApplicationInfoAndAppInfo1.localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            dataGridView1.DataSource = clsTestAppointment.getAllTestAppointments(this._personNationalNo, localDrivingLicenseApplication, _testType.ID);
            lblNumberOfRecords.Text = dataGridView1.Rows.Count.ToString();

            _initialiseLocalObjects();
        }

        public void fillControls(int applicationID)
        {
            drivingLicenseApplicationInfoAndAppInfo1.loadControlsWithData(applicationID);
        }

        private bool _isThereAppointments()
        {
            int localDrivingLicenseAppID = drivingLicenseApplicationInfoAndAppInfo1.localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            return clsTestAppointment.isThereAppointments(localDrivingLicenseAppID);
        }

        private void _initialiseLocalObjects()
        {
            _localApp = drivingLicenseApplicationInfoAndAppInfo1.localDrivingLicenseApplication;
            _licenseClass = drivingLicenseApplicationInfoAndAppInfo1.licenseClass;
            _application = drivingLicenseApplicationInfoAndAppInfo1.application;
        }

        private void _showFrmSchduleTestInAddNewMode()
        {

            decimal testTypeFees = _testType.Fees;

            frmScheduleTest frmScheduleTest = new frmScheduleTest(_localApp.LocalDrivingLicenseApplicationID,
                _licenseClass.ClassName,
                _application.ApplicantPersonFullName,
                drivingLicenseApplicationInfoAndAppInfo1.trailsPerTest,
                testTypeFees
            );

            initializeFrmScheduleTestTestType(frmScheduleTest);
            frmScheduleTest.onReservingAppointment += _loadDataGridViewWithData;
            frmScheduleTest.ShowDialog();
        }


        private bool _doesLocalDrivingLicenseAppHaveFailedTest(int localDrivingLicenseAppID, int testTypeID, 
            bool testResult)
        {

            return clsTest.doesLocalDrivingLicenseAppHaveFailedTest
                (localDrivingLicenseAppID, testTypeID, testResult);
        }

        private bool _doesLocalDrivingLicenseAppHaveSuccessTest(int localDrivingLicenseAppID, int testTypeID,
            bool testResult)
        {
            return clsTest.doesLocalDrivingLicenseAppHaveSuccessTest
                (localDrivingLicenseAppID, testTypeID, testResult);
        }

        private void initializeFrmScheduleTestTestType(frmScheduleTest frmScheduleTest)
        {
            switch(currentTestTypeMode)
            {
                case enTestTypeMode.visionTest:
                    frmScheduleTest.currentTestTypeMode = frmScheduleTest.enTestTypeMode.visionTest;
                    break;
                case enTestTypeMode.writtenTest:
                    frmScheduleTest.currentTestTypeMode = frmScheduleTest.enTestTypeMode.writtenTest;
                    break;
                case enTestTypeMode.streetTest:
                    frmScheduleTest.currentTestTypeMode = frmScheduleTest.enTestTypeMode.streetTest;
                    break;
            }
        }

        private void btnAddNewTestAppointment_Click(object sender, EventArgs e)
        {
            
                if (_isThereAppointments())
                {
                    MessageBox.Show(
                        "This application already has appointments. You cannot add a new one.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
                else
                {
                    
                    if (_doesLocalDrivingLicenseAppHaveSuccessTest(_localApp.LocalDrivingLicenseApplicationID,
                        _testType.ID, true))
                    {
                        MessageBox.Show(
                            "This person already passed this test, you can only retake failed tests.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    else if (_doesLocalDrivingLicenseAppHaveFailedTest(_localApp.LocalDrivingLicenseApplicationID, 
                        _testType.ID, false))
                    {
                        int countTrails = clsTest.countTrails(_localApp.LocalDrivingLicenseApplicationID, _testType.ID);
                        frmScheduleTest frmScheduleTest = new frmScheduleTest(_localApp.LocalDrivingLicenseApplicationID, 
                            _licenseClass.ClassName, _application.ApplicantPersonFullName, countTrails,
                            _application.PaidFees
                            );
                        initializeFrmScheduleTestTestType(frmScheduleTest);
                        frmScheduleTest.mode = frmScheduleTest.enMode.retakeTest;
                        frmScheduleTest.onReservingAppointment += _loadDataGridViewWithData;
                        frmScheduleTest.ShowDialog();
                    }
                    
                    else
                    {
                      _showFrmSchduleTestInAddNewMode();  
                    }
                
                }
        }

        private void _setDataGridViewContextMenuStrip()
        {
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void _defineFrmTitleBasedOnMode()
        {
            switch (currentTestTypeMode)
            {
                case enTestTypeMode.visionTest:
                    lblFrmTitle.Text = "Vision Test Appointments";
                    break;
                case enTestTypeMode.writtenTest:
                    lblFrmTitle.Text = "Written Test Appointments";
                    break;
                case enTestTypeMode.streetTest:
                    lblFrmTitle.Text = "Street Test Appointments";
                    break;
            }
        }

        private void _setFrmImageBasedOnTestType()
        {
            // My base path, you can change to yours.
            string basePath = @"C:\Users\MSI\Desktop\Development\c#\Projects\DVLD\Icons\";


            switch (currentTestTypeMode)
            {
                case enTestTypeMode.visionTest:
                    pbFrmImage.Image = Image.FromFile(basePath + "eye.png");
                    break;
                case enTestTypeMode.writtenTest:
                    pbFrmImage.Image = Image.FromFile(basePath + "writing.png");
                    break;
                case enTestTypeMode.streetTest:
                    pbFrmImage.Image = Image.FromFile(basePath + "car.png");
                    break;
            }
        }

        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            _initializeTestType();
            _loadDataGridViewWithData();
            _setDataGridViewContextMenuStrip();
            _defineFrmTitleBasedOnMode();
            _setFrmImageBasedOnTestType();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int selectedTestAppointmentID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TestAppointmentID"].Value);
            clsTestAppointment testAppointment = clsTestAppointment.find(selectedTestAppointmentID);
            decimal testTypeFees = _testType.Fees;
            _showFrmScheduleTestInUpdateMode(testAppointment, testTypeFees);
            
        }

        private void _showFrmScheduleTestInUpdateMode(clsTestAppointment testAppointment, decimal testTypeFees)
        {
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_localApp.LocalDrivingLicenseApplicationID,
                _licenseClass.ClassName,
                _application.ApplicantPersonFullName,
                drivingLicenseApplicationInfoAndAppInfo1.trailsPerTest,
                testTypeFees, testAppointment
            );
            frmScheduleTest.onReservingAppointment += _loadDataGridViewWithData;

            frmScheduleTest.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null) 
            {
                int testAppointmentID = Convert.ToInt32(
                    dataGridView1.CurrentRow.Cells["TestAppointmentID"].Value
                );

                DateTime date = Convert.ToDateTime(
                    dataGridView1.CurrentRow.Cells["AppointmentDate"].Value
                );

                decimal testTypeFees = _testType.Fees;

                int testTrails = clsTest.countTrails(_localApp.LocalDrivingLicenseApplicationID, _testType.ID);

                frmScheduledTest frmScheduledTest = new frmScheduledTest(
                    _localApp.LocalDrivingLicenseApplicationID,
                    _licenseClass.ClassName,
                    _application.ApplicantPersonFullName,
                    testTrails,
                    date,
                    testTypeFees,
                    testAppointmentID
                );

                // Subscribe to events (make sure these are declared as public events in frmScheduledTest)
                frmScheduledTest.onTakingTest += _loadDataGridViewWithData;
                frmScheduledTest.onTestFailed += fillControls;

                frmScheduledTest.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void _hasTestBeenCompleted()
        {
            int TestAppointmentID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TestAppointmentID"].Value);
            if (clsTestAppointment.hasTestBeenCompleted(_localApp.LocalDrivingLicenseApplicationID,
                TestAppointmentID))
            {
                contextMenuStrip1.Items["takeTestToolStripMenuItem"].Enabled = false;
            }
            else
            {
                contextMenuStrip1.Items["takeTestToolStripMenuItem"].Enabled = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _hasTestBeenCompleted();
        }

        private void frmVisionTestAppointments_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.onFormClosed?.Invoke();
        }
    }
}
