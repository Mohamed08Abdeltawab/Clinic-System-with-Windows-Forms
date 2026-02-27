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

namespace Clinic.Patient
{
    public partial class frmAddUpdatePatient : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _PatientID = -1;
        clsPatient _Patient;

        public frmAddUpdatePatient()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePatient(int PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;
            _Mode = enMode.Update;
        }

        private void frmAddUpdatePatient_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                cbBloodType.SelectedIndex = 0;
                lblTitle.Text = "New Patient";
                this.Text = "New Patient";
                _Patient = new clsPatient();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpPatientInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Patient";
                this.Text = "Update Patient";

                tpPatientInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _Patient = clsPatient.Find(_PatientID);

            if (_Patient == null)
            {
                MessageBox.Show("No Patient with ID = " + _PatientID, "Doctor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Close();
                return;
            }
            lblPatientID.Text = _Patient.PatientID.ToString();
            ctrlPersonCardWithFilter1.LoadPersonData(_Patient.PersonID);
            txtMedicalHistory.Text = _Patient.MedicalHistory.ToString();
            txtEmergencyContact.Text = _Patient.EmergencyContact.ToString();
            cbBloodType.SelectedIndex = _Patient.BloodType;
            
        }


        private void btnPatientNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpPatientInfo.Enabled = true;
                tcPatientInfo.SelectedTab = tcPatientInfo.TabPages["tpPatientInfo"];
                return;
            }

            // incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsPatient.IsPatientExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already a Patient, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpPatientInfo.Enabled = true;
                    tcPatientInfo.SelectedTab = tcPatientInfo.TabPages["tpPatientInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Patient.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Patient.MedicalHistory = txtMedicalHistory.Text.Trim();
            _Patient.EmergencyContact = txtEmergencyContact.Text.Trim();
            //blood type
            _Patient.BloodType = (byte)(clsPatient.enBloodType)cbBloodType.SelectedIndex;


            if (_Patient.Save())
            {
                lblPatientID.Text = _Patient.PatientID.ToString();
                // change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Patient";
                this.Text = "Update Patient";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        

        private void txtEmergencyContact_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmergencyContact.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmergencyContact, "txtEmergency Contact can't be blank!");
            }
            else
            {
                errorProvider1.SetError(txtEmergencyContact, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
