using evtiop.DAL.Entities;
using System.Collections.Generic;
using System.Windows.Media;

namespace evtiop.BLL.DTO
{
    public class ProductDTO
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
