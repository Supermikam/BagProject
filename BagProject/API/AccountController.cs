using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BagProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Linq;

//namespace BagProject.API
//{
//    public class NewUser
//    {
//        public string Email { get; set; }
//        public string Password { get; set; }
//        public string CustomerName { get; set; }
//        public string HomePhone { get; set; }
//        public string WorkPhone { get; set; }
//        public string MobilePhone { get; set; }
//        public string Address { get; set; }
//        public bool Active { get; set; }
//    }

//    public class LoginUser
//    {
//        public string Email { get; set; }
//        public string Password { get; set; }
//    }

//    [Route("api/[controller]/[action]")]
//    public class AccountController : Controller
//    {
//        public static readonly string secretKey = "mysupersecret_secretkey!123";
//        private readonly UserManager<AppUser> _userManager;
//        private readonly SignInManager<AppUser> _signInManager;
//       // private CustomerRepository _customerRepo;

//        public AccountController(
//        UserManager<AppUser> userManager,
//        SignInManager<AppUser> signInManager
//        //,CustomerRepository customerRepo
//        )
//        {
//            _signInManager = signInManager;
//            _userManager = userManager;
//            //_customerRepo = customerRepo;
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> Register([FromBody] NewUser newUser)
//        {
//            var user = new AppUser {
//                UserName = newUser.Email,
//                Email = newUser.Email,
//                Active = newUser.Active,
//                CustomerName = newUser.CustomerName,
//                HomePhone = newUser.HomePhone,
//                WorkPhone = newUser.WorkPhone,
//                MobilePhone = newUser.MobilePhone,
//                Address = newUser.Address
//            };
//            var result = await _userManager.CreateAsync(user, newUser.Password);
//            if (result.Succeeded)
//            {
//                return new NoContentResult();
//            }
//            return new BadRequestResult();

//        }

//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login([FromBody] LoginUser newUser)
//        {
//            var email = newUser.Email;
//            var password = newUser.Password;
//            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
//            if (result.Succeeded)
//            {
//                var user = await _userManager.FindByEmailAsync(email);
//                var userClaims = await _userManager.GetClaimsAsync(user);
//                var identity = new ClaimsIdentity(new GenericIdentity(email, "Token"), userClaims);
//                var now = DateTime.UtcNow;
//                var claims = new Claim[]
//                {
//                    new Claim(JwtRegisteredClaimNames.Sub, email),
//                    new Claim(JwtRegisteredClaimNames.Jti, await NonceGenerator()),
//                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
//                };

//                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
//                var exp = TimeSpan.FromMinutes(30);

//                var jwt = new JwtSecurityToken(
//                issuer: "Quality Bags",
//                audience: "App User",
//                claims: claims,
//                notBefore: now,
//                expires: now.Add(exp),
//                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

//                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

//                var response = new
//                {
//                    access_token = encodedJwt,
//                    expires_in = (int)exp.TotalSeconds
//                };

//                return new ObjectResult(response);
//            }
//            return new BadRequestResult();
//        }

//        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();

//        public Func<Task<string>> NonceGenerator { get; set; }
//            = new Func<Task<string>>(() => Task.FromResult(Guid.NewGuid().ToString()));


//        [Authorize]
//        [HttpGet]
//        public object Trial()
//        {
//            //return User.Claims.Select(c =>
//            //new
//            //{
//            //    Type = c.Type,
//            //    Value = c.Value
//            //});
//            return User;


//        }
//    }
//}
