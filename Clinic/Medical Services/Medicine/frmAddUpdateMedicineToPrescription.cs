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

namespace Clinic.Medical_Services.Medicine
{
    public partial class frmAddUpdateMedicineToPrescription : Form
    {
        // 1. تعريف الـ Mode والـ Delegate
        public enum enMode { AddNew = 0, Update = 1, Read = 2};
        private enMode _Mode = enMode.AddNew;

        public delegate void DataBackEventHandler(object sender, clsPrescriptionItem Item);
        public event DataBackEventHandler DataBack;

        private clsPrescriptionItem _Item;

        // 2. Constructor لحالة الإضافة
        public frmAddUpdateMedicineToPrescription()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        // 3. Constructor لحالة التعديل أو القراءة
        public frmAddUpdateMedicineToPrescription(clsPrescriptionItem Item, enMode Mode)
        {
            InitializeComponent();
            _Item = Item;
            _Mode = Mode;
        }

        private void _FillMedicinesComboBox()
        {
            DataTable dt = clsMedicine.GetAllMedicines();
            foreach (DataRow row in dt.Rows)
            {
                cbMedicines.Items.Add(row["MedicineName"].ToString());
            }
        }

        private void _LoadData()
        {
            // 1. دائماً نملأ قائمة الأدوية أولاً
            _FillMedicinesComboBox();

            switch (_Mode)
            {
                case enMode.AddNew:
                    lblTitle.Text = "Add Medicine To Prescription";
                    _Item = new clsPrescriptionItem();
                    // القيمة الافتراضية للكمية
                    NUDQuantity.Value = 1;
                    break;

                case enMode.Update:
                    lblTitle.Text = "Update Medicine In Prescription";
                    _FillFieldsWithData();
                    break;

                case enMode.Read:
                    lblTitle.Text = "Medicine Details (Read Only)";
                    _FillFieldsWithData();

                    // 2. قفل كافة العناصر باستخدام الكلاس العالمي
                    clsGlobal.SetControlsReadOnly(this, true);

                    // 3. إخفاء زر الحفظ وتحريك زر الإغلاق لمكانه (لشكل أفضل)
                    btnSave.Visible = false;
                    btnClose.Location = btnSave.Location;
                    break;
            }
        }

        private void _FillFieldsWithData()
        {
            if (_Item == null) return;

            // اختيار الدواء بناءً على الاسم (تأكد أن الأسماء مطابقة تماماً)
            cbMedicines.SelectedIndex = cbMedicines.FindStringExact(_Item.MedicineName);

            // تعبئة الكمية في الـ NumericUpDown
            NUDQuantity.Value = _Item.Quantity;

            txtDosage.Text = _Item.Dosage;
            txtInstructions.Text = _Item.Instructions;

            // عرض السعر (اختياري لو حابب يظهر في وضع العرض)
            clsMedicine medicine = clsMedicine.Find(_Item.MedicineID);
            if (medicine != null)
            {
                lblMedicinePrice.Text = medicine.Price.ToString() + " $";
            }
        }

        private void frmAddUpdateMedicineToPrescription_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void cbMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            // جلب بيانات الدواء عند الاختيار لعرض السعر
            clsMedicine medicine = clsMedicine.Find(cbMedicines.Text);
            if (medicine != null)
            {
                lblMedicinePrice.Text = medicine.Price.ToString() + " $";
                _Item.MedicineID = medicine.MedicineID;
                _Item.MedicineName = medicine.MedicineName;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // التحقق من المدخلات (Validation)
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // تعبئة الكائن بالبيانات الجديدة
            _Item.Quantity = (int)NUDQuantity.Value;
            _Item.Dosage = txtDosage.Text.Trim();
            _Item.Instructions = txtInstructions.Text.Trim();

            // إرسال البيانات للخلف
            DataBack?.Invoke(this, _Item);

            this.Close();
        }

        private void cbMedicines_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbMedicines.Text))
            {
                e.Cancel = true; 
                errorProvider1.SetError(cbMedicines, "Must Select Medicine!");
                return;
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbMedicines, "");
            }
        }
    }
}