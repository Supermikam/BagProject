using BagProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagProject.Models.ViewModels;

namespace BagProject.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository productRepo;
        public int PageSize = 20;
        public ProductController(ProductRepository repo)
        {
            productRepo = repo;
        }

        public ViewResult GetAllProduct(string category, int page = 1) 
            => View(new ProductListViewModel {
                    Products = productRepo.Products
                        .Where(p => category == null || p.Category.CategoryName == category)
                        .OrderBy(p => p.ProductID)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                    Page = new Page {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null ?
                            productRepo.Products.Count() :
                            productRepo.Products.Where(e =>
                            e.Category.CategoryName == category).Count()
                    },
                    CurrentCategory = category
            });

    }
}
