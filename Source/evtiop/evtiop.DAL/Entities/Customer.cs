using System.Collections.Generic;
using System;

namespace evtiop.DAL.Entities
{
    public class Customer
    {
        public ulong ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ImageURL { get; set; }
        public ICollection<Help> Helps { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
