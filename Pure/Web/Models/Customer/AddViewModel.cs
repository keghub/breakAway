using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BreakAway.Models.Customer
{
    public class AddViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public int CustomerTypeId { get; set; }

        public IEnumerable<SelectListItem> CustomerTypes { get; set; }

        public string Notes { get; set; }

        public int PrimaryActivityId { get; set; }

        public IEnumerable<SelectListItem> Activities { get; set; }

        public int PrimaryDestinationId { get; set; }

        public IEnumerable<SelectListItem> Destinations { get; set; }
    }
}