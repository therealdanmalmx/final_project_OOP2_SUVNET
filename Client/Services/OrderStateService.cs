using API.DTO.Order;
using API.DTO.OrderItem;
using API.Models;
using Client.DTO;
using Client.Models;

namespace Client.Services
{
    public class OrderStateService
    {
        // This holds the object you want to pass
        public GetRestaurantsDTO? Restaurant { get; set; }
        public GetMenuItemDTO? MenuItem { get; set; }
        public CreateOrderDTO? Order { get; set; }
        public int Quantity { get; set; }

    }
}