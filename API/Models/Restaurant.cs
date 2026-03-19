namespace API.Models
{
    public class Restaurant
    {
        public Guid Id { get; private set; } = Guid.CreateVersion7();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public float Review { get; set; }
        public string OpeningHours { get; set; } = string.Empty;
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<MenuItem>? MenuItems { get; set; }

        public Restaurant() { }
        public Restaurant(string name, string description, string address, string openingHours, decimal minimumOrderValue, decimal serviceFee)
        {
            Name = name;
            Description = description;
            Address = address;
            OpeningHours = openingHours;
            MinimumOrderValue = minimumOrderValue;
            ServiceFee = serviceFee;
        }
    }
}

