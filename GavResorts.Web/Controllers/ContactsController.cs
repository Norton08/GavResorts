using GavResorts.Web.Models;
using GavResorts.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GavResorts.Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactViewModel>>> Index()
        {
            var result = await _contactService.GetAllContacts();

            if(result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _contactService.CreateContact(model);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var result = await _contactService.FindContactById(id);

            if(result == null) return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.UpdateContact(model);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.FindContactById(id);

            if (result == null) return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeleteContact")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _contactService.DeleteContactById(id);

            if (!result)
                return View("Error");
            

            return RedirectToAction("Index");
        }
    }
}
