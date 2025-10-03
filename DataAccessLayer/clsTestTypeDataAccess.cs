using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestTypeDataAccess
    {
        public static DataTable GetAllTestTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from TestTypes";
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

        public static bool findByID(int TestTypeID, ref string Title, 
            ref string Description, ref decimal Fees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from TestTypes where TestTypeID = @testTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeID", TestTypeID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Title = (string)reader["TestTypeTitle"];
                    Description = (string)reader["TestTypeDescription"];
                    Fees = (decimal)reader["TestTypeFees"];
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

        public static bool updateTestType(int TestTypeID, string Title, 
            string Description, decimal Fees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE TestTypes SET
                TestTypeTitle = @title,
                TestTypeDescription = @description,
                TestTypeFees = @fees
            WHERE TestTypeID = @testTypeID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeID", TestTypeID);
            command.Parameters.AddWithValue("@title", Title);
            command.Parameters.AddWithValue("@description", Description);
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

        public static decimal getFees(string TestTypeTitle)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"Select TestTypeFees from TestTypes
            where TestTypeTitle = @testTypeTitle;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeTitle", TestTypeTitle);

            decimal fees = 0;
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(), out decimal foundedFees))
                {
                    fees = foundedFees;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return fees;
        }

        public static bool findByTitle(string TestTypeTitle, ref int TestTypeID,
            ref string Description, ref decimal Fees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from TestTypes where TestTypeTitle = @testTypeTitle";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeTitle", TestTypeTitle);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    Description = (string)reader["TestTypeDescription"];
                    Fees = (decimal)reader["TestTypeFees"];
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
