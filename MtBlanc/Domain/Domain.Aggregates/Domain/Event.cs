using System;
using System.Collections.Generic;
using Studentum.Domain;

namespace BreakAway.Domain
{
    public class Event : IEntity
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int LodgingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TripCostUSD { get; set; }

        public virtual Destination Location { get; set; }
        public virtual Lodging Lodging { get; set; }
        public virtual IList<Activity> Activities { get; set; }
    }
}