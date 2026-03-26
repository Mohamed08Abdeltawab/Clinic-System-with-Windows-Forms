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
        static DataTable _dtAllPrescription;
        DataTable _dtPrescription;
        public frmListPrescriptions()
        {
            InitializeComponent();
        }

        private void _RefreshPrescriptionList()
        {
            _dtAllPrescription = clsPrescription.GetAllPrescriptions();
            _dtPrescription = _dtAllPrescription.DefaultView.ToTable(false, "PrescriptionID", "VisitID", "MedicineID", "Quantity");

            dgvPrescription.DataSource = _dtPrescription;
            lblRecordsCount.Text = dgvPrescription.Rows.Count.ToString();
        }

        private void frmListPrescriptions_Load(object sender, EventArgs e)
        {
            _RefreshPrescriptionList();
            dgvPrescription.DataSource = _dtPrescription;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPrescription.Rows.Count.ToString();

            if (dgvPrescription.Rows.Count > 0)
            {
                dgvPrescription.Columns[0].HeaderText = "Prescription ID";
                dgvPrescription.Columns[0].Width = 110;


                dgvPrescription.Columns[1].HeaderText = "Visit ID";
                dgvPrescription.Columns[1].Width = 110;

                dgvPrescription.Columns[2].HeaderText = "Medicine ID";
                dgvPrescription.Columns[2].Width = 110;

                dgvPrescription.Columns[3].HeaderText = "Quantity";
                dgvPrescription.Columns[3].Width = 200;

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
                case "Prescription ID":
                    FilterColumn = "PrescriptionID";
                    break;

                case "Visit ID":
                    FilterColumn = "VisitID";
                    break;

                case "Medicine ID":
                    FilterColumn = "MedicineID";
                    break;

                case "Quantity":
                    FilterColumn = "Quantity";
                    break;

            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
                _dtPrescription.DefaultView.RowFilter = "";
            else
                _dtPrescription.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtPrescription.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
