using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { set; get; }
        public string FullName { set; get; }
        public string Phone { set; get; }
        public byte Gendor { set; get; } // تم استخدام byte لأن النوع في قاعدة البيانات tinyint
        public DateTime DateOfBirth { set; get; }

        // Constructor for Add New
        public clsPerson()
        {
            this.PersonID = -1;
            this.FullName = "";
            this.Phone = "";
            this.Gendor = 0;
            this.DateOfBirth = DateTime.Now;

            Mode = enMode.AddNew;
        }

        // Private Constructor for Update (used inside Find method)
        private clsPerson(int PersonID, string FullName, string Phone, byte Gendor, DateTime DateOfBirth)
        {
            this.PersonID = PersonID;
            this.FullName = FullName;
            this.Phone = Phone;
            this.Gendor = Gendor;
            this.DateOfBirth = DateOfBirth;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 
            this.PersonID = clsPersonData.AddNewPerson(this.FullName, this.Phone, this.Gendor, this.DateOfBirth);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 
            return clsPersonData.UpdatePerson(this.PersonID, this.FullName, this.Phone, this.Gendor, this.DateOfBirth);
        }

        public static clsPerson Find(int PersonID)
        {
            string FullName = "", Phone = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref FullName, ref Phone, ref Gendor, ref DateOfBirth);

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, FullName, Phone, Gendor, DateOfBirth);
            else
                return null;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }

    }
}