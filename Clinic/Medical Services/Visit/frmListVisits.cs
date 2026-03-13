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
        static DataTable _dtAllVisits = clsVisit.GetAllVisits();
        DataTable _dtVists = _dtAllVisits.DefaultView.ToTable(false, "VisitID", "AppointmentID", "PatientName", "DoctorName", "VisitDate", "Diagnosis");

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
            dgvVisit.DataSource = _dtVists;
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
    }
}
