
using API.DTO;
using API.Models;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrderDTO>>> GetAllOrder()
        {
            try
            {
                var orders = await _orderService.GetAllOrder();
                return Ok(orders);

            }
            catch (ArgumentException ex)
            {
                return NotFound( new {error = ex.Message});
            }

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetOrderDTO>> GetOrderById(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                return Ok(order);

            }
            catch (ArgumentException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<List<GetOrderDTO>>> GetOrdersByStatus(Status status)
        {
            try
            {
                var orders = await  _orderService.GetOrdersByStatus(status);
                return Ok(orders);

            }
            catch (ArgumentException ex)
            {
                return NotFound(new {error = ex.Message});
            }

        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateNewOrder(CreateOrderDTO newOrder)
        {
            try
            {
                var order = await _orderService.CreateNewOrder(newOrder);
                return Ok(order);

            }
            catch (ArgumentException ex)
            {
                return NotFound(new {error = ex.Message});
            }

        }

        [HttpPut("{orderId}/{courierId}")]
        public async Task<ActionResult<Order>> AssignOrderToCourier(Guid orderId, Guid courierId)
        {
            try
            {
                var courierOrder = await _orderService.AssignOrderToCourier(orderId, courierId);
                return Ok(courierOrder);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new {error = ex.Message});
            }

        }

        [HttpPut("{id:guid}/status")]
        public async Task<ActionResult<Order>> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDTO dto)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatus(id, dto);
                return Ok(updatedOrder);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new {error = ex.Message});
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new {error = ex.Message});
            }

        }
    }
}