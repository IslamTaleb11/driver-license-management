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

namespace DVLD.Drivers
{
    public partial class frmManageDrivers : Form
    {
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private void _loadAllDrivers()
        {
            dataGridView1.DataSource = clsDriver.getAllDrivers();
            _countNumberOfRecords();
        }

        private void _countNumberOfRecords()
        {
            lblNumberOfRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void _setCbFilterByToDefault()
        {
            cbFilterBy.SelectedItem = "None";
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _loadAllDrivers();
            _setCbFilterByToDefault();
        }

        private void _loadDriversByDriverID()
        {
            dataGridView1.DataSource = clsDriver.getDriversByDriverID(Convert.ToInt32(tbFilterBy.Text));
            _countNumberOfRecords();
        }

        private void _loadDriversByPersonID()
        {
            dataGridView1.DataSource = clsDriver.getDriversByPersonID(Convert.ToInt32(tbFilterBy.Text));
            _countNumberOfRecords();
        }

        private void _loadDriversByNationalNo()
        {
            dataGridView1.DataSource = clsDriver.getDriversByNationalNo(tbFilterBy.Text);
            _countNumberOfRecords();
        }

        private void _loadDriversByFullName()
        {
            dataGridView1.DataSource = clsDriver.getDriversByFullName(tbFilterBy.Text);
            _countNumberOfRecords();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch(cbFilterBy.SelectedItem)
            {
                case "None":
                    tbFilterBy.Visible = false;
                    break;
                case "DriverID":
                    tbFilterBy.Visible = true;
                    break;
                case "PersonID":
                    tbFilterBy.Visible = true;
                    break;
                case "NationalNo":
                    tbFilterBy.Visible = true;
                    break;
                case "FullName":
                    tbFilterBy.Visible = true;
                    break;
                
            }
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like Backspace)
            if (cbFilterBy.SelectedItem.ToString() == "DriverID" || cbFilterBy.SelectedItem.ToString() == "PersonID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block the input
                }
            }
            
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFilterBy.Text))
            {
                _loadAllDrivers();
            }
            else
            {
                switch (cbFilterBy.SelectedItem)
                {
                    case "DriverID":
                        _loadDriversByDriverID();
                        break;
                    case "PersonID":
                        _loadDriversByPersonID();
                        break;
                    case "NationalNo":
                        _loadDriversByNationalNo();
                        break;
                    case "FullName":
                        _loadDriversByFullName();
                        break;

                }
            }
        }
    }
}
