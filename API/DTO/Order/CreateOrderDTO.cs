using API.DTO.OrderItem;
namespace API.DTO
{
    public class CreateOrderDTO
    {
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Instructions { get; set; } = string.Empty;
        public List<CreateOrderItemDTO> OrderItems { get; set; } = [];
        public Guid? AccountId { get; set; }
        public bool Delivery { get; set; }
        public bool CourierIsAssigned { get; set; }

    }
}