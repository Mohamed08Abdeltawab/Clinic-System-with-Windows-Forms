using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Doctor;
using Clinic.Global_Classes;
using Clinic.Login;
using Clinic.Medical_Services.Mange_Services;
using Clinic.People;
using Clinic.User;
using Clinicbusiness;

namespace Clinic
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CheckIsAdmin())
            {
                frmListPeople frm = new frmListPeople();
                frm.ShowDialog();
            }
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CheckIsAdmin())
            {
                frmListUsers frm = new frmListUsers();
                frm.ShowDialog();
            }
        }

        private void mangeServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CheckIsAdmin())
            {
                frmListServices frm = new frmListServices();
                frm.ShowDialog();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CheckIsAdmin())
            {
                frmListDoctors frm = new frmListDoctors();
                frm.ShowDialog();
            }
        }
    }
}
