using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BagProject.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Discription { get; set; }
        public string ImageLink { get; set; }

        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public List<OrderLine> OrderLines { get; set; }
    }
}
