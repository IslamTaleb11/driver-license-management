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

namespace DVLD.Driving_Licenses.Tests
{
    public partial class frmScheduledTest : Form
    {

        public event Action onTakingTest;
        public event Action<int> onTestFailed;

        private int _DLAppID { get; set; }
        private string _DClass { get; set; }
        private string _Name { get; set; }
        private int _Trail { get; set; }
        private DateTime _Date { get; set; }
        private decimal _Fees { get; set; }
        private int _TestAppointmentID { get; set; }
        private clsTest _test { get; set; }


        public frmScheduledTest(int DLAppID, string DClass, string Name,
            int Trail, DateTime Date, decimal Fees, int TestAppointmentID)
        {
            InitializeComponent();
            this._DLAppID = DLAppID;
            this._DClass = DClass;
            this._Name = Name;
            this._Trail = Trail;
            this._Date = Date;
            this._Fees = Fees;
            this._TestAppointmentID = TestAppointmentID;
        }

        private void _fillControlsWithData()
        {
            lblDLAID.Text = this._DLAppID.ToString();
            lblDClass.Text = this._DClass;
            lblName.Text = this._Name;
            lblTrail.Text = this._Trail.ToString();
            lblDate.Text = this._Date.ToShortDateString();
            lblFees.Text = Math.Truncate( this._Fees).ToString();
        }

        

        private void frmScheduledTest_Load(object sender, EventArgs e)
        {
            _fillControlsWithData();
        }

        private void _prepareTestObject()
        {
            _test = new clsTest();
            _test.TestAppointmentID = _TestAppointmentID;
            _test.TestResult = rbPass.Checked ? true : false;
            _test.Notes = rtbNotes.Text;
            _test.CreatedByUserID = clsGlobalSettings.currentLoggedInUser.UserID;
        }

        private void _lockTestAppointment()
        {
            clsTestAppointment.lockTestAppointment(_TestAppointmentID);
        }

        

        private void _handleSaveSuccess()
        {
            MessageBox.Show(
                               "Test saved successfully!",
                               "Success",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information
                           );

            _lockTestAppointment();
            onTakingTest?.Invoke();
            this.Close();
        }

        private void _handleSaveFailed()
        {
            MessageBox.Show(
                            "Failed to save the test. Please try again.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
            );
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _prepareTestObject();

            // Ask for confirmation before saving
            DialogResult result = MessageBox.Show(
                "Are you sure you want to save this test?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (_test.save())
                {
                    _handleSaveSuccess();
                }
                else
                {
                    _handleSaveFailed();
                }
            }

        }
    }
}
