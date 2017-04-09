using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
            }

            context.SaveChanges();
            if (!context.Suppliers.Any())
            {
                context.Suppliers.AddRange(
                    new Supplier
                    {
                        SupplierName = "Bebe Bags",
                        HomePhone = "09-478-0000",
                        WorkPhone = "09-313-1111",
                        Email = "Bebebags@gmail.com"

                    },
                    new Supplier
                    {
                        SupplierName = "Tok Bags",
                        WorkPhone = "09-313-1111",
                        MobilePhone = "022-013-3333",
                        Email = "tokbags@gmail.com"
                    }
                    );
            }
            context.SaveChanges();
            //if (!context.Customers.Any())
            //{
            //    context.Customers.AddRange(
            //        new Customer
            //        {
            //            CustomerName = "Anny Woho",
            //            HomePhone = "09-222-2222",
            //            MobilePhone = "022-011-3333",
                 
            //            Address = "1 the road, District 1, City 1, 0000",
            //            Active = true,
                        
            //            Id = "0f9a5282-16e5-41cc-8176-25e9d3bbce91"

            //        },
            //        new Customer
            //        {
            //            CustomerName = "Billy Baby",
            //            HomePhone = "09-333-2222",
            //            MobilePhone = "022-022-3333",
                   
            //            Address = "2 the road, District 2, City 2, 1111",
            //            Active = true,
                        
            //            Id = "dbd65f7e-6122-4d58-93e4-f078d87b0afa"

            //        }
            //        );
                    
            //}
            //context.SaveChanges();

            if (!context.Products.Any())
            {
                var supplierOne = context.Suppliers.FirstOrDefault(s => s.SupplierName == "Bebe Bags");
                var supplierTwo = context.Suppliers.FirstOrDefault(s => s.SupplierName == "Tok Bags");
                var categoryOne = context.Categories.FirstOrDefault(c => c.CategoryName == "Tote Bags");
                var categoryTwo = context.Categories.FirstOrDefault(c => c.CategoryName == "Studio Pouches");
                var categoryThree = context.Categories.FirstOrDefault(c => c.CategoryName == "Drawstring Bags");
                var categoryFour = context.Categories.FirstOrDefault(c => c.CategoryName == "Laptop Sleeves");
                var productOne = new Product
                {
                    ProductName = "ballet from above",
                    Price = 19,
                    Discription = "Designed by Laura Zalenga",
                    ImageLink = "images/products/product1.jpg",
                    Category = categoryOne,
                    Supplier = supplierOne
                };
                var productTwo = new Product
                {
                    ProductName = "The Raven",
                    Price = 19,
                    Discription = "Designed by Jamie Stryker",
                    ImageLink = "images/products/product2.jpg",
                    Category = categoryTwo,
                    Supplier = supplierTwo,
                };
                var productThree = new Product
                {
                    ProductName = "Road to Nowhere",
                    Price = 26,
                    Discription = "Designed by steampunkgrub",
                    ImageLink = "images/products/product3.jpg",
                    Category = categoryThree,
                    Supplier = supplierOne,

                };
                var productFour = new Product
                {
                    ProductName = "Once Upon a Time",
                    Price = 33,
                    Discription = "Designed by nicebleed",
                    ImageLink = "images/products/product4.jpg",
                    Category = categoryFour,
                    Supplier = supplierTwo,
                };
                context.Products.AddRange(productOne, productTwo, productThree,productFour); 
            }
            context.SaveChanges();
            //if (!context.Orders.Any())
            //{
            //    var customerOne = context.Customers.FirstOrDefault(c => c.CustomerName == "Anny Woho");
            //    var customerTwo = context.Customers.FirstOrDefault(c => c.CustomerName == "Billy Baby");
            //     context.Orders.AddRange(
            //        new Order
            //        {
            //            CustomerID = customerOne.CustomerID,
            //            ShippingStatus = "Not Shipped"
            //        },
            //        new Order
            //        {
            //            CustomerID = customerTwo.CustomerID,
            //            ShippingStatus = "Not Shipped"
            //        }
            //        );

            //}
            //context.SaveChanges();
            //if (!context.OrderLines.Any())
            //{
            //    var products = context.Products.ToList();
            //    var orders = context.Orders.ToList();
            //    context.OrderLines.AddRange(
            //        new OrderLine
            //        {
            //            Product = products.ElementAt(0),
            //            Order = orders.ElementAt(0),
            //            Quantity = 1

            //        },
            //        new OrderLine
            //        {
            //            Product = products.ElementAt(1),
            //            Order = orders.ElementAt(0),
            //            Quantity = 1

            //        },
            //        new OrderLine
            //        {
            //            Product = products.ElementAt(2),
            //            Order = orders.ElementAt(1),
            //            Quantity = 2

            //        },
            //        new OrderLine
            //        {
            //            Product = products.ElementAt(3),
            //            Order = orders.ElementAt(1),
            //            Quantity = 2

            //        }
            //    );
            //}

            //context.SaveChanges();

        }
    }
}