using System;
using System.Collections.Generic;
using System.Data;
using ClinicData;

namespace Clinicbusiness
{
    public class clsVisitService
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int VisitServiceID { set; get; }
        public int VisitID { set; get; }
        public int ServiceID { set; get; }
        public decimal ServiceFees { set; get; }

        // خاصية للوصول لبيانات الخدمة مباشرة
        public clsService ServiceInfo;

        public clsVisitService()
        {
            this.VisitServiceID = -1;
            this.VisitID = -1;
            this.ServiceID = -1;
            this.ServiceFees = 0;
            Mode = enMode.AddNew;
        }

        private clsVisitService(int VisitServiceID, int VisitID, int ServiceID, decimal ServiceFees)
        {
            this.VisitServiceID = VisitServiceID;
            this.VisitID = VisitID;
            this.ServiceID = ServiceID;
            this.ServiceFees = ServiceFees;
            this.ServiceInfo = clsService.Find(ServiceID);
            Mode = enMode.Update;
        }

        private bool _AddNewVisitService()
        {
            this.VisitServiceID = clsVisitServiceData.AddNewVisitService(this.VisitID, this.ServiceID, this.ServiceFees);
            return (this.VisitServiceID != -1);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (_AddNewVisitService())
                {
                    Mode = enMode.Update;
                    return true;
                }
            }
            return false;
        }

        public static DataTable GetVisitServicesByVisitID(int VisitID)
        {
            return clsVisitServiceData.GetVisitServicesByVisitID(VisitID);
        }
    }
}