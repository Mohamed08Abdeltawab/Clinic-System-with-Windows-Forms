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

        public enum enPaymentStatus { Paid = 1, Unpaid = 2 };

        // إضافة Enum لطريقة الدفع بناءً على قاعدة البيانات
        public enum enPaymentMethod { Cash = 1, CreditCard = 2, Insurance = 3 };

        public int BillID { set; get; }
        public int VisitID { set; get; }
        public decimal TotalAmount { set; get; }
        public byte PaymentStatus { set; get; }

        // التعديل هنا: التاريخ يقبل Null
        public DateTime? PaymentDate { set; get; }

        // الخصائص الجديدة
        public byte PaymentMethod { set; get; }
        public decimal TaxAmount { set; get; }
        public decimal Discount { set; get; }

        public int CreatedByUserID { set; get; }

        public clsBill()
        {
            this.BillID = -1;
            this.VisitID = -1;
            this.TotalAmount = 0;
            this.PaymentStatus = (byte)enPaymentStatus.Unpaid;
            this.PaymentDate = null; // الافتراضي لا يوجد تاريخ دفع
            this.PaymentMethod = (byte)enPaymentMethod.Cash; // الافتراضي كاش
            this.TaxAmount = 0;
            this.Discount = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        // تحديث البناء الخاص ليشمل الحقول الجديدة
        private clsBill(int BillID, int VisitID, decimal TotalAmount,
                        byte PaymentStatus, DateTime? PaymentDate, byte PaymentMethod,
                        decimal TaxAmount, decimal Discount, int CreatedByUserID)
        {
            this.BillID = BillID;
            this.VisitID = VisitID;
            this.TotalAmount = TotalAmount;
            this.PaymentStatus = PaymentStatus;
            this.PaymentDate = PaymentDate;
            this.PaymentMethod = PaymentMethod;
            this.TaxAmount = TaxAmount;
            this.Discount = Discount;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewBill()
        {
            // تمرير المعاملات الجديدة للـ Data Layer
            this.BillID = clsBillData.AddNewBill(
                this.VisitID, this.TotalAmount, this.PaymentStatus, this.PaymentDate,
                this.PaymentMethod, this.TaxAmount, this.Discount, this.CreatedByUserID);

            return (this.BillID != -1);
        }

        private bool _UpdateBill()
        {
            // تمرير المعاملات الجديدة للـ Data Layer
            return clsBillData.UpdateBill(
                this.BillID, this.VisitID, this.TotalAmount, this.PaymentStatus, this.PaymentDate,
                this.PaymentMethod, this.TaxAmount, this.Discount, this.CreatedByUserID);
        }

        public static clsBill Find(int BillID)
        {
            int VisitID = -1, CreatedByUserID = -1;
            decimal TotalAmount = 0;
            byte PaymentStatus = 2;
            DateTime? PaymentDate = null; // متغير Nullable

            // متغيرات للحقول الجديدة
            byte PaymentMethod = 1;
            decimal TaxAmount = 0;
            decimal Discount = 0;

            bool IsFound = clsBillData.GetBillInfoByID(
                BillID, ref VisitID, ref TotalAmount, ref PaymentStatus, ref PaymentDate,
                ref PaymentMethod, ref TaxAmount, ref Discount, ref CreatedByUserID);

            if (IsFound)
                return new clsBill(BillID, VisitID, TotalAmount, PaymentStatus, PaymentDate,
                    PaymentMethod, TaxAmount, Discount, CreatedByUserID);
            else
                return null;
        }

        public static clsBill FindByVisitID(int VisitID)
        {
            int BillID = -1, CreatedByUserID = -1;
            decimal TotalAmount = 0;
            byte PaymentStatus = 2;
            DateTime? PaymentDate = null;

            byte PaymentMethod = 1;
            decimal TaxAmount = 0;
            decimal Discount = 0;

            bool IsFound = clsBillData.GetBillInfoByVisitID(
                VisitID, ref BillID, ref TotalAmount, ref PaymentStatus, ref PaymentDate,
                ref PaymentMethod, ref TaxAmount, ref Discount, ref CreatedByUserID);

            if (IsFound)
                return new clsBill(BillID, VisitID, TotalAmount, PaymentStatus, PaymentDate,
                    PaymentMethod, TaxAmount, Discount, CreatedByUserID);
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

        // دالة مفيدة للتحقق من وجود فاتورة لزيارة معينة
        public static bool IsBillExistByVisitID(int VisitID)
        {
            return clsBillData.IsBillExistByVisitID(VisitID);
        }
    }
}