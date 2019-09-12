using BreakAway.Entities;
using BreakAway.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BreakAway.Services
{

    public class FilterService : IFilterService
    {
        private readonly IEnumerable<IContactFilter> _filters;

        public FilterService(IEnumerable<IContactFilter> filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }
            _filters = filters;
        }

        public IEnumerable<ContactItem> GetFilteredContacts(IQueryable<Contact> contacts, FilterModel filterModel, int page, int pageSize, out int totalPages)
        {
            foreach (var filter in _filters)
            {
                if (filter.CanFilter(filterModel))
                {
                    contacts = filter.GetFilteredContacts(contacts, filterModel);
                }
            }
            totalPages = contacts.Count() / pageSize;

            contacts = contacts.OrderBy(m=>m.FirstName).Skip(pageSize * (page - 1)).Take(pageSize); 

            var fContacts= (from contact in contacts
                                    select new ContactItem
                                    {
                                        Id = contact.Id,
                                        FirstName = contact.FirstName,
                                        LastName = contact.LastName,
                                        Title = contact.Title
                                    }).ToArray();

            return fContacts;
        }

      
    }

    public class FirstNameContactFilter : IContactFilter
    {
        // Merge these two methods??
        public bool CanFilter(FilterModel filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            return !string.IsNullOrWhiteSpace(filter.FirstName);
        }


        public IQueryable<Contact> GetFilteredContacts(IQueryable<Contact> contacts, FilterModel filter)
        {
            if (contacts == null)
            {
                throw new ArgumentNullException(nameof(contacts));
            }

            if (!CanFilter(filter))
            {
                return contacts;
            }

            return contacts.Where(c => c.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
        }
    }

    public class LastNameContactFilter : IContactFilter
    {
        public bool CanFilter(FilterModel filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            return !string.IsNullOrWhiteSpace(filter.LastName);
        }


        public IQueryable<Contact> GetFilteredContacts(IQueryable<Contact> contacts, FilterModel filter)
        {
            if (contacts == null)
            {
                throw new ArgumentNullException(nameof(contacts));
            }
            return contacts.Where(c => c.LastName.ToLower().Contains(filter.LastName.ToLower()));
        }
    }

    public class TitleContactFilter : IContactFilter
    {
        public bool CanFilter(FilterModel filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            return !string.IsNullOrWhiteSpace(filter.Title);
        }


        public IQueryable<Contact> GetFilteredContacts(IQueryable<Contact> contacts, FilterModel filter)
        {
            if (contacts == null)
            {
                throw new ArgumentNullException(nameof(contacts));
            }
            return contacts.Where(c => c.Title.ToLower().Contains(filter.Title.ToLower()));
        }
    }
}