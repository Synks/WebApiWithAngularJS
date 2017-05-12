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
    }
}
