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
using System.Drawing;


namespace DVLD.Driving_Licenses.Tests
{
    public partial class frmScheduleTest : Form
    {
        public event Action onReservingAppointment;
        public enum enMode
        {
            addNewTest = 1,
            editTest = 2,
            retakeTest = 3
        }

        public enum enTestTypeMode
        {
            visionTest = 1,
            writtenTest = 2,
            streetTest = 3
        }

        public enTestTypeMode currentTestTypeMode;
        private clsTestType _testType;

        public enMode mode = enMode.addNewTest;
        private int _DLAppID { get; set; }
        private string _DClass { get; set; }
        private string _Name { get; set; }
        private int _Trail { get; set; }
        private decimal _Fees{ get; set; }

        private clsTestAppointment _testAppointment;
        private clsApplication _application;

        public frmScheduleTest(int DLAppID, string DClass, string Name, 
            int Trail, decimal Fees)
        {
            InitializeComponent();
            this._DLAppID = DLAppID;
            this._DClass = DClass;
            this._Name = Name;
            this._Trail = Trail;
            this._Fees = Fees;
        }

        public frmScheduleTest(int DLAppID, string DClass, string Name,
            int Trail, decimal Fees, clsTestAppointment testAppointment)
        {
            InitializeComponent();
            this._DLAppID = DLAppID;
            this._DClass = DClass;
            this._Name = Name;
            this._Trail = Trail;
            this._Fees = Fees;
            this._testAppointment = testAppointment;
        }

        private void _fillControlsWithData()
        {
            lblDLAID.Text = this._DLAppID.ToString();
            lblDClass.Text = this._DClass;
            lblName.Text = this._Name;
            lblTrail.Text = this._Trail.ToString();
            dtpDate.MinDate = DateTime.Now;
            lblFees.Text = Math.Truncate(this._Fees).ToString();
        }

        private void _setDtpDateValueInUpdateMode()
        {
            if (_testAppointment != null && _testAppointment.AppointmentDate >= DateTime.Now)
            {
                dtpDate.Value = _testAppointment.AppointmentDate;
            }
            else
            {
                dtpDate.Value = DateTime.Now;
            }
        }

        private bool _isTestAppointmentARetakeTest(int TestAppointmentID)
        {
            return clsTestAppointment.isTestAppointmentARetakeTest(_DLAppID, TestAppointmentID);
        }

        private void _setControlsIfTestCompleted()
        {
            if (mode != enMode.retakeTest)
            {
                if (_testAppointment == null)
                {
                    lblAppointmentLockedTitle.Visible = false;
                    dtpDate.Enabled = true;
                    btnSave.Enabled = true;
                    retakeTestInfo1.Enabled = false;
                }
                else if (clsTestAppointment.hasTestBeenCompleted(_DLAppID, _testAppointment.TestAppointmentID))
                {
                    lblAppointmentLockedTitle.Visible = true;
                    dtpDate.Enabled = false;
                    btnSave.Enabled = false;
                    retakeTestInfo1.Enabled = false;
                }

                else if (_isTestAppointmentARetakeTest(_testAppointment.TestAppointmentID))
                {
                    retakeTestInfo1.Enabled = true;
                }
            }

            

        }


        private void _enableAndSetControlsForRetakeTestMode()
        {
            switch (mode)
            {
                case enMode.retakeTest:
                    retakeTestInfo1.Enabled = true;
                    lblFormTitle.Text = "Schedule Retake Test";
                    btnSave.Enabled = true;
                    dtpDate.Enabled = true;
                    lblAppointmentLockedTitle.Visible = false;
                    clsApplicationType applicationType = clsApplicationType.find("Retake Test");
                    retakeTestInfo1.fillControlsWithData(applicationType.Fees, _application.ApplicationID);
                    break;
            }
        }

        private void _setApplicationObject()
        {
            _application = clsApplication.findByLocalDrivingLicenseAppID(_DLAppID);
        }

        private void initializeTestType()
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

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            initializeTestType();

            _fillControlsWithData();
            _setDtpDateValueInUpdateMode();
            _setApplicationObject();
            _setControlsIfTestCompleted();
            retakeTestInfo1.fillControlsWithData(this._Fees, _application.ApplicationID);
            _enableAndSetControlsForRetakeTestMode();
            _setFrmImageBasedOnTestType();
        }

        private void _setTestAppointmentObject()
        {
            _testAppointment = new clsTestAppointment();
            _testAppointment.TestTypeID = _testType.ID;
            _testAppointment.LocalDrivingLicenseApplicationID = _DLAppID;
            _testAppointment.AppointmentDate = dtpDate.Value;
            _testAppointment.PaidFees = this._Fees;
            _testAppointment.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;

        }

        private void _saveNormalTestAppointment()
        {
            if (_testAppointment != null)
            {
                dtpDate.TabIndex = 0;
                _testAppointment.AppointmentDate = dtpDate.Value;
            }
            else
            {
                _setTestAppointmentObject();
            }


            if (_testAppointment.save())
            {
                if (_testAppointment.mode == clsTestAppointment.enMode.addNew)
                {
                    MessageBox.Show(
                        "Test appointment created successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    onReservingAppointment?.Invoke();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "Test appointment updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    onReservingAppointment?.Invoke();
                    this.Close();
                }
            }
        }

        private bool _saveRetakeTestApplication()
        {
            clsApplication application = clsApplication.findByLocalDrivingLicenseAppID(this._DLAppID);
            clsApplicationType applicationType = clsApplicationType.find("Retake Test");

            clsApplication retakeTestApplication = new clsApplication();
            retakeTestApplication.ApplicantPersonID = application.ApplicantPersonID;
            retakeTestApplication.ApplicationDate = DateTime.Now;
            retakeTestApplication.ApplicationTypeID = applicationType.ID;
            retakeTestApplication.ApplicationStatus = 1;
            retakeTestApplication.LastStatusDate = DateTime.Now;
            retakeTestApplication.PaidFees = applicationType.Fees;
            retakeTestApplication.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;



            if (retakeTestApplication.save())
            {
                clsLocalDrivingLicenseApplication localDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.findByApplicationID(application.ApplicationID);

                localDrivingLicenseApplication.ApplicationID = retakeTestApplication.ApplicationID;
                if (!localDrivingLicenseApplication.save())
                {
                    return false;
                }
                //onTestFailed?.Invoke(retakeTestApplication.ApplicationID);
                return true;
            }
            return false;
        }

        private void _saveRetakeTestAppointment()
        {
            if (_saveRetakeTestApplication())
            {
                _setTestAppointmentObject();
                if (_testAppointment.save())
                {
                    MessageBox.Show(
                        "Retake test application saved successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    onReservingAppointment?.Invoke();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(
                    "Failed to save retake test application. Please try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            switch(mode)
            {
                case enMode.addNewTest:
                    _saveNormalTestAppointment();
                    break;
                case enMode.retakeTest:
                    _saveRetakeTestAppointment();
                    break;
            }

            
        }
    } 
}
