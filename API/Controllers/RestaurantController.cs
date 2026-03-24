using API.Data;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<GetRestaurantsDTO>> GetAllRestaurants()
        {
            try
            {
                var existingRestaurants = await _restaurantService.GetAllRestaurants();

                return Ok(existingRestaurants);

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetRestaurantsDTO>> GetRestaurantById(Guid id)
        {
           
        }

        [HttpPost]
        public async Task<ActionResult<GetRestaurantsDTO>> AddNewRestaurant(CreateRestaurantsDTO restaurant)
        {
            var newRestaurant = new Restaurant
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                Opens = restaurant.Opens,
                Closes = restaurant.Closes,
                OrderCutOffTime = restaurant.OrderCutOffTime,
                DeliveyCharge = restaurant.DeliveyCharge,
                MinimumOrderValue = restaurant.MinimumOrderValue,
                ServiceFee = restaurant.ServiceFee
            };

            if (newRestaurant is null)
            {
                return BadRequest("Could not add a new restaurant");
            }

            _dbContext.Restaurants.Add(newRestaurant);
            await _dbContext.SaveChangesAsync();

            return Created("/api/restaurant", newRestaurant);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetRestaurantsDTO>> UpdateRestaurant(UpdateRestaurantsDTO updateRestaurant, Guid id)
        {
        try
            {
                var restaurant = await _restaurantService.UpdateRestaurantAsync(dto, id);

                var result = new GetRestaurantsDTO
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Address = restaurant.Address,
                    Opens = restaurant.Opens,
                    Closes = restaurant.Closes,
                    OrderCutOffTime = restaurant.OrderCutOffTime,
                    DeliveyCharge = restaurant.DeliveyCharge,
                    MinimumOrderValue = restaurant.MinimumOrderValue,
                    ServiceFee = restaurant.ServiceFee
                };

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GetRestaurantsDTO>> DeleteRestaurant(Guid id)
        {
            var restaurantToDelete = await _dbContext.Restaurants.FindAsync(id);

            if (restaurantToDelete is null)
            {
                return BadRequest($"No restaurant with id {id} exists");
            }

            _dbContext.Restaurants.Remove(restaurantToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}