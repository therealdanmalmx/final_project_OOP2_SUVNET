namespace API.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }

    }
}