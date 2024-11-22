using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Address
    {
        public Address() { }
        public Address(string firstName, string lastName, string street, string city, string zipCode)
        {
            FirstName=firstName;
            LastName=lastName;
            Street=street;
            City=city;
            ZipCode=zipCode;
        }
            
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
    }
}
