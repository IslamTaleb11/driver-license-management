using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Policy;

namespace DataAccessLayer
{
    public class clsUserDataAccess
    {
        public static DataTable getUsers()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from users";
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
    
        public static bool isUsernameExists(string username)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select found = 1 from Users where UserName = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
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

        public static int addNewUser(int PersonID, string UserName, string Password,
            bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO Users 
                        (PersonID, UserName, Password, IsActive) 
                        VALUES 
                        (@personID, @userName, @password, 
                        @isActive);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@userName", UserName);
            command.Parameters.AddWithValue("@password", Password);
            command.Parameters.AddWithValue("@isActive", IsActive);
           
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        public static bool findByID(int UserID, ref int PersonID, ref string Username,
                ref string Password, ref bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from Users where UserID = @userID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", UserID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    Username = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["isActive"];
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


        public static bool deleteUserByID(int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"DELETE FROM Users WHERE UserID = @userID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", UserID);
            bool isDeleted = false;

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }


        public static bool updateUser(int UserID, string Username, string Password, 
            bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE Users SET
                UserName = @userName,
                Password = @password,
                IsActive = @isActive
            WHERE UserID = @userID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", UserID);
            command.Parameters.AddWithValue("@userName", Username);
            command.Parameters.AddWithValue("@password", Password);
            command.Parameters.AddWithValue("@isActive", IsActive);
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
    
        
        public static DataTable getUsersFilterByUserID(int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM Users 
                 WHERE PersonID = @userID 
                 OR CAST(UserID AS VARCHAR) LIKE @userID + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", UserID.ToString());
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


        public static DataTable getUsersFilterByPersonID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM Users 
                 WHERE PersonID = @personID 
                 OR CAST(PersonID AS VARCHAR) LIKE @personID + '%'";
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

        public static DataTable getUsersFilterByUsername(string Username)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM Users 
                 WHERE UserName = @username 
                 OR UserName LIKE @username + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", Username);
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


        public static DataTable getUsersFilterByFullName(string FullName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT *
            FROM Users
            INNER JOIN People ON Users.PersonID = People.PersonID
            WHERE 
                People.FirstName LIKE '%' + @fullName + '%'
                OR People.SecondName LIKE '%' + @fullName + '%'
                OR People.ThirdName LIKE '%' + @fullName + '%'
                OR People.LastName LIKE '%' + @fullName + '%'
                OR (People.FirstName + ' ' + People.SecondName + ' ' + 
                People.ThirdName + ' ' + People.LastName) LIKE '%' 
                + @fullName + '%'";



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


        public static DataTable getOnlyActiveUsers()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT *
            FROM Users where IsActive = 1";

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

        public static DataTable getOnlyNoneActiveUsers()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"
            SELECT *
            FROM Users where IsActive = 0";

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


        public static bool findByUsername(string Username, ref int UserID, 
            ref int PersonID,
                ref string Password, ref bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from Users where UserName = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", Username);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["isActive"];
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
