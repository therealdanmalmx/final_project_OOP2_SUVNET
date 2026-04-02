using System.ComponentModel.DataAnnotations;
namespace Client.Models
{
    public class CustomerOrder
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Instructions { get; set; }
    }
}