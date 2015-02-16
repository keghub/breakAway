using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BreakAway.Domain;

namespace BreakAway.Models.Customer
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            CustomerTypes = new SelectList(Enum.GetValues(typeof(CustomerType)).OfType<CustomerType>().Select(ct => new {Value = (int) ct, Text = ct.ToString()}), "Value", "Text");
        }

        public Form Filter { get; set; }

        public PagingInfo Paging { get; set; }

        public IEnumerable<CustomerItem> Customers { get; set; }

        public SelectList CustomerTypes { get; private set; }

        public class Form
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public CustomerType? CustomerType { get; set; }
        }

        public class CustomerItem
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public CustomerType Type { get; set; }
            public string PrimaryActivity { get; set; }
            public string SecondaryActivity { get; set; }
        }
    }
}