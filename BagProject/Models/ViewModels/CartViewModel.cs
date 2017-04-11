using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models.ViewModels
{
    public class CartViewModel
    {
        public CartRepository Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
