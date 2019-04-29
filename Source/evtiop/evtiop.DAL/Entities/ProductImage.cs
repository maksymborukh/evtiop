namespace evtiop.DAL.Entities
{
    public class ProductImage
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
    }
}
