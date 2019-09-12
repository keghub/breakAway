using BreakAway.Entities;
using BreakAway.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BreakAway.Services
{
    public interface IFilterService
    {
        IEnumerable<ContactItem> GetFilteredContacts(IQueryable<Contact> contacts, FilterModel filterModel,int page,int pageSize,out int totalPages);
    }

    public interface IContactFilter
    {
        bool CanFilter(FilterModel filter);
        IQueryable<Contact> GetFilteredContacts(IQueryable<Contact> contacts, FilterModel filter);
    }

    public class FilterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
}