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

namespace Clinic.Appointment.AppointmentType
{
    public partial class frmEditAppointmentType : Form
    {
        private int _AppointmentsTypeID = -1;
        clsAppointmentType _AppointmentType;
        public frmEditAppointmentType(int AppointmentTypeID)
        {
            InitializeComponent();
            _AppointmentsTypeID = AppointmentTypeID;
        }

        private void frmEditAppointmentType_Load(object sender, EventArgs e)
        {
            _AppointmentType = clsAppointmentType.Find(_AppointmentsTypeID);
            if (_AppointmentType == null)
            {
                MessageBox.Show($"Error, there is no Appointment Type with ID: {_AppointmentsTypeID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblApplicationTypeID.Text = _AppointmentsTypeID.ToString();
            txtAppointmentTypeName.Text = _AppointmentType.Name.ToString();
            txtAppointmentTypeFees.Text = _AppointmentType.Fees.ToString();

        }

        private void txtAppointmentTypeName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppointmentTypeName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppointmentTypeName, "Appointment Type Name cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtAppointmentTypeName, null);
            }
        }

        private void txtAppointmentTypeFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppointmentTypeFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppointmentTypeFees, "Appointment Type Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtAppointmentTypeFees, null);
            }

            //not used because using keypress to prevent the non degit input
            //if (!clsValidation.IsNumber(txtAppointmentTypeFees.Text))
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(txtAppointmentTypeFees, "Invalid Number.");
            //}
            //else
            //{
            //    errorProvider1.SetError(txtAppointmentTypeFees, null);
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _AppointmentType.Name = txtAppointmentTypeName.Text.Trim();
            _AppointmentType.Fees = Convert.ToDecimal(txtAppointmentTypeFees.Text.Trim());

            if (_AppointmentType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAppointmentTypeFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
