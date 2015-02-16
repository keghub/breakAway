using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BreakAway.Entities;
using BreakAway.Models.Customer;

namespace BreakAway.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Repository _repository;

        public CustomerController(Repository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            var viewModel = new IndexViewModel();

            viewModel.Customers = (from customer in _repository.Customers
                                   select new CustomerItem
                                       {
                                           Id = customer.Id,
                                           FirstName = customer.FirstName,
                                           LastName = customer.LastName,
                                           Type = customer.CustomerType,
                                           Title = customer.Title
                                       }).ToArray();

            return View(viewModel);
        }

        public ActionResult Edit(int id, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            var customer = _repository.Customers.FirstOrDefault(p => p.Id == id);

            if (customer == null)
            {
                return RedirectToAction("Index", "Customer");
            }

            var viewModel = new EditViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Title = customer.Title,
                CustomerTypeId = (int)customer.CustomerType,
                CustomerTypes = GetCustomerTypes(),
                Notes = customer.Notes,
                PrimaryActivityId = customer.PrimaryActivityId.Value,
                PrimaryDestinationId = customer.PrimaryDestinationId.Value,
                Activities = GetActivities(),
                Destinations = GetDestinations()
            };

            return View(viewModel);
        }

        private static IEnumerable<SelectListItem> GetCustomerTypes()
        {
            return from type in Enum.GetValues(typeof(CustomerType)).OfType<Enum>()
                select new SelectListItem
                {
                    Value = ((int)((CustomerType)type)).ToString(CultureInfo.InvariantCulture),
                    Text = type.ToString()
                };
        }

        private IEnumerable<SelectListItem> GetActivities()
        {
            return from activity in _repository.Activities.AsEnumerable()
                select new SelectListItem
                {
                    Value = activity.Id.ToString(CultureInfo.InvariantCulture),
                    Text = activity.Name
                };
        }

        private IEnumerable<SelectListItem> GetDestinations()
        {
            return from destination in _repository.Destinations.AsEnumerable()
                select new SelectListItem
                {
                    Value = destination.Id.ToString(CultureInfo.InvariantCulture),
                    Text = destination.Name
                };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit","Customer", model);
            }

            var customer = _repository.Customers.FirstOrDefault(p => p.Id == model.Id);

            if (customer == null)
            {
                return RedirectToAction("Index", "Customer", new { message = "Customer with id '" + model.Id + "' was not found" });
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Title = model.Title;
            customer.CustomerType = (CustomerType)model.CustomerTypeId;
            customer.Notes = model.Notes;
            customer.PrimaryActivity = _repository.Activities.FirstOrDefault(p => p.Id == model.PrimaryActivityId);
            customer.PrimaryDestination = _repository.Destinations.FirstOrDefault(p => p.Id == model.PrimaryDestinationId);

            _repository.Save();

            return RedirectToAction("Edit","Customer", new { id = customer.Id,message = "Changes saved successfully" });
        }

        public ActionResult Add()
        {
            var viewModel = new AddViewModel
            {
                CustomerTypes = GetCustomerTypes(),
                Activities = GetActivities(),
                Destinations = GetDestinations()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add", "Customer", new { message = "Customer not created" });
            }

            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Title = model.Title,
                CustomerType = (CustomerType)model.CustomerTypeId,
                Notes = model.Notes,
                PrimaryActivity = _repository.Activities.FirstOrDefault(p => p.Id == model.PrimaryActivityId),
                PrimaryDestination = _repository.Destinations.FirstOrDefault(p => p.Id == model.PrimaryDestinationId),
                ModifiedDate = DateTime.Now,
                AddDate = DateTime.Now
            };

            _repository.Customers.Add(customer);
            _repository.Save();

            return RedirectToAction("index", "Customer", new { message = "Customer added successfully" });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var customer = _repository.Customers.FirstOrDefault(p => p.Id == id);

            if (customer != null)
            {
                _repository.Customers.Delete(customer);
                _repository.Save();
            }

            return RedirectToAction("Index","Customer");
        }
    }
}
