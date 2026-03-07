using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Doctor;
using Clinic.Patient;
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

                dgvAppointment.Columns[1].Visible = false;
                dgvAppointment.Columns[2].Visible = false;

                dgvAppointment.Columns[3].HeaderText = "Patient Name"; // تعديل من ID لـ Name
                dgvAppointment.Columns[3].Width = 220;

                dgvAppointment.Columns[4].HeaderText = "Doctor Name"; // تعديل من ID لـ Name
                dgvAppointment.Columns[4].Width = 220;

                dgvAppointment.Columns[5].HeaderText = "Type";
                dgvAppointment.Columns[5].Width = 170;

                dgvAppointment.Columns[6].HeaderText = "Appointment Date";
                dgvAppointment.Columns[6].Width = 220;

                dgvAppointment.Columns[7].HeaderText = "Status";
                dgvAppointment.Columns[7].Width = 170;

                dgvAppointment.Columns[8].HeaderText = "Created By UserID";
                dgvAppointment.Columns[8].Width = 110;
                
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
            frmAddUpdateAppointment frm = new frmAddUpdateAppointment();
            frm.ShowDialog();
            frmListAppointment_Load(null, null);
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
            frmAddUpdateAppointment frm = new frmAddUpdateAppointment();
            frm.ShowDialog();
            frmListAppointment_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvAppointment.CurrentRow.Cells[0].Value;
            frmAddUpdateAppointment frm = new frmAddUpdateAppointment(AppointmentID);
            frm.ShowDialog();
            frmListAppointment_Load(null,null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvAppointment.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want to delete this Appointment!","Delete Appointment",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsAppointment.DeleteAppointment(AppointmentID))
                {
                    MessageBox.Show("Appointment has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListAppointment_Load(null, null);
                }
                else
                    MessageBox.Show("Appointment is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Appointment ID")//
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showPatientDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PatientID = (int)dgvAppointment.CurrentRow.Cells[1].Value;
            frmPatientInfo frm = new frmPatientInfo(PatientID);
            frm.ShowDialog();
        }

        private void showDoctorDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DoctorID = (int)dgvAppointment.CurrentRow.Cells[2].Value;
            frmDoctorInfo frm = new frmDoctorInfo(DoctorID);
            frm.ShowDialog();
        }
    }
}
