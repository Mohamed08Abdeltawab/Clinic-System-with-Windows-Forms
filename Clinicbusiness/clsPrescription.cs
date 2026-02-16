using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsPrescription
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PrescriptionID { set; get; }
        public int VisitID { set; get; }
        public int MedicineID { set; get; }
        public int Quantity { set; get; }
        public string Instructions { set; get; }

        // تم إضافة كائن الدواء لجلب بياناته (مثل الاسم والسعر) مباشرة عند الحاجة
        public clsMedicine MedicineInfo;

        public clsPrescription()
        {
            this.PrescriptionID = -1;
            this.VisitID = -1;
            this.MedicineID = -1;
            this.Quantity = 1;
            this.Instructions = "";

            Mode = enMode.AddNew;
        }

        private clsPrescription(int PrescriptionID, int VisitID, int MedicineID,
                                int Quantity, string Instructions)
        {
            this.PrescriptionID = PrescriptionID;
            this.VisitID = VisitID;
            this.MedicineID = MedicineID;
            this.Quantity = Quantity;
            this.Instructions = Instructions;

            // ربط تلقائي ببيانات الدواء عند العثور على السجل
            this.MedicineInfo = clsMedicine.Find(MedicineID);

            Mode = enMode.Update;
        }

        private bool _AddNewPrescription()
        {
            // استدعاء طبقة الوصول للبيانات لإضافة سجل جديد
            this.PrescriptionID = clsPrescriptionData.AddNewPrescription(
                this.VisitID, this.MedicineID, this.Quantity, this.Instructions);

            return (this.PrescriptionID != -1);
        }

        private bool _UpdatePrescription()
        {
            // استدعاء طبقة الوصول للبيانات لتحديث السجل الحالي
            return clsPrescriptionData.UpdatePrescription(
                this.PrescriptionID, this.VisitID, this.MedicineID, this.Quantity, this.Instructions);
        }

        public static clsPrescription Find(int PrescriptionID)
        {
            int VisitID = -1, MedicineID = -1, Quantity = 1;
            string Instructions = "";

            bool IsFound = clsPrescriptionData.GetPrescriptionInfoByID(
                PrescriptionID, ref VisitID, ref MedicineID, ref Quantity, ref Instructions);

            if (IsFound)
                return new clsPrescription(PrescriptionID, VisitID, MedicineID, Quantity, Instructions);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPrescription())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePrescription();
            }

            return false;
        }

        public static DataTable GetAllPrescriptions()
        {
            return clsPrescriptionData.GetAllPrescriptions();
        }

        public static bool DeletePrescription(int PrescriptionID)
        {
            return clsPrescriptionData.DeletePrescription(PrescriptionID);
        }

        public static bool IsPrescriptionExist(int PrescriptionID)
        {
            return clsPrescriptionData.IsPrescriptionExist(PrescriptionID);
        }

        // جلب جميع الأدوية الموصوفة لزيارة طبية محددة لعرضها في شاشة المريض
        public static DataTable GetPrescriptionsByVisitID(int VisitID)
        {
            return clsPrescriptionData.GetPrescriptionsByVisitID(VisitID);
        }
    }
}