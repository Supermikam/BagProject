using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;

namespace BagAPI.API
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository catRepo;
        public CategoryController(ICategoryRepository repo)
        {
            catRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Category> Get() => catRepo.Categories;

    }
}