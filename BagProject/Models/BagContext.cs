using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BagProject.Models
{
    public class BagContext : DbContext
    {
        public BagContext(DbContextOptions<BagContext> options)
            : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

    }
}
