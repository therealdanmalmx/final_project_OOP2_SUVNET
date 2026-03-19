
using Client.DTO;
using Client.Models;

namespace Client.Services
{
    public class OrderStateService
    {
        public GetRestaurantsDTO? Restaurant { get; set; }
        public GetMenuItemDTO? MenuItem { get; set; }
        public CreateOrderDTO? Order { get; set; }
        public int Quantity { get; set; }

    }
}