namespace evtiop.DAL.Entities
{
    public class OrderedProduct
    {
        public ulong ID { get; set; }
        public ulong ProductID { get; set; }
        public ulong OederID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
