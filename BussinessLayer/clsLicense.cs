using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public enum enMode
        {
            addNew = 1,
            update = 2
        }

        public enMode mode = enMode.update;

        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = 0;
            this.CreatedByUserID = -1;
            this.mode = enMode.addNew;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, 
            DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, 
            bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

        }

        private bool _addNewLicense()
        {
            this.LicenseID = clsLicenseDataAccess.addNewLicense(this.ApplicationID, this.DriverID, 
                this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, 
                this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);

            return this.LicenseID != -1;
        }

        private bool _updateLicense()
        {
            return clsLicenseDataAccess.updateLicense(this.LicenseID, this.ApplicationID, this.DriverID,
                this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, 
                this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
        }

        public bool save()
        {
            switch(mode)
            {
                case enMode.addNew:
                    return _addNewLicense();
                case enMode.update:
                    return _updateLicense();
            }
            return false;
        }

        public static bool hasExistingDrivingLicense(int ApplicationID, string NationalNo)
        {
            return clsLicenseDataAccess.hasExistingDrivingLicense(ApplicationID, NationalNo);
        }

        public static clsLicense find(int ApplicationID)
        {
            int LicenseID = -1, DriverID = -1, LicenseClassID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;

            if (clsLicenseDataAccess.findByApplicationID(ApplicationID, ref LicenseID, ref DriverID, 
                ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, 
                ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID,
                LicenseClassID, IssueDate, ExpirationDate, Notes,
                PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            return null;
        }

        public static DataTable loadAllLocalDrivingLicenses(int PersonID)
        {
            return clsLicenseDataAccess.getAllLocalDrivingLicenses(PersonID);
        }

        public static bool doesLicenseExistAndActive(int LicenseID)
        {
            return clsLicenseDataAccess.doesLicenseExistAndActive(LicenseID);
        }

        public static bool isLicenseHasIntlLink(int LicenseID)
        {
            return clsLicenseDataAccess.isLicenseHasIntlLink(LicenseID);
        }

        public static clsLicense findByLicenseID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClassID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;

            if (clsLicenseDataAccess.findByLicenseID(LicenseID, ref ApplicationID, ref DriverID,
                ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes,
                ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID,
                LicenseClassID, IssueDate, ExpirationDate, Notes,
                PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            return null;
        }
    }
}
