using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLoginScreen : Form
    {
        private const string _folderPath = "LoginFiles";
        private const string _fileName = "RememberMeCredentials.txt";
        private clsUser _user;
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private bool _validateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                return false;
            }

            return true;
        }

        private bool _checkCredentials()
        {
            if (clsUser.isUsernameExists(tbUsername.Text))
            {
                _user = clsUser.find(tbUsername.Text);
                if (tbPassword.Text == _user.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void _handleRememberMeFile()
        {
            string fullPath = Path.Combine(_folderPath, _fileName);
            if (cbRememberMe.Checked)
            {
                File.WriteAllText(fullPath, tbUsername.Text); // Overwrites file
                File.AppendAllText(fullPath, "\n" + tbPassword.Text); // Adds without deleting first
            }
            else
            {
                File.Delete(fullPath);
            }
        }

        private void _cacheLoggedInUser()
        {
            clsGlobalSettings.currentLoggedInUser = _user;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_validateInputs())
            {
                if (_checkCredentials())
                {
                    if (_user.IsActive)
                    {
                        _handleRememberMeFile();
                        _cacheLoggedInUser();
                        MainForm mainForm = new MainForm();
                        this.Hide();
                        mainForm.Show(); 
                    }
                    else
                    {
                        MessageBox.Show(
                        "This User is Unactive for the moment, please contact the admin.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Invalid username or password.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Please fill in all required fields correctly.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

        }

        private void _loadInputsWithData(string fullPath)
        {
            string[] lines = File.ReadAllLines(fullPath);

            tbUsername.Text = lines[0];
            tbPassword.Text = lines[1];
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string folderPath = "LoginFiles";
            string fileName = "RememberMeCredentials.txt";
            string fullPath = Path.Combine(folderPath, fileName);

            if (File.Exists(fullPath))
            {
                _loadInputsWithData(fullPath); 
            }
            
        }
    }
}
