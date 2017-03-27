using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models
{
    public class OrderLine
    {
        public int Quantity { get; set; }

        public int ProductID { get; set; }
        public Product Pruduct { get; set; }

        public int OrderID { get; set; }    
        public Order Order { get; set; }



    }
}
