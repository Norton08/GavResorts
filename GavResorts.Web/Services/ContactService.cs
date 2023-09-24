using GavResorts.Web.Models;
using GavResorts.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GavResorts.Web.Services;

public class ContactService : IContactService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/contacts/";
    private readonly JsonSerializerOptions _options;
    private ContactViewModel model;
    private IEnumerable<ContactViewModel> contactsVm;

    public ContactService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ContactViewModel>> GetAllContacts()
    {
        var client = _clientFactory.CreateClient("ContactApi");
        
        using(var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                contactsVm = await JsonSerializer.DeserializeAsync<IEnumerable<ContactViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return contactsVm;
    }
    public async Task<ContactViewModel> FindContactById(int id)
    {
        var client = _clientFactory.CreateClient("ContactApi");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                model = await JsonSerializer.DeserializeAsync<ContactViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return model;
    }

    public async Task<ContactViewModel> CreateContact(ContactViewModel model)
    {
        var client = _clientFactory.CreateClient("ContactApi");

        StringContent content = new StringContent(JsonSerializer.Serialize(model), 
                                    Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                model = await JsonSerializer.DeserializeAsync<ContactViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return model;
    }
    
    public async Task<ContactViewModel> UpdateContact(ContactViewModel model)
    {
        var client = _clientFactory.CreateClient("ContactApi");

        ContactViewModel contactUpdated = new ContactViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, model))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                contactUpdated = await JsonSerializer.DeserializeAsync<ContactViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return contactUpdated;
    }

    public async Task<bool> DeleteContactById(int id)
    {
        var client = _clientFactory.CreateClient("ContactApi");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }
}
