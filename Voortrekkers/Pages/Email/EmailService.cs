using System.Net.Http.Headers;
using Voortrekkers.Shared.Helper;

namespace Voortrekkers.Pages.Email;

public class EmailService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;


    public EmailService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }

    public async Task<bool> SendEmail(EmailModel model)
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PostAsync(_uri + "/SendBulkEmail?Subject="
                                                      +model.Subject+"&Message="
                                                      +model.Message+"&Staatmaker="
                                                      +model.Staatmaker, null);
        var res = await result.Content.ReadAsStringAsync();
        if (res == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public async Task<bool> UnSubscribe(string email)
    {
        var result = await _httpClient.PostAsync(_uri + "/UnSubscribe?email=" + email, null);
        var res = await result.Content.ReadAsStringAsync();
        if (res == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}