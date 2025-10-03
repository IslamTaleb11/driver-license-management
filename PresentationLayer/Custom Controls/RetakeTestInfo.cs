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
    public partial class RetakeTestInfo : UserControl
    {
        public RetakeTestInfo()
        {
            InitializeComponent();
        }

        public void fillControlsWithData(decimal applicationFees, int applicationID)
        {
            int total = Convert.ToInt32(applicationFees) + 5;
            lblTotalFees.Text = total.ToString();
            lblRetakeTestAppID.Text = applicationID.ToString();
        }
    }
}
