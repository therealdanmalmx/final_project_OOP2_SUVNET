using API.DTO.Courier;
using API.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
        public async Task<IActionResult> GetAvailableCouriers()
        {
            var availableCouriers = await _dbContext.Couriers
                .Where(c => c.IsAvailable)
                .Select(c => new GetCourierDTO
                {
                    Name = c.Name,
                    IsAvailable = c.IsAvailable,
                    OrderDeliveries = c.OrderDeliveries.
                }).ToListAsync();

            if(!availableCouriers.Any())
            {
                return NotFound("No available couriers");
            }

            return Ok(availableCouriers);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCourier(Courier newCourier)
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