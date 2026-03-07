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

        
        public clsAppointmentType AppointmentTypeInfo;

        public decimal AppointmentFees { set; get; } // السعر النهائي المحفوظ
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

        // المشيد الافتراضي
        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;

            // تهيئة الكائنات (Composition Initialization)
            this.AppointmentTypeInfo = new clsAppointmentType();
            this.AppointmentTypeInfo.ID = 1;
            this.AppointmentFees = 0;
            this.AppointmentDate = DateTime.Now;
            this.Status = (byte)enStatus.Scheduled;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        // المشيد الخاص لعملية الـ Find
        private clsAppointment(int AppointmentID, int PatientID, int DoctorID, int AppointmentTypeID,
                               decimal AppointmentFees, DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;

            // جلب بيانات النوع بالكامل (Composition)
            this.AppointmentTypeInfo = clsAppointmentType.Find(AppointmentTypeID);
            this.AppointmentTypeInfo.ID = AppointmentTypeID;
            this.AppointmentFees = AppointmentFees;
            this.AppointmentDate = AppointmentDate;
            this.Status = Status;
            this.CreatedByUserID = CreatedByUserID;

            this.PatientInfo = clsPatient.Find(PatientID);
            this.DoctorInfo = clsDoctor.Find(DoctorID);

            Mode = enMode.Update;
        }

        private bool _AddNewAppointment()
        {
            // نمرر الـ ID من داخل كائن الـ AppointmentTypeInfo
            this.AppointmentID = clsAppointmentData.AddNewAppointment(
                this.PatientID, this.DoctorID, this.AppointmentTypeInfo.ID, this.AppointmentFees,
                this.AppointmentDate, this.Status, this.CreatedByUserID);

            return (this.AppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            return clsAppointmentData.UpdateAppointment(
                this.AppointmentID, this.PatientID, this.DoctorID, this.AppointmentTypeInfo.ID, this.AppointmentFees,
                this.AppointmentDate, this.Status, this.CreatedByUserID);
        }

        public static clsAppointment Find(int AppointmentID)
        {
            int PatientID = -1, DoctorID = -1, CreatedByUserID = -1, AppointmentTypeID = -1;
            DateTime AppointmentDate = DateTime.Now;
            byte Status = 1;
            decimal AppointmentFees = 0;

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