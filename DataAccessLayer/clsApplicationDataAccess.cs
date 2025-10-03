using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApplicationDataAccess
    {
        public static int addNewApplication(int ApplicantPersonID,
                DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus,
                DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int applicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO Applications 
                        (ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                        ApplicationStatus, LastStatusDate, PaidFees, 
                        CreatedByUserID) 
                        VALUES 
                        (@applicantPersonID, @applicationDate, @applicationTypeID, 
                        @applicationStatus, @lastStatusDate, @paidFees, 
                        @createdByUserID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@applicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@applicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@applicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@lastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@paidFees", PaidFees);
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);
            
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    applicationID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return applicationID;
        }

        public static bool findByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate,
                ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, 
                ref decimal PaidFees, ref int CreatedByUserID, ref string ApplicantPersonFullName,
                ref string ApplicationTypeName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select * from Applications
            inner join People on Applications.ApplicantPersonID = People.PersonID
            inner join ApplicationTypes on Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID
            where Applications.ApplicationID = @applicationID;";
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
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    ApplicantPersonFullName = (string)reader["FirstName"] + " " + (string)reader["SecondName"] + " " +
                        (string)reader["ThirdName"] + " " + (string)reader["LastName"];
                    ApplicationTypeName = (string)reader["ApplicationTypeTitle"];
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


        public static bool findByLocalDrivingLicenseAppID(int LocalDrivingLicenseAppID, ref int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate,
                ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate,
                ref decimal PaidFees, ref int CreatedByUserID, ref string ApplicantPersonFullName,
                ref string ApplicationTypeName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select * from Applications
            inner join People on Applications.ApplicantPersonID = People.PersonID
            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
            inner join ApplicationTypes on Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID
            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID", LocalDrivingLicenseAppID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    ApplicantPersonFullName = (string)reader["FirstName"] + " " + (string)reader["SecondName"] + " " +
                        (string)reader["ThirdName"] + " " + (string)reader["LastName"];
                    ApplicationTypeName = (string)reader["ApplicationTypeTitle"];
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


        public static bool checkRetakeTestForLocalDrivingLicenseApplication
            (int LocalDrivingLicenseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select * from Applications
            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
            inner join ApplicationTypes on Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID
            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID
            and ApplicationTypes.ApplicationTypeTitle = 'Retake Test';";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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


        public static bool deleteApplication
            (int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"DELETE T
            FROM Tests T
            INNER JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
            INNER JOIN LocalDrivingLicenseApplications LDA ON TA.LocalDrivingLicenseApplicationID = LDA.LocalDrivingLicenseApplicationID
            WHERE LDA.ApplicationID = @applicationID;

            DELETE TA
            FROM TestAppointments TA
            INNER JOIN LocalDrivingLicenseApplications LDA 
                ON TA.LocalDrivingLicenseApplicationID = LDA.LocalDrivingLicenseApplicationID
            WHERE LDA.ApplicationID = @applicationID;

            DELETE FROM LocalDrivingLicenseApplications
            WHERE ApplicationID = @applicationID;

            DELETE FROM Applications
            WHERE ApplicationID = @applicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            bool deleted = false;
            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    deleted = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return deleted;
        }


        public static bool cancelApplication
            (int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE Applications
            SET ApplicationStatus = 2
            WHERE ApplicationID = @applicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            bool canceled = false;
            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    canceled = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return canceled;
        }


        public static bool isApplicationCanceled
            (int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"Select found = 1 from Applications
            WHERE ApplicationID = @applicationID
            and ApplicationStatus = 2;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            bool found = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    found = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return found;
        }

    }
}
