using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;

namespace BagProject.API
{
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private SupplierRepository supplierRepo;
        public SupplierController(SupplierRepository repo)
        {
            supplierRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Supplier> Get() => supplierRepo.Suppliers;

        [HttpGet("{id}", Name = "getSupplierByID")]
        public IActionResult GetByID(int id)
        {
            var supplier = supplierRepo.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return new ObjectResult(supplier);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }

            supplierRepo.AddSupplier(supplier);
            return CreatedAtRoute("GetSupplierByID", new { id = supplier.SupplierID}, supplier);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Supplier supplier)
        {
            if (supplier == null || supplier.SupplierID != id)
            {
                return BadRequest();
            }

            var supplierToChange = supplierRepo.Find(id);
            if (supplierToChange == null)
            {
                return NotFound();
            }

            supplierToChange.SupplierName = supplier.SupplierName;
            supplierToChange.HomePhone = supplier.HomePhone;
            supplierToChange.MobilePhone = supplier.MobilePhone;
            supplierToChange.WorkPhone = supplier.WorkPhone;
            supplierToChange.Email = supplier.Email;

            supplierRepo.Update(supplierToChange);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var supplier = supplierRepo.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            supplierRepo.Delete(supplier);
            return new NoContentResult();
        }

    }
}
