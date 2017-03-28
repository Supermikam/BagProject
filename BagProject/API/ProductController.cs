using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BagProject.API
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private ProductRepository productRepo;
        public ProductController(ProductRepository repo)
        {
            productRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Product> Get() => productRepo.Products;

        [HttpGet("{id}", Name = "getProductByID")]
        public IActionResult GetByID(int id)
        {
            var product = productRepo.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            productRepo.AddProduct(product);
            return CreatedAtRoute("GetProductByID", new { id = product.ProductID }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (product == null || product.ProductID != id)
            {
                return BadRequest();
            }

            var productToChange = productRepo.Find(id);
            if (productToChange == null)
            {
                return NotFound();
            }

            productToChange.ProductName = product.ProductName;
            productToChange.Price = product.Price;
            productToChange.Discription = product.Discription;
            productToChange.ImageLink = product.ImageLink;
            productToChange.SupplierID = product.SupplierID;
            productToChange.CategoryID = product.CategoryID;

            productRepo.Update(productToChange);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = productRepo.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            productRepo.Delete(product);
            return new NoContentResult();
        }


    }
}
