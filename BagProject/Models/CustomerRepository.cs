using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BagProject.Models
{
    public class CustomerRepository
    {
        private BagContext context;
        public CustomerRepository(BagContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<AppUser> Customers => context.AppUsers;

        public AppUser Find(string email)
        {
            return context.AppUsers.FirstOrDefault(au => au.Email == email);
        }

        public AppUser GetCustomerInfo(string email)
        {
            var customer = context.AppUsers.FirstOrDefault(au => au.Email == email);
            context.Entry(customer).Collection(c => c.Orders).Load();
            var orders = customer.Orders.ToList();

            if (orders.Any())
            {
                foreach (Order o in orders)
                {
                    context.Entry(o).Collection(od => od.OrderLines).Load();
                }
            }
            return customer;
        }


        public void Update(AppUser customer)
        {
            context.AppUsers.Update(customer);
            context.SaveChanges();
        }

        public void Delete(AppUser customer)
        {
            context.AppUsers.Remove(customer);
            context.SaveChanges();
        }
    }
}
