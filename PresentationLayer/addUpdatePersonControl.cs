using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD
{
    public partial class addUpdatePersonControl : UserControl
    {
        private clsPerson _person;
        static public event Action onPersonSaved;
        static public event Action onClickingCancel;
        public delegate void PersonObjectBackToFormEventHandler(clsPerson person);
        public event PersonObjectBackToFormEventHandler personObjectBack;
        private string _imagePath;

        private enum _enCurrentMode
        {
            addNew = 0,
            update = 1
        }
        private _enCurrentMode _currentMode; 

        public addUpdatePersonControl()
        {
            InitializeComponent();
            clsPerson.mode = clsPerson.enMode.addNew;
        }

        //public addUpdatePersonControl(int personID = -1)
        //{
        //    InitializeComponent();
        //    if (personID != -1)
        //    {
        //        _createPersonObject(personID);
        //        _fillControlersWithPersonData(_person);
        //    }
        //}
        public void createPersonObject(int personID)
        {
            _person = clsPerson.find(personID);
            _fillControlersWithPersonData(_person);
        }
        private void _fillControlersWithPersonData(clsPerson person)
        {
            lblPersonID.Text = person.ID.ToString();
            tbFirstName.Text = person.FirstName;
            tbSecondName.Text = person.SecondName;
            tbThirdName.Text = person.ThirdName;
            tbLastName.Text = person.LastName;
            tbNationalNo.Text = person.NationalNo;
            dtpDateOfBirth.Value = person.DateOfBirth;
            if (person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
            mtbPhone.Text = person.Phone;
            tbEmail.Text = person.Email;
            cbCountry.SelectedItem = clsCountry.FindCountry(person.NationalCountryID).CountryName;
            rtbAddress.Text = person.Address;

            pbImage.ImageLocation = !string.IsNullOrWhiteSpace(person.ImagePath) ?
                person.ImagePath : null;
        }

        private bool _validateControls()
        {
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbSecondName.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbThirdName.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbNationalNo.Text))
            {
                return false;
            }
            if (!dtpDateOfBirth.Checked)
            {
                return false;
            }
            if (!rbMale.Checked && !rbFemale.Checked)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(mtbPhone.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cbCountry.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(rtbAddress.Text))
            {
                return false;
            }
            return true;
        }

        private void _addNewPerson()
        {
            _currentMode = _enCurrentMode.addNew;
             int gender = rbMale.Checked ? 0 : 1;
             clsCountry country = clsCountry.FindCountry(cbCountry.SelectedItem.ToString());
             _person = new clsPerson();
            _person.FirstName = tbFirstName.Text;
            _person.SecondName = tbSecondName.Text;
            _person.ThirdName = tbThirdName.Text;
            _person.LastName = tbLastName.Text;
            _person.NationalNo = tbNationalNo.Text;
            _person.DateOfBirth = dtpDateOfBirth.Value;
            _person.Gender = gender;
            _person.Phone = mtbPhone.Text;
            _person.Email = tbEmail.Text;
            _person.NationalCountryID = country.CountryID;
            _person.Address = rtbAddress.Text;
            _person.ImagePath = _imagePath;
        }

        private void _updatePerson()
        {
            _currentMode = _enCurrentMode.update;
            int gender = rbMale.Checked ? 0 : 1;
            clsCountry country = clsCountry.FindCountry(cbCountry.SelectedItem.ToString());
            _person.FirstName = tbFirstName.Text;
            _person.SecondName = tbSecondName.Text;
            _person.ThirdName = tbThirdName.Text;
            _person.LastName = tbLastName.Text;
            _person.NationalNo = tbNationalNo.Text;
            _person.DateOfBirth = dtpDateOfBirth.Value;
            _person.Gender = gender;
            _person.Phone = mtbPhone.Text;
            _person.Email = tbEmail.Text;
            _person.NationalCountryID = country.CountryID;
            _person.Address = rtbAddress.Text;
            _person.ImagePath = _imagePath; 
        }

        private void _showMessageBox()
        {
            if (_currentMode == _enCurrentMode.addNew)
            {
                MessageBox.Show("New person added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Person updated successfully!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_validateControls())
            {
                if (_person == null)
                {
                    _addNewPerson();
                }
                else
                {
                    _updatePerson();
                }

                if (_person.Save())
                {
                    
                     personObjectBack?.Invoke(_person);
                    
                    _showMessageBox();
                    _fillControlersWithPersonData(_person);
                    onPersonSaved?.Invoke();
                }
            }
            else
            {
                MessageBox.Show("Some Fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                errorProvider1.SetError(tbFirstName, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbFirstName, "");
            }
        }

        private void tbSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSecondName.Text))
            {
                errorProvider1.SetError(tbSecondName, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbSecondName, "");
            }
        }

        private void tbThirdName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbThirdName.Text))
            {
                errorProvider1.SetError(tbThirdName, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbThirdName, "");
            }
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                errorProvider1.SetError(tbLastName, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(tbLastName, "");
            }
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNationalNo.Text))
            {
                errorProvider1.SetError(tbNationalNo, "This field is required.");
            }
            else
            {
                if (clsPerson.IsPersonExists(tbNationalNo.Text))
                {
                    errorProvider1.SetError(tbNationalNo, "Person with this National " +
                        "Number already exists!");
                }
                else
                {
                    errorProvider1.SetError(tbNationalNo, "");
                }
                
            }
        }

        private void _setMaxLicenseHoldingBirthDate()
        {
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
        }

        private void _setCountriesCbWithCountries()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
            cbCountry.SelectedItem = "Algeria";
        }

        private void panel1_Enter(object sender, EventArgs e)
        {
            _setMaxLicenseHoldingBirthDate();
            _setCountriesCbWithCountries();
        }

        private bool _IsValidEmail(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return address.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void txEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                errorProvider1.SetError(tbEmail, "This field is required.");
            }
            else
            {
                if (!_IsValidEmail(tbEmail.Text))
                {
                    errorProvider1.SetError(tbEmail, "Email Not Valid!");
                }
                else
                {
                    errorProvider1.SetError(tbEmail, "");
                }

            }
        }

        private void mtbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mtbPhone.Text))
            {
                errorProvider1.SetError(mtbPhone, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(mtbPhone, "");
            }
        }

        private void rtbAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbAddress.Text))
            {
                errorProvider1.SetError(rtbAddress, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(rtbAddress, "");
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Image";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbImage.Image = Image.FromFile(openFileDialog.FileName);
                _imagePath = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            onClickingCancel?.Invoke();
        }
    }
}
