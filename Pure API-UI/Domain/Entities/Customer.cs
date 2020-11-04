using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BreakAway.Entities
{
    public class Customer
    {
        public int ContactId { get; set; }
        public DateTime? InitialDate { get; set; }

        public CustomerType CustomerType { get; set; }

        public int? PrimaryDestinationId { get; set; }

        public int? SecondaryDestinationId { get; set; }

        public int? PrimaryActivityId { get; set; }

        public int? SecondaryActivityId { get; set; }

        public string Notes { get; set; }

        public byte[] CustomerRowVersion { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Activity PrimaryActivity { get; set; }

        public virtual Activity SecondaryActivity { get; set; }

        public virtual Destination PrimaryDestination { get; set; }

        public virtual Destination SecondaryDestination { get; set; }
    }

    public enum CustomerType
    {
        Standard = 1,
        Silver = 2,
        Gold = 3
    }
}