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
                ctrlPersonCardWithFilter1.FilterFocus();
                tpAppointmentInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Appointment";
                this.Text = "Update Appointment";

                tpAppointmentInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }



        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _Appointment = clsAppointment.Find(_AppointmentID);

            if (_AppointmentID == null)
            {
                MessageBox.Show("No Doctor with ID = " + _AppointmentID, "Doctor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Close();
                return;
            }
            //lblDoctorID.Text = _Appointment.DoctorID.ToString();
            //ctrlPersonCardWithFilter1.LoadPersonData(_Doctor.PersonID);
            //txtSpecialization.Text = _Doctor.Specialization.ToString();
            //txtConsultationFees.Text = _Doctor.ConsultationFees.ToString();

        }

        private void frmAddUpdateAppointment_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Doctor.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Doctor.Specialization = txtSpecialization.Text.Trim();
            _Doctor.ConsultationFees = Convert.ToDecimal(txtConsultationFees.Text.Trim());

            // التأكد من اختيار يوم عمل واحد على الأقل
            if (chkWorkingDays.CheckedItems.Count < 1)
            {
                chkWorkingDays.Focus();
                if (MessageBox.Show("Do you want to complete without giving Working Days for this Doctor?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }
            }

            // 🌟 استدعاء الدالة لتعبئة List<byte> قبل الحفظ
            FillDoctorWorkingDaysList();

            if (_Doctor.Save())
            {
                lblDoctorID.Text = _Doctor.DoctorID.ToString();
                // change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Doctor";
                this.Text = "Update Doctor";

                

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsAppointment.IsAppointmentExistByPatientID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Patient already a Appointment, choose another one.", "Select another Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpAppointmentInfo.Enabled = true;
                    tcAppointmentInfo.SelectedTab = tcAppointmentInfo.TabPages["tpAppointmentInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }
    }
}
