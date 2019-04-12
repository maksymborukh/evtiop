using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtiop.DAL.Entities
{
    class Customer
    {
        public ulong ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ImageURL { get; set; }
    }
}
