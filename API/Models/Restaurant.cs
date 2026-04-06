namespace API.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public TimeOnly Opens { get; set; }
        public TimeOnly Closes { get; set; }
        public TimeOnly OrderCutOffTime { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal DeliveyCharge { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<MenuItem>? MenuItems { get; set; }
        public List<Review>? Reviews { get; set; }

        public Restaurant() { }
        public Restaurant(string name, string description, string address, TimeOnly opens, TimeOnly closes, TimeOnly orderCutOffTime, decimal deliveyCharge, decimal serviceFee)
        {
            Name = name;
            Description = description;
            Address = address;
            Opens = opens;
            Closes = closes;
            OrderCutOffTime = orderCutOffTime;
            DeliveyCharge = deliveyCharge;
            ServiceFee = serviceFee;
        }
    }
}

