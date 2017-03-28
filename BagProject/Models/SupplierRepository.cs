using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BagProject.Models
{
    public class SupplierRepository
    {
        private BagContext context;
        public SupplierRepository(BagContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Supplier> Suppliers => context.Suppliers;

        public Supplier Find(int id)
        {
            return context.Suppliers.FirstOrDefault(s => s.SupplierID == id);
        }

        public void AddSupplier(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
            context.SaveChanges();
        }

        public void Update(Supplier supplier)
        {
            context.Suppliers.Update(supplier);
            context.SaveChanges();
        }

        public void Delete(Supplier supplier)
        {
            context.Suppliers.Remove(supplier);
            context.SaveChanges();
        }
    }
}
