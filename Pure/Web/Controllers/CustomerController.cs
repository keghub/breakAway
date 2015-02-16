using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            IndexViewModel viewModel = new IndexViewModel();
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

            var activities = _repository.Activities.AsEnumerable();
            var destinations = _repository.Destinations.AsEnumerable();

            EditViewModel viewModel = new EditViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Title = customer.Title,
                CustomerTypeId = (int)customer.CustomerType,
                CustomerTypes = from type in Enum.GetValues(typeof(CustomerType)).OfType<Enum>()
                                select new SelectListItem
                                {
                                    Value = ((int)((CustomerType)type)).ToString(),
                                    Text = type.ToString()
                                },
                Notes = customer.Notes,
                PrimaryActivityId = customer.PrimaryActivityId.Value,
                PrimaryDestinationId = customer.PrimaryDestinationId.Value,
                Activities = from activity in activities
                             select new SelectListItem
                             {
                                 Value = activity.Id.ToString(),
                                 Text = activity.Name
                             },
                Destinations = from destination in destinations
                               select new SelectListItem
                               {
                                   Value = destination.Id.ToString(),
                                   Text = destination.Name
                               }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit","Customer", model);
            }
            else
            {
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
        }

        public ActionResult Add()
        {
            var activities = _repository.Activities.AsEnumerable();
            var destinations = _repository.Destinations.AsEnumerable();

            AddViewModel viewModel = new AddViewModel
            {
                CustomerTypes = from type in Enum.GetValues(typeof(CustomerType)).OfType<Enum>()
                                select new SelectListItem
                                {
                                    Value = ((int)((CustomerType)type)).ToString(),
                                    Text = type.ToString()
                                },
                Activities = from activity in activities
                             select new SelectListItem
                             {
                                 Value = activity.Id.ToString(),
                                 Text = activity.Name
                             },
                Destinations = from destination in destinations
                               select new SelectListItem
                               {
                                   Value = destination.Id.ToString(),
                                   Text = destination.Name
                               }
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
            else
            {
                Customer customer = new Customer
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
            }

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
