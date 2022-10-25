using ContactListManagement.Core.Entities;
using ContactListManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactListsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ContactListsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactList>>> GetContactLists()
        {
            if(_applicationDbContext.ContactLists == null)
            {
                return NotFound();
            }
            return await _applicationDbContext.ContactLists.ToListAsync();
        }

        [HttpGet("byId")]
        public async Task<ActionResult<ContactList>> GetContactList(int id)
        {
            if (_applicationDbContext.ContactLists == null)
            {
                return NotFound();
            }
            var contactList = await _applicationDbContext.ContactLists.FindAsync(id);
            if (contactList == null)
            {
                return NotFound();
            }
            return contactList;
        }

        [HttpPost]
        public async Task<ActionResult<ContactList>> GetContactList(ContactList contactList)
        {
            _applicationDbContext.ContactLists.Add(contactList);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContactList), new { id = contactList.Id }, contactList);
        }

        [HttpPut()]
        public async Task<IActionResult> PutMovie(ContactList contactList)
        {
            //if (id != contactList.Id)
            //{
            //    return BadRequest();
            //}

            _applicationDbContext.Entry(contactList).State = EntityState.Modified;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactListExists(contactList.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactList(int id)
        {
            if (_applicationDbContext.ContactLists == null)
            {
                return NotFound();
            }
            var contactList = await _applicationDbContext.ContactLists.FindAsync(id);
            if (contactList == null)
            {
                return NotFound();
            }
            _applicationDbContext.ContactLists.Remove(contactList);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }
        private bool ContactListExists(long id)
        {
            return (_applicationDbContext.ContactLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
