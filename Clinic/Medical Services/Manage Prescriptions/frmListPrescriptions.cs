using Clinic.Medical_Services.Medicine;
using Clinic.Medical_Services.Visit;
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

namespace Clinic.Medical_Services.Manage_Prescriptions
{
    public partial class frmListPrescriptions : Form
    {
        private enum enMode { AddNew = 0, Update = 1, Read = 2 };
        // نستخدم DataTable واحدة للتعامل مع العرض والفلترة
        private DataTable _dtAllPrescriptions;


        public frmListPrescriptions()
        {
            InitializeComponent();
        }


        private void frmListPrescriptions_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllPrescriptions = clsPrescription.GetAllPrescriptions();
            dgvPrescription.DataSource = _dtAllPrescriptions;
            lblRecordsCount.Text = dgvPrescription.Rows.Count.ToString();

            if (dgvPrescription.Rows.Count > 0)
            {
                dgvPrescription.Columns[0].HeaderText = "Prescription ID";
                dgvPrescription.Columns[0].Width = 110;

                dgvPrescription.Columns[1].HeaderText = "Patient Name";
                dgvPrescription.Columns[1].Width = 250;

                dgvPrescription.Columns[2].HeaderText = "Date";
                dgvPrescription.Columns[2].Width = 170;

                dgvPrescription.Columns[3].HeaderText = "Visit ID";
                dgvPrescription.Columns[3].Width = 110;
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

            // ربط الاختيار باسم العمود الحقيقي في الداتابيز
            switch (cbFilterBy.Text)
            {
                case "Prescription ID":
                    FilterColumn = "PrescriptionID";
                    break;

                case "Patient Name":
                    FilterColumn = "PatientName";
                    break;

                case "Visit ID":
                    FilterColumn = "VisitID";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtAllPrescriptions.DefaultView.RowFilter = "";
            }
            else
            {
                // إذا كان الفلتر نصي (اسم المريض) نستخدم LIKE
                if (FilterColumn == "PatientName")
                    _dtAllPrescriptions.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());
                else
                    // إذا كان الفلتر رقمي (ID) نستخدم = 
                    _dtAllPrescriptions.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }

            lblRecordsCount.Text = dgvPrescription.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // نمنع كتابة الحروف فقط لو كان الفلتر يعتمد على أرقام الـ IDs
            if (cbFilterBy.Text == "Prescription ID" || cbFilterBy.Text == "Visit ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void showPrescriptionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFillVisitDetails frm = new frmFillVisitDetails((int)dgvPrescription.CurrentRow.Cells["VisitID"].Value, (int)enMode.Read);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListVisits frm = new frmListVisits();
            frm.ShowDialog();
            frmListPrescriptions_Load(null, null);
        }

        private void showVisitandPrescriptionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int VisitID = (int)dgvPrescription.CurrentRow.Cells["VisitID"].Value;
            clsAppointment _Appointment = clsVisit.GetAppointmentInfoByVisitID(VisitID);

            frmFillVisitDetails frm = new frmFillVisitDetails(_Appointment.AppointmentID, (int)enMode.Read);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int VisitID = (int)dgvPrescription.CurrentRow.Cells["VisitID"].Value;

            // 2. البحث عن الـ AppointmentID المرتبط بالزيارة دي (لأن شاشتك بتفتح بيه)
            clsVisit visit = clsVisit.Find(VisitID);

            if (visit != null)
            {
                // 3. فتح شاشة الإضافة/التعديل
                frmFillVisitDetails frm = new frmFillVisitDetails(visit.AppointmentID, 1);//1 is for update mode

                // 4. الحركة السحرية: ننده ميثود تخلي الروشتة هي اللي مفتوحة والزيارة مقفولة
                frm.SetOnlyPrescriptionMode();

                frm.ShowDialog();
                frmListPrescriptions_Load(null, null);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this prescription?", "Confirm Delete",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int PrescriptionID = (int)dgvPrescription.CurrentRow.Cells["PrescriptionID"].Value;

                if (clsPrescription.DeletePrescription(PrescriptionID))
                {
                    MessageBox.Show("Prescription Deleted Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListPrescriptions_Load(null,null); 
                }
                else
                {
                    MessageBox.Show("Error: Could not delete prescription.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}