using System;

namespace evtiop.DAL.Entities
{
    public class Order
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public long AddressID { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DeliveryAddress { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; }
    }
}
