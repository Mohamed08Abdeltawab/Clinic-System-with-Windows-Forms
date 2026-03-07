using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsAppointmentType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.Update;

        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Fees { get; set; }

        // مشيد (Constructor) فارغ
        public clsAppointmentType()
        {
            this.ID = -1;
            this.Name = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }

        // مشيد خاص لعملية الـ Find
        private clsAppointmentType(int ID, string Name, decimal Fees)
        {
            this.ID = ID;
            this.Name = Name;
            this.Fees = Fees;
            Mode = enMode.Update;
        }

        // 1. دالة البحث عن نوع معين
        public static clsAppointmentType Find(int ID)
        {
            string Name = "";
            decimal Fees = 0;

            if (clsAppointmentTypeData.GetAppointmentTypeInfoByID(ID, ref Name, ref Fees))
            {
                return new clsAppointmentType(ID, Name, Fees);
            }
            else
            {
                return null;
            }
        }

        // 2. دالة جلب كل الأنواع لتعبئة الـ ComboBox
        public static DataTable GetAllAppointmentTypes()
        {
            return clsAppointmentTypeData.GetAllAppointmentTypes();
        }

        // 3. دالة التحديث (خاصة)
        private bool _Update()
        {
            return clsAppointmentTypeData.UpdateAppointmentType(this.ID, this.Name, this.Fees);
        }

        // 4. دالة الحفظ العامة
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    // يمكنك إضافة دالة AddNew في الـ Data Layer لو أردت إضافة أنواع جديدة برمجياً
                    return false;
                case enMode.Update:
                    return _Update();
            }
            return false;
        }
    }
}
