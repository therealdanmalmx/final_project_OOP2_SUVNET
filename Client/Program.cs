using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using BlazorBlueprint.Components;
using Blazored.LocalStorage;
using API.Services.Auth;
using Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5095") });
builder.Services.AddScoped(sp => new System.Text.Json.JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
});

builder.Services.AddBlazorBlueprintComponents();
builder.Services.AddBlazoredToast();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

await builder.Build().RunAsync();
