using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsVisit
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int VisitID { set; get; }
        public int AppointmentID { set; get; }
        public DateTime VisitDate { set; get; }
        public string Diagnosis { set; get; }
        public string Notes { set; get; }

        // Composition: للوصول لبيانات الموعد (ومنها للمريض والطبيب)
        public clsAppointment AppointmentInfo;

        public clsVisit()
        {
            this.VisitID = -1;
            this.AppointmentID = -1;
            this.VisitDate = DateTime.Now;
            this.Diagnosis = "";
            this.Notes = "";

            Mode = enMode.AddNew;
        }

        private clsVisit(int VisitID, int AppointmentID, DateTime VisitDate,
                         string Diagnosis, string Notes)
        {
            this.VisitID = VisitID;
            this.AppointmentID = AppointmentID;
            this.VisitDate = VisitDate;
            this.Diagnosis = Diagnosis;

            // التعامل مع الـ Null في الملاحظات
            this.Notes = (Notes == null) ? "" : Notes;

            // تحميل بيانات الموعد المرتبط تلقائياً
            this.AppointmentInfo = clsAppointment.Find(AppointmentID);

            Mode = enMode.Update;
        }

        private bool _AddNewVisit()
        {
            //call DataAccess Layer 
            this.VisitID = clsVisitData.AddNewVisit(
                this.AppointmentID, this.VisitDate, this.Diagnosis, this.Notes);

            return (this.VisitID != -1);
        }

        private bool _UpdateVisit()
        {
            //call DataAccess Layer 
            return clsVisitData.UpdateVisit(
                this.VisitID, this.AppointmentID, this.VisitDate, this.Diagnosis, this.Notes);
        }

        public static clsVisit Find(int VisitID)
        {
            int AppointmentID = -1;
            DateTime VisitDate = DateTime.Now;
            string Diagnosis = "", Notes = "";

            bool IsFound = clsVisitData.GetVisitInfoByID(
                VisitID, ref AppointmentID, ref VisitDate, ref Diagnosis, ref Notes);

            if (IsFound)
                return new clsVisit(VisitID, AppointmentID, VisitDate, Diagnosis, Notes);
            else
                return null;
        }

        // دالة للبحث عن الزيارة الخاصة بموعد معين
        // مفيدة جداً لمنع إنشاء زيارة ثانية لنفس الموعد
        public static clsVisit FindByAppointmentID(int AppointmentID)
        {
            int VisitID = -1;
            DateTime VisitDate = DateTime.Now;
            string Diagnosis = "", Notes = "";

            bool IsFound = clsVisitData.GetVisitInfoByAppointmentID(
                AppointmentID, ref VisitID, ref VisitDate, ref Diagnosis, ref Notes);

            if (IsFound)
                return new clsVisit(VisitID, AppointmentID, VisitDate, Diagnosis, Notes);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewVisit())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateVisit();
            }

            return false;
        }

        public static DataTable GetAllVisits()
        {
            return clsVisitData.GetAllVisits();
        }

        public static bool DeleteVisit(int VisitID)
        {
            return clsVisitData.DeleteVisit(VisitID);
        }

        public static bool IsVisitExist(int VisitID)
        {
            return clsVisitData.IsVisitExist(VisitID);
        }

        public static bool IsVisitExistByAppointmentID(int AppointmentID)
        {
            return clsVisitData.IsVisitExistByAppointmentID(AppointmentID);
        }

        // --- دوال إضافية ذكية ---

        // دالة تجلب كل الوصفات الطبية المرتبطة بهذه الزيارة الحالية
        // استخدامها: myVisit.GetPrescriptions();
        public DataTable GetPrescriptions()
        {
            return clsPrescription.GetPrescriptionsByVisitID(this.VisitID);
        }
    }
}