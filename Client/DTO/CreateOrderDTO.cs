using System.ComponentModel.DataAnnotations;

namespace API.DTO.Order
{
    public class CreateOrderDTO
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
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