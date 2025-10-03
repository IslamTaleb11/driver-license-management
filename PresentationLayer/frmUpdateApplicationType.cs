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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD
{
    public partial class frmUpdateApplicationType : Form
    {
        private clsApplicationType _applicationType;
        public event Action onApplicationTypeUpdate;
        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _applicationType = clsApplicationType.find(ApplicationTypeID);
        }

        private void _fillControlsWithData()
        {
            lblID.Text = _applicationType.ID.ToString();
            tbTitle.Text = _applicationType.Title;
            tbFees.Text = _applicationType.Fees.ToString();
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _fillControlsWithData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _validateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
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
            _applicationType.Title = tbTitle.Text;
            _applicationType.Fees = Convert.ToDecimal(tbFees.Text);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_validateInputs())
            {
                _setObjectData();
                if (_applicationType.save())
                {
                    onApplicationTypeUpdate?.Invoke();
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
