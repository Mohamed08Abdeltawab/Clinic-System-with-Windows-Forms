using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsDoctor
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DoctorID { set; get; }
        public int PersonID { set; get; }
        public string Specialization { set; get; }
        public decimal ConsultationFees { set; get; }

        // 🌟 الخاصية الجديدة للتعامل مع أيام العمل كقائمة من الأرقام
        public List<byte> WorkingDaysIDs { set; get; }

        public clsPerson PersonInfo;

        public clsDoctor()
        {
            this.DoctorID = -1;
            this.PersonID = -1;
            this.Specialization = "";
            this.ConsultationFees = 0;

            // تهيئة القائمة لتجنب خطأ NullReferenceException
            this.WorkingDaysIDs = new List<byte>();

            Mode = enMode.AddNew;
        }

        private clsDoctor(int DoctorID, int PersonID, string Specialization, decimal ConsultationFees)
        {
            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.Specialization = Specialization;
            this.ConsultationFees = ConsultationFees;

            this.WorkingDaysIDs = new List<byte>();

            this.PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }

        private bool _AddNewDoctor()
        {
            // 1. إضافة الطبيب الأساسي أولاً للحصول على الـ ID الخاص به
            this.DoctorID = clsDoctorData.AddNewDoctor(this.PersonID, this.Specialization, this.ConsultationFees);

            if (this.DoctorID != -1)
            {
                // 2. بعد نجاح إضافة الطبيب، نحفظ أيام عمله في الجدول الوسيط
                foreach (byte DayID in this.WorkingDaysIDs)
                {
                    clsDoctorData.AddDoctorWorkingDay(this.DoctorID, DayID);
                }
                return true;
            }
            return false;
        }

        private bool _UpdateDoctor()
        {
            // 1. تحديث بيانات الطبيب الأساسية
            bool isUpdated = clsDoctorData.UpdateDoctor(this.DoctorID, this.PersonID, this.Specialization, this.ConsultationFees);

            if (isUpdated)
            {
                // 2. مسح كل الأيام القديمة لهذا الطبيب من الجدول الوسيط
                clsDoctorData.DeleteDoctorWorkingDays(this.DoctorID);

                // 3. إدخال الأيام الجديدة الموجودة في القائمة الحالية
                foreach (byte DayID in this.WorkingDaysIDs)
                {
                    clsDoctorData.AddDoctorWorkingDay(this.DoctorID, DayID);
                }
                return true;
            }
            return false;
        }

        public static clsDoctor Find(int DoctorID)
        {
            int PersonID = -1;
            string Specialization = "";
            decimal ConsultationFees = 0;

            bool IsFound = clsDoctorData.GetDoctorInfoByID(DoctorID, ref PersonID, ref Specialization, ref ConsultationFees);

            if (IsFound)
            {
                clsDoctor doctor = new clsDoctor(DoctorID, PersonID, Specialization, ConsultationFees);

                // 🌟 جلب أيام العمل وتعبئتها في القائمة
                DataTable dtDays = clsDoctorData.GetDoctorWorkingDays(DoctorID);
                foreach (DataRow row in dtDays.Rows)
                {
                    doctor.WorkingDaysIDs.Add((byte)row["DayID"]);
                }

                return doctor;
            }
            else
                return null;
        }

        public static clsDoctor FindByPersonID(int PersonID)
        {
            int DoctorID = -1;
            string Specialization = "";
            decimal ConsultationFees = 0;

            bool IsFound = clsDoctorData.GetDoctorInfoByPersonID(PersonID, ref DoctorID, ref Specialization, ref ConsultationFees);

            if (IsFound)
            {
                clsDoctor doctor = new clsDoctor(DoctorID, PersonID, Specialization, ConsultationFees);

                // 🌟 جلب أيام العمل وتعبئتها في القائمة
                DataTable dtDays = clsDoctorData.GetDoctorWorkingDays(DoctorID);
                foreach (DataRow row in dtDays.Rows)
                {
                    doctor.WorkingDaysIDs.Add((byte)row["DayID"]);
                }

                return doctor;
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDoctor())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDoctor();
            }

            return false;
        }

        public static DataTable GetAllDoctors()
        {
            return clsDoctorData.GetAllDoctors();
        }

        public static bool DeleteDoctor(int DoctorID)
        {
            return clsDoctorData.DeleteDoctor(DoctorID);
        }

        public static bool IsDoctorExist(int DoctorID)
        {
            return clsDoctorData.IsDoctorExist(DoctorID);
        }

        public static bool IsDoctorExistForPersonID(int PersonID)
        {
            return clsDoctorData.IsDoctorExistByPersonID(PersonID);
        }
    }
}