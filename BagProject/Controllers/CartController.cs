using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BagProject.Helper;
using BagProject.Models;
using BagProject.Models.ViewModels;


namespace BagProject.Controllers
{
    public class CartController : Controller
    {
        private ProductRepository _productRepo;
        private CartRepository _cart;
       
        public CartController(ProductRepository pRepo, CartRepository cartFromService)
        {
            _productRepo = pRepo;
            _cart = cartFromService;
        }

        public ViewResult ShowCart(string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }
        public void AddToCart(int productId)
        {
            Product product = _productRepo.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
            
                _cart.AddItem(product, 1);
              
            }
            //return RedirectToAction("ShowCart", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _productRepo.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                
                _cart.RemoveLine(product);
 
            }
            return RedirectToAction("ShowCart", new { returnUrl });
        }

        public RedirectToActionResult ClearCart(string returnUrl)
        {
            _cart.Clear();
            return RedirectToAction("ShowCart", new { returnUrl });

        }

       

    }
}
