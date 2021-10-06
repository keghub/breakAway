using System;

namespace Domain
{
    public class Deal
    {
        public int Id { get; set; }
        public int SalesEmployeeId { get; set; }
        public string CustomerName { get; set; }
        public DateTime AgreementDate { get; set; }
        public decimal Price { get; set; }
    }
}