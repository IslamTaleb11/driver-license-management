using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsTestAppointment
    {
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }

        public enum enMode
        {
            addNew = 0,
            update = 1
        }
        public enMode mode = enMode.update;

        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = 0;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;

            mode = enMode.addNew;
        }

        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
        }

        public static bool isThereAppointments(int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentDataAccess.isThereAppointments(LocalDrivingLicenseApplicationID);
        }

        private bool _addNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentDataAccess.addNewTestAppointment(
                this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked);
            return this.TestAppointmentID != -1;
        }

        private bool _updateTestAppointment()
        {
            if (clsTestAppointmentDataAccess.updateTestAppointment(this.TestAppointmentID, this.AppointmentDate))
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
                    return _addNewTestAppointment();
                case enMode.update:
                    return _updateTestAppointment();
            }
            return false;
        }

        public static DataTable getAllTestAppointments(string PersonNationalNo, int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentDataAccess.getAllTestAppointments(PersonNationalNo, LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static clsTestAppointment find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsLocked = false;

            if (clsTestAppointmentDataAccess.findByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            }
            return null;
        }


        public static void lockTestAppointment(int TestAppointmentID)
        {
             clsTestAppointmentDataAccess.lockTestAppointment(TestAppointmentID);
        }

        public static bool hasTestBeenCompleted(int LocalDrivingLicenseAppID,
            int TestAppointmentID)
        {
            return clsTestAppointmentDataAccess.hasTestBeenCompleted(LocalDrivingLicenseAppID, TestAppointmentID);
        }

        public static bool isTestAppointmentARetakeTest(int LocalDrivingLicenseAppID, int TestAppointmentID)
        {
            return clsTestAppointmentDataAccess.isTestAppointmentARetakeTest(LocalDrivingLicenseAppID, TestAppointmentID);
        }
    }
}
