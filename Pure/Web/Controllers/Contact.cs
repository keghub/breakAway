using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BreakAway.Entities;
using BreakAway.Models.Contact;

namespace BreakAway.Controllers
{
    public class ContactController:Controller
    {
        private readonly Repository _repository;

        public ContactController(Repository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        public ActionResult Index( )
        {
            var viewModel = new IndexViewModel();

            viewModel.Contacts = (from contact in _repository.Contacts
                                   select new ContactItem
                                   {
                                       Id = contact.Id,
                                       FirstName = contact.FirstName,
                                       LastName = contact.LastName,
                                       Title = contact.Title
                                   }).ToArray();

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {

            var contact = _repository.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                return RedirectToAction("Index", "Contact");
            }

            var viewModel = new EditViewModel
            {
                Id = contact.Id,
                Title = contact.Title,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };

            return View(viewModel);
        }
    }
}