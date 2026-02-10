using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Global_Classes;
using Clinicbusiness;

namespace Clinic.Login
{
    public partial class frmLogin : Form
    {
        private clsUser _User;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = "";
            string password = "";

            if(clsGlobal.GetStordCardintal(ref username,ref password))
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else 
                chkRememberMe.Checked = false;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _User = clsUser.FindByUsernameAndPassword(txtUserName.Text,txtPassword.Text);
            if(_User == null)
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid username or password, please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (chkRememberMe.Checked)
            {
                clsGlobal.RememberUserAndPassword(txtUserName.Text, txtPassword.Text);
            }
            else
                clsGlobal.RememberUserAndPassword("", "");

            // Set the current user in the global context
            clsGlobal.CurrentUser = _User;
            this.Hide();

            frmMain frm = new frmMain();
            frm.ShowDialog();
        }
    }
}
