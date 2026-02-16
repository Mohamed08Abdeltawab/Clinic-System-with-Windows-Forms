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

        public enum enStatus { Scheduled = 1, Cancelled = 2, Completed = 3 };

        // 1. إضافة Enum لنوع الموعد لتسهيل استخدامه في الواجهة
        public enum enAppointmentType { Checkup = 1, Consultation = 2 };

        public enMode Mode = enMode.AddNew;

        public int AppointmentID { set; get; }
        public int PatientID { set; get; }
        public int DoctorID { set; get; }

        // 2. الخاصية الجديدة
        public byte AppointmentType { set; get; }

        public DateTime AppointmentDate { set; get; }
        public byte Status { set; get; }
        public int CreatedByUserID { set; get; }

        public clsPatient PatientInfo;
        public clsDoctor DoctorInfo;

        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.AppointmentType = (byte)enAppointmentType.Checkup; // القيمة الافتراضية
            this.AppointmentDate = DateTime.Now;
            this.Status = (byte)enStatus.Scheduled;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        // 3. تحديث البناء الخاص (Private Constructor)
        private clsAppointment(int AppointmentID, int PatientID, int DoctorID, byte AppointmentType,
                               DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
            this.AppointmentType = AppointmentType; // تعيين القيمة
            this.AppointmentDate = AppointmentDate;
            this.Status = Status;
            this.CreatedByUserID = CreatedByUserID;

            this.PatientInfo = clsPatient.Find(PatientID);
            this.DoctorInfo = clsDoctor.Find(DoctorID);

            Mode = enMode.Update;
        }

        private bool _AddNewAppointment()
        {
            // 4. تمرير AppointmentType للـ Data Layer
            this.AppointmentID = clsAppointmentData.AddNewAppointment(
                this.PatientID, this.DoctorID, this.AppointmentType,
                this.AppointmentDate, this.Status, this.CreatedByUserID);

            return (this.AppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            // 5. تمرير AppointmentType للـ Data Layer
            return clsAppointmentData.UpdateAppointment(
                this.AppointmentID, this.PatientID, this.DoctorID, this.AppointmentType,
                this.AppointmentDate, this.Status, this.CreatedByUserID);
        }

        public static clsAppointment Find(int AppointmentID)
        {
            int PatientID = -1, DoctorID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            byte Status = 1;
            byte AppointmentType = 1; // متغير لاستقبال القيمة

            // 6. تحديث دالة البحث
            bool IsFound = clsAppointmentData.GetAppointmentInfoByID(
                AppointmentID, ref PatientID, ref DoctorID, ref AppointmentType,
                ref AppointmentDate, ref Status, ref CreatedByUserID);

            if (IsFound)
                // تمرير القيم للكونستركتور
                return new clsAppointment(AppointmentID, PatientID, DoctorID, AppointmentType,
                    AppointmentDate, Status, CreatedByUserID);
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

        public static DataTable GetAppointmentsByPatientID(int PatientID)
        {
            return clsAppointmentData.GetAppointmentsByPatientID(PatientID);
        }

        public static DataTable GetAppointmentsByDoctorID(int DoctorID)
        {
            return clsAppointmentData.GetAppointmentsByDoctorID(DoctorID);
        }
    }
}