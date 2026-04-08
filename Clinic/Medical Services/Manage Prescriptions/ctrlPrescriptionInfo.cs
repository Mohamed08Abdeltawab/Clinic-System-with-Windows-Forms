using Clinic.Global_Classes;
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
    public partial class ctrlPrescriptionInfo : UserControl
    {
        private int _PrescriptionID = -1;
        private clsPrescription _Prescription;

        public clsPrescription SelectedPrescription => _Prescription;
        public int PrescriptionID => _PrescriptionID;

        private int _Mode = 0;//0:update, 1:read
        public int Mode
        {
            get { return _Mode; }
            set
            {
                _Mode = value;
                _HandleModeChange();
            }
        }

        public ctrlPrescriptionInfo()
        {
            InitializeComponent();
        }

        public void _ResetDefaultValues()
        {
            _PrescriptionID = -1;
            _Prescription = null;

            lblPrescriptionID.Text = "[???]";
            lblVisitID.Text = "[???]";
            dtpPrescriptionDate.Value = DateTime.Now;
            txtPrescriptionNotes.Text = "";

            // تنظيف الجريد
            dgvMedicines.DataSource = null;
        }

        private void _FillPrescriptionData()
        {
            _PrescriptionID = _Prescription.PrescriptionID;
            lblPrescriptionID.Text = _Prescription.PrescriptionID.ToString();
            lblVisitID.Text = _Prescription.VisitID.ToString();
            dtpPrescriptionDate.Value = _Prescription.PrescriptionDate;
            txtPrescriptionNotes.Text = string.IsNullOrEmpty(_Prescription.Notes) ? "No Notes" : _Prescription.Notes;

            _RefreshGrid();
        }

        private void _HandleModeChange()
        {
            // إذا كان الوضع قراءة، نجعل التكست بوكس للقراءة فقط
            bool isReadOnly = (_Mode == 1);
            dgvMedicines.ReadOnly = isReadOnly;
            txtPrescriptionNotes.ReadOnly = isReadOnly;
            dtpPrescriptionDate.Enabled = !isReadOnly;
        }

        private void _RefreshGrid()
        {
            dgvMedicines.DataSource = null;
            // حماية في حالة كانت القائمة فارغة
            if (_Prescription == null || _Prescription.ItemsList == null)
                return;

            dgvMedicines.DataSource = _Prescription.ItemsList;

            if (dgvMedicines.Rows.Count > 0)
            {
                dgvMedicines.Columns["ItemID"].Visible = false;
                dgvMedicines.Columns["PrescriptionID"].Visible = false;

                dgvMedicines.Columns["MedicineID"].HeaderText = "ID";
                dgvMedicines.Columns["MedicineID"].Width = 70;

                dgvMedicines.Columns["MedicineName"].HeaderText = "Medicine Name";
                dgvMedicines.Columns["MedicineName"].Width = 150;

                dgvMedicines.Columns["Quantity"].HeaderText = "Qty";
                dgvMedicines.Columns["Quantity"].Width = 70;

                dgvMedicines.Columns["Dosage"].HeaderText = "Dosage";
                dgvMedicines.Columns["Dosage"].Width = 220;

                dgvMedicines.Columns["Instructions"].HeaderText = "Instructions";
                dgvMedicines.Columns["Instructions"].Width = 220;
            }
        }

        public void LoadPrescriptionInfoByPrescriptionID(int PrescriptionID)
        {
            _Prescription = clsPrescription.Find(PrescriptionID);

            if (_Prescription == null)
            {
                MessageBox.Show("No Prescription with ID = " + PrescriptionID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            _FillPrescriptionData();
        }

        public void LoadPrescriptionInfoByVisitID(int VisitID)
        {
            _Prescription = clsPrescription.FindByVisitID(VisitID);

            if (_Prescription == null)
            {
                MessageBox.Show("No Prescription with Visit ID = " + VisitID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            _FillPrescriptionData();
        }
    }
}