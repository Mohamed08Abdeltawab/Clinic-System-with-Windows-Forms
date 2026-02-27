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
    public partial class ctrlPatientInfo : UserControl
    {
        private clsPatient _Patient;
        private int _PatientID;

        //need public id
        public int PatientID
        {
            get { return _PatientID; }
        }
        public ctrlPatientInfo()
        {
            InitializeComponent();
        }


        public void LoadDoctorInfo(int PatientID)
        {
            _Patient = clsPatient.Find(PatientID);
            if (_Patient == null)
            {
                _ResetPersonInfo();
                MessageBox.Show($"No Patient with ID: {PatientID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillDoctorInfo();
        }

        private void _FillDoctorInfo()
        {

            ctrlPersonCard1.LoadInfo(_Patient.PersonID);
            lblPatientID.Text = _Patient.PatientID.ToString();
            lblMedicalHistory.Text = _Patient.MedicalHistory.ToString();
            lblBloodType.Text = _Patient.BloodType.ToString();
            lblEmergencyContact.Text = _Patient.EmergencyContact.ToString();

        }

        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetPersonInfo();
            lblPatientID.Text = "[???]";
            lblMedicalHistory.Text = "[???]";
            lblBloodType.Text = "[???]";
            lblEmergencyContact.Text = "[???]";
        }
    }
}
