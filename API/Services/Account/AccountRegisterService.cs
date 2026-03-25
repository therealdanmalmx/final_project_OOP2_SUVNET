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

        public async Task<List<Models.Account>> GetAllAccounts()
        {
            if (_accountManager.Users == null)
            {
                return [];
            }

            return await _accountManager.Users.ToListAsync();
        }
    }
}