using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };

        // تسهيل التعامل مع الحالة بدل الأرقام المبهمة
        public enum enStatus { Scheduled = 1, Cancelled = 2, Completed = 3 };

        public enMode Mode = enMode.AddNew;

        public int AppointmentID { set; get; }
        public int PatientID { set; get; }
        public int DoctorID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public byte Status { set; get; }
        public int CreatedByUserID { set; get; }

        // كائنات للوصول لبيانات المريض والطبيب مباشرة
        public clsPatient PatientInfo;
        public clsDoctor DoctorInfo;

        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.AppointmentDate = DateTime.Now;
            this.Status = (byte)enStatus.Scheduled; // الافتراضي مجدول
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsAppointment(int AppointmentID, int PatientID, int DoctorID,
                               DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
            this.AppointmentDate = AppointmentDate;
            this.Status = Status;
            this.CreatedByUserID = CreatedByUserID;

            // تحميل البيانات المرتبطة تلقائياً
            this.PatientInfo = clsPatient.Find(PatientID);
            this.DoctorInfo = clsDoctor.Find(DoctorID);

            Mode = enMode.Update;
        }

        private bool _AddNewAppointment()
        {
            //call DataAccess Layer 
            this.AppointmentID = clsAppointmentData.AddNewAppointment(
                this.PatientID, this.DoctorID, this.AppointmentDate, this.Status, this.CreatedByUserID);

            return (this.AppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            //call DataAccess Layer 
            return clsAppointmentData.UpdateAppointment(
                this.AppointmentID, this.PatientID, this.DoctorID, this.AppointmentDate, this.Status, this.CreatedByUserID);
        }

        public static clsAppointment Find(int AppointmentID)
        {
            int PatientID = -1, DoctorID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            byte Status = 1;

            bool IsFound = clsAppointmentData.GetAppointmentInfoByID(
                AppointmentID, ref PatientID, ref DoctorID, ref AppointmentDate, ref Status, ref CreatedByUserID);

            if (IsFound)
                return new clsAppointment(AppointmentID, PatientID, DoctorID, AppointmentDate, Status, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateAppointment();
            }

            return false;
        }

        public static DataTable GetAllAppointments()
        {
            return clsAppointmentData.GetAllAppointments();
        }

        public static bool DeleteAppointment(int AppointmentID)
        {
            return clsAppointmentData.DeleteAppointment(AppointmentID);
        }

        public static bool IsAppointmentExist(int AppointmentID)
        {
            return clsAppointmentData.IsAppointmentExist(AppointmentID);
        }

        // --- دوال إضافية مفيدة جداً للنظام ---

        // جلب مواعيد مريض معين
        public static DataTable GetAppointmentsByPatientID(int PatientID)
        {
            return clsAppointmentData.GetAppointmentsByPatientID(PatientID);
        }

        // جلب مواعيد طبيب معين
        public static DataTable GetAppointmentsByDoctorID(int DoctorID)
        {
            return clsAppointmentData.GetAppointmentsByDoctorID(DoctorID);
        }

    }
}