using System.ComponentModel.DataAnnotations;

namespace BagProject.Models.ViewModels
{
    public class AdminLoginViewModel 
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }

}
