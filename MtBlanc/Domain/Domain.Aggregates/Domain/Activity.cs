using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Studentum.Domain;
using Studentum.Infrastructure.Caching;

namespace BreakAway.Domain
{
    public class Activity : ICachedEntity
    {
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(100)]
        public string ImagePath { get; set; }

        [StringLength(50), Required]
        public string Category { get; set; }

        public int Key
        {
            get { return Id; }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}