using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BagProject.Models
{
    public class OrderLineRepository
    {
        private BagContext context;
        public OrderLineRepository(BagContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<OrderLine> OrderLines => context.OrderLines;

        public void AddOrderLine(OrderLine orderLine)
        {
            context.OrderLines.Add(orderLine);
            context.SaveChanges();
        }

        public void Update(OrderLine orderLine)
        {
            context.OrderLines.Update(orderLine);
            context.SaveChanges();
        }

        public void Delete(OrderLine orderLine)
        {
            context.OrderLines.Remove(orderLine);
            context.SaveChanges();
        }

    }
}
