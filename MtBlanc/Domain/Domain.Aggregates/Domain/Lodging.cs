using System.ComponentModel.DataAnnotations;
using Studentum.Domain;

namespace BreakAway.Domain
{
    public class Lodging : IEntity
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        public int ContactId { get; set; }
        
        public int LocationId { get; set; }

        public virtual Contact Contact { get; set; }
        
        public virtual Destination Location { get; set; }
    }
}