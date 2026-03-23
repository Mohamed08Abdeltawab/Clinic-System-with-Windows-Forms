using Clinic.Doctor;
using Clinic.Patient;
using Clinicbusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Visit
{
    public partial class frmFillVisitDetails : Form
    {
        private enum enMode { AddNew = 0, Update = 1};
        private enMode _Mode = enMode.AddNew;
        private int _AppointmentID;
        private clsVisit _Visit;
        public frmFillVisitDetails(int AppointmentID, int Mode)//sending mode as int to avoid issues with combo box selected value
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _Mode = (enMode)Mode;
        }

        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Set Visit Details";
                _Visit = new clsVisit(); // تهيئة الكائن الجديد هنا
                lblVisitID.Text = "[???]";
                dtpDateTime.MinDate = DateTime.Now;
                dtpDateTime.Value = DateTime.Now;

                // جلب بيانات الموعد لعرض معرف المريض والطبيب
                // نفترض وجود كلاس clsAppointment
                var appointment = clsAppointment.Find(_AppointmentID);
                if (appointment == null)
                {
                    MessageBox.Show("No appointment found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblPatientID.Text = appointment.PatientID.ToString();
                lblDoctorID.Text = appointment.DoctorID.ToString();
                lblAppointmentID.Text = _AppointmentID.ToString();
            }
            else
            {
                lblTitle.Text = "Edit Visit Details";
                _Visit = clsVisit.FindByAppointmentID(_AppointmentID);

                if (_Visit == null)
                {
                    MessageBox.Show("No visit found for this appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                lblVisitID.Text = _Visit.VisitID.ToString();
                txtDiagnosis.Text = _Visit.Diagnosis;
                txtNotes.Text = _Visit.Notes;
                dtpDateTime.MinDate = DateTime.Now.AddDays(-_Visit.VisitDate.Day);
                dtpDateTime.Value = dtpDateTime.MinDate;
                lblPatientID.Text = _Visit.AppointmentInfo.PatientID.ToString();
                lblDoctorID.Text = _Visit.AppointmentInfo.DoctorID.ToString();
                lblAppointmentID.Text = _Visit.AppointmentID.ToString();
            }
        }

        private void frmFillVisitDetails_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void llPatientInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblPatientID.Text, out int patientID))
            {
                frmPatientInfo frm = new frmPatientInfo(patientID);
                frm.ShowDialog();
            }
        }

        private void llDoctorInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblDoctorID.Text, out int doctorID))
            {
                frmDoctorInfo frm = new frmDoctorInfo(doctorID);
                frm.ShowDialog();
            }
        }

        private void txtDiagnosis_Validating(object sender, CancelEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtDiagnosis.Text.Trim()))
            {
                errorProvider1.SetError(txtDiagnosis, "");
            }
            else
            {
                errorProvider1.SetError(txtDiagnosis, "Please enter the diagnosis.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Visit.AppointmentID = _AppointmentID;
            _Visit.VisitDate = dtpDateTime.Value;
            _Visit.Diagnosis = txtDiagnosis.Text.Trim();
            _Visit.Notes = txtNotes.Text.Trim();

            if (_Visit.Save())
            {
                lblVisitID.Text = _Visit.VisitID.ToString();
                lblTitle.Text = "Edit Visit Details";
                _Mode = enMode.Update;
                MessageBox.Show("Visit details saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to save visit details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
