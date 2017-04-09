using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BagProject.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string ShippingStatus { get; set; }

        public string Id { get; set; }
        [ForeignKey("Id")]
        public AppUser AppUser { get; set; }

        public List<OrderLine> OrderLines { get; set; }

    }

}
