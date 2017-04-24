using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;


//namespace BagProject.API
//{
//    [Route("api/[controller]/[action]")]
//    public class CustomerController : Controller
//    {
//        private CustomerRepository customerRepo;
//        private readonly UserManager<AppUser> _userManager;
//        private readonly SignInManager<AppUser> _signInManager;
//        public CustomerController(
//            CustomerRepository repo,
//            UserManager<AppUser> userManager,
//            SignInManager<AppUser> signInManager)
//        {
//            customerRepo = repo;
//            _signInManager = signInManager;
//            _userManager = userManager;

//        }

//        [HttpGet]
//        public IActionResult GetALLCustomers()
//        {
//            if (customerRepo.Customers != null)
//            {
//                return new ObjectResult(customerRepo.Customers);
//            }
//            return NotFound();
//        }

//        [Authorize]
//        [HttpGet(Name = "getCustomerInfo")]
//        public IActionResult GetCustomerInfo()
//        {
//            var email = User.Identity.Name;
//            var customer = customerRepo.GetCustomerInfo(email);
//            if (customer == null)
//            {
//                return NotFound();
//            }
//            return new ObjectResult(customer);
//        }

        //[Authorize]
        //[HttpPut]
        //public IActionResult Update(int id, [FromBody] Customer customer)
        //{
        //    if (customer == null || customer.CustomerID != id)
        //    {
        //        return BadRequest();
        //    }

        //    var customerToChange = customerRepo.Find(id);
        //    if (customerToChange == null)
        //    {
        //        return NotFound();
        //    }

        //    customerToChange.CustomerName = customer.CustomerName;
        //    customerToChange.HomePhone = customer.HomePhone;
        //    customerToChange.WorkPhone = customer.WorkPhone;
        //    customerToChange.MobilePhone = customer.MobilePhone;
        //    customerToChange.Address = customer.Address;
            

        //    customerRepo.Update(customerToChange);
        //    return new NoContentResult();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var customer = customerRepo.Find(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }
        //    customerRepo.Delete(customer);
        //    return new NoContentResult();
        //}
//    }
//}
