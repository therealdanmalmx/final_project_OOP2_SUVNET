using Client.DTO;

namespace Client.DTO
{
    public class OrderState
    {
        public GetMenuItemDTO? MenuItem { get; set; }
        public GetRestaurantsDTO? Restaurant { get; set; }
        public int Quantity { get; set; }
    }
}