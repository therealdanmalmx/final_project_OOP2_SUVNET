using System.Diagnostics.CodeAnalysis;

namespace API.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? Instructions { get; set; }
        public Status Status { get; set; } = Status.received;
        public Guid? AccountId { get; set; }
        public Account? Account { get; set; }
        public bool Delivery { get; set; }
        public bool CourierIsAssigned { get; set; }
        public List<OrderItem> OrderItems { get; private set; } = [];

        public void AddOrderItem(string name, decimal price, int quantity)
        {
            var item = new OrderItem {Name = name, Price = price, Quantity = quantity };
            OrderItems.Add(item);
        }

        public Order() { }
        [SetsRequiredMembers]
        public Order(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
        }
    }
}