using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.DTO.Account;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public interface IAccountRegisterService
    {
        Task<AccountRegistrationResponse> RegisterAccount(AccountRegistrationRequest request);
        Task<List<AccountRequestDTO>> GetAllAccounts();
        Task<AccountRequestDTO> GetAccountById(Guid id);
        Task AssignRole(string userName, string roleName);
    }
}