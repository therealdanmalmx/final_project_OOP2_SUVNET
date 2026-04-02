using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
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
                UserName = u.UserName,
                Role = u.Role
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
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    UserName = u.UserName,
                    Role = u.Role
                }).FirstOrDefaultAsync();

                if (account is null)
                {
                    Console.WriteLine("No account");
                }

                return account;

            }
    }
}