using System;

namespace evtiop.DAL.Entities
{
    public class ProductSpecial
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public DateTime Available { get; set; }
        public DateTime ExpireOn { get; set; }
        public int Discount { get; set; }
    }
}
