using API.Data;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("mealspicttures")]
        public async Task<IActionResult> GetMeals(string category)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync(
                $"https://www.themealdb.com/api/json/v1/1/filter.php?a={category}"
            );

            return Content(result, "application/json");
        }

        public async Task<IActionResult> GetMeals()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://www.themealdb.com/api/json/v1/1/filter.php?a=French");
            return Content(result, "application/json");
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Restaurant>> AddNewRestaurant(CreateRestaurantsDTO restaurant)
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

        [Authorize(Roles = "Admin")]
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