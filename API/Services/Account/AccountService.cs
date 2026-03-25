// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using API.Data;
// using API.DTO.Account;
// using API.Models;
// using Microsoft.EntityFrameworkCore;

// namespace API.Services.Account
// {
//     public class AccountService : IAccountService
//     {
//         private readonly AppDbContext _dbContext;

//         public AccountService(AppDbContext dbContext)
//         {
//             _dbContext = dbContext;
//         }
//         public async Task<Models.Account> CreateNewAccount(CreateAccountDTO newAccount)
//         {
//             if(newAccount is null)
//             {
//                 throw new ArgumentNullException(nameof(newAccount), "Account can't be empty");
//             }

//             if(string.IsNullOrWhiteSpace(newAccount.Name))
//             {
//                 throw new ArgumentNullException(nameof(newAccount.Name), "Name must be set");
//             }

//             if(string.IsNullOrWhiteSpace(newAccount.Address))
//             {
//                 throw new ArgumentNullException(nameof(newAccount.Address), "Address must be set");
//             }

//             if(string.IsNullOrWhiteSpace(newAccount.Phone))
//             {
//                 throw new ArgumentNullException(nameof(newAccount.Phone), "Phone must be set");
//             }

//             if(string.IsNullOrWhiteSpace(newAccount.Email))
//             {
//                 throw new ArgumentNullException(nameof(newAccount.Email), "Email must be set");
//             }

//             if(newAccount.Role > (Role)2)
//             {
//                 throw new ArgumentNullException(nameof(newAccount.Role), "Role must be set");
//             }

//             var account = new Models.Account
//             {
//                 Name = newAccount.Name,
//                 Address = newAccount.Address,
//                 Phone = newAccount.Phone,
//                 Email = newAccount.Email,
//                 Role = newAccount.Role
//             };

//             if (account is null)
//             {
//                 throw new ArgumentNullException(nameof(account), "Account could not be created");
//             }

//             _dbContext.Accounts.Add(account);
//             await _dbContext.SaveChangesAsync();

//             return account;
//         }

//         public async Task<List<Models.Account>> GetAllAccounts()
//         {
//             var accounts = await _dbContext.Accounts.ToListAsync();

//             if(accounts is null)
//             {
//                 throw new ArgumentNullException(nameof(accounts), "No accounts were found");
//             }

//             return accounts;
//         }
//     }
// }