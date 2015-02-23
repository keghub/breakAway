using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BreakAway.Domain;
using BreakAway.Models;
using BreakAway.Models.Customer;
using ITCloud.Web.Routing;
using Studentum.Infrastructure.Repository;
using Studentum.Infrastructure.Repository.Specifications;

namespace BreakAway.Web.Controllers
{
    public class CustomerController : Controller
    {
        private const int PageSize = 25;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerController()
        {
            _customerRepository = RepositoryFactory.GetReadOnlyRepository<CustomerRepository>();
        }

        [UrlRoute(Path = "customer")]
        public ActionResult Index(IndexViewModel.Form filter = null, string sortExpression = null, int page = 1, string message = null)
        {
            filter = filter ?? new IndexViewModel.Form();

            if (!string.IsNullOrWhiteSpace(message))
                ViewBag.Message = message;

            var skipIndex = (page - 1)*PageSize;

            int totalItems;

            var customers = GetCustomers(filter, skipIndex, out totalItems);

            var items = TransofmToCustomerItem(customers);

            var viewModel = new IndexViewModel {Paging = new PagingInfo(page, totalItems, PageSize), Filter = filter, Customers = items.ToArray()};

            return View(viewModel);
        }

        private IQueryable<Customer> GetCustomers(IndexViewModel.Form filter, int skipIndex, out int totalItems)
        {
            var customers = _customerRepository.Items;

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                customers = customers.Where(c => c.FirstName.StartsWith(filter.FirstName));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                customers = customers.Where(c => c.LastName.StartsWith(filter.LastName));

            if (filter.CustomerType.HasValue)
            {
                var customerTypeId = (int) filter.CustomerType.Value;
                customers = customers.Where(c => c.CustomerTypeId == customerTypeId);
            }

            totalItems = customers.Count();

            customers = customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);

            customers = customers.Skip(skipIndex).Take(PageSize);
            return customers;
        }

        private static IEnumerable<IndexViewModel.CustomerItem> TransofmToCustomerItem(IQueryable<Customer> customers)
        {
            var items = customers.Select(c => new IndexViewModel.CustomerItem
            {
                Id = c.Id,
                FirstName = c.FirstName.Trim(),
                LastName = c.LastName.Trim(),
                Type = (CustomerType) c.CustomerTypeId,
                PrimaryActivity = c.PrimaryActivity.Name,
                SecondaryActivity = c.SecondaryActivity.Name
            });
            return items.ToList();
        }
    }
} 
