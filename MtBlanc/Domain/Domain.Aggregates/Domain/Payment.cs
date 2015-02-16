using System;
using Studentum.Domain;

namespace BreakAway.Domain
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}