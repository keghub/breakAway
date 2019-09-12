using System;
using System.Linq;
using System.Web.Mvc;
using BreakAway.Entities;
using BreakAway.Models.Contact;
using BreakAway.Services;

namespace BreakAway.Controllers
{
    public class ContactController : Controller
    {
        private readonly Repository _repository;
        private readonly IFilterService _filterService;

        private const int PageSize = 25;

        public ContactController(Repository repository, IFilterService filterService)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (filterService == null)
            {
                throw new ArgumentNullException(nameof(filterService));
            }
            _repository = repository;
            _filterService = filterService;
        }

        public ActionResult Index([Bind(Prefix =nameof(IndexViewModel.FilterViewModel))]FilterItem filter, int? page)
        {
            filter = filter ?? new FilterItem();
            page = page ?? 1;

            var viewModel = new IndexViewModel();
            IQueryable<Contact> contacts = _repository.Contacts;

            //Filter
            var filterModel = new FilterModel { Title = filter.Title, FirstName = filter.FirstName, LastName = filter.LastName };

            var filteredContacts = _filterService.GetFilteredContacts(contacts, filterModel, page.Value, PageSize, out int totalPages);

            //Paging
            var pageModel = new PageItem
            {
                Page = (int)page,
                TotalPage = totalPages
            };

            viewModel.Contacts = filteredContacts.ToArray();
            viewModel.FilterViewModel = filter;
            viewModel.PageViewModel = pageModel;

            return View(viewModel);
        }

        public ActionResult Add()
        {
            var viewModel = new AddViewModel { };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add", "Contact", new { message = "Contact not created" });
            }

            var contact = new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Title = model.Title,
                AddDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            _repository.Contacts.Add(contact);
            _repository.Save();

            return RedirectToAction("Index", "Customer", new { message = "Contact created successfully" });
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
                LastName = contact.LastName,
                Addresses = contact.Addresses.Select(address => new AddressViewModel
                {
                    Id = address.Id,
                    AddressType = address.AddressType,
                    Mail = new MailModel
                    {
                        Street1 = address.Mail.Street1,
                        Street2 = address.Mail.Street2,
                        City = address.Mail.City,
                        StateProvince = address.Mail.StateProvince
                    },
                    PostalCode = address.PostalCode,
                    CountryRegion = address.CountryRegion
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contact = _repository.Contacts.FirstOrDefault(c => c.Id == model.Id);
            if (contact == null)
            {
                return RedirectToAction("Index", "Contact", new { message = "Contact with id '" + model.Id + "' was not found" });
            }

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Title = model.Title;

            if (model.Addresses == null)
            {
                // remove all addresses
                var addresses = contact.Addresses.ToArray();
                foreach (var address in addresses)
                {
                    _repository.Addresses.Delete(address);
                }
            }
            else
            {

                foreach (var address in contact.Addresses.ToArray())
                {
                    var item = model.Addresses.SingleOrDefault(i => i.Id == address.Id);
                    if (item == null)
                    {
                        //remove address
                        _repository.Addresses.Delete(address);
                        _repository.Save();
                        continue;
                    }
                }

                foreach (var item in model.Addresses.ToArray())
                {

                    var address = contact.Addresses.SingleOrDefault(a => a.Id == item.Id);
                    if (address == null)
                    {
                        // add address
                        var newMail = new Mail
                        {
                            Street1 = item.Mail.Street1,
                            Street2 = item.Mail.Street2,
                            City = item.Mail.City,
                            StateProvince = item.Mail.StateProvince
                        };

                        var newAddress = new Address
                        {
                            ContactId = contact.Id,
                            AddressType = item.AddressType,
                            PostalCode = item.PostalCode,
                            CountryRegion = item.CountryRegion,
                            Mail = newMail,
                            ModifiedDate = DateTime.Now
                        };

                        _repository.Addresses.Add(newAddress);
                        _repository.Save();

                        continue;
                    }

                    // update address if changed ??if changed -> EF change tracking
                    if (item.AddressType != null) address.AddressType = item.AddressType;
                    if (item.PostalCode != null) address.PostalCode = item.PostalCode;
                    if (item.CountryRegion != null) address.CountryRegion = item.CountryRegion;
                    if (item.Mail != null)
                    {
                        var itemMail = item.Mail;
                        var addressMail = address.Mail;
                        if (itemMail.Street1 != null) addressMail.Street1 = itemMail.Street1;
                        if (itemMail.Street2 != null) addressMail.Street2 = itemMail.Street2;
                        if (itemMail.City != null) addressMail.City = itemMail.City;
                        if (itemMail.StateProvince != null) addressMail.StateProvince = itemMail.StateProvince;
                    }
                    address.ModifiedDate = DateTime.Now;
                }
            }

            _repository.Save();

            return RedirectToAction("Index", "Contact", new { id = contact.Id, message = "Changes saved successfully" });
        }

        [HttpGet]
        public ActionResult AddressLayout()
        {
            return View("_Address", new AddressViewModel());
        }

        public int GetPageNumber(int totalItem, int pageSize, int? page)
        {
            if (page == null)
            {
                return 1;
            }

            var totalPage = 0;
            if (totalItem % pageSize == 0)
            {
                totalPage = totalItem / pageSize;
            }
            else
            {
                totalPage = totalItem / pageSize + 1;
            }

            var pageNumber = (int)page;
            if (page < 1)
            {
                pageNumber = 1;
            }
            else if (page > totalPage)
            {
                pageNumber = totalPage;
            }

            return pageNumber;
        }
    }
}