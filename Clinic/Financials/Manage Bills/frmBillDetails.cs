using Clinic.Global_Classes;
using Clinicbusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        clsVisit _Visit;
        public frmBillDetails(int billID)
        {
            InitializeComponent();
            _BillID = billID;
            _LoadClasses();
            _LoadBillData();
        }

        PrintDocument printDoc = new PrintDocument();

        private void _LoadClasses()
        {
            _Bill = clsBill.Find(_BillID);
            if (_Bill == null)
            {
                MessageBox.Show("Bill not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            _Visit = clsVisit.Find(_Bill.VisitID);
            _PatientPerson = clsPerson.FindByPatientID(clsAppointment.Find(_Visit.AppointmentID).PatientID);
        }

        private void _LoadBillData()
        {
            if (_Bill == null)
            {
                MessageBox.Show("Bill not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
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

        private void PrintBillPage(object sender, PrintPageEventArgs e)
        {
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show("No printers found!", "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Graphics g = e.Graphics;

            // Fonts setup (Similar to your UI design)
            Font fontTitle = new Font("Segoe UI", 22, FontStyle.Bold);
            Font fontLabel = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontValue = new Font("Segoe UI", 14, FontStyle.Italic);
            Font fontFooter = new Font("Segoe UI", 12, FontStyle.Regular);

            float pageWidth = e.PageBounds.Width;
            float margin = 40;
            float y = 40;
            float rowHeight = 40; // Increased for better readability

            StringFormat left = new StringFormat() { Alignment = StringAlignment.Near };
            StringFormat center = new StringFormat() { Alignment = StringAlignment.Center };

            // ===== 1. Header (Clinic System Title) =====
            g.DrawString("Clinic System Bill", fontTitle, Brushes.DarkBlue, pageWidth / 2, y, center);
            y += rowHeight + 20;

            // Sub-header for Bill Number and Date
            g.DrawString($"Bill Number: #{_Bill.BillID.ToString("D6")}", fontLabel, Brushes.Black, margin, y, left);
            g.DrawString($"Date: {DateTime.Now:yyyy-MM-dd HH:mm}", fontLabel, Brushes.Black, pageWidth - (margin * 7), y, left);
            y += rowHeight;

            g.DrawLine(new Pen(Color.Black, 2), margin, y, pageWidth - margin, y);
            y += 20;

            // ===== 2. Bill Details (Drawing labels and values as shown in your screen) =====

            // List of fields to print based on your UI
            var billData = new Dictionary<string, string>
    {
        { "Patient Name:", _PatientPerson.FullName },
        { "Visit ID:", _Bill.VisitID.ToString() },
        { "Payment Status:", (_Bill.PaymentStatus == 1 ? "Paid" : "Unpaid") },
        { "Payment Method:", (_Bill.PaymentMethod == 1 ? "Cash" : "Card") },
        { "Payment Date:", _Bill.PaymentDate?.ToString("yyyy-MM-dd") ?? "N/A" },
        { "Discount:", $"{_Bill.Discount:C2}" },
        { "Tax Amount:", $"{_Bill.TaxAmount:C2}" },
        { "Total Amount:", $"{_Bill.TotalAmount:C2}" }
    };

            foreach (var item in billData)
            {
                // Draw Label (Blue-ish color to match your UI)
                g.DrawString(item.Key, fontLabel, Brushes.SteelBlue, margin + 20, y, left);

                // Draw Value (Italic and Black)
                g.DrawString(item.Value, fontValue, Brushes.Black, margin + 250, y, left);

                y += rowHeight;
            }

            y += 20;
            g.DrawLine(new Pen(Color.Gray, 1), margin, y, pageWidth - margin, y);
            y += 20;

            // ===== 3. Total Cost Section =====
            Font fontTotal = new Font("Segoe UI", 18, FontStyle.Bold);
            g.DrawString("Total Cost:", fontTotal, Brushes.DarkRed, margin + 20, y, left);
            g.DrawString($"{_Bill.TotalAmount + _Bill.TaxAmount - _Bill.Discount:C2}", fontTotal, Brushes.DarkRed, margin + 250, y, left);

            y += rowHeight + 50;

            // ===== 4. Footer =====
            g.DrawLine(new Pen(Color.Black, 2), margin, y, pageWidth - margin, y);
            y += 10;
            g.DrawString("Thank you for choosing our clinic", fontFooter, Brushes.Gray, pageWidth / 2, y, center);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Unsubscribe from previous events to avoid multiple printing
            printDoc.PrintPage -= PrintBillPage;
            printDoc.PrintPage += PrintBillPage;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDoc;

            // Setting preview window size
            ((Form)preview).WindowState = FormWindowState.Maximized;
            preview.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
