using System;
using System.Collections.Generic;
using Studentum.Domain;

namespace BreakAway.Domain
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int CustomerId { get; set; }
        public int? EventId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Event Event { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}