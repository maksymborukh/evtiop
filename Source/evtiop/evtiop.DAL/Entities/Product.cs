using System.Collections.Generic;
using System.Windows.Media;

namespace evtiop.DAL.Entities
{
    public class Product
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public long ManufacturerID { get; set; }
        public string ImageURL { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ImageSource ImageSource { get; set; }
    }
}
