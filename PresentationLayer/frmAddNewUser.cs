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

namespace DVLD
{
    public partial class frmAddNewUser : Form
    {
        public event Action onAddUser;
        private clsUser _user;
        public frmAddNewUser()
        {
            InitializeComponent();
        }


        private void btnNextTab_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                errorProvider1.SetError(tbUsername, "This field is required.");
            }
            else
            {
                if (clsUser.isUsernameExists(tbUsername.Text))
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

        private void tbPasswordConfirmation_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPasswordConfirmation.Text))
            {
                errorProvider1.SetError(tbPasswordConfirmation, "This field is required.");
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

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                errorProvider1.SetError(tbPassword, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbPassword, "");
            }
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

        private void _addNewUser()
        {
            _user = new clsUser();
            _user.UserName = tbUsername.Text;
            _user.Password = tbPassword.Text;
            _user.IsActive = cbIsActive.Checked ? true : false;
            _user.PersonID = findPersonControl1.person.ID;
        }

        private void updateUserIDLbl(int userID)
        {
            lblUserID.Text = userID.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isInputsValidated())
            {
                if (findPersonControl1.person != null)
                {
                    _addNewUser();
                    if (_user.Save())
                    {
                        updateUserIDLbl(_user.UserID);
                        onAddUser?.Invoke();
                        MessageBox.Show("User Added Successfully!.","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("You Must link a person to user!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error Adding user, check your inputs validations.", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            findPersonControl1.addingMode = FindPersonControl.enAddingMode.AddNewUser;
        }
    }
}
