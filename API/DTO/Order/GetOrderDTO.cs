using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.OrderItem;
using API.Models;

namespace API.DTO.Order
{
    public class GetOrderDTO
    {
        public int Number { get; }
        public string Name { get; }
        public string Address { get; }
        public string Phone { get; set; }
        public string Instructions { get;}
        public Status Status { get;  }
        public List<GetOrderItemDTO> OrderItems { get; set; }
    }
}