using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsBill
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        // قمت بإنشاء هذا التعداد بناءً على الوصف الموجود في الصورة
        public enum enPaymentStatus { Paid = 1, Unpaid = 2 };

        public int BillID { set; get; }
        public int VisitID { set; get; }
        public decimal TotalAmount { set; get; } // smallmoney maps to decimal
        public byte PaymentStatus { set; get; }
        public DateTime PaymentDate { set; get; }
        public int CreatedByUserID { set; get; }

        public clsBill()
        {
            this.BillID = -1;
            this.VisitID = -1;
            this.TotalAmount = 0;
            this.PaymentStatus = (byte)enPaymentStatus.Unpaid; // القيمة الافتراضية
            this.PaymentDate = DateTime.Now;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsBill(int BillID, int VisitID, decimal TotalAmount,
                        byte PaymentStatus, DateTime PaymentDate, int CreatedByUserID)
        {
            this.BillID = BillID;
            this.VisitID = VisitID;
            this.TotalAmount = TotalAmount;
            this.PaymentStatus = PaymentStatus;
            this.PaymentDate = PaymentDate;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewBill()
        {
            //call DataAccess Layer 
            this.BillID = clsBillData.AddNewBill(
                this.VisitID, this.TotalAmount, this.PaymentStatus, this.PaymentDate, this.CreatedByUserID);

            return (this.BillID != -1);
        }

        private bool _UpdateBill()
        {
            //call DataAccess Layer 
            return clsBillData.UpdateBill(
                this.BillID, this.VisitID, this.TotalAmount, this.PaymentStatus, this.PaymentDate, this.CreatedByUserID);
        }

        public static clsBill Find(int BillID)
        {
            int VisitID = -1, CreatedByUserID = -1;
            decimal TotalAmount = 0;
            byte PaymentStatus = 2;
            DateTime PaymentDate = DateTime.Now;

            bool IsFound = clsBillData.GetBillInfoByID(
                BillID, ref VisitID, ref TotalAmount, ref PaymentStatus, ref PaymentDate, ref CreatedByUserID);

            if (IsFound)
                return new clsBill(BillID, VisitID, TotalAmount, PaymentStatus, PaymentDate, CreatedByUserID);
            else
                return null;
        }

        // دالة للبحث عن الفاتورة الخاصة بزيارة معينة
        public static clsBill FindByVisitID(int VisitID)
        {
            int BillID = -1, CreatedByUserID = -1;
            decimal TotalAmount = 0;
            byte PaymentStatus = 2;
            DateTime PaymentDate = DateTime.Now;

            bool IsFound = clsBillData.GetBillInfoByVisitID(
                VisitID, ref BillID, ref TotalAmount, ref PaymentStatus, ref PaymentDate, ref CreatedByUserID);

            if (IsFound)
                return new clsBill(BillID, VisitID, TotalAmount, PaymentStatus, PaymentDate, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBill())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateBill();
            }

            return false;
        }

        public static DataTable GetAllBills()
        {
            return clsBillData.GetAllBills();
        }

        public static bool DeleteBill(int BillID)
        {
            return clsBillData.DeleteBill(BillID);
        }

        public static bool IsBillExist(int BillID)
        {
            return clsBillData.IsBillExist(BillID);
        }
    }
}