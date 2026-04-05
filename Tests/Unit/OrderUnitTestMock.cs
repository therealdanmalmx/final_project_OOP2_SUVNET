using API.Controllers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Unit
{
    public class OrderUnitTestMock
    {
        [Fact]
        public async Task GetAllOrders_ReturnsOk()
        {
            var mockService = new Mock<IOrderService>();
            mockService.Setup(s => s.GetAllOrder()).ReturnsAsync(
            [
                new Order("Malin Andersson", "Sjöbovägen 18, 505 65 Borås", "0774562398"),
                new Order("Lars Pettersson", "Torgilsgatan 16c, 506 35 Borås", "0708640976")
            ]);

            var controller = new OrderController(mockService.Object);

            var result = await controller.GetAllOrder();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var orders = Assert.IsType<List<Order>>(ok.Value);
            Assert.Equal(2, orders.Count);

            mockService.Verify(s => s.GetAllOrder(), Times.Once);
        }

        [Fact]
        public async Task GetAllOrders_ReturnsNotFound()
        {
            var mockService = new Mock<IOrderService>();
            mockService.Setup(s => s.GetAllOrder())
                .ThrowsAsync(new ArgumentException("No orders found"));

            var controller = new OrderController(mockService.Object);

            var result = await controller.GetAllOrder();

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}