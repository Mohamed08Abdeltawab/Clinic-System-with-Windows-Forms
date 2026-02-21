using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace Clinicbusiness
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        // تم إضافة الـ enum هنا
        public enum enRole { Admin = 0, Doctor = 1, Receptionist = 2 };

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }

        public enRole Role { set; get; }
        public string RoleName
        {
            get
            {
                switch(Role)
                {
                    case enRole.Admin:
                        return "Admin";
                    case enRole.Doctor:
                        return "Doctor";
                    case enRole.Receptionist:
                        return "Receptionist";
                    default:
                        return "Unknown";
                }
            }
        }

        public clsPerson PersonInfo;

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.Role = enRole.Receptionist;
            Mode = enMode.AddNew;
        }

        // تم تعديل البراميتر Role ليكون من نوع enRole
        private clsUser(int UserID, int PersonID, string UserName, string Password, enRole Role)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.Role = Role;

            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            // تم تحويل الـ enum إلى رقم (int) ليتم إرساله لقاعدة البيانات
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, (byte)this.Role);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            // تم تحويل الـ enum إلى رقم (int) ليتم إرساله لقاعدة البيانات
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, (byte)this.Role);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";

            // تم تغيير متغير الـ Role ليكون int ليتوافق مع قاعدة البيانات
            byte RoleValue = 0;

            bool IsFound = clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref RoleValue);

            if (IsFound)
                // تم تحويل الرقم القادم من قاعدة البيانات إلى enRole
                return new clsUser(UserID, PersonID, UserName, Password, (enRole)RoleValue);
            else
                return null;
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;

            // تم تغيير متغير الـ Role ليكون int ليتوافق مع قاعدة البيانات
            byte RoleValue = 0;

            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword(UserName, Password, ref UserID, ref PersonID, ref RoleValue);

            if (IsFound)
                // تم تحويل الرقم القادم من قاعدة البيانات إلى enRole
                return new clsUser(UserID, PersonID, UserName, Password, (enRole)RoleValue);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
    }
}