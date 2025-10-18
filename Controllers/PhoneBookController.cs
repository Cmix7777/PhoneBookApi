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
            if (PhoneBookService.Contacts.ContainsKey(id))
            {
                Contact contact = PhoneBookService.Contacts[id];
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Contact> CreateContact(CreateContactDto dto)
        {
            Contact contact;

            if (dto.Type == "Business")
            {
                contact = new BusinessContact();
            }
            else
            {
                contact = new PersonalContact();
            }

            contact.Id = GenerateId(); 
            contact.Name = dto.Name;
            contact.PhoneNumber = dto.PhoneNumber;

            PhoneBookService.Contacts[contact.Id] = contact;
            return Ok(contact); 
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContact(string id, CreateContactDto dto)
        {
            if (!PhoneBookService.Contacts.ContainsKey(id))
            {
                return NotFound();
            }

            Contact existing = PhoneBookService.Contacts[id];
            existing.Name = dto.Name;
            existing.PhoneNumber = dto.PhoneNumber;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteContact(string id)
        {
            if (PhoneBookService.Contacts.ContainsKey(id))
            {
                PhoneBookService.Contacts.Remove(id);
                return NoContent();
            }
            return NotFound();
        }

        private string GenerateId()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}