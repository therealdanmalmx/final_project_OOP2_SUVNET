using API.DTO;
using API.DTO.Account;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountRegisterService : IAccountRegisterService
    {
        private readonly UserManager<Account> _accountManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRegisterService(UserManager<Account> accountManager, RoleManager<IdentityRole> roleManager)
        {
            _accountManager = accountManager;
            _roleManager = roleManager;
        }

        public async Task<AccountRegistrationResponse> RegisterAccount(AccountRegistrationRequest request)
        {
            var newAccount = new Models.Account
            {
                Name = request.Name,
                Address = request.Address,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
            };

            var result = await _accountManager.CreateAsync(newAccount, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return new AccountRegistrationResponse(false, errors);
            }

            return new AccountRegistrationResponse(true);
        }

        public async Task<List<AccountRequestDTO>> GetAllAccounts()
        {
            if (_accountManager.Users is null)
            {
                return [];
            }

            return await _accountManager.Users.Select(u => new AccountRequestDTO
            {
                Name = u.Name,
                Address = u.Address,
                PhoneNumber = u.PhoneNumber!,
                Email = u.Email!,
                UserName = u.UserName!,
            }).ToListAsync();
        }
        public async Task<AccountRequestDTO> GetAccountById(Guid id)
        {
             var account = await _accountManager.Users
                .Where(u => u.Id == id.ToString())
                .Select(u => new AccountRequestDTO
                {
                    Name = u.Name,
                    Address = u.Address,
                    PhoneNumber = u.PhoneNumber!,
                    Email = u.Email!,
                    UserName = u.UserName!,
                }).FirstOrDefaultAsync();

                if (account is null)
                {
                    Console.WriteLine("No account");
                }

                return account!;

            }

        public async Task AssignRole(string userName, string roleName)
        {
            if (roleName != "Admin".ToLower() || roleName != "Customer".ToLower() || roleName != "Courier".ToLower())
            {
                throw new ArgumentException($"{roleName} is not a valid role. Has to be admin, customer or courier");
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var account = await _accountManager.FindByEmailAsync(userName);
            await _accountManager.AddToRoleAsync(account!, roleName);
        }

    }
}