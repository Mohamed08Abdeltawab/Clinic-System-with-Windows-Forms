using System;
using System.Collections.Generic;
using System.Data;
using ClinicData;

namespace Clinicbusiness
{
    public class clsPrescriptionItem
    {
        public int ItemID { get; set; } = -1;
        public int PrescriptionID { get; set; }
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
    }
    public class clsPrescription
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PrescriptionID { set; get; }
        public int VisitID { set; get; }
        public DateTime PrescriptionDate { set; get; }
        public string Notes { set; get; }

        // التعديل الأهم: قائمة الأدوية بدل متغير MedicineID واحد
        public List<clsPrescriptionItem> ItemsList;

        public clsPrescription()
        {
            this.PrescriptionID = -1;
            this.VisitID = -1;
            this.PrescriptionDate = DateTime.Now;
            this.Notes = "";
            this.ItemsList = new List<clsPrescriptionItem>();

            Mode = enMode.AddNew;
        }

        private clsPrescription(int PrescriptionID, int VisitID, DateTime PrescriptionDate, string Notes)
        {
            this.PrescriptionID = PrescriptionID;
            this.VisitID = VisitID;
            this.PrescriptionDate = PrescriptionDate;
            this.Notes = Notes;

            // جلب الأدوية التابعة لهذه الروشتة وملء القائمة
            this.ItemsList = _LoadPrescriptionItems(PrescriptionID);

            Mode = enMode.Update;
        }

        private List<clsPrescriptionItem> _LoadPrescriptionItems(int PrescriptionID)
        {
            List<clsPrescriptionItem> items = new List<clsPrescriptionItem>();
            DataTable dt = clsPrescriptionData.GetPrescriptionItems(PrescriptionID);

            foreach (DataRow row in dt.Rows)
            {
                items.Add(new clsPrescriptionItem
                {
                    ItemID = (int)row["ItemID"],
                    MedicineID = (int)row["MedicineID"],
                    MedicineName = (string)row["MedicineName"],
                    Quantity = (int)row["Quantity"],
                    Dosage = row["Dosage"] == DBNull.Value ? "" : (string)row["Dosage"],
                    Instructions = row["Instructions"] == DBNull.Value ? "" : (string)row["Instructions"]
                });
            }
            return items;
        }

        private bool _AddNewPrescription()
        {
            // 1. حفظ الرأس (Header) والحصول على الـ ID الجديد
            this.PrescriptionID = clsPrescriptionData.AddNewPrescription(this.VisitID, this.PrescriptionDate, this.Notes);

            if (this.PrescriptionID == -1) return false;

            // 2. حفظ جميع الأدوية الموجودة في القائمة
            foreach (var item in ItemsList)
            {
                if (clsPrescriptionData.AddPrescriptionItem(this.PrescriptionID, item.MedicineID, item.Quantity, item.Dosage,item.Instructions) == -1)
                {
                    // ملاحظة: في المشاريع الكبيرة نستخدم Transaction هنا لضمان حفظ الكل أو لا شيء
                    return false;
                }
            }

            return true;
        }

        private bool _UpdatePrescription()
        {
            // عند التعديل: الأفضل مسح الأدوية القديمة وإضافة الجديدة من القائمة
            if (clsPrescriptionData.DeleteAllItemsByPrescriptionID(this.PrescriptionID))
            {
                foreach (var item in ItemsList)
                {
                    if (clsPrescriptionData.AddPrescriptionItem(this.PrescriptionID, item.MedicineID, item.Quantity,item.Dosage, item.Instructions) == -1)
                        return false;
                }
                return true;
            }
            return false;
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
                    return false;

                case enMode.Update:
                    return _UpdatePrescription();
            }
            return false;
        }

        public static clsPrescription Find(int PrescriptionID)
        {
            int VisitID = -1;
            DateTime PrescriptionDate = DateTime.Now;
            string Notes = "";

            if (clsPrescriptionData.GetPrescriptionInfoByID(PrescriptionID, ref VisitID, ref PrescriptionDate, ref Notes))
                return new clsPrescription(PrescriptionID, VisitID, PrescriptionDate, Notes);

            return null;
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

        public static bool IsPrescriptionExistByVisitID(int VisitID)
        {
            return clsPrescriptionData.IsPrescriptionExistByVisitID(VisitID);
        }

        public static DataTable GetPrescriptionItemsDataTable(int PrescriptionID)
        {
            return clsPrescriptionData.GetPrescriptionItems(PrescriptionID);
        }

        public static DataTable GetPrescriptionsByVisitID(int VisitID)
        {
            return clsPrescriptionData.GetPrescriptionsByVisitID(VisitID);
        }
    }
}