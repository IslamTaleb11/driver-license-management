using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsTest
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;
        }

        public static int countTrails(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsTestDataAccess.countTrails(localDrivingLicenseApplicationID, testTypeID);
        }

        private bool _addNewTest()
        {
            this.TestID = clsTestDataAccess.addNewTest(this.TestAppointmentID, this.TestResult, this.Notes, 
                this.CreatedByUserID);

            return this.TestID != -1;
        }

        public bool save()
        {
            return _addNewTest();
        }

        public static bool doesLocalDrivingLicenseAppHaveFailedTest
            (int LocalDrivingLicenseAppID, int TestTypeID, bool TestResult)
        {
            return clsTestDataAccess.doesLocalDrivingLicenseAppHaveFailedTest
                (LocalDrivingLicenseAppID, TestTypeID, TestResult);
        }

        public static bool doesLocalDrivingLicenseAppHaveSuccessTest
            (int LocalDrivingLicenseAppID, int TestTypeID, bool TestResult)
        {
            return clsTestDataAccess.doesLocalDrivingLicenseAppHaveSuccessTest
                (LocalDrivingLicenseAppID, TestTypeID, TestResult);
        }
    }
}
