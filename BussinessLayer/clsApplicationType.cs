using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsApplicationType
    {
        public int ID { get; }
        public string Title { get; set; }
        public decimal Fees { get; set; }

        private clsApplicationType(int ID, string Title, decimal Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeDataAccess.GetAllApplicationTypes();
        }

        public static clsApplicationType find(int ApplicationTypeID)
        {
            string Title = "";
            decimal Fees = 0;
            if (clsApplicationTypeDataAccess.findByID(ApplicationTypeID, ref Title, ref Fees))
            {
                return new clsApplicationType(ApplicationTypeID, Title, Fees);
            }
            else
            {
                return null;
            }
        }

        public static clsApplicationType find(string ApplicationTypeTitle)
        {
            int ApplicationTypeID = -1;
            decimal Fees = 0;
            if (clsApplicationTypeDataAccess.findByTitle(ApplicationTypeTitle, ref ApplicationTypeID, ref Fees))
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, Fees);
            }
            else
            {
                return null;
            }
        }

        private bool _updateApplicationType()
        {
            return clsApplicationTypeDataAccess.updateApplicationType(this.ID, this.Title, 
                this.Fees);
        }

        public bool save()
        {
            
            return _updateApplicationType();
           
        }

        public static decimal GetApplicationFees(string ApplicationType)
        {
            decimal ApplicationFees = -1;
            if(clsApplicationTypeDataAccess.getApplicationFees(ApplicationType, ref ApplicationFees))
            {
                return ApplicationFees;
            }
            return ApplicationFees;
        }
    }
}
