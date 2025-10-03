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

namespace DVLD
{
    public partial class frmUpdateTestType : Form
    {
        private clsTestType _testType;
        public event Action onTestTypeUpdate;
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            _testType = clsTestType.find(TestTypeID);
        }

        private void _fillControlsWithData()
        {
            lblID.Text = _testType.ID.ToString();
            tbTitle.Text = _testType.Title;
            rtbDecription.Text = _testType.Description;
            tbFees.Text = _testType.Fees.ToString();
        }

        

        

        private bool _validateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(rtbDecription.Text))
            {
                return false;
            }
            if (!decimal.TryParse(tbFees.Text, out decimal value))
            {
                return false;
            }
            return true;
        }

        private void _setObjectData()
        {
            _testType.Title = tbTitle.Text;
            _testType.Description = rtbDecription.Text;
            _testType.Fees = Convert.ToDecimal(tbFees.Text);
        }

        
        

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, control keys (like backspace), and one decimal point
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && (e.KeyChar != '.'))
            {
                e.Handled = true; // Stop the character from being entered
            }


        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _fillControlsWithData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_validateInputs())
            {
                _setObjectData();
                if (_testType.save())
                {
                    onTestTypeUpdate?.Invoke();
                    MessageBox.Show("Application type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update application type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all required fields correctly.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
