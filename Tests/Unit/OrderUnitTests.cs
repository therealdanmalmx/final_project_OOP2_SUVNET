using API.DTO;
using API.DTO.OrderItem;
using API.Services.Order;

namespace Tests;
public class OrderTests
{
    private readonly OrderService _service = new(null!);

    private CreateOrderDTO ValidOrderDTO() => new()
    {

        Name = "Lars Pettersson",
        Address = "Torgilsgatan 16c, 506 35 Borås",
        Phone = "0708640976",
        OrderItems =
        [
            new CreateOrderItemDTO { Name = "Beef Bourguignon", Price = 200m, Quantity = 1 }
        ]
    };

   [Fact]
    public async Task CreateNewOrder_WithoutInput()
    {
        var exception = await Record.ExceptionAsync(() =>
            _service.CreateNewOrder(null!));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task CreateNewOrder_NameMissing()
    {
        var dto = ValidOrderDTO();
        dto.Name = "";

        var exception = await Record.ExceptionAsync(() =>
            _service.CreateNewOrder(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task CreateNewOrder_AddressMissing()
    {
        var dto = ValidOrderDTO();
        dto.Address = "";

        var exception = await Record.ExceptionAsync(() =>
            _service.CreateNewOrder(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task CreateNewOrder_PhoneMissing()
    {
        var dto = ValidOrderDTO();
        dto.Phone = "";

        var exception = await Record.ExceptionAsync(() =>
            _service.CreateNewOrder(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task CreateNewOrder_OrderItemNameMissing()
    {
        var dto = ValidOrderDTO();
        dto.OrderItems[0].Name = "";

        var exception = await Record.ExceptionAsync(() =>
            _service.CreateNewOrder(dto));

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

}