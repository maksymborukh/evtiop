namespace evtiop.DAL.Entities
{
    public class Address
    {
        public long ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public ulong CustomerID { get; set; }
    }
}
