using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BussinessLayer
{
    public class clsPerson
    {
        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalCountryID { get; set; }
        public string ImagePath { get; set; }


        public enum enMode
        {
            addNew = 0,
            update = 1
        }
        public static enMode mode = enMode.update;

        public clsPerson()
        {
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalCountryID = 0;
            this.ImagePath = "";
            mode = enMode.addNew;
        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, 
            string ThirdName, string LastName, DateTime DateOfBirth, int Gender,
            string Address, string Phone, string Email, int NationalCountryID,
            string ImagePath)
        {
            this.ID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalCountryID = NationalCountryID;
            this.ImagePath = ImagePath;

            mode = enMode.update;
        }
        public static DataTable GetPeople()
        {
            return clsPersonDataAccess.getPeople();
        }

        private bool _AddNewPerson()
        {
            this.ID = clsPersonDataAccess.addNewPerson(
                this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address,
                this.Phone, this.Email, this.NationalCountryID, this.ImagePath
            );
            mode = enMode.update;
            return this.ID != -1;
        }


        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.updatePerson(this.ID, this.NationalNo, 
                this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address,
                this.Phone, this.Email, this.NationalCountryID, this.ImagePath);
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.addNew:
                    return _AddNewPerson();
                case enMode.update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static bool IsPersonExists(string NationalNo)
        {
            return clsPersonDataAccess.isPersonExists(NationalNo);
        }

        public static clsPerson find(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            int Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalCountryID = 0;
            string ImagePath = "";
            if (clsPersonDataAccess.findByID(PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gender,
                ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName,
                SecondName, ThirdName, LastName, DateOfBirth, Gender,
                Address, Phone, Email, NationalCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetPeopleFilterByPersonID(int PersonID)
        {
            return clsPersonDataAccess.getPeopleFilterByPersonID(PersonID);
        }

        public static DataTable GetPeopleFilterByNationalNo(string NationalNo)
        {
            return clsPersonDataAccess.getPeopleFilterByNationalNo(NationalNo);
        }

        public static DataTable GetPeopleFilterByFirstName(string FirstName)
        {
            return clsPersonDataAccess.getPeopleFilterByFirstName(FirstName);
        }

        public static DataTable GetPeopleFilterBySecondName(string SecondName)
        {
            return clsPersonDataAccess.getPeopleFilterBySecondName(SecondName);
        }

        public static DataTable GetPeopleFilterByThirdName(string ThirdName)
        {
            return clsPersonDataAccess.getPeopleFilterByThirdName(ThirdName);
        }

        public static DataTable GetPeopleFilterByLastName(string LastName)
        {
            return clsPersonDataAccess.getPeopleFilterByLastName(LastName);
        }

        public static DataTable GetPeopleFilterByNationality(string Nationality)
        {
            return clsPersonDataAccess.getPeopleFilterByNationality(Nationality);
        }

        public static DataTable GetPeopleFilterByGender(string Gender)
        {
            return clsPersonDataAccess.getPeopleFilterByGender(Gender);
        }

        public static DataTable GetPeopleFilterByEmail(string Email)
        {
            return clsPersonDataAccess.getPeopleFilterByEmail(Email);
        }

        public static DataTable GetPeopleFilterByPhone(string Phone)
        {
            return clsPersonDataAccess.getPeopleFilterByPhone(Phone);
        }

        public static bool deletePerson(int PersonID)
        {
            return clsPersonDataAccess.deletePerson(PersonID);
        }


        public static clsPerson find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            int Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalCountryID = 0;
            string ImagePath = "";
            if (clsPersonDataAccess.findByNationalNo(ref PersonID, NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gender,
                ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName,
                SecondName, ThirdName, LastName, DateOfBirth, Gender,
                Address, Phone, Email, NationalCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static bool isPersonLinkedToUser(string NationalNo)
        {
            return clsPersonDataAccess.isPersonLinkedToUser(NationalNo);
        }


        public static clsPerson findByLicenseID(int LicenseID)
        {
            int PersonID = -1;
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            int Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalCountryID = 0;
            string ImagePath = "";
            if (clsPersonDataAccess.findByLicenseID(LicenseID, ref PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gender,
                ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName,
                SecondName, ThirdName, LastName, DateOfBirth, Gender,
                Address, Phone, Email, NationalCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }
    }
}
