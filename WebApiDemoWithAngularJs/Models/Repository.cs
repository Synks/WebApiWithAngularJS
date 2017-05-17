using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiDemoWithAngularJs.Models
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllPersons();
        Person GetPersonbById(int PersonId);
        int InsertPerson(Person personObject);
        int UpdatePerson(Person personObject);
        int DeletePerson(int PersonId);
    }

    public class PersonRepository : IPersonRepository
    {
        static readonly string selectAllPersonsQuery = @"SELECT [PersonId],[FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber]FROM [dbo].[Person]";
        static readonly string selectPersonbyIdQuery = @"SELECT [PersonId],[FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber]FROM [dbo].[Person] where PersonId=@personId";
        static readonly string insertPersonQuery = @"INSERT INTO [dbo].[Person]([FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber])
                                                VALUES(@FirstName,@MiddleName,@LastName,@PrimaryEmailAddress,@SecondaryEmailAddress,@PhoneNumber,@MobileNumber);SELECT SCOPE_IDENTITY();";
        static readonly string updatePersonByIdQuery = @"Update [dbo].[Person] SET [FirstName]=@FirstName,[MiddleName]=@MiddleName,[LastName]=@LastName,[PrimaryEmailAddress]=@PrimaryEmailAddress,[SecondaryEmailAddress]=@SecondaryEmailAddress,[PhoneNumber]=@PhoneNumber,[MobileNumber]=@MobileNumber where PersonId=@personId";
        static readonly string deletePersonQuery = @"SELECT [PersonId],[FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber]FROM [dbo].[Person]";
        public IEnumerable<Person> GetAllPersons()
        {
            //Create and open a connection to SQL Server 
            SqlConnection connection = null;
            List<Person> persons = new List<Person>();

            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDemo"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(selectAllPersonsQuery, connection);
                //Create DataReader for storing the returning table into server memory
                SqlDataReader dataReader = command.ExecuteReader();
                Person person = null;
                //load into the result object the returned row from the database
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        person = new Person();

                        person.PersonId = Convert.ToInt32(dataReader["PersonId"]);
                        person.FirstName = dataReader["FirstName"].ToString();
                        person.MiddleName = dataReader["MiddleName"].ToString();
                        person.LastName = dataReader["LastName"].ToString();
                        person.PrimaryEmailAddress = dataReader["PrimaryEmailAddress"].ToString();
                        person.SecondaryEmailAddress = dataReader["SecondaryEmailAddress"].ToString();
                        person.MobileNumber = dataReader["MobileNumber"].ToString();
                        person.PhoneNumber = dataReader["PhoneNumber"].ToString();
                        persons.Add(person);
                    }
                }
            }

            return persons;
        }

        public Person GetPersonbById(int PersonId)
        {
            SqlConnection connection = null;
            Person person = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDemo"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(selectPersonbyIdQuery, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@personId", PersonId);
                //Create DataReader for storing the returning table into server memory
                SqlDataReader reader = command.ExecuteReader();
                
                //load into the result object the returned row from the database
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        person = new Person();
                        person.PersonId = Convert.ToInt32(reader["PersonId"]);
                        person.FirstName = reader["FirstName"].ToString();
                        person.MiddleName = reader["MiddleName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.PrimaryEmailAddress = reader["PrimaryEmailAddress"].ToString();
                        person.SecondaryEmailAddress = reader["SecondaryEmailAddress"].ToString();
                        person.MobileNumber = reader["MobileNumber"].ToString();
                        person.PhoneNumber = reader["PhoneNumber"].ToString();
                    }
                }
            }

            return person;
        }

        public int InsertPerson(Person personObject)
        {
            SqlConnection connection = null;
            int result = 0;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDemo"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(insertPersonQuery, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@FirstName", personObject.FirstName);
                command.Parameters.AddWithValue("@MiddleName", personObject.MiddleName);
                command.Parameters.AddWithValue("@LastName", personObject.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", personObject.PhoneNumber);
                command.Parameters.AddWithValue("@MobileNumber", personObject.MobileNumber);
                command.Parameters.AddWithValue("@PrimaryEmailAddress", personObject.PrimaryEmailAddress);
                command.Parameters.AddWithValue("@SecondaryEmailAddress", personObject.SecondaryEmailAddress);

                result = Convert.ToInt32(command.ExecuteScalar());
            }
            return result;
        }

        public int UpdatePerson(Person personObject)
        {
            SqlConnection connection = null;
            int result = 0;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDemo"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(updatePersonByIdQuery, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@PersonId", personObject.PersonId);
                command.Parameters.AddWithValue("@FirstName", personObject.FirstName);
                command.Parameters.AddWithValue("@MiddleName", personObject.MiddleName);
                command.Parameters.AddWithValue("@LastName", personObject.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", personObject.PhoneNumber);
                command.Parameters.AddWithValue("@MobileNumber", personObject.MobileNumber);
                command.Parameters.AddWithValue("@PrimaryEmailAddress", personObject.PrimaryEmailAddress);
                command.Parameters.AddWithValue("@SecondaryEmailAddress", personObject.SecondaryEmailAddress);

                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int DeletePerson(int PersonId)
        {
            SqlConnection connection = null;
            int result = 0;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDemo"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(deletePersonQuery, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@PersonId", PersonId);
                result = command.ExecuteNonQuery();
            }
            return result;
        }
    }
}