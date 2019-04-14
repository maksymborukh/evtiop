using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtiop.DAL.Entities
{
    public class Basket
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public DateTime Added { get; set; }
    }
}
