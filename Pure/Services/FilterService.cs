using BreakAway.Entities;
using BreakAway.Models.Contact;
using System.Linq;

namespace BreakAway.Services
{
    public class FilterService : IFilterService
    {
        public FilterService(){}

        public ContactItem[] GetFilterdContacts(IQueryable<Contact> contacts, string firstNameFilter, string lastNameFilter)
        {
            if (!string.IsNullOrWhiteSpace(firstNameFilter))
            {
                contacts = contacts.Where(c => c.FirstName.Contains(firstNameFilter));
            }
            if (!string.IsNullOrWhiteSpace(lastNameFilter))
            {
                contacts = contacts.Where(c => c.LastName.Contains(lastNameFilter));
            }

            return (from contact in contacts
                    select new ContactItem
                    {
                        Id = contact.Id,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Title = contact.Title
                    }).ToArray();
        }
    }
}