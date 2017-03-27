using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BagProject.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string HomePhone { get; set; }
        [Required]
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }

        public List<Product> Products { get; set; }

    }
}
