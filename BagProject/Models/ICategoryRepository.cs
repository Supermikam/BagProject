using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagProject.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        Category Find(int id);
        void AddCategory(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
