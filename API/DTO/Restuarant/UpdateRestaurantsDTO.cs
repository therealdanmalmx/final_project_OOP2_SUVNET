namespace DTO
{
    public class UpdateRestaurantsDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public TimeOnly Opens { get; set; }
        public TimeOnly Closes { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public decimal DeliveyCharge { get; set; }
        public decimal ServiceFee { get; set; }
    }
}