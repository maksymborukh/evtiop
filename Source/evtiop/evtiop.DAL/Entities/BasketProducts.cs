namespace evtiop.DAL.Entities
{
    public class BasketProducts
    {
        public ulong ProductID { get; set; }
        public ulong BasketID { get; set; }
        public int Quantity { get; set; }
    }
}
