using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
    }
}
