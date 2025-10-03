using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    public class clsDetainedLicenseDataAccess
    {
        public static bool findByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate,
                ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, 
                ref int ReleasedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select * from DetainedLicenses
            where LicenseID = @licenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", LicenseID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    ReleaseDate = (DateTime)reader["ReleaseDate"];
                    ReleasedByUserID = (int)reader["ReleasedByUserID"];

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


        public static int addNewDetainLicense(int LicenseID, DateTime DetainDate, decimal FineFees,
            int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID)
        {
            int detainedLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO DetainedLicenses 
                        (LicenseID, DetainDate, FineFees, CreatedByUserID, 
                        IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID) 
                        VALUES 
                        (@licenseID, @detainDate, @fineFees, 
                        @createdByUserID, @isReleased, @releaseDate, @releasedByUserID, 
                        @releaseApplicationID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", LicenseID);
            command.Parameters.AddWithValue("@detainDate", DetainDate);
            command.Parameters.AddWithValue("@fineFees", FineFees);
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@isReleased", IsReleased);
            command.Parameters.AddWithValue("@releaseDate", DBNull.Value);
            command.Parameters.AddWithValue("@releasedByUserID", DBNull.Value);
            command.Parameters.AddWithValue("@releaseApplicationID", DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    detainedLicenseID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return detainedLicenseID;
        }
    
    
        public static bool isLicenseDetained(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"Select * from DetainedLicenses where LicenseID = @licenseID
            and IsReleased = 0;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", LicenseID);
            bool isFound = false;
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                }
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


        public static bool releaseDetainedLicense(int DetainedLicenseID, DateTime ReleaseDate, 
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);

            string query = @"UPDATE DetainedLicenses SET
                IsReleased = 1,
                ReleaseDate = @releaseDate,
                ReleasedByUserID = @releasedByUserID,
                ReleaseApplicationID = @releaseApplicationID
            WHERE DetainID = @detainedLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@detainedLicenseID", DetainedLicenseID);
            command.Parameters.AddWithValue("@releaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@releasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@releaseApplicationID", ReleaseApplicationID);
            try
            {
                connection.Open();

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return false;
        }


        public static DataTable getAllDetainedLicenses()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
                DetainedLicenses.DetainID, 
                DetainedLicenses.LicenseID, 
                DetainedLicenses.DetainDate, 
                DetainedLicenses.IsReleased, 
                DetainedLicenses.FineFees, 
                DetainedLicenses.ReleaseDate, 
                People.NationalNo, 
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,
                DetainedLicenses.ReleaseApplicationID,
                People.PersonID
                
            FROM DetainedLicenses
            INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
            INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
            INNER JOIN People ON Drivers.PersonID = People.PersonID;";

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


        public static DataTable getDetainedLicensesFilterByDetainID(int DetainID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
                DetainedLicenses.DetainID, 
                DetainedLicenses.LicenseID, 
                DetainedLicenses.DetainDate, 
                DetainedLicenses.IsReleased, 
                DetainedLicenses.FineFees, 
                DetainedLicenses.ReleaseDate, 
                People.NationalNo, 
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,
                DetainedLicenses.ReleaseApplicationID,
                People.PersonID
                
            FROM DetainedLicenses
            INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
            INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
            INNER JOIN People ON Drivers.PersonID = People.PersonID

            WHERE DetainID = @detainID 
                 OR CAST(DetainID AS VARCHAR) LIKE @detainID + '%';";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@detainID", DetainID.ToString());

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


        public static DataTable getDetainedLicensesFilterByNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
                DetainedLicenses.DetainID, 
                DetainedLicenses.LicenseID, 
                DetainedLicenses.DetainDate, 
                DetainedLicenses.IsReleased, 
                DetainedLicenses.FineFees, 
                DetainedLicenses.ReleaseDate, 
                People.NationalNo, 
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,
                DetainedLicenses.ReleaseApplicationID,
                People.PersonID
                
            FROM DetainedLicenses
            INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
            INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
            INNER JOIN People ON Drivers.PersonID = People.PersonID

            WHERE People.NationalNo = @nationalNo 
                 OR People.NationalNo LIKE @nationalNo + '%';";

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


        public static DataTable getDetainedLicensesFilterByFullName(string FullName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
                DetainedLicenses.DetainID, 
                DetainedLicenses.LicenseID, 
                DetainedLicenses.DetainDate, 
                DetainedLicenses.IsReleased, 
                DetainedLicenses.FineFees, 
                DetainedLicenses.ReleaseDate, 
                People.NationalNo, 
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,
                DetainedLicenses.ReleaseApplicationID,
                People.PersonID
                
            FROM DetainedLicenses
            INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
            INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
            INNER JOIN People ON Drivers.PersonID = People.PersonID

            WHERE CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) = @fullName
            OR CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) LIKE @fullName + '%';
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", FullName);

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


        public static DataTable getDetainedLicensesFilterByReleaseAppID(int ReleaseAppID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
                DetainedLicenses.DetainID, 
                DetainedLicenses.LicenseID, 
                DetainedLicenses.DetainDate, 
                DetainedLicenses.IsReleased, 
                DetainedLicenses.FineFees, 
                DetainedLicenses.ReleaseDate, 
                People.NationalNo, 
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,
                DetainedLicenses.ReleaseApplicationID,
                People.PersonID
                
            FROM DetainedLicenses
            INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
            INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
            INNER JOIN People ON Drivers.PersonID = People.PersonID

            WHERE ReleaseApplicationID = @releaseAppID 
                 OR CAST(ReleaseApplicationID  AS VARCHAR) LIKE @releaseAppID + '%';";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@releaseAppID", ReleaseAppID.ToString());

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



        public static DataTable getDetainedLicensesFilterByAllReleased()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
                DetainedLicenses.DetainID, 
                DetainedLicenses.LicenseID, 
                DetainedLicenses.DetainDate, 
                DetainedLicenses.IsReleased, 
                DetainedLicenses.FineFees, 
                DetainedLicenses.ReleaseDate, 
                People.NationalNo, 
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,
                DetainedLicenses.ReleaseApplicationID,
                People.PersonID
                
            FROM DetainedLicenses
            INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
            INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
            INNER JOIN People ON Drivers.PersonID = People.PersonID

            WHERE IsReleased = 1;";

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


        public static DataTable getDetainedLicensesFilterByNotReleased()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
                DetainedLicenses.DetainID, 
                DetainedLicenses.LicenseID, 
                DetainedLicenses.DetainDate, 
                DetainedLicenses.IsReleased, 
                DetainedLicenses.FineFees, 
                DetainedLicenses.ReleaseDate, 
                People.NationalNo, 
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,
                DetainedLicenses.ReleaseApplicationID,
                People.PersonID
                
            FROM DetainedLicenses
            INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
            INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
            INNER JOIN People ON Drivers.PersonID = People.PersonID

            WHERE IsReleased = 0;";

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
    }
}
