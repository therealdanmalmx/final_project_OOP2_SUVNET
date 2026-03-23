using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using API.Models;

namespace Client.DTO
{
    public class CreateOrderDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public int Number { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Instructions { get; set; }
        public Status Status { get; set; }
        public Guid? CourierId { get; set; }
        public Guid? AccountId { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; } = [];

        public void AddOrderItem(string name, decimal price, int quantity)
        {
            OrderItems.Add(new CreateOrderItemDTO
            {
                Name = name,
                Price = price,
                Quantity = quantity
            });
        }
    }
}