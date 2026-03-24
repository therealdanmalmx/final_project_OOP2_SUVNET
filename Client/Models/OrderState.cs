using Client.DTO;

namespace Client.Models
{
    public class OrderState
    {
        public GetMenuItemDTO? MenuItem { get; set; }
        public GetRestaurantsDTO? Restaurant { get; set; }
        public int Quantity { get; set; }
    }
}