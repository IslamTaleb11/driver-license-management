using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsLocalDrivingLicenseApplication
    {
        public enum enMode
        {
            addNew = 1,
            update = 2
        }

        public enMode mode = enMode.update;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            mode = enMode.addNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
        }

        public static bool PersonHasLicenseInClass(int LicenseClassID, int PersonID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.PersonHasLicenseInClass(LicenseClassID, PersonID);
        }

        private bool _addNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = 
                clsLocalDrivingLicenseApplicationDataAccess.addNewLocalDrivingLicenseApplication(this.ApplicationID,
                this.LicenseClassID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }

        private bool _updateLocalDrivingLicenseApplication()
        {
            if (clsLocalDrivingLicenseApplicationDataAccess.
                updateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, 
                this.ApplicationID))
            {
                return true;
            }
            return false;
        }

        public bool save()
        {
            switch(mode)
            {
                case enMode.addNew:
                    return _addNewLocalDrivingLicenseApplication();
                case enMode.update:
                    return _updateLocalDrivingLicenseApplication();
            }
            return false;
            
        }

        public static DataTable getAllApplications()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.getAllApplications();
        }

        public static DataTable getAllApplicationsFilterByNationalNo(string NationalNo)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.getAllApplicationsFilterByNationalNo(NationalNo);
        }

        public static DataTable getAllApplicationsFilterByLocalDrivingLicenseAppID(int LocalDrivingLicenseAppID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.getAllApplicationsFilterByLocalDrivingLicenseAppID(LocalDrivingLicenseAppID);
        }

        public static DataTable getAllApplicationsFilterByFullName(string FullName)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.getAllApplicationsFilterByFullName(FullName);
        }

        public static DataTable getAllApplicationsFilterByStatus(string SelectedStatus)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.getAllApplicationsFilterByStatus(SelectedStatus);
        }

        public static int getPassedTestsOnApplicationByAppID(int ApplicationID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.getPassedTestsOnApplicationByAppID(ApplicationID);
        }

        public static int getPassedTestsOnApplicationByLDLAppID(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.getPassedTestsOnApplicationByLDLAppID(LDLAppID);
        }

        public static clsLocalDrivingLicenseApplication findByApplicationID(int applicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationDataAccess.findByApplicationID(applicationID, 
                ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, applicationID, LicenseClassID);
            }
            return null;
        }
    }
}
