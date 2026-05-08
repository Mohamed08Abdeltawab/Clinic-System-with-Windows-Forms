using Clinic.Doctor;
using Clinic.Financials.Manage_Bills;
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
    public partial class frmAddUpdatelVisitDetails : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        private enum enMode { AddNew = 0, Update = 1};
        private enMode _Mode = enMode.AddNew;

        private enum enTargetTab { Both, VisitOnly, PrescriptionOnly }
        private enTargetTab _TargetTab = enTargetTab.Both;

        private int _AppointmentID = -1;
        private clsVisit _Visit;
        private clsAppointment _Appointment;
        private clsPrescription _Prescription;

        private clsBill _Bill;
        private int _BillID = -1;

        public frmAddUpdatelVisitDetails(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _Appointment = clsAppointment.Find(_AppointmentID);
        }

        public void SetOnlyPrescriptionMode()
        {
            _TargetTab = enTargetTab.PrescriptionOnly;
        }

        public void SetOnlyVisitMode()
        {
            _TargetTab = enTargetTab.VisitOnly;
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

                btnShowBill.Visible = false; // تعطيل زرار عرض الفاتورة في وضع الإضافة لحد ما يحفظ الزيارة والروشتة

                lblVisitID.Text = "[???]";
                lblPrescriptionID.Text = "[???]";

                dtpDateTime.MinDate = DateTime.Now;
                dtpDateTime.Value = DateTime.Now;

                dtpPrescriptionDate.MinDate = DateTime.Now;
                dtpPrescriptionDate.Value = DateTime.Now;

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

                _Prescription = clsPrescription.FindByVisitID(_Visit.VisitID);
                if (_Prescription == null) _Prescription = new clsPrescription();

                btnShowBill.Visible = true; // تفعيل زرار عرض الفاتورة بعد حفظ الروشتة

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

                _Bill = clsBill.FindByVisitID(_Visit.VisitID);
                _BillID = (_Bill != null) ? _Bill.BillID : -1;
                // --- كود الوقت في حالة Update ---

                // أولاً: للزيارة
                DateTime visitDate = _Visit.VisitDate;
                // بنحسب الـ MinDate بتاعك (أيام الشهر الحالي)
                DateTime calcVisitMin = DateTime.Now.AddDays(-visitDate.Day);

                // التأمين: لو تاريخ الزيارة قديم أوي، بنخلي الـ MinDate هو تاريخ الزيارة نفسه
                dtpDateTime.MinDate = (visitDate < calcVisitMin) ? visitDate : calcVisitMin;
                dtpDateTime.Value = visitDate;


                // ثانياً: للروشتة (لو موجودة)
                if (_Prescription.PrescriptionID != -1)
                {
                    DateTime presDate = _Prescription.PrescriptionDate;
                    DateTime calcPresMin = DateTime.Now.AddDays(-presDate.Day);

                    dtpPrescriptionDate.MinDate = (presDate < calcPresMin) ? presDate : calcPresMin;
                    dtpPrescriptionDate.Value = presDate;
                }
                else
                {
                    // لو لسه ملوش روشتة، بنبدأ من النهاردة
                    dtpPrescriptionDate.MinDate = DateTime.Now;
                    dtpPrescriptionDate.Value = DateTime.Now;
                }

                _RefreshGrid();

                // --- التعديل الجوهري هنا لـ Update/Read ---
                tpPrescriptionInfo.Enabled = true;    // مفتوح لأن الزيارة موجودة
                btnSaveandNext.Enabled = true;        // شغال للتنقل
                btnSave.Enabled = (_Mode == enMode.Update); // شغال في التعديل ومطفي في القراءة

                //handel taps based on target
                if (_TargetTab == enTargetTab.VisitOnly)
                {
                    tpPrescriptionInfo.Enabled = false;
                    tcVisitInfo.SelectedTab = tpVisitInfo;
                    lblTitle.Text = "Update Visit Only";
                }
                else if (_TargetTab == enTargetTab.PrescriptionOnly)
                {
                    tpVisitInfo.Enabled = false;
                    tcVisitInfo.SelectedTab = tpPrescriptionInfo;
                    lblTitle.Text = "Update Prescription Only";
                }
            }

        }

        private void _SaveBill()
        {
            if (_Bill == null)
            {
                _Bill = new clsBill
                {
                    VisitID = _Visit.VisitID,
                    PaymentStatus = (byte)clsBill.enPaymentStatus.Paid,
                    PaymentDate = DateTime.Now,
                    PaymentMethod = (byte)clsBill.enPaymentMethod.Cash,
                    TaxAmount = 5, // لو في ضريبة، تحسب هنا
                    Discount = 0,
                    TotalAmount = _Prescription.ItemsList.Sum(i => i.Quantity * i.UnitPrice),
                    CreatedByUserID = clsGlobal.CurrentUser.UserID
                };
            }
            if (_Bill.Save())
            {
                _BillID = _Bill.BillID;
                MessageBox.Show("Bill saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if(dgvMedicines.Rows.Count <= 0)
            {
                MessageBox.Show("Must Select at lest one Medicine.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // تعبئة بيانات الروشتة
            _Prescription.VisitID = _Visit.VisitID;
            _Prescription.PrescriptionDate = DateTime.Now;
            _Prescription.Notes = txtPrescriptionNotes.Text.Trim();

            // حفظ الروشتة ومعها قائمة الأدوية (ItemsList)
            if (_Prescription.Save())
            {
                //save bill
                _SaveBill();

                lblPrescriptionID.Text = _Prescription.PrescriptionID.ToString();
                _Mode = enMode.Update;
                btnShowBill.Visible = true; // تفعيل زرار عرض الفاتورة بعد حفظ الروشتة

                MessageBox.Show("Prescription saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Visit.VisitID);
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
            // 1. Determine Mode
            if (clsVisit.IsVisitExistByAppointmentID(_AppointmentID))
                _Mode = enMode.Update;
            else if (clsAppointment.IsAppointmentExist(_AppointmentID))
                _Mode = enMode.AddNew;
            else
            {
                MessageBox.Show("Appointment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // هنا مسموح باستخدام Close لأن الشاشة بدأت العمل فعلياً
                return;
            }

            // 2. Load the rest of data
            _LoadData();
        }

        // ... باقى الـ Events (LinkClicked, Close, إلخ) تظل كما هي ...



        private void llPatientInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblPatientID.Text, out int patientID))
            {
                frmPatientInfo frm = new frmPatientInfo(patientID);

                frm.ShowDialog();
            }
        }

        private void llDoctorInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(lblDoctorID.Text, out int doctorID))
            {
                frmDoctorInfo frm = new frmDoctorInfo(doctorID);

                frm.ShowDialog();
            }
        }

        private void txtDiagnosis_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiagnosis.Text.Trim()))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDiagnosis, "");
            }
            else
            {
                e.Cancel = true;
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
            if (dgvMedicines.CurrentRow == null) return;
            clsPrescriptionItem SelectedItem = _Prescription.ItemsList[dgvMedicines.CurrentRow.Index];
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
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before proceeding.", "Validation warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                

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

        private void btnShowBill_Click(object sender, EventArgs e)
        {
           if(_BillID != -1)
            {
                frmBillDetails frm = new frmBillDetails(_BillID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No bill available to show.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

