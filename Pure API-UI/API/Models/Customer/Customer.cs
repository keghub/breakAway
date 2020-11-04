using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;
using BreakAway.Entities;

namespace BreakAway.Models
{
    public class CustomerList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerType Type { get; set; }
    }

    public class Customer
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int CustomerTypeId { get; set; }
        public IEnumerable<IdValue> CustomerTypes { get; set; }
        public string Notes { get; set; }
        public int PrimaryActivityId { get; set; }
        public int PrimaryDestinationId { get; set; }
        public IEnumerable<IdValue> Activities { get; set; }
        public IEnumerable<IdValue> Destinations { get; set; }
    }
}