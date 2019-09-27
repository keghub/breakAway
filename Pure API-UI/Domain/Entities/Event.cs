using System;
using System.Collections.Generic;

namespace BreakAway.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int LodgingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TripCostUSD { get; set; }

        public virtual Destination Location { get; set; }
        public virtual Lodging Lodging { get; set; }
        public virtual IList<EventActivity> Activities { get; set; }
    }
}