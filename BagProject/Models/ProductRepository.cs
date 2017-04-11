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

        public IEnumerable<Product> Products => context.Products
                                                .Include(product => product.Category)
                                                .ToList();

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
            var productToChange = Find(product.ProductID);
            if (productToChange != null) {
                productToChange.ProductName = product.ProductName;
                productToChange.Price = product.Price;
                productToChange.Discription = product.Discription;
                productToChange.CategoryID = product.CategoryID;
                productToChange.SupplierID = product.SupplierID;
                context.Products.Update(productToChange);
                context.SaveChanges();
            }
            
        }

        public void Delete(int id)
        {
            var productToDelet = Find(id);
            if(productToDelet != null)
            {
                context.Products.Remove(productToDelet);
                context.SaveChanges();
            }      
        }

    }
}
