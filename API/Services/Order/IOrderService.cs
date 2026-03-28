using API.DTO;
using API.Models;

namespace API.Services
{
    public interface IOrderService
    {
        Task <List<Models.Order>> GetAllOrder();

        Task <Models.Order> GetOrderById(Guid id);

        Task<List<Models.Order>> GetOrdersByStatus(Status status);

        Task<Models.Order> CreateNewOrder(CreateOrderDTO newOrder);

        Task<Models.Order> AssignOrderToCourier(Guid orderId, Guid courierId);

        Task<Models.Order> UpdateOrderStatus(Guid id, UpdateOrderStatusDTO dto);
        Task<Models.Order> DeleteOrder(Guid id);

    }
}