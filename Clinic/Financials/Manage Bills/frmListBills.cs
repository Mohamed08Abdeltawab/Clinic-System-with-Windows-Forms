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

namespace Clinic.Financials.Manage_Bills
{
    public partial class frmListBills : Form
    {
        public frmListBills()
        {
            InitializeComponent();
        }
        static DataTable _dtAllBills;
        DataTable _dtBills;
        private void _RefreshBillList()
        {
            _dtAllBills = clsBill.GetAllBills();
            _dtBills = _dtAllBills.DefaultView.ToTable(false, "BillID", "VisitID", "PatientName", "TotalAmount", "StatusCaption", "PaymentMethodCaption", "PaymentDate");

            dgvBills.DataSource = _dtBills;
            lblRecordsCount.Text = dgvBills.Rows.Count.ToString();
        }

        private void frmListBills_Load(object sender, EventArgs e)
        {
            _RefreshBillList();
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvBills.Rows.Count.ToString();

            if (dgvBills.Rows.Count > 0)
            {
                dgvBills.Columns[0].HeaderText = "Bill ID";
                dgvBills.Columns[0].Width = 110;


                dgvBills.Columns[1].HeaderText = "Visit ID";
                dgvBills.Columns[1].Width = 110;

                dgvBills.Columns[2].HeaderText = "Patient Name";
                dgvBills.Columns[2].Width = 260;

                dgvBills.Columns[3].HeaderText = "Total Amount";
                dgvBills.Columns[3].Width = 130;

                dgvBills.Columns[4].HeaderText = "Status";
                dgvBills.Columns[4].Width = 120;

                dgvBills.Columns[5].HeaderText = "Payment Method";
                dgvBills.Columns[5].Width = 150;

                dgvBills.Columns[6].HeaderText = "Payment Date";
                dgvBills.Columns[6].Width = 200;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Payment Status" && cbFilterBy.Text != "Payment Method");

            if (txtFilterValue.Visible)
            {
                cbPaymentMethod.Visible = false;
                cbPaymentStatus.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else if (cbFilterBy.Text == "Payment Status")
            {
                cbPaymentStatus.SelectedIndex = 0;
                cbPaymentStatus.Visible = true;
                txtFilterValue.Visible = false;
                cbPaymentMethod.Visible = false;
            }
            else if (cbFilterBy.Text == "Payment Method")
            {
                cbPaymentMethod.SelectedIndex = 0;
                cbPaymentMethod.Visible = true;
                txtFilterValue.Visible = false;
                cbPaymentStatus.Visible = false;
            }
            else
            {
                cbPaymentMethod.Visible = false;
                cbPaymentStatus.Visible = false;
            }
        }

        private void cbPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbPaymentStatus.Text;

            if (FilterValue == "All")
            {
                _dtBills.DefaultView.RowFilter = "";
            }
            else
                _dtBills.DefaultView.RowFilter = string.Format("[StatusCaption] LIKE '{0}'", FilterValue);

            lblRecordsCount.Text = dgvBills.Rows.Count.ToString();

        }

        private void cbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbPaymentMethod.Text;

            if (FilterValue == "All")
            {
                _dtBills.DefaultView.RowFilter = "";
            }
            else
                _dtBills.DefaultView.RowFilter = string.Format("[PaymentMethodCaption] LIKE '{0}'", FilterValue);

            lblRecordsCount.Text = dgvBills.Rows.Count.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Bill ID":
                    FilterColumn = "BillID";
                    break;

                case "Visit ID":
                    FilterColumn = "VisitID";
                    break;

                case "Patient Name":
                    FilterColumn = "PatientName";
                    break;

            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtBills.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtBills.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "BillID" || FilterColumn == "VisitID")
                //in this case we deal with integer not string.
                _dtBills.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtBills.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtBills.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Bill ID" || cbFilterBy.Text == "Visit ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
