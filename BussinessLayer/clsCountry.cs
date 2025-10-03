using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsCountry
    {
        public int CountryID { get ; set ; }
        public string CountryName { get; set; }

        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

        public static clsCountry FindCountry(string CountryName)
        {
            int CountryID = -1;
            if (clsCountryDataAccess.FindCountryByName(CountryName, ref CountryID))
            {
                return new clsCountry(CountryID, CountryName);
            }
            return null;
        }

        public static clsCountry FindCountry(int CountryID)
        {
            string CountryName = "";
            if (clsCountryDataAccess.FindCountryByID(ref CountryName, CountryID))
            {
                return new clsCountry(CountryID, CountryName);
            }
            return null;
        }
    }
}
