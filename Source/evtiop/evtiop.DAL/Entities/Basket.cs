using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtiop.DAL.Entities
{
    public class Basket
    {
        public ulong ID { get; set; }
        public ulong CustomerID { get; set; }
        public DateTime Added { get; set; }
    }
}
