using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;
using BagProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BagProject.Controllers
{
    
    public class OrderController : Controller       
    {
        private OrderRepository _orderRepo { set; get; }
        private CartRepository _cart { get; set; }
        private UserManager<AppUser> _userManager;

        public OrderController(OrderRepository orderRepo, CartRepository cartRepo, UserManager<AppUser> userManager)
        {
            _orderRepo = orderRepo;
            _cart = cartRepo;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<ViewResult> CheckOut()
        {
            if (_cart.Lines.Count() == 0)
            {
                var message = "Sorry, you cart is empty.";
                return View(new CheckOutViewModel
                {
                    message = message
                });
            }
            else
            {
                var user = await GetCurrentUserAsync();
                var newOrder = new Order
                {
                    ShippingStatus = "Waiting",
                    OrderLines = _cart.Lines.ToList(),
                    AppUser = user
                };

                _orderRepo.AddOrder(newOrder);
                _cart.Clear();
                var message = "Your order has been placed. Thank you for shopping with Quality Bags.";
                return View(new CheckOutViewModel
                {
                    message = message
                });
            }
        }

        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


    }
}
