using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestDataAccess
    {
        public static int countTrails(int localDrivingLicenseApplicationID,
            int testTypeID)
        {
            int count = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select count(Tests.TestID) from Tests
            inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
            inner join TestTypes on TestAppointments.TestTypeID = TestTypes.TestTypeID
            inner join LocalDrivingLicenseApplications on TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID
            and TestTypes.TestTypeID = @testTypeID
            and Tests.TestResult = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int countedResult))
                {
                    count = countedResult;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return count;
        }

        public static int addNewTest(int TestAppointmentID
            , bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO Tests 
                        (TestAppointmentID, TestResult, 
                        Notes, CreatedByUserID) 
                        VALUES 
                        (@testAppointmentID, @testResult, 
                        @notes, @createdByUserID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@testResult", TestResult);
            if (!string.IsNullOrWhiteSpace(Notes))
            {
               command.Parameters.AddWithValue("@notes", Notes);
            }
            else
            {
                command.Parameters.AddWithValue("@notes", DBNull.Value);
            }
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return TestAppointmentID;
        }

        public static bool doesLocalDrivingLicenseAppHaveFailedTest
            (int LocalDrivingLicenseAppID, int TestTypeID, bool TestResult)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select * from Tests
            inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
            where LocalDrivingLicenseApplicationID = @localDrivingLicenseAppID 
            and TestTypeID = @testTypeID and TestResult = @testResult
            and IsLocked = 1;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@testResult", TestResult);

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

        public static bool doesLocalDrivingLicenseAppHaveSuccessTest
            (int LocalDrivingLicenseAppID, int TestTypeID, bool TestResult)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select * from Tests
            inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
            where LocalDrivingLicenseApplicationID = @localDrivingLicenseAppID 
            and TestTypeID = @testTypeID and TestResult = @testResult
            and IsLocked = 1;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@testResult", TestResult);

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
    }
}
