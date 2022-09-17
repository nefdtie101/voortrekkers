using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace Voortrekkers.Pages.Login;

public class LoginService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;
    
    
    public LoginService(ILocalStorageService localStorageService , AuthenticationStateProvider authenticationStateProvider , HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
    }

  public async Task Login(LoginModel login)
    {
        var result = await _httpClient.PostAsJsonAsync("http://localhost:5111/login", login);
        var token = await result.Content.ReadAsStringAsync();
        await _localStorageService.SetItemAsync("token", token);
        await _authenticationStateProvider.GetAuthenticationStateAsync();
    }
}