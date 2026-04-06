using Clinic.Appointment;
using Clinic.People;
using Clinicbusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Visit
{
    public partial class frmListVisits : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        static DataTable _dtAllVisits;
        DataTable _dtVists;

        public frmListVisits()
        {
            InitializeComponent();
        }

        private void _RefreshVisitList()
        {
            _dtAllVisits = clsVisit.GetAllVisits();
            _dtVists = _dtAllVisits.DefaultView.ToTable(false, "VisitID", "AppointmentID", "PatientName", "DoctorName", "VisitDate", "Diagnosis");

            dgvVisit.DataSource = _dtVists;
            lblRecordsCount.Text = dgvVisit.Rows.Count.ToString();
        }

        private void frmListVisits_Load(object sender, EventArgs e)
        {
            _RefreshVisitList();
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvVisit.Rows.Count.ToString();

            if(dgvVisit.Rows.Count > 0 )
            {
                dgvVisit.Columns[0].HeaderText = "Visit ID";
                dgvVisit.Columns[0].Width = 110;


                dgvVisit.Columns[1].HeaderText = "Appointment ID";
                dgvVisit.Columns[1].Width = 110;

                dgvVisit.Columns[2].HeaderText = "Patient Name";
                dgvVisit.Columns[2].Width = 260;

                dgvVisit.Columns[3].HeaderText = "Docotr Name";
                dgvVisit.Columns[3].Width = 260;

                dgvVisit.Columns[4].HeaderText = "Visit Date";
                dgvVisit.Columns[4].Width = 200;

                dgvVisit.Columns[5].HeaderText = "Diagnosis";
                dgvVisit.Columns[5].Width = 260;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Visit ID":
                    FilterColumn = "VisitID";
                    break;

                case "Appointment ID":
                    FilterColumn = "AppointmentID";
                    break;

                case "Patient Name":
                    FilterColumn = "PatientName";
                    break;

                case "Doctor Name":
                    FilterColumn = "DoctorName";
                    break;

                case "Diagnosis":
                    FilterColumn = "Diagnosis";
                    break;

            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtVists.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtVists.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "AppointmentID" || FilterColumn == "VisitID")
                //in this case we deal with integer not string.
                _dtVists.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtVists.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtVists.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Appointment ID" || cbFilterBy.Text == "Visit ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void ShowAppointmentListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListAppointment frm = new frmListAppointment();
            frm.ShowDialog();
            frmListVisits_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvVisit.CurrentRow.Cells[1].Value;

            frmAddUpdatelVisitDetails frm = new frmAddUpdatelVisitDetails(AppointmentID, (int)enMode.Update);
            frm.ShowDialog();
            frmListVisits_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int VisitID = (int)dgvVisit.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want delete this Visit!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsVisit.DeleteVisit(VisitID))
                {
                    MessageBox.Show("Visit has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListVisits_Load(null, null);
                }
                else
                    MessageBox.Show("Visit is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
