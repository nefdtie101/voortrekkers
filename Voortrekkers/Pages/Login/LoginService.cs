using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace Voortrekkers.Pages.Login;

public class LoginService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    
    
    public LoginService(ILocalStorageService localStorageService , AuthenticationStateProvider authenticationStateProvider , HttpClient httpClient , IConfiguration  config)
    {
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
        _config = config;
    }

  public async Task<bool> Login(LoginModel login)
  {
        var uri = _config.GetValue<string>("deployUriApi") + "/login";
        var result = await _httpClient.PostAsJsonAsync(uri, login);
        var token = await result.Content.ReadAsStringAsync();
        if (token == "false")
        {
            return false;
        }
        else
        {
           
            await _localStorageService.SetItemAsync("token", token);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            return true;
        }
       
    }
}