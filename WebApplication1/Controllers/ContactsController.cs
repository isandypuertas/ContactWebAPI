using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Domain;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactlyDBContext dBContext;

        public ContactsController(ContactlyDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAllContacts()
        {
            List<Contact> contactList = dBContext.Contacts.ToList();
            return Ok(contactList);
        }

        [HttpPost]
        public IActionResult AddContact(AddContactDTO contact)
        {
            Contact domainContact = new Contact
            {
                Id = new Guid(),
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Favorite = contact.Favorite
            };

            dBContext.Contacts.Add(domainContact);
            dBContext.SaveChanges();

            return Ok(domainContact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteContact(Guid id)
        {
            Contact contact = dBContext.Contacts.Find(id);

            if (contact is not null)
            {
                dBContext.Contacts.Remove(contact);
                dBContext.SaveChanges();
            }

            return Ok();
        }

        [HttpPatch]
        [Route("{id:guid}")]
        public IActionResult EditFavoriteContact(Guid id)
        {
            Contact contact = dBContext.Contacts.Find(id);

            if (contact is not null)
            {
                contact.Favorite = !contact.Favorite;
                dBContext.Update(contact);
                dBContext.SaveChanges();
            }            

            return Ok();
        }
    }
}
