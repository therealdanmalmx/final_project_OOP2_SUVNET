using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO.Account
{
    public class AccountRegistrationResponse
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string>? Errors { get; set; }

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