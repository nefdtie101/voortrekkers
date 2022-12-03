global using Blazored.LocalStorage;
using DataBaseModles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Voortrekkers;
using Voortrekkers.Pages.Login;
using Voortrekkers.Pages.Organization;
using Voortrekkers.Pages.ResetPassword;
using Voortrekkers.Pages.Roles;
using Voortrekkers.Pages.Roles.Module;
using Voortrekkers.Pages.UserManger;
using Voortrekkers.Shared.Helper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
WebAssemblyHostConfiguration configuration = builder.Configuration;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ResetPasswordService>();
builder.Services.AddScoped<TokenHelper>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<rolesService>();
builder.Services.AddScoped<OrganizationService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<modulesService>();


await builder.Build().RunAsync();