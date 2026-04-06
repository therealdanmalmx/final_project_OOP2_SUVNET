using API.DTO;
using API.Models;

namespace API.DTO
{
    public class GetOrderDTO
    {
        public int Number { get; set;}
        public string Name { get; } = string.Empty;
        public string Address { get; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Instructions { get;} = string.Empty;
        public Status Status { get;  }
        public List<GetOrderItemDTO>? OrderItems { get; set; }
        public bool Delivery { get; set; }
        public bool CourierIsAssigned { get; set; }

    }
}