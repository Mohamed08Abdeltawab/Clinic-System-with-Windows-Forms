using System;
using System.Collections.Generic;
using System.Data;
using ClinicData;

namespace Clinicbusiness
{
    public class clsService
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ServiceID { set; get; }
        public string ServiceName { set; get; }
        public string ServiceDescription { set; get; }
        public decimal ServicePrice { set; get; }

        public clsService()
        {
            this.ServiceID = -1;
            this.ServiceName = "";
            this.ServiceDescription = "";
            this.ServicePrice = 0;
            Mode = enMode.AddNew;
        }

        private clsService(int ServiceID, string ServiceName, string ServiceDescription, decimal ServicePrice)
        {
            this.ServiceID = ServiceID;
            this.ServiceName = ServiceName;
            this.ServiceDescription = ServiceDescription;
            this.ServicePrice = ServicePrice;
            Mode = enMode.Update;
        }

        private bool _AddNewService()
        {
            this.ServiceID = clsServiceData.AddNewService(this.ServiceName, this.ServiceDescription, this.ServicePrice);
            return (this.ServiceID != -1);
        }

        private bool _UpdateService()
        {
            return clsServiceData.UpdateService(this.ServiceID, this.ServiceName, this.ServiceDescription, this.ServicePrice);
        }

        public static clsService Find(int ServiceID)
        {
            string ServiceName = "", ServiceDescription = "";
            decimal ServicePrice = 0;

            if (clsServiceData.GetServiceInfoByID(ServiceID, ref ServiceName, ref ServiceDescription, ref ServicePrice))
                return new clsService(ServiceID, ServiceName, ServiceDescription, ServicePrice);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewService())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateService();
            }
            return false;
        }

        public static DataTable GetAllServices()
        {
            return clsServiceData.GetAllServices();
        }

        public static bool DeleteService(int ServiceID)
        {
            return clsServiceData.DeleteService(ServiceID);
        }
    }
}