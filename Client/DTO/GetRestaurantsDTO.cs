using Models;

namespace Client.DTO
{
    public class GetRestaurantsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MyProperty { get; set; }
        public float Review { get; set; }
        public string OpeningHours { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
        public string Image { get; set; }
        public List<MenuItem>? MenuItems { get; set; }
    }
}