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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _loadAllTestTypes()
        {
            dataGridView1.DataSource = clsTestType.GetAllTestTypes();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            _fillRecordsCountLbl();
        }

        

        private void _fillRecordsCountLbl()
        {
            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _loadAllTestTypes();
        }

        private void editTestTypeStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int selectedTestTypeID = Convert.ToInt32(selectedRow.Cells["TestTypeID"].Value);
                frmUpdateTestType frmUpdateTestType = new frmUpdateTestType(selectedTestTypeID);
                frmUpdateTestType.onTestTypeUpdate += _loadAllTestTypes;
                frmUpdateTestType.ShowDialog();
            }
        }
    }
}
