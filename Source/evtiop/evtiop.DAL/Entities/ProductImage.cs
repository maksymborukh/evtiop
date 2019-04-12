using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtiop.DAL.Entities
{
    public class ProductImage
    {
        public ulong ID { get; set; }
        public ulong ProductID { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
    }
}
