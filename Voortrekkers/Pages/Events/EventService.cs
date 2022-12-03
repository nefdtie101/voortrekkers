using System.Net.Http.Headers;
using System.Net.Http.Json;
using Voortrekkers.Shared.Helper;

public class EventService 
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;
    
    public EventService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }

    public async Task<List<EventModel>> GetAllEvents()
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<EventModel>>(_uri + "/GetAllEvents");
        return result;
    }
    
    public async Task<bool> CreateEvent(EventModel _event) 
    {
        var token =  await _tokenHelper.getToken(); 
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PostAsJsonAsync(_uri + "/AddEvent", _event);
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
    
    public async Task<bool> UpdateEvent(EventModel _event)
    {
        var token =  await _tokenHelper.getToken(); 
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PutAsJsonAsync(_uri + "/EditEvent", _event);
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
    
    public async Task<bool> DeleteEvent(EventModel _event)
    {
        var token =  await _tokenHelper.getToken(); 
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PutAsJsonAsync(_uri + "/DeleteEvent", _event);
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

 