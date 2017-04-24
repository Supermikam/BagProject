using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models.ViewModels
{
    public class UserDetailViewModel
    {
        public AppUser User { get; set; }
        public List<Order> Orders { get; set; }
    }
}
