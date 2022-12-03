using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataBaseModles;
using Voortrekkers.Shared.Helper;

namespace Voortrekkers.Pages.Roles.Module;

public class modulesService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;

    public modulesService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }
    
    public async Task<List<ModuleModel>> GetAllModules()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<ModuleModel>>(_uri + "/GetAllModules");
        return result;
    }
    
     public async Task<bool> CreateModule(ModuleModel module)
      {
          var token =  await _tokenHelper.getToken(); 
          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          var result = await _httpClient.PostAsJsonAsync(_uri + "/AddModule", module);
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
      
      public async Task<bool> UpdateModule(ModuleModel module)
      {
          var token =  await _tokenHelper.getToken(); 
          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          var result = await _httpClient.PutAsJsonAsync(_uri + "/EditModule", module);
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

      public async Task<bool> DeleteModule(ModuleModel module)
      {
          var token =  await _tokenHelper.getToken(); 
          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          var result = await _httpClient.PutAsJsonAsync(_uri + "/DeleteModule", module);
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