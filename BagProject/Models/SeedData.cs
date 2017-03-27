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
                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(
                        new Customer
                        {
                            CustomerName = "Anny Woho",
                            HomePhone = "09-222-2222",
                            MobilePhone = "022-011-3333",
                            Email = "annywoho@gmail.com",
                            Address = "1 the road, District 1, City 1, 0000" 
                        },
                        new Customer
                        {
                            CustomerName = "Billy Baby",
                            HomePhone = "09-333-2222",
                            MobilePhone = "022-022-3333",
                            Email = "billybaby@gmail.com",
                            Address = "2 the road, District 2, City 2, 1111"
                        }
                        );
                    
                }

                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product
                        {
                            ProductName = "ballet from above",
                            Price = 19,
                            Discription = "Designed by Laura Zalenga",
                            ImageLink = "images/products/product1.jpg",
                            SupplierID = 1,
                            CategoryID = 1
                        },
                        new Product
                        {
                            ProductName = "The Raven",
                            Price = 19,
                            Discription = "Designed by Jamie Stryker",
                            ImageLink = "images/products/product2.jpg",
                            SupplierID = 2,
                            CategoryID = 2
                        },
                        new Product
                        {
                            ProductName = "Road to Nowhere",
                            Price = 26,
                            Discription = "Designed by steampunkgrub",
                            ImageLink = "images/products/product3.jpg",
                            SupplierID = 1,
                            CategoryID = 3
                        },
                        new Product
                        {
                            ProductName = "Once Upon a Time",
                            Price = 33,
                            Discription = "Designed by nicebleed",
                            ImageLink = "images/products/product4.jpg",
                            SupplierID = 2,
                            CategoryID = 4
                        }
                     ); 
                }
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(
                        new Order
                        {
                            CustomerID = 1,
                            ShippingStatus = "Not Shipped"
                        },
                        new Order
                        {
                            CustomerID = 2,
                            ShippingStatus = "Not Shipped"
                        }
                        );

                }

                if (!context.OrderLines.Any())
                {
                    context.OrderLines.AddRange(
                        new OrderLine
                        {
                            ProductID = 1,
                            OrderID = 1,
                            Quantity = 1
                      
                        },
                        new OrderLine
                        {
                            ProductID = 2,
                            OrderID = 1,
                            Quantity = 1

                        },
                        new OrderLine
                        {
                            ProductID = 3,
                            OrderID = 2,
                            Quantity = 2

                        },
                        new OrderLine
                        {
                            ProductID = 4,
                            OrderID = 2,
                            Quantity = 2

                        }
                        );
                }

                    context.SaveChanges();
            }
        }
    }
}