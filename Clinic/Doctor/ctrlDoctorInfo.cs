using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinicbusiness;

namespace Clinic.Doctor
{
    public partial class ctrlDoctorInfo : UserControl
    {
        private clsDoctor _Doctor;
        private int _DoctorID;

        // need public id
        public int DoctorID
        {
            get { return _DoctorID; }
        }

        public ctrlDoctorInfo()
        {
            InitializeComponent();
        }

        public void LoadDoctorInfo(int DoctorID)
        {
            _Doctor = clsDoctor.Find(DoctorID);
            if (_Doctor == null)
            {
                _ResetDoctorInfo();
                MessageBox.Show($"No Doctor with ID: {DoctorID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DoctorID = _Doctor.DoctorID; // تحديث الـ ID المحلي
            _FillDoctorInfo();
        }

        // 🌟 دالة مساعدة جديدة لتحويل أرقام الأيام إلى نص مقروء
        private string _GetWorkingDaysNames()
        {
            if (_Doctor.WorkingDaysIDs == null || _Doctor.WorkingDaysIDs.Count == 0)
                return "Not Available";

            // مصفوفة بأسماء الأيام (المكان رقم 0 فارغ عشان رقم 1 يقابله الأحد مباشرة)
            string[] daysNames = { "", "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            List<string> selectedDays = new List<string>();

            foreach (byte dayID in _Doctor.WorkingDaysIDs)
            {
                if (dayID >= 1 && dayID <= 7)
                {
                    selectedDays.Add(daysNames[dayID]);
                }
            }

            // تجميع الأيام وبينهم فاصلة
            return string.Join(", ", selectedDays);
        }

        private void _FillDoctorInfo()
        {
            ctrlPersonCard1.LoadInfo(_Doctor.PersonID);
            lblDoctorID.Text = _Doctor.DoctorID.ToString();
            lblConsultationFees.Text = _Doctor.ConsultationFees.ToString();
            lblSpecialization.Text = _Doctor.Specialization.ToString();

            // 🌟 استخدام الدالة الجديدة لعرض الأيام
            lblWorkingDays.Text = _GetWorkingDaysNames();
        }

        private void _ResetDoctorInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblDoctorID.Text = "[???]";
            lblConsultationFees.Text = "[???]";
            lblSpecialization.Text = "[???]";
            lblWorkingDays.Text = "[???]";
        }
    }
}