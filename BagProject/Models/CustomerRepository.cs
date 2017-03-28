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
        public CustomerRepository(BagContext ctx, ILogger<CustomerRepository> logger)
        {
            context = ctx;
        }

        public IEnumerable<Customer> Customers => context.Customers;

        public Customer Find(int id)
        {
            return context.Customers.FirstOrDefault(c => c.CustomerID == id);
        }

        public Customer GetCustomerByID(int customerID)
        {
            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == customerID);
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


        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            context.Customers.Update(customer);
            context.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
        }
    }
}
