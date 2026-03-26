using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using BlazorBlueprint.Components;
using Blazored.LocalStorage;

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


await builder.Build().RunAsync();
