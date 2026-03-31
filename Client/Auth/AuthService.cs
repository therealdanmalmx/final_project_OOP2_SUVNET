using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.DTO;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
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
        private readonly IToastService _toastService;


        public AuthService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider, IToastService toastService)
        {
            _http = http;
            _nav = nav;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
            _toastService = toastService;
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
                    _toastService.ShowError("Ett oväntat fel inträffade. Prova igen");
                }
                else
                {
                    if (response.Token is not null)
                    {
                        await _localStorage.SetItemAsStringAsync("authToken", response.Token);
                        await _authStateProvider.GetAuthenticationStateAsync();
                    }
                    _toastService.ShowSuccess("Du är inloggad");
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
                        _toastService.ShowError(error);
                    }
                }
                else if (!response.IsSuccessful)
                {
                    _toastService.ShowError("An unexpected error occured");
                }
                else
                {
                    _toastService.ShowSuccess("Du har registrerart dig");
                }

            }

        }
    }
}