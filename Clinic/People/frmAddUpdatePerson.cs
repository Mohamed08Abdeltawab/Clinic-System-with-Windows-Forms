using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Global_Classes;
using Clinic.Properties;
using Clinicbusiness;

namespace Clinic.People
{
    public partial class frmAddUpdatePerson : Form
    {
        //declare a delegate to return databack to the calling form after saving the person

        public delegate void DataBackEventHandler(object sender, int PersonID);

        //declare an event of delegate type to be raised after saving the person
        public event DataBackEventHandler DataBack;

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

        //reset default values to the controls when the form loads
        private void _ResetDefaultValues()
        {
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson(); //initialize a new person object for add new mode
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null); //show remove image link only if there is an image to remove

            dtpDateOfBirth.Value = DateTime.Now;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-150);

            txtFullName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            rbMale.Checked = true;
            
        }

        private void _LoadData()
        {
            //just will called on update mode 
            _Person = clsPerson.Find(_PersonID);

            if(_Person == null)
            {
                MessageBox.Show("Person not found. The form will now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFullName.Text = _Person.FullName;
            txtPhone.Text = _Person.Phone;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            txtAddress.Text = _Person.Address ?? "";//if null take ""
            txtEmail.Text = _Person.Email ?? "";
            if(!string.IsNullOrEmpty(pbPersonImage.ImageLocation))
            pbPersonImage.ImageLocation = _Person.ImagePath ;

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;


            llRemoveImage.Visible = (!string.IsNullOrEmpty(_Person.ImagePath));

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        //handel image person
        private bool _HandlePersonImage()
        {
            //step1: check if the new image is same image will be true don't compelete
            if(pbPersonImage.ImageLocation != _Person.ImagePath)
            {
                //step2: we need to delete the image check if exist
                if(_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                //step3: update new image 
                if(pbPersonImage.ImageLocation != null)
                {
                    //assign source file path to equal the imagelocation
                    string sourceFilePath = pbPersonImage.ImageLocation.ToString();

                    if(util.CopyImageToProjectImages(ref sourceFilePath))
                    {
                        pbPersonImage.ImageLocation = sourceFilePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        private void _LoadInfo()
        {
            //change photo to default if no image path is set
            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = (_Person.ImagePath != "");//show remove image link only if there is an image to remove
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            if(!this._HandlePersonImage())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _Mode = enMode.Update; // Switch to update mode after saving
                MessageBox.Show("Person saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Raise the DataBack event to notify the calling form that a person has been saved
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("An error occurred while saving the person. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false; //hide the remove image link since there is no image to remove
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            //initialize temp from textbox text to avoid multiple calls 
            TextBox temp = ((TextBox)sender);

            if (string.IsNullOrEmpty(temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError((Control)temp, "This field is required.");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError((Control)temp, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //Email can be optional, but if provided, it must be valid
            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()) && !clsValidation.ValidateEmail(txtEmail.Text))
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

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.Female_512;
            }
        }
    }
}
