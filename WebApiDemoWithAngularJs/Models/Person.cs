using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemoWithAngularJs.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmailAddress { get; set; }
        public string SecondaryEmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}