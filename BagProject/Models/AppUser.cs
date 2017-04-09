using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models
{
    public class AppUser: IdentityUser
    {
    
        public string CustomerName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        [Required]
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }


        public List<Order> Orders { get; set; }

    }
}
