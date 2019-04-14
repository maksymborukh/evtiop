namespace evtiop.DAL.Entities
{
    public class OrderedProduct
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public long OrderID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
