using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsInternationalLicense
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }


        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;
        }


        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, 
            bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
        }

        public static clsInternationalLicense find(int InternationaLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1;
            DateTime IssuedDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicenseDataAccess.findByInternationalLicenseID(InternationaLicenseID, 
                ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssuedDate, 
                ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationaLicenseID, ApplicationID, DriverID,
                    IssuedUsingLocalLicenseID, IssuedDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            return null;
        }

        private bool _addNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseDataAccess.addNewInternationalLicense(
                this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, 
                this.ExpirationDate, this.IsActive, this.CreatedByUserID
                );

            return this.InternationalLicenseID != -1;
        }

        public bool save()
        {
            return _addNewInternationalLicense();
        }

        public static DataTable loadPersonInternationalLicenses(int PersonID)
        {
            return clsInternationalLicenseDataAccess.getPersonInternationalLicenses(PersonID);
        }

        public static DataTable loadAllInternationalLicenses()
        {
            return clsInternationalLicenseDataAccess.getAllInternationalLicenses();
        }

        public static DataTable loadIntlLicensesFilterByIntlLicenseID(int IntlLicenseID)
        {
            return clsInternationalLicenseDataAccess.loadIntlLicensesFilterByIntlLicenseID(IntlLicenseID);
        }

        public static DataTable loadIntlLicensesFilterByApplicationID(int ApplicationID)
        {
            return clsInternationalLicenseDataAccess.loadIntlLicensesFilterByApplicationID(ApplicationID);
        }

        public static DataTable loadIntlLicensesFilterByDriverID(int DriverID)
        {
            return clsInternationalLicenseDataAccess.loadIntlLicensesFilterByDriverID(DriverID);
        }

        public static DataTable loadIntlLicensesFilterByLocalLicenseID(int LocalLicenseID)
        {
            return clsInternationalLicenseDataAccess.loadIntlLicensesFilterByLocalLicenseID(LocalLicenseID);
        }

        public static DataTable loadIntlLicensesFilterByActive(bool Active)
        {
            return clsInternationalLicenseDataAccess.loadIntlLicensesFilterByActive(Active);
        }
    }
}
