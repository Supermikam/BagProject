using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Page Page { get; set; }
        public string CurrentCategory { get; set; }
    }
}
