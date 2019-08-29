using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BreakAway.Models.Contact
{
    public class EditViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        //[ForeignKey("ContactId")]
        public List<AddressViewModel> Addresses { get; set; }
    }

    public class AddressViewModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string CountryRegion { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(50), Required]
        public string AddressType { get; set; }

        public MailModel Mail { get; set; }

        public DateTime ModifiedDate { get; set; }

    }

    public class MailModel
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