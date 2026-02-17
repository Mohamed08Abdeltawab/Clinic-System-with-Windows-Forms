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

namespace Clinic.People
{
    public partial class frmAddUpdatePerson : Form
    {
        clsPerson _Person;
        private int _PersonID = -1;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        //initialize enMode
        enMode _Mode = enMode.AddNew;

        //when no personID is sent will be in add new mode by default
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        //use when i want to update person from another form by sending the personID to this form
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            //
            _Person.Mode = clsPerson.enMode.AddNew;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Person.FullName = txtFullName.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            //if the user left the address or email fields empty, we can set them to an empty string to avoid null values
            _Person.Address = txtAddress.Text?.Trim() ?? "";
            _Person.Email = txtEmail.Text?.Trim() ?? "";

            if (rbMale.Checked)
                _Person.Gendor = (byte)enGendor.Male;
            else
                _Person.Gendor = (byte)enGendor.Female;



            if (_Person.Save())
            {
                lblTitle.Text = "Update Person"; // Change title to indicate we're now in edit mode
                lblPersonID.Text = _Person.PersonID.ToString(); // Get the assigned PersonID after saving
                MessageBox.Show("Person saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error occurred while saving the person. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(sender.ToString().Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError((Control)sender, "This field is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError((Control)sender, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //Email can be optional, but if provided, it must be valid
            if (!string.IsNullOrEmpty(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Please enter a valid email address.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
        }


    }
}
