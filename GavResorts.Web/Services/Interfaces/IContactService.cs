using GavResorts.Web.Models;

namespace GavResorts.Web.Services.Interfaces;

public interface IContactService
{
    Task<IEnumerable<ContactViewModel>> GetAllContacts(string token);
    Task<ContactViewModel> FindContactById(int id, string token);
    Task<ContactViewModel> CreateContact(ContactViewModel model, string token);
    Task<ContactViewModel> UpdateContact(ContactViewModel model, string token);
    Task<bool> DeleteContactById(int id, string token);
}
