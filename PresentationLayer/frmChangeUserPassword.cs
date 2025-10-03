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
    public partial class frmChangeUserPassword : Form
    {
        private clsUser _user;
        private clsPerson _person;
        public frmChangeUserPassword(int userID)
        {
            InitializeComponent();
            _user = clsUser.find(userID);
            _person = clsPerson.find(_user.PersonID);
        }

        public frmChangeUserPassword(clsUser user)
        {
            InitializeComponent();
            _user = user;
            _person = clsPerson.find(_user.PersonID);
        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbCurrentPassword.Text))
            {
                errorProvider1.SetError(tbCurrentPassword, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbCurrentPassword, "");
            }
            if (tbCurrentPassword.Text != _user.Password)
            {
                errorProvider1.SetError(tbCurrentPassword, "The current password you entered is incorrect.");
            }
        }

        

        private void tbPasswordConfirmation_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPasswordConfirmation.Text))
            {
                errorProvider1.SetError(tbPasswordConfirmation, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbPasswordConfirmation, "");
            }

            if (tbNewPassword.Text != tbPasswordConfirmation.Text)
            {
                errorProvider1.SetError(tbPasswordConfirmation, "The new password and confirmation do not match.");
            }
        }

        private void tbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNewPassword.Text))
            {
                errorProvider1.SetError(tbNewPassword, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbNewPassword, "");
            }
        }

        private void personDetailsControl1_Load(object sender, EventArgs e)
        {
            userDetails1.fillControlsWithPersonData(_user);

            personDetailsControl1.fillControlsWithPersonData(_person);
        }

        private bool _validateInputs()
        {
            if (tbCurrentPassword.Text != _user.Password)
            {
                return false;
            }
            if (tbNewPassword.Text != tbPasswordConfirmation.Text)
            {
                return false;
            }
            return true;
        }

        private void _updateUserObject()
        {
            _user.Password = tbNewPassword.Text;
        }

        private void _clearInputs()
        {
            tbCurrentPassword.Text = "";
            tbNewPassword.Text = "";
            tbPasswordConfirmation.Text = "";
            errorProvider1.SetError(tbPasswordConfirmation, "");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (_validateInputs())
            {
                _updateUserObject();
                if (_user.Save())
                {
                    MessageBox.Show(
                        "Password updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    _clearInputs();
                }
                else
                {
                    MessageBox.Show(
                        "An error occurred while saving the new password. " +
                        "Please try again.",
                        "Save Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Please correct the highlighted errors before saving.",
                    "Validation Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
