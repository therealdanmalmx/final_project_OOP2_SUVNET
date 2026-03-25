using API.DTO.Account;
using API.Models;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Services;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<Account>> GetAllAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllAccounts();
                return Ok(accounts);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateNewAccount(CreateAccountDTO newAccount)
        {
            try
            {
                var account = await _accountService.CreateNewAccount(newAccount);
                return Ok(account);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }
    }
}