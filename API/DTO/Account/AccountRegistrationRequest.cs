using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.DTO.Account
{
    public class AccountRegistrationRequest
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required Role Role { get; set; }
    }
}