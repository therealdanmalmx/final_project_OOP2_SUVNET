using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.OrderItem;
using API.Models;

namespace API.DTO.Order
{
    public class GetOrderDTO
    {
        public Guid Id { get; }
        public int Number { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Address { get; init;} = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Instructions { get; init;} = string.Empty;
        public Status Status { get;  init; }
        // public List<GetOrderItemDTO> OrderItems { get; set; }
    }
}