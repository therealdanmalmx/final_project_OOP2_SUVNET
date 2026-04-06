using API.DTO;
using API.DTO.OrderItem;
using API.Models;
using API.Services.Order;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests;
public class OrderTests
{
    private readonly OrderService _service;

    public OrderTests()
    {
        var userStoreMock = new Mock<IUserStore<Account>>();
        var accountManagerMock = new Mock<UserManager<Account>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null
        );

        _service = new OrderService(null!, accountManagerMock.Object);
    }

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