using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class AccountRegistrationRequest
    {
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(30, MinimumLength = 10,
        ErrorMessage = "Namn måste vara minst 10 bokstävder långt")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address är obligatoriskt")]
        [StringLength(50, MinimumLength = 10,
        ErrorMessage = "Address måste vara minst 10 bokstävder långt")]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email är obligatoriskt"), EmailAddress(ErrorMessage = "Emailhar inte rätt format.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(10, MinimumLength = 10,
        ErrorMessage = "Telefonnummer måste vara 10 siffror långt")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}