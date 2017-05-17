using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebApiDemoWithAngularJs.Models;

namespace WebApiDemoWithAngularJs.Controllers
{
    public class PersonController : ApiController
    {
        PersonRepository objRepository = new PersonRepository();
        public List<Person> GetPersons()
        {
            var persons=objRepository.GetAllPersons();
            return persons.ToList();
        }

        public Person PostPerson(Person person)
        {
            var result = objRepository.InsertPerson(person);
            if (result>1)
            {
                return objRepository.GetPersonbById(result); ;
            }
            return new Person();
        }

        public List<Person> PutPerson(Person person)
        {
            var result = objRepository.UpdatePerson(person);
            return GetPersons();
        }

        public void DeletePerson(int personId)
        {
            objRepository.DeletePerson(personId);
        }

    }
}
