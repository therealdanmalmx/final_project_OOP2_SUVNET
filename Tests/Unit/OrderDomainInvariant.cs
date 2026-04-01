using API.Models;

namespace Tests.Unit
{
    public class OrderDomainInvariant
    {
        [Fact]
        public void AddOrderItem_AddsItemToOrder()
        {
            var order = new Order("Lars Pettersson", "Torgilsgatan 16c, 506 35 Borås", "0708640976");

            order.AddOrderItem("Beef Bourguignon", 200m, 1);

            Assert.Single(order.OrderItems);
            Assert.Equal("Beef Bourguignon", order.OrderItems[0].Name);
            Assert.Equal(200m, order.OrderItems[0].Price);
            Assert.Equal(1, order.OrderItems[0].Quantity);
        }

        [Fact]
        public void AddOrderItem_AddMultipleItems()
        {
            var order = new Order("Lars Pettersson", "Torgilsgatan 16c, 506 35 Borås", "0708640976");

            order.AddOrderItem("Beef Bourguignon", 200m, 1);
            order.AddOrderItem("Boulangère Potatoes", 140m, 1);

            Assert.Equal(2, order.OrderItems.Count);
        }
    }
}