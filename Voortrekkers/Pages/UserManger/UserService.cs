using System.Net.Http.Headers;
using System.Net.Http.Json;
using Voortrekkers.Shared.Helper;

namespace Voortrekkers.Pages.UserManger;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;

    public UserService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }


      public async Task<List<UserModel>> GetAllUsers()
      {
          var token =  await _tokenHelper.getToken();
          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          var result = await _httpClient.GetFromJsonAsync<List<UserModel>>(_uri + "/GetAllUsers");
          return result;
      }

      public async Task<bool> CreateUser(UserModel user)
      {
          var token =  await _tokenHelper.getToken(); 
          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          var result = await _httpClient.PostAsJsonAsync(_uri + "/CreateUser", user);
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