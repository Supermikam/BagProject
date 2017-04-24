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
        [Required(ErrorMessage = "Please enter a name")]
        public string CustomerName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        [Required(ErrorMessage = "Please enter a mobile phone Number")]
        public string MobilePhone { get; set; }
        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }
        public bool Active { get; set; }


        public List<Order> Orders { get; set; }

    }
}
