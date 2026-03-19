namespace API.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid RestaurantId { get; set; }
    }
}