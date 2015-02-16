using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreakAway.Entities;

namespace BreakAway.Models.Customer
{
    public class IndexViewModel
    {
        public CustomerItem[] Customers { get; set; }
    }

    public class CustomerItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerType Type { get; set; }
    }
}