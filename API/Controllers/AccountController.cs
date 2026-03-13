using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.Account;
using API.DTO.Courier;
using API.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _dbContext.Accounts.ToListAsync();

            if(accounts is null)
            {
                return BadRequest();
            }

            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewAccount(CreateAccountDTO newAccount)
        {
            if(newAccount is null)
            {
                return BadRequest("Account can't be empty");
            }

            var account = new Account
            {
                Name = newAccount.Name,
                Address = newAccount.Address,
                Phone = newAccount.Phone,
                Email = newAccount.Email,
                Role = newAccount.Role
            };

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            return Created($"/api/account", account);
        }
    }
}