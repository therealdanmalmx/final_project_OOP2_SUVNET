using API.Models;
using API.DTO;

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