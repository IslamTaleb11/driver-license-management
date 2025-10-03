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

namespace DVLD.Custom_Controls
{
    public partial class FindPersonControl : UserControl
    {
        public clsPerson person;
        public delegate void PersonDataBackEventHandler(clsPerson person);
        public event PersonDataBackEventHandler onPersonSelected;
        public enum enAddingMode
        {
            AddNewUser = 0,
            AddNewDrivingLicense = 1
        }

        public enAddingMode addingMode;

       
        public FindPersonControl()
        {
            InitializeComponent();
        }

        public void disableSearchOrAddUser()
        {
           searchOrAddPanel.Enabled = false;   
        }

        public void addDataToSearchUserControl()
        {
            tbSelectUserFilter.Text = person.ID.ToString();
            cbAddNewUserFilterBy.SelectedItem = "PersonID";
        }

        public void fillControlsWithData()
        {
            personDetailsControl1.fillControlsWithPersonData(person);
        }

        private void FindPersonControl_Load(object sender, EventArgs e)
        {
            cbAddNewUserFilterBy.SelectedItem = "National No";

        }

        private void _getReturnedObjectFromAddNewPersonFrm(clsPerson Person)
        {
            person = Person;
            onPersonSelected?.Invoke(person);
            personDetailsControl1.fillControlsWithPersonData(person);
        }

        private void btnAddNewPerson2_Click(object sender, EventArgs e)
        {
            addUpdatePerson addUpdateUserFrm = new addUpdatePerson();
            addUpdateUserFrm.personObjectBack += _getReturnedObjectFromAddNewPersonFrm;
            addUpdateUserFrm.ShowDialog();
        }

        private void _addNewUser()
        {
            if (!clsPerson.isPersonLinkedToUser(tbSelectUserFilter.Text))
            {
                person = clsPerson.find(tbSelectUserFilter.Text);
                personDetailsControl1.fillControlsWithPersonData(person);
                onPersonSelected?.Invoke(person);
            }
            else
            {
                MessageBox.Show("This person is already linked to a user.", "Duplication", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _addNewLocalDrivingLicense()
        {
            person = clsPerson.find(tbSelectUserFilter.Text);
            personDetailsControl1.fillControlsWithPersonData(person);
            onPersonSelected?.Invoke(person);
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbSelectUserFilter.Text))
            {
                if (clsPerson.IsPersonExists(tbSelectUserFilter.Text))
                {
                    switch(addingMode)
                    {
                        case enAddingMode.AddNewUser:
                            _addNewUser();
                            break;
                        case enAddingMode.AddNewDrivingLicense:
                            _addNewLocalDrivingLicense();
                            break;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Person with this NationalNo does not exists!.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
    }
}
