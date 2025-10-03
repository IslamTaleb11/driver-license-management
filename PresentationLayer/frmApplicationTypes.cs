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
    public partial class frmApplicationTypes : Form
    {
        public frmApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _loadAllApplicationTypes()
        {
            dataGridView1.DataSource = clsApplicationType.GetAllApplicationTypes();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            _fillRecordsCountLbl();
        }

        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _loadAllApplicationTypes();
        }

        private void _fillRecordsCountLbl()
        {
            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int selectedApplicationTypeID = Convert.ToInt32(selectedRow.Cells["ApplicationTypeID"].Value);
                frmUpdateApplicationType frmUpdateApplicationType = new frmUpdateApplicationType(selectedApplicationTypeID);
                frmUpdateApplicationType.onApplicationTypeUpdate += _loadAllApplicationTypes;
                frmUpdateApplicationType.ShowDialog();
            }
        }
    }
}
