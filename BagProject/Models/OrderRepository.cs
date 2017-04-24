using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BagProject.Models
{
    public class OrderRepository
    {

        private BagContext context;
        public OrderRepository(BagContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Order> Orders => context.Orders
                        .Include(o => o.OrderLines)
                            .ThenInclude(ol => ol.Product)
                        .Include(o => o.AppUser);

        public Order Find(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderID == id);
            context.Entry(order).Collection(o => o.OrderLines).Load();
            return order;
        }

        public void ShipOrder(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderID == id);
            if (order.ShippingStatus == "Shipped")
            {
                order.ShippingStatus = "Waiting";
            }
            else
            {
                order.ShippingStatus = "Shipped";
            }

            Update(order);


        }

        public List<Order> FindCustomerOrders(string custID)
        {
            var orders = new List<Order>();
            foreach (Order o in Orders.ToList())
            {
                if (o.Id == custID)
                {
                    orders.Add(o);
                }
            }
            return orders;
        }

        public void AddOrder(Order order)
        {
            context.AttachRange(order.OrderLines.Select(l => l.Product));
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void Update(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }

        public void Delete(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }
    }
}
