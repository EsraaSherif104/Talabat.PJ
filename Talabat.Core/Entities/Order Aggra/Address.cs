using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Core_Aggra
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string firstName, string lastName, string street, string city, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            this.street = street;
            this.city = city;
            Country = country;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string street { get; set; }

        public string city { get; set; }
        public string Country { get; set; }
    }
}
