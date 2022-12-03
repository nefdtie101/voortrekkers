using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataBaseModles;
using Voortrekkers.Shared.Helper;

namespace Voortrekkers.Pages.Organization;

public class OrganizationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;


    public OrganizationService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }
    
    public async Task<List<OrganizationModel>> GetAllOrganizations()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<OrganizationModel>>(_uri + "/GetAllOrganizations");
        return result;
    }
    
    public async Task<bool> CreateOrganization(OrganizationModel org)
    {
        var token =  await _tokenHelper.getToken(); 
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PostAsJsonAsync(_uri + "/AddOrganization", org);
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
    
    public async Task<bool> UpdateOrganization(OrganizationModel org)
    {
        var token =  await _tokenHelper.getToken(); 
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PutAsJsonAsync(_uri + "/EditOrganization", org);
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
    
    public async Task<bool> DeleteOrganization(OrganizationModel org)
    {
        var token =  await _tokenHelper.getToken(); 
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PutAsJsonAsync(_uri + "/DeleteOrganization", org);
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