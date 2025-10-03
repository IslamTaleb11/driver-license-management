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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        private void _loadAllUsers()
        {
            DataTable dt = clsUser.getAllUsers();

            _addFullNameColumnToGrid(dt);


            dataGridView1.DataSource = dt;
            _loadNumberOfRecordsToLbl();
        }

        private void _addFullNameColumnToGrid(DataTable dt)
        {
            dt.Columns.Add("FullName", typeof(string));


            foreach (DataRow dr in dt.Rows)
            {
                int personId = Convert.ToInt32(dr["PersonID"]);
                clsPerson person = clsPerson.find(personId);
                dr["FullName"] = person.FirstName + " " + person.SecondName + " " +
                    person.ThirdName + " " + person.LastName;
            }
            
        }

        private void _loadNumberOfRecordsToLbl()
        {
            lblNumberOfRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _loadAllUsers();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["FullName"].DisplayIndex = 2;
            cbFilterBy.SelectedIndex = 0;
        }

        private void _loadUsersInDgv()
        {
            _loadAllUsers();
            dataGridView1.Columns["FullName"].DisplayIndex = 2;
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _openAddNewUserFrm();
        }



        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int selectedUserID = Convert.ToInt32(selectedRow.Cells["UserID"].Value);

                Form frmUserDetails = new frmUserDetails(selectedUserID);

                frmUserDetails.Show();
            }
        }

        private void _openAddNewUserFrm()
        {
            frmAddNewUser addNewUserFrm = new frmAddNewUser();
            addNewUserFrm.onAddUser += _loadUsersInDgv;


            addNewUserFrm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _openAddNewUserFrm();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    int selectedUserID = Convert.ToInt32(selectedRow.Cells["UserID"].Value);

                    clsUser user = clsUser.find(selectedUserID);

                    if (true) // here we have to check if the user is not connected to a data in the system.
                    {
                        if (user.Delete())
                        {
                            _loadUsersInDgv();
                            MessageBox.Show("User deleted successfully.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error deleting user.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int selectedUserID = Convert.ToInt32(selectedRow.Cells["UserID"].Value);
                frmUpdateUser frmUpdateUser = new frmUpdateUser(selectedUserID);
                frmUpdateUser.onUpdateUser += _loadUsersInDgv;
                frmUpdateUser.ShowDialog();
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                tbFilterBy.Visible = false;
                cbIsActiveOptions.Visible = false;
                mtbFilterBy.Visible = false;
            }
            else if(cbFilterBy.SelectedItem.ToString() == "IsActive")
            {
                cbIsActiveOptions.SelectedIndex = 0;
                cbIsActiveOptions.Visible = true;
                tbFilterBy.Visible = false;
                mtbFilterBy.Visible = false;
            }
            else if (cbFilterBy.SelectedItem.ToString() == "PersonID" ||
                cbFilterBy.SelectedItem.ToString() == "UserID")
            {
                cbIsActiveOptions.Visible = false;
                tbFilterBy.Visible = false;
                mtbFilterBy.Visible = true;
                mtbFilterBy.PromptChar = ' '; // blank
            }
            else
            {
                tbFilterBy.Visible = true;
                cbIsActiveOptions.Visible = false;
                mtbFilterBy.Visible = false;
            }
        }

        private void _handleFullNameColumn()
        {
            if (dataGridView1.Rows.Count > 0)
            {
              dataGridView1.Columns["FullName"].DisplayIndex = 2;  
            }
            else
            {
                dataGridView1.Columns.Remove("FullName");
            }
        }

        private void _loadUsersByID()
        {
            DataTable dt = clsUser.getUsersFilterByUserID
                (Convert.ToInt32(mtbFilterBy.Text));
            _addFullNameColumnToGrid(dt);

            dataGridView1.DataSource = dt;
            _handleFullNameColumn();
        }

        private void _loadUsersByUsername()
        {
            DataTable dt = clsUser.getUsersFilterByUsername
                (tbFilterBy.Text);
            _addFullNameColumnToGrid(dt);

            dataGridView1.DataSource = dt;
            _handleFullNameColumn();
        }


        private void _loadUsersByFullName()
        {
            DataTable dt = clsUser.getUsersFilterByFullName
                (tbFilterBy.Text);
            _addFullNameColumnToGrid(dt);

            dataGridView1.DataSource = dt;
            _handleFullNameColumn();
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFilterBy.Text))
            {
                _loadAllUsers();
            }
            else
            {
                switch (cbFilterBy.SelectedItem.ToString())
                {
                    case "UserName":
                        _loadUsersByUsername();
                        break;
                    case "FullName":
                        _loadUsersByFullName();
                        break;
                }
            }
            _loadNumberOfRecordsToLbl();
        }

        private void mtbFilterBy_Click(object sender, EventArgs e)
        {
            mtbFilterBy.Select(0, 0);
        }

        private void mtbFilterBy_Enter(object sender, EventArgs e)
        {
            mtbFilterBy.Select(0, 0);
        }


        private void _loadUsersByPersonID()
        {
            DataTable dt = clsUser.getUsersFilterByPersonID
                (Convert.ToInt32(mtbFilterBy.Text));
            _addFullNameColumnToGrid(dt);

            dataGridView1.DataSource = dt;
            _handleFullNameColumn();
        }

        private void mtbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mtbFilterBy.Text))
            {
                _loadAllUsers();
            }
            else
            {
                switch (cbFilterBy.SelectedItem.ToString())
                {
                    case "UserID":
                        _loadUsersByID();
                        break;
                    case "PersonID":
                        _loadUsersByPersonID();
                        break;
                }
            }
            _loadNumberOfRecordsToLbl();
        }

        private void _loadOnlyActiveUsers()
        {
            DataTable dt = clsUser.getOnlyActiveUsers();
            _addFullNameColumnToGrid(dt);
            dataGridView1.DataSource = dt;
            _loadNumberOfRecordsToLbl();
            _handleFullNameColumn();
        }

        private void _loadOnlyNoneActiveUsers()
        {
            DataTable dt = clsUser.getOnlyNoneActiveUsers();
            _addFullNameColumnToGrid(dt);
            dataGridView1.DataSource = dt;
            _loadNumberOfRecordsToLbl();
            _handleFullNameColumn();
        }

        private void cbIsActiveOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActiveOptions.SelectedItem.ToString())
            {
                case "All":
                    _loadAllUsers();
                    break;
                case "Yes":
                    _loadOnlyActiveUsers();
                    break;
                case "No":
                    _loadOnlyNoneActiveUsers();
                    break;
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int selectedUserID = Convert.ToInt32(selectedRow.Cells["UserID"].Value);
                frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword(selectedUserID);
                frmChangeUserPassword.ShowDialog();
            }
        }
    }
}
