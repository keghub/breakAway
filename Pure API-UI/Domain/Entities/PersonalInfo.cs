using System;
using System.ComponentModel.DataAnnotations;

namespace BreakAway.Entities
{
    public class PersonalInfo
    {
        public int ContactId { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public int? HeightInches { get; set; }

        public int? WeightPounds { get; set; }

        [StringLength(250)]
        public string DietaryRestrictions { get; set; }
    }
}