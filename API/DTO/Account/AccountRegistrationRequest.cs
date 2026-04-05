using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.DTO
{
    public class AccountRegistrationRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
        [Required, PasswordPropertyText]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}