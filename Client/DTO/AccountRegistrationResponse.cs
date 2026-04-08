namespace Client.DTO
{
    public class AccountRegistrationResponse
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public AccountRegistrationResponse() { }
        public AccountRegistrationResponse(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
        public AccountRegistrationResponse(bool isSuccessful, IEnumerable<string>? errors)
        {
            IsSuccessful = isSuccessful;
            Errors = errors;
        }
    }
}