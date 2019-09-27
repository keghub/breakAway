using System;
using System.Collections.Generic;

namespace BreakAway.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ContactId { get; set; }
        public int? EventId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Event Event { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}