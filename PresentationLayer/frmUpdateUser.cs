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
    public partial class frmUpdateUser : Form
    {
        private clsUser _user;
        private clsPerson _person;
        public event Action onUpdateUser;
        public frmUpdateUser(int userID)
        {
            InitializeComponent();
            _user = clsUser.find(userID);
            _person = clsPerson.find(_user.PersonID);
        }

        private void _fillControlsWithUserData()
        {
            lblUserID.Text = _user.UserID.ToString();
            tbUsername.Text = _user.UserName;
            tbPassword.Text = _user.Password;
            tbPasswordConfirmation.Text = _user.Password;
            cbIsActive.Checked = _user.IsActive;

            personDetailsControl1.fillControlsWithPersonData(_person);
        }

        private void frmUpdateUser_Load(object sender, EventArgs e)
        {
            _fillControlsWithUserData();
        }

        private void btnNextTab_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void _updateUserObject()
        {
            _user.UserName = tbUsername.Text;
            _user.Password = tbPassword.Text;
            _user.IsActive = cbIsActive.Checked ? true : false;
        }

        private bool _isInputsValidated()
        {
            
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                return false;
            }
            if (clsUser.isUsernameExists(tbUsername.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPasswordConfirmation.Text))
            {
                return false;
            }
            if (tbPassword.Text != tbPasswordConfirmation.Text)
            {
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _updateUserObject();

            if(_isInputsValidated())
            {
                if (_user.Save())
                {
                   MessageBox.Show("User updated successfully.", "Success",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                   _fillControlsWithUserData();
                    onUpdateUser?.Invoke();
                }
                else
                {
                   MessageBox.Show("Error updating user.", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error updating user, check your inputs validations.", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                errorProvider1.SetError(tbUsername, "This field is required.");
            }
            else
            {
                if (clsUser.isUsernameExists(tbUsername.Text) && tbUsername.Text != _user.UserName)
                {
                    errorProvider1.SetError(tbUsername, "Person with this National " +
                        "Number already exists!");
                }
                else
                {
                    errorProvider1.SetError(tbUsername, "");
                }
            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                errorProvider1.SetError(tbPassword, "This field is required.");
            }
            else
            {
                if (tbPasswordConfirmation.Text != tbPassword.Text)
                {
                    errorProvider1.SetError(tbPasswordConfirmation, "The password confirmation is not correct!");
                }
                else
                {
                    errorProvider1.SetError(tbPasswordConfirmation, "");
                }
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
        }
    }
}
