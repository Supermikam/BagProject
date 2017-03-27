using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BagProject.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            BagContext context = app.ApplicationServices.GetRequiredService<BagContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        CategoryName = "Tote Bags"
                    },
                    new Category
                    {
                        CategoryName = "Studio Pouches"
                    },
                    new Category
                    {
                        CategoryName = "Drawstring Bags"
                    },
                    new Category
                    {
                        CategoryName = "Laptop Sleeves"
                    }
                );
                context.SaveChanges();
            }


        }
    }
}