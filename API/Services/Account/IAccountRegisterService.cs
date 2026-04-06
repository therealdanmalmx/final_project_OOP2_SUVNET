using API.DTO;

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