using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BagProject.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string ShippingStatus { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public List<OrderLine> OrderLines { get; set; }

    }

}
