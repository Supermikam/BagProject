using Microsoft.AspNetCore.Mvc;
using BagProject.Models;

namespace BagProject.Component
{
    public class CartIcon: ViewComponent
    {

        private CartRepository _cart;
        public CartIcon(CartRepository cartFromService)
        {
            _cart = cartFromService;

        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
