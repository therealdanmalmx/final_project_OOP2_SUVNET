using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.Order;
using API.DTO.OrderItem;
using API.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public OrderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrderDTO>>> GetAllOrder()
        {
            var orders = await _dbContext.Orders.ToListAsync();

            if(orders is null)
            {
                return BadRequest("No orders have been created");
            }

            return Ok(orders);
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<List<GetOrderDTO>>> GetAllOrder(Status status)
        {
            var orders = await _dbContext.Orders.Where(s => s.Status == status).ToListAsync();

            if(orders is null)
            {
                return BadRequest("No orders have been created");
            }

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(CreateOrderDTO newOrder)
        {
            if(newOrder is null)
            {
                return BadRequest("Order can't be empty");
            }

            if (string.IsNullOrWhiteSpace(newOrder.Name))
            {
                return BadRequest("Name is required");
            }

            if (string.IsNullOrWhiteSpace(newOrder.Address))
            {
                return BadRequest("Address is required");
            }

            if (string.IsNullOrWhiteSpace(newOrder.Phone))
            {
                return BadRequest("Phone is required");
            }

            var order = new Order
            {
                Name = newOrder.Name,
                Address = newOrder.Address,
                Phone = newOrder.Phone,
                Instructions = newOrder.Instructions,
            };

            foreach (var item in newOrder.OrderItems)
            {
                order.AddOrderItem(item.Name, item.Price, item.Quantity);
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return Created($"/api/order/{order.Id}", order);
        }
    }
}