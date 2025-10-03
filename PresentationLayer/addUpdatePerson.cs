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
    public partial class addUpdatePerson : Form
    {
        public delegate void PersonObjectBackToAddNewUserFormEventHandler(clsPerson person);
        public event PersonObjectBackToAddNewUserFormEventHandler personObjectBack;
        public addUpdatePerson()
        {
            InitializeComponent();
            addUpdatePersonControl1.personObjectBack += _sendPersonObjectToAddUserFrm;
            addUpdatePersonControl.onPersonSaved += _changeMainLabel;
            addUpdatePersonControl.onClickingCancel += _closeDialog;
        }
        public addUpdatePerson(int personID)
        {
            InitializeComponent();
            addUpdatePersonControl1.personObjectBack += _sendPersonObjectToAddUserFrm;
            addUpdatePersonControl1.createPersonObject(personID);
            addUpdatePersonControl.onClickingCancel += _closeDialog;
        }

        private void _closeDialog()
        {
            this.Close();
        }

        private void _sendPersonObjectToAddUserFrm(clsPerson person)
        {
            personObjectBack?.Invoke(person);
        }
        private void _changeMainLabel()
        {
            if (clsPerson.mode == clsPerson.enMode.addNew)
            {
                lblAddUpdatePerson.Text = "Add New Person";
            }
            else
            {
                lblAddUpdatePerson.Text = "Update Person";
            }
        }
        private void addUpdatePerson_Load(object sender, EventArgs e)
        {
            if (clsPerson.mode == clsPerson.enMode.addNew)
            {
                lblAddUpdatePerson.Text = "Add New Person";
            }
            else
            {
                lblAddUpdatePerson.Text = "Update Person";
            }
        }

    }
}
