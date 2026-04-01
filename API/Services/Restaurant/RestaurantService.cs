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
                throw new NullReferenceException("Restaurant can't be empty");
            }

            if (string.IsNullOrWhiteSpace(restaurant.Name))
            {
                throw new ArgumentException(nameof(restaurant.Name), "Name must be set");
            }

            if (string.IsNullOrWhiteSpace(restaurant.Description))
            {
                throw new ArgumentException(nameof(restaurant.Description), "Description must be");
            }

            if (string.IsNullOrWhiteSpace(restaurant.Address))
            {
                throw new ArgumentException(nameof(restaurant.Address), "Address must be set");
            }

            if (restaurant.Opens == default || restaurant.Closes == default)
            {
                throw new ArgumentException("Opening hourse must be set");
            }

            if (restaurant.OrderCutOffTime == default)
            {
                throw new ArgumentException(nameof(restaurant.OrderCutOffTime), "Order cutoff time must be set");

            }

            if (restaurant.DeliveyCharge == default)
            {
                throw new ArgumentException(nameof(restaurant.DeliveyCharge), "Delivery charge must be set");
            }

            if (restaurant.MinimumOrderValue == default)
            {
                throw new ArgumentException(nameof(restaurant.MinimumOrderValue), "Minimum order value must be set");
            }

            if (restaurant.ServiceFee == default)
            {
                throw new ArgumentException(nameof(restaurant.ServiceFee), "Service fee must be set");
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

            if (newRestaurant is null)
            {
                throw new ArgumentException(nameof(newRestaurant), "You can't create a restaurant withour fillin in all details");
            }

                _dbContext.Restaurants.Add(newRestaurant);
                await _dbContext.SaveChangesAsync();

                return newRestaurant;
        }

        public async Task<Restaurant> DeleteRestaurant(Guid id)
        {
            var restaurantToDelete = await _dbContext.Restaurants.FindAsync(id);

            if (restaurantToDelete is null)
            {
                throw new NotImplementedException();
            }

            _dbContext.Restaurants.Remove(restaurantToDelete);
            await _dbContext.SaveChangesAsync();

            return restaurantToDelete;
        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            List<Restaurant> restaurants = await _dbContext.Restaurants.ToListAsync();

            if (restaurants is null)
            {
                throw new NotImplementedException();
            }

            return restaurants;
        }

        public async Task<Restaurant> GetRestaurantById(Guid id)
        {
            var restaurant = await _dbContext.Restaurants
                .Where(r => r.Id == id)
                .Select(r => new Restaurant
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Category = r.Category,
                    Image = r.Image,
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
                {
                    throw new ArgumentNullException(nameof(restaurant), "Resturant can't have empty fields");
                }

                return restaurant;
        }

        public async Task<Restaurant> UpdateRestaurant(UpdateRestaurantsDTO updateRestaurant, Guid id)
        {
            var resturantToUpdate = await _dbContext.Restaurants.FindAsync(id);

            if (resturantToUpdate is null)
            {
                throw new ArgumentNullException(nameof(resturantToUpdate), $"Restaurant can't be empty.");
            }

            if (string.IsNullOrWhiteSpace(updateRestaurant.Name))
            {
                throw new ArgumentException(nameof(updateRestaurant.Name), "Name must be set");
            }
            resturantToUpdate.Name = updateRestaurant.Name;

            if (string.IsNullOrWhiteSpace(updateRestaurant.Description))
            {
                throw new ArgumentException(nameof(updateRestaurant.Description), "Description must be set");
            }
            resturantToUpdate.Description = updateRestaurant.Description;

            if (string.IsNullOrWhiteSpace(updateRestaurant.Address))
            {
                throw new ArgumentException(nameof(updateRestaurant.Address), "Address must be set");
            }
            resturantToUpdate.Address = updateRestaurant.Address;

            if (updateRestaurant.Opens == default || updateRestaurant.Closes == default)
            {
                throw new ArgumentException("Opening hour must be set");
            }
            resturantToUpdate.Opens = updateRestaurant.Opens;
            resturantToUpdate.Closes = updateRestaurant.Closes;

            if (updateRestaurant.OrderCutOffTime != default)
            {
                throw new ArgumentException(nameof(updateRestaurant.OrderCutOffTime), "Order cutoff time must be set");
            }
                resturantToUpdate.OrderCutOffTime = updateRestaurant.OrderCutOffTime;

            if (updateRestaurant.MinimumOrderValue == 0.0m)
            {
                throw new ArgumentException(nameof(updateRestaurant.MinimumOrderValue), "Minimum order value must be set");
            }
            resturantToUpdate.MinimumOrderValue = updateRestaurant.MinimumOrderValue;

            if (updateRestaurant.ServiceFee == 0.0m)
            {
                throw new ArgumentException(nameof(updateRestaurant.ServiceFee), "Service fee must be set");
            }
            resturantToUpdate.ServiceFee = updateRestaurant.ServiceFee;

            _dbContext.Restaurants.Update(resturantToUpdate);
            await _dbContext.SaveChangesAsync();

            return resturantToUpdate;
        }

    }
}