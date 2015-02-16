using System;
using System.Linq;
using System.Web.Mvc;
using BreakAway.Domain;

namespace BreakAway.Models.Customer
{
    public class EditViewModel
    {
        public EditViewModel()
        {
            CustomerTypes = new SelectList(Enum.GetValues(typeof(CustomerType)).OfType<CustomerType>().Select(ct => new { Value = (int)ct, Text = ct.ToString() }), "Value", "Text");            
        }

        public SelectList CustomerTypes { get; private set; }

        public SelectList Activities { get; set; }

        public SelectList Destinations { get; set; }

        public CustomerModel Customer { get; set; }
    }
}