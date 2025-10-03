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
    public partial class frmUserDetails : Form
    {
        private clsUser _user;
        private clsPerson _person;
        public frmUserDetails(int userID)
        {
            InitializeComponent();
            this._user = clsUser.find(userID);
            this._person = clsPerson.find(this._user.PersonID);
        }

        public frmUserDetails(clsUser user)
        {
            InitializeComponent();
            this._user = user;
            this._person = clsPerson.find(this._user.PersonID);
        }

        private void _fillMainControlsWithUserData()
        {
            personDetailsControl1.fillControlsWithPersonData(_person);
            userDetails1.fillControlsWithPersonData(_user);
        }

        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            _fillMainControlsWithUserData();
        }
    }
}
