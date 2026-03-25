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
            try
            {
                var restaurant = await _restaurantService.GetRestaurantById(id);
                return Ok(restaurant);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetRestaurantsDTO>> AddNewRestaurant(CreateRestaurantsDTO restaurant)
        {
            try
            {
                var newRestaurant = await _restaurantService.AddNewRestaurant(restaurant);
                return Ok(newRestaurant);
            }

            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetRestaurantsDTO>> UpdateRestaurant(UpdateRestaurantsDTO updateRestaurant, Guid id)
        {
            try
            {
                var restaurant = await _restaurantService.UpdateRestaurant(updateRestaurant, id);
                return Ok(restaurant);
            }

            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}