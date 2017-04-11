using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models.ViewModels
{
    public class Page
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages =>
        (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
