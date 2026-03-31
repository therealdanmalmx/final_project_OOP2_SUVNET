using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Account : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Role Role { get; set; }

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