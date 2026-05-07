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

        clsPerson _PatientPerson;
        public frmBillDetails(int billID)
        {
            InitializeComponent();
            _BillID = billID;
            _LoadBillData();
        }

        //add new bill c
        public frmBillDetails(clsBill newBill)
        {
            InitializeComponent();
            _Bill = newBill;
            _LoadBillData();
        }

        private void _LoadBillData()
        {
            _Bill = clsBill.Find(_BillID);
            if(_Bill == null)
            {
                MessageBox.Show("Bill not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            // Use properties instead of List indexes (Type-Safe)
            
            _PatientPerson = clsPerson.FindByPatientID(clsAppointment.Find(_Bill.VisitID).PatientID);
            lblPatientName.Text = _PatientPerson != null ? _PatientPerson.FullName : "N/A";
            lblBillID.Text = _Bill.BillID.ToString();
            lblVisitID.Text = _Bill.VisitID.ToString();
            lblTotalAmount.Text = _Bill.TotalAmount.ToString("C");
            lblPaymentStatus.Text = _Bill.PaymentStatus.ToString();
            lblBillDate.Text = _Bill.PaymentDate.HasValue ? _Bill.PaymentDate.Value.ToShortDateString() : "N/A";
            lblDiscount.Text = _Bill.Discount.ToString("C");
            lblPaymentMethod.Text = ((clsBill.enPaymentMethod)_Bill.PaymentMethod).ToString();
            lblPaymentStatus.Text = ((clsBill.enPaymentStatus)_Bill.PaymentStatus).ToString();
            lblTaxAmount.Text = _Bill.TaxAmount.ToString("C");
            lblTotalCost.Text = (_Bill.TotalAmount + _Bill.TaxAmount - _Bill.Discount).ToString("C");
            lblPaymentDate.Text = _Bill.PaymentDate.HasValue ? _Bill.PaymentDate.Value.ToShortDateString() : "N/A";
            lblUserName.Text = _Bill.UserInfo?.UserName ?? "N/A";
        }

    }
}
