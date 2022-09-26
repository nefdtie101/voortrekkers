using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Voortrekkers.Pages.ResetPassword;

public class ResetPasswordService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ResetPasswordService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    
    public async Task<bool> CreatePassword(ResetPasswordModel pass , string jwt)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var uri = _config.GetValue<string>("deployUriApi") + "/CreatePassword";
        var result = await _httpClient.PostAsJsonAsync(uri, pass);
        var res = await result.Content.ReadAsStringAsync();
        if (res == "false")
        {
            return false;
        }
        else
        {
            return true;
        }
       
    }
}