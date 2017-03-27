using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;

namespace BagProject.API
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

        [HttpGet("{id}", Name = "getCategoryByID")]
        public IActionResult GetByID(int id)
        {
            var category = catRepo.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return new ObjectResult(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (category == null) {
                return BadRequest();
            }

            catRepo.AddCategory(category);
            return CreatedAtRoute("GetCategoryByID", new { id = category.CategoryID }, category);
        }

        [HttpPut ("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (category == null || category.CategoryID != id)
            {
                return BadRequest();
            }

            var categoryToChange = catRepo.Find(id);
            if (categoryToChange == null)
            {
                return NotFound();
            }

            categoryToChange.CategoryName = category.CategoryName;

            catRepo.Update(categoryToChange);
            return new NoContentResult();
        }
    }
}