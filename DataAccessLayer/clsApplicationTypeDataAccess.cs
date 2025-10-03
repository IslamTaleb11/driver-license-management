using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApplicationTypeDataAccess
    {
        public static DataTable GetAllApplicationTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from ApplicationTypes";
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

        public static bool findByID(int ApplicationTypeID, ref string Title, ref decimal Fees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from ApplicationTypes where ApplicationTypeID = @applicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationTypeID", ApplicationTypeID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Title = (string)reader["ApplicationTypeTitle"];
                    Fees = (decimal)reader["ApplicationFees"];
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

        public static bool updateApplicationType(int ApplicationTypeID, string Title, decimal Fees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE ApplicationTypes SET
                ApplicationTypeTitle = @title,
                ApplicationFees = @fees
            WHERE ApplicationTypeID = @applicationTypeID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@title", Title);
            command.Parameters.AddWithValue("@fees", Fees);
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


        public static bool getApplicationFees(string ApplicationTypeTitle, ref decimal ApplicationFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select ApplicationFees from ApplicationTypes where ApplicationTypeTitle = @applicationTypeTitle";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationTypeTitle", ApplicationTypeTitle);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationFees = (decimal)reader["ApplicationFees"];
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


        public static bool findByTitle(string ApplicationTypeTitle, ref int ApplicationTypeID, ref decimal Fees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from ApplicationTypes where ApplicationTypeTitle = @applicationTypeTitle";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationTypeTitle", ApplicationTypeTitle);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    Fees = (decimal)reader["ApplicationFees"];
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
