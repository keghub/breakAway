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
        ContactItem[] GetFilterdContacts(IQueryable<Contact> contacts, string firstNameFilter, string lastNameFilter);
    }
}