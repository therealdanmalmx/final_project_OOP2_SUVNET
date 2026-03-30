using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;

namespace API.Services
{
    public interface IAccountLoginService
    {
        Task<AccountLoginResponse> Login(AccountLoginRequest request);

    }
}