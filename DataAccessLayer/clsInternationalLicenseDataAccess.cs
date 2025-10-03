using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsInternationalLicenseDataAccess
    {
        public static int addNewInternationalLicense(int ApplicationID,
            int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, 
            bool IsActive, int CreatedByUserID)
        {
            int internationalDrivingLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO InternationalLicenses 
                        (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, 
                        IsActive, CreatedByUserID) 
                        VALUES 
                        (@applicationID, @driverID, @issuedUsingLocalLicenseID, @issueDate, @expirationDate, 
                        @isActive, @createdByUserID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@driverID", DriverID);
            command.Parameters.AddWithValue("@issuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@issueDate", IssueDate);
            command.Parameters.AddWithValue("@expirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@isActive", IsActive);
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    internationalDrivingLicenseID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return internationalDrivingLicenseID;
        }


        public static DataTable getPersonInternationalLicenses(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenses.InternationalLicenseID, Applications.ApplicationID, InternationalLicenses.IssuedUsingLocalLicenseID, 
            InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive
            from InternationalLicenses 
            inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
            where Applications.ApplicantPersonID = @personID";

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


        public static DataTable getAllInternationalLicenses()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenses.InternationalLicenseID, Applications.ApplicationID, InternationalLicenses.DriverID,
            InternationalLicenses.IssuedUsingLocalLicenseID 
            as [L.LicenseID], 
            InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive,
			 People.PersonID
            from InternationalLicenses 
            inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID;";

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


        public static DataTable loadIntlLicensesFilterByIntlLicenseID(int IntlLicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenses.InternationalLicenseID, Applications.ApplicationID, InternationalLicenses.DriverID,
            InternationalLicenses.IssuedUsingLocalLicenseID 
            as [L.LicenseID], 
            InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive,
			 People.PersonID
            from InternationalLicenses 
            inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where InternationalLicenses.InternationalLicenseID = @intlLicenseID
            OR CAST(InternationalLicenses.InternationalLicenseID AS VARCHAR) LIKE @intlLicenseID + '%'";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@intlLicenseID", IntlLicenseID.ToString());
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


        public static DataTable loadIntlLicensesFilterByApplicationID(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenses.InternationalLicenseID, Applications.ApplicationID, InternationalLicenses.DriverID,
            InternationalLicenses.IssuedUsingLocalLicenseID 
            as [L.LicenseID], 
            InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive,
			 People.PersonID
            from InternationalLicenses 
            inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where InternationalLicenses.ApplicationID = @applicationID
            OR CAST(InternationalLicenses.ApplicationID AS VARCHAR) LIKE @applicationID + '%'";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID.ToString());
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


        public static DataTable loadIntlLicensesFilterByDriverID(int DriverID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenses.InternationalLicenseID, Applications.ApplicationID, InternationalLicenses.DriverID,
            InternationalLicenses.IssuedUsingLocalLicenseID 
            as [L.LicenseID], 
            InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive,
			 People.PersonID
            from InternationalLicenses 
            inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where InternationalLicenses.DriverID = @driverID
            OR CAST(InternationalLicenses.DriverID AS VARCHAR) LIKE @driverID + '%'";

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


        public static DataTable loadIntlLicensesFilterByLocalLicenseID(int LocalLicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenses.InternationalLicenseID, Applications.ApplicationID, InternationalLicenses.DriverID,
            InternationalLicenses.IssuedUsingLocalLicenseID 
            as [L.LicenseID], 
            InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive,
			 People.PersonID
            from InternationalLicenses 
            inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where InternationalLicenses.IssuedUsingLocalLicenseID = @localLicenseID
            OR CAST(InternationalLicenses.IssuedUsingLocalLicenseID AS VARCHAR) LIKE @localLicenseID + '%'";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localLicenseID", LocalLicenseID.ToString());
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



        public static DataTable loadIntlLicensesFilterByActive(bool Active)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenses.InternationalLicenseID, Applications.ApplicationID, InternationalLicenses.DriverID,
            InternationalLicenses.IssuedUsingLocalLicenseID 
            as [L.LicenseID], 
            InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive,
			 People.PersonID
            from InternationalLicenses 
            inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where InternationalLicenses.IsActive = @active";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@active", Active);
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



        public static bool findByInternationalLicenseID(int InternationaLicenseID, 
            ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, 
                ref DateTime IssuedDate, ref DateTime ExpirationDate, ref bool IsActive, 
                ref int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select InternationalLicenseID, ApplicationID, DriverID,
            IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,
            IsActive, CreatedByUserID from InternationalLicenses
            where InternationalLicenseID = @internationalLicenseID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@internationalLicenseID", InternationaLicenseID);
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
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssuedDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
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
    }
}
