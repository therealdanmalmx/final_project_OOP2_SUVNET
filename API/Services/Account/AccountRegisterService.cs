using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Account
{
    public class AccountRegisterService : IAccountRegisterService
    {
        private readonly UserManager<Models.Account> _accountManager;

        public AccountRegisterService(UserManager<Models.Account> accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task<AccountRegistrationResponse> RegisterAccount(AccountRegistrationRequest request)
        {
            var newTenant = new Models.Account
            {
                Name = request.Name,
                Address = request.Address,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Role = request.Role
            };

            var result = await _accountManager.CreateAsync(newTenant, request.Password);

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
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                UserName = u.UserName
            }).ToListAsync();

        }
    }
}