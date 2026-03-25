using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.Account;

namespace API.Services.Account
{
    public interface IAccountRegisterService
    {
        Task<AccountRegistrationResponse> RegisterAccount(AccountRegistrationRequest request);
        Task<List<AccountRequestDTO>> GetAllAccounts();
    }
}