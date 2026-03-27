using Clinic.Doctor;
using Clinic.Global_Classes;
using Clinic.Medical_Services.Medicine;
using Clinic.Patient;
using Clinic.People.Controls;
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

namespace Clinic.Medical_Services.Visit
{
    public partial class frmFillVisitDetails : Form
    {
        private enum enMode { AddNew = 0, Update = 1, Read = 2 };
        private enMode _Mode = enMode.AddNew;
        private int _AppointmentID;
        private clsVisit _Visit;
        private clsPrescription _Prescription;

        public frmFillVisitDetails(int AppointmentID, int Mode)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _Mode = (enMode)Mode;
        }

        private void _RefreshGrid()
        {
            // ربط الـ List الموجودة في كائن الروشتة بالـ DataGridView
            // ملاحظة: الأفضل استخدام BindingSource أو تحويل الـ List لـ DataTable مؤقتاً للعرض
            dgvMedicines.DataSource = null;
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

        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Set Visit Details";
                _Visit = new clsVisit();
                _Prescription = new clsPrescription(); // تهيئة روشتة جديدة

                lblVisitID.Text = "[???]";
                lblPrescriptionID.Text = "[???]";
                dtpDateTime.Value = DateTime.Now;

                var appointment = clsAppointment.Find(_AppointmentID);
                if (appointment == null) { this.Close(); return; }

                lblPatientID.Text = appointment.PatientID.ToString();
                lblDoctorID.Text = appointment.DoctorID.ToString();
                lblAppointmentID.Text = _AppointmentID.ToString();
            }
            else
            {
                _Visit = clsVisit.FindByAppointmentID(_AppointmentID);
                if (_Visit == null) { this.Close(); return; }

                // تحميل الروشتة التابعة لهذه الزيارة
                _Prescription = clsPrescription.Find(_Visit.VisitID); // تأكد من وجود ميثود Find تأخذ VisitID
                if (_Prescription == null) _Prescription = new clsPrescription();

                lblVisitID.Text = _Visit.VisitID.ToString();
                lblPrescriptionID.Text = _Prescription.PrescriptionID == -1 ? "[???]" : _Prescription.PrescriptionID.ToString();
                txtDiagnosis.Text = _Visit.Diagnosis;
                txtNotes.Text = _Visit.Notes;
                txtPrescriptionNotes.Text = _Prescription.Notes; // عرض ملاحظات الروشتة
                dtpDateTime.Value = _Visit.VisitDate;

                lblPatientID.Text = _Visit.AppointmentInfo.PatientID.ToString();
                lblDoctorID.Text = _Visit.AppointmentInfo.DoctorID.ToString();
                lblAppointmentID.Text = _Visit.AppointmentID.ToString();

                _RefreshGrid();
            }

            if (_Mode == enMode.Read)
            {
                btnSave.Visible = false;
                btnAddNewMedicine.Enabled = false;
                clsGlobal.SetControlsReadOnly(this, true); // استخدام الميثود العالمية
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) return;

            // 1. تعبئة بيانات الزيارة
            _Visit.AppointmentID = _AppointmentID;
            _Visit.VisitDate = dtpDateTime.Value;
            _Visit.Diagnosis = txtDiagnosis.Text.Trim();
            _Visit.Notes = txtNotes.Text.Trim();

            // 2. حفظ الزيارة أولاً (لأن الروشتة تحتاج VisitID)
            if (_Visit.Save())
            {
                // 3. تعبئة بيانات الروشتة برقم الزيارة الجديد
                _Prescription.VisitID = _Visit.VisitID;
                _Prescription.PrescriptionDate = DateTime.Now;
                _Prescription.Notes = txtPrescriptionNotes.Text.Trim();

                // 4. حفظ الروشتة وأدويتها (تمت برمجتها داخل Save في clsPrescription)
                if (_Prescription.Save())
                {
                    lblVisitID.Text = _Visit.VisitID.ToString();
                    lblPrescriptionID.Text = _Prescription.PrescriptionID.ToString();
                    _Mode = enMode.Update;
                    MessageBox.Show("Visit and Prescription saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Visit saved, but failed to save prescription.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Failed to save visit details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNewMedicine_Click(object sender, EventArgs e)
        {
            frmAddUpdateMedicineToPrescription frm = new frmAddUpdateMedicineToPrescription();

            frm.DataBack += (s, Item) => {
                // إضافة الدواء للقائمة وتحديث الـ Grid في الميموري
                _Prescription.ItemsList.Add(Item);
                _RefreshGrid();
            };

            frm.ShowDialog();
        }

        private void frmFillVisitDetails_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        // ... باقى الـ Events (LinkClicked, Close, إلخ) تظل كما هي ...



        private void llPatientInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblPatientID.Text, out int patientID))
            {
                frmPatientInfo frm = new frmPatientInfo(patientID);

                if (_Mode == enMode.Read)
                {
                    frm.IsReadOnly = true;
                }

                frm.ShowDialog();
            }
        }

        private void llDoctorInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblPatientID.Text, out int doctorID))
            {
                frmDoctorInfo frm = new frmDoctorInfo(doctorID);

                if (_Mode == enMode.Read)
                {
                    frm.IsReadOnly = true;
                }

                frm.ShowDialog();
            }
        }

        private void txtDiagnosis_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiagnosis.Text.Trim()))
            {
                errorProvider1.SetError(txtDiagnosis, "");
            }
            else
            {
                errorProvider1.SetError(txtDiagnosis, "Please enter the diagnosis.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrecriptionNext_Click(object sender, EventArgs e)
        {
            // 1. لو إحنا في حالة القراءة فقط، اسمح له يتنقل بين الـ Tabs عادي
            if (_Mode == enMode.Read)
            {
                tcVisitInfo.SelectedTab = tcVisitInfo.TabPages["tpPrescriptionInfo"];
                return;
            }

            // 2. التحقق من صحة بيانات الزيارة (التشخيص مهم جداً قبل الروشتة)
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fill the Diagnosis before moving to Prescription.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. التعامل مع حالة الـ Update
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpPrescriptionInfo.Enabled = true; // تفعيل صفحة الروشتة
                tcVisitInfo.SelectedTab = tcVisitInfo.TabPages["tpPrescriptionInfo"];
                return;
            }

            // 4. حالة الـ Add New
            // هنا بنتأكد إننا مش بنعمل "روشتة مكررة" لو الزيارة موجودة فعلاً (حالة نادرة في الشاشة دي)
            if (_Visit.VisitID != -1 && clsPrescription.IsPrescriptionExistByVisitID(_Visit.VisitID))
            {
                MessageBox.Show("This visit already has a prescription.", "Existing Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // الانتقال لصفحة الروشتة بنجاح
                btnSave.Enabled = true;
                tpPrescriptionInfo.Enabled = true;
                tcVisitInfo.SelectedTab = tcVisitInfo.TabPages["tpPrescriptionInfo"];
            }
        }
    }
}

