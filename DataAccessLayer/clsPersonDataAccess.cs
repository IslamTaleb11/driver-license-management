using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsPersonDataAccess
    {
        public static DataTable getPeople()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from People";
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

        public static int addNewPerson(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, int Gender,
            string Address, string Phone, string Email, int NationalCountryID, 
            string ImagePath)
        {
            int personID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"INSERT INTO People 
                        (nationalNo, firstName, secondName, thirdName, 
                        lastName, dateOfBirth, gender, address, 
                        phone, email, nationalityCountryID, ImagePath) 
                        VALUES 
                        (@nationalNo, @firstName, @secondName, 
                        @thirdName, @lastName, @dateOfBirth, @gender, 
                        @address, @phone, @email, @nationalityCountryID, @imagePath);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            command.Parameters.AddWithValue("@firstName", FirstName);
            command.Parameters.AddWithValue("@secondName", SecondName);
            command.Parameters.AddWithValue("@thirdName", ThirdName);
            command.Parameters.AddWithValue("@lastName", LastName);
            command.Parameters.AddWithValue("@dateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@gender", Gender);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@nationalityCountryID", NationalCountryID);
            if (!string.IsNullOrWhiteSpace(ImagePath))
            {
                command.Parameters.AddWithValue("@imagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@imagePath", DBNull.Value);
            }
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    personID = insertedID;
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return personID;
        }


        public static bool isPersonExists(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select found = 1 from People where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
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



        public static bool findByID(int PersonID, ref string NationalNo, ref string FirstName,
                ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, 
                ref int Gender, ref string Address, ref string Phone, 
                ref string Email, ref int NationalCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from People where PersonID = @personID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt32(reader["Gender"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : null;
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



        public static bool updatePerson(int PersonID,string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, int Gender,
            string Address, string Phone, string Email, int NationalCountryID,
            string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"UPDATE People SET
                firstName = @firstName,
                secondName = @secondName,
                thirdName = @thirdName,
                lastName = @lastName,
                dateOfBirth = @dateOfBirth,
                gender = @gender,
                address = @address,
                phone = @phone,
                email = @email,
                nationalityCountryID = @nationalityCountryID,
                ImagePath = @imagePath
            WHERE PersonID = @personID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            command.Parameters.AddWithValue("@firstName", FirstName);
            command.Parameters.AddWithValue("@secondName", SecondName);
            command.Parameters.AddWithValue("@thirdName", ThirdName);
            command.Parameters.AddWithValue("@lastName", LastName);
            command.Parameters.AddWithValue("@dateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@gender", Gender);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@nationalityCountryID", NationalCountryID);
            if (!string.IsNullOrWhiteSpace(ImagePath))
            {
                command.Parameters.AddWithValue("@imagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@imagePath", DBNull.Value);
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



        public static DataTable getPeopleFilterByPersonID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
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



        public static DataTable getPeopleFilterByNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
                WHERE NationalNo = @nationalNo 
               OR NationalNo LIKE @nationalNo + '%'";
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


        public static DataTable getPeopleFilterByFirstName(string FirstName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
                WHERE FirstName = @firstName 
               OR FirstName LIKE @firstName + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstName", FirstName);
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


        public static DataTable getPeopleFilterBySecondName(string SecondName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
                WHERE SecondName = @secondName
               OR SecondName LIKE @secondName + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@secondName", SecondName);
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

        public static DataTable getPeopleFilterByThirdName(string ThirdName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
                WHERE ThirdName = @thirdName
               OR ThirdName LIKE @thirdName + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@thirdName", ThirdName);
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

        public static DataTable getPeopleFilterByLastName(string LastName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
                WHERE LastName = @lastName
               OR LastName LIKE @lastName + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@lastName", LastName);
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

        public static DataTable getPeopleFilterByNationality(string Nationality)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People
                inner join Countries on People.NationalityCountryID = Countries.CountryID
                WHERE Countries.CountryName = @nationality
               OR Countries.CountryName LIKE @nationality + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nationality", Nationality);
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


        public static DataTable getPeopleFilterByGender(string Gender)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "";
            string male = "male";
            string female = "female";

            if (Gender.Length <= male.Length || Gender.Length <= female.Length)
            {
                if (Gender.Length <= male.Length)
                {
                    if (Gender.ToLower() == male.Substring(0, Gender.Length))
                    {
                        query = "Select * from people where Gender = '0';";
                    }
                }
                if (Gender.Length <= female.Length)
                {
                    if (Gender.ToLower() == female.Substring(0, Gender.Length))
                    {
                        query = "Select * from people where Gender = '1';";
                    }
                }
                
            }
            else
            {
                return null;
            }


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


        public static DataTable getPeopleFilterByEmail(string Email)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
                WHERE Email = @email
               OR Email LIKE @email + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@email", Email);
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


        public static DataTable getPeopleFilterByPhone(string Phone)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"SELECT * FROM People 
                WHERE Phone = @phone
               OR Phone LIKE @phone + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@phone", Phone);
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

        public static bool deletePerson(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"DELETE FROM People WHERE PersonID = @personID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
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


        public static bool findByNationalNo(ref int PersonID,string NationalNo, ref string FirstName,
               ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
               ref int Gender, ref string Address, ref string Phone,
               ref string Email, ref int NationalCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = "Select * from People where NationalNo = @nationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt32(reader["Gender"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : null;
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


        public static bool isPersonLinkedToUser(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"Select found = 1 from Users 
            inner join People on Users.PersonID = People.PersonID
            where People.NationalNo = @nationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
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


        public static bool findByLicenseID(int LicenseID, ref int PersonID, ref string NationalNo, ref string FirstName,
                ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
                ref int Gender, ref string Address, ref string Phone,
                ref string Email, ref int NationalCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString);
            string query = @"Select 
             People.PersonID,
                People.NationalNo,
                People.FirstName,
                People.SecondName,
                People.ThirdName,
                People.LastName,
                People.DateOfBirth,
                People.Gender,
                People.Address,
                People.Phone,
                People.Email,
                People.NationalityCountryID,
                People.ImagePath

            from Licenses 
            inner join Drivers on Licenses.DriverID = Drivers.DriverID
            inner join People on Drivers.PersonID = People.PersonID
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
                    PersonID = (int)reader["PersonID"];
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt32(reader["Gender"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : null;
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



