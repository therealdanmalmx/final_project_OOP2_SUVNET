using Client.Models;

namespace Client.DTO
{
    public class GetOrderDTO
    {
        public Guid Id { get; init; }
        public int Number { get; set; }
        public string Name { get; init; } = string.Empty;
        public string Address { get; init;} = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Instructions { get; init;} = string.Empty;
        public Guid? AccountId { get; set; }
        public Status Status { get;  init; }
        public bool Delivery { get; set; }
        public List<GetOrderItemDTO> OrderItems { get; set; }
    }
}