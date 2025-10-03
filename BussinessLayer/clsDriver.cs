using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }

        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }

        private bool _addNewDriver()
        {
            this.DriverID = clsDriverDataAccess.addNewDriver(this.PersonID, this.CreatedByUserID
                , this.CreatedDate);

            return this.DriverID != -1;
        }
        public bool save()
        {
            return _addNewDriver();
        }

        public static bool isPersonAlreadyADriver(int PersonID)
        {
            return clsDriverDataAccess.isPersonAlreadyADriver(PersonID);
        }

        public static clsDriver find(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverDataAccess.findByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            return null;
        }


        public static DataTable getAllDrivers()
        {
            return clsDriverDataAccess.getAllDrivers();
        }

        public static DataTable getDriversByDriverID(int DriverID)
        {
            return clsDriverDataAccess.getDriversByDriverID(DriverID);
        }

        public static DataTable getDriversByPersonID(int PersonID)
        {
            return clsDriverDataAccess.getDriversByPersonID(PersonID);
        }

        public static DataTable getDriversByNationalNo(string NationalNo)
        {
            return clsDriverDataAccess.getDriversByNationalNo(NationalNo);
        }

        public static DataTable getDriversByFullName(string FullName)
        {
            return clsDriverDataAccess.getDriversByFullName(FullName);
        }
    }
}
