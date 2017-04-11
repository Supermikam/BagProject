using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models.ViewModels
{
    public class EditProductViewModel
    {
        public String Category{ get; set; }
        public List<SelectListItem> Categories { get; set; }
        public Product Product { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
        public String Supplier { get; set; }

    }
}
