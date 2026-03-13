using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.DTO.Order
{
    public class CreateOrderDTO
    {
        public int Number { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? Instructions { get; set; }
        public Guid? CourierId { get; set; }
        public Guid? AccountId { get; set; }
    }
}