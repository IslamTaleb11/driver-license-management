using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationDataAccess
    {

        public static bool PersonHasLicenseInClass(int LicenseClassID, int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT *
            FROM LocalDrivingLicenseApplications
            INNER JOIN Applications 
                ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
            WHERE Applications.ApplicantPersonID = @personID
              AND LicenseClassID = @licenseClassID
              AND ApplicationStatus IN (1, 3);
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@personID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
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
            return false;
        }


        public static int addNewLocalDrivingLicenseApplication(int ApplicationID,
            int LicenseClassID)
        {
            int localDrivingLicenseApplication = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO LocalDrivingLicenseApplications 
                        (ApplicationID, LicenseClassID) 
                        VALUES 
                        (@applicationID, @licenseClassID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@licenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    localDrivingLicenseApplication = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return localDrivingLicenseApplication;
        }

        public static DataTable getAllApplications()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT 
                Applications.ApplicationID,
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,  
                LicenseClasses.ClassName,  
                People.NationalNo,  
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,  
                Applications.ApplicationDate,  

                (SELECT COUNT(Tests.TestID)  
                 FROM Tests  
                 INNER JOIN TestAppointments  
                    ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID  
                 WHERE LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  
                   AND Tests.TestResult = 1  
                ) AS PassedTests,  

                CASE  
                    WHEN ApplicationStatus = 1 THEN 'New'  
                    WHEN ApplicationStatus = 2 THEN 'Canceled'  
                    WHEN ApplicationStatus = 3 THEN 'Completed'  
                END AS Status 

            FROM LocalDrivingLicenseApplications  
            INNER JOIN Applications  
                ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID  
            INNER JOIN LicenseClasses  
                ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID  
            INNER JOIN People  
                ON Applications.ApplicantPersonID = People.PersonID;";
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


        public static DataTable getAllApplicationsFilterByNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT 
                Applications.ApplicationID,
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,  
                LicenseClasses.ClassName,  
                People.NationalNo,  
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,  
                Applications.ApplicationDate,  

                (SELECT COUNT(Tests.TestID)  
                 FROM Tests  
                 INNER JOIN TestAppointments  
                    ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID  
                 WHERE LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  
                   AND Tests.TestResult = 1  
                ) AS PassedTests,  

                CASE  
                    WHEN ApplicationStatus = 1 THEN 'New'  
                    WHEN ApplicationStatus = 2 THEN 'Canceled'  
                    WHEN ApplicationStatus = 3 THEN 'Completed'  
                END AS Status 

            FROM LocalDrivingLicenseApplications  
            INNER JOIN Applications  
                ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID  
            INNER JOIN LicenseClasses  
                ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID  
            INNER JOIN People  
                ON Applications.ApplicantPersonID = People.PersonID
            where People.NationalNo = @nationalNo
            or People.NationalNo Like @nationalNo + '%'";
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

        public static DataTable getAllApplicationsFilterByLocalDrivingLicenseAppID(int LocalDrivingLicenseAppID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT 
                Applications.ApplicationID,
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,  
                LicenseClasses.ClassName,  
                People.NationalNo,  
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,  
                Applications.ApplicationDate,  

                (SELECT COUNT(Tests.TestID)  
                 FROM Tests  
                 INNER JOIN TestAppointments  
                    ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID  
                 WHERE LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  
                   AND Tests.TestResult = 1  
                ) AS PassedTests,  

                CASE  
                    WHEN ApplicationStatus = 1 THEN 'New'  
                    WHEN ApplicationStatus = 2 THEN 'Canceled'  
                    WHEN ApplicationStatus = 3 THEN 'Completed'  
                END AS Status 

            FROM LocalDrivingLicenseApplications  
            INNER JOIN Applications  
                ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID  
            INNER JOIN LicenseClasses  
                ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID  
            INNER JOIN People  
                ON Applications.ApplicantPersonID = People.PersonID
            WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLAIDDexact
             OR CAST(LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS VARCHAR) LIKE @LDLAIDlike;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAIDDexact", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@LDLAIDlike", LocalDrivingLicenseAppID.ToString() + "%");
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


        public static DataTable getAllApplicationsFilterByFullName(string FullName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT 
                Applications.ApplicationID,
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,  
                LicenseClasses.ClassName,  
                People.NationalNo,  
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,  
                Applications.ApplicationDate,  

                (SELECT COUNT(Tests.TestID)  
                 FROM Tests  
                 INNER JOIN TestAppointments  
                    ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID  
                 WHERE LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  
                   AND Tests.TestResult = 1  
                ) AS PassedTests,  

                CASE  
                    WHEN ApplicationStatus = 1 THEN 'New'  
                    WHEN ApplicationStatus = 2 THEN 'Canceled'  
                    WHEN ApplicationStatus = 3 THEN 'Completed'  
                END AS Status 

            FROM LocalDrivingLicenseApplications  
            INNER JOIN Applications  
                ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID  
            INNER JOIN LicenseClasses  
                ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID  
            INNER JOIN People  
                ON Applications.ApplicantPersonID = People.PersonID
            WHERE CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName)  = @fullName
             OR CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) LIKE @fullName + '%';";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@fullName", FullName);
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


        public static DataTable getAllApplicationsFilterByStatus(string SelectedStatus)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT 
                Applications.ApplicationID,
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,  
                LicenseClasses.ClassName,  
                People.NationalNo,  
                CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) AS FullName,  
                Applications.ApplicationDate,  

                (SELECT COUNT(Tests.TestID)  
                 FROM Tests  
                 INNER JOIN TestAppointments  
                    ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID  
                 WHERE LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  
                   AND Tests.TestResult = 1  
                ) AS PassedTests,  

                CASE  
                    WHEN ApplicationStatus = 1 THEN 'New'  
                    WHEN ApplicationStatus = 2 THEN 'Canceled'  
                    WHEN ApplicationStatus = 3 THEN 'Completed'  
                END AS Status 

            FROM LocalDrivingLicenseApplications  
            INNER JOIN Applications  
                ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID  
            INNER JOIN LicenseClasses  
                ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID  
            INNER JOIN People  
                ON Applications.ApplicantPersonID = People.PersonID
            WHERE 
            CASE  
                WHEN ApplicationStatus = 1 THEN 'New'  
                WHEN ApplicationStatus = 2 THEN 'Canceled'  
                WHEN ApplicationStatus = 3 THEN 'Completed'  
            END = @selectedStatus;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@selectedStatus", SelectedStatus);
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


        public static int getPassedTestsOnApplicationByAppID(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select count(ApplicationID) from LocalDrivingLicenseApplications 
            inner join TestAppointments on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
            inner join Tests on TestAppointments.TestAppointmentID = Tests.TestAppointmentID
            where ApplicationID = @applicationID and TestResult = 1;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);

            int result = 0;
            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                {
                    result = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return result;
        }


        public static int getPassedTestsOnApplicationByLDLAppID(int LDLAppID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select count(LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) from LocalDrivingLicenseApplications
            inner join TestAppointments on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
            inner join Tests on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLAppID 
            and Tests.TestResult = 1;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            int result = 0;
            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                {
                    result = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return result;
        }


        public static bool findByApplicationID(int ApplicationID, ref  int LocalDrivingLicenseApplicationID,
            ref int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from LocalDrivingLicenseApplications where ApplicationID = @applicationID";
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
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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


        public static bool updateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, 
            int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE LocalDrivingLicenseApplications SET
                ApplicationID = @applicationID
            WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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
