using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLicenseDataAccess
    {
        public static int addNewLicense(int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int licenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO Licenses 
                        (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate,
                        Notes, PaidFees, IsActive, IssueReason, CreatedByUserID) 
                        VALUES 
                        (@applicationID, @driverID, @licenseClassID, @issueDate, @experationDate,
                        @notes, @paidFees, @isActive, @issueReason, @createdByUserID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@driverID", DriverID);
            command.Parameters.AddWithValue("@licenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@issueDate", IssueDate);
            command.Parameters.AddWithValue("@experationDate", ExpirationDate);
            if (!string.IsNullOrWhiteSpace(Notes))
            {
                command.Parameters.AddWithValue("@notes", Notes);
            }
            else
            {
                command.Parameters.AddWithValue("@notes", DBNull.Value);
            }
            command.Parameters.AddWithValue("@paidFees", PaidFees);
            command.Parameters.AddWithValue("@isActive", IsActive);
            command.Parameters.AddWithValue("@issueReason", IssueReason);
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    licenseID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return licenseID;
        }


        public static bool hasExistingDrivingLicense(int ApplicationID, string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select Applications.ApplicationID, People.NationalNo from Licenses
            inner join Applications on Licenses.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where Applications.ApplicationID = @applicationID
            and People.NationalNo = @nationalNo;
            UPDATE Applications
            SET ApplicationStatus = 3
            WHERE ApplicationID = @applicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);

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


        public static bool findByApplicationID(int ApplicationID, ref int LicenseID,
            ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, 
            ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, 
            ref int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
            Licenses.LicenseID, 
            Applications.ApplicationID, 
            Licenses.DriverID, 
            Licenses.LicenseClass, 
            Licenses.IssueDate, 
            Licenses.ExpirationDate, 
            Licenses.Notes,
            Licenses.PaidFees, 
            Licenses.IsActive, 
            Licenses.IssueReason, 
            Licenses.CreatedByUserID
            FROM Licenses
            INNER JOIN Applications ON Licenses.ApplicationID = Applications.ApplicationID
            where Licenses.ApplicationID = @applicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : null;
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    
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


        public static DataTable getAllLocalDrivingLicenses(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select 
                Licenses.LicenseID as [Lic.ID],
                Applications.ApplicationID as [App.ID],
                LicenseClasses.ClassName,
                Licenses.IssueDate,
                Licenses.ExpirationDate,
                Licenses.IsActive
                from Licenses
                inner join Applications on Licenses.ApplicationID = Applications.ApplicationID
                inner join LicenseClasses on Licenses.LicenseClass = LicenseClasses.LicenseClassID
                where Applications.ApplicantPersonID = @personID;
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
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


        public static bool doesLicenseExistAndActive(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT found = 1 from Licenses
            where LicenseID = @licenseID
            and IsActive = 1;";

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


        public static bool isLicenseHasIntlLink(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT found = 1 from InternationalLicenses
            where IssuedUsingLocalLicenseID = @licenseID";

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


        public static bool findByLicenseID(int LicenseID, ref int ApplicationID,
                ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate,
                ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason,
                ref int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT 
            Licenses.LicenseID, 
            Applications.ApplicationID, 
            Licenses.DriverID, 
            Licenses.LicenseClass, 
            Licenses.IssueDate, 
            Licenses.ExpirationDate, 
            Licenses.Notes,
            Licenses.PaidFees, 
            Licenses.IsActive, 
            Licenses.IssueReason, 
            Licenses.CreatedByUserID
            FROM Licenses
            INNER JOIN Applications ON Licenses.ApplicationID = Applications.ApplicationID
            where Licenses.LicenseID = @licenseID";

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
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : null;
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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



        public static bool updateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE Licenses SET
                ApplicationID = @applicationID,
                DriverID = @driverID,
                LicenseClass = @licenseClass,
                IssueDate = @issueDate,
                ExpirationDate = @expirationDate,
                Notes = @notes,
                PaidFees = @paidFees,
                IsActive = @isActive,
                IssueReason = @issueReason,
                CreatedByUserID = @createdByUserID
            WHERE LicenseID = @licenseID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@driverID", DriverID);
            command.Parameters.AddWithValue("@licenseClass", LicenseClassID);
            command.Parameters.AddWithValue("@issueDate", IssueDate);
            command.Parameters.AddWithValue("@expirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@paidFees", PaidFees);
            command.Parameters.AddWithValue("@isActive", IsActive);
            command.Parameters.AddWithValue("@issueReason", IssueReason);
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@licenseID", LicenseID);
            if (!string.IsNullOrWhiteSpace(Notes))
            {
                command.Parameters.AddWithValue("@notes", Notes);
            }
            else
            {
                command.Parameters.AddWithValue("@notes", DBNull.Value);
            }
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
    }
}
