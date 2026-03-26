namespace API.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public float Review { get; set; }
        public TimeOnly Opens { get; set; }
        public TimeOnly Closes { get; set; }
        public TimeOnly OrderCutOffTime { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal DeliveyCharge { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<MenuItem>? MenuItems { get; set; }

        public Restaurant() { }
        public Restaurant(string name, string description, string address, string openingHours, TimeOnly opens, TimeOnly closes, decimal deliveyCharge, decimal minimumOrderValue, decimal serviceFee)
        {
            Name = name;
            Description = description;
            Address = address;
            Opens = opens;
            Closes = closes;
            DeliveyCharge = deliveyCharge;
            MinimumOrderValue = minimumOrderValue;
            ServiceFee = serviceFee;
        }
    }
}

