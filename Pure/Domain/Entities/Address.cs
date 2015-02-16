using System;
using System.ComponentModel.DataAnnotations;

namespace BreakAway.Entities
{
    public class Address
    {
        public int Id { get; set; }
        
        public Mail Mail { get; set; }

        [StringLength(50)]
        public string CountryRegion { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(50), Required]
        public string AddressType { get; set; }

        public int ContactId { get; set; }
        
        public DateTime ModifiedDate { get; set; }
        
        public byte[] RowVersion { get; set; }

        public virtual Contact Contact { get; set; }
    }

    public class Mail
    {
        [StringLength(50)]
        public string Street1 { get; set; }

        [StringLength(50)]
        public string Street2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string StateProvince { get; set; }
    }
}