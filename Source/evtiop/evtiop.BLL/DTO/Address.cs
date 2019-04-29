namespace evtiop.BLL.DTO
{
    public class Address
    {
        public long ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public long CustomerID { get; set; }
    }
}
