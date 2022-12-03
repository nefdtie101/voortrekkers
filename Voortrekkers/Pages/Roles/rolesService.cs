using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataBaseModles;
using Voortrekkers.Shared.Helper;

namespace Voortrekkers.Pages.Roles;

public class rolesService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;

  public  rolesService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }
  
  
    public async Task<List<RoleModel>> GetAllRoles()
      {
          var token =  await _tokenHelper.getToken();
          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          var result = await _httpClient.GetFromJsonAsync<List<RoleModel>>(_uri + "/GetAllRoles");
          return result;
      }
      
       public async Task<bool> CreateRole(RoleModel role)
        {
            var token =  await _tokenHelper.getToken(); 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await _httpClient.PostAsJsonAsync(_uri + "/AddRoles", role);
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
        
        public async Task<bool> UpdateRole(RoleModel role)
        {
            var token =  await _tokenHelper.getToken(); 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await _httpClient.PutAsJsonAsync(_uri + "/EditRole", role);
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
  
        public async Task<bool> DeleteRole(RoleModel role)
        {
            var token =  await _tokenHelper.getToken(); 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await _httpClient.PutAsJsonAsync(_uri + "/DeleteRoles", role);
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