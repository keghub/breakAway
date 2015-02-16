using System;
using System.ComponentModel.DataAnnotations;
using BreakAway.Domain;

namespace BreakAway.Models.Customer
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50), Required]
        public string FirstName { get; set; }

        [StringLength(50), Required]
        public string LastName { get; set; }

        [Required]
        public int? CustomerTypeId { get; set; }
        
        public int? PrimaryActivity { get; set; }
        
        public int? SecondaryActivity { get; set; }
        
        public int? PrimaryDestination { get; set; }
        
        public int? SecondaryDestination { get; set; }
        
        public DateTime AddDate { get; set; }
        
        public DateTime? InitialDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
        
        public string Notes { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public int? Height { get; set; }
        
        public int? Weight { get; set; }

        [StringLength(250)]
        public string DietaryRestrictions { get; set; }
    }

}