using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsTestType
    {
        public int ID { get; }
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal Fees { get; set; }

        private clsTestType(int ID, string Title, string Description, decimal Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeDataAccess.GetAllTestTypes();
        }

        public static clsTestType find(int TestTypeID)
        {
            string Title = "";
            string Description = "";
            decimal Fees = 0;
            if (clsTestTypeDataAccess.findByID(TestTypeID, ref Title,
                ref Description, ref Fees))
            {
                return new clsTestType(TestTypeID, Title, Description, Fees);
            }
            else
            {
                return null;
            }
        }

        public static clsTestType find(string TestTypeTitle)
        {
            int TestTypeID = -1;
            string Description = "";
            decimal Fees = 0;
            if (clsTestTypeDataAccess.findByTitle(TestTypeTitle, ref TestTypeID,
                ref Description, ref Fees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, Description, Fees);
            }
            else
            {
                return null;
            }
        }

        private bool _updateTestnType()
        {
            return clsTestTypeDataAccess.updateTestType(this.ID, this.Title,this.Description,
                this.Fees);
        }

        public bool save()
        {

            return _updateTestnType();
        }

        public static decimal getFees(string TestTypeTitle)
        {
            return clsTestTypeDataAccess.getFees(TestTypeTitle);
        }
    }
}
