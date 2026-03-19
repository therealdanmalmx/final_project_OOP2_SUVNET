namespace API.Models
{
    public class Courier
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public List<Order>? Orders { get; set; }
    }
}