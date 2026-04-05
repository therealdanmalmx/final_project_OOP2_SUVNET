using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AccountLoginService : IAccountLoginService
    {
        private readonly SignInManager<Models.Account> _signInManager;
        private readonly UserManager<Account> _accountManager;
        private readonly IConfiguration _config;

        public AccountLoginService(SignInManager<Account> signInManager, IConfiguration config, UserManager<Account> accountManager)
        {
            _signInManager = signInManager;
            _config = config;
            _accountManager = accountManager;
        }

        public async Task<AccountLoginResponse> Login(AccountLoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.UserName,
                request.Password,
                false,
                false
            );

            if (!result.Succeeded)
            {
                return new AccountLoginResponse(false, "Email or password are wrong");
            }

            var user = await _accountManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new AccountLoginResponse(false, "Användaren finns inte");
            }

            var accountRoles = await _accountManager.GetRolesAsync(user);


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, request.UserName),
            };
            claims.AddRange(accountRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtsecurityKey"]!)
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.UtcNow.AddDays(Convert.ToInt16(_config["JwtExpiryDate"]));
            var token = new JwtSecurityToken(
                issuer: _config["JwtIssuer"],
                audience: _config["JwtAudience"],
                claims: claims,
                expires: expiryDate,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new AccountLoginResponse(true, null, jwt);
        }

    }
}