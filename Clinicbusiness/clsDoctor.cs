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

        // خاصية Composition للوصول لاسم الطبيب وبياناته الشخصية
        public clsPerson PersonInfo;

        public clsDoctor()
        {
            this.DoctorID = -1;
            this.PersonID = -1;
            this.Specialization = "";
            Mode = enMode.AddNew;
        }

        private clsDoctor(int DoctorID, int PersonID, string Specialization)
        {
            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.Specialization = Specialization;

            // تحميل بيانات الشخص المرتبط بهذا الطبيب تلقائياً
            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewDoctor()
        {
            //call DataAccess Layer 
            this.DoctorID = clsDoctorData.AddNewDoctor(this.PersonID, this.Specialization);

            return (this.DoctorID != -1);
        }

        private bool _UpdateDoctor()
        {
            //call DataAccess Layer 
            return clsDoctorData.UpdateDoctor(this.DoctorID, this.PersonID, this.Specialization);
        }

        public static clsDoctor Find(int DoctorID)
        {
            int PersonID = -1;
            string Specialization = "";

            bool IsFound = clsDoctorData.GetDoctorInfoByID(DoctorID, ref PersonID, ref Specialization);

            if (IsFound)
                return new clsDoctor(DoctorID, PersonID, Specialization);
            else
                return null;
        }

        // دالة للبحث عن الطبيب باستخدام PersonID (مفيدة جداً)
        public static clsDoctor FindByPersonID(int PersonID)
        {
            int DoctorID = -1;
            string Specialization = "";

            bool IsFound = clsDoctorData.GetDoctorInfoByPersonID(PersonID, ref DoctorID, ref Specialization);

            if (IsFound)
                return new clsDoctor(DoctorID, PersonID, Specialization);
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

        // دالة للتأكد أن الشخص غير مسجل كطبيب مسبقاً (لمنع التكرار)
        public static bool IsDoctorExistForPersonID(int PersonID)
        {
            return clsDoctorData.IsDoctorExistByPersonID(PersonID);
        }
    }
}