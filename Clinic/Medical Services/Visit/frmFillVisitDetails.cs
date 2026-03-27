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
            // 1. التحقق من وجود زيارة سابقة (حماية الداتا)
            if (_Mode == enMode.AddNew && clsVisit.IsVisitExistByAppointmentID(_AppointmentID))
            {
                MessageBox.Show("This appointment already has a visit recorded.",
                                "Existing Visit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Set Visit Details";
                _Visit = new clsVisit();
                _Prescription = new clsPrescription();

                lblVisitID.Text = "[???]";
                lblPrescriptionID.Text = "[???]";
                dtpDateTime.Value = DateTime.Now;

                var appointment = clsAppointment.Find(_AppointmentID);
                if (appointment == null) { this.Close(); return; }

                lblPatientID.Text = appointment.PatientID.ToString();
                lblDoctorID.Text = appointment.DoctorID.ToString();
                lblAppointmentID.Text = _AppointmentID.ToString();

                // --- التعديل الجوهري هنا لـ AddNew ---
                tpPrescriptionInfo.Enabled = false;   // مقفول لحد ما يحفظ الزيارة
                btnSaveandNext.Enabled = true;        // شغال لأنه هو اللي هيعمل Save للزيارة وينقله
                btnSave.Enabled = false;              // زرار الروشتة (الخارجي) مطفي حالياً
            }
            else
            {
                // وضعية الـ Update والـ Read
                lblTitle.Text = (_Mode == enMode.Update) ? "Edit Visit Details" : "Visit Details";

                _Visit = clsVisit.FindByAppointmentID(_AppointmentID);
                if (_Visit == null) { this.Close(); return; }

                _Prescription = clsPrescription.Find(_Visit.VisitID);
                if (_Prescription == null) _Prescription = new clsPrescription();

                // تعبئة البيانات (نفس كودك السليم)
                lblVisitID.Text = _Visit.VisitID.ToString();
                lblPrescriptionID.Text = _Prescription.PrescriptionID == -1 ? "[???]" : _Prescription.PrescriptionID.ToString();
                txtDiagnosis.Text = _Visit.Diagnosis;
                txtNotes.Text = _Visit.Notes;
                txtPrescriptionNotes.Text = _Prescription.Notes;
                dtpDateTime.Value = _Visit.VisitDate;

                lblPatientID.Text = _Visit.AppointmentInfo.PatientID.ToString();
                lblDoctorID.Text = _Visit.AppointmentInfo.DoctorID.ToString();
                lblAppointmentID.Text = _Visit.AppointmentID.ToString();

                _RefreshGrid();

                // --- التعديل الجوهري هنا لـ Update/Read ---
                tpPrescriptionInfo.Enabled = true;    // مفتوح لأن الزيارة موجودة
                btnSaveandNext.Enabled = true;        // شغال للتنقل
                btnSave.Enabled = (_Mode == enMode.Update); // شغال في التعديل ومطفي في القراءة

            }

            // تعامل خاص مع الـ Read Mode
            if (_Mode == enMode.Read)
            {
                btnSave.Visible = false;
                btnAddNewMedicine.Enabled = false;
                clsGlobal.SetControlsReadOnly(this, true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // التأكد إن فيه زيارة محفوظة أصلاً (لأن الروشتة Foreign Key للزيارة)
            if (_Visit.VisitID == -1)
            {
                MessageBox.Show("Please save visit details first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // تعبئة بيانات الروشتة
            _Prescription.VisitID = _Visit.VisitID;
            _Prescription.PrescriptionDate = DateTime.Now;
            _Prescription.Notes = txtPrescriptionNotes.Text.Trim();

            // حفظ الروشتة ومعها قائمة الأدوية (ItemsList)
            if (_Prescription.Save())
            {
                lblPrescriptionID.Text = _Prescription.PrescriptionID.ToString();
                MessageBox.Show("Prescription saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                //this.Close(); // إغلاق الشاشة بعد الحفظ النهائي
            }
            else
            {
                MessageBox.Show("Failed to save prescription.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (int.TryParse(lblDoctorID.Text, out int doctorID))
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

        private void removeMedicineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.CurrentRow == null) return;
            if (MessageBox.Show("Are you sure you want to remove this medicine?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // مسح العنصر من الـ List الأصلية باستخدام الـ Index بتاع السطر
                _Prescription.ItemsList.RemoveAt(dgvMedicines.CurrentRow.Index);

                // إعادة تحديث الـ Grid
                _RefreshGrid();
            }
        }

        private void ShowMedicineInfotoolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. التأكد إن فيه سطر مختار
            if (dgvMedicines.CurrentRow == null) return;

            // 2. جلب الكائن المختار من الـ List (بناءً على الـ Index بتاع السطر)
            clsPrescriptionItem SelectedItem = _Prescription.ItemsList[dgvMedicines.CurrentRow.Index];

            // 3. فتح نفس الشاشة اللي عملناها، بس بتبعت لها الـ Mode = Read
            // هنا إنت بتبعت الـ Item والـ Mode، والشاشة هتتولى قفل كل الـ Controls أوتوماتيك
            frmAddUpdateMedicineToPrescription frm = new frmAddUpdateMedicineToPrescription(SelectedItem, frmAddUpdateMedicineToPrescription.enMode.Read);

            // مش محتاج تشترك في الـ DataBack هنا لأن مفيش حفظ هيحصل
            frm.ShowDialog();

        }

        private void EditMedicinetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.CurrentRow == null) return;
            // 1. جلب الدواء المختار من الـ List بناءً على السطر المحدد في الـ Grid
            clsPrescriptionItem SelectedItem = _Prescription.ItemsList[dgvMedicines.CurrentRow.Index];

            // 2. فتح الشاشة في وضع الـ Update وإرسال الكائن
            frmAddUpdateMedicineToPrescription frm = new frmAddUpdateMedicineToPrescription(SelectedItem, frmAddUpdateMedicineToPrescription.enMode.Update);

            // 3. الاشتراك في الـ Event لتحديث البيانات بعد التعديل
            frm.DataBack += (s, UpdatedItem) => {
                _RefreshGrid(); // تحديث العرض فوراً
            };

            frm.ShowDialog();
        }

        private void btnSaveandNext_Click(object sender, EventArgs e)
        {
            // 1. التحقق من البيانات (التشخيص إلزامي)
            if (!ValidateChildren()) return;

            // 2. تعبئة بيانات الزيارة
            _Visit.AppointmentID = _AppointmentID;
            _Visit.VisitDate = dtpDateTime.Value;
            _Visit.Diagnosis = txtDiagnosis.Text.Trim();
            _Visit.Notes = txtNotes.Text.Trim();

            // 3. حفظ الزيارة في قاعدة البيانات
            if (_Visit.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblVisitID.Text = _Visit.VisitID.ToString();

                // الانتقال للوضع التالي
                tpPrescriptionInfo.Enabled = true;
                tcVisitInfo.SelectedTab = tcVisitInfo.TabPages["tpPrescriptionInfo"];

                // تغيير الحالة لـ Update عشان لو داس الزرار تاني يعدل ميكررش
                _Mode = enMode.Update;

                // تفعيل زرار الحفظ النهائي للروشتة
                btnSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("Failed to save visit details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

