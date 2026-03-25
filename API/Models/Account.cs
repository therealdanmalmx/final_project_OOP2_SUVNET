using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Account : IdentityUser
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required Role Role { get; set; }

        public Account() { }
        public Account(string name, string address, string userName, string email, string phoneNumber, Role role)
        {
            Name = name;
            Address = address;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
        }
    }
}