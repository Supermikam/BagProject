using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BagProject.Models
{
    public class ProductRepository
    {
        private BagContext context;
        public ProductRepository(BagContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Product> Products => context.Products;

        public Product Find(int id)
        {
            return context.Products.FirstOrDefault(p => p.ProductID == id);
        }

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

    }
}
