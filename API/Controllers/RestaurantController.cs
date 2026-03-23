using API.Data;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RestaurantController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<GetRestaurantsDTO>> GetAllRestaurants()
        {
            var existingRestaurants = await _dbContext.Restaurants.Include(r => r.MenuItems).ToListAsync();

            if (existingRestaurants is null)
            {
                return NotFound("No restaurants found");
            }

            return Ok(existingRestaurants);

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetRestaurantsDTO>> GetRestaurantById(Guid id)
        {
            var restaurant = await _dbContext.Restaurants
                .Where(r => r.Id == id)
                .Select(r => new GetRestaurantsDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Category = r.Category,
                    Image = r.Image,
                    Review = r.Review,
                    Opens = r.Opens,
                    OrderCutOffTime = r.OrderCutOffTime,
                    Closes = r.Closes,
                    DeliveyCharge = r.DeliveyCharge,
                    MinimumOrderValue = r.MinimumOrderValue,
                    ServiceFee = r.ServiceFee,
                    MenuItems = r.MenuItems
                })
                .FirstOrDefaultAsync();

            if (restaurant is null)
                return NotFound($"Restaurant with id {id} not found");

            return Ok(restaurant);
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
            var resturantToUpdate = await _dbContext.Restaurants.FindAsync(id);

            if (resturantToUpdate is null)
            {
                return BadRequest($"No restaurant with id {id} exists");
            }

            if (!string.IsNullOrWhiteSpace(updateRestaurant.Name))
            {
                resturantToUpdate.Name = updateRestaurant.Name;
            }
            if (!string.IsNullOrWhiteSpace(updateRestaurant.Description))
            {
                resturantToUpdate.Description = updateRestaurant.Description;
            }
            if (!string.IsNullOrWhiteSpace(updateRestaurant.Address))
            {
                resturantToUpdate.Address = updateRestaurant.Address;
            }
            if (updateRestaurant.Opens != default)
            {
                resturantToUpdate.Opens = updateRestaurant.Opens;
            }
            if (updateRestaurant.Closes != default)
            {
                resturantToUpdate.Closes = updateRestaurant.Closes;
            }
            if (updateRestaurant.OrderCutOffTime != default)
            {
                resturantToUpdate.OrderCutOffTime = updateRestaurant.OrderCutOffTime;
            }
            if (updateRestaurant.MinimumOrderValue != 0.0m)
            {
                resturantToUpdate.MinimumOrderValue = updateRestaurant.MinimumOrderValue;
            }
            if (updateRestaurant.MinimumOrderValue != 0.0m)
            {
                resturantToUpdate.MinimumOrderValue = updateRestaurant.MinimumOrderValue;
            }
            if (updateRestaurant.ServiceFee != 0.0m)
            {
                resturantToUpdate.ServiceFee = updateRestaurant.ServiceFee;
            }

            _dbContext.Restaurants.Update(resturantToUpdate);
            await _dbContext.SaveChangesAsync();

            return Ok(resturantToUpdate);
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