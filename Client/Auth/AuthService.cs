using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.DTO;
using Blazored.LocalStorage;
using Client.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _nav;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _nav = nav;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task Login(AccountLoginRequest request)
        {
            var result = await _http.PostAsJsonAsync("/api/account/login", request);
            if (result is not null)
            {
                var response = await result.Content.ReadFromJsonAsync<AccountLoginResponse>();

                if (!response.IsSuccessful && response.Errors is not null)
                {
                    Console.WriteLine(response.Errors);
                }
                else if (!response.IsSuccessful)
                {
                    Console.WriteLine("An unexpected error occured");
                }
                else
                {
                    if (response.Token is not null)
                    {
                        await _localStorage.SetItemAsStringAsync("authToken", response.Token);
                        await _authStateProvider.GetAuthenticationStateAsync();
                    }
                    Console.WriteLine("Successful login");
                    _nav.NavigateTo("/");
                }
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _authStateProvider.GetAuthenticationStateAsync();
            _nav.NavigateTo("/login");
        }

        public async Task Register(AccountRegistrationRequest request)
        {
            var result = await _http.PostAsJsonAsync("api/account/register", request);

            if (result is not null)
            {
                var response = await result.Content.ReadFromJsonAsync<AccountRegistrationResponse>();

                if(!response.IsSuccessful &&response.Errors != null)
                {
                    foreach (var error in response.Errors)
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (!response.IsSuccessful)
                {
                    Console.WriteLine("An unexpected error occured");
                }
                else
                {
                    Console.WriteLine("Successful registration");
                }

            }

        }
    }
}