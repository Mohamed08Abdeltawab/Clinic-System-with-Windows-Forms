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
    public partial class frmListAppointment : Form
    {
        private DataTable _dtAppointments;
        public frmListAppointment()
        {
            InitializeComponent();
        }

        private void frmListAppointment_Load(object sender, EventArgs e)
        {
            _dtAppointments = clsAppointment.GetAllAppointments();
            dgvAppointment.DataSource = _dtAppointments;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvAppointment.Rows.Count.ToString();

            if (dgvAppointment.Rows.Count > 0)
            {
                dgvAppointment.Columns[0].HeaderText = "Appointment ID";
                dgvAppointment.Columns[0].Width = 110;

                dgvAppointment.Columns[1].HeaderText = "Patient Name"; // تعديل من ID لـ Name
                dgvAppointment.Columns[1].Width = 220;

                dgvAppointment.Columns[2].HeaderText = "Doctor Name"; // تعديل من ID لـ Name
                dgvAppointment.Columns[2].Width = 220;

                dgvAppointment.Columns[3].HeaderText = "Type";
                dgvAppointment.Columns[3].Width = 170;

                dgvAppointment.Columns[4].HeaderText = "Appointment Date";
                dgvAppointment.Columns[4].Width = 220;

                dgvAppointment.Columns[5].HeaderText = "Status";
                dgvAppointment.Columns[5].Width = 170;

                dgvAppointment.Columns[6].HeaderText = "Created By UserID";
                dgvAppointment.Columns[6].Width = 110;
            }
        }

        
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Appointment ID":
                    FilterColumn = "AppointmentID";
                    break;

                case "Patient Name":
                    FilterColumn = "PatientName";
                    break;

                case "Doctor Name":
                    FilterColumn = "DoctorName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAppointments.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAppointments.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "AppointmentID")
                //in this case we deal with integer not string.
                _dtAppointments.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAppointments.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAppointments.Rows.Count.ToString();
        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Appointment Type" && cbFilterBy.Text != "Status");

            if (txtFilterValue.Visible)
            {
                cbAppointmentType.Visible = false;
                cbStatus.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else if (cbFilterBy.Text == "Appointment Type")
            {
                cbAppointmentType.SelectedIndex = 0;
                cbAppointmentType.Visible = true;
                txtFilterValue.Visible = false;
                cbStatus.Visible = false;
            }
            else if (cbFilterBy.Text == "Status")
            {
                cbStatus.SelectedIndex = 0;
                cbStatus.Visible = true;
                txtFilterValue.Visible = false;
                cbAppointmentType.Visible = false;
            }
            else
            {
                cbAppointmentType.Visible = false;
                cbStatus.Visible = false;
            }
        }

        private void cbAppointmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbAppointmentType.Text;

            if (FilterValue == "All")
            {
                _dtAppointments.DefaultView.RowFilter = "";
            }
            else
                _dtAppointments.DefaultView.RowFilter = string.Format("[AppointmentType] LIKE '%{0}%'", FilterValue);

            lblRecordsCount.Text = dgvAppointment.Rows.Count.ToString();

        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbStatus.Text;

            if (FilterValue == "All")
            {
                _dtAppointments.DefaultView.RowFilter = "";
            }
            else
                _dtAppointments.DefaultView.RowFilter = string.Format("[Status] LIKE '%{0}%'", FilterValue);

            lblRecordsCount.Text = dgvAppointment.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            //
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //details of patient and some of doctor
        }

        private void startVisitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show visit form 
        }

        private void rescheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //update date
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Appointment ID")//
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
