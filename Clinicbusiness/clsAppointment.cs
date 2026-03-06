using System;
using System.Collections.Generic;
using System.Data;
using ClinicData;

namespace Clinicbusiness
{
    public class clsAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enStatus { Scheduled = 1, Cancelled = 2, Completed = 3 };

        public enMode Mode = enMode.AddNew;

        public int AppointmentID { set; get; }
        public int PatientID { set; get; }
        public int DoctorID { set; get; }

        // التحديث: تغيير النوع لـ int وإضافة عمود السعر
        public int AppointmentTypeID { set; get; }
        public decimal AppointmentFees { set; get; }

        public string AppointmentTypeName
        {
            get
            {
                // نستخدم الكلاس الجديد لجلب الاسم من قاعدة البيانات لضمان المرونة
                clsAppointmentType type = clsAppointmentType.Find(this.AppointmentTypeID);
                return (type != null) ? type.Name : "Unknown";
            }
        }

        public DateTime AppointmentDate { set; get; }
        public byte Status { set; get; }
        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case 1: return "Scheduled";
                    case 2: return "Cancelled";
                    case 3: return "Completed";
                    default: return "Scheduled";
                }
            }
        }

        public int CreatedByUserID { set; get; }

        public clsPatient PatientInfo;
        public clsDoctor DoctorInfo;

        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.AppointmentTypeID = 1; // الافتراضي: Normal Visit
            this.AppointmentFees = 0;
            this.AppointmentDate = DateTime.Now;
            this.Status = (byte)enStatus.Scheduled;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsAppointment(int AppointmentID, int PatientID, int DoctorID, int AppointmentTypeID,
                               decimal AppointmentFees, DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
            this.AppointmentTypeID = AppointmentTypeID;
            this.AppointmentFees = AppointmentFees; // تعيين القيمة الجديدة
            this.AppointmentDate = AppointmentDate;
            this.Status = Status;
            this.CreatedByUserID = CreatedByUserID;

            this.PatientInfo = clsPatient.Find(PatientID);
            this.DoctorInfo = clsDoctor.Find(DoctorID);

            Mode = enMode.Update;
        }

        private bool _AddNewAppointment()
        {
            // تمرير الحقول الجديدة للـ Data Layer
            this.AppointmentID = clsAppointmentData.AddNewAppointment(
                this.PatientID, this.DoctorID, this.AppointmentTypeID, this.AppointmentFees,
                this.AppointmentDate, this.Status, this.CreatedByUserID);

            return (this.AppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            return clsAppointmentData.UpdateAppointment(
                this.AppointmentID, this.PatientID, this.DoctorID, this.AppointmentTypeID, this.AppointmentFees,
                this.AppointmentDate, this.Status, this.CreatedByUserID);
        }

        public static clsAppointment Find(int AppointmentID)
        {
            int PatientID = -1, DoctorID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            byte Status = 1;
            int AppointmentTypeID = 1;
            decimal AppointmentFees = 0; // متغير لاستقبال السعر

            bool IsFound = clsAppointmentData.GetAppointmentInfoByID(
                AppointmentID, ref PatientID, ref DoctorID, ref AppointmentTypeID, ref AppointmentFees,
                ref AppointmentDate, ref Status, ref CreatedByUserID);

            if (IsFound)
                return new clsAppointment(AppointmentID, PatientID, DoctorID, AppointmentTypeID,
                                          AppointmentFees, AppointmentDate, Status, CreatedByUserID);
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
                    return false;

                case enMode.Update:
                    return _UpdateAppointment();
            }

            return false;
        }

        // الدوال الاستاتيكية تبقى كما هي لأنها تعتمد على الـ DataTable
        public static DataTable GetAllAppointments() => clsAppointmentData.GetAllAppointments();
        public static bool DeleteAppointment(int AppointmentID) => clsAppointmentData.DeleteAppointment(AppointmentID);
        public static bool IsAppointmentExist(int AppointmentID) => clsAppointmentData.IsAppointmentExist(AppointmentID);
        public static bool IsAppointmentExistByPatientIDandDoctorID(int PatientID, int DoctorID) =>
            clsAppointmentData.IsAppointmentExistByPatientIDandDoctorID(PatientID, DoctorID);
    }
}