using API.Services;
using DTO;

namespace Tests;
public class RestaurantUnitTests
{
    private readonly RestaurantService _service = new(null!);
    private CreateRestaurantsDTO ValidRestaurantDTO() => new()
    {
        Name = "Test Restaurant",
        Description = "A test description",
        Address = "Test Street 1",
        Opens = new TimeOnly(10, 0),
        Closes = new TimeOnly(22, 0),
        OrderCutOffTime = new TimeOnly(21, 30),
        DeliveyCharge = 25m,
        ServiceFee = 5m
    };

    [Fact]
    public async Task AddNewRestaurant_WithoutInput()
    {
        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(null!));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_NameMissing()
    {
        var dto = ValidRestaurantDTO();
        dto.Name = "";

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_DescriptionMissing()
    {
        var dto = ValidRestaurantDTO();
        dto.Description = "";

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_AddressMissing()
    {
        var dto = ValidRestaurantDTO();
        dto.Address = "";

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_OpensIsDefault()
    {
        var dto = ValidRestaurantDTO();
        dto.Opens = default;

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_ClosesIsDefault()
    {
        var dto = ValidRestaurantDTO();
        dto.Closes = default;

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_OrderCutOffTimeIsDefault()
    {
        var dto = ValidRestaurantDTO();
        dto.OrderCutOffTime = default;

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_DeliveryChargeIsNull()
    {
        var dto = ValidRestaurantDTO();
        dto.DeliveyCharge = 0;

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task AddNewRestaurant_ServiceFeeIsNull()
    {
        var dto = ValidRestaurantDTO();
        dto.ServiceFee = 0;

        var exception = await Record.ExceptionAsync(() =>
            _service.AddNewRestaurant(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }
}