using System;

namespace BreakAway.Entities
{
    public class Payment
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