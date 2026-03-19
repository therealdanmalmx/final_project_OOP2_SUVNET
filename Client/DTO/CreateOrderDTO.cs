using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.OrderItem;
using API.Models;

namespace API.DTO.Order
{
    public class CreateOrderDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Instructions { get; set; }
        public Guid? CourierId { get; set; }
        public Guid? AccountId { get; set; }
    }
}