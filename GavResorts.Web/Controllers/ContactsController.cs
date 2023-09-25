using GavResorts.Web.Models;
using GavResorts.Web.Roles;
using GavResorts.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<ContactViewModel>>> Index()
        {
            var result = await _contactService.GetAllContacts(await GetAccessToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateContact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _contactService.CreateContact(model, await GetAccessToken());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var result = await _contactService.FindContactById(id, await GetAccessToken());

            if(result == null) return View("Error");

            return View(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateContact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.UpdateContact(model, await GetAccessToken());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.FindContactById(id, await GetAccessToken());

            if (result == null) return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeleteContact")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _contactService.DeleteContactById(id, await GetAccessToken());

            if (!result)
                return View("Error");
            

            return RedirectToAction("Index");
        }

        private async Task<string?> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }
    }
}
