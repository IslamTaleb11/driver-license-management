using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsDriverDataAccess
    {
        public static int addNewDriver(int PersonID,
            int CreatedByUserID, DateTime CreatedDate)
        {
            int driverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO Drivers 
                        (PersonID, CreatedByUserID, CreatedDate) 
                        VALUES 
                        (@personID, @createdByUserID, @createdDate);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@createdDate", CreatedDate);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    driverID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return driverID;
        }

        public static bool isPersonAlreadyADriver(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select found = 1 from Drivers
            where PersonID = @personID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool findByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID,
                ref DateTime CreatedDate)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from Drivers where PersonID = @personID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        public static DataTable getAllDrivers()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select Drivers.DriverID, People.PersonID, People.NationalNo, CONCAT(People.FirstName, ' ', People.SecondName, ' ',
            People.ThirdName, ' ', People.LastName) as FullName, Drivers.CreatedDate, 
            (
            select count(Licenses.LicenseID) from Licenses
            where Licenses.DriverID = Drivers.DriverID
            ) as ActiveLicenses
            from Drivers
            inner join People on Drivers.PersonID = People.PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            DataTable resultDataTable = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return resultDataTable;
        }


        public static DataTable getDriversByDriverID(int DriverID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select Drivers.DriverID, People.PersonID, People.NationalNo, CONCAT(People.FirstName, ' ', People.SecondName, ' ',
            People.ThirdName, ' ', People.LastName) as FullName, Drivers.CreatedDate, 
            (
            select count(Licenses.LicenseID) from Licenses
            where Licenses.DriverID = Drivers.DriverID
            ) as ActiveLicenses
            from Drivers
            inner join People on Drivers.PersonID = People.PersonID
            where Drivers.DriverID = @driverID
                 OR CAST(Drivers.DriverID AS VARCHAR) LIKE @driverID + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@driverID", DriverID.ToString());
            DataTable resultDataTable = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return resultDataTable;
        }


        public static DataTable getDriversByPersonID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select Drivers.DriverID, People.PersonID, People.NationalNo, CONCAT(People.FirstName, ' ', People.SecondName, ' ',
            People.ThirdName, ' ', People.LastName) as FullName, Drivers.CreatedDate, 
            (
            select count(Licenses.LicenseID) from Licenses
            where Licenses.DriverID = Drivers.DriverID
            ) as ActiveLicenses
            from Drivers
            inner join People on Drivers.PersonID = People.PersonID
            where People.PersonID = @personID
                 OR CAST(People.PersonID AS VARCHAR) LIKE @personID + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID.ToString());
            DataTable resultDataTable = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return resultDataTable;
        }


        public static DataTable getDriversByNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select Drivers.DriverID, People.PersonID, People.NationalNo, CONCAT(People.FirstName, ' ', People.SecondName, ' ',
            People.ThirdName, ' ', People.LastName) as FullName, Drivers.CreatedDate, 
            (
            select count(Licenses.LicenseID) from Licenses
            where Licenses.DriverID = Drivers.DriverID
            ) as ActiveLicenses
            from Drivers
            inner join People on Drivers.PersonID = People.PersonID
            where People.NationalNo = @nationalNo
                 OR People.NationalNo LIKE @nationalNo + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            DataTable resultDataTable = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return resultDataTable;
        }


        public static DataTable getDriversByFullName(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select Drivers.DriverID, People.PersonID, People.NationalNo, CONCAT(People.FirstName, ' ', People.SecondName, ' ',
            People.ThirdName, ' ', People.LastName) as FullName, Drivers.CreatedDate, 
            (
            select count(Licenses.LicenseID) from Licenses
            where Licenses.DriverID = Drivers.DriverID
            ) as ActiveLicenses
            from Drivers
            inner join People on Drivers.PersonID = People.PersonID
            where CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) = @fullName
            OR CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) LIKE @fullName + '%'
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@fullName", NationalNo);
            DataTable resultDataTable = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return resultDataTable;
        }

    }
}
