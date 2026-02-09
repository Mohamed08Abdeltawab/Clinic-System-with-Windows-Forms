using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsMedicine
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int MedicineID { set; get; }
        public string MedicineName { set; get; }
        public decimal Price { set; get; } // smallmoney maps to decimal in C#

        public clsMedicine()
        {
            this.MedicineID = -1;
            this.MedicineName = "";
            this.Price = 0;

            Mode = enMode.AddNew;
        }

        private clsMedicine(int MedicineID, string MedicineName, decimal Price)
        {
            this.MedicineID = MedicineID;
            this.MedicineName = MedicineName;
            this.Price = Price;

            Mode = enMode.Update;
        }

        private bool _AddNewMedicine()
        {
            //call DataAccess Layer 
            this.MedicineID = clsMedicineData.AddNewMedicine(this.MedicineName, this.Price);

            return (this.MedicineID != -1);
        }

        private bool _UpdateMedicine()
        {
            //call DataAccess Layer 
            return clsMedicineData.UpdateMedicine(this.MedicineID, this.MedicineName, this.Price);
        }

        public static clsMedicine Find(int MedicineID)
        {
            string MedicineName = "";
            decimal Price = 0;

            bool IsFound = clsMedicineData.GetMedicineInfoByID(MedicineID, ref MedicineName, ref Price);

            if (IsFound)
                return new clsMedicine(MedicineID, MedicineName, Price);
            else
                return null;
        }

        // دالة للبحث بالاسم (مفيدة لمنع تكرار اسم الدواء)
        public static clsMedicine Find(string MedicineName)
        {
            int MedicineID = -1;
            decimal Price = 0;

            bool IsFound = clsMedicineData.GetMedicineInfoByName(MedicineName, ref MedicineID, ref Price);

            if (IsFound)
                return new clsMedicine(MedicineID, MedicineName, Price);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMedicine())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateMedicine();
            }

            return false;
        }

        public static DataTable GetAllMedicines()
        {
            return clsMedicineData.GetAllMedicines();
        }

        public static bool DeleteMedicine(int MedicineID)
        {
            return clsMedicineData.DeleteMedicine(MedicineID);
        }

        public static bool IsMedicineExist(int MedicineID)
        {
            return clsMedicineData.IsMedicineExist(MedicineID);
        }

        public static bool IsMedicineExist(string MedicineName)
        {
            return clsMedicineData.IsMedicineExist(MedicineName);
        }
    }
}