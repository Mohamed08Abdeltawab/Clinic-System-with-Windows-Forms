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
    public partial class frmListPatient : Form
    {
        private DataTable _dtPatients;
        public frmListPatient()
        {
            InitializeComponent();
        }

        private void frmListPatient_Load(object sender, EventArgs e)
        {
            _dtPatients = clsPatient.GetAllPatients();
            dgvPatient.DataSource = _dtPatients;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPatient.Rows.Count.ToString();

            if (dgvPatient.Rows.Count > 0)
            {
                dgvPatient.Columns[0].HeaderText = "Patient ID";
                dgvPatient.Columns[0].Width = 110;

                dgvPatient.Columns[1].HeaderText = "Person ID";
                dgvPatient.Columns[1].Width = 110;

                dgvPatient.Columns[2].HeaderText = "Full Name";
                dgvPatient.Columns[2].Width = 280;

                dgvPatient.Columns[3].HeaderText = "MedicalHistory";
                dgvPatient.Columns[3].Width = 280;

                dgvPatient.Columns[4].HeaderText = "Blood Type Name";
                dgvPatient.Columns[4].Width = 110;

                dgvPatient.Columns[5].HeaderText = "Emergency Contact";
                dgvPatient.Columns[5].Width = 170;

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Patient ID":
                    FilterColumn = "PatientID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Medical History":
                    FilterColumn = "MedicalHistory";
                    break;

                case "Emergency Contact":
                    FilterColumn = "EmergencyContact";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPatients.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPatient.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "PatientID")
                //in this case we deal with integer not string.
                _dtPatients.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPatients.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvPatient.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Blood Type");
            cbBloodType.Visible = (cbFilterBy.Text == "Blood Type");
            if (txtFilterValue.Visible)
            {
                cbBloodType.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if (cbBloodType.Visible)
            {
                cbBloodType.SelectedIndex = 0;
                cbBloodType.Visible = true;
                txtFilterValue.Visible = false;
            }
        }

        private void cbBloodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBloodType.Text == "All" || cbBloodType.SelectedIndex == -1)//when mode is all or no item selected
            {
                _dtPatients.DefaultView.RowFilter = "";
            }
            else
                // تأكد أن اسم العمود في الـ DataTable هو BloodTypeName
                _dtPatients.DefaultView.RowFilter = string.Format("[BloodTypeName] = '{0}'", cbBloodType.Text);

            lblRecordsCount.Text = dgvPatient.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            frmAddUpdatePatient frm = new frmAddUpdatePatient();
            frm.ShowDialog();
            frmListPatient_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PatientID = (int)dgvPatient.CurrentRow.Cells[0].Value;
            frmPatientInfo frm = new frmPatientInfo(PatientID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PatientID = (int)dgvPatient.CurrentRow.Cells[0].Value;
            frmAddUpdatePatient frm = new frmAddUpdatePatient(PatientID);
            frm.ShowDialog();
            frmListPatient_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PatientID = (int)dgvPatient.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want delete this Patient!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPatient.IsPatientExist(PatientID))
                {
                    MessageBox.Show("Patient has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListPatient_Load(null, null);
                }
                else
                    MessageBox.Show("Patient is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Patient ID" || cbFilterBy.Text == "Person ID")//
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePatient frm = new frmAddUpdatePatient();
            frm.ShowDialog();
            frmListPatient_Load(null, null);
        }
    }
}
