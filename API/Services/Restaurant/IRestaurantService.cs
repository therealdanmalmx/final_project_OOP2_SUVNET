using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public interface IRestaurantService
    {
        Task<List<Restaurant>> GetAllRestaurants();
        Task<Restaurant> GetRestaurantById(Guid id);
        Task<Restaurant> AddNewRestaurant(CreateRestaurantsDTO restaurant);
        Task<Restaurant> UpdateRestaurant(UpdateRestaurantsDTO updateRestaurant, Guid id);
        Task<Restaurant> DeleteRestaurant(Guid id);
    }
}