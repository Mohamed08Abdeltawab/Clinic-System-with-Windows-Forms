using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Global_Classes;
using Clinic.Properties;
using Clinicbusiness;

namespace Clinic.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID;
        private clsPerson _Person;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if(_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _FillPersonInfo();
        }

        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = clsFormat.DateToShort(_Person.DateOfBirth);
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";

            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
            {
                if(File.Exists(_Person.ImagePath))
                {
                   pbPersonImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Image file not found: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;

        }



        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();
            LoadInfo(_PersonID);

        }
    }
}
