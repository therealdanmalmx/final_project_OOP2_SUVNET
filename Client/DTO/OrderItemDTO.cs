namespace Client.DTO
{
    public class OrderItem
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }

    }
}