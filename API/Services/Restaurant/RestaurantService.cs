using API.Data;
using API.Models;
using API.DTO;
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

            if (restaurant.ServiceFee == default)
            {
                throw new ArgumentException(nameof(restaurant.ServiceFee), "Service fee must be set");
            }

            var newRestaurant = new Restaurant
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                Address = restaurant.Address,
                Opens = restaurant.Opens,
                Closes = restaurant.Closes,
                OrderCutOffTime = restaurant.OrderCutOffTime,
                DeliveyCharge = restaurant.DeliveyCharge,
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

            if (updateRestaurant.Opens != default || updateRestaurant.Closes != default)
            {
                resturantToUpdate.Opens = updateRestaurant.Opens;
                resturantToUpdate.Closes = updateRestaurant.Closes;
            }

            if (updateRestaurant.OrderCutOffTime == default)
            {
                resturantToUpdate.OrderCutOffTime = updateRestaurant.OrderCutOffTime;
            }

            if (updateRestaurant.ServiceFee != 0.0m)
            {
                resturantToUpdate.ServiceFee = updateRestaurant.ServiceFee;
            }

            if (!string.IsNullOrWhiteSpace(updateRestaurant.Image))
            {
                resturantToUpdate.Image = updateRestaurant.Image;
            }

            _dbContext.Restaurants.Update(resturantToUpdate);
            await _dbContext.SaveChangesAsync();

            return resturantToUpdate;
        }

    }
}