using Microsoft.AspNetCore.Mvc;
using PhoneA.Models;

namespace PhoneA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneBookController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts()
        {
            return Ok(PhoneBookService.Contacts.Values);
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContact(string id)
        {
            if (PhoneBookService.Contacts.TryGetValue(id, out var contact))
            {
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Contact> CreateContact(CreateContactDto dto)
        {
            Contact contact = dto.Type switch
            {
                "Business" => new BusinessContact(),
                _ => new PersonalContact()
            };

            contact.Id = Guid.NewGuid().ToString();
            contact.Name = dto.Name;
            contact.PhoneNumber = dto.PhoneNumber;

            PhoneBookService.Contacts[contact.Id] = contact;
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContact(string id, CreateContactDto dto)
        {
            if (!PhoneBookService.Contacts.ContainsKey(id))
            {
                return NotFound();
            }

            var existing = PhoneBookService.Contacts[id];
            existing.Name = dto.Name;
            existing.PhoneNumber = dto.PhoneNumber;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteContact(string id)
        {
            if (PhoneBookService.Contacts.Remove(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}