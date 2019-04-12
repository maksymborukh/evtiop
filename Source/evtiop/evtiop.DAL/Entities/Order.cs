using System;

namespace evtiop.DAL.Entities
{
    public class Order
    {
        public ulong ID { get; set; }
        public ulong CustomerID { get; set; }
        public ulong AddressID { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DeliveryAddress { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; }
    }
}
