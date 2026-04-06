namespace API.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid RestaurantId { get; set; }
    }
}