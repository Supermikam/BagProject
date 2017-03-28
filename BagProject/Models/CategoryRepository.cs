using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BagProject.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private BagContext context;
        public CategoryRepository(BagContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Category> Categories => context.Categories;

        public Category Find(int id)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryID == id);
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Update(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }
    }
}
