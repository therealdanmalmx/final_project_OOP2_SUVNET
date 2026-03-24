using API.Data;
using API.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
namespace API.Services
{
    public class RestaurantService : IRestaurantService
    {

        private readonly AppDbContext _dbContext;

        public RestaurantService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Restaurant> AddNewRestaurant(CreateRestaurantsDTO restaurant)
        {

            if (restaurant is null)
            {
                throw new ArgumentNullException();
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


            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occured: {ex}");
            }

            return newRestaurant;

        }

        public Task<GetRestaurantsDTO> DeleteRestaurant(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetRestaurantsDTO> GetAllRestaurants()
        {
            throw new NotImplementedException();
        }

        public async Task<Restaurant> GetRestaurantById(Guid id)
        {
            try
            {
                var restaurant = await _dbContext.Restaurants
                    .Where(r => r.Id == id)
                    .Select(r => new Restaurant
                    {
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

                return restaurant;

            }
            catch (System.Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<Restaurant> UpdateRestaurant(UpdateRestaurantsDTO updateRestaurant, Guid id)
        {
             var resturantToUpdate = await _dbContext.Restaurants.FindAsync(id);

            try
            {
                if (resturantToUpdate is null)
                {
                    throw new KeyNotFoundException($"No restaurant with id {id} exists.");
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

                return resturantToUpdate;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("The restaurant was updated or removed by another process.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Could not save restaurant changes to the database.", ex);
            }
        }
    }
}