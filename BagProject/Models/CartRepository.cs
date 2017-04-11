using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using BagProject.Helper;
using System.Collections.Generic;
using System.Linq;

namespace BagProject.Models
{
    public class CartRepository
    {
     
        public CartRepository()
        {
            
        }

        private List<OrderLine> OrderLines = new List<OrderLine>();
        [JsonIgnore]
        public ISession Session { get; set; }

        public static CartRepository GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            CartRepository cart = session?.GetJson<CartRepository>("Cart")
            ?? new CartRepository();
            cart.Session = session;
            return cart;
        }

        

        public virtual void AddItem(Product product, int quantity)
        {
            OrderLine potentialLine = OrderLines
                .Where(l => l.Product.ProductID == product.ProductID)
                .FirstOrDefault();
               
            if (potentialLine == null)
            {
                OrderLines.Add(new OrderLine
                {
                    Product = product,
                    Quantity = quantity
                });

            }           
            else
            {
                potentialLine.Quantity += quantity;
            }

            Session.SetJson("Cart", this);
        }

        public virtual void RemoveLine(Product product)
        {
            OrderLines.RemoveAll(l => l.Product.ProductID == product.ProductID);
            Session.SetJson("Cart", this);
        }
        public virtual decimal ComputeTotalValue() 
            => OrderLines.Sum(l => l.Product.Price * l.Quantity);

        public virtual string ComputeGST()
        {
            decimal total = ComputeTotalValue();
            
            var GST = Convert.ToDouble(total) * 0.15;
            return GST.ToString("c{0:0.00}");


        }
            

        public virtual void Clear()
        {
            OrderLines.Clear();
            Session.SetJson("Cart", this);
        }

        public virtual IEnumerable<OrderLine> Lines => OrderLines;
    }
}
