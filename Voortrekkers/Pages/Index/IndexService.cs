using System.Net.Http.Headers;
using System.Net.Http.Json;
using Voortrekkers.Shared.Helper;

namespace Voortrekkers.Pages.Index;

public class IndexService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;

    public IndexService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }
    
    public async Task<List<EventModel>> GetAllInterneAksies()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<EventModel>>(_uri + "/GetAllInteneAksies");
        return result;
    }
    
    public async Task<List<EventModel>> GetAllGebiedsAksies()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<EventModel>>(_uri + "/GetAllGebiedAksies");
        return result;
    }
    
    public async Task<List<EventModel>> GetAllSpesialeAksies()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<EventModel>>(_uri + "/GetAllSpesialeAksies");
        return result;
    }
    
    public async Task<List<EventModel>> GetAllKampe()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<EventModel>>(_uri + "/GetAllKampe");
        return result;
    }
    
    public async Task<List<OrganizationModel>> GetAllGroepe()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<OrganizationModel>>(_uri + "/GetAllOrganizations");
        return result;
    }
}