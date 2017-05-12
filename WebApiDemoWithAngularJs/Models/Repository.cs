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
        static readonly string selectAllPersons = @"SELECT [PersonId],[FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber]FROM [dbo].[Person]";
        static readonly string selectPersonbyId = @"SELECT [PersonId],[FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber]FROM [dbo].[Person] where PersonId=@personId";
        static readonly string insertPerson = @"INSERT INTO [dbo].[Person]([FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber])
                                                VALUES(@FirstName,@MiddleName,@LastName,@PrimaryEmailAddress,@SecondaryEmailAddress,@PhoneNumber,@MobileNumber)";
        static readonly string updatePersonById = @"Update [dbo].[Person] SET [FirstName]=@FirstName,[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber] where PersonId=@personId";
        //static readonly string deletePerson = @"SELECT [PersonId],[FirstName],[MiddleName],[LastName],[PrimaryEmailAddress],[SecondaryEmailAddress],[PhoneNumber],[MobileNumber]FROM [dbo].[Person]";
        public IEnumerable<Person> GetAllPersons()
        {
            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDemo"].ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(selectAllPersons, connection);

            //Create DataReader for storing the returning table into server memory
            SqlDataReader dataReader = command.ExecuteReader();

            Person person = null;
            List<Person> persons = new List<Person>();

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

            return persons;
        }

        public Person GetPersonbById(int PersonId)
        {
            throw new NotImplementedException();
        }

        public int InsertPerson(Person personObject)
        {
            throw new NotImplementedException();
        }

        public int UpdatePerson(Person personObject)
        {
            throw new NotImplementedException();
        }

        public int DeletePerson(int PersonId)
        {
            throw new NotImplementedException();
        }
    }
}