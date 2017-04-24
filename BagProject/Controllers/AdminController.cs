using Microsoft.AspNetCore.Mvc;
using BagProject.Models;
using System.Linq;
using BagProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace BagProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private OrderRepository _orderRepo;
        private SupplierRepository _supplierRepo;
        private IHostingEnvironment _environment;
        private UserManager<AppUser> _userManager;

        public AdminController(
             ProductRepository productRepo,
             ICategoryRepository categoryRepo,
             OrderRepository orderRepo,
             SupplierRepository supplierRepo,
             IHostingEnvironment environment,
             UserManager<AppUser> userManager
            )
        {
            _categoryRepo = categoryRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _supplierRepo = supplierRepo;
            _environment = environment;
            _userManager = userManager;

        }

        public ViewResult GetProducts() => View(_productRepo.Products);

        public ViewResult EditProduct(int productId)
        {
            var product = _productRepo.Products
                .FirstOrDefault(p => p.ProductID == productId);
            var category = product.Category.CategoryName;
            var Categories = new List<SelectListItem>();
            foreach (var cat in _categoryRepo.Categories)
            {
                SelectListItem item = new SelectListItem { Value = cat.CategoryName, Text = cat.CategoryName };
                Categories.Add(item);
            }
            var Suppliers = new List<SelectListItem>();
            foreach (var s in _supplierRepo.Suppliers)
            {
                SelectListItem item = new SelectListItem { Value = s.SupplierName, Text = s.SupplierName };
                Suppliers.Add(item);
            }
            var supplier = product.Supplier.SupplierName;
            return View(new EditProductViewModel {
                Category = category,
                Product = product,
                Categories = Categories,
                Supplier = supplier,
                Suppliers = Suppliers,
                Image = null
            });
                          
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var categoryToChange = _categoryRepo.Categories.FirstOrDefault(c => c.CategoryName == vm.Category);
                var supplierToChange = _supplierRepo.Suppliers.FirstOrDefault(s => s.SupplierName == vm.Supplier);
                vm.Product.CategoryID = categoryToChange.CategoryID;
                vm.Product.SupplierID = supplierToChange.SupplierID;
                
                // save image and get image url
                if (vm.Image.Length > 0)
                {
                    var imageUrl = "/images/products/" + vm.Image.FileName;
                        
                    var filePath = Path.Combine(
                        _environment.WebRootPath,
                        "images",
                        "products",
                         vm.Image.FileName   
                        );
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.Image.CopyToAsync(stream);
                    }
                    vm.Product.ImageLink = imageUrl;
                }
                if (vm.Product.ProductID == 0)
                {
                    _productRepo.AddProduct(vm.Product);
                }
                else { _productRepo.Update(vm.Product); }


                TempData["message"] = $"{vm.Product.ProductName} has been saved";
                return RedirectToAction("GetProducts");
            }return View(vm);

        }

       

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var name = _productRepo.Products.First(p => p.ProductID == productId).ProductName;         
            _productRepo.Delete(productId);            
            TempData["message"] = $"{name} was deleted";           
            return RedirectToAction("Index");
        }

        public ViewResult CreateProduct() {
            var product = new Product();
            var category = _categoryRepo.Categories.First().CategoryName;
            var Categories = new List<SelectListItem>();
            foreach (var cat in _categoryRepo.Categories)
            {
                SelectListItem item = new SelectListItem { Value = cat.CategoryName, Text = cat.CategoryName };
                Categories.Add(item);
            }
            var Suppliers = new List<SelectListItem>();
            foreach (var s in _supplierRepo.Suppliers)
            {
                SelectListItem item = new SelectListItem { Value = s.SupplierName, Text = s.SupplierName };
                Suppliers.Add(item);
            }
            var supplier = _supplierRepo.Suppliers.First().SupplierName;
            return View("EditProduct",new EditProductViewModel
            {
                Category = category,
                Product = product,
                Categories = Categories,
                Supplier = supplier,
                Suppliers = Suppliers,
                Image = null
            });
        }

        public ViewResult GetUsers() => View(_userManager.Users);

        public ViewResult EditUser(string userId)
        {
            var user = _userManager.Users
                .FirstOrDefault(u => u.Id == userId);
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> SaveEditUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                AppUser userToChange = await  _userManager.FindByIdAsync(user.Id);
                userToChange.Email = user.Email;
                userToChange.CustomerName = user.CustomerName;
                userToChange.HomePhone = user.HomePhone;
                userToChange.WorkPhone = user.WorkPhone;
                userToChange.MobilePhone = user.MobilePhone;
                userToChange.Address = user.Address;
                IdentityResult result = await _userManager.UpdateAsync(userToChange);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Changes of {user.CustomerName} was saved";
                    return RedirectToAction("GetUsers");
                }
                else
                {
                    TempData["message"] = result.Errors.ToString();
                    return View("EditUser", userToChange);
                }
   
            }
            return View("EditUser", user);
        }

        public async Task<IActionResult> ChangeUserState(string userId)
        {
            AppUser userToChange = await _userManager.FindByIdAsync(userId);
            userToChange.Active = !userToChange.Active;
            await _userManager.UpdateAsync(userToChange);
            return RedirectToAction("GetUsers");
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            AppUser userToDelete = await _userManager.FindByIdAsync(userId);
            TempData["message"] = $"{userToDelete.CustomerName} was Deleted";
            await _userManager.DeleteAsync(userToDelete);           
            return RedirectToAction("GetUsers");
        }

        public ViewResult GetOrders() => View(_orderRepo.Orders);

        public IActionResult ChangeOrderState(int orderId)
        {
            _orderRepo.ShipOrder(orderId);
            return RedirectToAction("GetOrders");

        }

        public ViewResult GetCategories() => View(_categoryRepo.Categories);

        public ViewResult EditCategory(int catId)
        {
            var cat = _categoryRepo.Find(catId);
            return View(cat);
        }

        public IActionResult SaveEditCategory(Category cat)
        {
            if (ModelState.IsValid)
            {
                if (cat.CategoryID == 0)
                {
                    _categoryRepo.AddCategory(cat);
                    TempData["message"] = $"{cat.CategoryName} was created";
                    return RedirectToAction("GetCategories");
                }
                else
                {
                    _categoryRepo.Update(cat);
                    TempData["message"] = $"Changes to {cat.CategoryName} was saved";
                    return RedirectToAction("GetCategories");

                }
            }

            return View("EditCategory", cat);
            

        }

        public IActionResult DeleteCategory(int catId)
        {
            var cat = _categoryRepo.Categories.First(c => c.CategoryID == catId);
            TempData["message"] = $"{cat.CategoryName} was deleted";
            _categoryRepo.Delete(cat);      
            return RedirectToAction("GetCategories");
        }

        public ViewResult CreateCategory()
        {
            return View("EditCategory", new Category());
        }

        public ViewResult GetSuppliers() => View(_supplierRepo.Suppliers);

        public ViewResult EditSupplier(int supplierId)
        {
            var supplier = _supplierRepo.Find(supplierId);
            return View(supplier);
        }

        public IActionResult SaveEditSupplier(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.SupplierID == 0)
                {
                    _supplierRepo.AddSupplier(supplier);
                    TempData["message"] = $"{supplier.SupplierName} was created";
                    return RedirectToAction("GetSuppliers");
                }
                else
                {
                    _supplierRepo.Update(supplier);
                    TempData["message"] = $"Changes to {supplier.SupplierName} was saved";
                    return RedirectToAction("GetSuppliers");

                }

            }
            return View("EditSupplier", supplier);
        }

        public IActionResult DeleteSupplier(int supplierId)
        {
            var supplier = _supplierRepo.Suppliers.FirstOrDefault(s => s.SupplierID == supplierId);
            TempData["message"] = $"{supplier.SupplierName} was deleted";
            _supplierRepo.Delete(supplier);
            return RedirectToAction("GetSuppliers");
        }

        public ViewResult CreateSupplier()
        {
            return View("EditSupplier", new Supplier());
        }

    }
}
