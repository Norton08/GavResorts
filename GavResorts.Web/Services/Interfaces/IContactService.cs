using GavResorts.Web.Models;

namespace GavResorts.Web.Services.Interfaces;

public interface IContactService
{
    Task<IEnumerable<ContactViewModel>> GetAllContacts();
    Task<ContactViewModel> FindContactById(int id);
    Task<ContactViewModel> CreateContact(ContactViewModel model);
    Task<ContactViewModel> UpdateContact(ContactViewModel model);
    Task<bool> DeleteContactById(int id);
}
