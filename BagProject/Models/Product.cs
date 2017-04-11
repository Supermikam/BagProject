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
        [Required(ErrorMessage = "Please enter a Name")]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter a valid integer price")]
        public decimal Price { get; set; }
        [Required (ErrorMessage = "Please enter a description")]
        public string Discription { get; set; }
        public string ImageLink { get; set; }

        
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public List<OrderLine> OrderLines { get; set; }
    }
}
