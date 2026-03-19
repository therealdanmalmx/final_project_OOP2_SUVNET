namespace DTO
{
    public class UpdateRestaurantsDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OpeningHours { get; set; } = string.Empty;
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
    }
}