using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsUser
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public enum enMode
        {
            addNew = 0,
            update = 1
        }

        public enMode mode = enMode.update;
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;

            mode = enMode.addNew;
        }

        private clsUser(int UserID, int PersonID, string Username, string Password
            ,bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = Username;
            this.Password = Password;
            this.IsActive = isActive;
        }

        public static DataTable getAllUsers()
        {
            return clsUserDataAccess.getUsers();
        }

        public static bool isUsernameExists(string username)
        {
            return clsUserDataAccess.isUsernameExists(username);
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserDataAccess.addNewUser(this.PersonID, this.UserName, this.Password,
                this.IsActive);

            return this.UserID != -1;
        }

        public static clsUser find(int UserID)
        {
            int PersonID = -1;
            string Username = "";
            string Password = "";
            bool IsActive = false;

            if (clsUserDataAccess.findByID(UserID, ref PersonID, ref Username, 
                ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser find(string Username)
        {
            int PersonID = -1;
            int UserID = -1;
            string Password = "";
            bool IsActive = false;

            if (clsUserDataAccess.findByUsername(Username,ref UserID, ref PersonID,
                ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateUser()
        {
            return clsUserDataAccess.updateUser(this.UserID, this.UserName, 
                this.Password,
                this.IsActive);
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.addNew:
                    return _AddNewUser();
                case enMode.update:
                    return _UpdateUser();
            }
            return false;
        }

        public bool Delete()
        {
            return clsUserDataAccess.deleteUserByID(this.UserID);
        }

        public static DataTable getUsersFilterByUserID(int UserId)
        {
            return clsUserDataAccess.getUsersFilterByUserID(UserId);
        }

        public static DataTable getUsersFilterByPersonID(int UserId)
        {
            return clsUserDataAccess.getUsersFilterByPersonID(UserId);
        }

        public static DataTable getUsersFilterByUsername(string Username)
        {
            return clsUserDataAccess.getUsersFilterByUsername(Username);
        }

        public static DataTable getUsersFilterByFullName(string FullName)
        {
            return clsUserDataAccess.getUsersFilterByFullName(FullName);
        }

        public static DataTable getOnlyActiveUsers()
        {
            return clsUserDataAccess.getOnlyActiveUsers();
        }

        public static DataTable getOnlyNoneActiveUsers()
        {
            return clsUserDataAccess.getOnlyNoneActiveUsers();
        }
    }
}
