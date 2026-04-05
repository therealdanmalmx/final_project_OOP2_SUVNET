using API.DTO;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using Microsoft.AspNetCore.Identity;
using API.DTO.Account;
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
        public async Task<ActionResult<List<AccountRequestDTO>>> GetAccounts()
        {
            var result = await _accountRegisterService.GetAllAccounts();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AccountRequestDTO>> GetAccountById(Guid id)
        {
            var result = await _accountRegisterService.GetAccountById(id);
            if (result is null)
            {
                return NotFound("Hittade inte account");
            }
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

        [HttpPost("role")]
        public async Task<ActionResult> AssignRole([FromBody] AssignRoleDTO request)
        {
            await _accountRegisterService.AssignRole(request.UserName, request.RoleName);
            return Ok();
        }
    }
}