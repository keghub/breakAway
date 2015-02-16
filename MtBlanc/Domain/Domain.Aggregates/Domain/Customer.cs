using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BreakAway.Domain
{
    public class Customer : Contact
    {
        public int CustomerTypeId { get; set; }

        public DateTime? InitialDate { get; set; }

        public CustomerType CustomerType
        {
            get { return (CustomerType)CustomerTypeId; }
            set { CustomerTypeId = (int) value; }
        }

        public int? PrimaryDestinationId { get; set; }

        public int? SecondaryDestinationId { get; set; }

        public int? PrimaryActivityId { get; set; }

        public int? SecondaryActivityId { get; set; }

        public string Notes { get; set; }

        public byte[] CustomerRowVersion { get; set; }

        public virtual Activity PrimaryActivity { get; set; }

        public virtual Activity SecondaryActivity { get; set; }

        public virtual Destination PrimaryDestination { get; set; }

        public virtual Destination SecondaryDestination { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? HeightInches { get; set; }

        public int? WeightPounds { get; set; }

        [StringLength(250)]
        public string DietaryRestrictions { get; set; }

        public virtual IList<Reservation> Reservations { get; set; }
    }

    public enum CustomerType
    {
        Standard = 1,
        Silver = 2,
        Gold = 3
    }
}