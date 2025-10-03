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
    public partial class PersonDetailsControl : UserControl
    {
        public PersonDetailsControl()
        {
            InitializeComponent();

        }

        private string _getPersonFullName(clsPerson person)
        {
            return person.FirstName + " " + person.SecondName + " " + person.ThirdName + " "
                + person.LastName;
        }

        public void fillControlsWithPersonData(clsPerson person)
        {
            lblPersonID.Text = person.ID.ToString();
            lblFullName.Text = _getPersonFullName(person);
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = person.Email;
            lblAddress.Text = person.Address;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblPhone.Text = person.Phone;
            lblCountry.Text = clsCountry.FindCountry(person.NationalCountryID).CountryName;
            if (!string.IsNullOrWhiteSpace(person.ImagePath))
            {
                if (File.Exists(person.ImagePath))
                {
                    pbImage.Image = Image.FromFile(person.ImagePath);
                } 
            }
        }


        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (lblPersonID.Text != "???")
            {
                addUpdatePerson addUpdatePersonFrm = new addUpdatePerson(Convert.ToInt32(lblPersonID.Text));
                addUpdatePersonFrm.personObjectBack += fillControlsWithPersonData;
                addUpdatePersonFrm.ShowDialog();
            }
        }
    }
}
