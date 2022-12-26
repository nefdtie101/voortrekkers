using System.Net.Http.Headers;
using System.Net.Http.Json;
using Voortrekkers.Shared.Helper;

namespace Voortrekkers.Pages.Forms;

public class FormService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorageService;
    private TokenHelper _tokenHelper;
    private string _uri;

    public FormService(HttpClient httpClient, IConfiguration config , ILocalStorageService localStorageService ,TokenHelper tokenHelper)
    {
        _httpClient = httpClient;
        _config = config;
        _uri = _config.GetValue<string>("deployUriApi");
        _localStorageService = localStorageService;
        _tokenHelper = tokenHelper;
    }

    public async Task<bool> FillInBasicForm(BasicFormModel form)
    {
        var result = await _httpClient.PostAsJsonAsync(_uri + "/FillInBasicForm", form);
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
    
    public async Task<bool> FillInBasicStudentForm(BasicStudentFormModel form)
    {
        var result = await _httpClient.PostAsJsonAsync(_uri + "/FillInBasicStudentForm", form);
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

    public async Task<List<BasicFormModel>> GetBasicFormFormEventId(string id)
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<BasicFormModel>>(_uri + "/GetFormByEventId?id=" + id);
        return result;
    }

    public async Task<bool> MarkBasicAsPaid(string id)
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PostAsync(_uri + "/MarkBasicAsPaid?id="+id, null);
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
    
    public async Task<bool> MarkAsAttended(string id)
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PostAsync(_uri + "/MarkBasicAsAttended?id="+id, null);
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
    
    public async Task<List<BasicStudentFormModel>> GetStudentFormByEventId(string id)
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetFromJsonAsync<List<BasicStudentFormModel>>(_uri + "/GetStudentFormByEventId?id=" + id);
        return result;
    }
    
    public async Task<bool> MarkStudentAsPaid(string id)
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PostAsync(_uri + "/MarkStudentFormAsPaid?id="+id, null);
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
    
    public async Task<bool> MarkStudentAsAttended(string id)
    {
        var token =  await _tokenHelper.getToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.PostAsync(_uri + "/MarkStudentFormAsAttended?id="+id, null);
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