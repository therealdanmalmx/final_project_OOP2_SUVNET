using Models;
namespace DTO
{
    public class GetRestaurantsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public float Review { get; set; }
        public string OpeningHours { get; set; } = string.Empty;
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<MenuItem>? MenuItems { get; set; }



    }
}