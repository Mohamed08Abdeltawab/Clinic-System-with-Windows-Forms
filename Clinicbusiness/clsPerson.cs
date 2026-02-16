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
        public byte Gendor { set; get; } // tinyint maps to byte
        public DateTime DateOfBirth { set; get; }

        // الخصائص الجديدة
        public string Address { set; get; }
        public string Email { set; get; }
        public string ImagePath { set; get; }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FullName = "";
            this.Phone = "";
            this.Gendor = 0;
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Email = "";
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string FullName, string Phone, byte Gendor,
                          DateTime DateOfBirth, string Address, string Email, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FullName = FullName;
            this.Phone = Phone;
            this.Gendor = Gendor;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Email = Email;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 
            this.PersonID = clsPersonData.AddNewPerson(this.FullName, this.Phone, this.Gendor,
                this.DateOfBirth, this.Address, this.Email, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 
            return clsPersonData.UpdatePerson(this.PersonID, this.FullName, this.Phone, this.Gendor,
                this.DateOfBirth, this.Address, this.Email, this.ImagePath);
        }

        public static clsPerson Find(int PersonID)
        {
            string FullName = "", Phone = "", Address = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref FullName, ref Phone, ref Gendor,
                ref DateOfBirth, ref Address, ref Email, ref ImagePath);

            if (IsFound)
                return new clsPerson(PersonID, FullName, Phone, Gendor, DateOfBirth, Address, Email, ImagePath);
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