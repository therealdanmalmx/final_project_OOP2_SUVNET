using System.ComponentModel.DataAnnotations;
using API.Models;

namespace Client.DTO
{
    public class CreateOrderDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(30, MinimumLength = 10,
        ErrorMessage = "Namn måste vara minst 10 bokstävder låmgt")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Adress är obligatoriskt")]
        [StringLength(50, MinimumLength = 5,
        ErrorMessage = "Adress måste vara minst 5 bokstävder låmgt")]
        public string Address { get; set; } = string.Empty;
        public int Number { get; set; }
        [Required(ErrorMessage = "Telefon är obligatoriskt")]
        [StringLength(10, MinimumLength = 10,
        ErrorMessage = "Telefonnummer skall ha 10 siffror")]
        public string Phone { get; set; } = string.Empty;
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