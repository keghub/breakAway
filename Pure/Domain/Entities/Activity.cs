using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BreakAway.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(100)]
        public string ImagePath { get; set; }

        [StringLength(50), Required]
        public string Category { get; set; }
        
        public virtual IList<Equipment> Equipments { get; set; }
    }
}