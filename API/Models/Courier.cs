namespace API.Models
{
    public class Courier
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public List<Order>? Orders { get; set; }

        public Courier(string name, bool isAvailable)
        {
            Name = name;
            IsAvailable = isAvailable;
        }
    }
}