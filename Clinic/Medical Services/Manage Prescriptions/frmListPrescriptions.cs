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
    }
}