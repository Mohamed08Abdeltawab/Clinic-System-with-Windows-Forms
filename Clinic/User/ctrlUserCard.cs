using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinicbusiness;

namespace Clinic.User
{
    public partial class ctrlUserCard : UserControl
    {
        clsUser _User;
        private int _UserID = -1;

        //useing UserID in forms 
        public int UserID
        {
            get {  return _UserID; }
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }


        public void LoadUserInfo(int PersonID)
        {
            _User = clsUser.FindByPersonID(PersonID);
            _UserID = _User.UserID;
            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("Error, user is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            lblRole.Text = _User.RoleName.ToString();
        }

        private void _ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblRole.Text = "[???]";
        }
    }
}
