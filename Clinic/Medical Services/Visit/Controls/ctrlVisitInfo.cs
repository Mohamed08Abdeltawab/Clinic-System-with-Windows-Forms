using Clinic.Doctor;
using Clinic.Patient;
using Clinicbusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Visit
{
    public partial class ctrlVisitInfo : UserControl
    {
        private int _VisitID = -1;
        private clsVisit _Visit;

        public int VisitID => _VisitID;
        public clsVisit SelectedVisitInfo => _Visit;
        
        private int _Mode = 0;//0:update, 1:read
        
        public ctrlVisitInfo()
        {
            InitializeComponent();
        }


        // دالة لإعادة ضبط القيم للوضع الافتراضي
        public void ResetDefaultValues()
        {
            _VisitID = -1;
            _Visit = null;

            lblVisitID.Text = "[???]";
            lblAppointmentID.Text = "[???]";
            lblPatientID.Text = "[???]";
            lblDoctorID.Text = "[???]";
            dtpDateTime.Value = DateTime.Now;
            txtDiagnosis.Text = "";
            txtNotes.Text = "";
        }

        // دالة تحميل البيانات باستخدام الـ VisitID
        public void LoadVisitInfo(int VisitID)
        {
            _Visit = clsVisit.Find(VisitID);
            if (_Visit == null)
            {
                MessageBox.Show("No Visit with ID = " + VisitID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetDefaultValues();
                return;
            }

            _FillVisitData();
        }

        // دالة تحميل البيانات باستخدام الـ AppointmentID (مهمة جداً لشاشة الـ Payment)
        public void LoadVisitInfoByAppointmentID(int AppointmentID)
        {
            _Visit = clsVisit.FindByAppointmentID(AppointmentID);
            if (_Visit == null)
            {
                MessageBox.Show("No Visit found for Appointment ID = " + AppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetDefaultValues();
                return;
            }

            _FillVisitData();
        }

        public void LoadVisitInfoByPrescriptionID(int PrescriptionID)
        {
            _Visit = clsVisit.FindByPrescriptionID(PrescriptionID);
            if (_Visit == null)
            {
                MessageBox.Show("No Visit found for Prescription ID = " + PrescriptionID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetDefaultValues();
                return;
            }

            _FillVisitData();
        }

        private void _FillVisitData()
        {
            _VisitID = _Visit.VisitID;
            lblVisitID.Text = _Visit.VisitID.ToString();
            lblAppointmentID.Text = _Visit.AppointmentID.ToString();
            lblPatientID.Text = _Visit.AppointmentInfo.PatientID.ToString();
            lblDoctorID.Text = _Visit.AppointmentInfo.DoctorID.ToString();
            dtpDateTime.Value = _Visit.VisitDate;
            txtDiagnosis.Text = _Visit.Diagnosis;
            txtNotes.Text = string.IsNullOrEmpty(_Visit.Notes) ? "No Notes" : _Visit.Notes;
        }

        private void llPatientInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblPatientID.Text, out int patientID))
            {
                frmPatientInfo frm = new frmPatientInfo(patientID);

                if (_Mode == 1)//Read-Only Mode
                {
                    frm.IsReadOnly = true;
                }

                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Patient ID Not Selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llDoctorInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblDoctorID.Text, out int doctorID))
            {
                frmDoctorInfo frm = new frmDoctorInfo(doctorID);

                if (_Mode == 1)//Read-Only Mode
                {
                    frm.IsReadOnly = true;
                }

                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Doctor ID Not Selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llEditVisitInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(string.IsNullOrEmpty(lblAppointmentID.Text)) { return; }
            frmAddUpdatelVisitDetails frm = new frmAddUpdatelVisitDetails(int.Parse(lblAppointmentID.Text));
            frm.SetOnlyVisitMode();
            frm.ShowDialog();
        }
    }
}
