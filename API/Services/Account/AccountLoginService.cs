using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Account
{
    public class AccountLoginService : IAccountLoginService
    {
        private readonly SignInManager<Models.Account> _signInManager;
        private readonly IConfiguration _config;

        public AccountLoginService(SignInManager<Models.Account> signInManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _config = config;
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

            var claims = new []
            {
                new Claim(ClaimTypes.Name, request.UserName)
            };

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

        Task<AccountLoginResponse> IAccountLoginService.Login(AccountLoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}