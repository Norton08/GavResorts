using GavResorts.ContactApi.Data;
using GavResorts.ContactApi.Dto;
using GavResorts.ContactApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GavResorts.ContactApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        [HttpGet] 
        public async Task<IActionResult> GetAllContactsAsync([FromServices]AppDbContext context)
        {
            var contacts = await context.Contacts.AsNoTracking().ToListAsync();

            return Ok(contacts);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdContactAsync([FromServices] AppDbContext context, [FromRoute]int id)
        {
            var contact = await context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return contact == null ? NotFound("Contact not found.") : Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> PostContactAsync([FromServices] AppDbContext context, [FromBody]ContactsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data invalid");

            var contact = new Contacts
            {
                Name = dto.Name,
                Note = dto.Note,
                Telephone = dto.Telephone,
                Email = dto.Email
            };

            try
            {
                await context.Contacts.AddAsync(contact);
                await context.SaveChangesAsync();
                return Created($"api/contacts/{contact.Id}", contact);
            }
            catch(Exception exception) { }
            {
                return BadRequest("Unable to save changes on Database.");
            }
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateContactAsync([FromServices] AppDbContext context, [FromBody] ContactsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data invalid");

            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (contact == null)
                return NotFound("Contact with this id not found");

            try
            {
                contact.Name = dto.Name;
                contact.Note = dto.Note;
                contact.Telephone = dto.Telephone;
                contact.Email = dto.Email;

                context.Contacts.Update(contact);
                await context.SaveChangesAsync();

                return Ok(contact);
            }
            catch (Exception exception) { }
            {
                return BadRequest("Unable to update changes on Database.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();
                return Ok("Success on delete register.");
            }
            catch (Exception exception) { }
            {
                return BadRequest("Unable to delete register on Database.");
            }
        }
    }
}
