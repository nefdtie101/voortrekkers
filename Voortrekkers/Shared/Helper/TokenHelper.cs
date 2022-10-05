using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.JSInterop;


namespace Voortrekkers.Shared.Helper;

public class TokenHelper
{
    
    private readonly ILocalStorageService _localStorageService;
    private readonly IJSRuntime _jsRuntime;
    private readonly HttpClient _httpClient;
    private string _uri;
    private readonly IConfiguration _config;
    private readonly AuthenticationStateProvider _authenticationStateProvider;


    public TokenHelper(ILocalStorageService localStorageService ,IJSRuntime jsRuntime, HttpClient httpClient , IConfiguration config, AuthenticationStateProvider authenticationStateProvider) 
    {
        _localStorageService = localStorageService;
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    
    public async Task<string> getToken()
    {
        IdentityModelEventSource.ShowPII = true;
        var token = await _localStorageService.GetItemAsStringAsync("token");
        token =  token.Replace("\"", "");
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await _httpClient.GetFromJsonAsync<bool>(_uri + "/CheckToken");
            if (result)
            {
                return token;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return await RefreshToken(token) ;
    }


    private async Task<string> RefreshToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var claims = jwt.Claims.ToList();
        var email = claims[0].Value;
        var refresh = await _localStorageService.GetItemAsStringAsync("refresh");
        refresh =  refresh.Replace("\"", "");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refresh);
        var result = await _httpClient.GetFromJsonAsync<JwtModel>(_uri + $"/Refresh?email={email}&token={refresh}");
        if (result.valid)
        {
            await _localStorageService.SetItemAsync("token", result.token);
            await _localStorageService.SetItemAsync("refresh", result.refresh);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            return result.token;
        }
        
        await _localStorageService.RemoveItemAsync("token");
        await _localStorageService.RemoveItemAsync("refresh");
        await _authenticationStateProvider.GetAuthenticationStateAsync();
        return "";
    }
    
}