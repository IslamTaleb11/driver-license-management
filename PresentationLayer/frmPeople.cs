using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;

namespace DVLD
{
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();
            addUpdatePersonControl.onPersonSaved += _loadDataInDataGridView;
        }

        private DataTable _loadPeople()
        {
            return clsPerson.GetPeople();
        }

        private void _loadDataInDataGridView()
        {
            dataGridView1.DataSource = _loadPeople();
            lblNumberOfRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            _loadDataInDataGridView();
            cbFilterBy.SelectedItem = "None";
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            Form addEditPersonFrm = new addUpdatePerson();
            clsPerson.mode = clsPerson.enMode.addNew;
            addEditPersonFrm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int selectedPersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);

                Form personDetailsFrm = new PersonDetails(selectedPersonID);

                personDetailsFrm.Show();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int selectedPersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);

                Form addEditPersonForm = new addUpdatePerson(selectedPersonID);
                
                addEditPersonForm.ShowDialog();
            }
            
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem != "None")
            {
                tbFilterBy.Visible = true;
            }
            else
            {
                tbFilterBy.Visible = false;
            }
        }

        private void _getPeopleFilterByPersonID()
        {
            if (int.TryParse(tbFilterBy.Text, out _))
            {
                dataGridView1.DataSource = clsPerson.GetPeopleFilterByPersonID(Convert.ToInt32(tbFilterBy.Text));
                _setRecordsCount();
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void _getPeopleFilterByNationalNo()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByNationalNo(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterByFirstName()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByFirstName(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterBySecondName()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterBySecondName(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterByThirdName()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByThirdName(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterByLastName()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByLastName(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterByNationality()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByNationality(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterByGender()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByGender(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterByEmail()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByEmail(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _getPeopleFilterByPhone()
        {
            dataGridView1.DataSource = clsPerson.GetPeopleFilterByPhone(tbFilterBy.Text);
            _setRecordsCount();
        }

        private void _setRecordsCount()
        {
            lblNumberOfRecords.Text = dataGridView1.RowCount.ToString();
        }
        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFilterBy.Text))
            {
                _loadDataInDataGridView();
            }
            else
            {
                switch (cbFilterBy.SelectedItem.ToString())
                {
                    case "PersonID":
                        _getPeopleFilterByPersonID();
                        break;
                    case "NationalNo":
                        _getPeopleFilterByNationalNo();
                        break;
                    case "FirstName":
                        _getPeopleFilterByFirstName();
                        break;
                    case "SecondName":
                        _getPeopleFilterBySecondName();
                        break;
                    case "ThirdName":
                        _getPeopleFilterByThirdName();
                        break;
                    case "LastName":
                        _getPeopleFilterByLastName();
                        break;
                    case "Nationality":
                        _getPeopleFilterByNationality();
                        break;
                    case "Gender":
                        _getPeopleFilterByGender();
                        break;
                    case "Email":
                        _getPeopleFilterByEmail();
                        break;
                    case "Phone":
                        _getPeopleFilterByPhone();
                        break;
                }
            }
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this person?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    int selectedPersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);
                    if (clsPerson.deletePerson(selectedPersonID))
                    {
                        _loadDataInDataGridView();
                        MessageBox.Show("Person deleted successfully.", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the person. Please try again.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
            
        }
    }
}
