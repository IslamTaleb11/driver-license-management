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
    public partial class PersonDetails : Form
    {
        private clsPerson _person;
        public PersonDetails(int PersonID)
        {
            InitializeComponent();
            _person = clsPerson.find(PersonID);
            personDetailsControl1.fillControlsWithPersonData(_person);
        }

        public PersonDetails(clsPerson person)
        {
            InitializeComponent();
            _person = person;
            personDetailsControl1.fillControlsWithPersonData(_person);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
