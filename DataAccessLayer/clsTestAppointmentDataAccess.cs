using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestAppointmentDataAccess
    {
        public static bool isThereAppointments(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"Select found = 1 from TestAppointments where  
                LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID
                and IsLocked = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
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

        public static int addNewTestAppointment(int TestTypeID
            , int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int CreatedByUserID, bool isLocked)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO TestAppointments 
                        (TestTypeID, LocalDrivingLicenseApplicationID, 
                        AppointmentDate, PaidFees, CreatedByUserID, isLocked) 
                        VALUES 
                        (@testTypeID, @localDrivingLicenseApplicationID, 
                        @appointmentDate, @paidFees, @createdByUserID, @isLocked);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeID", TestTypeID);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@appointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@paidFees", PaidFees);
            command.Parameters.AddWithValue("@createdByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@isLocked", isLocked);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
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

        public static DataTable getAllTestAppointments(string PersonNationalNo,
            int localDrivingLicenseApplication, int TestTypeID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select TestAppointments.TestAppointmentID, TestAppointments.AppointmentDate, 
            TestAppointments.PaidFees, TestAppointments.IsLocked
            from TestAppointments
            inner join TestTypes on TestAppointments.TestTypeID = TestTypes.TestTypeID
            inner join LocalDrivingLicenseApplications on TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
            inner join Applications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where People.NationalNo = @personNationalNo
            and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplication
            and TestTypes.TestTypeID = @testTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personNationalNo", PersonNationalNo);
            command.Parameters.AddWithValue("@localDrivingLicenseApplication", localDrivingLicenseApplication);
            command.Parameters.AddWithValue("@testTypeID", TestTypeID);

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


        public static bool findByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
                ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID,
                ref bool IsLocked)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select TestAppointments.TestAppointmentID, TestAppointments.TestTypeID,
            TestAppointments.AppointmentDate, 
            TestAppointments.PaidFees, TestAppointments.IsLocked,
            LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
            from TestAppointments
            inner join TestTypes on TestAppointments.TestTypeID = TestTypes.TestTypeID
            inner join LocalDrivingLicenseApplications on TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
            inner join Applications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
            inner join People on Applications.ApplicantPersonID = People.PersonID
            where TestAppointments.TestAppointmentID = @testAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentID", TestAppointmentID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

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



        public static bool updateTestAppointment(int TestAppointmentID, DateTime TestAppointmentDate)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE TestAppointments SET
                AppointmentDate = @testAppointmentDate
            WHERE TestAppointmentID = @testAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentDate", TestAppointmentDate);
            command.Parameters.AddWithValue("@testAppointmentID", TestAppointmentID);

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


        public static bool lockTestAppointment(int TestAppointmentID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE TestAppointments SET
                IsLocked = 1
            WHERE TestAppointmentID = @testAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentID", TestAppointmentID);

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


        public static bool hasTestBeenCompleted(int LocalDrivingLicenseAppID,
            int TestAppointmentID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select 1 from TestAppointments
            where LocalDrivingLicenseApplicationID = @localDrivingLicenseAppID
            and IsLocked = 1 and TestAppointmentID = @testAppointmentID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@testAppointmentID", TestAppointmentID);

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


        public static bool isTestAppointmentARetakeTest(int LocalDrivingLicenseAppID, int TestAppointmentID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"select * from TestAppointments
            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
            inner join Applications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
            inner join ApplicationTypes on ApplicationTypes.ApplicationTypeID = Applications.ApplicationTypeID
            where ApplicationTypeTitle = 'Retake Test' 
            and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID  
            and IsLocked = 0 and TestAppointmentID = @testAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@testAppointmentID", TestAppointmentID);

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
