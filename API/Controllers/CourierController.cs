using API.DTO;
using API.DTO.Order;
using API.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourierController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public CourierController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<GetCourierDTO>> GetAvailableCouriers()
        {
            var availableCouriers = await _dbContext.Couriers
                .Where(c => c.IsAvailable)
                .Select(c => new GetCourierDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsAvailable = c.IsAvailable,
                    Orders = c.Orders.Select(o => o.Id).ToList()
                }).ToListAsync();

            if(!availableCouriers.Any())
            {
                return NotFound("No available couriers");
            }

            return Ok(availableCouriers);
        }

        [HttpPost]
        public async Task<ActionResult<CreateCourierDTO>> AddNewCourier(Courier newCourier)
        {
            if (newCourier is null)
            {
                return BadRequest("Courier can't be empty");
            }

            _dbContext.Couriers.Add(newCourier);
            await _dbContext.SaveChangesAsync();

            return Created("/api/courier", newCourier);
        }
    }
}