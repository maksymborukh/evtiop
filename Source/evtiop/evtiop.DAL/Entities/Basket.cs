using System;

namespace evtiop.DAL.Entities
{
    public class Basket
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public DateTime Added { get; set; }
    }
}
