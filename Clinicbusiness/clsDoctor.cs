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

        // الخصائص الجديدة
        public decimal ConsultationFees { set; get; }
        public string WorkingDays { set; get; }

        public clsPerson PersonInfo;

        public clsDoctor()
        {
            this.DoctorID = -1;
            this.PersonID = -1;
            this.Specialization = "";
            this.ConsultationFees = 0;
            this.WorkingDays = "";
            Mode = enMode.AddNew;
        }

        private clsDoctor(int DoctorID, int PersonID, string Specialization,
                          decimal ConsultationFees, string WorkingDays)
        {
            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.Specialization = Specialization;
            this.ConsultationFees = ConsultationFees;
            this.WorkingDays = WorkingDays;

            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewDoctor()
        {
            // تمرير الحقول الجديدة للـ Data Layer
            this.DoctorID = clsDoctorData.AddNewDoctor(this.PersonID, this.Specialization,
                this.ConsultationFees, this.WorkingDays);

            return (this.DoctorID != -1);
        }

        private bool _UpdateDoctor()
        {
            // تمرير الحقول الجديدة للـ Data Layer
            return clsDoctorData.UpdateDoctor(this.DoctorID, this.PersonID, this.Specialization,
                this.ConsultationFees, this.WorkingDays);
        }

        public static clsDoctor Find(int DoctorID)
        {
            int PersonID = -1;
            string Specialization = "";
            decimal ConsultationFees = 0;
            string WorkingDays = "";

            bool IsFound = clsDoctorData.GetDoctorInfoByID(DoctorID, ref PersonID, ref Specialization,
                ref ConsultationFees, ref WorkingDays);

            if (IsFound)
                return new clsDoctor(DoctorID, PersonID, Specialization, ConsultationFees, WorkingDays);
            else
                return null;
        }

        public static clsDoctor FindByPersonID(int PersonID)
        {
            int DoctorID = -1;
            string Specialization = "";
            decimal ConsultationFees = 0;
            string WorkingDays = "";

            bool IsFound = clsDoctorData.GetDoctorInfoByPersonID(PersonID, ref DoctorID, ref Specialization,
                ref ConsultationFees, ref WorkingDays);

            if (IsFound)
                return new clsDoctor(DoctorID, PersonID, Specialization, ConsultationFees, WorkingDays);
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