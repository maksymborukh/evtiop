using System;

namespace evtiop.DAL.Entities
{
    public class Help
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CustomerID { get; set; }
    }
}
