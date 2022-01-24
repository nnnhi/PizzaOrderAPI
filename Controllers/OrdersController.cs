using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaDelivery.ApiModels;
using PizzaDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly ApplicationContext _context;

        public OrdersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(y => y.OrderDetails)
                .ThenInclude(z => z.Pizza)
                .ToListAsync();

            var result = orders.Select(o => GetFrom(o));

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders.Where(x => x.Id == id)
                .Include(y => y.OrderDetails)
                .ThenInclude(z => z.Pizza)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound("Order not found");
            }

            var result = GetFrom(order);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputOrderDto newOrder)
        {
            var products = _context.Pizzas.Where(x => newOrder.OrderDetails.Select(c => c.PizzaId).Contains(x.Id));
            if (products.Count() != newOrder.OrderDetails.Count)
            {
                return NotFound("Some pizzas do not exist");
            }

            var order = new Order()
            {
                CustomerName = newOrder.CustomerName,
                Address = newOrder.Address,
                Cardnumber = newOrder.Cardnumber,
                Phone = newOrder.Phone,
                OrderDate = DateTime.Now,
                PostalCode = newOrder.PostalCode,
                NoteForShipper = newOrder.NoteForShipper
            };

            foreach(var detail in newOrder.OrderDetails)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    PizzaId = detail.PizzaId,
                    Quantity = detail.Quantity,
                    AdditionalRequirement = detail.AdditionalRequirement
                });
            }

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] InputOrderDto updatedOrder)
        {
            var order = await _context.Orders.Where(x => x.Id == id)
                .Include(d => d.OrderDetails).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound("Order not found");
            }

            order.CustomerName = updatedOrder.CustomerName;
            order.Address = updatedOrder.Address;
            order.Cardnumber = updatedOrder.Cardnumber;
            order.Phone = updatedOrder.Phone;
            order.PostalCode = updatedOrder.PostalCode;

            foreach(var updateDetail in updatedOrder.OrderDetails)
            {
                var detail = order.OrderDetails.Where(x => x.PizzaId == updateDetail.PizzaId).FirstOrDefault();
                if (detail == null)
                {
                    return NotFound("Pizza not found");
                }

                detail.Quantity = updateDetail.Quantity;
                detail.AdditionalRequirement = updateDetail.AdditionalRequirement;

                _context.Update(detail);
            }


            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound("Order not found");
            }

            _context.Remove(order);
            _context.SaveChanges();

            return Ok();
        }

        private OrderDto GetFrom(Order order)
        {
            var result = new OrderDto
            {
                CustomerName = order.CustomerName,
                Phone = order.Phone,
                PostalCode = order.PostalCode,
                Address = order.Address,
                NoteForShipper = order.NoteForShipper
            };

            result.OrderDetails = order.OrderDetails.Select(d => new OrderDetailDto()
            {
                PizzaName = d.Pizza.Name,
                Quantity = d.Quantity,
                AdditionalRequirement = d.AdditionalRequirement,
                UnitPrice = Math.Round(d.Pizza.Price, 2)
            });

            return result;
        }
    }
}
