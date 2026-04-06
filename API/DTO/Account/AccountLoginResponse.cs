namespace API.DTO
{
    public class AccountLoginResponse
    {
        public bool IsSuccessful { get; set; }
        public string? Errors { get; set; } = null;
        public string? Token { get; set; } = null;

        public AccountLoginResponse() { }
        public AccountLoginResponse(bool isSuccessful, string? errors)
        {
            IsSuccessful = isSuccessful;
            Errors = errors;
        }
        public AccountLoginResponse(bool isSuccessful, string? errors, string? token)
        {
            IsSuccessful = isSuccessful;
            Errors = errors;
            Token = token;
        }
    }
}