using System;
using System.ComponentModel.DataAnnotations;

namespace BreakAway.Models.Address
{
    public class EditViewModel
    {
        //[Key]
        public int Id { get; set; }

        public Mail Mail { get; set; }

        public string CountryRegion { get; set; }

        public string PostalCode { get; set; }

        public string AddressType { get; set; }

        public DateTime ModifiedDate { get; set; }

    }

    public class Mail
    {
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string StateProvince { get; set; }
    }
}