using System;

namespace evtiop.DAL.Entities
{
    public class Help
    {
        public ulong ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public ulong CustomerID { get; set; }
    }
}
