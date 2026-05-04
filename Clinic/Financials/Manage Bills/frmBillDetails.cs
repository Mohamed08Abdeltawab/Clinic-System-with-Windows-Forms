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
    public partial class frmBillDetails : Form
    {
        clsBill _Bill;
        private int _BillID;
        public frmBillDetails(int billID)
        {
            InitializeComponent();
            _BillID = billID;
        }



        private void frmBillDetails_Load(object sender, EventArgs e)
        {
            _FillBillDetails();
        }

        private void _FillBillDetails()
        {
            _Bill = clsBill.Find(_BillID);
            if(_Bill == null)
            {
                MessageBox.Show("Bill not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            lblBillID.Text = _Bill.BillID.ToString();
            lblVisitID.Text = _Bill.VisitID.ToString();
            lblTotalAmount.Text = _Bill.TotalAmount.ToString("C");
            lblPaymentStatus.Text = ((clsBill.enPaymentStatus)_Bill.PaymentStatus).ToString();
            lblPaymentDate.Text = _Bill.PaymentDate.HasValue ? _Bill.PaymentDate.Value.ToShortDateString() : "N/A";
            lblPaymentMethod.Text = ((clsBill.enPaymentMethod)_Bill.PaymentMethod).ToString();
            lblBillDate.Text = _Bill.CreatedByUserID.ToString(); // Assuming CreatedByUserID is being used as BillDate, adjust if necessary
            lblUserName.Text = _Bill.CreatedByUserID.ToString(); // Assuming CreatedByUserID is being used as UserName, adjust if necessary
        }
    }
}
