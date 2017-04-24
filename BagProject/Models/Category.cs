
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BagProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}
