using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class RestaurantService
    {

        private readonly AppDbContext _dbContext;

        public RestaurantService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<Restaurant>> AddNewRestaurant(CreateRestaurantsDTO restaurant)
        {

            if (restaurant is null)
            {
                throw new xxx;
            }

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

            try
            {
                _dbContext.Restaurants.Add(newRestaurant);
                await _dbContext.SaveChangesAsync();

                return newRestaurant;

            }
            catch (Exception ex)
            {

                throw ;
            }





        }
    }
}