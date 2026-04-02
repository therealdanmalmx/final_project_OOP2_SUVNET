using API.Data;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Order
{
    public class OrderService : IOrderService
    {

        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.Order> AssignOrderToCourier(Guid orderId, Guid courierId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);

            if (order is null)
            {
                throw new ArgumentException($"Order with id {orderId} can't be found");
            }

            if (order.Status > Status.confirmed && order.Status < Status.courier_accepted)
            {
                throw new ArgumentException("Only confirmed and not yet accepted orders can be assigned to a courier");
            }

            if (order.Delivery)
            {
                throw new ArgumentException("Customer is picking the order up. Can't be assigned");
            }

            var courier = await _dbContext.Accounts.FindAsync(courierId.ToString());

            if (courier is null)
            {
                throw new ArgumentException($"Account with id {courierId} can't be found");
            }

            if(courier.Role != Role.Courier)
            {
                throw new ArgumentException($"Account with id {courierId} is not a courier");
            }

            order.AccountId = courierId;
            order.CourierIsAssigned = true;

            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Models.Order> CreateNewOrder(CreateOrderDTO newOrder)
        {
            if(newOrder is null)
            {
                throw new ArgumentException("Order can't be empty");
            }

            if (string.IsNullOrWhiteSpace(newOrder.Name))
            {
                throw new ArgumentException("Name can't be empty");
            }

            if (string.IsNullOrWhiteSpace(newOrder.Address))
            {
                throw new ArgumentException("Address can't be empty");
            }

            if (string.IsNullOrWhiteSpace(newOrder.Phone))
            {
                throw new ArgumentException("Phone can't be empty");
            }

            var order = new Models.Order
            {
                Number = (int)Random.Shared.NextInt64(1000, 100000),
                Name = newOrder.Name,
                Address = newOrder.Address,
                Phone = newOrder.Phone,
                Instructions = newOrder.Instructions,
                Delivery = newOrder.Delivery,
            };

            foreach (var item in newOrder.OrderItems)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    throw new ArgumentException(nameof(item.Name), "Item name can't be empty");

                }

                order.AddOrderItem(item.Name, item.Price, item.Quantity);
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<List<Models.Order>> GetAllOrder()
        {
            var orders = await _dbContext.Orders.ToListAsync();

            if (orders is null)
            {
                throw new ArgumentException("Can't find any order based on ");
            }

            return orders;
        }

        public async Task<List<Models.Order>> GetOrdersByStatus(Status status)
        {
            List<Models.Order> order = await _dbContext.Orders.Where(o => o.Status == status).ToListAsync();

            if (order is null)
            {
                throw new ArgumentException($"Can't find any orders based on status {status}");
            }

            return order;
        }

        public async Task<Models.Order> GetOrderById(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if (order is null)
            {
                throw new ArgumentException($"Can't find any orders with id {id}");
            }

            var getOrder = new Models.Order
            {
                Id = order.Id,
                Number = order.Number,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Status = order.Status,
                AccountId = order.AccountId,
                Delivery = order.Delivery
            };

            return getOrder;
        }

        public async Task<Models.Order> UpdateOrderStatus(Guid id, UpdateOrderStatusDTO dto)
        {

            var order = await _dbContext.Orders.FindAsync(id);

            if (order is null)
            {
                throw new ArgumentException($"Order with id {id} does not exist");
            }

            if(dto.Status < Status.received || dto.Status > Status.delivered)
            {
                throw new InvalidOperationException("Invalid status");
            }

            if(dto.Status -1 < order.Status || dto.Status + 1 > order.Status)
            {
                throw new ArgumentException($"Status can only be changed to '{order.Status +1}'");

            }

            order.Status = dto.Status;

            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Models.Order> DeleteOrder(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if (order is null)
            {
                throw new ArgumentException($"Order with id {id} does not exist");
            }

            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;

        }

    }
}