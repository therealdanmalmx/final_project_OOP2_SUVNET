using API.DTO.Account;
using API.Models;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Services;
using API.Services.Account;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRegisterService _accountRegisterService;
        private readonly IAccountLoginService _accountLoginService;

        public AccountController(IAccountRegisterService accountRegisterService, IAccountLoginService accountLoginService)
        {
            _accountRegisterService = accountRegisterService;
            _accountLoginService = accountLoginService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountRegistrationResponse>>> GetAccounts()
        {
            var result = await _accountRegisterService.GetAllAccounts();
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AccountRegistrationResponse>> RegisterParticipant(
            AccountRegistrationRequest request)
        {
            var result = await _accountRegisterService.RegisterAccount(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(new AccountRegistrationResponse(false, result.Errors));
            }

            return Ok(new AccountRegistrationResponse(true));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountLoginResponse>> Login(AccountLoginRequest request)
        {
            var result = await _accountLoginService.Login(request);

            if (!result.IsSuccessful)
            {
                return new AccountLoginResponse(false, result.Errors);
            }

            return Ok(result);
        }
    }
}