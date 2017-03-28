using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;


namespace BagProject.API
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private CustomerRepository customerRepo;
        public CustomerController(CustomerRepository repo)
        {
            customerRepo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (customerRepo.Customers != null)
            {
                return new ObjectResult(customerRepo.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}", Name = "getCustomerByID")]
        public IActionResult GetByID(int id)
        {
            var customer = customerRepo.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            customerRepo.AddCustomer(customer);
            return CreatedAtRoute("GetCustomerByID", new { id = customer.CustomerID }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.CustomerID != id)
            {
                return BadRequest();
            }

            var customerToChange = customerRepo.Find(id);
            if (customerToChange == null)
            {
                return NotFound();
            }

            customerToChange.CustomerName = customer.CustomerName;
            customerToChange.HomePhone = customer.HomePhone;
            customerToChange.WorkPhone = customer.WorkPhone;
            customerToChange.MobilePhone = customer.MobilePhone;
            customerToChange.Email = customer.Email;
            customerToChange.Address = customer.Address;
            customerToChange.Password = customer.Password;

            customerRepo.Update(customerToChange);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = customerRepo.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customerRepo.Delete(customer);
            return new NoContentResult();
        }
    }
}
