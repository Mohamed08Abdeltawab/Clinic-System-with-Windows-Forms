using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Global_Classes;
using Clinicbusiness;

namespace Clinic.Doctor
{
    public partial class frmAddUpdateDoctor : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _DoctorID = -1;
        clsDoctor _Doctor;

        public frmAddUpdateDoctor()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateDoctor(int DoctorID)
        {
            InitializeComponent();
            _DoctorID = DoctorID;
            _Mode = enMode.Update;
        }

        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Doctor";
                this.Text = "New Doctor";
                _Doctor = new clsDoctor();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpDoctorInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Doctor";
                this.Text = "Update Doctor";

                tpDoctorInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        // 🌟 دالة جديدة ونظيفة لعرض الأيام المحفوظة بناءً على الأرقام (IDs)
        public void ReadSelectedDays()
        {
            // 1. مسح أي اختيارات سابقة أولاً
            for (int i = 0; i < chkWorkingDays.Items.Count; i++)
            {
                chkWorkingDays.SetItemChecked(i, false);
            }

            // 2. إذا لم يكن لديه أيام محفوظة، نخرج
            if (_Doctor.WorkingDaysIDs == null || _Doctor.WorkingDaysIDs.Count == 0)
                return;

            // 3. تحديد الأيام بناءً على الأرقام القادمة من الداتا بيز
            foreach (byte dayID in _Doctor.WorkingDaysIDs)
            {
                // الـ Index في الـ CheckedListBox يبدأ من 0
                // ورقم اليوم في الداتا بيز يبدأ من 1 (الأحد=1، الإثنين=2...)
                int index = dayID - 1;

                // التأكد من أن الاندكس موجود داخل حدود الأداة لتجنب أي كراش
                if (index >= 0 && index < chkWorkingDays.Items.Count)
                {
                    chkWorkingDays.SetItemChecked(index, true);
                }
            }
        }

        // 🌟 دالة جديدة لتعبئة قائمة الأيام قبل الحفظ
        private void FillDoctorWorkingDaysList()
        {
            // تفريغ القائمة القديمة قبل إضافة الأيام الجديدة المحددة
            _Doctor.WorkingDaysIDs.Clear();

            // اللف على كل العناصر المحددة (Checked) واستخراج الاندكس الخاص بها
            foreach (int index in chkWorkingDays.CheckedIndices)
            {
                // تحويل الاندكس إلى رقم اليوم وإضافته للقائمة
                byte dayID = (byte)(index + 1);
                _Doctor.WorkingDaysIDs.Add(dayID);
            }
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _Doctor = clsDoctor.Find(_DoctorID);

            if (_Doctor == null)
            {
                MessageBox.Show("No Doctor with ID = " + _DoctorID, "Doctor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonData(_Doctor.PersonID);
            txtSpecialization.Text = _Doctor.Specialization.ToString();
            txtConsultationFees.Text = _Doctor.ConsultationFees.ToString();

            // استدعاء دالة عرض الأيام
            ReadSelectedDays();
        }

        private void frmAddUpdateDoctor_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnDoctorNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpDoctorInfo.Enabled = true;
                tcDoctorInfo.SelectedTab = tcDoctorInfo.TabPages["tpDoctorInfo"];
                return;
            }

            // incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsDoctor.IsDoctorExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already a Doctor, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpDoctorInfo.Enabled = true;
                    tcDoctorInfo.SelectedTab = tcDoctorInfo.TabPages["tpDoctorInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Doctor.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Doctor.Specialization = txtSpecialization.Text.Trim();
            _Doctor.ConsultationFees = Convert.ToDecimal(txtConsultationFees.Text.Trim());

            // التأكد من اختيار يوم عمل واحد على الأقل
            if (chkWorkingDays.CheckedItems.Count < 1)
            {
                chkWorkingDays.Focus();
                if (MessageBox.Show("Do you want to complete without giving Working Days for this Doctor?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }
            }

            // 🌟 استدعاء الدالة لتعبئة List<byte> قبل الحفظ
            FillDoctorWorkingDaysList();

            if (_Doctor.Save())
            {
                lblDoctorID.Text = _Doctor.DoctorID.ToString();
                // change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Doctor";
                this.Text = "Update Doctor";

                // تحديث الـ UI بدلاً من تعطيل زر الحفظ لتشجيع التعديلات اللاحقة لو لزم الأمر
                // btnSave.Enabled = false; 

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkWorkingDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkWorkingDays.ClearSelected();
        }

        private void txtSpecialization_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSpecialization.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSpecialization, "Specialization can't be blank!");
            }
            else
            {
                errorProvider1.SetError(txtSpecialization, "");
            }
        }

        private void txtConsultationFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConsultationFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConsultationFees, "Consultation Fees can't be blank!");
            }
            else
            {
                errorProvider1.SetError(txtConsultationFees, "");
            }
        }

        private void txtConsultationFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}