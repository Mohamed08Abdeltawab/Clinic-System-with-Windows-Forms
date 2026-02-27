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

        // إضافة Enum فصائل الدم هنا ليكون متاحاً لطبقة الواجهة (UI)
        public enum enBloodType : byte
        {
            Unknown = 0,
            A_Plus = 1,
            A_Minus = 2,
            B_Plus = 3,
            B_Minus = 4,
            AB_Plus = 5,
            AB_Minus = 6,
            O_Plus = 7,
            O_Minus = 8
        }

        public int PatientID { set; get; }
        public int PersonID { set; get; }

        public string MedicalHistory { set; get; }

        // تعديل نوع BloodType إلى byte ليتوافق مع Data Layer
        public byte BloodType { set; get; }

        public string BloodTypeName
        {
            get
            {
                switch ((enBloodType)this.BloodType)
                {
                    case enBloodType.A_Plus: return "A+";
                    case enBloodType.A_Minus: return "A-";
                    case enBloodType.B_Plus: return "B+";
                    case enBloodType.B_Minus: return "B-";
                    case enBloodType.AB_Plus: return "AB+";
                    case enBloodType.AB_Minus: return "AB-";
                    case enBloodType.O_Plus: return "O+";
                    case enBloodType.O_Minus: return "O-";
                    default: return "Unknown";
                }
            }
        }

        public string EmergencyContact { set; get; }

        public clsPerson PersonInfo;

        public clsPatient()
        {
            this.PatientID = -1;
            this.PersonID = -1;
            this.MedicalHistory = "";
            this.BloodType = 0; // 0 تعني Unknown كقيمة افتراضية
            this.EmergencyContact = "";
            Mode = enMode.AddNew;
        }

        // تعديل باراميتر BloodType إلى byte
        private clsPatient(int PatientID, int PersonID, string MedicalHistory,
                           byte BloodType, string EmergencyContact)
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
            this.PatientID = clsPatientData.AddNewPatient(this.PersonID, this.MedicalHistory,
                this.BloodType, this.EmergencyContact);

            return (this.PatientID != -1);
        }

        private bool _UpdatePatient()
        {
            return clsPatientData.UpdatePatient(this.PatientID, this.PersonID,
                this.MedicalHistory, this.BloodType, this.EmergencyContact);
        }

        public static clsPatient Find(int PatientID)
        {
            int PersonID = -1;
            string MedicalHistory = "";
            byte BloodType = 0; // تعديل إلى byte
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
            byte BloodType = 0; // تعديل إلى byte
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