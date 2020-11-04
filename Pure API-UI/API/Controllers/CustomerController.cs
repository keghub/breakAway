using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using BreakAway.Entities;
using BreakAway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreakAway.Controllers
{
    public class CustomerController : ControllerBase
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

        [HttpGet]
        [Route("[controller]")]
        public async Task<ActionResult<CustomerList[]>> Get()
        {
            var models = await _repository.Customers.Select(c => new CustomerList()
            {
                Id = c.ContactId,
                FirstName = c.Contact.FirstName.Trim(),
                LastName = c.Contact.LastName.Trim(),
                Type = c.CustomerType,
                Title = c.Contact.Title.Trim()
            })
            .ToArrayAsync();

            return models;
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<ActionResult<Models.Customer>> Get(int id)
        {            
            var customer = await _repository.Customers.FirstOrDefaultAsync(p => p.ContactId == id);

            if (customer == null)
            {
                return BadRequest("Unable to find customer");
            }

            var model = new Models.Customer
            {
                Id = customer.ContactId,
                FirstName = customer.Contact.FirstName.Trim(),
                LastName = customer.Contact.LastName.Trim(),
                Title = customer.Contact.Title.Trim(),
                CustomerTypeId = (int)customer.CustomerType,
                CustomerTypes = GetCustomerTypes(),
                Notes = customer.Notes.Trim(),
                PrimaryActivityId = customer.PrimaryActivityId.Value,
                PrimaryDestinationId = customer.PrimaryDestinationId.Value,
                Activities = GetActivities(),
                Destinations = GetDestinations()
            };

            return model;

        }

        private static IEnumerable<IdValue> GetCustomerTypes()
        {
            return from type in Enum.GetValues(typeof(CustomerType)).OfType<Enum>()
                select new IdValue
                {
                    Id = (int)((CustomerType)type),
                    Value = type.ToString().Trim()
                };
        }

        private IEnumerable<IdValue> GetActivities()
        {
            return from activity in _repository.Activities.AsEnumerable()
                select new IdValue
                {
                    Id = activity.Id,
                    Value = activity.Name.Trim()
                };
        }

        private IEnumerable<IdValue> GetDestinations()
        {
            return from destination in _repository.Destinations.AsEnumerable()
                select new IdValue
                {
                    Id = destination.Id,
                    Value = destination.Name.Trim()
                };
        }

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] Models.Customer model)
        {
            Entities.Customer customer;

            if (!model.Id.HasValue)
            {
                customer = new Entities.Customer
                {
                    Contact = new Contact
                    {
                        AddDate = DateTime.UtcNow,
                    }
                };
                _repository.Customers.Add(customer);
            }
            else
            {
                customer = await _repository.Customers.FirstOrDefaultAsync(p => p.ContactId == model.Id);
            }

            customer.Contact.FirstName = model.FirstName;
            customer.Contact.LastName = model.LastName;
            customer.Contact.Title = model.Title;
            customer.CustomerType = (CustomerType)model.CustomerTypeId;
            customer.Notes = model.Notes;
            customer.PrimaryActivity = _repository.Activities.FirstOrDefault(p => p.Id == model.PrimaryActivityId);
            customer.PrimaryDestination = _repository.Destinations.FirstOrDefault(p => p.Id == model.PrimaryDestinationId);
            customer.Contact.ModifiedDate = DateTime.UtcNow;

            _repository.Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _repository.Customers.FirstOrDefaultAsync(p => p.ContactId == id);
            if (customer == null)
            {
                return BadRequest("Unable to find customer");
            }

            _repository.Customers.Delete(customer);
            _repository.Save();

            return Ok();
        }
    }
}
