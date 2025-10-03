using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsApplication
    {
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public string ApplicantPersonFullName { get; set; }
        public string ApplicationTypeName{ get; set; }
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 0;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.ApplicantPersonFullName = "";
            this.ApplicationTypeName = "";
        }

        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID, string ApplicantPersonFullName, 
            string ApplicationTypeName)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicantPersonFullName = ApplicantPersonFullName;
            this.ApplicationTypeName = ApplicationTypeName;
        }

        private bool _addNewApplication()
        {
            this.ApplicationID = clsApplicationDataAccess.addNewApplication(this.ApplicantPersonID,
                this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return this.ApplicationID != -1;
        }

        public bool save()
        {
            return _addNewApplication();
        }

        public static clsApplication find(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1,
                CreatedByUserID = -1;
            byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            decimal PaidFees = 0;
            string ApplicantPersonFullName = "", ApplicationTypeName = "";

            if (clsApplicationDataAccess.findByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
                ref CreatedByUserID, ref ApplicantPersonFullName, ref ApplicationTypeName))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees,
                CreatedByUserID, ApplicantPersonFullName, ApplicationTypeName);
            }
            return null;
        }

        public static clsApplication findByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int ApplicantPersonID = -1, ApplicationTypeID = -1,
                CreatedByUserID = -1;
            byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            decimal PaidFees = 0;
            string ApplicantPersonFullName = "", ApplicationTypeName = "";

            if (clsApplicationDataAccess.findByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID, ref ApplicationID,ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
                ref CreatedByUserID, ref ApplicantPersonFullName, ref ApplicationTypeName))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees,
                CreatedByUserID, ApplicantPersonFullName, ApplicationTypeName);
            }
            return null;
        }

        public static bool checkRetakeTestForLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            return clsApplicationDataAccess.checkRetakeTestForLocalDrivingLicenseApplication
                (LocalDrivingLicenseApplicationID);
        }

        public static bool delete(int ApplicationID)
        {
            return clsApplicationDataAccess.deleteApplication(ApplicationID);
        }


        public static bool cancel(int ApplicationID)
        {
            return clsApplicationDataAccess.cancelApplication(ApplicationID);
        }

        public static bool isCanceled(int ApplicationID)
        {
            return clsApplicationDataAccess.isApplicationCanceled(ApplicationID);
        }
    }
}
