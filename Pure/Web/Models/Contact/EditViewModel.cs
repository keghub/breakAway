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
        public List<AddressIndexViewModel> Addresses { get; set; }

    }

    public class AddressIndexViewModel
    {
        [Key]
        public int Id { get; set; }

        public string CountryRegion { get; set; }

        public string PostalCode { get; set; }

        public string AddressType { get; set; }

    }
}