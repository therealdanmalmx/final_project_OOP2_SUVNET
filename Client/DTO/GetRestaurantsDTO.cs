using API.DTO.Review;

namespace Client.DTO
{
    public class GetRestaurantsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int MyProperty { get; set; }
        public TimeOnly Opens { get; set; }
        public TimeOnly Closes { get; set; }
        public TimeOnly OrderCutOffTime { get; set; }
        public decimal DeliveyCharge { get; set; }
        public decimal ServiceFee { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<GetMenuItemDTO>? MenuItems { get; set; }
        public List<AddNewReviewDTO> Reviews { get; set; }

    }
}