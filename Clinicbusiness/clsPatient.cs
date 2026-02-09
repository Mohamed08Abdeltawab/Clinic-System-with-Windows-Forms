using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsPatient
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PatientID { set; get; } // تقابل PetientID في قاعدة البيانات
        public int PersonID { set; get; }

        // خاصية Composition للوصول لبيانات الشخص (الاسم، الهاتف، الخ)
        public clsPerson PersonInfo;

        public clsPatient()
        {
            this.PatientID = -1;
            this.PersonID = -1;
            Mode = enMode.AddNew;
        }

        private clsPatient(int PatientID, int PersonID)
        {
            this.PatientID = PatientID;
            this.PersonID = PersonID;

            // تحميل بيانات الشخص المرتبط بهذا المريض تلقائياً
            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewPatient()
        {
            //call DataAccess Layer 
            this.PatientID = clsPatientData.AddNewPatient(this.PersonID);

            return (this.PatientID != -1);
        }

        private bool _UpdatePatient()
        {
            //call DataAccess Layer 
            return clsPatientData.UpdatePatient(this.PatientID, this.PersonID);
        }

        public static clsPatient Find(int PatientID)
        {
            int PersonID = -1;

            bool IsFound = clsPatientData.GetPatientInfoByID(PatientID, ref PersonID);

            if (IsFound)
                return new clsPatient(PatientID, PersonID);
            else
                return null;
        }

        // دالة للبحث عن المريض باستخدام PersonID
        // مفيدة للتأكد هل هذا الشخص مسجل كمريض أم لا
        public static clsPatient FindByPersonID(int PersonID)
        {
            int PatientID = -1;

            bool IsFound = clsPatientData.GetPatientInfoByPersonID(PersonID, ref PatientID);

            if (IsFound)
                return new clsPatient(PatientID, PersonID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPatient())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePatient();
            }

            return false;
        }

        public static DataTable GetAllPatients()
        {
            return clsPatientData.GetAllPatients();
        }

        public static bool DeletePatient(int PatientID)
        {
            return clsPatientData.DeletePatient(PatientID);
        }

        public static bool IsPatientExist(int PatientID)
        {
            return clsPatientData.IsPatientExist(PatientID);
        }

        // دالة للتأكد أن الشخص غير مضاف كمريض مسبقاً
        public static bool IsPatientExistForPersonID(int PersonID)
        {
            return clsPatientData.IsPatientExistByPersonID(PersonID);
        }
    }
}