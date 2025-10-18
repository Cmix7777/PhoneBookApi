using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PhoneA.Models;

namespace PhoneA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneBookController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Contact>> GetContacts() =>
            Ok(new List<Contact>(PhoneBookService.Contacts.Values));
        
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

            PhoneBookService.Contacts.Add(contact.Id, contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(string id, CreateContactDto dto)
        {
            if (!PhoneBookService.Contacts.ContainsKey(id)) return NotFound();

            var existing = PhoneBookService.Contacts[id];
            existing.Name = dto.Name;
            existing.PhoneNumber = dto.PhoneNumber;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            if (PhoneBookService.Contacts.Remove(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        private string GenerateId() => DateTime.Now.Ticks.ToString();
    }
}