using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

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
            var existingRestaurants = await _dbContext.Restaurants.ToListAsync();

            if (existingRestaurants is null)
            {
                return BadRequest("No restaurants have been added");
            }

            return Ok(existingRestaurants);
        }

        [HttpPost]
        public async Task<ActionResult<GetRestaurantsDTO>> AddNewRestaurant(CreateRestaurantsDTO restaurant)
        {
            var newRestaurant = new Restaurant
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                OpeningHours = restaurant.OpeningHours,
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
            if (!string.IsNullOrWhiteSpace(updateRestaurant.OpeningHours))
            {
                resturantToUpdate.OpeningHours = updateRestaurant.OpeningHours;
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
    }
}