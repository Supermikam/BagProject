using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BagProject.Models.ViewModels;
using BagProject.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace BagProject.Controllers
{

    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private OrderRepository _orderRepo;

        public AccountController(
            UserManager<AppUser> userMgr,
            SignInManager<AppUser> signInMgr,
            OrderRepository orderRepo)
        {
            _signInManager = signInMgr;
            _userManager = userMgr;
            _orderRepo = orderRepo;
        }


        public ViewResult Login(string returnUrl)
        {
            return View(new AdminLoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {   
                    if (user.Active)
                    {
                        await _signInManager.SignOutAsync();
                        if ((await _signInManager.PasswordSignInAsync(user,
                        vm.Password, false, false)).Succeeded)
                        {
                            if (await _userManager.IsInRoleAsync(user, "Admin"))
                            {
                                return Redirect(vm?.ReturnUrl ?? "/Admin/GetProducts");
                            }
                            else
                            {
                                return Redirect(vm?.ReturnUrl ?? "/");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your account has been locked by the Administrator. Please contact us by email.");
                        return View(vm);
                    }              
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(vm);
        }


        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public ViewResult Register(string returnUrl)
        {
            return View(new RegisterViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {

            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser
                {
                    UserName = vm.Email,
                    CustomerName = vm.FullName,
                    Email = vm.Email,
                    HomePhone = vm.HomePhone,
                    WorkPhone = vm.WorkPhone,
                    MobilePhone = vm.MobilePhone,
                    Address = vm.Address,
                    Active = true
                };

                IdentityResult result
                    = await _userManager.CreateAsync(newUser, vm.Password);

                if (result.Succeeded)
                {
                    //Send email part
                    var message = "Hi "+ vm.FullName + ",/n" + "Your account: " + newUser.Email + "/n" + "Your Password: " + vm.Password;
                    await SendEmailAsync(newUser.Email, message);


                    await _userManager.AddToRoleAsync(newUser,"Customer");
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return Redirect(vm?.ReturnUrl ?? "/");
                }
                else
                {

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        [Authorize]
        public async Task<ViewResult> GetUserDetail()
        {
            var user = await GetCurrentUserAsync();
            var fullUser = await _userManager.FindByIdAsync(user.Id);
            var orders = _orderRepo.FindCustomerOrders(user.Id);
            return View(new UserDetailViewModel
            {
                User = fullUser,
                Orders = orders
            });
        }

        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task SendEmailAsync(string email, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("QualityBags", "liuruohannah@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("NewUser", email));
            emailMessage.Subject = "Welcome to Quality Bags";
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {

                await client.ConnectAsync("smtp.sendgrid.net", 587, SecureSocketOptions.None).ConfigureAwait(false);
                client.Authenticate("apikey", "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");
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        //public async Task SendEmailAsync(string email, string message)
        //{

        //    var apiKey = "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";
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("liuruohannah@gmail.com", "QualityBags");
        //    var subject = "Welcome to Quality Bags";
        //    var to = new EmailAddress(email, "NewUser");
        //    var plainTextContent = message;
        //    var htmlContent = "<strong>we sell quality bags</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
    }
}
