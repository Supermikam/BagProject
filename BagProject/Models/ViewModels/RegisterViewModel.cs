using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        [Required]
        [UIHint("password")]
        [Compare("Password")]
        public string ComfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter a Name for delivery")]
        public string FullName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        [Required(ErrorMessage = "Please enter a Mobil Phone Number for delivery")]
        public string MobilePhone { get; set; }
        [Required(ErrorMessage = "Please enter an address for delivery")]
        public string Address { get; set; }


        public string ReturnUrl { get; set; } = "/";

    }
}
