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

namespace Clinic.Appointment
{
    public partial class frmAddUpdateAppointment : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _AppointmentID = -1;
        clsAppointment _Appointment;
        public frmAddUpdateAppointment()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateAppointment(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _Mode = enMode.Update;
        }

        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Appointment";
                this.Text = "New Appointment";
                _Appointment = new clsAppointment();
                ctrlPatientCardWithFilter1.FilterFocus();
                tpAppointmentInfo.Enabled = false;
                tpDoctorInfo.Enabled = false;
                dtpAppointmentDate.MinDate = DateTime.Now;
                cbAppointmentType.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
                lblCreatedByUserID.Text = clsGlobal.CurrentUser.UserID.ToString();
                cbStatus.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Appointment";
                this.Text = "Update Appointment";

                tpAppointmentInfo.Enabled = true;
                tpDoctorInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }



        private void _LoadData()
        {
            ctrlPatientCardWithFilter1.FilterEnabled = false;
            ctrlDoctorCardWithFilter1.FilterEnabled = false;
            _Appointment = clsAppointment.Find(_AppointmentID);

            if (_Appointment == null)
            {
                MessageBox.Show("No Appointment with ID = " + _AppointmentID, "Appointment Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            lblAppointmentID.Text = _Appointment.AppointmentID.ToString();
            lblDoctorID.Text = _Appointment.DoctorID.ToString();
            lblPatientID.Text = _Appointment.PatientID.ToString();
            lblCreatedByUserID.Text = _Appointment.CreatedByUserID.ToString();
            cbAppointmentType.SelectedIndex = _Appointment.AppointmentTypeInfo.ID - 1;
            cbStatus.SelectedIndex = _Appointment.Status - 1;
            dtpAppointmentDate.Value = _Appointment.AppointmentDate;
            lblWorkingDays.Text = ctrlDoctorCardWithFilter1.DoctorWorkingDays;

        }

        private void frmAddUpdateAppointment_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }


        private bool IsDoctorWorkingInThatDay()
        {
            int doctorID = ctrlDoctorCardWithFilter1.DoctorID;

            // 2. حول اليوم المختار لرقم (0 = Sunday, 1 = Monday, etc.)
            byte selectedDay = (byte)(dtpAppointmentDate.Value.DayOfWeek + 1);

            // 3. افحص هل الدكتور شغال في اليوم ده؟
            if (!clsDoctor.IsWorkingOnDay(doctorID, selectedDay))
            {
                MessageBox.Show("Doctor not Working in that day, Select another one.", "Wrong Day", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                dtpAppointmentDate.Value = DateTime.Now;
                return false;
            }
            return true;
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Appointment.PatientID = Convert.ToInt32(lblPatientID.Text);
            _Appointment.DoctorID =Convert.ToInt32(lblDoctorID.Text);
            _Appointment.CreatedByUserID = Convert.ToInt32(lblCreatedByUserID.Text);
            _Appointment.AppointmentTypeInfo.ID = cbAppointmentType.SelectedIndex + 1;
            _Appointment.Status = (byte)(cbStatus.SelectedIndex + 1);
            _Appointment.AppointmentFees = Convert.ToDecimal(lblAppointmentTypeFees.Text);

            //validation of working days of doctor
            if (!IsDoctorWorkingInThatDay())
                return;

            _Appointment.AppointmentDate = dtpAppointmentDate.Value;
            

            if (_Appointment.Save())
            {
                lblAppointmentID.Text = _Appointment.AppointmentID.ToString();
                // change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Appointment";
                this.Text = "Update Appointment";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDoctorNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                //btnSave.Enabled = true;
                tpDoctorInfo.Enabled = true;
                tcAppointmentInfo.SelectedTab = tcAppointmentInfo.TabPages["tpDoctorInfo"];
                return;
            }

            // incase of add new mode.
            if (ctrlPatientCardWithFilter1.PatientID != -1)
            {
                ctrlDoctorCardWithFilter1.FilterFocus();
                tpDoctorInfo.Enabled = true;
                tcAppointmentInfo.SelectedTab = tcAppointmentInfo.TabPages["tpDoctorInfo"];
                lblPatientID.Text = ctrlPatientCardWithFilter1.PatientID.ToString();
            }
            else
            {
                MessageBox.Show("Please Select a Patient", "Select a Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPatientCardWithFilter1.FilterFocus();
            }
        }

        private void btnAppointmentNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpAppointmentInfo.Enabled = true;
                tcAppointmentInfo.SelectedTab = tcAppointmentInfo.TabPages["tpAppointmentInfo"];
                return;
            }

            // incase of add new mode.
            if (ctrlDoctorCardWithFilter1.DoctorID != -1)
            {
                if (clsAppointment.IsAppointmentExistByPatientIDandDoctorID(ctrlPatientCardWithFilter1.PatientID, ctrlDoctorCardWithFilter1.DoctorID))
                {
                    MessageBox.Show("Selected (Patient & Doctor) already have an Appointment, choose another one.", "Select another (Patient | Doctor)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlDoctorCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpAppointmentInfo.Enabled = true;
                    tcAppointmentInfo.SelectedTab = tcAppointmentInfo.TabPages["tpAppointmentInfo"];
                    lblWorkingDays.Text = ctrlDoctorCardWithFilter1.DoctorWorkingDays;
                    lblDoctorID.Text = ctrlDoctorCardWithFilter1.DoctorID.ToString();
                    lblDoctorConsaltantFees.Text = ctrlDoctorCardWithFilter1.DoctorConsultationFees;
                    lblAppointmentTypeFees.Text = _Appointment.AppointmentTypeInfo.Fees.ToString();

                    lblTotalAppointmentFees.Text = (Convert.ToDecimal(lblDoctorConsaltantFees.Text) + Convert.ToDecimal(lblAppointmentTypeFees.Text)).ToString();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Doctor", "Select a Doctor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlDoctorCardWithFilter1.FilterFocus();
            }
        }

        private void cbAppointmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
             lblAppointmentTypeFees.Text = _Appointment.AppointmentTypeInfo.Fees.ToString();

             decimal doctorFees = Convert.ToDecimal(ctrlDoctorCardWithFilter1.DoctorConsultationFees);

             _Appointment.AppointmentFees = doctorFees + _Appointment.AppointmentTypeInfo.Fees;

             lblTotalAppointmentFees.Text = _Appointment.AppointmentFees.ToString() + " $";
        }

    }
}
