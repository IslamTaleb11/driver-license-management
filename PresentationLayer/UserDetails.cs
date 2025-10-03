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
    public partial class UserDetails : UserControl
    {
        public UserDetails()
        {
            InitializeComponent();
        }

        public void fillControlsWithPersonData(clsUser user)
        {
            lblUserID.Text = user.UserID.ToString();
            lblUsername.Text = user.UserName;
            lblIsActive.Text = user.IsActive ? "Yes" : "No";
        }
    }
}
