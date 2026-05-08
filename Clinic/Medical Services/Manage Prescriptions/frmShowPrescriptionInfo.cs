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

namespace Clinic.Medical_Services.Manage_Prescriptions
{
    public partial class frmShowPrescriptionInfo : Form
    {
        int _PrescriptionID = -1;
        clsPrescription _Prescription;
        PrintDocument printDoc = new PrintDocument();

        public frmShowPrescriptionInfo(int PrescriptionID)
        {
            InitializeComponent();
            _PrescriptionID = PrescriptionID;
            ctrlPrescriptionInfo1.LoadPrescriptionInfoByPrescriptionID(PrescriptionID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrintPrescriptionPage(object sender, PrintPageEventArgs e)
        {
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show("Don't have any installed printers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Graphics g = e.Graphics;

            // Same Fonts as your previous code
            Font fontTitle = new Font("Tahoma", 24, FontStyle.Bold);
            Font fontHeader = new Font("Tahoma", 18, FontStyle.Bold);
            Font fontBody = new Font("Tahoma", 16);
            Font fontSmallItalic = new Font("Tahoma", 12, FontStyle.Italic);

            float pageWidth = e.PageBounds.Width;
            float margin = 40;
            float y = 40;
            float rowHeight = 40;

            StringFormat right = new StringFormat() { Alignment = StringAlignment.Far };
            StringFormat center = new StringFormat() { Alignment = StringAlignment.Center };
            StringFormat left = new StringFormat() { Alignment = StringAlignment.Near };

            // 1. Get all data from Business Layer using the FullInfo method
            // Assuming _PrescriptionID is available in the form
            clsPrescription prescription = clsPrescription.FindFullInfo(_PrescriptionID);

            if (prescription == null) return;

            // ===== Header (Clinic Name) =====
            g.DrawString("Clinic System", fontTitle, Brushes.Black, pageWidth / 2, y, center);
            y += rowHeight + 30;

            // Patient and Doctor Info (Left and Right setup)
            g.DrawString($"Patient: {prescription.PatientName}", fontBody, Brushes.Black, pageWidth - margin, y, right);
            g.DrawString($"Doctor: {prescription.DoctorName}", fontBody, Brushes.Black, margin, y, left);
            y += rowHeight;

            g.DrawString($"Date: {prescription.PrescriptionDate:yyyy-MM-dd}", fontBody, Brushes.Black, pageWidth - margin, y, right);
            g.DrawString($"Diagnosis: {prescription.Diagnosis}", fontBody, Brushes.Black, margin, y, left);
            y += rowHeight + 10;

            g.DrawLine(Pens.Black, margin, y, pageWidth - margin, y);
            y += 20;

            // ===== Medicines Table Header =====
            g.DrawString("Medicine", fontHeader, Brushes.Black, pageWidth - margin, y, right);
            g.DrawString("Quantity", fontHeader, Brushes.Black, margin + 400, y, left);
            g.DrawString("Dosage", fontHeader, Brushes.Black, margin, y, left);

            y += rowHeight;
            g.DrawLine(Pens.Gray, margin, y, pageWidth - margin, y);
            y += 15;

            // ===== Draw Medicine Items =====
            foreach (var item in prescription.ItemsList)
            {
                // Draw Main Item Info
                g.DrawString(item.MedicineName, fontBody, Brushes.Black, pageWidth - margin, y, right);
                g.DrawString(item.Quantity.ToString(), fontBody, Brushes.Black, margin + 450, y, left);
                g.DrawString(item.Dosage, fontBody, Brushes.Black, margin, y, left);

                y += rowHeight - 5;

                // Draw Instructions (If exists) in smaller font
                if (!string.IsNullOrEmpty(item.Instructions))
                {
                    g.DrawString($"Instructions: {item.Instructions}", fontSmallItalic, Brushes.DarkSlateGray, pageWidth - margin - 20, y, right);
                    y += rowHeight - 10;
                }

                y += 10; // Space between items
            }

            // ===== Footer =====
            y = e.PageBounds.Height - 100; // Position at bottom
            g.DrawLine(Pens.Black, margin, y, pageWidth - margin, y);
            y += 10;
            g.DrawString("Get Well Soon", fontBody, Brushes.Black, pageWidth / 2, y, center);
        }

        private void btnPrintPrescription_Click(object sender, EventArgs e)
        {
            if(_PrescriptionID == -1)
            {
                MessageBox.Show("Invalid Prescription ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            printDoc.PrintPage -= PrintPrescriptionPage; // Avoid multiple subscriptions
            printDoc.PrintPage += new PrintPageEventHandler(PrintPrescriptionPage);

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDoc;
            ((Form)preview).WindowState = FormWindowState.Maximized;
            preview.ShowDialog();
        }
    }
}
