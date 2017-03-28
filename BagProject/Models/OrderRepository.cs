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
                        .Include(o => o.Customer);

        public Order Find(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderID == id);
            context.Entry(order).Collection(o => o.OrderLines).Load();
            return order;
        }

        public void AddOrder(Order order)
        {
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
