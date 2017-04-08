using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BagProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace BagProject.API
{
    public class NewUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        } 

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] NewUser newUser)
        {
            var user = new AppUser { UserName = newUser.Email, Email = newUser.Email };
            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (result.Succeeded)
            {
                return new NoContentResult();
            }
            return new BadRequestResult();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] NewUser newUser)
        {
            var result = await _signInManager.PasswordSignInAsync(newUser.Email, newUser.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return new NoContentResult();
            }
            return new BadRequestResult();
        }
    }
}