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
        [UrlRoute(Path = "customer")]
        public ActionResult Index(IndexViewModel.Form filter = null, string sortExpression = null, int page = 1, string message = null)
        {
            CustomerRepository repository = RepositoryFactory.GetReadOnlyRepository<CustomerRepository>();
            filter = filter ?? new IndexViewModel.Form();

            if (!string.IsNullOrWhiteSpace(message))
                ViewBag.Message = message;

            int pageSize = 25;
            int skipIndex = (page - 1)*pageSize;

            IQueryable<Customer> customers = repository.Items;

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                customers = customers.Where(c => c.FirstName.StartsWith(filter.FirstName));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                customers = customers.Where(c => c.LastName.StartsWith(filter.LastName));

            if (filter.CustomerType.HasValue)
            {
                int customerTypeId = (int)filter.CustomerType.Value;
                customers = customers.Where(c => c.CustomerTypeId == customerTypeId);
            }

            int totalItems = customers.Count();

            customers = customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);

            customers = customers.Skip(skipIndex).Take(pageSize);

            var items = customers.Select(c => new IndexViewModel.CustomerItem
            {
                Id = c.Id,
                FirstName = c.FirstName.Trim(),
                LastName = c.LastName.Trim(),
                Type = (CustomerType)c.CustomerTypeId,
                PrimaryActivity = c.PrimaryActivity.Name,
                SecondaryActivity = c.SecondaryActivity.Name
            });

            IndexViewModel viewModel = new IndexViewModel {Paging = new PagingInfo(page, totalItems, pageSize), Filter = filter, Customers = items.ToArray()};

            return View(viewModel);
        }

    }
}
