using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;


namespace BagProject.API
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private OrderRepository orderRepo;
        public OrderController(OrderRepository repo)
        {
            orderRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Order> Get() => orderRepo.Orders;

        [HttpGet("{id}", Name = "getOrderByID")]
        public IActionResult GetByID(int id)
        {
            var order = orderRepo.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return new ObjectResult(order);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            orderRepo.AddOrder(order);
            return CreatedAtRoute("GetOrderByID", new { id = order.OrderID }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Order order)
        {
            if (order == null || order.OrderID != id)
            {
                return BadRequest();
            }

            var orderToChange = orderRepo.Find(id);
            if (orderToChange == null)
            {
                return NotFound();
            }

            orderToChange.Id = order.Id;
            orderToChange.ShippingStatus = order.ShippingStatus;
            orderToChange.OrderLines = order.OrderLines;


            orderRepo.Update(orderToChange);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = orderRepo.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            orderRepo.Delete(order);
            return new NoContentResult();
        }
    }
}
