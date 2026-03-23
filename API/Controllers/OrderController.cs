
using API.DTO.Order;
using API.Models;
using API.Data;
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
                return NotFound("No orders have been created");
            }

            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetOrderDTO>> GetOrderById(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if (order is null)
            {
                return NotFound("Not able to find order");
            }

            var getOrder = new Order
            {
                Id = order.Id,
                Number = order.Number,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Status = order.Status,
                CourierId = order.CourierId,
                AccountId = order.AccountId
            };

            return Ok(getOrder);
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<List<GetOrderDTO>>> GetlOrderByStatus(Status status)
        {
            var orders = await _dbContext.Orders.Where(s => s.Status == status).ToListAsync();

            if(orders is null)
            {
                return NotFound("No orders have been created");
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
                Number = (int)Random.Shared.NextInt64(1000, 100000),
                Name = newOrder.Name,
                Address = newOrder.Address,
                Phone = newOrder.Phone,
                Instructions = newOrder.Instructions,
            };

            foreach (var item in newOrder.OrderItems)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    return BadRequest("Name of the dish is required");
                }
                order.AddOrderItem(item.Name, item.Price, item.Quantity);
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return Created($"/api/order", order);
        }

        [HttpPut("{orderId}/{courierId}")]
        public async Task<IActionResult> AssignOrderToCourier(Guid orderId, Guid courierId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);

            if (order is null)
            {
                return NotFound($"Order with id {orderId} not found");
            }

            if (order.Status > Status.confirmed && order.Status < Status.courier_accepted)
            {
                return BadRequest($"Only confirmed and not yet accepted orders can be assigned to a courier");
            }

            var courier = await _dbContext.Couriers.FindAsync(courierId);

            if (courier is null)
            {
                return NotFound($"Courier with id {courierId} not found");
            }

            order.CourierId = courierId;

            await _dbContext.SaveChangesAsync();

            return Ok(order);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDTO dto)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if (order is null)
            {
                return BadRequest($"Order with id {id} does not exist");
            }

            if (!Enum.TryParse<Status>(dto.Status.ToString(), true, out var status))
            {
                return BadRequest("Invalid status value");
            }

            order.Status = status;

            await _dbContext.SaveChangesAsync();

            return Ok(order);
        }
    }
}