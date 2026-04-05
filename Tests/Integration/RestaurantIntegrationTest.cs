using API.Data;
using API.Services;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Tests;
public class RestaurantIntegrationTest
{
    private AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private CreateRestaurantsDTO ValidRestaurantDTO() => new()
    {
        Name = "Test Restaurant",
        Description = "A test description",
        Address = "Test Street 1",
        Opens = new TimeOnly(10, 0),
        Closes = new TimeOnly(22, 0),
        OrderCutOffTime = new TimeOnly(21, 30),
        DeliveyCharge = 25m,
        MinimumOrderValue = 150m,
        ServiceFee = 5m
    };

    [Fact]
    public async Task AddNewRestaurant_CreatesRestaurant()
    {
        using var db = CreateInMemoryDb();
        var service = new RestaurantService(db);
        var dto = ValidRestaurantDTO();

        var result = await service.AddNewRestaurant(dto);

        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Description, result.Description);
        Assert.Equal(dto.Address, result.Address);
        Assert.Equal(dto.Opens, result.Opens);
        Assert.Equal(dto.Closes, result.Closes);
        Assert.Equal(dto.DeliveyCharge, result.DeliveyCharge);
        Assert.Equal(dto.MinimumOrderValue, result.MinimumOrderValue);
        Assert.Equal(dto.ServiceFee, result.ServiceFee);
    }

    [Fact]
    public async Task AddNewRestaurant_SaveToDatabase()
    {
        using var db = CreateInMemoryDb();
        var service = new RestaurantService(db);

        var result = await service.AddNewRestaurant(ValidRestaurantDTO());

        var saved = await db.Restaurants.FindAsync(result.Id);
        Assert.NotNull(saved);
        Assert.Equal(result.Name, saved.Name);
    }
}