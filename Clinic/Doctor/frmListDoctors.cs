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

namespace Clinic.Doctor
{
    public partial class frmListDoctors : Form
    {
        private DataTable _dtDoctors;

        public frmListDoctors()
        {
            InitializeComponent();
        }

        private void frmListDoctors_Load(object sender, EventArgs e)
        {
            _dtDoctors = clsDoctor.GetAllDoctors();
            dgvDoctors.DataSource = _dtDoctors;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvDoctors.Rows.Count.ToString();

            if (dgvDoctors.Rows.Count > 0)
            {
                dgvDoctors.Columns[0].HeaderText = "Doctor ID";
                dgvDoctors.Columns[0].Width = 110;

                dgvDoctors.Columns[1].HeaderText = "Person ID";
                dgvDoctors.Columns[1].Width = 110;

                dgvDoctors.Columns[2].HeaderText = "Full Name";
                dgvDoctors.Columns[2].Width = 320;

                dgvDoctors.Columns[3].HeaderText = "Specialization";
                dgvDoctors.Columns[3].Width = 170;

                dgvDoctors.Columns[4].HeaderText = "Consultation Fees";
                dgvDoctors.Columns[4].Width = 130;

                dgvDoctors.Columns[5].HeaderText = "Working Days";
                dgvDoctors.Columns[5].Width = 200;

                // تم إزالة عمود WorkingDays من هنا لأنه لم يعد موجوداً في الـ DataTable الأساسي
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Doctor ID":
                    FilterColumn = "DoctorID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Specialization":
                    FilterColumn = "Specialization";
                    break;

                // تم إزالة فلتر WorkingDays لأنه يحتاج Query خاص بسبب الجدول الوسيط

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDoctors.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDoctors.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "DoctorID")
                //in this case we deal with integer not string.
                _dtDoctors.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtDoctors.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvDoctors.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" || cbFilterBy.Text != "Working Days");

            if (txtFilterValue.Visible)
            {
                cbWorkingDay.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if(cbFilterBy.Text == "Working Days")
            {
                cbWorkingDay.SelectedIndex = 0;
                cbWorkingDay.Visible = true;
                txtFilterValue.Visible = false;
            }
        }

        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor frm = new frmAddUpdateDoctor();
            frm.ShowDialog();
            frmListDoctors_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // تم التصحيح: استخراج الـ DoctorID إذا كان الفورم يحتاجه، أو الـ PersonID من العمود الصحيح
            int DoctorID = (int)dgvDoctors.CurrentRow.Cells[0].Value;
            // إذا كان فورم التفاصيل يأخذ PersonID، يجب قراءته من العمود 1
            int PersonID = (int)dgvDoctors.CurrentRow.Cells[1].Value;

            // تأكد من الباراميتر الذي يتوقعه فورم التفاصيل
            frmDoctorInfo frm = new frmDoctorInfo(PersonID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor frm = new frmAddUpdateDoctor();
            frm.ShowDialog();
            frmListDoctors_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // تم التصحيح: العمود 0 هو DoctorID
            int DoctorID = (int)dgvDoctors.CurrentRow.Cells[0].Value;
            frmAddUpdateDoctor frm = new frmAddUpdateDoctor(DoctorID);
            frm.ShowDialog();
            frmListDoctors_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DoctorID = (int)dgvDoctors.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want delete this Doctor!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsDoctor.DeleteDoctor(DoctorID))
                {
                    MessageBox.Show("Doctor has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListDoctors_Load(null, null);
                }
                else
                    MessageBox.Show("Doctor is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Doctor ID" || cbFilterBy.Text == "Person ID")//
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbWorkingDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbWorkingDay.Text;
            
            if (FilterValue == "All")
            {
                _dtDoctors.DefaultView.RowFilter = "";
            }
            else
                _dtDoctors.DefaultView.RowFilter = string.Format("[WorkingDays] LIKE '%{0}%'", FilterValue);

            lblRecordsCount.Text = dgvDoctors.Rows.Count.ToString();

        }
    }
}