using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsDetainedLicense
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }


        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
        }

        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, 
            int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
        }

        public static clsDetainedLicense findByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = 0;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = 0;

            if (clsDetainedLicenseDataAccess.findByLicenseID(LicenseID, ref DetainID, ref DetainDate, 
                ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, 
                    IsReleased, ReleaseDate, ReleasedByUserID);
            }
            return null;
        }

        private bool _addNewDetainLicense()
        {
            this.DetainID = clsDetainedLicenseDataAccess.addNewDetainLicense(this.LicenseID, this.DetainDate, 
                this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID);

            return this.DetainID != -1;
        }

        public bool save()
        {
            return _addNewDetainLicense();
        }

        public static bool isLicenseDetained(int licenseID)
        {
            return clsDetainedLicenseDataAccess.isLicenseDetained(licenseID);
        }

        public bool releaseDetainedLicense(int ReleaseApplicationID, int ReleaseByUserID)
        {
            DateTime ReleaseDate = DateTime.Now;
            return clsDetainedLicenseDataAccess.releaseDetainedLicense(this.DetainID, ReleaseDate, 
                ReleaseByUserID, ReleaseApplicationID);
        }

        public static DataTable getAllDetainedLicenses()
        {
            return clsDetainedLicenseDataAccess.getAllDetainedLicenses();
        }

        public static DataTable getDetainedLicensesFilterByDetainID(int detaindID)
        {
            return clsDetainedLicenseDataAccess.getDetainedLicensesFilterByDetainID(detaindID);
        }

        public static DataTable getDetainedLicensesFilterByNationalNo(string nationalNo)
        {
            return clsDetainedLicenseDataAccess.getDetainedLicensesFilterByNationalNo(nationalNo);
        }

        public static DataTable getDetainedLicensesFilterByFullName(string fullName)
        {
            return clsDetainedLicenseDataAccess.getDetainedLicensesFilterByFullName(fullName);
        }

        public static DataTable getDetainedLicensesFilterByReleaseAppID(int releaseAppID)
        {
            return clsDetainedLicenseDataAccess.getDetainedLicensesFilterByReleaseAppID(releaseAppID);
        }

        public static DataTable getDetainedLicensesFilterByAllReleased()
        {
            return clsDetainedLicenseDataAccess.getDetainedLicensesFilterByAllReleased();
        }

        public static DataTable getDetainedLicensesFilterByNotReleased()
        {
            return clsDetainedLicenseDataAccess.getDetainedLicensesFilterByNotReleased();
        }
    }
}
