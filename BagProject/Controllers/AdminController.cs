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

namespace BagProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private CustomerRepository _customerRepo;
        private OrderRepository _orderRepo;
        private SupplierRepository _supplierRepo;

        public AdminController(
             ProductRepository productRepo,
             ICategoryRepository categoryRepo,
             CustomerRepository customerRepo,
             OrderRepository orderRepo,
             SupplierRepository supplierRepo
            )
        {
            _categoryRepo = categoryRepo;
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _supplierRepo = supplierRepo;

        }

        public ViewResult Index() => View(_productRepo.Products);

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
                Suppliers = Suppliers
            });
                          
        }

        [HttpPost]
        public IActionResult EditProduct(EditProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var categoryToChange = _categoryRepo.Categories.FirstOrDefault(c => c.CategoryName == vm.Category);
                var supplierToChange = _supplierRepo.Suppliers.FirstOrDefault(s => s.SupplierName == vm.Supplier);
                vm.Product.CategoryID = categoryToChange.CategoryID;
                vm.Product.SupplierID = supplierToChange.SupplierID;
                _productRepo.Update(vm.Product);
                TempData["message"] = $"{vm.Product.ProductName} has been saved";
                return RedirectToAction("Index");
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

        //[HttpPost("UploadFiles")]
        //public async Task<IActionResult> Post(List<IFormFile> files)
        //{
        //    long size = files.Sum(f => f.Length);

        //    // full path to file in temp location
        //    var filePath = Path.GetTempFileName();

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }

        //    // process uploaded files
        //    // Don't rely on or trust the FileName property without validation.

        //    return Ok(new { count = files.Count, size, filePath });
        //}
    }
}
