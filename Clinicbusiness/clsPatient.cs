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

        public int PatientID { set; get; }
        public int PersonID { set; get; }

        // الخصائص الجديدة
        public string MedicalHistory { set; get; }
        public string BloodType { set; get; }
        public string EmergencyContact { set; get; } // Assuming string for simplicity (phone number)

        public clsPerson PersonInfo;

        public clsPatient()
        {
            this.PatientID = -1;
            this.PersonID = -1;
            this.MedicalHistory = "";
            this.BloodType = "";
            this.EmergencyContact = "";
            Mode = enMode.AddNew;
        }

        private clsPatient(int PatientID, int PersonID, string MedicalHistory,
                           string BloodType, string EmergencyContact)
        {
            this.PatientID = PatientID;
            this.PersonID = PersonID;
            this.MedicalHistory = MedicalHistory;
            this.BloodType = BloodType;
            this.EmergencyContact = EmergencyContact;

            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewPatient()
        {
            // تمرير الحقول الجديدة للـ Data Layer
            this.PatientID = clsPatientData.AddNewPatient(this.PersonID, this.MedicalHistory,
                this.BloodType, this.EmergencyContact);

            return (this.PatientID != -1);
        }

        private bool _UpdatePatient()
        {
            // تمرير الحقول الجديدة للـ Data Layer
            return clsPatientData.UpdatePatient(this.PatientID, this.PersonID,
                this.MedicalHistory, this.BloodType, this.EmergencyContact);
        }

        public static clsPatient Find(int PatientID)
        {
            int PersonID = -1;
            string MedicalHistory = "";
            string BloodType = "";
            string EmergencyContact = "";

            bool IsFound = clsPatientData.GetPatientInfoByID(PatientID, ref PersonID,
                ref MedicalHistory, ref BloodType, ref EmergencyContact);

            if (IsFound)
                return new clsPatient(PatientID, PersonID, MedicalHistory, BloodType, EmergencyContact);
            else
                return null;
        }

        public static clsPatient FindByPersonID(int PersonID)
        {
            int PatientID = -1;
            string MedicalHistory = "";
            string BloodType = "";
            string EmergencyContact = "";

            bool IsFound = clsPatientData.GetPatientInfoByPersonID(PersonID, ref PatientID,
                ref MedicalHistory, ref BloodType, ref EmergencyContact);

            if (IsFound)
                return new clsPatient(PatientID, PersonID, MedicalHistory, BloodType, EmergencyContact);
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

        public static bool IsPatientExistForPersonID(int PersonID)
        {
            return clsPatientData.IsPatientExistByPersonID(PersonID);
        }
    }
}