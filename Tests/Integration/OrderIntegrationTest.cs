using API.Controllers;
using API.Data;
using API.Models;
using API.Services.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Integration
{
    public class OrderIntegrationTest
    {
        private AppDbContext CreateInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllOrders_ReturnsOk()
        {
            using var db = CreateInMemoryDb();

            db.Orders.AddRange(
                new Order("Lars Pettersson", "Torgilsgatan 16c, 506 35 Borås", "0708640976"),
                new Order("Malin Andersson", "Sjöbovägen 18, 505 65 Borås", "0774562398")
            );
            await db.SaveChangesAsync();

            var userStoreMock = new Mock<IUserStore<Account>>();
            var accountManagerMock = new Mock<UserManager<Account>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null
            );
            var service = new OrderService(db, accountManagerMock.Object);
            var controller = new OrderController(service);

            var result = await controller.GetAllOrder();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var orders = Assert.IsType<List<Order>>(ok.Value);
            Assert.Equal(2, orders.Count);
        }
    }
}