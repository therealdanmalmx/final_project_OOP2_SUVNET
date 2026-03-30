using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;

namespace API.Services.Auth
{
    public interface IAuthService
    {
        Task Register(AccountRegistrationRequest request);
        Task Login(AccountLoginRequest request);
        Task Logout();
    }
}